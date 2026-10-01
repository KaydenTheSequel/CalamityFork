using CalamityMod.Items.BaseItems;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class HolyCollider : CustomUseProjItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 114;
		base.Item.height = 146;
		base.Item.damage = 2900;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.useAnimation = 45;
		base.Item.useStyle = 1;
		base.Item.useTime = 45;
		base.Item.useTurn = true;
		base.Item.knockBack = 7.75f;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 10f;
		base.Item.channel = true;
		base.Item.shoot = ModContent.ProjectileType<HolyColliderHoldout>();
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override bool MeleePrefix()
	{
		return true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(source, player.MountedCenter, Vector2.Zero, type, damage, knockback, player.whoAmI, 0f, 0f, 5f);
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>(Texture + "Glow", (AssetRequestMode)2).Value);
	}
}
