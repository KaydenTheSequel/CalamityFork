using System;
using System.IO;
using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace CalamityMod.Projectiles.Summon;

public class DaedalusLightning : ModProjectile, ILocalizedModType, IModType
{
	public const int MaximumBranchingIterations = 3;

	public const float LightningTurnRandomnessFactor = 1.7f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float InitialVelocityAngle => ref base.Projectile.ai[0];

	public ref float BaseTurnAngleRatio => ref base.Projectile.ai[1];

	public ref float AccumulatedXMovementSpeeds => ref base.Projectile.localAI[0];

	public ref float BranchingIteration => ref base.Projectile.localAI[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 100;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.width = (base.Projectile.height = 14);
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 2;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 20;
		base.Projectile.timeLeft = 45 * base.Projectile.extraUpdates;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
		if (base.Projectile.velocity != Vector2.Zero)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity /= (float)base.Projectile.extraUpdates;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(AccumulatedXMovementSpeeds);
		writer.Write(BranchingIteration);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		AccumulatedXMovementSpeeds = reader.ReadSingle();
		BranchingIteration = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		base.Projectile.scale = (float)Math.Sin((float)Math.PI * (float)base.Projectile.timeLeft / (45f * (float)(base.Projectile.MaxUpdates - 1))) * 4f;
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

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return MathHelper.Lerp(2f, 6f, (float)Math.Sin((float)Math.PI * 4f * completionRatio) * 0.5f + 0.5f) * base.Projectile.scale * (float)Math.Sin((float)Math.PI * completionRatio);
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(Color.Lerp(Color.Pink, Color.HotPink, (float)Math.Sin((float)Math.PI * 2f * completionRatio + Main.GlobalTimeWrappedHourly * 4f) * 0.5f + 0.5f), Color.Red, ((float)Math.Sin((float)Math.PI * completionRatio + Main.GlobalTimeWrappedHourly * 4f) * 0.5f + 0.5f) * 0.8f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: false), 90);
		return false;
	}
}
