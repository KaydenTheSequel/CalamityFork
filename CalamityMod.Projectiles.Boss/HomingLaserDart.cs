using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class HomingLaserDart : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 900;
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
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		//IL_059f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		//IL_069d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_0772: Unknown result type (might be due to invalid IL or missing references)
		//IL_0801: Unknown result type (might be due to invalid IL or missing references)
		//IL_0808: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityWorld.revenge)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 5)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 4)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.alpha -= 40;
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		if (base.Projectile.alpha < 40)
		{
			int laserDust = Dust.NewDust(base.Projectile.Center - Vector2.One * 5f, 10, 10, 244, (0f - base.Projectile.velocity.X) / 3f, (0f - base.Projectile.velocity.Y) / 3f, 150, Color.Transparent, 0.6f);
			Main.dust[laserDust].noGravity = true;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.ai[1] == -1f)
		{
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 18f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 1.05f;
			}
			else
			{
				base.Projectile.tileCollide = true;
			}
			return;
		}
		Vector2 maxVelocity = default(Vector2);
		((Vector2)(ref maxVelocity))._002Ector(death ? 13.2f : 12f, death ? 13.2f : 12f);
		float maxAcceleration = (death ? 0.44f : 0.4f);
		float timeBeforeHoming = (death ? 30f : 45f);
		float distanceAboveTargetBeforeHomingDownward = (death ? 400f : 480f);
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.localAI[0]++;
			if (base.Projectile.localAI[0] >= timeBeforeHoming)
			{
				base.Projectile.localAI[0] = 0f;
				base.Projectile.ai[0] = 1f;
				base.Projectile.ai[1] = (int)Player.FindClosest(base.Projectile.position, base.Projectile.width, base.Projectile.height);
				base.Projectile.netUpdate = true;
			}
			base.Projectile.velocity.X = base.Projectile.velocity.RotatedBy(0.0).X;
			base.Projectile.velocity.X = MathHelper.Clamp(base.Projectile.velocity.X, 0f - maxVelocity.X, maxVelocity.X);
			base.Projectile.velocity.Y -= maxAcceleration * 0.2f;
			if (base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.Y -= maxAcceleration * 0.5f;
			}
			if (base.Projectile.velocity.Y < 0f - maxVelocity.Y)
			{
				base.Projectile.velocity.Y = 0f - maxVelocity.Y;
			}
		}
		else if (base.Projectile.ai[0] == 1f)
		{
			if (Main.player[(int)base.Projectile.ai[1]].Center.Y > base.Projectile.Center.Y + distanceAboveTargetBeforeHomingDownward)
			{
				base.Projectile.ai[0] = 2f;
				base.Projectile.netUpdate = true;
			}
			base.Projectile.velocity.X = base.Projectile.velocity.RotatedBy(0.0).X;
			base.Projectile.velocity.X = MathHelper.Clamp(base.Projectile.velocity.X, 0f - maxVelocity.X, maxVelocity.X);
			base.Projectile.velocity.Y -= maxAcceleration * 0.2f;
			if (base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.Y -= maxAcceleration * 0.5f;
			}
			if (base.Projectile.velocity.Y < 0f - maxVelocity.Y)
			{
				base.Projectile.velocity.Y = 0f - maxVelocity.Y;
			}
		}
		else
		{
			if (base.Projectile.ai[0] != 2f)
			{
				return;
			}
			if (Main.player[(int)base.Projectile.ai[1]].Center.Y < base.Projectile.Center.Y)
			{
				base.Projectile.tileCollide = true;
			}
			Vector2 playerDistance = Main.player[(int)base.Projectile.ai[1]].Center - base.Projectile.Center;
			Rectangle hitbox = base.Projectile.Hitbox;
			if (((Rectangle)(ref hitbox)).Intersects(Main.player[(int)base.Projectile.ai[1]].Hitbox))
			{
				base.Projectile.Kill();
				return;
			}
			Vector2 projectileVelocity = playerDistance.SafeNormalize(Vector2.UnitY);
			projectileVelocity *= ((Vector2)(ref maxVelocity)).Length();
			projectileVelocity = Vector2.Lerp(base.Projectile.velocity, projectileVelocity, 0.6f);
			if (projectileVelocity.Y < maxVelocity.Y)
			{
				projectileVelocity.Y = maxVelocity.Y;
			}
			if (base.Projectile.velocity.X < projectileVelocity.X)
			{
				base.Projectile.velocity.X += maxAcceleration;
				if (base.Projectile.velocity.X < 0f && projectileVelocity.X > 0f)
				{
					base.Projectile.velocity.X += maxAcceleration;
				}
			}
			else if (base.Projectile.velocity.X > projectileVelocity.X)
			{
				base.Projectile.velocity.X -= maxAcceleration;
				if (base.Projectile.velocity.X > 0f && projectileVelocity.X < 0f)
				{
					base.Projectile.velocity.X -= maxAcceleration;
				}
			}
			if (base.Projectile.velocity.Y < projectileVelocity.Y)
			{
				base.Projectile.velocity.Y += maxAcceleration;
				if (base.Projectile.velocity.Y < 0f && projectileVelocity.Y > 0f)
				{
					base.Projectile.velocity.Y += maxAcceleration;
				}
			}
			else if (base.Projectile.velocity.Y > projectileVelocity.Y)
			{
				base.Projectile.velocity.Y -= maxAcceleration;
				if (base.Projectile.velocity.Y > 0f && projectileVelocity.Y < 0f)
				{
					base.Projectile.velocity.Y -= maxAcceleration;
				}
			}
			float pushForce = (death ? 0.12f : 0.08f);
			float pushDistance = (death ? 60f : 40f);
			for (int k = 0; k < Main.maxProjectiles; k++)
			{
				Projectile otherProj = Main.projectile[k];
				if (!otherProj.active || k == base.Projectile.whoAmI)
				{
					continue;
				}
				bool num = otherProj.type == base.Projectile.type;
				float taxicabDist = Vector2.Distance(base.Projectile.Center, otherProj.Center);
				if (num && taxicabDist < pushDistance)
				{
					if (base.Projectile.position.X < otherProj.position.X)
					{
						base.Projectile.velocity.X -= pushForce;
					}
					else
					{
						base.Projectile.velocity.X += pushForce;
					}
					if (base.Projectile.position.Y < otherProj.position.Y)
					{
						base.Projectile.velocity.Y -= pushForce;
					}
					else
					{
						base.Projectile.velocity.Y += pushForce;
					}
				}
			}
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 50, 50, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Zombie103, base.Projectile.Center);
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 96);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int i = 0; i < 3; i++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
		}
		for (int j = 0; j < 30; j++)
		{
			int killDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 0, default(Color), 2.5f);
			Main.dust[killDust].noGravity = true;
			Dust obj = Main.dust[killDust];
			obj.velocity *= 3f;
			killDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 100, default(Color), 1.5f);
			Dust obj2 = Main.dust[killDust];
			obj2.velocity *= 2f;
			Main.dust[killDust].noGravity = true;
		}
	}
}
