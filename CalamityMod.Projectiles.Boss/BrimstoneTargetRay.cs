using System;
using System.IO;
using CalamityMod.NPCs.BrimstoneElemental;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class BrimstoneTargetRay : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.hostile = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.Projectile.scale = 0.1f;
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
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		Vector2? vector78 = null;
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		if (Main.npc[(int)base.Projectile.ai[1]].active && Main.npc[(int)base.Projectile.ai[1]].type == ModContent.NPCType<BrimstoneElemental>())
		{
			Vector2 fireFrom = default(Vector2);
			((Vector2)(ref fireFrom))._002Ector(Main.npc[(int)base.Projectile.ai[1]].Center.X + ((Main.npc[(int)base.Projectile.ai[1]].spriteDirection > 0) ? 34f : (-34f)), Main.npc[(int)base.Projectile.ai[1]].Center.Y - 74f);
			base.Projectile.position = fireFrom - new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f;
		}
		else
		{
			base.Projectile.Kill();
		}
		Vector2 laserVelocity = default(Vector2);
		((Vector2)(ref laserVelocity))._002Ector(Main.npc[(int)base.Projectile.ai[1]].Calamity().newAI[1], Main.npc[(int)base.Projectile.ai[1]].Calamity().newAI[2]);
		float rotationVelocity = ((base.Projectile.ai[0] == 0f) ? laserVelocity.ToRotation() : base.Projectile.velocity.ToRotation());
		base.Projectile.rotation = rotationVelocity - (float)Math.PI / 2f;
		base.Projectile.velocity = rotationVelocity.ToRotationVector2();
		if (base.Projectile.ai[0] == 0f)
		{
			if (Main.npc[(int)base.Projectile.ai[1]].ai[1] >= 150f)
			{
				base.Projectile.Kill();
				return;
			}
		}
		else if (Main.npc[(int)base.Projectile.ai[1]].ai[1] >= 180f)
		{
			base.Projectile.Kill();
			return;
		}
		float projWidth = base.Projectile.width;
		Vector2 samplingPoint = base.Projectile.Center;
		if (vector78.HasValue)
		{
			samplingPoint = vector78.Value;
		}
		float[] array3 = new float[3];
		Collision.LaserScan(samplingPoint, base.Projectile.velocity, projWidth * base.Projectile.scale, 2400f, array3);
		float rayLength = 0f;
		for (int i = 0; i < array3.Length; i++)
		{
			rayLength += array3[i];
		}
		rayLength /= 3f;
		if (!Collision.CanHitLine(Main.npc[(int)base.Projectile.ai[1]].Center, 1, 1, Main.player[Main.npc[(int)base.Projectile.ai[1]].target].Center, 1, 1))
		{
			rayLength = 2400f;
		}
		float amount = 0.5f;
		base.Projectile.localAI[1] = MathHelper.Lerp(base.Projectile.localAI[1], rayLength, amount);
		DelegateMethods.v3_1 = new Vector3(0.9f, 0.3f, 0.3f);
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CastLight);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		Texture2D texture2D19 = TextureAssets.Projectile[base.Type].Value;
		Texture2D texture2D20 = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/BrimstoneRayMid", (AssetRequestMode)1).Value;
		Texture2D texture2D21 = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/BrimstoneRayEnd", (AssetRequestMode)1).Value;
		float rayDrawLength = base.Projectile.localAI[1];
		Color baseColor = new Color(255, 255, 255, 0) * 0.9f;
		Vector2 vector = base.Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(texture2D19, vector, null, baseColor, base.Projectile.rotation, texture2D19.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		rayDrawLength -= (float)(texture2D19.Height / 2 + texture2D21.Height) * base.Projectile.scale;
		Vector2 projCenter = base.Projectile.Center;
		projCenter += base.Projectile.velocity * base.Projectile.scale * (float)texture2D19.Height / 2f;
		if (rayDrawLength > 0f)
		{
			float raySegment = 0f;
			Rectangle drawRectangle = default(Rectangle);
			((Rectangle)(ref drawRectangle))._002Ector(0, 16 * (base.Projectile.timeLeft / 3 % 5), texture2D20.Width, 16);
			while (raySegment + 1f < rayDrawLength)
			{
				if (rayDrawLength - raySegment < (float)drawRectangle.Height)
				{
					drawRectangle.Height = (int)(rayDrawLength - raySegment);
				}
				Main.EntitySpriteDraw(texture2D20, projCenter - Main.screenPosition, drawRectangle, baseColor, base.Projectile.rotation, new Vector2((float)(drawRectangle.Width / 2), 0f), base.Projectile.scale, (SpriteEffects)0);
				raySegment += (float)drawRectangle.Height * base.Projectile.scale;
				projCenter += base.Projectile.velocity * (float)drawRectangle.Height * base.Projectile.scale;
				drawRectangle.Y += 16;
				if (drawRectangle.Y + drawRectangle.Height > texture2D20.Height)
				{
					drawRectangle.Y = 0;
				}
			}
		}
		Vector2 vector2 = projCenter - Main.screenPosition;
		Main.EntitySpriteDraw(texture2D21, vector2, null, baseColor, base.Projectile.rotation, texture2D21.Frame().Top(), base.Projectile.scale, (SpriteEffects)0);
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
		float useless = 0f;
		if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], 22f * base.Projectile.scale, ref useless))
		{
			return true;
		}
		return false;
	}
}
