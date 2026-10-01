using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class FrostyBatPet : ModProjectile, ILocalizedModType, IModType
{
	public Color effectColor;

	public float flapMult;

	public int flapTime;

	public int flapRate;

	public Vector2 goalPosition;

	public Vector2 dashDirection;

	public int dashingTimer;

	public int dashLength;

	public new string LocalizationCategory => "Projectiles.Pets";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 8;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.LightPet[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 24);
		base.Projectile.friendly = true;
		base.Projectile.netImportant = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		if (VerifyOwnerIsPresent())
		{
			return;
		}
		HandleFrames();
		int maxFlapTime = flapRate * Main.projFrames[base.Type];
		if (base.Projectile.frame == 4)
		{
			if (flapTime == 0)
			{
				flapTime = maxFlapTime;
			}
			float lerp = 1f - (float)Math.Pow(Utils.GetLerpValue(flapRate - 1, 0f, base.Projectile.frameCounter % flapRate), 1.75);
			flapMult = CalamityUtils.EaseInOutExp(lerp, 2.5f, 1f);
		}
		else
		{
			flapMult = 1f - (float)Math.Pow(Utils.GetLerpValue(maxFlapTime, 1f, flapTime), 3.0);
		}
		if (flapTime > 0)
		{
			flapTime--;
		}
		float sine = (float)Math.Sin(time * 0.05f / (float)Math.PI);
		float sine2 = (float)Math.Sin(time * 0.05f / (float)Math.PI * 2f);
		if (dashingTimer == 0)
		{
			goalPosition = Owner.Center + new Vector2(45f * sine, -60f + 10f * sine2);
		}
		float dashIntensity = 1f - Math.Abs(Utils.GetLerpValue(dashLength / 2, 0f, dashingTimer));
		if (Owner.dashDelay == -1 && dashingTimer == 0)
		{
			dashingTimer = dashLength;
			base.Projectile.extraUpdates = 1;
			dashDirection = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld);
		}
		if (dashingTimer > 0)
		{
			float velPower = (float)Math.Pow(((Vector2)(ref Owner.velocity)).Length(), 0.75) * 35f;
			goalPosition = Owner.Center + dashDirection * (250f + velPower) * dashIntensity;
			float opacity = Utils.GetLerpValue(6f, 12f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true) * 0.25f + 0.05f;
			Vector2 relativePosition = base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 14f;
			Vector2 velocity = Vector2.One.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(0.3f, 1f) + base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.8f);
			float scale = Main.rand.NextFloat(0.3f, 0.55f);
			Color color = effectColor * opacity;
			Vector2 stretch = new Vector2(1f, 1.3f);
			float glowOpacity = opacity;
			GeneralParticleHandler.SpawnParticle(new CustomSpark(relativePosition, velocity, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 22, scale, color, stretch, useAddativeBlend: true, glowCenter: true, Main.rand.NextFloat(0f, (float)Math.PI * 2f), fadeIn: false, affectedByLight: false, 0f, 1f, glowOpacity, flipHorizontal: false, noShrink: false, Main.rand.NextFloat(-0.1f, 0.1f)));
		}
		else if (base.Projectile.extraUpdates > 0)
		{
			base.Projectile.extraUpdates = 0;
		}
		float followSpeed = 10f;
		base.Projectile.velocity = (goalPosition - base.Projectile.Center) / followSpeed;
		base.Projectile.rotation = base.Projectile.velocity.X * 0.035f;
		float tileCollideMult = ((!Collision.SolidCollision(base.Projectile.Center, 2, 2)) ? 1f : ((dashingTimer > 0) ? Math.Max(Utils.GetLerpValue((float)dashLength * 0.8f, dashLength, dashingTimer, clamped: true), 0.15f) : 1f));
		if (tileCollideMult > 0f)
		{
			Lighting.AddLight(base.Projectile.Center, ((Color)(ref effectColor)).ToVector3() * (0.65f + 0.5f * dashIntensity) * tileCollideMult);
		}
		if (Main.rand.NextBool(15 / ((dashingTimer == 0) ? 1 : 3)))
		{
			Vector2 vel = Vector2.One.RotatedByRandom(6.2831854820251465);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + vel * 9f, vel * Main.rand.NextFloat(0.3f, 1.5f) + base.Projectile.velocity * Main.rand.NextFloat(0.1f, 0.4f), "CalamityMod/Particles/GlowFlakeParticle", affectedByGravity: false, Main.rand.Next(25, 35), Main.rand.NextFloat(0.2f, 0.45f) + ((dashingTimer > 0) ? 0.2f : 0f), effectColor * 0.7f, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: true, Main.rand.NextFloat(0f, (float)Math.PI * 2f), fadeIn: false, affectedByLight: false, 0f, 1f, 0.8f, flipHorizontal: false, noShrink: false, Main.rand.NextFloat(-0.3f, 0.3f)));
		}
		time++;
		if (dashingTimer > 0)
		{
			dashingTimer--;
		}
	}

	public bool VerifyOwnerIsPresent()
	{
		if (!Owner.active)
		{
			base.Projectile.Kill();
			return true;
		}
		if (Owner.dead)
		{
			Owner.Calamity().frostyBat = false;
		}
		if (Owner.Calamity().frostyBat)
		{
			base.Projectile.timeLeft = 2;
		}
		return false;
	}

	public void HandleFrames()
	{
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / flapRate % Main.projFrames[base.Type];
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Texture2D glow = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		float sine = (float)Math.Sin(time * 0.05f / (float)Math.PI * 3f);
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + new Vector2(0f, 7f) * MathHelper.Lerp(1f, -1f, flapMult);
		_ = base.Projectile.rotation;
		_ = base.Projectile.spriteDirection;
		_ = -1;
		_ = texture.Size() * 0.5f;
		Rectangle frame = texture.Frame(1, 8, 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		for (int i = 0; i < 2; i++)
		{
			Color val = effectColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(glow, drawPosition, null, val * (0.3f + 0.65f * (float)i), base.Projectile.rotation, glow.Size() * 0.5f, new Vector2(1.2f, 1f) * base.Projectile.scale * (0.5f + sine * 0.05f * (float)(1 - i) - 0.3f * (float)i), (SpriteEffects)(Owner.direction == -1));
		}
		Main.EntitySpriteDraw(texture, drawPosition, frame, Color.White, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)(Owner.direction == -1));
		return false;
	}

	public FrostyBatPet()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		effectColor = Color.CornflowerBlue;
		flapRate = 5;
		dashLength = 120;
		base._002Ector();
	}
}
