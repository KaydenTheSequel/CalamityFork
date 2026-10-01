using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class FrostMist : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 300;
		base.Projectile.Opacity = 0f;
		base.Projectile.coldDamage = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.ai[1] == 0f)
		{
			for (int i = 0; i < 5; i++)
			{
				int mistDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 187, 0f, 0f, 100, default(Color), 2f);
				Dust obj = Main.dust[mistDust];
				obj.velocity *= 3f;
				if (Main.rand.NextBool())
				{
					Main.dust[mistDust].scale = 0.5f;
					Main.dust[mistDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			base.Projectile.ai[1] = 1f;
		}
		base.Projectile.localAI[1]++;
		if (base.Projectile.localAI[1] == 16f)
		{
			base.Projectile.localAI[1] = 0f;
			for (int l = 0; l < 12; l++)
			{
				Vector2 dustRotation = Vector2.UnitX * (float)(-base.Projectile.width) / 2f;
				dustRotation += -Vector2.UnitY.RotatedBy((float)l * (float)Math.PI / 6f) * new Vector2(8f, 16f);
				dustRotation = dustRotation.RotatedBy(base.Projectile.rotation - (float)Math.PI / 2f);
				int extraMistDust = Dust.NewDust(base.Projectile.Center, 0, 0, 187, 0f, 0f, 160);
				Main.dust[extraMistDust].scale = 1.1f;
				Main.dust[extraMistDust].noGravity = true;
				Main.dust[extraMistDust].position = base.Projectile.Center + dustRotation;
				Main.dust[extraMistDust].velocity = base.Projectile.velocity * 0.1f;
				Main.dust[extraMistDust].velocity = Vector2.Normalize(base.Projectile.Center - base.Projectile.velocity * 3f - Main.dust[extraMistDust].position) * 1.25f;
			}
		}
		if (base.Projectile.timeLeft < 30)
		{
			base.Projectile.Opacity = MathHelper.Clamp((float)base.Projectile.timeLeft / 30f, 0f, 1f);
		}
		else
		{
			base.Projectile.Opacity = MathHelper.Clamp(1f - (float)(base.Projectile.timeLeft - 270) / 30f, 0f, 1f);
		}
		int playerTracker = Player.FindClosest(base.Projectile.Center, 1, 1);
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] < 140f && base.Projectile.ai[1] > 60f)
		{
			float projVelocityMult = ((Vector2)(ref base.Projectile.velocity)).Length();
			Vector2 playerDistance = Main.player[playerTracker].Center - base.Projectile.Center;
			((Vector2)(ref playerDistance)).Normalize();
			playerDistance *= projVelocityMult;
			base.Projectile.velocity = (base.Projectile.velocity * 24f + playerDistance) / 25f;
			((Vector2)(ref base.Projectile.velocity)).Normalize();
			Projectile projectile = base.Projectile;
			projectile.velocity *= projVelocityMult;
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 12f)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 1.02f;
		}
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
		Lighting.AddLight(base.Projectile.Center, 0f, 0.35f * base.Projectile.Opacity, 0.35f * base.Projectile.Opacity);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		((Color)(ref lightColor)).R = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).G = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).B = (byte)(255f * base.Projectile.Opacity);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.Opacity == 1f;
	}
}
