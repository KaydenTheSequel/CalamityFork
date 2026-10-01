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

public class ScorpioLargeRocket : ModProjectile, ILocalizedModType, IModType
{
	public static int Lifetime = 600;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public ref float RocketID => ref base.Projectile.ai[0];

	public ref float ProjectileSpeed => ref base.Projectile.ai[1];

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
		base.Projectile.MaxUpdates = 3;
		base.Projectile.width = (base.Projectile.height = 15);
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		NPC target = base.Projectile.Center.ClosestNPCAt(Scorpio.NukeEnemyDistanceDetection);
		if (target != null)
		{
			Vector2 targetDirection = base.Projectile.SafeDirectionTo(target.Center);
			float trackingSpeed = ((Vector2.Dot(targetDirection, base.Projectile.rotation.ToRotationVector2()) > Scorpio.NukeRequiredRotationProximity) ? Scorpio.NukeTrackingSpeed : 0f);
			base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleTowards(targetDirection.ToRotation(), trackingSpeed).ToRotationVector2() * ProjectileSpeed;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1.032f;
		if (base.Projectile.wet && (RocketID == 4459f || RocketID == 4447f || RocketID == 4448f || RocketID == 4449f))
		{
			base.Projectile.Kill();
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 4)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
			base.Projectile.frameCounter = 0;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (!Main.dedServ)
		{
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
			if (base.Projectile.timeLeft < Lifetime - 5)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center - base.Projectile.velocity * 2f, base.Projectile.velocity * 0.01f, affectedByGravity: false, 8, 1.3f, ScorpioHoldout.StaticEffectsColor));
			}
			if (base.Projectile.timeLeft % 3 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new NanoParticle(base.Projectile.Center, -base.Projectile.velocity.RotatedBy(Main.rand.NextFloat(-0.2f, 0.2f)), Main.rand.NextBool(3) ? ScorpioHoldout.EffectsColor : ScorpioHoldout.StaticEffectsColor, Main.rand.NextFloat(0.65f, 0.9f), Main.rand.Next(15, 21), Main.rand.NextBool(), emitsLight: true));
			}
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center + base.Projectile.velocity * 1.5f, Vector2.Zero, ScorpioHoldout.StaticEffectsColor * 2f, Vector2.One, 0f, 0.25f, 0.25f, 2));
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= 15f;
		if (base.Projectile.numHits == 1)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.89f);
		}
		if (base.Projectile.numHits > 1)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.92f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		modifiers.SourceDamage *= 1.8f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.RocketBehaviorInfo rocketBehaviorInfo = new CalamityUtils.RocketBehaviorInfo((int)RocketID);
		rocketBehaviorInfo.smallRadius = 13;
		rocketBehaviorInfo.mediumRadius = 26;
		rocketBehaviorInfo.largeRadius = 40;
		CalamityUtils.RocketBehaviorInfo info = rocketBehaviorInfo;
		int blastRadius = base.Projectile.RocketBehavior(info);
		base.Projectile.ExpandHitboxBy(blastRadius);
		base.Projectile.Damage();
		if (!Main.dedServ)
		{
			int dustAmount = Main.rand.Next(30, 36);
			for (int i = 0; i < dustAmount; i++)
			{
				Vector2 center = base.Projectile.Center;
				int dustEffectsID = ScorpioHoldout.DustEffectsID;
				Vector2? velocity = ((float)Math.PI * 2f / (float)dustAmount * (float)i).ToRotationVector2() * Main.rand.NextFloat(4f, 10f);
				float scale = Main.rand.NextFloat(1.2f, 1.75f);
				Dust dust = Dust.NewDustPerfect(center, dustEffectsID, velocity, 0, default(Color), scale);
				dust.noGravity = true;
				dust.noLight = true;
				dust.noLightEmittence = true;
			}
			for (int j = 0; j < 40; j++)
			{
				float num = (float)base.Projectile.width / 33f;
				Vector2 velocity2 = Utils.RotatedByRandom(new Vector2(num, num), 100.0) * Main.rand.NextFloat(0.3f, 1.7f);
				GeneralParticleHandler.SpawnParticle(new NanoParticle(base.Projectile.Center, velocity2, Main.rand.NextBool(3) ? ScorpioHoldout.EffectsColor : ScorpioHoldout.StaticEffectsColor, Main.rand.NextFloat(2f, 3f), 45, Main.rand.NextBool(), emitsLight: true));
			}
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, ScorpioHoldout.StaticEffectsColor, Vector2.One, 0f, (float)base.Projectile.width / 2180f, (float)base.Projectile.width / 312f, 40));
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, ScorpioHoldout.EffectsColor * 0.6f, Vector2.One, 0f, (float)base.Projectile.width / 1755f, (float)base.Projectile.width / 175f, 20));
			Vector2 BurstFXDirection = default(Vector2);
			((Vector2)(ref BurstFXDirection))._002Ector(15f, 0f);
			for (int k = 0; k < 4; k++)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, BurstFXDirection * (float)(k + 1), affectedByGravity: false, 11, 5f - (float)k * 0.6f, ScorpioHoldout.StaticEffectsColor * 0.8f));
			}
			for (int l = 0; l < 4; l++)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, -BurstFXDirection * (float)(l + 1), affectedByGravity: false, 11, 5f - (float)l * 0.6f, ScorpioHoldout.StaticEffectsColor * 0.8f));
			}
			GeneralParticleHandler.SpawnParticle(new GenericBloom(base.Projectile.Center, Vector2.Zero, ScorpioHoldout.StaticEffectsColor, 2f, 13, produceLight: false));
			GeneralParticleHandler.SpawnParticle(new GenericBloom(base.Projectile.Center, Vector2.Zero, Color.White, 1.5f, 12, produceLight: false));
			SoundEngine.PlaySound(in Scorpio.RocketHit, base.Projectile.Center);
			SoundEngine.PlaySound(in Scorpio.NukeHit, base.Projectile.Center);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.numHits < 1)
		{
			Main.player[base.Projectile.owner].SetScreenshake(6f);
		}
	}

	public float TrailWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return Utils.Remap(completionRatio, 0f, 0.8f, 15f, 0f);
	}

	public Color TrailColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(ScorpioHoldout.EffectsColor, ScorpioHoldout.EffectsColor * 0.3f, Utils.GetLerpValue(0f, 0.8f, completionRatio)) * Utils.GetLerpValue(255f, 0f, base.Projectile.alpha);
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
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Texture2D glowTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/ScorpioLargeRocket_Glow", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation + (float)Math.PI / 2f;
		Vector2 rotationPoint = frame.Size() * 0.5f;
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(TrailWidthFunction, TrailColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: false, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 25);
		Main.EntitySpriteDraw(value, drawPosition, frame, drawColor, drawRotation, rotationPoint, base.Projectile.scale, (SpriteEffects)0);
		Main.EntitySpriteDraw(glowTexture, drawPosition, frame, Color.White, drawRotation, rotationPoint, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
