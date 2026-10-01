using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace CalamityMod.Projectiles.Ranged;

public class ExoLightningBolt : ModProjectile, ILocalizedModType, IModType
{
	public bool HasPlayedSound;

	public const int Lifetime = 45;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public ref float InitialVelocityAngle => ref base.Projectile.ai[0];

	public ref float BaseTurnAngleRatio => ref base.Projectile.ai[1];

	public ref float AccumulatedXMovementSpeeds => ref base.Projectile.localAI[0];

	public ref float BranchingIteration => ref base.Projectile.localAI[1];

	public virtual float LightningTurnRandomnessFactor { get; } = 2f;

	public override string Texture => "CalamityMod/Projectiles/LightningProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 50;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 22;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.MaxUpdates = 5;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 13;
		base.Projectile.timeLeft = base.Projectile.MaxUpdates * 45;
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
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		base.Projectile.oldPos[1] = base.Projectile.oldPos[0];
		float adjustedTimeLife = base.Projectile.timeLeft / base.Projectile.MaxUpdates;
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 9f, adjustedTimeLife, clamped: true) * Utils.GetLerpValue(45f, 42f, adjustedTimeLife, clamped: true);
		base.Projectile.scale = base.Projectile.Opacity;
		if (!HasPlayedSound)
		{
			SoundStyle style = HeavenlyGale.LightningStrikeSound with
			{
				Volume = 0.3f
			};
			SoundEngine.PlaySound(in style, Main.player[base.Projectile.owner].Center);
			HasPlayedSound = true;
		}
		Vector2 center = base.Projectile.Center;
		Color white = Color.White;
		Lighting.AddLight(center, ((Color)(ref white)).ToVector3());
		if (base.Projectile.frameCounter < base.Projectile.extraUpdates * 2)
		{
			return;
		}
		base.Projectile.frameCounter = 0;
		float originalSpeed = MathHelper.Min(15f, ((Vector2)(ref base.Projectile.velocity)).Length());
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
			if (Math.Abs(potentialBaseDirection.X * (float)(base.Projectile.extraUpdates + 1) * 2f * originalSpeed + AccumulatedXMovementSpeeds) > (float)base.Projectile.MaxUpdates * LightningTurnRandomnessFactor)
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

	public float PrimitiveWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return CalamityUtils.Convert01To010(completionRatio) * base.Projectile.scale * (float)base.Projectile.width;
	}

	public Color PrimitiveColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.MulticolorLerp((float)Math.Sin((float)base.Projectile.identity / 3f + completionRatio * 20f + Main.GlobalTimeWrappedHourly * 1.1f) * 0.5f + 0.5f, Color.GreenYellow, Color.Lime, Color.Cyan);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		List<Vector2> checkPoints = base.Projectile.oldPos.Where(delegate(Vector2 oldPos)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return oldPos != Vector2.Zero;
		}).ToList();
		if (checkPoints.Count <= 2)
		{
			return false;
		}
		for (int i = 0; i < checkPoints.Count - 1; i++)
		{
			float _ = 0f;
			float width = PrimitiveWidthFunction((float)i / (float)checkPoints.Count, Vector2.Zero);
			if (Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), checkPoints[i], checkPoints[i + 1], width * 0.8f, ref _))
			{
				return true;
			}
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		GameShaders.Misc["CalamityMod:HeavenlyGaleLightningArc"].UseImage1("Images/Misc/Perlin");
		GameShaders.Misc["CalamityMod:HeavenlyGaleLightningArc"].Apply();
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(PrimitiveWidthFunction, PrimitiveColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: false, pixelate: false, GameShaders.Misc["CalamityMod:HeavenlyGaleLightningArc"]), 18);
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SetCrit();
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}
}
