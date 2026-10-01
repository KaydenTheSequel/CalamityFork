using System;
using CalamityMod.NPCs.Cryogen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class IceBlast : ModProjectile, ILocalizedModType, IModType
{
	private const int TimeLeft = 600;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.scale = 1.2f;
		base.Projectile.penetrate = -1;
		base.Projectile.hostile = true;
		base.Projectile.coldDamage = true;
		base.Projectile.timeLeft = 600;
	}

	public override void AI()
	{
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 1f)
		{
			float spreadOutCutoffTime = 510f;
			float homeInCutoffTime = 435f;
			float minAcceleration = 0.05f;
			float maxAcceleration = 0.1f;
			float homingVelocity = 20f;
			if ((float)base.Projectile.timeLeft > homeInCutoffTime && (float)base.Projectile.timeLeft <= spreadOutCutoffTime)
			{
				int playerIndex = (int)base.Projectile.ai[0];
				Vector2 velocity = base.Projectile.velocity;
				if (Main.player.IndexInRange(playerIndex))
				{
					Player player = Main.player[playerIndex];
					velocity = base.Projectile.DirectionTo(player.Center) * homingVelocity;
				}
				float amount = MathHelper.Lerp(minAcceleration, maxAcceleration, Utils.GetLerpValue(spreadOutCutoffTime, 30f, base.Projectile.timeLeft, clamped: true));
				base.Projectile.velocity = Vector2.SmoothStep(base.Projectile.velocity, velocity, amount);
			}
		}
		Lighting.AddLight((int)((base.Projectile.position.X + (float)(base.Projectile.width / 2)) / 16f), (int)((base.Projectile.position.Y + (float)(base.Projectile.height / 2)) / 16f), 0f, 0.25f, 0.25f);
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
		for (int i = 0; i < 2; i++)
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 92, base.Projectile.velocity.X, base.Projectile.velocity.Y, 50, default(Color), 0.6f);
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= 0.3f;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		return new Color(1f, 1f, 1f, 1f) * base.Projectile.Opacity;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 5; i++)
		{
			int iceDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 92);
			if (!Main.rand.NextBool(3))
			{
				Dust obj = Main.dust[iceDust];
				obj.velocity *= 2f;
				Main.dust[iceDust].noGravity = true;
				Main.dust[iceDust].scale *= 1.75f;
			}
			else
			{
				Main.dust[iceDust].scale *= 0.5f;
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(44, 120);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.DrawProjectileWithBackglow(Cryogen.BackglowColor, lightColor, 4f, null, null, (SpriteEffects)0);
		return false;
	}
}
