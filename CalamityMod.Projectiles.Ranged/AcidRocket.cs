using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

[PierceResistException(false)]
public class AcidRocket : ModProjectile, ILocalizedModType, IModType
{
	public NPC chosenTarget;

	public bool stuckInTarget;

	public bool stuckInGround;

	public int stuckTimer;

	public bool canDamage;

	public bool canStick;

	public Vector2 vibrate;

	public Vector2 placementCenter;

	private float placementDistance;

	public Vector2 storedVelocity;

	private Vector2 placementVelocity;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public ref float RocketID => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 36;
		base.Projectile.height = 36;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.timeLeft = 600;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 11;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_032a: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		float power = 3f;
		vibrate = Main.rand.NextVector2Circular(power, power);
		if (!stuckInTarget)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 720f, 16f, (float)base.Projectile.MaxUpdates * 20f);
			if (!Main.dedServ && base.Projectile.FinalExtraUpdate() && ((Vector2)(ref base.Projectile.velocity)).Length() > 3f)
			{
				Color color = default(Color);
				((Color)(ref color))._002Ector(136, 211, 113, 127);
				Color fadeColor = default(Color);
				((Color)(ref fadeColor))._002Ector(165, 165, 86);
				Vector2 position = base.Projectile.Center + Main.rand.NextVector2Circular(8f, 8f);
				Vector2 gasVelocity = base.Projectile.velocity * 1.2f + base.Projectile.velocity.RotatedBy(0.75) * 0.3f;
				gasVelocity *= Main.rand.NextFloat(0.24f, 0.6f);
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(position, gasVelocity, color, fadeColor, Main.rand.NextFloat(0.5f, 1f), 205 - Main.rand.Next(50), 0.02f));
				for (int i = 0; i < 2; i++)
				{
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 10f, -base.Projectile.velocity, affectedByGravity: false, 8, 0.13f * ((i == 0) ? 0.2f : 0.5f), Color.Lerp(Color.SeaGreen, Color.PaleGreen, 0.25f) * 0.5f, new Vector2(0.3f, 1f), quickShrink: false, glow: false, 0.8f));
				}
				for (int j = 0; j < 2; j++)
				{
					Color bubbleColor = (Main.rand.NextBool() ? Color.SeaGreen : Color.YellowGreen);
					Vector2 position2 = base.Projectile.Center + Main.rand.NextVector2Circular(50f, 50f);
					Vector2 bubbleVelocity = -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.8f);
					GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(position2, bubbleVelocity, bubbleColor, new Vector2(0.8f, 1f), 0f, 0.1f, 0f, 75));
				}
			}
		}
		else if (stuckInTarget)
		{
			placementCenter = chosenTarget.Center + placementVelocity * placementDistance + storedVelocity;
			base.Projectile.Center = placementCenter;
			base.Projectile.rotation = storedVelocity.SafeNormalize(Vector2.UnitX).ToRotation() + (float)Math.PI / 2f;
			stuckTimer--;
			if (chosenTarget.life <= 0 || chosenTarget == null)
			{
				stuckTimer = 0;
			}
			if (stuckTimer == 0)
			{
				base.Projectile.Kill();
			}
			if (stuckTimer == 40)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/PlagueSounds/PBGAttackSwitchShort");
				style.Volume = 0.4f;
				style.PitchVariance = 0.2f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
		}
		base.Projectile.Opacity = 1f;
	}

	public override bool? CanDamage()
	{
		if (!canDamage)
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		if (target == chosenTarget)
		{
			target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 180);
		}
		if (!stuckInTarget && canStick)
		{
			canDamage = false;
			base.Projectile.rotation = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).ToRotation() + (float)Math.PI / 2f;
			placementDistance = 0f - Vector2.Distance(target.Center, base.Projectile.Center);
			placementVelocity = (target.Center - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
			placementCenter = placementVelocity * (placementDistance * 0.01f);
			chosenTarget = target;
			stuckInTarget = true;
			storedVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 8f;
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.netUpdate = true;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 180);
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		float rotation = base.Projectile.velocity.ToRotation();
		GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, base.Projectile.velocity / 2f, Color.Gray, new Vector2(1f, 3f), rotation, 0.08f, 0.2f, 15));
	}

	public override void OnKill(int timeLeft)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Custom/PlagueSounds/PlagueBoom", 4);
		CalamityUtils.RocketBehaviorInfo rocketBehaviorInfo = new CalamityUtils.RocketBehaviorInfo((int)RocketID)
		{
			clusterProjectileID = 0,
			destructiveClusterProjectileID = 0
		};
		bool isClusterRocket = RocketID == 4445f || RocketID == 4446f;
		SoundEngine.PlaySound(soundStyle with
		{
			Volume = 0.9f,
			Pitch = -0.3f
		}, base.Projectile.Center);
		SoundEngine.PlaySound(in SoundID.Item107, base.Projectile.Center);
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<SulphuricAcidCannonExplosion>(), (int)((float)base.Projectile.damage * (isClusterRocket ? 1.5f : 2f)), base.Projectile.knockBack, base.Projectile.owner);
			if (isClusterRocket)
			{
				for (int k = 0; k < 3; k++)
				{
					Vector2 acidVelocity = (Vector2.UnitY * (-12f + Main.rand.NextFloat(-3f, 4f))).RotatedByRandom(MathHelper.ToRadians(40f));
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity + acidVelocity, ModContent.ProjectileType<SulphuricDrop>(), (int)((float)base.Projectile.damage * 0.15f), base.Projectile.knockBack, base.Projectile.owner, RocketID);
				}
			}
		}
		for (int i = 0; i < 35; i++)
		{
			Vector2 smokeVel = Main.rand.NextVector2Unit() * Main.rand.NextVector2Circular(32f, 32f);
			Color smokeColor = (Main.rand.NextBool() ? Color.SeaGreen : Color.PaleGreen);
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, smokeVel, smokeColor, Color.Black, Main.rand.NextFloat(1.4f, 4f), 200 - Main.rand.Next(60), 0.08f));
		}
		for (int j = 0; j < 8; j++)
		{
			Vector2 bubbleVel = Main.rand.NextVector2Circular(26f, 26f);
			Color bubbleColor = (Main.rand.NextBool() ? Color.OliveDrab : Color.SeaGreen);
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, bubbleVel, bubbleColor, new Vector2(0.8f, 1f), 0f, 0.21f, 0f, 50));
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.SeaGreen, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.2f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
		{
			((Rectangle)(ref targetHitbox)).Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
		}
		return null;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		float glowOutwardness = MathHelper.SmoothStep(3f, 0f, Utils.GetLerpValue(90f, 270f, stuckTimer, clamped: true));
		Texture2D Texture = TextureAssets.Projectile[base.Projectile.type].Value;
		Rectangle frame = Texture.Frame(1, Main.projFrames[base.Projectile.type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Color glowColor = Color.Lerp(Color.YellowGreen, Color.Lime, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 5f) * 0.5f + 0.5f);
		((Color)(ref glowColor)).A = 0;
		Vector2 drawPosition;
		if (stuckInTarget)
		{
			for (int i = 0; i < 8; i++)
			{
				drawPosition = base.Projectile.Center + ((float)Math.PI * 2f * (float)i / 8f + Main.GlobalTimeWrappedHourly * 4f).ToRotationVector2() * glowOutwardness - Main.screenPosition;
				Main.EntitySpriteDraw(Texture, drawPosition, frame, base.Projectile.GetAlpha(glowColor), base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
			}
		}
		drawPosition = base.Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(Texture, drawPosition + vibrate, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public AcidRocket()
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		stuckTimer = 200;
		canDamage = true;
		canStick = true;
		vibrate = Vector2.Zero;
		base._002Ector();
	}
}
