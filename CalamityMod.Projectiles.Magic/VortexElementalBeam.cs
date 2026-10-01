using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace CalamityMod.Projectiles.Magic;

public class VortexElementalBeam : ModProjectile, ILocalizedModType, IModType
{
	public const int Lifetime = 30;

	public const float LightningTurnRandomnessFactor = 1.7f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float InitialVelocityAngle => ref base.Projectile.ai[0];

	public ref float BaseTurnAngleRatio => ref base.Projectile.ai[1];

	public ref float AccumulatedXMovementSpeeds => ref base.Projectile.localAI[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 30;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 20;
		base.Projectile.timeLeft = 45 * base.Projectile.extraUpdates;
		if (base.Projectile.velocity != Vector2.Zero)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity /= (float)base.Projectile.extraUpdates;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(AccumulatedXMovementSpeeds);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		AccumulatedXMovementSpeeds = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		base.Projectile.scale += 0.04f / (float)base.Projectile.MaxUpdates;
		if (base.Projectile.scale > 1f)
		{
			base.Projectile.scale = 1f;
		}
		Vector2 center = base.Projectile.Center;
		Color pink = Color.Pink;
		Lighting.AddLight(center, ((Color)(ref pink)).ToVector3());
		if (base.Projectile.frameCounter < base.Projectile.extraUpdates * 2)
		{
			return;
		}
		base.Projectile.frameCounter = 0;
		float originalSpeed = MathHelper.Min(6f, ((Vector2)(ref base.Projectile.velocity)).Length());
		UnifiedRandom unifiedRandom = new UnifiedRandom((int)BaseTurnAngleRatio);
		int turnTries = 0;
		Vector2 newBaseDirection = -Vector2.UnitY;
		do
		{
			BaseTurnAngleRatio = unifiedRandom.Next() % 100;
			Vector2 potentialBaseDirection = (BaseTurnAngleRatio / 100f * ((float)Math.PI * 2f)).ToRotationVector2();
			potentialBaseDirection.Y = 0f - Math.Abs(potentialBaseDirection.Y);
			bool canChangeLightningDirection = true;
			if (potentialBaseDirection.Y > -0.02f)
			{
				canChangeLightningDirection = false;
			}
			if (Math.Abs(potentialBaseDirection.X * (float)(base.Projectile.extraUpdates + 1) * 2f * originalSpeed + AccumulatedXMovementSpeeds) > (float)base.Projectile.MaxUpdates * 1.7f)
			{
				canChangeLightningDirection = false;
			}
			if (canChangeLightningDirection)
			{
				newBaseDirection = potentialBaseDirection;
			}
			turnTries++;
		}
		while (turnTries < 100);
		if (base.Projectile.velocity != Vector2.Zero)
		{
			AccumulatedXMovementSpeeds += newBaseDirection.X * (float)(base.Projectile.extraUpdates + 1) * 2f * originalSpeed;
			base.Projectile.velocity = newBaseDirection.RotatedBy(InitialVelocityAngle + (float)Math.PI / 2f) * originalSpeed;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		Texture2D lightningSegmentTexture = TextureAssets.Projectile[base.Type].Value;
		base.Projectile.GetAlpha(lightColor);
		Vector2 lightningScale = new Vector2(base.Projectile.scale) / 2f;
		for (int i = 0; i < 3; i++)
		{
			switch (i)
			{
			case 0:
				lightningScale = new Vector2(base.Projectile.scale) * 0.6f;
				DelegateMethods.c_1 = Color.DarkCyan * 0.85f;
				break;
			case 1:
				lightningScale = new Vector2(base.Projectile.scale) * 0.4f;
				DelegateMethods.c_1 = Color.Cyan * 0.85f;
				break;
			default:
				lightningScale = new Vector2(base.Projectile.scale) * 0.2f;
				DelegateMethods.c_1 = Color.White;
				break;
			}
			DelegateMethods.f_1 = 1f;
			for (int j = base.Projectile.oldPos.Length - 1; j > 0; j--)
			{
				if (base.Projectile.oldPos[j] != Vector2.Zero)
				{
					Vector2 start = base.Projectile.oldPos[j] + base.Projectile.Size * 0.5f + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition;
					Vector2 end = base.Projectile.oldPos[j - 1] + base.Projectile.Size * 0.5f + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition;
					Utils.DrawLaser(Main.spriteBatch, lightningSegmentTexture, start, end, lightningScale, DelegateMethods.LightningLaserDraw);
				}
			}
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 30);
		for (int i = 0; i < base.Projectile.oldPos.Length - 1; i++)
		{
			if (!(base.Projectile.oldPos[i + 1] == Vector2.Zero))
			{
				for (int j = 0; j < 8; j++)
				{
					Dust dust = Dust.NewDustPerfect(Vector2.Lerp(base.Projectile.oldPos[i], base.Projectile.oldPos[i + 1], (float)j / 8f) + base.Projectile.Size * 0.5f, Main.rand.NextBool() ? 226 : 229);
					dust.velocity = Vector2.Zero;
					dust.scale = Main.rand.NextFloat(1.1f, 1.18f);
					dust.noGravity = true;
				}
			}
		}
	}
}
