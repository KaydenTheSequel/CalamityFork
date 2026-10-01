using System;
using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace CalamityMod.Projectiles.Rogue;

public class DynamicPursuerElectricity : ModProjectile, ILocalizedModType, IModType
{
	public const int MaximumBranchingIterations = 3;

	public const float LightningTurnRandomnessFactor = 1.7f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public ref float InitialVelocityAngle => ref base.Projectile.ai[0];

	public ref float BaseTurnAngleRatio => ref base.Projectile.ai[1];

	public ref float AccumulatedXMovementSpeeds => ref base.Projectile.localAI[0];

	public ref float BranchingIteration => ref base.Projectile.localAI[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 50;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 2;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 10;
		base.Projectile.timeLeft = 60 * base.Projectile.extraUpdates;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
		if (base.Projectile.velocity != Vector2.Zero)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity /= (float)base.Projectile.extraUpdates;
		}
	}

	public override void AI()
	{
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		_ = base.Projectile.timeLeft / base.Projectile.MaxUpdates;
		base.Projectile.scale = (float)Math.Sin((float)Math.PI * (float)base.Projectile.timeLeft / (45f * (float)(base.Projectile.MaxUpdates - 1))) * 4f;
		if (base.Projectile.scale > 1f)
		{
			base.Projectile.scale = 1f;
		}
		Vector2 center = base.Projectile.Center;
		Color blue = Color.Blue;
		Lighting.AddLight(center, ((Color)(ref blue)).ToVector3());
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
		while (turnTries < 20);
		if (base.Projectile.velocity != Vector2.Zero)
		{
			AccumulatedXMovementSpeeds += newBaseDirection.X * (float)(base.Projectile.extraUpdates + 1) * 2f * originalSpeed;
			base.Projectile.velocity = newBaseDirection.RotatedBy(InitialVelocityAngle + (float)Math.PI / 2f) * originalSpeed;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		}
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return MathHelper.Lerp(4f, 7f, (float)Math.Sin((float)Math.PI * 4f * completionRatio) * 0.5f + 0.5f) * base.Projectile.scale * (float)Math.Sin((float)Math.PI * completionRatio);
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(Color.Lerp(Color.Blue, Color.LightSteelBlue, (float)Math.Sin((float)Math.PI * 2f * completionRatio + Main.GlobalTimeWrappedHourly * 4f) * 0.5f + 0.5f), Color.LightBlue, ((float)Math.Sin((float)Math.PI * completionRatio + Main.GlobalTimeWrappedHourly * 4f) * 0.5f + 0.5f) * 0.8f);
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
