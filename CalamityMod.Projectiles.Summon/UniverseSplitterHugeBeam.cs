using System;
using System.IO;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Enums;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class UniverseSplitterHugeBeam : ModProjectile, ILocalizedModType, IModType
{
	public const int TotalFadeoutTime = 25;

	public const int TimeLeft = 180;

	public const float MaximumLength = 3000f;

	public const float LaserSize = 1.45f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 12000;
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 14);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 180;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 6;
		base.Projectile.DamageType = DamageClass.Summon;
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
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = (base.Projectile.velocity.ToRotation() + base.Projectile.ai[0]).ToRotationVector2();
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] < 25f)
		{
			base.Projectile.scale = MathHelper.Lerp(0.01f, 1.45f, base.Projectile.localAI[0] / 25f);
		}
		if (base.Projectile.timeLeft < 25)
		{
			base.Projectile.scale = MathHelper.Lerp(1.45f, 0f, 1f - (float)base.Projectile.timeLeft / 25f);
		}
		Vector2 top = base.Projectile.Top;
		float[] samples = new float[12];
		float determinedLength = 0f;
		Collision.LaserScan(top, base.Projectile.velocity, (float)base.Projectile.width * base.Projectile.scale, 3000f, samples);
		for (int i = 0; i < samples.Length; i++)
		{
			determinedLength += samples[i];
		}
		determinedLength /= (float)samples.Length;
		determinedLength = MathHelper.Clamp(determinedLength, 900.00006f, 3000f);
		float lerpDelta = 0.5f;
		base.Projectile.localAI[1] = MathHelper.Lerp(base.Projectile.localAI[1], determinedLength - 20f, lerpDelta);
		if (base.Projectile.localAI[0] < 30f)
		{
			base.Projectile.localAI[1] = MathHelper.Lerp(72f, base.Projectile.localAI[1], base.Projectile.localAI[0] / 30f);
		}
		Vector2 beamEndPosition = base.Projectile.Center + base.Projectile.velocity * (base.Projectile.localAI[1] - 6f);
		if (base.Projectile.localAI[0] >= 30f && base.Projectile.localAI[0] <= 35f)
		{
			if (base.Projectile.localAI[0] == 30f)
			{
				SoundEngine.PlaySound(in TeslaCannon.FireSound, base.Projectile.Center);
			}
			for (int j = 0; j < 75; j++)
			{
				Dust dust = Dust.NewDustPerfect(beamEndPosition, 269);
				dust.velocity = Main.rand.NextVector2Circular(16f, 11f);
				dust.velocity = dust.velocity.SafeNormalize(Vector2.UnitY) * new Vector2(5f, 3.5f);
				dust.scale = Main.rand.NextFloat(1.2f, 1.5f);
				dust.noGravity = true;
				Dust dust2 = Dust.NewDustPerfect(beamEndPosition, 269);
				dust2.velocity = ((float)j / 75f * ((float)Math.PI * 2f)).ToRotationVector2().RotatedByRandom(0.20000000298023224) * new Vector2(16f, 11f) * 1.3f;
				dust2.scale = Main.rand.NextFloat(1.4f, 1.75f);
				dust2.noGravity = true;
			}
		}
		if (base.Projectile.localAI[0] > 130f)
		{
			float light = 1f;
			if (base.Projectile.localAI[0] < 150f)
			{
				light = MathHelper.Lerp(0f, 1f, (base.Projectile.localAI[0] - 150f) / 30f);
			}
			MoonlordDeathDrama.RequestLight(light, beamEndPosition);
		}
		DelegateMethods.v3_1 = new Vector3(0.62f, 0.94f, 0.38f);
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CastLight);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		Texture2D laserTailTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UniverseSplitterHugeBeamEnd", (AssetRequestMode)1).Value;
		Texture2D laserBodyTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/UniverseSplitterHugeBeamMid", (AssetRequestMode)1).Value;
		float laserLength = base.Projectile.localAI[1];
		Color drawColor = new Color(1f, 1f, 1f) * 0.9f;
		laserLength -= (float)(laserTailTexture.Height / 2) * base.Projectile.scale;
		Vector2 centerDelta = base.Projectile.Center - 76f * base.Projectile.velocity * base.Projectile.scale;
		centerDelta += base.Projectile.velocity * base.Projectile.scale * (float)laserTailTexture.Height / 2f;
		if (laserLength > 0f)
		{
			float laserLengthDelta = 0f;
			Rectangle sourceRectangle = default(Rectangle);
			((Rectangle)(ref sourceRectangle))._002Ector(0, 76 * (base.Projectile.timeLeft / 3 % 5), laserBodyTexture.Width, 76);
			while (laserLengthDelta + 1f < laserLength)
			{
				if (laserLength - laserLengthDelta < (float)sourceRectangle.Height)
				{
					sourceRectangle.Height = (int)(laserLength - laserLengthDelta);
				}
				Main.EntitySpriteDraw(laserBodyTexture, centerDelta - Main.screenPosition, sourceRectangle, drawColor, base.Projectile.rotation, new Vector2((float)sourceRectangle.Width / 2f, 0f), base.Projectile.scale, (SpriteEffects)0);
				laserLengthDelta += (float)sourceRectangle.Height * base.Projectile.scale;
				centerDelta += base.Projectile.velocity * (float)sourceRectangle.Height * base.Projectile.scale;
				sourceRectangle.Y += (int)(76f * base.Projectile.scale);
				if (sourceRectangle.Y + sourceRectangle.Height > laserBodyTexture.Height)
				{
					sourceRectangle.Y = 0;
				}
			}
		}
		centerDelta += base.Projectile.velocity * base.Projectile.scale * 38f;
		Rectangle tailFrameRectangle = default(Rectangle);
		((Rectangle)(ref tailFrameRectangle))._002Ector(0, 76 * (base.Projectile.timeLeft / 3 % 5), laserTailTexture.Width, 76);
		Main.EntitySpriteDraw(laserTailTexture, centerDelta - Main.screenPosition, tailFrameRectangle, drawColor, base.Projectile.rotation, new Vector2(148f, 76f) / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void CutTiles()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
		Vector2 unit = base.Projectile.velocity;
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + unit * base.Projectile.localAI[1], (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CutTiles);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		if (((Rectangle)(ref projHitbox)).Intersects(targetHitbox))
		{
			return true;
		}
		float value = 0f;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * (base.Projectile.localAI[1] + 76f), 22f * base.Projectile.scale, ref value))
		{
			return true;
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(144, 300);
	}
}
