using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "ArkoftheAncients" })]
public class FracturedArk : ModItem, ILocalizedModType, IModType
{
	public float Combo = 1f;

	public float Charge;

	public static float chargeDamageMultiplier = 1.5f;

	public static float beamDamageMultiplier = 0.8f;

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
		base.Item.width = (base.Item.height = 60);
		base.Item.damage = 41;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.useAnimation = 22;
		base.Item.useTime = 22;
		base.Item.useTurn = true;
		base.Item.useStyle = 5;
		base.Item.knockBack = 6.25f;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.shoot = 10;
		base.Item.shootSpeed = 15f;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		return !Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<ArkoftheAncientsSwungBlade>());
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			if (!Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && (n.type == ModContent.ProjectileType<ArkoftheAncientsParryHoldout>() || n.type == ModContent.ProjectileType<TrueArkoftheAncientsParryHoldout>() || n.type == ModContent.ProjectileType<ArkoftheElementsParryHoldout>() || n.type == ModContent.ProjectileType<ArkoftheCosmosParryHoldout>())))
			{
				Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<ArkoftheAncientsParryHoldout>(), damage, 0f, player.whoAmI);
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
		Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<ArkoftheAncientsSwungBlade>(), damage, knockback, player.whoAmI, Combo, Charge);
		Combo *= -1f;
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
		if (modItem is FracturedArk a && item.ModItem is FracturedArk a2)
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
			float barScale = 1.34f;
			Texture2D barBG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarBack", (AssetRequestMode)2).Value;
			Texture2D barFG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarFront", (AssetRequestMode)2).Value;
			Vector2 barOrigin = barBG.Size() * 0.5f;
			float yOffset = 23f;
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
		CreateRecipe().AddIngredient(989).AddIngredient<PurifiedGel>(5).AddRecipeGroup("AnyCopperBar", 10)
			.AddTile(16)
			.Register();
		CreateRecipe().AddIngredient(4144).AddIngredient<PurifiedGel>(5).AddRecipeGroup("AnyCopperBar", 10)
			.AddTile(16)
			.Register();
	}
}
