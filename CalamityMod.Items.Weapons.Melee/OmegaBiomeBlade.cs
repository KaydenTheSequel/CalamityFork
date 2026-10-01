using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.DataStructures;
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

public class OmegaBiomeBlade : ModItem, ILocalizedModType, IModType
{
	public Attunement mainAttunement;

	public Attunement secondaryAttunement;

	public Projectile MeatHook;

	public int UseTimer;

	public bool OnHitProc;

	public static int BaseDamage;

	public static int WhirlwindAttunement_BaseDamage;

	public static int WhirlwindAttunement_LocalIFrames;

	public static float WhirlwindAttunement_EnergyDamageMult;

	public static float WhirlwindAttunement_BaseSwingDamageMult;

	public static float WhirlwindAttunement_FullSwingDamageMult;

	public static float WhirlwindAttunement_ThrowDamageBoost;

	public static float WhirlwindAttunement_MonolithDamageMult;

	public static int WhirlwindAttunement_PassiveBaseDamage;

	public static int SuperPogoAttunement_BaseDamage;

	public static int SuperPogoAttunement_PlayerShredIFrames;

	public static int SuperPogoAttunement_LocalIFrames;

	public static float SuperPogoAttunement_ShotDamageMult;

	public static float SuperPogoAttunement_SliceDamageMult;

	public static int SuperPogoAttunementSliceLifesteal;

	public static int SuperPogoAttunement_PlayerSliceIFrames;

	public static float SuperPogoAttunement_ShredDecayRate;

	public static int SuperPogoAttunement_PassiveLifeSteal;

	public static int ShockwaveAttunement_BaseDamage;

	public static float ShockwaveAttunement_BeamDamageMult;

	public static int ShockwaveAttunement_DashHitIFrames;

	public static float ShockwaveAttunement_FullChargeMult;

	public static int ShockwaveAttunement_SigilTime;

	public static float ShockwaveAttunement_MonolithDamageBoost;

	public static int ShockwaveAttunement_PassiveBaseDamage;

	public static int FlailBladeAttunement_BaseDamage;

	public static int FlailBladeAttunement_LocalIFrames;

	public static int FlailBladeAttunement_FlailTime;

	public static int FlailBladeAttunement_Reach;

	public static float FlailBladeAttunement_ChainDamageReduction;

	public static float FlailBladeAttunement_GhostChainDamageReduction;

	public static int FlailBladeAttunement_PassiveBaseDamage;

	public static float WhirlwindAttunement_WhirlwindProc;

	public static float WhirlwindAttunement_SwordThrowProc;

	public static float WhirlwindAttunement_MonolithProc;

	public static float SuperPogoAttunement_ShredderProc;

	public static float SuperPogoAttunement_WheelProc;

	public static float SuperPogoAttunement_DashProc;

	public static float ShockwaveAttunement_SwordProc;

	public static float ShockwaveAttunement_SwordBeamProc;

	public static float ShockwaveAttunement_BlastProc;

	public static float ShockwaveAttunement_ShockwaveProc;

	public static float FlailBladeAttunement_BladeProc;

	public static float FlailBladeAttunement_ChainProc;

	public static float FlailBladeAttunement_GhostChainProc;

