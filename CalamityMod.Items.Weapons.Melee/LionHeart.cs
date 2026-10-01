using CalamityMod.Cooldowns;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class LionHeart : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 60;
		base.Item.height = 62;
		base.Item.damage = 960;
		base.Item.knockBack = 5.5f;
		base.Item.useStyle = 1;
		base.Item.useAnimation = 15;
		base.Item.useTime = 15;
		base.Item.shootSpeed = 0f;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTurn = true;
		base.Item.autoReuse = true;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.Calamity().donorItem = true;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		base.Item.shoot = ((player.altFunctionUse == 2) ? ModContent.ProjectileType<EnergyShell>() : 0);
		return base.CanUseItem(player);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2 && !player.HasCooldown(LionHeartShield.ID) && player.ownedProjectileCounts[ModContent.ProjectileType<EnergyShell>()] <= 0)
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<EnergyShell>(), 0, 0f, player.whoAmI);
		}
		return false;
	}

	public override bool? CanHitNPC(Player player, NPC target)
	{
		if (player.altFunctionUse == 2)
		{
			return false;
		}
		return null;
	}

	public override bool CanHitPvp(Player player, Player target)
	{
		return player.altFunctionUse != 2;
	}

	public override void OnHitNPC(Player player, NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		int explosion = Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), target.Center, Vector2.Zero, ModContent.ProjectileType<PlanarRipperExplosion>(), base.Item.damage, base.Item.knockBack, player.whoAmI);
		if (explosion.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[explosion].DamageType = DamageClass.Melee;
		}
	}

	public override void OnHitPvp(Player player, Player target, Player.HurtInfo hurtInfo)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		int explosion = Projectile.NewProjectile(player.GetSource_ItemUse(base.Item), target.Center, Vector2.Zero, ModContent.ProjectileType<PlanarRipperExplosion>(), base.Item.damage, base.Item.knockBack, player.whoAmI);
		if (explosion.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[explosion].DamageType = DamageClass.Melee;
		}
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(3))
		{
			Dust.NewDust(new Vector2((float)hitbox.X, (float)hitbox.Y), hitbox.Width, hitbox.Height, 132);
		}
	}
}
