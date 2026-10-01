using System;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ScorpioRocket : ModProjectile, ILocalizedModType, IModType
{
	public static float TimeToLaunch = 15f;

	public static float TimeForFullPropulsion = 10f;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public ref float RocketID => ref base.Projectile.ai[0];

	public ref float ProjectileSpeed => ref base.Projectile.ai[1];

	public ref float Time => ref base.Projectile.ai[2];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.width = (base.Projectile.height = 34);
		base.Projectile.timeLeft = 600;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.friendly = true;
		base.Projectile.usesLocalNPCImmunity = true;
	}

	public override void AI()
	{
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		if (Time >= TimeToLaunch)
		{
			NPC target = base.Projectile.Center.ClosestNPCAt(Scorpio.EnemyDetectionDistance);
			float projectileSpeed = Utils.Remap(Time, TimeToLaunch, TimeToLaunch + TimeForFullPropulsion, 1f, ProjectileSpeed);
			if (target != null)
			{
				float targetDirectionRotation = base.Projectile.SafeDirectionTo(target.Center).ToRotation();
				float turningRate = Utils.Remap(Time, TimeToLaunch, TimeToLaunch + TimeForFullPropulsion, 0.01f, Scorpio.TrackingSpeed);
				base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleTowards(targetDirectionRotation, turningRate).ToRotationVector2() * projectileSpeed;
			}
			else
			{
				base.Projectile.velocity = base.Projectile.rotation.ToRotationVector2() * projectileSpeed;
			}
			if (Main.dedServ)
			{
				return;
			}
			base.Projectile.alpha = (int)Utils.Remap(base.Projectile.timeLeft, 30f, 0f, 0f, 255f);
			Vector2 position = base.Projectile.position;
			int width = base.Projectile.width;
			int height = base.Projectile.height;
			int dustEffectsID = ScorpioHoldout.DustEffectsID;
			float scale = Main.rand.NextFloat(0.8f, 1f);
			Dust dust = Dust.NewDustDirect(position, width, height, dustEffectsID, 0f, 0f, 0, default(Color), scale);
			dust.noGravity = true;
			dust.noLight = true;
			dust.noLightEmittence = true;
		}
		else
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.9f;
		}
		if (base.Projectile.wet && (RocketID == 4459f || RocketID == 4447f || RocketID == 4448f || RocketID == 4449f))
		{
			base.Projectile.Kill();
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 4)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
			base.Projectile.frameCounter = 0;
		}
		Time++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		int blastRadius = CalamityUtils.RocketBehavior(info: new CalamityUtils.RocketBehaviorInfo((int)RocketID), proj: base.Projectile);
		base.Projectile.ExpandHitboxBy((float)blastRadius);
		base.Projectile.Damage();
		if (!Main.dedServ)
		{
			int dustAmount = Main.rand.Next(25, 31);
			for (int i = 0; i < dustAmount; i++)
			{
				Vector2 center = base.Projectile.Center;
				int dustEffectsID = ScorpioHoldout.DustEffectsID;
				Vector2? velocity = ((float)Math.PI * 2f / (float)dustAmount * (float)i).ToRotationVector2() * Main.rand.NextFloat(4f, 10f);
				float scale = Main.rand.NextFloat(1f, 1.45f);
				Dust dust = Dust.NewDustPerfect(center, dustEffectsID, velocity, 0, default(Color), scale);
				dust.noGravity = true;
				dust.noLight = true;
				dust.noLightEmittence = true;
			}
			for (int j = 0; j < 7; j++)
			{
				float num = (float)base.Projectile.width / 35f;
				Vector2 velocity2 = Utils.RotatedByRandom(new Vector2(num, num), 100.0) * Main.rand.NextFloat(0.5f, 1.5f);
				GeneralParticleHandler.SpawnParticle(new NanoParticle(base.Projectile.Center, velocity2, Main.rand.NextBool(3) ? ScorpioHoldout.EffectsColor : ScorpioHoldout.StaticEffectsColor, Main.rand.NextFloat(1f, 1.5f), 40, Main.rand.NextBool(), emitsLight: true));
			}
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, ScorpioHoldout.StaticEffectsColor, Vector2.One, 0f, (float)base.Projectile.width / 2180f, (float)base.Projectile.width / 312f, 40));
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, ScorpioHoldout.EffectsColor * 0.6f, Vector2.One, 0f, (float)base.Projectile.width / 1755f, (float)base.Projectile.width / 175f, 20));
			SoundEngine.PlaySound(in Scorpio.RocketHit, base.Projectile.Center);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= 1f;
		if (base.Projectile.numHits > 1)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.2f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override bool? CanDamage()
	{
		if (!(Time >= TimeToLaunch))
		{
			return false;
		}
		return null;
	}

	public float TrailWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return Utils.Remap(completionRatio, 0f, 0.8f, 6f, 0f);
	}

	public Color TrailColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(ScorpioHoldout.EffectsColor, ScorpioHoldout.StaticEffectsColor * 0.75f, Utils.GetLerpValue(0f, 0.5f, completionRatio)) * Utils.GetLerpValue(255f, 0f, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Texture2D glowTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/ScorpioRocket_Glow", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation + (float)Math.PI / 2f;
		Vector2 rotationPoint = frame.Size() * 0.5f;
		if (Time >= TimeToLaunch)
		{
			GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
			PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(TrailWidthFunction, TrailColorFunction, delegate
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_0010: Unknown result type (might be due to invalid IL or missing references)
				return base.Projectile.Size * 0.5f;
			}, smoothen: false, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 25);
		}
		Main.EntitySpriteDraw(value, drawPosition, frame, drawColor, drawRotation, rotationPoint, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(glowTexture, drawPosition, frame, Color.White, drawRotation, rotationPoint, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
