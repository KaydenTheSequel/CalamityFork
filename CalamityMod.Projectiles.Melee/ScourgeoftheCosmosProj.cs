using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class ScourgeoftheCosmosProj : ModProjectile, ILocalizedModType, IModType
{
	private int bounce = 3;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 36);
		base.Projectile.alpha = 255;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 1;
	}

	public override void AI()
	{
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha <= 200)
		{
			for (int i = 0; i < 2; i++)
			{
				int dustType = (Main.rand.NextBool(3) ? 56 : 242);
				Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType);
				dust.position = base.Projectile.Center - base.Projectile.velocity * (float)i * 0.25f;
				dust.velocity *= 0f;
				dust.scale = 0.7f;
			}
		}
		base.Projectile.alpha -= 50;
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 180f)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.4f;
			base.Projectile.velocity.X = base.Projectile.velocity.X * 0.97f;
		}
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		bounce--;
		if (bounce <= 0)
		{
			base.Projectile.Kill();
		}
		else
		{
			SoundEngine.PlaySound(in SoundID.NPCHit4, base.Projectile.position);
			if (base.Projectile.velocity.X != oldVelocity.X)
			{
				base.Projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (base.Projectile.velocity.Y != oldVelocity.Y)
			{
				base.Projectile.velocity.Y = 0f - oldVelocity.Y;
			}
			if (base.Projectile.owner == Main.myPlayer)
			{
				int minisAmt = Main.rand.Next(1, 3);
				for (int j = 0; j < minisAmt; j++)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Main.rand.NextVector2Circular(2f, 2f), ModContent.ProjectileType<ScourgeoftheCosmosMini>(), (int)((double)base.Projectile.damage * 0.75), base.Projectile.knockBack * 0.35f, base.Projectile.owner);
				}
			}
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCHit4, base.Projectile.position);
		for (int i = 0; i < 10; i++)
		{
			int dustType = (Main.rand.NextBool(3) ? 56 : 242);
			Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType);
			dust.scale *= 1.1f;
			dust.noGravity = true;
		}
		for (int j = 0; j < 15; j++)
		{
			int dustType2 = (Main.rand.NextBool(3) ? 56 : 242);
			Dust dust2 = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType2);
			dust2.velocity *= 2.5f;
			dust2.scale *= 0.8f;
			dust2.noGravity = true;
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			int minisAmt = Main.rand.Next(3, 5);
			for (int k = 0; k < minisAmt; k++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Main.rand.NextVector2Circular(2f, 2f), ModContent.ProjectileType<ScourgeoftheCosmosMini>(), (int)((double)base.Projectile.damage * 0.75), base.Projectile.knockBack * 0.35f, base.Projectile.owner);
			}
		}
	}
}
