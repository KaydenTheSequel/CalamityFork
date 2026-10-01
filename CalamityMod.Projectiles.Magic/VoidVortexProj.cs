using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VoidVortexProj : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public int timeOffset;

	public bool doDamage;

	public bool fireBeam = true;

	public bool rotDirection;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 15;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 38;
		base.Projectile.height = 38;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 90;
		base.Projectile.extraUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[2] == 3f)
		{
			if (time == 0)
			{
				timeOffset = Main.rand.Next(20, 41);
				base.Projectile.timeLeft = 500;
				base.Projectile.extraUpdates = 2;
				base.Projectile.scale = Main.rand.NextFloat(0.35f, 0.55f);
				rotDirection = Main.rand.NextBool();
			}
			if (time >= 20)
			{
				doDamage = true;
			}
			if (time >= timeOffset && time % 25 == 0)
			{
				base.Projectile.velocity = base.Projectile.velocity.RotatedBy(rotDirection ? 0.65f : (-0.65f));
				rotDirection = !rotDirection;
			}
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 1000f, 15f, MathHelper.Clamp(100f - (float)time * 0.3f, 40f, 100f));
			if (Main.rand.NextBool())
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 226, -base.Projectile.velocity * Main.rand.NextFloat(0.05f, 0.3f));
				dust.scale = Main.rand.NextFloat(0.35f, 0.75f);
				dust.noGravity = true;
			}
		}
		else
		{
			if (base.Projectile.timeLeft < 65)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.98f;
			}
			else
			{
				float spinTheta = 0.11f;
				if (base.Projectile.localAI[0] == 0f)
				{
					base.Projectile.localAI[0] = (Main.rand.NextBool() ? (0f - spinTheta) : spinTheta);
				}
				float revolutionTheta = 0.14f * base.Projectile.ai[1];
				if (base.Projectile.ai[0] % 2f == 0f)
				{
					base.Projectile.velocity = base.Projectile.velocity.RotatedBy(revolutionTheta) * 1.0092f;
				}
			}
			if (time == 25 && base.Projectile.ai[2] == 1f)
			{
				base.Projectile.scale = 1.5f;
				base.Projectile.alpha = 0;
				for (int k = 0; k < 25; k++)
				{
					Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 226, Utils.RotatedByRandom(new Vector2(15f, 15f), 100.0) * Main.rand.NextFloat(0.05f, 0.8f));
					dust2.scale = Main.rand.NextFloat(0.45f, 0.95f);
					dust2.noGravity = true;
				}
				base.Projectile.ai[0] = 0f;
			}
			if (time >= 25 && base.Projectile.ai[2] == 1f && time % 2 == 0 && base.Projectile.timeLeft > 20)
			{
				GeneralParticleHandler.SpawnParticle(new CrackParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(8f, 8f), 100.0), Color.Aqua * 0.65f, Vector2.One, 0f, 0f, Main.rand.NextFloat(0.4f, 0.65f), 11));
			}
			base.Projectile.ai[0]--;
			NPC target = base.Projectile.Center.ClosestNPCAt(1000f);
			if (fireBeam && base.Projectile.ai[0] == -30f && base.Projectile.ai[2] <= 0f && target != null)
			{
				CalamityUtils.MagnetSphereHitscan(base.Projectile, Vector2.Distance(base.Projectile.Center, target.Center), 8f, 0f, 1, ModContent.ProjectileType<ClimaxBeam>(), 1.0, attackMultiple: true);
				fireBeam = false;
			}
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 3)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame++;
			if (base.Projectile.frame > 4)
			{
				base.Projectile.frame = 0;
			}
		}
		if (base.Projectile.ai[0] <= 0f)
		{
			time++;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.netUpdate = true;
		if (base.Projectile.ai[2] == 1f)
		{
			doDamage = true;
			base.Projectile.ExpandHitboxBy(400);
			base.Projectile.Damage();
			if (Main.myPlayer == base.Projectile.owner)
			{
				for (int k = 0; k < 12; k++)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Utils.RotatedByRandom(new Vector2(10f, 10f), 100.0) * Main.rand.NextFloat(0.4f, 0.55f), ModContent.ProjectileType<VoidVortexProj>(), base.Projectile.damage / 6, 0f, Main.myPlayer, 0f, 0f, 3f);
				}
			}
			for (int i = 0; i < 40; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 226, Utils.RotatedByRandom(new Vector2(25f, 25f), 100.0) * Main.rand.NextFloat(0.05f, 0.8f));
				dust.scale = Main.rand.NextFloat(0.65f, 1.15f);
				dust.noGravity = true;
			}
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/AuricBulletHit");
			style.Volume = 0.4f;
			style.Pitch = 0f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Aqua, "CalamityMod/Particles/HighResFoggyCircleHardEdge", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.03f, 0.16f, 16, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		if (base.Projectile.ai[2] == 3f)
		{
			for (int j = 0; j < 7; j++)
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 226, Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.05f, 0.8f));
				dust2.scale = Main.rand.NextFloat(0.45f, 0.75f);
				dust2.noGravity = true;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AuricRebuke>(), 180);
		if (hit.Damage > 1 && base.Projectile.ai[2] == 3f)
		{
			base.Projectile.Kill();
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 20)
		{
			float timerAlpha = (float)base.Projectile.timeLeft / 20f;
			base.Projectile.alpha = (int)(255f - 255f * timerAlpha);
		}
		if (time < 25 && base.Projectile.ai[2] == 1f)
		{
			base.Projectile.alpha = 255;
		}
		return new Color(255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 255 - base.Projectile.alpha, 0);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture2D13 = TextureAssets.Projectile[base.Type].Value;
		int framing = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		Main.spriteBatch.Draw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture2D13.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)framing / 2f), base.Projectile.scale * ((base.Projectile.ai[2] == 3f) ? 1.3f : 1f), (SpriteEffects)0, 0f);
		if (base.Projectile.ai[2] == 3f)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor * 0.3f, 1, texture2D13);
		}
		return false;
	}

	public override bool? CanDamage()
	{
		if (!doDamage)
		{
			return false;
		}
		return null;
	}
}
