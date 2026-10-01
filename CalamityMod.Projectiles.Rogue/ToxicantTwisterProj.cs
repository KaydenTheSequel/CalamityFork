using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ToxicantTwisterProj : ModProjectile, ILocalizedModType, IModType
{
	public float circleRange = -150f;

	public float circleSpeed = 1f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/ToxicantTwister";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 15;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 42;
		base.Projectile.height = 46;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 60 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		if (Owner.dead || !Owner.active)
		{
			base.Projectile.Kill();
			return;
		}
		if (base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.MaxUpdates = 3;
			if (base.Projectile.ai[1] == 0f)
			{
				base.Projectile.timeLeft = 600;
				circleRange *= 5f;
				circleSpeed *= 3f;
			}
			if (base.Projectile.timeLeft % 50 == 0)
			{
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity.RotatedByRandom(0.10000000149011612) * -0.6f, ModContent.ProjectileType<ToxicantTwisterDust>(), (int)((float)base.Projectile.damage * 0.4f), 0f, base.Projectile.owner, 0f, 0f, base.Projectile.ai[2]).timeLeft = 240;
			}
		}
		else if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.timeLeft = (int)((float)base.Projectile.timeLeft * Main.rand.NextFloat(1.05f, 1.2f));
		}
		if (Main.rand.NextBool(4) && base.Projectile.ai[1] > 7f)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(13f, 13f), Main.rand.NextBool(3) ? 215 : 75);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.9f, 1.3f) * Utils.GetLerpValue(255f, 0f, base.Projectile.alpha);
			dust.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.7f);
		}
		float targetDist = Vector2.Distance(Owner.Center, base.Projectile.Center);
		if (base.Projectile.ai[1] % 2f == 0f && base.Projectile.ai[1] > 7f && targetDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + base.Projectile.velocity * Main.rand.NextFloat(-2f, -1f), -base.Projectile.velocity * 0.3f, affectedByGravity: false, 6, 0.07f, Color.Lerp(Color.Green, Color.Chartreuse, 0.8f) * 0.65f, new Vector2(1f, 0.3f), quickShrink: true, glow: false));
		}
		base.Projectile.rotation += 0.5f / circleSpeed;
		if (base.Projectile.ai[1] > 20f)
		{
			Vector2 moveToMouse = (Owner.ClampedMouseWorld() + Utils.RotatedBy(new Vector2(0f, circleRange), (double)(base.Projectile.rotation * 0.2f), default(Vector2)).RotatedBy(MathHelper.ToRadians(90f) * base.Projectile.ai[2]) - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 18f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity += moveToMouse * circleSpeed;
			}
			else
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.9f;
			}
		}
		else
		{
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= 0.985f;
		}
		base.Projectile.ai[1]++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 5; i++)
		{
			Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 215 : 75, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.3f, 1.8f), 0, default(Color), Main.rand.NextFloat(1.3f, 1.8f)).noGravity = true;
		}
		for (int j = 0; j < 5; j++)
		{
			Vector2 dustVel = Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(4f, 8f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + dustVel, 278, dustVel * 0.7f);
			dust.scale = Main.rand.NextFloat(0.8f, 0.9f);
			dust.noGravity = false;
			dust.color = Color.Lerp(Color.White, Main.rand.NextBool(4) ? Color.Chartreuse : Color.Green, 0.7f);
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Chartreuse, "CalamityMod/Particles/HighResFoggyCircleHardEdge", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.07f, 8, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/RavagerRockPillarHit", 3);
		style.Volume = 0.4f;
		style.Pitch = Main.rand.NextFloat(-0.2f, 0.2f);
		style.MaxInstances = 4;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		for (int i = 0; i < 3; i++)
		{
			Color auraColor = Color.Lerp(Color.Chartreuse, Color.Lime, Utils.GetLerpValue(0f, 3f, i)) * 0.7f;
			Vector2 rotationalDrawOffset = ((float)Math.PI * 2f * (float)i / 7f + Main.GlobalTimeWrappedHourly * 27f).ToRotationVector2();
			rotationalDrawOffset *= MathHelper.Lerp(3f, 5.25f, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 15f) * 0.5f + 1f);
			Texture2D value = tex.Value;
			Vector2 position = base.Projectile.Center - Main.screenPosition + rotationalDrawOffset;
			Color val = auraColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value, position, null, val * Utils.GetLerpValue(255f, 0f, base.Projectile.alpha), base.Projectile.rotation, tex.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor * 0.3f, 2);
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 180);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (target.Calamity().unbreakableDR)
		{
			modifiers.SourceDamage *= MathHelper.Clamp((float)Math.Pow(0.8999999761581421, base.Projectile.numHits - 1), 0.1f, 1f);
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 55f, targetHitbox);
	}
}
