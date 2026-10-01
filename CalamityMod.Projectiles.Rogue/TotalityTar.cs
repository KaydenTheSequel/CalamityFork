using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class TotalityTar : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 30;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		if (base.Projectile.velocity.X != base.Projectile.velocity.X)
		{
			base.Projectile.velocity.X *= -0.1f;
		}
		if (base.Projectile.velocity.X != base.Projectile.velocity.X)
		{
			base.Projectile.velocity.X *= -0.5f;
		}
		if (base.Projectile.velocity.Y != base.Projectile.velocity.Y && base.Projectile.velocity.Y > 1f)
		{
			base.Projectile.velocity.Y *= -0.5f;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 5f)
		{
			base.Projectile.ai[0] = 5f;
			if (base.Projectile.velocity.Y == 0f && base.Projectile.velocity.X != 0f)
			{
				base.Projectile.velocity.X *= 0.97f;
				if (Math.Abs(base.Projectile.velocity.X) < 0.01f)
				{
					base.Projectile.velocity.X = 0f;
					base.Projectile.netUpdate = true;
				}
			}
			base.Projectile.velocity.Y += 0.2f;
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
		if (base.Projectile.velocity.Y < 0.25f && base.Projectile.velocity.Y > 0.15f)
		{
			base.Projectile.velocity.X *= 0.8f;
		}
		base.Projectile.rotation = (0f - base.Projectile.velocity.X) * 0.05f;
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (target.buffImmune[204])
		{
			target.buffImmune[204] = false;
		}
		target.AddBuff(204, 600);
		target.AddBuff(323, 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(323, 300);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item74, base.Projectile.position);
		Vector2 vector2 = default(Vector2);
		((Vector2)(ref vector2))._002Ector(20f, 20f);
		for (int i = 0; i < 3; i++)
		{
			Dust.NewDust(base.Projectile.Center - vector2 / 2f, (int)vector2.X, (int)vector2.Y, 191, 0f, 0f, 0, Color.Red);
		}
		for (int j = 0; j < 5; j++)
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.Center - vector2 / 2f, (int)vector2.X, (int)vector2.Y, 31, 0f, 0f, 100, default(Color), 1.5f);
			dust.velocity *= 1.4f;
			Dust dust2 = Dust.NewDustDirect(base.Projectile.Center - vector2 / 2f, (int)vector2.X, (int)vector2.Y, 6, 0f, 0f, 100, default(Color), 2.5f);
			dust2.noGravity = true;
			dust2.velocity *= 5f;
		}
		int fireAmt = Main.rand.Next(2, 4);
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int f = 0; f < fireAmt; f++)
			{
				Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<TotalityFire>(), base.Projectile.damage, 1f, Main.myPlayer);
			}
		}
	}
}
