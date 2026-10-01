using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class MidnightSunBeam : ModProjectile, ILocalizedModType, IModType
{
	public const int TrueTimeLeft = 120;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 120;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
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
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		Projectile body = Main.projectile[(int)base.Projectile.ai[1]];
		if (body.type != ModContent.ProjectileType<MidnightSunUFO>() || !body.active)
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		if (Main.projectile[(int)base.Projectile.ai[1]].active)
		{
			base.Projectile.Center = Main.projectile[(int)base.Projectile.ai[1]].Center;
		}
		if (base.Projectile.velocity.HasNaNs() || base.Projectile.velocity == Vector2.Zero)
		{
			base.Projectile.velocity = -Vector2.UnitY;
		}
		float laserSize = 1f;
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
		float velocityAsRotation = body.rotation + (float)Math.PI / 2f;
		base.Projectile.rotation = velocityAsRotation - (float)Math.PI / 2f;
		base.Projectile.velocity = velocityAsRotation.ToRotationVector2();
		_ = base.Projectile.Center;
		_ = new float[3];
		base.Projectile.localAI[1] = body.ai[1];
		DelegateMethods.v3_1 = new Vector3(0.52f, 0.93f, 0.97f);
		Utils.PlotTileLine(base.Projectile.Center, base.Projectile.Center + base.Projectile.velocity * base.Projectile.localAI[1], (float)base.Projectile.width * base.Projectile.scale, DelegateMethods.CastLight);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity == Vector2.Zero)
		{
			return false;
		}
		Texture2D laserTailTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/MidnightSunBeamBegin", (AssetRequestMode)1).Value;
		Texture2D laserBodyTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/MidnightSunBeamMid", (AssetRequestMode)1).Value;
		Texture2D laserHeadTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/MidnightSunBeamEnd", (AssetRequestMode)1).Value;
		float laserLength = base.Projectile.localAI[1];
		Color drawColor = new Color(1f, 1f, 1f) * 0.9f;
		Main.EntitySpriteDraw(laserTailTexture, base.Projectile.Center - Main.screenPosition, null, drawColor, base.Projectile.rotation, laserTailTexture.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
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
				Main.EntitySpriteDraw(laserBodyTexture, centerDelta - Main.screenPosition, sourceRectangle, drawColor, base.Projectile.rotation, new Vector2((float)sourceRectangle.Width / 2f, 0f), base.Projectile.scale, (SpriteEffects)0);
				laserLengthDelta += (float)sourceRectangle.Height * base.Projectile.scale;
				centerDelta += base.Projectile.velocity * (float)sourceRectangle.Height * base.Projectile.scale;
				sourceRectangle.Y += 16;
				if (sourceRectangle.Y + sourceRectangle.Height > laserBodyTexture.Height)
				{
					sourceRectangle.Y = 0;
				}
			}
		}
		Main.EntitySpriteDraw(laserHeadTexture, centerDelta - Main.screenPosition, null, drawColor, base.Projectile.rotation, laserHeadTexture.Frame().Top(), base.Projectile.scale, (SpriteEffects)0);
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

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AuricRebuke>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<AuricRebuke>(), 120);
	}
}
