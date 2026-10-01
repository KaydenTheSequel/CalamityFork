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
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace CalamityMod.Items.Weapons.Melee;

public class TrueBiomeBlade : CustomUseProjItem, ILocalizedModType, IModType
{
	public Attunement mainAttunement;

	public Attunement secondaryAttunement;

	public int Combo;

	public float ComboResetTimer;

	public int StoredLunges = 2;

	public int PowerLungeCounter;

	public static int BaseDamage;

	public static int DefaultAttunement_BaseDamage;

	public static int DefaultAttunement_SigilTime;

	public static int DefaultAttunement_BeamTime;

	public static float DefaultAttunement_LungeDamageMult;

	public static int DefaultAttunement_LungeIFrames;

	public static float DefaultAttunement_HomingAngle;

	public static int EvilAttunement_BaseDamage;

	public static int EvilAttunement_Lifesteal;

	public static int EvilAttunement_BounceIFrames;

	public static float EvilAttunement_SlashDamageBoost;

	public static int EvilAttunement_SlashIFrames;

	public static int ColdAttunement_BaseDamage;

	public static float ColdAttunement_ThirdSwingBoost;

	public static float ColdAttunement_MistDamageReduction;

	public static int HotAttunement_BaseDamage;

	public static int HotAttunement_PlayerShredIFrames;

	public static float HotAttunement_ShotDamageBoost;

	public static int HotAttunement_LocalIFrames;

	public static float HotAttunement_ShredDecayRate;

	public static int TropicalAttunement_BaseDamage;

	public static float TropicalAttunement_ChainDamageReduction;

	public static float TropicalAttunement_VineDamageReduction;

	public static float TropicalAttunement_SweetSpotDamageMultiplier;

	public static int TropicalAttunement_LocalIFrames;

	public static int HolyAttunement_BaseDamage;

	public static float HolyAttunement_BaseSwingDamageMult;

	public static float HolyAttunement_FullSwingDamageMult;

	public static float HolyAttunement_ThrowDamageBoost;

	public static int HolyAttunement_LocalIFrames;

