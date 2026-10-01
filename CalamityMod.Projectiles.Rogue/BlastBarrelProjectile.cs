using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class BlastBarrelProjectile : ModProjectile, ILocalizedModType, IModType
{
	public float BounceEffectCooldown;

	public float OldVelocityX;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/BlastBarrel";

	public float RemainingBounces
	{
		get
		{
			return base.Projectile.ai[0];
		}
		set
		{
			base.Projectile.ai[0] = value;
		}
	}

	public bool CollideX => base.Projectile.oldPosition.X == base.Projectile.position.X;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 48;
		base.Projectile.height = 48;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 480;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		if (base.Projectile.localAI[0] == 0f)
		{
			RemainingBounces = ((!base.Projectile.Calamity().stealthStrike) ? 1 : 3);
			base.Projectile.localAI[0] = 1f;
		}
		base.Projectile.rotation += (float)Math.Sign(base.Projectile.velocity.X) * MathHelper.ToRadians(8f);
		if (base.Projectile.velocity.Y < 10f)
		{
			base.Projectile.velocity.Y += 0.2f;
		}
		if (CollideX && BounceEffectCooldown == 0f)
		{
			BounceEffects();
			base.Projectile.velocity.X = 0f - OldVelocityX;
		}
		else if (BounceEffectCooldown > 0f)
		{
			BounceEffectCooldown--;
		}
		if (base.Projectile.velocity.X != 0f)
		{
			OldVelocityX = (float)Math.Sign(base.Projectile.velocity.X) * 12f;
		}
	}

	public void BounceEffects()
	{
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		int projectileCount = 12;
		if (base.Projectile.Calamity().stealthStrike)
		{
			projectileCount += 4;
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int i = 0; i < projectileCount; i++)
			{
				if (Main.rand.NextBool(3))
				{
					Vector2 shrapnelVelocity = (Vector2.UnitY * Main.rand.NextFloat(-19f, -4f)).RotatedByRandom(MathHelper.ToRadians(30f));
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity + shrapnelVelocity, ModContent.ProjectileType<BarrelShrapnel>(), base.Projectile.damage, 3f, base.Projectile.owner);
					continue;
				}
				Vector2 fireVelocity = (Vector2.UnitY * Main.rand.NextFloat(-19f, -4f)).RotatedByRandom(MathHelper.ToRadians(40f));
				Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity + fireVelocity, ModContent.ProjectileType<TotalityFire>(), (int)((float)base.Projectile.damage * 0.75f), 1f, base.Projectile.owner);
				projectile.timeLeft = 300;
				projectile.penetrate = 3;
			}
		}
		RemainingBounces--;
		BounceEffectCooldown = 15f;
		if (RemainingBounces <= 0f)
		{
			base.Projectile.Kill();
		}
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}
}
