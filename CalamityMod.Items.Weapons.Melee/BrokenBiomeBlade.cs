using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.DataStructures;
using CalamityMod.Items.BaseItems;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "BiomeBlade" })]
public class BrokenBiomeBlade : CustomUseProjItem, ILocalizedModType, IModType
{
	public Attunement mainAttunement;

	public Attunement secondaryAttunement;

	public int Combo;

	public float ComboResetTimer;

	public int CanLunge = 1;

	public static int BaseDamage;

	public static int DefaultAttunement_BaseDamage;

	public static int EvilAttunement_BaseDamage;

	public static int EvilAttunement_Lifesteal;

	public static int EvilAttunement_BounceIFrames;

	public static int ColdAttunement_BaseDamage;

	public static float ColdAttunement_ThirdSwingBoost;

	public static int HotAttunement_BaseDamage;

	public static int HotAttunement_ShredPlayerIFrames;

	public static int HotAttunement_LocalIFrames;

	public static float HotAttunement_ShredDecayRate;

	internal static ChargingEnergyParticleSet BiomeEnergyParticles;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		if (list == null)
		{
			return;
		}
		SafeCheckAttunements();
		if (Main.LocalPlayer == null)
		{
			return;
		}
		TooltipLine effectDescTooltip = list.FirstOrDefault((TooltipLine x) => x.Text.Contains("[FUNC]") && x.Mod == "Terraria");
		TooltipLine mainAttunementTooltip = list.FirstOrDefault((TooltipLine x) => x.Text.Contains("[ATT1]") && x.Mod == "Terraria");
		TooltipLine secondaryAttunementTooltip = list.FirstOrDefault((TooltipLine x) => x.Text.Contains("[ATT2]") && x.Mod == "Terraria");
		if (effectDescTooltip != null)
		{
			effectDescTooltip.Text = this.GetLocalizedValue("DefaultFunction");
			effectDescTooltip.OverrideColor = new Color(163, 163, 163);
		}
		if (mainAttunement != null)
		{
			if (effectDescTooltip != null)
			{
				effectDescTooltip.Text = Lang.SupportGlyphs(mainAttunement.FunctionText.ToString());
				effectDescTooltip.OverrideColor = mainAttunement.tooltipColor;
			}
			if (mainAttunementTooltip != null)
			{
				mainAttunementTooltip.Text = mainAttunementTooltip.Text.Replace("ATT1", mainAttunement.AttunementName.ToString());
				mainAttunementTooltip.OverrideColor = mainAttunement.tooltipColor;
			}
		}
		else if (mainAttunementTooltip != null)
		{
			mainAttunementTooltip.Text = mainAttunementTooltip.Text.Replace("ATT1", Language.GetTextValue("LegacyInterface.23"));
			mainAttunementTooltip.OverrideColor = new Color(163, 163, 163);
		}
		if (secondaryAttunement != null && secondaryAttunementTooltip != null)
		{
			secondaryAttunementTooltip.Text = secondaryAttunementTooltip.Text.Replace("ATT2", secondaryAttunement.AttunementName.ToString());
			secondaryAttunementTooltip.OverrideColor = Color.Lerp(secondaryAttunement.tooltipColor, Color.Gray, 0.5f);
		}
		else if (secondaryAttunementTooltip != null)
		{
			secondaryAttunementTooltip.Text = secondaryAttunementTooltip.Text.Replace("ATT2", Language.GetTextValue("LegacyInterface.23"));
			secondaryAttunementTooltip.OverrideColor = new Color(163, 163, 163);
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 36);
		base.Item.damage = BaseDamage;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 30;
		base.Item.useTime = 30;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.shoot = 10;
		base.Item.knockBack = 5f;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.shootSpeed = 12f;
	}

	public override ModItem Clone(Item item)
	{
		ModItem clone = base.Clone(item);
		if (Main.mouseItem.type == ModContent.ItemType<BrokenBiomeBlade>())
		{
			item.ModItem?.HoldItem(Main.LocalPlayer);
		}
		if (clone is BrokenBiomeBlade a && item.ModItem is BrokenBiomeBlade a2)
		{
			a.mainAttunement = a2.mainAttunement;
			a.secondaryAttunement = a2.secondaryAttunement;
		}
		if (clone.Item.prefix == 39)
		{
			clone.Item.Prefix(81);
			clone.Item.prefix = 81;
		}
		return clone;
	}

	public override void SaveData(TagCompound tag)
	{
		int attunement1 = ((mainAttunement == null) ? (-1) : ((int)mainAttunement.id));
		int attunement2 = ((secondaryAttunement == null) ? (-1) : ((int)secondaryAttunement.id));
		tag["mainAttunement"] = attunement1;
		tag["secondaryAttunement"] = attunement2;
	}

	public override void LoadData(TagCompound tag)
	{
		int attunement1 = tag.GetInt("mainAttunement");
		int attunement2 = tag.GetInt("secondaryAttunement");
		mainAttunement = AttunementSystem.FindOrNull(attunement1);
		secondaryAttunement = AttunementSystem.FindOrNull(attunement2);
		if (mainAttunement == secondaryAttunement)
		{
			secondaryAttunement = null;
		}
		SafeCheckAttunements();
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write((mainAttunement != null) ? ((int)mainAttunement.id) : AttunementSystem.EmptyID);
		writer.Write((secondaryAttunement != null) ? ((int)secondaryAttunement.id) : AttunementSystem.EmptyID);
	}

	public override void NetReceive(BinaryReader reader)
	{
		mainAttunement = AttunementSystem.FindOrNull(reader.ReadInt32());
		secondaryAttunement = AttunementSystem.FindOrNull(reader.ReadInt32());
	}

	public override void ModifyWeaponDamage(Player player, ref StatModifier damage)
	{
		damage += (mainAttunement?.DamageMultiplier ?? 1f) - 1f;
	}

	public void SafeCheckAttunements()
	{
		if (mainAttunement != null)
		{
			mainAttunement = AttunementSystem.FindOrNull(ClampAttunementRange((int)mainAttunement.id));
		}
		if (secondaryAttunement != null)
		{
			secondaryAttunement = AttunementSystem.FindOrNull(ClampAttunementRange((int)secondaryAttunement.id));
		}
		if (mainAttunement == secondaryAttunement)
		{
			secondaryAttunement = null;
		}
	}

	private static int ClampAttunementRange(int input)
	{
		if (input < 0)
		{
			return 0;
		}
		if (input > 3)
		{
			return 3;
		}
		return input;
	}

	public override void HoldItem(Player player)
	{
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		IEntitySource source = player.GetSource_ItemUse(base.Item);
		player.Calamity().rightClickListener = true;
		player.Calamity().mouseWorldListener = true;
		if (player.velocity.Y == 0f)
		{
			CanLunge = 1;
		}
		if (mainAttunement == null)
		{
			base.Item.noUseGraphic = false;
			base.Item.useStyle = 1;
			base.Item.noMelee = false;
			base.Item.channel = false;
			base.Item.shoot = 10;
			base.Item.shootSpeed = 12f;
			base.Item.UseSound = SoundID.Item1;
			Combo = 0;
		}
		else
		{
			mainAttunement.ApplyStats(base.Item);
		}
		if (mainAttunement != null && mainAttunement.id != AttunementID.Cold)
		{
			Combo = 0;
		}
		if (player.Calamity().mouseRight && CanUseItem(player) && player.whoAmI == Main.myPlayer && !Main.mapFullscreen && !Main.blockMouse && !Main.projectile.Any((Projectile n) => n.active && n.type == ModContent.ProjectileType<BrokenBiomeBladeHoldout>() && n.owner == player.whoAmI))
		{
			bool mayAttune = player.StandingStill() && !player.mount.Active && player.CheckSolidGround(1, 3);
			Vector2 displace = default(Vector2);
			((Vector2)(ref displace))._002Ector(18f, 0f);
			Projectile.NewProjectile(source, player.Top + displace, Vector2.Zero, ModContent.ProjectileType<BrokenBiomeBladeHoldout>(), 0, 0f, player.whoAmI, mayAttune ? 0f : 1f);
		}
	}

	public override void UpdateInventory(Player player)
	{
		SafeCheckAttunements();
		if (mainAttunement != null && mainAttunement.id == AttunementID.Cold && CanUseItem(player))
		{
			ComboResetTimer -= 0.02f;
		}
		if (ComboResetTimer < 0f)
		{
			Combo = 0;
		}
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 0)
		{
			return !Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && (n.type == ModContent.ProjectileType<BitingEmbrace>() || n.type == ModContent.ProjectileType<AridGrandeur>()));
		}
		return false;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		if (mainAttunement == null || player.altFunctionUse != 0)
		{
			return false;
		}
		int powerLungeCounter = 0;
		ComboResetTimer = 1f;
		return mainAttunement.Shoot(player, source, ref position, ref velocity.X, ref velocity.Y, ref type, ref damage, ref knockback, ref Combo, ref CanLunge, ref powerLungeCounter);
	}

	internal static void UpdateAllParticleSets()
	{
		BiomeEnergyParticles.Update();
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		Texture2D itemTexture = TextureAssets.Item[base.Type].Value;
		Rectangle itemFrame = ((Main.itemAnimations[base.Type] == null) ? itemTexture.Frame() : Main.itemAnimations[base.Type].GetFrame(itemTexture));
		if (mainAttunement == null)
		{
			return true;
		}
		Vector2 particleDrawCenter = position + new Vector2(12f, 16f) * Main.inventoryScale - frame.Size() * 0.3f;
		BiomeEnergyParticles.EdgeColor = mainAttunement.energyParticleEdgeColor;
		BiomeEnergyParticles.CenterColor = mainAttunement.energyParticleCenterColor;
		BiomeEnergyParticles.InterpolationSpeed = 0.1f;
		BiomeEnergyParticles.DrawSet(particleDrawCenter + Main.screenPosition);
		Vector2 displacement = Vector2.UnitX.RotatedBy(Main.GlobalTimeWrappedHourly * 3f) * 2f * (float)Math.Sin(Main.GlobalTimeWrappedHourly);
		spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.Additive, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
		spriteBatch.Draw(itemTexture, position + displacement, (Rectangle?)itemFrame, BiomeEnergyParticles.CenterColor, 0f, origin, scale, (SpriteEffects)0, 0f);
		spriteBatch.Draw(itemTexture, position - displacement, (Rectangle?)itemFrame, BiomeEnergyParticles.CenterColor, 0f, origin, scale, (SpriteEffects)0, 0f);
		spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyWoodenSword").AddIngredient(ModContent.ItemType<AerialiteBar>(), 10).AddIngredient(175, 10)
			.AddIngredient(2, 50)
			.AddIngredient(3, 50)
			.AddTile(16)
			.Register();
	}

	static BrokenBiomeBlade()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		BaseDamage = 38;
		DefaultAttunement_BaseDamage = 38;
		EvilAttunement_BaseDamage = 50;
		EvilAttunement_Lifesteal = 2;
		EvilAttunement_BounceIFrames = 10;
		ColdAttunement_BaseDamage = 40;
		ColdAttunement_ThirdSwingBoost = 1.15f;
		HotAttunement_BaseDamage = 42;
		HotAttunement_ShredPlayerIFrames = 6;
		HotAttunement_LocalIFrames = 30;
		HotAttunement_ShredDecayRate = 1f;
		BiomeEnergyParticles = new ChargingEnergyParticleSet(-1, 2, Color.DarkViolet, Color.White, 0.04f, 20f);
	}
}
