using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class FrostyFlare : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 10;
		base.Item.height = 22;
		base.Item.damage = 37;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.useAnimation = (base.Item.useTime = 13);
		base.Item.useStyle = 1;
		base.Item.useTurn = false;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.knockBack = 2f;
		base.Item.value = CalamityGlobalItem.RarityLightPurpleBuyPrice;
		base.Item.rare = 6;
		base.Item.shoot = ModContent.ProjectileType<FrostyFlareProj>();
		base.Item.shootSpeed = 22f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		if (player.Calamity().StealthStrikeAvailable())
		{
			int flare = Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<FrostyFlareStealth>(), damage, knockback, player.whoAmI);
			if (flare.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[flare].Calamity().stealthStrike = true;
			}
			return false;
		}
		return true;
	}
}
