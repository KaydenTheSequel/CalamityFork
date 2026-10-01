using System;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Typeless;

public class ClaretCannon : ModItem, IClaretCannonInstance, ILocalizedModType, IModType
{
	public float baseUseDir;

	public new string LocalizationCategory => "Items.Weapons.Typeless";

	public int CooldownMax => 600;

	public override void SetDefaults()
	{
		base.Item.width = 48;
		base.Item.height = 30;
		base.Item.damage = 500;
		base.Item.DamageType = AverageDamageClass.Instance;
		base.Item.useTime = 10;
		base.Item.useAnimation = 10;
		base.Item.useLimitPerAnimation = 1;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5.5f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.UseSound = SoundID.Item40;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 12f;
		base.Item.shoot = ModContent.ProjectileType<ClaretCannonProj>();
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-5f, 0f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BloodstoneCore>(4).AddTile(134).Register();
	}

	public override bool CanUseItem(Player player)
	{
		return player.GetModPlayer<ClaretCannonPlayer>().ClaretCooldown <= 0;
	}

	public override void UseAnimation(Player player)
	{
	}

	public override void UseItemFrame(Player player)
	{
		float comp = 1f - (float)player.itemTime / (float)player.itemTimeMax;
		float lerpValue1 = MathHelper.Lerp(0f, 90f, MathF.Pow(comp, 0.3f));
		float lerpValue2 = MathHelper.Lerp(90f, 0f, MathF.Pow(comp, 2f));
		player.itemRotation = baseUseDir - MathHelper.ToRadians(Math.Min(lerpValue1, lerpValue2)) * (float)player.direction;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		baseUseDir = player.itemRotation;
		player.GetModPlayer<ClaretCannonPlayer>().ClaretCooldown = CooldownMax;
		Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		return false;
	}

	public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		float fill = (float)Main.LocalPlayer.GetModPlayer<ClaretCannonPlayer>().ClaretCooldown / (float)CooldownMax;
		if (!(fill <= 0f))
		{
			float barScale = 1.5f;
			Texture2D barBG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarBack", (AssetRequestMode)2).Value;
			Texture2D barFG = ModContent.Request<Texture2D>("CalamityMod/UI/MiscTextures/GenericBarFront", (AssetRequestMode)2).Value;
			Vector2 barOrigin = barBG.Size() * 0.5f;
			float yOffset = 5f;
			Vector2 drawPos = position + Vector2.UnitY * scale * ((float)frame.Height - yOffset);
			Rectangle frameCrop = default(Rectangle);
			((Rectangle)(ref frameCrop))._002Ector(0, 0, (int)(fill * (float)barFG.Width), barFG.Height);
			Color colorBG = Color.Crimson;
			Color colorFG = Color.Lerp(Color.OrangeRed, Color.DarkOrange, fill);
			spriteBatch.Draw(barBG, drawPos, (Rectangle?)null, colorBG, 0f, barOrigin, scale * barScale, (SpriteEffects)0, 0f);
			spriteBatch.Draw(barFG, drawPos, (Rectangle?)frameCrop, colorFG, 0f, barOrigin, scale * barScale, (SpriteEffects)0, 0f);
		}
	}
}
