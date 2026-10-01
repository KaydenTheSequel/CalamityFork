using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class AntlionSkewer : RogueWeapon
{
	public static float CloudDamageDebuffMult = 0.9f;

	public override void SetDefaults()
	{
		base.Item.width = 58;
		base.Item.height = 56;
		base.Item.damage = 19;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.useAnimation = (base.Item.useTime = 28);
		base.Item.knockBack = 2f;
		base.Item.shoot = ModContent.ProjectileType<AntlionSkewerProj>();
		base.Item.shootSpeed = 12f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.useStyle = 1;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		int p = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (p.WithinBounds(Main.maxProjectiles) && player.Calamity().StealthStrikeAvailable())
		{
			Main.projectile[p].Calamity().stealthStrike = true;
		}
		return false;
	}
}
