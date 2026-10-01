using System;
using System.IO;
using CalamityMod.NPCs;
using CalamityMod.NPCs.ExoMechs.Ares;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class AresLaserBeamStart : ModProjectile, ILocalizedModType, IModType
{
	private const int maxFrames = 5;

	private int frameDrawn;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.hostile = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.CooldownSlot = 1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(frameDrawn);
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		frameDrawn = reader.ReadInt32();
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0607: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_0629: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_059f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		Vector2? vector78 = null;
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		bool killLaser = false;
		if (CalamityGlobalNPC.draedonExoMechPrime != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechPrime].active)
		{
			killLaser = Main.npc[CalamityGlobalNPC.draedonExoMechPrime].Calamity().newAI[1] == 2f || Main.npc[CalamityGlobalNPC.draedonExoMechPrime].Calamity().newAI[0] == 1f;
		}
		if (Main.npc[(int)base.Projectile.ai[1]].active && Main.npc[(int)base.Projectile.ai[1]].type == ModContent.NPCType<AresLaserCannon>() && !killLaser)
		{
			float offset = 84f;
			float offset2 = 16f;
			Vector2 fireFrom = ((Main.npc[(int)base.Projectile.ai[1]].Calamity().newAI[3] == 0f) ? new Vector2(Main.npc[(int)base.Projectile.ai[1]].Center.X - offset2 * (float)Main.npc[(int)base.Projectile.ai[1]].direction, Main.npc[(int)base.Projectile.ai[1]].Center.Y + offset) : new Vector2(Main.npc[(int)base.Projectile.ai[1]].Center.X + offset * (float)Main.npc[(int)base.Projectile.ai[1]].direction, Main.npc[(int)base.Projectile.ai[1]].Center.Y + offset2));
			base.Projectile.position = fireFrom - new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f;
		}
		else
		{
			base.Projectile.Kill();
		}
		float projScale = 1f;
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] >= 60f)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.scale = (float)Math.Sin(base.Projectile.localAI[0] * (float)Math.PI / 60f) * 10f * projScale;
		if (base.Projectile.scale > projScale)
		{
			base.Projectile.scale = projScale;
		}
		float projVelRotation = base.Projectile.velocity.ToRotation();
		base.Projectile.rotation = projVelRotation - (float)Math.PI / 2f;
		base.Projectile.velocity = projVelRotation.ToRotationVector2();
		float projWidth = base.Projectile.width;
		Vector2 samplingPoint = base.Projectile.Center;
		if (vector78.HasValue)
		{
			samplingPoint = vector78.Value;
		}
		float[] array3 = new float[3];
		Collision.LaserScan(samplingPoint, base.Projectile.velocity, projWidth * base.Projectile.scale, 2400f, array3);
		float laserLength = 0f;
		for (int j = 0; j < array3.Length; j++)
		{
			laserLength += array3[j];
		}
		laserLength /= 3f;
		if (!Collision.CanHitLine(Main.npc[(int)base.Projectile.ai[1]].Center, 1, 1, Main.player[Main.npc[(int)base.Projectile.ai[1]].target].Center, 1, 1))
		{
			laserLength = 2400f;
		}
		float amount = 0.5f;
		base.Projectile.localAI[1] = MathHelper.Lerp(base.Projectile.localAI[1], laserLength, amount);
		int dustType = 235;
		Vector2 dustPos = base.Projectile.Center + base.Projectile.velocity * (base.Projectile.localAI[1] - 14f);
		Vector2 dustVel = default(Vector2);
		for (int i = 0; i < 2; i++)
		{
			float dustRot = base.Projectile.velocity.ToRotation() + (Main.rand.NextBool(2) ? (-1f) : 1f) * ((float)Math.PI / 2f);
			float dustVelMult = (float)Main.rand.NextDouble() * 2f + 2f;
			((Vector2)(ref dustVel))._002Ector((float)Math.Cos(dustRot) * dustVelMult, (float)Math.Sin(dustRot) * dustVelMult);
			int dust = Dust.NewDust(dustPos, 0, 0, dustType, dustVel.X, dustVel.Y);
			Main.dust[dust].noGravity = true;
			Main.dust[dust].scale = 1.7f;
		}
		if (Main.rand.NextBool(5))
		{
			Vector2 dustRot2 = base.Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * (float)base.Projectile.width;
			int dust2 = Dust.NewDust(dustPos + dustRot2 - Vector2.One * 4f, 8, 8, dustType, 0f, 0f, 100, default(Color), 1.5f);
			Dust obj = Main.dust[dust2];
			obj.velocity *= 0.5f;
			Main.dust[dust2].velocity.Y = 0f - Math.Abs(Main.dust[dust2].velocity.Y);
		}
		DelegateMethods.v3_1 = new Vector3(0.9f, 0.3f, 0.3f);
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CastLight);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		Texture2D beamStart = TextureAssets.Projectile[base.Type].Value;
		Texture2D beamMiddle = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/AresLaserBeamMiddle", (AssetRequestMode)1).Value;
		Texture2D beamEnd = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/AresLaserBeamEnd", (AssetRequestMode)1).Value;
		float drawLength = base.Projectile.localAI[1];
		Color color = default(Color);
		((Color)(ref color))._002Ector(250, 250, 250, 100);
		if (base.Projectile.localAI[0] % 5f == 0f)
		{
			frameDrawn++;
			if (frameDrawn >= 5)
			{
				frameDrawn = 0;
			}
		}
		Vector2 vector = base.Projectile.Center - Main.screenPosition;
		Rectangle? sourceRectangle = new Rectangle(0, beamStart.Height / 5 * frameDrawn, beamStart.Width, beamStart.Height / 5);
		Main.EntitySpriteDraw(beamStart, vector, sourceRectangle, color, base.Projectile.rotation, new Vector2((float)beamStart.Width, (float)(beamStart.Height / 5)) / 2f, base.Projectile.scale, (SpriteEffects)0);
		drawLength -= (float)(beamStart.Height / 5 / 2 + beamEnd.Height / 5) * base.Projectile.scale;
		Vector2 center = base.Projectile.Center;
		center += base.Projectile.velocity * base.Projectile.scale * (float)beamStart.Height / 5f / 2f;
		if (drawLength > 0f)
		{
			float i = 0f;
			int middleFrameDrawn = frameDrawn;
			Rectangle rectangle = default(Rectangle);
			while (i + 1f < drawLength)
			{
				((Rectangle)(ref rectangle))._002Ector(0, beamMiddle.Height / 5 * middleFrameDrawn, beamMiddle.Width, beamMiddle.Height / 5);
				if (drawLength - i < (float)rectangle.Height)
				{
					rectangle.Height = (int)(drawLength - i);
				}
				Main.EntitySpriteDraw(beamMiddle, center - Main.screenPosition, rectangle, color, base.Projectile.rotation, new Vector2((float)rectangle.Width / 2f, 0f), base.Projectile.scale, (SpriteEffects)0);
				middleFrameDrawn++;
				if (middleFrameDrawn >= 5)
				{
					middleFrameDrawn = 0;
				}
				i += (float)rectangle.Height * base.Projectile.scale;
				center += base.Projectile.velocity * (float)rectangle.Height * base.Projectile.scale;
				rectangle.Y += beamMiddle.Height / 5;
				if (rectangle.Y + rectangle.Height > beamMiddle.Height / 5)
				{
					rectangle.Y = 0;
				}
			}
		}
		Vector2 vector2 = center - Main.screenPosition;
		sourceRectangle = new Rectangle(0, beamEnd.Height / 5 * frameDrawn, beamEnd.Width, beamEnd.Height / 5);
		Main.EntitySpriteDraw(beamEnd, vector2, sourceRectangle, color, base.Projectile.rotation, new Vector2((float)beamEnd.Width, (float)(beamEnd.Height / 5)) / 2f, base.Projectile.scale, (SpriteEffects)0);
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
		float num6 = 0f;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], 30f * base.Projectile.scale, ref num6))
		{
			return true;
		}
		return false;
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.scale >= 0.5f;
	}
}
