using System;
using System.IO;
using CalamityMod.Items.Weapons.Melee;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Yoyos;

public class SmokingCometYoyo : ModProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<SmokingComet>();

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.YoyosLifeTimeMultiplier[base.Type] = SmokingComet.Duration;
		ProjectileID.Sets.YoyosMaximumRange[base.Type] = SmokingComet.Reach;
		ProjectileID.Sets.YoyosTopSpeed[base.Type] = SmokingComet.Speed;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.aiStyle = 99;
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation -= 0.25f;
		Vector2 velocity = base.Projectile.position - Main.player[base.Projectile.owner].position;
		if (((Vector2)(ref velocity)).Length() > 3200f)
		{
			base.Projectile.Kill();
		}
		base.Projectile.localAI[1]++;
		float starRainGateValue = 27f;
		if (base.Projectile.localAI[1] % starRainGateValue == 0f)
		{
			Vector2 starSpawnLocation = base.Projectile.Center + new Vector2((float)Main.rand.Next(-200, 201), -600f);
			Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), starSpawnLocation, Vector2.Normalize(base.Projectile.Center - starSpawnLocation) * 12f, 9, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			projectile.MaxUpdates = 2;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 40;
		}
		if (Main.rand.NextBool(5))
		{
			Dust.NewDust(base.Projectile.Center + new Vector2(-25f, -25f), 50, 50, 58, 0f, 0f, 150, default(Color), 1.2f);
		}
		if (Main.rand.NextBool(10))
		{
			IEntitySource source_FromAI = base.Projectile.GetSource_FromAI();
			Vector2 position = base.Projectile.position;
			velocity = default(Vector2);
			Gore.NewGore(source_FromAI, position, velocity, Main.rand.Next(16, 18));
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 28f, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 vector = Main.player[base.Projectile.owner].MountedCenter;
		vector.Y += Main.player[base.Projectile.owner].gfxOffY;
		float yoyoXVel = base.Projectile.Center.X - vector.X;
		float yoyoYVel = base.Projectile.Center.Y - vector.Y;
		Math.Sqrt(yoyoXVel * yoyoXVel + yoyoYVel * yoyoYVel);
		if (!base.Projectile.counterweight)
		{
			int distanceCheck = -1;
			if (base.Projectile.position.X + (float)(base.Projectile.width / 2) < Main.player[base.Projectile.owner].position.X + (float)(Main.player[base.Projectile.owner].width / 2))
			{
				distanceCheck = 1;
			}
			distanceCheck *= -1;
			Main.player[base.Projectile.owner].itemRotation = (float)Math.Atan2(yoyoYVel * (float)distanceCheck, yoyoXVel * (float)distanceCheck);
		}
		bool isActive = true;
		if (yoyoXVel == 0f && yoyoYVel == 0f)
		{
			isActive = false;
		}
		else
		{
			float yoyoVelocity = (float)Math.Sqrt(yoyoXVel * yoyoXVel + yoyoYVel * yoyoYVel);
			yoyoVelocity = 12f / yoyoVelocity;
			yoyoXVel *= yoyoVelocity;
			yoyoYVel *= yoyoVelocity;
			vector.X -= yoyoXVel * 0.1f;
			vector.Y -= yoyoYVel * 0.1f;
			yoyoXVel = base.Projectile.position.X + (float)base.Projectile.width * 0.5f - vector.X;
			yoyoYVel = base.Projectile.position.Y + (float)base.Projectile.height * 0.5f - vector.Y;
		}
		while (isActive)
		{
			float chainWidth = 12f;
			float yoyoVelocityAgain = (float)Math.Sqrt(yoyoXVel * yoyoXVel + yoyoYVel * yoyoYVel);
			float yoyoVelocityCopy = yoyoVelocityAgain;
			if (float.IsNaN(yoyoVelocityAgain) || float.IsNaN(yoyoVelocityCopy))
			{
				isActive = false;
				continue;
			}
			if (yoyoVelocityAgain < 20f)
			{
				chainWidth = yoyoVelocityAgain - 8f;
				isActive = false;
			}
			yoyoVelocityAgain = 12f / yoyoVelocityAgain;
			yoyoXVel *= yoyoVelocityAgain;
			yoyoYVel *= yoyoVelocityAgain;
			vector.X += yoyoXVel;
			vector.Y += yoyoYVel;
			yoyoXVel = base.Projectile.position.X + (float)base.Projectile.width * 0.5f - vector.X;
			yoyoYVel = base.Projectile.position.Y + (float)base.Projectile.height * 0.1f - vector.Y;
			if (yoyoVelocityCopy > 12f)
			{
				float absVelocityCheck = 0.3f;
				float absVelocity = Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y);
				if (absVelocity > 16f)
				{
					absVelocity = 16f;
				}
				absVelocity = 1f - absVelocity / 16f;
				absVelocityCheck *= absVelocity;
				absVelocity = yoyoVelocityCopy / 80f;
				if (absVelocity > 1f)
				{
					absVelocity = 1f;
				}
				absVelocityCheck *= absVelocity;
				if (absVelocityCheck < 0f)
				{
					absVelocityCheck = 0f;
				}
				absVelocityCheck *= absVelocity;
				absVelocityCheck *= 0.5f;
				if (yoyoYVel > 0f)
				{
					yoyoYVel *= 1f + absVelocityCheck;
					yoyoXVel *= 1f - absVelocityCheck;
				}
				else
				{
					absVelocity = Math.Abs(base.Projectile.velocity.X) / 3f;
					if (absVelocity > 1f)
					{
						absVelocity = 1f;
					}
					absVelocity -= 0.5f;
					absVelocityCheck *= absVelocity;
					if (absVelocityCheck > 0f)
					{
						absVelocityCheck *= 2f;
					}
					yoyoYVel *= 1f + absVelocityCheck;
					yoyoXVel *= 1f - absVelocityCheck;
				}
			}
			float stringHelper = (float)Math.Atan2(yoyoYVel, yoyoXVel) - (float)Math.PI / 2f;
			Texture2D stringTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/Yoyos/SmokingCometChain", (AssetRequestMode)2).Value;
			Main.spriteBatch.Draw(stringTexture, new Vector2(vector.X - Main.screenPosition.X + (float)ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/Yoyos/SmokingCometChain", (AssetRequestMode)2).Width() * 0.5f, vector.Y - Main.screenPosition.Y + (float)ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/Yoyos/SmokingCometChain", (AssetRequestMode)2).Height() * 0.5f) - new Vector2(6f, 0f), (Rectangle?)new Rectangle(0, 0, ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/Yoyos/SmokingCometChain", (AssetRequestMode)2).Width(), (int)chainWidth), Color.White, stringHelper, new Vector2((float)ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/Yoyos/SmokingCometChain", (AssetRequestMode)2).Width() * 0.5f, 0f), 1f, (SpriteEffects)0, 0f);
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
