using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Events;
using CalamityMod.NPCs.BrimstoneElemental;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class BrimstoneRay : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.hostile = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.CooldownSlot = 1;
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
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_075a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0765: Unknown result type (might be due to invalid IL or missing references)
		//IL_0770: Unknown result type (might be due to invalid IL or missing references)
		//IL_077b: Unknown result type (might be due to invalid IL or missing references)
		//IL_078d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0792: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_0712: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		Vector2? vector78 = null;
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		if (Main.npc[(int)base.Projectile.ai[1]].active && Main.npc[(int)base.Projectile.ai[1]].type == ModContent.NPCType<BrimstoneElemental>())
		{
			if (Main.npc[(int)base.Projectile.ai[1]].ai[0] == 5f)
			{
				Vector2 fireFrom = default(Vector2);
				((Vector2)(ref fireFrom))._002Ector(Main.npc[(int)base.Projectile.ai[1]].Center.X + ((Main.npc[(int)base.Projectile.ai[1]].spriteDirection > 0) ? 34f : (-34f)), Main.npc[(int)base.Projectile.ai[1]].Center.Y - 74f);
				base.Projectile.position = fireFrom - new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f;
			}
		}
		else
		{
			base.Projectile.Kill();
		}
		float projScale = 1f;
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] >= 45f)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.scale = (float)Math.Sin(base.Projectile.localAI[0] * (float)Math.PI / 45f) * 10f * projScale;
		if (base.Projectile.scale > projScale)
		{
			base.Projectile.scale = projScale;
		}
		float projVelRotation = base.Projectile.velocity.ToRotation();
		if (base.Projectile.ai[0] > 0f)
		{
			projVelRotation += (float)Main.npc[(int)base.Projectile.ai[1]].spriteDirection * ((float)Math.PI * 2f) / 360f;
		}
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
		base.Projectile.localAI[1] = MathHelper.Lerp(base.Projectile.localAI[1], rayLength, 0.5f);
		Vector2 dustSpawnPos = base.Projectile.Center + base.Projectile.velocity * (base.Projectile.localAI[1] - 14f);
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (Main.npc[(int)base.Projectile.ai[1]].ai[1] == 210f && base.Projectile.owner == Main.myPlayer && Main.npc[(int)base.Projectile.ai[1]].ai[0] == 5f)
		{
			Vector2 velocity = base.Projectile.velocity;
			((Vector2)(ref velocity)).Normalize();
			float distanceBetweenProjectiles = (Main.zenithWorld ? 360f : 144f);
			Vector2 fireFrom2 = new Vector2(Main.npc[(int)base.Projectile.ai[1]].Center.X + ((Main.npc[(int)base.Projectile.ai[1]].spriteDirection > 0) ? 34f : (-34f)), Main.npc[(int)base.Projectile.ai[1]].Center.Y - 74f) + velocity * distanceBetweenProjectiles;
			int projectileAmt = (int)(base.Projectile.localAI[1] / distanceBetweenProjectiles);
			int type = ModContent.ProjectileType<BrimstoneBarrage>();
			float projectileVelocityToPass = 12f;
			for (int j = 0; j < projectileAmt; j++)
			{
				int totalProjectiles = 2;
				float radians = (float)Math.PI * 2f / (float)totalProjectiles;
				for (int k = 0; k < totalProjectiles; k++)
				{
					Vector2 projVelocity = base.Projectile.velocity.RotatedBy(radians * (float)k + (float)Math.PI / 2f);
					int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), fireFrom2, projVelocity, type, BrimstoneElemental.DartDamage, 0f, Main.myPlayer, death ? 2f : 1f, 0f, projectileVelocityToPass);
					Main.projectile[proj].tileCollide = true;
				}
				fireFrom2 += velocity * distanceBetweenProjectiles;
			}
		}
		Vector2 dustDirection = default(Vector2);
		for (int l = 0; l < 2; l++)
		{
			float dustRotation = base.Projectile.velocity.ToRotation() + (Main.rand.NextBool(2) ? (-1f) : 1f) * ((float)Math.PI / 2f);
			float randomFloatOffset = (float)Main.rand.NextDouble() * 2f + 2f;
			((Vector2)(ref dustDirection))._002Ector((float)Math.Cos(dustRotation) * randomFloatOffset, (float)Math.Sin(dustRotation) * randomFloatOffset);
			int brimDust = Dust.NewDust(dustSpawnPos, 0, 0, 235, dustDirection.X, dustDirection.Y);
			Main.dust[brimDust].noGravity = true;
			Main.dust[brimDust].scale = 1.7f;
		}
		if (Main.rand.NextBool(5))
		{
			Vector2 extraDustSpawn = base.Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * (float)base.Projectile.width;
			int extraBrimDust = Dust.NewDust(dustSpawnPos + extraDustSpawn - Vector2.One * 4f, 8, 8, 235, 0f, 0f, 100, default(Color), 1.5f);
			Dust obj = Main.dust[extraBrimDust];
			obj.velocity *= 0.5f;
			Main.dust[extraBrimDust].velocity.Y = 0f - Math.Abs(Main.dust[extraBrimDust].velocity.Y);
		}
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
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
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
			((Rectangle)(ref drawRectangle))._002Ector(0, 0, texture2D20.Width, texture2D20.Height);
			while (raySegment + 1f < rayDrawLength)
			{
				if (rayDrawLength - raySegment < (float)drawRectangle.Height)
				{
					drawRectangle.Height = (int)(rayDrawLength - raySegment);
				}
				Main.EntitySpriteDraw(texture2D20, projCenter - Main.screenPosition, drawRectangle, baseColor, base.Projectile.rotation, new Vector2((float)(drawRectangle.Width / 2), 0f), base.Projectile.scale, (SpriteEffects)0);
				raySegment += (float)drawRectangle.Height * base.Projectile.scale;
				projCenter += base.Projectile.velocity * (float)drawRectangle.Height * base.Projectile.scale;
				drawRectangle.Y += texture2D20.Height;
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

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 240);
		}
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.scale >= 0.5f;
	}
}
