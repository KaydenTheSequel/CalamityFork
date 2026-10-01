using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class DeepWounder : RogueWeapon
{
	public override float StealthVelocityMultiplier => 1.1f;

	public override void SetDefaults()
	{
		base.Item.width = 58;
		base.Item.height = 52;
		base.Item.damage = 132;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useTime = (base.Item.useAnimation = 25);
		base.Item.useStyle = 1;
		base.Item.knockBack = 3f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.maxStack = 1;
		base.Item.rare = 7;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.shoot = ModContent.ProjectileType<DeepWounderProjectile>();
		base.Item.shootSpeed = 17f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int p = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
			if (p.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[p].Calamity().stealthStrike = true;
			}
			return false;
		}
		return true;
	}
}