	public static float HolyAttunement_MonolithDamage;

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
		base.Item.width = (base.Item.height = 68);
		base.Item.damage = BaseDamage;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 21;
		base.Item.useTime = 21;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 7.5f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLightPurpleBuyPrice;
		base.Item.rare = 6;
		base.Item.shoot = 10;
		base.Item.shootSpeed = 12f;
	}

	public override ModItem Clone(Item item)
	{
		ModItem modItem = base.Clone(item);
		if (Main.mouseItem.type == ModContent.ItemType<TrueBiomeBlade>())
		{
			item.ModItem?.HoldItem(Main.LocalPlayer);
		}
		if (modItem is TrueBiomeBlade a && item.ModItem is TrueBiomeBlade a2)
		{
			a.mainAttunement = a2.mainAttunement;
			a.secondaryAttunement = a2.secondaryAttunement;
		}
		return modItem;
	}

	public override void SaveData(TagCompound tag)
	{
		int attunement1 = ((mainAttunement == null) ? (-1) : ((int)mainAttunement.id));
		int attunement2 = ((secondaryAttunement == null) ? (-1) : ((int)secondaryAttunement.id));
		tag.Add("mainAttunement", attunement1);
		tag.Add("secondaryAttunement", attunement2);
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
		if (mainAttunement != null)
		{
			damage += (mainAttunement?.DamageMultiplier ?? 1f) - 1f;
		}
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
		if (input < 4)
		{
			return 4;
		}
		if (input > 9)
		{
			return 9;
		}
		return input;
	}

	public override void HoldItem(Player player)
	{
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().rightClickListener = true;
		player.Calamity().mouseWorldListener = true;
		if (player.velocity.Y == 0f)
		{
			StoredLunges = 2;
			if (PowerLungeCounter != 3)
			{
				PowerLungeCounter = 0;
			}
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
			Combo = 0;
			PowerLungeCounter = 0;
		}
		else
		{
			mainAttunement.ApplyStats(base.Item);
		}
		if (mainAttunement != null && mainAttunement.id != AttunementID.TrueCold && mainAttunement.id != AttunementID.TrueTropical)
		{
			Combo = 0;
		}
		if (player.Calamity().mouseRight && CanUseItem(player) && player.whoAmI == Main.myPlayer && !Main.mapFullscreen && !Main.projectile.Any((Projectile n) => n.active && n.type == ModContent.ProjectileType<BiomeBladeHoldout>() && n.owner == player.whoAmI))
		{
			IEntitySource source_ItemUse = player.GetSource_ItemUse(base.Item);
			bool mayAttune = player.StandingStill() && !player.mount.Active && player.CheckSolidGround(1, 3);
			Vector2 displace = default(Vector2);
			((Vector2)(ref displace))._002Ector(18f, 0f);
			Projectile.NewProjectile(source_ItemUse, player.Top + displace, Vector2.Zero, ModContent.ProjectileType<BiomeBladeHoldout>(), 0, 0f, player.whoAmI, mayAttune ? 0f : 1f);
		}
	}

	public override void UpdateInventory(Player player)
	{
		SafeCheckAttunements();
		if (mainAttunement != null && mainAttunement.id == AttunementID.TrueCold && CanUseItem(player))
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
			return !Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && (n.type == ModContent.ProjectileType<TrueBitingEmbrace>() || n.type == ModContent.ProjectileType<TrueGrovetendersTouch>() || n.type == ModContent.ProjectileType<TrueAridGrandeur>() || n.type == ModContent.ProjectileType<HeavensMight>()));
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
		ComboResetTimer = 1f;
		return mainAttunement.Shoot(player, source, ref position, ref velocity.X, ref velocity.Y, ref type, ref damage, ref knockback, ref Combo, ref StoredLunges, ref PowerLungeCounter);
	}

	internal static void UpdateAllParticleSets()
	{
		BiomeEnergyParticles.Update();
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D itemTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MendedBiomeBlade", (AssetRequestMode)2).Value;
		if (mainAttunement == null)
		{
			spriteBatch.Draw(itemTexture, position, (Rectangle?)null, drawColor, 0f, origin, scale, (SpriteEffects)0, 0f);
			return false;
		}
		Vector2 particleDrawCenter = position + new Vector2(12f, 16f) * Main.inventoryScale - frame.Size() * 0.18f;
		BiomeEnergyParticles.EdgeColor = mainAttunement.energyParticleEdgeColor;
		BiomeEnergyParticles.CenterColor = mainAttunement.energyParticleCenterColor;
		BiomeEnergyParticles.InterpolationSpeed = 0.1f;
		BiomeEnergyParticles.DrawSet(particleDrawCenter + Main.screenPosition);
		Vector2 displacement = Vector2.UnitX.RotatedBy(Main.GlobalTimeWrappedHourly * 3f) * 2f * (float)Math.Sin(Main.GlobalTimeWrappedHourly);
		spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
		spriteBatch.Draw(itemTexture, position + displacement, (Rectangle?)null, BiomeEnergyParticles.CenterColor, 0f, origin, scale, (SpriteEffects)0, 0f);
		spriteBatch.Draw(itemTexture, position - displacement, (Rectangle?)null, BiomeEnergyParticles.CenterColor, 0f, origin, scale, (SpriteEffects)0, 0f);
		spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
		spriteBatch.Draw(itemTexture, position, (Rectangle?)null, drawColor, 0f, origin, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		Texture2D itemTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MendedBiomeBlade", (AssetRequestMode)2).Value;
		spriteBatch.Draw(itemTexture, base.Item.Center - Main.screenPosition, (Rectangle?)null, lightColor, rotation, base.Item.Size * 0.5f, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BrokenBiomeBlade>().AddIngredient(547).AddIngredient(548)
			.AddIngredient(549)
			.AddIngredient(501, 2)
			.AddIngredient<StarblightSoot>(10)
			.AddTile(134)
			.Register();
	}

	static TrueBiomeBlade()
	{
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		BaseDamage = 115;
		DefaultAttunement_BaseDamage = 90;
		DefaultAttunement_SigilTime = 900;
		DefaultAttunement_BeamTime = 90;
		DefaultAttunement_LungeDamageMult = 2f;
		DefaultAttunement_LungeIFrames = 20;
		DefaultAttunement_HomingAngle = (float)Math.PI / 3f;
		EvilAttunement_BaseDamage = 155;
		EvilAttunement_Lifesteal = 2;
		EvilAttunement_BounceIFrames = 10;
		EvilAttunement_SlashDamageBoost = 3f;
		EvilAttunement_SlashIFrames = 60;
		ColdAttunement_BaseDamage = 140;
		ColdAttunement_ThirdSwingBoost = 1.25f;
		ColdAttunement_MistDamageReduction = 0.11f;
		HotAttunement_BaseDamage = 126;
		HotAttunement_PlayerShredIFrames = 8;
		HotAttunement_ShotDamageBoost = 4.5f;
		HotAttunement_LocalIFrames = 24;
		HotAttunement_ShredDecayRate = 0.65f;
		TropicalAttunement_BaseDamage = 132;
		TropicalAttunement_ChainDamageReduction = 0.6f;
		TropicalAttunement_VineDamageReduction = 0.3f;
		TropicalAttunement_SweetSpotDamageMultiplier = 1.5f;
		TropicalAttunement_LocalIFrames = 60;
		HolyAttunement_BaseDamage = 84;
		HolyAttunement_BaseSwingDamageMult = 0.5f;
		HolyAttunement_FullSwingDamageMult = 1f;
		HolyAttunement_ThrowDamageBoost = 3f;
		HolyAttunement_LocalIFrames = 20;
		HolyAttunement_MonolithDamage = 0.5f;
		BiomeEnergyParticles = new ChargingEnergyParticleSet(-1, 2, Color.White, Color.White, 0.04f, 20f);
	}
}
