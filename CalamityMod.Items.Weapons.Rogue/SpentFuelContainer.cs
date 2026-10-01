using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class SpentFuelContainer : RogueWeapon
{
	public override float StealthDamageMultiplier => 0.5f;

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 24;
		base.Item.damage = 80;
		base.Item.useAnimation = 50;
		base.Item.useTime = 50;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 1;
		base.Item.knockBack = 4.5f;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.UseSound = SoundID.Item106;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<SpentFuelContainerProjectile>();
		base.Item.shootSpeed = 15f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		bool stealthAvailable = player.Calamity().StealthStrikeAvailable();
		int p = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<SpentFuelContainerProjectile>(), damage, knockback, player.whoAmI, stealthAvailable ? 1f : 0f);
		if (stealthAvailable && p.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[p].Calamity().stealthStrike = true;
		}
		return false;
	}
}
