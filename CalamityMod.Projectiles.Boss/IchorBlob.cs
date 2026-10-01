using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class IchorBlob : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 52;
		base.Projectile.height = 56;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
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
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_0597: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.position.Y > base.Projectile.ai[1] - 48f)
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.localAI[1]++;
		if (base.Projectile.localAI[1] > 900f)
		{
			base.Projectile.localAI[0] += 10f;
			base.Projectile.damage = 0;
		}
		if (base.Projectile.localAI[0] > 255f)
		{
			base.Projectile.Kill();
			base.Projectile.localAI[0] = 255f;
		}
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0.2f / 255f, (float)(255 - base.Projectile.alpha) * 0.16f / 255f, (float)(255 - base.Projectile.alpha) * 0.04f / 255f);
		base.Projectile.alpha = (int)(100.0 + (double)base.Projectile.localAI[0] * 0.7);
		if (base.Projectile.velocity.Y != 0f && base.Projectile.ai[0] == 0f)
		{
			base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) - (float)Math.PI / 2f;
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter > 6)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame > 1)
			{
				base.Projectile.frame = 0;
			}
		}
		else
		{
			base.Projectile.velocity.X = 0f;
			base.Projectile.ai[0] = 1f;
			if (base.Projectile.frame < 2)
			{
				base.Projectile.frame = 2;
				base.Projectile.frameCounter = 0;
				SoundEngine.PlaySound(in SoundID.NPCDeath21, base.Projectile.Center);
				Vector2 dustVelocity = (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length();
				for (int i = 0; i < 10; i++)
				{
					int ichorDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 170, 0f, 0f, 200, default(Color), 1.6f);
					Dust obj = Main.dust[ichorDust];
					obj.position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
					obj.noGravity = true;
					obj.velocity.Y -= 2f;
					obj.velocity *= 3f;
					obj.velocity += dustVelocity * Main.rand.NextFloat();
					ichorDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 170, 0f, 0f, 100, default(Color), 0.8f);
					obj.position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
					obj.velocity.Y -= 2f;
					obj.velocity *= 2f;
					obj.noGravity = true;
					obj.fadeIn = 1f;
					obj.velocity += dustVelocity * Main.rand.NextFloat();
				}
				for (int j = 0; j < 5; j++)
				{
					int ichorDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 170, 0f, 0f, 0, default(Color), 2f);
					Dust obj2 = Main.dust[ichorDust2];
					obj2.position = base.Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(base.Projectile.velocity.ToRotation()) * (float)base.Projectile.width / 3f;
					obj2.noGravity = true;
					obj2.velocity.Y -= 2f;
					obj2.velocity *= 0.5f;
					obj2.velocity += dustVelocity * (0.6f + 0.6f * Main.rand.NextFloat());
				}
			}
			base.Projectile.rotation = 0f;
			base.Projectile.gfxOffY = 4f;
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter > 6)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame > 5)
			{
				base.Projectile.frame = 5;
			}
		}
		base.Projectile.velocity.X *= 0.995f;
		if (base.Projectile.wet || base.Projectile.lavaWet)
		{
			base.Projectile.velocity.Y = 0f;
			return;
		}
		base.Projectile.velocity.Y += 0.1f;
		if (base.Projectile.velocity.Y > 6f)
		{
			base.Projectile.velocity.Y = 6f;
		}
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision((base.Projectile.frame >= 2) ? (base.Projectile.Bottom - Vector2.UnitY * 16f) : base.Projectile.Center, 16f, targetHitbox);
	}

	public override bool CanHitPlayer(Player target)
	{
		if (base.Projectile.localAI[1] <= 900f)
		{
			return base.Projectile.localAI[1] > 120f;
		}
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[1] > 900f)
		{
			byte b2 = (byte)((26f - (base.Projectile.localAI[1] - 900f)) * 10f);
			byte a2 = (byte)((float)base.Projectile.alpha * ((float)(int)b2 / 255f));
			return new Color((int)b2, (int)b2, (int)b2, (int)a2);
		}
		return new Color(255, 255, 255, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && base.Projectile.localAI[1] <= 900f && base.Projectile.localAI[1] > 120f)
		{
			target.AddBuff(69, 360);
		}
	}
}