	internal static ChargingEnergyParticleSet BiomeEnergyParticles;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
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
		TooltipLine passiveDescTooltip = list.FirstOrDefault((TooltipLine x) => x.Text.Contains("[PASS]") && x.Mod == "Terraria");
		TooltipLine mainAttunementTooltip = list.FirstOrDefault((TooltipLine x) => x.Text.Contains("[ATT1]") && x.Mod == "Terraria");
		TooltipLine secondaryAttunementTooltip = list.FirstOrDefault((TooltipLine x) => x.Text.Contains("[ATT2]") && x.Mod == "Terraria");
		if (effectDescTooltip != null)
		{
			effectDescTooltip.Text = this.GetLocalizedValue("DefaultFunction");
			effectDescTooltip.OverrideColor = new Color(163, 163, 163);
		}
		if (passiveDescTooltip != null)
		{
			passiveDescTooltip.Text = this.GetLocalizedValue("DefaultPassive");
			passiveDescTooltip.OverrideColor = new Color(163, 163, 163);
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
				mainAttunementTooltip.OverrideColor = Color.Lerp(mainAttunement.tooltipColor, mainAttunement.tooltipColor2, 0.5f + (float)Math.Sin(Main.GlobalTimeWrappedHourly) * 0.5f);
			}
		}
		else if (mainAttunementTooltip != null)
		{
			mainAttunementTooltip.Text = mainAttunementTooltip.Text.Replace("ATT1", Language.GetTextValue("LegacyInterface.23"));
			mainAttunementTooltip.OverrideColor = new Color(163, 163, 163);
		}
		if (secondaryAttunement != null)
		{
			if (passiveDescTooltip != null)
			{
				passiveDescTooltip.Text = secondaryAttunement.PassiveDesc.ToString();
				passiveDescTooltip.OverrideColor = secondaryAttunement.tooltipColor;
			}
			if (secondaryAttunementTooltip != null)
			{
				secondaryAttunementTooltip.Text = secondaryAttunementTooltip.Text.Replace("ATT2", secondaryAttunement.AttunementName.ToString());
				secondaryAttunementTooltip.OverrideColor = Color.Lerp(Color.Lerp(secondaryAttunement.tooltipColor, secondaryAttunement.tooltipColor2, 0.5f + (float)Math.Sin(Main.GlobalTimeWrappedHourly) * 0.5f), Color.Gray, 0.5f);
			}
		}
		else if (secondaryAttunementTooltip != null)
		{
			secondaryAttunementTooltip.Text = secondaryAttunementTooltip.Text.Replace("ATT2", Language.GetTextValue("LegacyInterface.23"));
			secondaryAttunementTooltip.OverrideColor = new Color(163, 163, 163);
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 92);
		base.Item.damage = BaseDamage;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useAnimation = 18;
		base.Item.useTime = 18;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 8f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
		base.Item.rare = 9;
		base.Item.shoot = 10;
		base.Item.shootSpeed = 15f;
	}

	public override ModItem Clone(Item item)
	{
		ModItem modItem = base.Clone(item);
		if (Main.mouseItem.type == ModContent.ItemType<OmegaBiomeBlade>())
		{
			item.ModItem?.HoldItem(Main.LocalPlayer);
		}
		if (modItem is OmegaBiomeBlade a && item.ModItem is OmegaBiomeBlade a2)
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
		if (input < 10)
		{
			return 10;
		}
		if (input > 13)
		{
			return 13;
		}
		return input;
	}

	public override void HoldItem(Player player)
	{
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().rightClickListener = true;
		player.Calamity().mouseWorldListener = true;
		if (CanUseItem(player))
		{
			player.Calamity().LungingDown = false;
		}
		else
		{
			UseTimer++;
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
		}
		else
		{
			mainAttunement.ApplyStats(base.Item);
		}
		if (player.whoAmI != Main.myPlayer)
		{
			return;
		}
		IEntitySource source = player.GetSource_ItemUse(base.Item);
		if (secondaryAttunement != null)
		{
			if (secondaryAttunement.id != AttunementID.FlailBlade || MeatHook == null || !MeatHook.active)
			{
				MeatHook = null;
			}
			if (secondaryAttunement.id == AttunementID.FlailBlade && MeatHook == null)
			{
				int damage = (int)player.GetTotalDamage<MeleeDamageClass>().ApplyTo(FlailBladeAttunement_PassiveBaseDamage);
				MeatHook = Projectile.NewProjectileDirect(source, player.Center, Vector2.Zero, ModContent.ProjectileType<ChainedMeatHook>(), damage, 0f, player.whoAmI);
			}
			secondaryAttunement.PassiveEffect(player, source, ref UseTimer, ref OnHitProc, MeatHook);
		}
		if (player.Calamity().mouseRight && CanUseItem(player) && player.whoAmI == Main.myPlayer && !Main.mapFullscreen && !Main.projectile.Any((Projectile n) => n.active && n.type == ModContent.ProjectileType<TrueBiomeBladeHoldout>() && n.owner == player.whoAmI))
		{
			Projectile.NewProjectile(source, player.Top, Vector2.Zero, ModContent.ProjectileType<TrueBiomeBladeHoldout>(), 0, 0f, player.whoAmI);
		}
	}

	public override void UpdateInventory(Player player)
	{
		SafeCheckAttunements();
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 0)
		{
			return !Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && (n.type == ModContent.ProjectileType<SwordsmithsPride>() || n.type == ModContent.ProjectileType<EarthenTides>() || n.type == ModContent.ProjectileType<SanguineFury>() || n.type == ModContent.ProjectileType<LamentationsOfTheChained>()));
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
		return true;
	}

	internal static void UpdateAllParticleSets()
	{
		BiomeEnergyParticles.Update();
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		if (mainAttunement == null)
		{
			return true;
		}
		position.Y -= 6f * scale;
		Texture2D itemTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/OmegaBiomeBladeExtra", (AssetRequestMode)2).Value;
		Rectangle itemFrame = ((Main.itemAnimations[base.Type] == null) ? itemTexture.Frame() : Main.itemAnimations[base.Type].GetFrame(itemTexture));
		Vector2 particleDrawCenter = position + new Vector2(12f, 16f) * Main.inventoryScale - frame.Size() * 0.12f;
		BiomeEnergyParticles.EdgeColor = mainAttunement.tooltipColor2;
		BiomeEnergyParticles.CenterColor = mainAttunement.tooltipColor;
		BiomeEnergyParticles.InterpolationSpeed = 0.1f;
		BiomeEnergyParticles.DrawSet(particleDrawCenter + Main.screenPosition);
		Vector2 displacement = Vector2.UnitX.RotatedBy(Main.GlobalTimeWrappedHourly * 3f) * 2f * (float)Math.Sin(Main.GlobalTimeWrappedHourly);
		spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
		spriteBatch.Draw(itemTexture, position + displacement, (Rectangle?)itemFrame, BiomeEnergyParticles.CenterColor, 0f, origin, scale, (SpriteEffects)0, 0f);
		spriteBatch.Draw(itemTexture, position - displacement, (Rectangle?)itemFrame, BiomeEnergyParticles.CenterColor, 0f, origin, scale, (SpriteEffects)0, 0f);
		spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
		spriteBatch.Draw(itemTexture, position, (Rectangle?)itemFrame, Color.White, 0f, origin, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (mainAttunement == null)
		{
			return true;
		}
		Texture2D itemTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/OmegaBiomeBladeExtra", (AssetRequestMode)2).Value;
		spriteBatch.Draw(itemTexture, base.Item.Center - Main.screenPosition, (Rectangle?)null, lightColor, rotation, base.Item.Size * 0.5f, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<TrueBiomeBlade>().AddIngredient<AstralBar>(3).AddIngredient<LifeAlloy>(3)
			.AddIngredient<CoreofCalamity>()
			.AddTile(412)
			.Register();
	}

	static OmegaBiomeBlade()
	{
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		BaseDamage = 200;
		WhirlwindAttunement_BaseDamage = 120;
		WhirlwindAttunement_LocalIFrames = 20;
		WhirlwindAttunement_EnergyDamageMult = 0.5f;
		WhirlwindAttunement_BaseSwingDamageMult = 0.4f;
		WhirlwindAttunement_FullSwingDamageMult = 1f;
		WhirlwindAttunement_ThrowDamageBoost = 3.3f;
		WhirlwindAttunement_MonolithDamageMult = 0.5f;
		WhirlwindAttunement_PassiveBaseDamage = 80;
		SuperPogoAttunement_BaseDamage = 190;
		SuperPogoAttunement_PlayerShredIFrames = 8;
		SuperPogoAttunement_LocalIFrames = 24;
		SuperPogoAttunement_ShotDamageMult = 2.5f;
		SuperPogoAttunement_SliceDamageMult = 2.5f;
		SuperPogoAttunementSliceLifesteal = 4;
		SuperPogoAttunement_PlayerSliceIFrames = 20;
		SuperPogoAttunement_ShredDecayRate = 0.65f;
		SuperPogoAttunement_PassiveLifeSteal = 7;
		ShockwaveAttunement_BaseDamage = 550;
		ShockwaveAttunement_BeamDamageMult = 0.25f;
		ShockwaveAttunement_DashHitIFrames = 20;
		ShockwaveAttunement_FullChargeMult = 3.6f;
		ShockwaveAttunement_SigilTime = 1000;
		ShockwaveAttunement_MonolithDamageBoost = 0.75f;
		ShockwaveAttunement_PassiveBaseDamage = 200;
		FlailBladeAttunement_BaseDamage = 200;
		FlailBladeAttunement_LocalIFrames = 15;
		FlailBladeAttunement_FlailTime = 10;
		FlailBladeAttunement_Reach = 400;
		FlailBladeAttunement_ChainDamageReduction = 0.5f;
		FlailBladeAttunement_GhostChainDamageReduction = 0.5f;
		FlailBladeAttunement_PassiveBaseDamage = 500;
		WhirlwindAttunement_WhirlwindProc = 0.24f;
		WhirlwindAttunement_SwordThrowProc = 1f;
		WhirlwindAttunement_MonolithProc = 0.25f;
		SuperPogoAttunement_ShredderProc = 0.1f;
		SuperPogoAttunement_WheelProc = 0.4f;
		SuperPogoAttunement_DashProc = 1f;
		ShockwaveAttunement_SwordProc = 1f;
		ShockwaveAttunement_SwordBeamProc = 0.05f;
		ShockwaveAttunement_BlastProc = 0.33f;
		ShockwaveAttunement_ShockwaveProc = 0.33f;
		FlailBladeAttunement_BladeProc = 0.1f;
		FlailBladeAttunement_ChainProc = 0.05f;
		FlailBladeAttunement_GhostChainProc = 0.1f;
		BiomeEnergyParticles = new ChargingEnergyParticleSet(-1, 2, Color.White, Color.White, 0.04f, 20f);
	}
}
