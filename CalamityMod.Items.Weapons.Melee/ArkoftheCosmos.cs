using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class ArkoftheCosmos : ModItem, ILocalizedModType, IModType
{
	public float Combo;

	public float Charge;

	public static int DoubleRightClickFrames = 11;

	private int rmbFrames;

	public static float NeedleDamageMultiplier = 0.7f;

	public static float MaxThrowReach = 760f;

	public static float SnapDamageMultiplier = 1.2f;

	public static float MaxCharge = 16f;

	public static float chargeDamageMultiplier = 1.35f;

	public static float chainDamageMultiplier = 0.1f;

	public static float SnapBoltsDamageMultiplier = 0.1f;

	public static float BlastDamageMultiplier = 2f;

	public static float BlastBoltsDamageMultiplier = 0.2f;

	public static float SwirlBoltAmount = 6f;

	public static float SwirlBoltDamageMultiplier = 0.7f;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		if (tooltips != null && Main.LocalPlayer != null)
		{
			TooltipLine comboTooltip = tooltips.FirstOrDefault((TooltipLine x) => x.Text.Contains("[COMBO]") && x.Mod == "Terraria");
			if (comboTooltip != null)
			{
				comboTooltip.Text = Lang.SupportGlyphs(this.GetLocalizedValue("ComboInfo"));
				comboTooltip.OverrideColor = Color.Lerp(Color.Gold, Color.Goldenrod, 0.5f + (float)Math.Sin(Main.GlobalTimeWrappedHourly) * 0.5f);
			}
			TooltipLine parryTooltip = tooltips.FirstOrDefault((TooltipLine x) => x.Text.Contains("[PARRY]") && x.Mod == "Terraria");
			if (parryTooltip != null)
			{
				parryTooltip.Text = Lang.SupportGlyphs(this.GetLocalizedValue("ParryInfo"));
				parryTooltip.OverrideColor = Color.Lerp(Color.Cyan, Color.DeepSkyBlue, 0.5f + (float)Math.Sin(Main.GlobalTimeWrappedHourly) * 0.75f);
			}
			TooltipLine blastTooltip = tooltips.FirstOrDefault((TooltipLine x) => x.Text.Contains("[BLAST]") && x.Mod == "Terraria");
			if (blastTooltip != null)
			{
				blastTooltip.Text = Lang.SupportGlyphs(this.GetLocalizedValue("BlastInfo"));
				blastTooltip.OverrideColor = Color.Lerp(Color.HotPink, Color.Crimson, 0.5f + (float)Math.Sin(Main.GlobalTimeWrappedHourly) * 0.625f);
			}
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 136);
		base.Item.damage = 1800;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 15;
		base.Item.useTime = 15;
		base.Item.useTurn = true;
		base.Item.useStyle = 5;
		base.Item.knockBack = 9.5f;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.shoot = 10;
		base.Item.shootSpeed = 28f;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 15f;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
		if (rmbFrames > 0)
		{
			rmbFrames--;
		}
		if (CanUseItem(player) && Combo != 4f)
		{
			base.Item.channel = false;
		}
		if (Combo == 4f)
		{
			base.Item.channel = true;
		}
	}

	public override bool CanUseItem(Player player)
	{
		return !Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<ArkoftheCosmosSwungBlade>());
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.altFunctionUse != 2)
		{
			return 1f;
		}
		return 5f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			bool num = rmbFrames > 0;
			Projectile parrier = Main.projectile.FirstOrDefault((Projectile p) => p.active && p.owner == player.whoAmI && p.type == ModContent.ProjectileType<ArkoftheCosmosParryHoldout>(), null);
			Projectile blast = Main.projectile.FirstOrDefault((Projectile p) => p.active && p.owner == player.whoAmI && p.type == ModContent.ProjectileType<ArkoftheCosmosBlast>(), null);
			bool canExecuteBlast = num && Charge > 0f && blast == null;
			bool canExecuteParry = parrier == null && !canExecuteBlast;
			if (canExecuteBlast)
			{
				float angle = velocity.ToRotation();
				Projectile.NewProjectile(source, player.Center + angle.ToRotationVector2() * 90f, velocity, ModContent.ProjectileType<ArkoftheCosmosBlast>(), (int)((float)damage * BlastDamageMultiplier), 0f, player.whoAmI, Charge);
				Charge = 0f;
				if (parrier != null && (float)parrier.timeLeft > 340f - (float)DoubleRightClickFrames)
				{
					parrier.active = false;
					parrier.netUpdate = true;
				}
			}
			else if (canExecuteParry && !Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && (n.type == ModContent.ProjectileType<ArkoftheAncientsParryHoldout>() || n.type == ModContent.ProjectileType<TrueArkoftheAncientsParryHoldout>() || n.type == ModContent.ProjectileType<ArkoftheElementsParryHoldout>() || n.type == ModContent.ProjectileType<ArkoftheCosmosParryHoldout>())))
			{
				Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<ArkoftheCosmosParryHoldout>(), damage, 0f, player.whoAmI);
			}
			rmbFrames = DoubleRightClickFrames;
			return false;
		}
		if (Charge > 0f)
		{
			damage = (int)(chargeDamageMultiplier * (float)damage);
		}
		float scissorState = ((Combo == 4f) ? 2f : (Combo % 2f));
		Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<ArkoftheCosmosSwungBlade>(), damage, knockback, player.whoAmI, scissorState, Charge);
		if (scissorState != 2f)
		{
			Projectile.NewProjectile(source, player.Center + velocity.SafeNormalize(Vector2.Zero) * 20f, velocity * 1.4f, ModContent.ProjectileType<RendingNeedle>(), (int)((float)damage * NeedleDamageMultiplier), knockback, player.whoAmI);
		}
		Combo++;
		if (Combo > 4f)
		{
			Combo = 0f;
		}
		Charge--;
		if (Charge < 0f)
		{
			Charge = 0f;
		}
		return false;
	}

	public override ModItem Clone(Item item)
	{
		ModItem modItem = base.Clone(item);
		if (modItem is ArkoftheCosmos a && item.ModItem is ArkoftheCosmos a2)
		{
			a.Charge = a2.Charge;
		}
		return modItem;
	}

	public override void NetSend(BinaryWriter writer)
	{
		writer.Write(Charge);
	}

	public override void NetReceive(BinaryReader reader)
	{
		Charge = reader.ReadSingle();
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		Texture2D handleTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/ArkoftheCosmosHandle", (AssetRequestMode)2).Value;
		Texture2D bladeTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/ArkoftheCosmosGlow", (AssetRequestMode)2).Value;
		float bladeOpacity = ((Charge > 0f) ? 1f : (MathHelper.Clamp((float)Math.Sin(Main.GlobalTimeWrappedHourly % (float)Math.PI) * 2f, 0f, 1f) * 0.7f + 0.3f));
		spriteBatch.Draw(handleTexture, position, (Rectangle?)null, drawColor, 0f, origin, scale, (SpriteEffects)0, 0f);
		spriteBatch.Draw(bladeTexture, position, (Rectangle?)null, drawColor * bladeOpacity, 0f, origin, scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		if (!(Charge <= 0f))
		{
			Texture2D barBG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarBack", (AssetRequestMode)2).Value;
			Texture2D barFG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarFront", (AssetRequestMode)2).Value;
			float barScale = 4f;
			Vector2 barOrigin = barBG.Size() * 0.5f;
			float yOffset = 50f;
			Vector2 drawPos = position + Vector2.UnitY * scale * ((float)frame.Height - yOffset);
			Rectangle frameCrop = default(Rectangle);
			((Rectangle)(ref frameCrop))._002Ector(0, 0, (int)(Charge / MaxCharge * (float)barFG.Width), barFG.Height);
			Color color = Main.hslToRgb(Main.GlobalTimeWrappedHourly * 0.6f % 1f, 1f, 0.75f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f) * 0.1f);
			spriteBatch.Draw(barBG, drawPos, (Rectangle?)null, color, 0f, barOrigin, scale * barScale, (SpriteEffects)0, 0f);
			spriteBatch.Draw(barFG, drawPos, (Rectangle?)frameCrop, color * 0.8f, 0f, barOrigin, scale * barScale, (SpriteEffects)0, 0f);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<FourSeasonsGalaxia>().AddIngredient<ArkoftheElements>().AddIngredient<AuricBar>(5)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
