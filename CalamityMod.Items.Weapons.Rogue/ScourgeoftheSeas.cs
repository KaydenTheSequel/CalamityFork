using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class ScourgeoftheSeas : RogueWeapon
{
	public override float StealthDamageMultiplier => 1.55f;

	public override float StealthVelocityMultiplier => 1.2f;

	public override void SetDefaults()
	{
		base.Item.width = 64;
		base.Item.height = 66;
		base.Item.damage = 50;
		base.Item.knockBack = 3.5f;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.autoReuse = true;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.shootSpeed = 10f;
		base.Item.shoot = ModContent.ProjectileType<ScourgeoftheSeasProjectile>();
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.rare = 5;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int stealth = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<ScourgeoftheSeasProjectile>(), damage, knockback, player.whoAmI, 0f, 1f);
			if (stealth.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[stealth].Calamity().stealthStrike = true;
			}
			return false;
		}
		return true;
	}
}
