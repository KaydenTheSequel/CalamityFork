using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class ShinobiBlade : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 16;
		base.Item.height = 42;
		base.Item.damage = 24;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 10);
		base.Item.useStyle = 1;
		base.Item.knockBack = 1f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.maxStack = 1;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.shoot = ModContent.ProjectileType<ShinobiBladeProjectile>();
		base.Item.shootSpeed = 10f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		int p = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (p.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[p].Calamity().stealthStrike = player.Calamity().StealthStrikeAvailable();
		}
		return false;
	}
}
