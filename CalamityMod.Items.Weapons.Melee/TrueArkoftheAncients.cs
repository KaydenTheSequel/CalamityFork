using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class TrueArkoftheAncients : ModItem, ILocalizedModType, IModType
{
	public float Combo = 1f;

	public float Charge;

	public static float chargeDamageMultiplier = 1.25f;

	public static float beamDamageMultiplier = 1f;

	public static float glassStarDamageMultiplier = 1f;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		if (tooltips != null && Main.LocalPlayer != null)
		{
			TooltipLine tooltip = tooltips.FirstOrDefault((TooltipLine x) => x.Text.Contains("[PARRY]") && x.Mod == "Terraria");
			if (tooltip != null)
			{
				tooltip.Text = Lang.SupportGlyphs(this.GetLocalizedValue("ParryInfo"));
				tooltip.OverrideColor = Color.CornflowerBlue;
			}
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = (base.Item.height = 72);
		base.Item.damage = 130;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.useAnimation = 25;
		base.Item.useTime = 25;
		base.Item.useTurn = true;
		base.Item.useStyle = 5;
		base.Item.knockBack = 6.5f;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.shoot = 10;
		base.Item.shootSpeed = 12f;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		return !Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<TrueArkoftheAncientsSwungBlade>());
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			if (!Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && (n.type == ModContent.ProjectileType<ArkoftheAncientsParryHoldout>() || n.type == ModContent.ProjectileType<TrueArkoftheAncientsParryHoldout>() || n.type == ModContent.ProjectileType<ArkoftheElementsParryHoldout>() || n.type == ModContent.ProjectileType<ArkoftheCosmosParryHoldout>())))
			{
				Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<TrueArkoftheAncientsParryHoldout>(), damage, 0f, player.whoAmI);
			}
			return false;
		}
		if (Combo != -1f && Combo != 1f)
		{
			Combo = 1f;
		}
		if (Charge > 0f)
		{
			damage = (int)(chargeDamageMultiplier * (float)damage);
		}
		Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<TrueArkoftheAncientsSwungBlade>(), damage, knockback, player.whoAmI, Combo, Charge);
		Combo *= -1f;
		if (Combo == -1f)
		{
			if (Charge == 0f)
			{
				Projectile.NewProjectile(source, player.Center + velocity.SafeNormalize(Vector2.Zero) * 20f, velocity, ModContent.ProjectileType<AncientStar>(), (int)((float)damage * glassStarDamageMultiplier), knockback, player.whoAmI);
			}
			Vector2 Shift = velocity.RotatedBy(1.5707963705062866).SafeNormalize(Vector2.Zero) * 30f;
			Projectile.NewProjectile(source, player.Center + Shift, velocity.RotatedBy(0.2356194704771042), ModContent.ProjectileType<AncientStar>(), (int)((float)damage * glassStarDamageMultiplier), knockback, player.whoAmI, (Charge > 0f) ? 1 : 0);
			Projectile.NewProjectile(source, player.Center - Shift, velocity.RotatedBy(-0.2356194704771042), ModContent.ProjectileType<AncientStar>(), (int)((float)damage * glassStarDamageMultiplier), knockback, player.whoAmI, (Charge > 0f) ? 1 : 0);
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
		if (modItem is TrueArkoftheAncients a && item.ModItem is TrueArkoftheAncients a2)
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
			float barScale = 1.625f;
			Vector2 barOrigin = barBG.Size() * 0.5f;
			float yOffset = 27f;
			Vector2 drawPos = position + Vector2.UnitY * scale * ((float)frame.Height - yOffset);
			Rectangle frameCrop = default(Rectangle);
			((Rectangle)(ref frameCrop))._002Ector(0, 0, (int)(Charge / 10f * (float)barFG.Width), barFG.Height);
			Color color = Main.hslToRgb(Main.GlobalTimeWrappedHourly * 0.6f % 1f, 1f, 0.85f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f) * 0.1f);
			spriteBatch.Draw(barBG, drawPos, (Rectangle?)null, color, 0f, barOrigin, scale * barScale, (SpriteEffects)0, 0f);
			spriteBatch.Draw(barFG, drawPos, (Rectangle?)frameCrop, color * 0.8f, 0f, barOrigin, scale * barScale, (SpriteEffects)0, 0f);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<FracturedArk>().AddIngredient(65).AddIngredient(1570)
			.AddTile(134)
			.Register();
	}
}
