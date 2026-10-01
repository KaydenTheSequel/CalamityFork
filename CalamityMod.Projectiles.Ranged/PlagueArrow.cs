using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class PlagueArrow : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.arrow = true;
		base.Projectile.penetrate = 1;
		base.Projectile.aiStyle = 1;
		base.Projectile.timeLeft = 600;
		base.AIType = 1;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<PlagueExplosionFriendly>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		for (int i = 0; i < 6; i++)
		{
			if (i % 2 != 1 || Main.rand.NextBool(3))
			{
				Vector2 projPos = base.Projectile.position;
				Vector2 projVel = base.Projectile.oldVelocity;
				((Vector2)(ref projVel)).Normalize();
				projVel *= 8f;
				float beeVelX = (float)Main.rand.Next(-35, 36) * 0.01f;
				float beeVelY = (float)Main.rand.Next(-35, 36) * 0.01f;
				projPos -= projVel * (float)i;
				beeVelX += base.Projectile.oldVelocity.X / 6f;
				beeVelY += base.Projectile.oldVelocity.Y / 6f;
				int bee = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), projPos.X, projPos.Y, beeVelX, beeVelY, Main.player[base.Projectile.owner].beeType(), Main.player[base.Projectile.owner].beeDamage(base.Projectile.damage / 2), Main.player[base.Projectile.owner].beeKB(0f), Main.myPlayer);
				if (bee.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[bee].penetrate = 2;
					Main.projectile[bee].DamageType = DamageClass.Ranged;
				}
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}
}
