using System;
using System.IO;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Enums;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class CragmawBeam : ModProjectile, ILocalizedModType, IModType
{
	public const int Lifetime = 120;

	private const float maximumLength = 1200f;

	public new string LocalizationCategory => "Projectiles.Enemy";

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 22;
		base.Projectile.hostile = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 120;
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
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.npc[(int)base.Projectile.ai[1]].active)
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.velocity != -Vector2.UnitY)
		{
			base.Projectile.velocity = -Vector2.UnitY;
			base.Projectile.netUpdate = true;
		}
		if (Main.npc[(int)base.Projectile.ai[1]].active)
		{
			base.Projectile.Center = Main.npc[(int)base.Projectile.ai[1]].Top + new Vector2(0f, 4f);
		}
		float laserSize = 1.6f;
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] >= 120f)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.scale = (float)Math.Sin(base.Projectile.localAI[0] * (float)Math.PI / 120f) * 10f * laserSize;
		if (base.Projectile.scale > laserSize)
		{
			base.Projectile.scale = laserSize;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		Vector2 center = base.Projectile.Center;
		float[] samples = new float[3];
		float determinedLength = 0f;
		Collision.LaserScan(center, base.Projectile.velocity, (float)base.Projectile.width * base.Projectile.scale, 1200f, samples);
		for (int i = 0; i < samples.Length; i++)
		{
			determinedLength += samples[i];
		}
		determinedLength /= (float)samples.Length;
		determinedLength = MathHelper.Clamp(determinedLength, 600f, 1200f);
		float lerpDelta = 0.5f;
		base.Projectile.localAI[1] = MathHelper.Lerp(base.Projectile.localAI[1], determinedLength, lerpDelta);
		Vector2 beamEndPosiiton = base.Projectile.Center + base.Projectile.velocity * (base.Projectile.localAI[1] - 6f);
		if (base.Projectile.timeLeft % 20 == 19 && Main.netMode != 1)
		{
			Vector2 acidSpawnPosition = base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1] * Main.rand.NextFloat(0.1f, 0.8f);
			if (!Main.player[Player.FindClosest(acidSpawnPosition, 1, 1)].WithinRange(acidSpawnPosition, 300f))
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), acidSpawnPosition, Main.rand.NextVector2CircularEdge(8f, 8f), ModContent.ProjectileType<CragmawAcidDrop>(), base.Projectile.damage / 2, 0f);
			}
		}
		if (WorldGen.SolidTile((int)(beamEndPosiiton.X / 16f), (int)(beamEndPosiiton.Y / 16f)))
		{
			for (int j = 0; j < 4; j++)
			{
				float f = base.Projectile.velocity.ToRotation() + (float)Main.rand.NextBool().ToDirectionInt() * ((float)Math.PI / 2f);
				float speed = (float)Main.rand.NextDouble() * 2f + 2f;
				Vector2 velocity = f.ToRotationVector2() * speed;
				Dust dust = Dust.NewDustDirect(beamEndPosiiton, 0, 0, 75, velocity.X, velocity.Y);
				dust.noGravity = true;
				dust.scale = 2.1f;
			}
			for (int k = 0; k < 8; k++)
			{
				Dust dust2 = Dust.NewDustPerfect(beamEndPosiiton, 75);
				dust2.velocity = Vector2.UnitY.RotatedByRandom(MathHelper.ToRadians(55f)).RotatedBy(base.Projectile.rotation);
				dust2.noGravity = true;
				dust2.scale = 1.8f;
			}
			for (int l = 0; l < 16; l++)
			{
				Dust dust3 = Dust.NewDustPerfect(beamEndPosiiton, 75);
				dust3.velocity = ((float)l / 16f * ((float)Math.PI * 2f)).ToRotationVector2() * 3f;
				dust3.noGravity = true;
				dust3.scale = 2.4f;
			}
		}
		DelegateMethods.v3_1 = new Vector3(0.62f, 0.94f, 0.38f);
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CastLight);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		Texture2D laserTailTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/CragmawMireBeamBegin", (AssetRequestMode)1).Value;
		Texture2D laserBodyTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/CragmawMireBeamMid", (AssetRequestMode)1).Value;
		Texture2D laserHeadTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/CragmawMireBeamEnd", (AssetRequestMode)1).Value;
		float laserLength = base.Projectile.localAI[1];
		Color drawColor = new Color(1f, 1f, 1f) * 0.9f;
		Main.spriteBatch.Draw(laserTailTexture, base.Projectile.Center - Main.screenPosition, (Rectangle?)null, drawColor, base.Projectile.rotation, laserTailTexture.Size() / 2f, base.Projectile.scale, (SpriteEffects)0, 0f);
		laserLength -= (float)(laserTailTexture.Height / 2 + laserHeadTexture.Height) * base.Projectile.scale;
		Vector2 centerDelta = base.Projectile.Center;
		centerDelta += base.Projectile.velocity * base.Projectile.scale * (float)laserTailTexture.Height / 2f;
		if (laserLength > 0f)
		{
			float laserLengthDelta = 0f;
			Rectangle sourceRectangle = default(Rectangle);
			((Rectangle)(ref sourceRectangle))._002Ector(0, 16 * (base.Projectile.timeLeft / 3 % 5), laserBodyTexture.Width, 16);
			while (laserLengthDelta + 1f < laserLength)
			{
				if (laserLength - laserLengthDelta < (float)sourceRectangle.Height)
				{
					sourceRectangle.Height = (int)(laserLength - laserLengthDelta);
				}
				Main.spriteBatch.Draw(laserBodyTexture, centerDelta - Main.screenPosition, (Rectangle?)sourceRectangle, drawColor, base.Projectile.rotation, new Vector2((float)sourceRectangle.Width / 2f, 0f), base.Projectile.scale, (SpriteEffects)0, 0f);
				laserLengthDelta += (float)sourceRectangle.Height * base.Projectile.scale;
				centerDelta += base.Projectile.velocity * (float)sourceRectangle.Height * base.Projectile.scale;
				sourceRectangle.Y += 16;
				if (sourceRectangle.Y + sourceRectangle.Height > laserBodyTexture.Height)
				{
					sourceRectangle.Y = 0;
				}
			}
		}
		Main.spriteBatch.Draw(laserHeadTexture, centerDelta - Main.screenPosition, (Rectangle?)null, drawColor, base.Projectile.rotation, laserHeadTexture.Frame().Top(), base.Projectile.scale, (SpriteEffects)0, 0f);
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
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		if (((Rectangle)(ref projHitbox)).Intersects(targetHitbox))
		{
			return true;
		}
		float value = 0f;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], 22f * base.Projectile.scale, ref value))
		{
			return true;
		}
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Irradiated>(), 300);
		}
	}
}
