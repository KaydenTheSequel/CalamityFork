using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class SlickCane : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 36;
		base.Item.damage = 50;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.useAnimation = 26;
		base.Item.useTime = 26;
		base.Item.useStyle = 5;
		base.Item.knockBack = 6f;
		base.Item.UseSound = SoundID.DD2_GhastlyGlaivePierce;
		base.Item.autoReuse = true;
		base.Item.value = Item.buyPrice(0, 35);
		base.Item.rare = 3;
		base.Item.shoot = ModContent.ProjectileType<SlickCaneProjectile>();
		base.Item.shootSpeed = 16f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		float ai0 = Main.rand.NextFloat() * base.Item.shootSpeed * 1.5f * (float)player.direction;
		int projectileIndex = Projectile.NewProjectile(source, position.X, position.Y + 100f, velocity.X * 2f, velocity.Y * 2f, type, damage, knockback, player.whoAmI, ai0);
		if (projectileIndex.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[projectileIndex].Calamity().stealthStrike = player.Calamity().StealthStrikeAvailable();
		}
		return false;
	}
}
