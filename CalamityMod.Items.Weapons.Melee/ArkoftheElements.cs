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

public class ArkoftheElements : ModItem, ILocalizedModType, IModType
{
	public float Combo;

	public float Charge;

	public const float ComboLength = 4f;

	public static float snapDamageMultiplier = 1.2f;

	public static float chargeDamageMultiplier = 1.35f;

	public static float needleDamageMultiplier = 0.8f;

	public static float glassStarDamageMultiplier = 0.2f;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		if (tooltips != null && Main.LocalPlayer != null)
		{
			TooltipLine comboTooltip = tooltips.FirstOrDefault((TooltipLine x) => x.Text.Contains("[COMBO]") && x.Mod == "Terraria");
			if (comboTooltip != null)
			{
				comboTooltip.Text = Lang.SupportGlyphs(this.GetLocalizedValue("ComboInfo"));
				comboTooltip.OverrideColor = Color.Crimson;
			}
			TooltipLine parryTooltip = tooltips.FirstOrDefault((TooltipLine x) => x.Text.Contains("[PARRY]") && x.Mod == "Terraria");
			if (parryTooltip != null)
			{
				parryTooltip.Text = Lang.SupportGlyphs(this.GetLocalizedValue("ParryInfo"));
				parryTooltip.OverrideColor = Color.Orange;
			}
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 112;
		base.Item.height = 172;
		base.Item.damage = 507;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.useAnimation = 20;
		base.Item.useTime = 20;
		base.Item.useTurn = true;
		base.Item.useStyle = 5;
		base.Item.knockBack = 8.5f;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.shoot = 10;
		base.Item.shootSpeed = 16f;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 10f;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
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
		return !Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && n.type == ModContent.ProjectileType<ArkoftheElementsSwungBlade>());
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			if (!Main.projectile.Any((Projectile n) => n.active && n.owner == player.whoAmI && (n.type == ModContent.ProjectileType<ArkoftheAncientsParryHoldout>() || n.type == ModContent.ProjectileType<TrueArkoftheAncientsParryHoldout>() || n.type == ModContent.ProjectileType<ArkoftheElementsParryHoldout>() || n.type == ModContent.ProjectileType<ArkoftheCosmosParryHoldout>())))
			{
				Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<ArkoftheElementsParryHoldout>(), damage, 0f, player.whoAmI);
			}
			return false;
		}
		if (Charge > 0f)
		{
			damage = (int)(chargeDamageMultiplier * (float)damage);
		}
		float scissorState = ((Combo == 4f) ? 2f : (Combo % 2f));
		Projectile.NewProjectile(source, player.Center, velocity, ModContent.ProjectileType<ArkoftheElementsSwungBlade>(), damage, knockback, player.whoAmI, scissorState, Charge);
		Combo++;
		if (Combo > 4f)
		{
			Combo = 0f;
		}
		if (scissorState == 1f)
		{
			float empoweredNeedles = ((Charge > 0f) ? 1f : 0f);
			Projectile.NewProjectile(source, player.Center + velocity.SafeNormalize(Vector2.Zero) * 20f, velocity * 2.8f, ModContent.ProjectileType<SolarNeedle>(), (int)((float)damage * needleDamageMultiplier), knockback, player.whoAmI, empoweredNeedles);
			Vector2 Shift = velocity.RotatedBy(1.5707963705062866).SafeNormalize(Vector2.Zero) * 20f;
			Projectile.NewProjectile(source, player.Center + Shift, velocity.RotatedBy(0.2356194704771042), ModContent.ProjectileType<ElementalGlassStar>(), (int)((float)damage * glassStarDamageMultiplier), knockback, player.whoAmI);
			Projectile.NewProjectile(source, player.Center + Shift * 1.2f, velocity.RotatedBy(0.3141592741012573) * 0.8f, ModContent.ProjectileType<ElementalGlassStar>(), (int)((float)damage * glassStarDamageMultiplier), knockback, player.whoAmI);
			Projectile.NewProjectile(source, player.Center - Shift, velocity.RotatedBy(-0.2356194704771042), ModContent.ProjectileType<ElementalGlassStar>(), (int)((float)damage * glassStarDamageMultiplier), knockback, player.whoAmI);
			Projectile.NewProjectile(source, player.Center - Shift * 1.2f, velocity.RotatedBy(-0.3141592741012573) * 0.8f, ModContent.ProjectileType<ElementalGlassStar>(), (int)((float)damage * glassStarDamageMultiplier), knockback, player.whoAmI);
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
		if (modItem is ArkoftheElements a && item.ModItem is ArkoftheElements a2)
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
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		float extraScale = 0.3f;
		Texture2D frontTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/ArkoftheElements", (AssetRequestMode)2).Value;
		Texture2D backTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/ArkoftheElementsBack", (AssetRequestMode)2).Value;
		float tweakedScale = scale * (1f + extraScale);
		Vector2 offset = frontTexture.Size() * extraScale / 2f * scale;
		offset -= Vector2.UnitY * 16f * scale;
		float backLayerOpacity = ((Charge > 0f) ? 1f : ((float)Math.Sin(Main.GlobalTimeWrappedHourly * 0.9f) * 0.2f + 0.3f));
		spriteBatch.Draw(backTexture, position - offset, (Rectangle?)null, drawColor * backLayerOpacity, 0f, origin, tweakedScale, (SpriteEffects)0, 0f);
		spriteBatch.Draw(frontTexture, position - offset, (Rectangle?)null, drawColor, 0f, origin, tweakedScale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D frontTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/ArkoftheElements", (AssetRequestMode)2).Value;
		Texture2D backTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/ArkoftheElementsBack", (AssetRequestMode)2).Value;
		spriteBatch.Draw(backTexture, base.Item.Center - Main.screenPosition, (Rectangle?)null, lightColor, rotation, base.Item.Size * 0.5f, scale, (SpriteEffects)0, 0f);
		spriteBatch.Draw(frontTexture, base.Item.Center - Main.screenPosition, (Rectangle?)null, lightColor, rotation, base.Item.Size * 0.5f, scale, (SpriteEffects)0, 0f);
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
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		if (!(Charge <= 0f))
		{
			Texture2D barBG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarBack", (AssetRequestMode)2).Value;
			Texture2D barFG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarFront", (AssetRequestMode)2).Value;
			float barScale = 3.9f;
			Vector2 barOrigin = barBG.Size() * 0.5f;
			float yOffset = 65f;
			Vector2 drawPos = position + Vector2.UnitY * scale * ((float)frame.Height - yOffset);
			Rectangle frameCrop = default(Rectangle);
			((Rectangle)(ref frameCrop))._002Ector(0, 0, (int)(Charge / 10f * (float)barFG.Width), barFG.Height);
			Color color = Main.hslToRgb(((float)Math.Sin(Main.GlobalTimeWrappedHourly * 0.6f) * 0.5f + 0.5f) * 0.15f, 1f, 0.85f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f) * 0.1f);
			spriteBatch.Draw(barBG, drawPos, (Rectangle?)null, color, 0f, barOrigin, scale * barScale, (SpriteEffects)0, 0f);
			spriteBatch.Draw(barFG, drawPos, (Rectangle?)frameCrop, color * 0.8f, 0f, barOrigin, scale * barScale, (SpriteEffects)0, 0f);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<TrueArkoftheAncients>().AddIngredient(3467, 5).AddIngredient<LifeAlloy>(5)
			.AddIngredient(3458, 5)
			.AddTile(134)
			.Register();
	}
}
