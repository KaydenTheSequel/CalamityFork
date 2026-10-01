using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class FlyingOrthocera : ModProjectile, ILocalizedModType, IModType
{
	public const float SearchDistance = 850f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 46;
		base.Projectile.height = 42;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.sentry = true;
		base.Projectile.timeLeft = 36000;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (base.Projectile.localAI[0] == 0f)
		{
			for (int i = 0; i < 56; i++)
			{
				float angle = (float)Math.PI / 28f * (float)i;
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 75);
				dust.scale = 1.5f;
				dust.velocity = angle.ToRotationVector2() * 7f;
				dust.noGravity = true;
			}
			base.Projectile.localAI[0]++;
		}
		base.Projectile.frameCounter++;
		if ((float)base.Projectile.frameCounter % 5f == 4f)
		{
			base.Projectile.frame++;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		NPC potentialTarget = base.Projectile.Center.MinionHoming(850f, player);
		if (potentialTarget != null)
		{
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(base.Projectile.AngleTo(potentialTarget.Center) - (float)Math.PI / 4f, 0.085f);
			base.Projectile.spriteDirection = (base.Projectile.rotation < (float)Math.PI).ToDirectionInt();
			if (base.Projectile.ai[0]++ % 30f == 29f && base.Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.SafeDirectionTo(potentialTarget.Center, Vector2.UnitY) * 11f, ModContent.ProjectileType<FlyingOrthoceraStream>(), base.Projectile.damage, 4f, base.Projectile.owner);
			}
		}
		else
		{
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(0f, 0.15f);
		}
	}
}
