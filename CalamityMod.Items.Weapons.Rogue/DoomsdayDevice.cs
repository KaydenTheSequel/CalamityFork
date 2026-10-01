using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

[LegacyName(new string[] { "ShockGrenade" })]
public class DoomsdayDevice : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 14;
		base.Item.height = 30;
		base.Item.damage = 240;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useTime = 55;
		base.Item.useAnimation = 55;
		base.Item.useStyle = 1;
		base.Item.knockBack = 12f;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.shoot = ModContent.ProjectileType<DoomsdayDeviceProjectile>();
		base.Item.shootSpeed = 12f;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.channel = true;
	}

	public override bool CanUseItem(Player player)
	{
		for (int x = 0; x < Main.maxProjectiles; x++)
		{
			Projectile projectile = Main.projectile[x];
			if (projectile.active && projectile.type == base.Item.shoot && projectile.localAI[1] < 5f && projectile.owner == player.whoAmI)
			{
				return false;
			}
		}
		return true;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(source, position, velocity, type, damage, 0f, player.whoAmI);
		return false;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/DoomsdayDeviceGlow", (AssetRequestMode)2).Value);
	}
}
