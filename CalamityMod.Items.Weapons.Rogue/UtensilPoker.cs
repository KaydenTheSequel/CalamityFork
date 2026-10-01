using CalamityMod.CalPlayer;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class UtensilPoker : RogueWeapon
{
	private int counter;

	public override float StealthDamageMultiplier => 2f;

	public override float StealthVelocityMultiplier => 1.4f;

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 66;
		base.Item.damage = 333;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.knockBack = 8f;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 1;
		base.Item.useTime = 15;
		base.Item.useAnimation = 45;
		base.Item.reuseDelay = 15;
		base.Item.useLimitPerAnimation = 3;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.shoot = ModContent.ProjectileType<Fork>();
		base.Item.shootSpeed = 12f;
	}

	public override void ModifyStatsExtra(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (player.Calamity().StealthStrikeAvailable())
		{
			type = ModContent.ProjectileType<ButcherKnife>();
			return;
		}
		type = ModContent.ProjectileType<Fork>();
		double dmgMult = 1.0;
		float kbMult = 1f;
		switch (counter)
		{
		case 0:
			type = ModContent.ProjectileType<Fork>();
			dmgMult = 1.1;
			kbMult = 2f;
			break;
		case 1:
			type = ModContent.ProjectileType<Knife>();
			dmgMult = 1.2;
			kbMult = 1f;
			break;
		case 2:
			type = ModContent.ProjectileType<CarvingFork>();
			dmgMult = 1.0;
			kbMult = 1f;
			break;
		}
		damage = (int)((double)damage * dmgMult);
		knockback *= kbMult;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer mp = player.Calamity();
		int idx = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (idx.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[idx].Calamity().stealthStrike = mp.StealthStrikeAvailable();
		}
		counter++;
		if (counter >= 3)
		{
			counter = 0;
		}
		return false;
	}
}
