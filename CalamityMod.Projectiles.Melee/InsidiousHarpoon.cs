using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

[PierceResistException(false)]
public class InsidiousHarpoon : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public float fade;

	public int strongTimeMax;

	public int strongTimer;

	public bool isPowered;

	public bool canBePowered;

	public Vector2 storedVel;

	public NPC targetedNPC;

	public bool hasHitTarget;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<InsidiousImpaler>();

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 11;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 66;
		base.Projectile.height = 70;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10 * base.Projectile.MaxUpdates;
		base.Projectile.timeLeft = 600;
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0615: Unknown result type (might be due to invalid IL or missing references)
		//IL_0648: Unknown result type (might be due to invalid IL or missing references)
		//IL_064d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0666: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0591: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = storedVel.ToRotation() + MathHelper.ToRadians(45f);
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (time > 2)
		{
			if (!canBePowered)
			{
				canBePowered = true;
			}
			base.Projectile.alpha = 0;
			if (fade < 1f)
			{
				fade += 0.008f;
			}
		}
		if (canBePowered && !isPowered)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.975f;
			strongTimer++;
			if (strongTimer >= strongTimeMax)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ImpalerLaunch");
				style.Volume = 0.8f;
				style.Pitch = Main.rand.NextFloat(0.2f, 0.3f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				targetedNPC = base.Projectile.Center.ClosestNPCAt(1200f);
				if (targetedNPC != null)
				{
					storedVel = (base.Projectile.Center - targetedNPC.Center).SafeNormalize(Vector2.UnitX) * -30f;
				}
				base.Projectile.velocity = storedVel * 1.15f;
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center - storedVel * 3f, -storedVel * 0.1f, Color.Chartreuse * 0.7f, "CalamityMod/Particles/DustyCircleHardEdge", new Vector2(0.4f, 1f), storedVel.ToRotation(), 0f, 0.13f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center - storedVel * 3f, -storedVel * 0.2f, Color.Chartreuse * 0.7f, "CalamityMod/Particles/FlameExplosion", new Vector2(0.4f, 1f), storedVel.ToRotation(), 0f, 0.25f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				base.Projectile.extraUpdates = 3;
				strongTimer = 0;
				isPowered = true;
			}
		}
		if (isPowered)
		{
			if (targetDist < 1400f)
			{
				if (Main.rand.NextBool(7))
				{
					GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + Main.rand.NextVector2Circular(30f, 30f), -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.8f), affectedByGravity: false, 60, Main.rand.NextFloat(0.8f, 1.3f), Color.Chartreuse));
				}
				if (time % 10 == 0)
				{
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center - base.Projectile.velocity, base.Projectile.velocity * 0.1f, Color.Chartreuse, "CalamityMod/Particles/DustyCircleHardEdge", new Vector2(0.4f, 1.1f), base.Projectile.velocity.ToRotation(), 0f, 0.075f, 14, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				}
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + base.Projectile.velocity * Main.rand.NextFloat(-2f, -1f), -base.Projectile.velocity * 0.3f, affectedByGravity: false, 6, 0.07f, Color.Lerp(Color.Green, Color.Chartreuse, 0.8f) * 0.65f, new Vector2(1f, 0.3f), quickShrink: true, glow: false));
			}
			targetedNPC = (hasHitTarget ? null : base.Projectile.Center.ClosestNPCAt(600f));
			CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targetedNPC, ignoreTiles: true, 0.6f, 23f, 0.97f, 0.95f, accelerate: true);
		}
		else if (Main.rand.NextBool(3))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(13f, 13f), Main.rand.NextBool(7) ? 28 : 215);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.9f, 1.3f);
			dust.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.7f);
		}
		if (strongTimer == 0)
		{
			storedVel = base.Projectile.velocity;
		}
		if (Main.rand.NextBool(5))
		{
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(13f, 13f), 75);
			dust2.noGravity = true;
			dust2.scale = Main.rand.NextFloat(0.9f, 1.3f);
			dust2.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.7f);
		}
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.numHits < 1)
		{
			canBePowered = true;
			strongTimer = 1;
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, storedVel * 1.5f, affectedByGravity: false, 7, 0.057f, Color.Chartreuse, new Vector2(1.7f, 0.8f), quickShrink: true));
			for (int i = 0; i <= 15; i++)
			{
				Dust.NewDustPerfect(base.Projectile.Center, 75, storedVel.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.3f, 1.8f), 0, default(Color), Main.rand.NextFloat(1.3f, 1.8f)).noGravity = true;
			}
			if (!isPowered)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= -1f;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/NuclearTerrorHit");
				style.Volume = 0.8f;
				style.Pitch = 0.7f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				style = new SoundStyle("CalamityMod/Sounds/NPCHit/PerfMediumHit2");
				style.Volume = 0.65f;
				style.Pitch = -0.6f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
		}
		target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 180);
		modifiers.SourceDamage *= (isPowered ? 1.2f : 1f);
		if (targetedNPC != null && target == targetedNPC)
		{
			hasHitTarget = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		if (time < 7)
		{
			return false;
		}
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		if (isPowered || canBePowered)
		{
			for (int i = 0; i < 3; i++)
			{
				Color auraColor = Color.Lerp(Color.Chartreuse, Color.Lime, Utils.GetLerpValue(0f, 3f, i)) * 0.7f * fade;
				Vector2 rotationalDrawOffset = ((float)Math.PI * 2f * (float)i / 7f + Main.GlobalTimeWrappedHourly * 27f).ToRotationVector2();
				rotationalDrawOffset *= MathHelper.Lerp(3f, 5.25f, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 15f) * 0.5f + (isPowered ? 1.5f : (4.5f * Utils.GetLerpValue(0f, strongTimeMax, strongTimer))));
				Texture2D value = tex.Value;
				Vector2 position = base.Projectile.Center - Main.screenPosition + rotationalDrawOffset;
				Color color = auraColor;
				((Color)(ref color)).A = 0;
				Main.EntitySpriteDraw(value, position, null, color, storedVel.ToRotation() + MathHelper.ToRadians(45f), tex.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
			}
		}
		if (!isPowered)
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		}
		return true;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 40f, targetHitbox);
	}

	public InsidiousHarpoon()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		strongTimeMax = 60;
		storedVel = Vector2.Zero;
		base._002Ector();
	}
}
