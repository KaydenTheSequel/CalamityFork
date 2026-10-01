using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace CalamityMod.Projectiles.Boss;

public class RedLightning : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/LightningProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.hostile = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 20;
		base.Projectile.timeLeft = 1260;
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
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0597: Unknown result type (might be due to invalid IL or missing references)
		//IL_059c: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_060c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.frameCounter == 0 || base.Projectile.oldPos[0] == Vector2.Zero)
		{
			for (int i = base.Projectile.oldPos.Length - 1; i > 0; i--)
			{
				base.Projectile.oldPos[i] = base.Projectile.oldPos[i - 1];
			}
			base.Projectile.oldPos[0] = base.Projectile.position;
			if (base.Projectile.velocity == Vector2.Zero)
			{
				float dustRotation = base.Projectile.rotation + (float)Math.PI / 2f + (Main.rand.NextBool(2) ? (-1f) : 1f) * ((float)Math.PI / 2f);
				float randDustRotateMod = (float)Main.rand.NextDouble() * 2f + 2f;
				Vector2 dustVelocity = default(Vector2);
				((Vector2)(ref dustVelocity))._002Ector((float)Math.Cos(dustRotation) * randDustRotateMod, (float)Math.Sin(dustRotation) * randDustRotateMod);
				int redDust = Dust.NewDust(base.Projectile.oldPos[base.Projectile.oldPos.Length - 1], 0, 0, 60, dustVelocity.X, dustVelocity.Y);
				Main.dust[redDust].noGravity = true;
				Main.dust[redDust].scale = 1.7f;
			}
		}
		int inc = base.Projectile.frameCounter;
		base.Projectile.frameCounter = inc + 1;
		Lighting.AddLight(base.Projectile.Center, 0.8f, 0.25f, 0.15f);
		if (base.Projectile.velocity == Vector2.Zero)
		{
			if (base.Projectile.frameCounter >= base.Projectile.extraUpdates * 2)
			{
				base.Projectile.frameCounter = 0;
				bool lightningExpired = true;
				for (int j = 1; j < base.Projectile.oldPos.Length; j++)
				{
					if (base.Projectile.oldPos[j] != base.Projectile.oldPos[0])
					{
						lightningExpired = false;
					}
				}
				if (lightningExpired)
				{
					base.Projectile.Kill();
					return;
				}
			}
			if (Main.rand.NextBool(base.Projectile.extraUpdates))
			{
				Vector2 dustVelocity2 = default(Vector2);
				for (int k = 0; k < 2; k++)
				{
					float extraDustRotate = base.Projectile.rotation + (Main.rand.NextBool(2) ? (-1f) : 1f) * ((float)Math.PI / 2f);
					float extraRandRotate = (float)Main.rand.NextDouble() * 0.8f + 1f;
					((Vector2)(ref dustVelocity2))._002Ector((float)Math.Cos(extraDustRotate) * extraRandRotate, (float)Math.Sin(extraDustRotate) * extraRandRotate);
					int extraRedDust = Dust.NewDust(base.Projectile.Center, 0, 0, 60, dustVelocity2.X, dustVelocity2.Y);
					Main.dust[extraRedDust].noGravity = true;
					Main.dust[extraRedDust].scale = 1.2f;
				}
				if (Main.rand.NextBool(5))
				{
					Vector2 moreDustRotation = base.Projectile.velocity.RotatedBy(1.5707963705062866) * ((float)Main.rand.NextDouble() - 0.5f) * (float)base.Projectile.width;
					int moreExtraRedDust = Dust.NewDust(base.Projectile.Center + moreDustRotation - Vector2.One * 4f, 8, 8, 60, 0f, 0f, 100, default(Color), 1.5f);
					Dust obj = Main.dust[moreExtraRedDust];
					obj.velocity *= 0.5f;
					Main.dust[moreExtraRedDust].velocity.Y = 0f - Math.Abs(Main.dust[moreExtraRedDust].velocity.Y);
				}
			}
		}
		else
		{
			if (base.Projectile.frameCounter < base.Projectile.extraUpdates * 2)
			{
				return;
			}
			base.Projectile.frameCounter = 0;
			float projSpeed = ((Vector2)(ref base.Projectile.velocity)).Length();
			UnifiedRandom unifiedRandom = new UnifiedRandom((int)base.Projectile.ai[1]);
			int frameIncrement = 0;
			Vector2 randomLightningMovement = -Vector2.UnitY;
			while (true)
			{
				int randomIncrement = unifiedRandom.Next();
				base.Projectile.ai[1] = randomIncrement;
				randomIncrement %= 100;
				Vector2 lightningYRotation = ((float)randomIncrement / 100f * ((float)Math.PI * 2f)).ToRotationVector2();
				if (lightningYRotation.Y > 0f)
				{
					lightningYRotation.Y *= -1f;
				}
				bool stopLightning = false;
				if (lightningYRotation.Y > -0.02f)
				{
					stopLightning = true;
				}
				if (lightningYRotation.X * (float)(base.Projectile.extraUpdates + 1) * 2f * projSpeed + base.Projectile.localAI[0] > 40f)
				{
					stopLightning = true;
				}
				if (lightningYRotation.X * (float)(base.Projectile.extraUpdates + 1) * 2f * projSpeed + base.Projectile.localAI[0] < -40f)
				{
					stopLightning = true;
				}
				if (stopLightning)
				{
					if (frameIncrement++ >= 100)
					{
						base.Projectile.velocity = Vector2.Zero;
						base.Projectile.localAI[1] = 1f;
						break;
					}
					continue;
				}
				randomLightningMovement = lightningYRotation;
				break;
			}
			if (base.Projectile.velocity != Vector2.Zero)
			{
				base.Projectile.localAI[0] += randomLightningMovement.X * (float)(base.Projectile.extraUpdates + 1) * 2f * projSpeed;
				base.Projectile.velocity = randomLightningMovement.RotatedBy(base.Projectile.ai[0] + (float)Math.PI / 2f) * projSpeed;
				base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<VermillionFlux>(), 120);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		Vector2 end = base.Projectile.position + new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition;
		Texture2D tex3 = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Lasers/RedLightningTexture", (AssetRequestMode)2).Value;
		base.Projectile.GetAlpha(lightColor);
		Vector2 lightningScale = new Vector2(base.Projectile.scale) / 2f;
		for (int i = 0; i < 3; i++)
		{
			switch (i)
			{
			case 0:
				lightningScale = new Vector2(base.Projectile.scale) * 0.6f;
				DelegateMethods.c_1 = new Color(219, 104, 58, 0) * 0.5f;
				break;
			case 1:
				lightningScale = new Vector2(base.Projectile.scale) * 0.4f;
				DelegateMethods.c_1 = new Color(255, 126, 56, 0) * 0.5f;
				break;
			default:
				lightningScale = new Vector2(base.Projectile.scale) * 0.2f;
				DelegateMethods.c_1 = new Color(255, 128, 128, 0) * 0.5f;
				break;
			}
			DelegateMethods.f_1 = 1f;
			for (int j = base.Projectile.oldPos.Length - 1; j > 0; j--)
			{
				if (!(base.Projectile.oldPos[j] == Vector2.Zero))
				{
					Vector2 start = base.Projectile.oldPos[j] + new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition;
					Vector2 end2 = base.Projectile.oldPos[j - 1] + new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition;
					Utils.DrawLaser(Main.spriteBatch, tex3, start, end2, lightningScale, DelegateMethods.LightningLaserDraw);
				}
			}
			if (base.Projectile.oldPos[0] != Vector2.Zero)
			{
				DelegateMethods.f_1 = 1f;
				Vector2 start2 = base.Projectile.oldPos[0] + new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition;
				Utils.DrawLaser(Main.spriteBatch, tex3, start2, end, lightningScale, DelegateMethods.LightningLaserDraw);
			}
		}
		return false;
	}
}
