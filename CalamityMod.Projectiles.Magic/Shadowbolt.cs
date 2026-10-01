using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class Shadowbolt : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public float platFade;

	public float platRot;

	public bool spawnPlat;

	public bool hasSetPlatSpawn;

	public bool hasReboundOffPlat;

	public bool reflecting;

	public int reflectionTimer = 50;

	public Vector2 platPosCenter;

	public Vector2 platPosWall;

	public NPC chosenTarget;

	public Vector2 targetCenter;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 10;
		base.Projectile.timeLeft = 2400;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		float targetDist = Vector2.Distance(Owner.Center, base.Projectile.Center);
		if (!spawnPlat && !hasReboundOffPlat && targetCenter != Vector2.Zero)
		{
			platFade = MathHelper.Lerp(platFade, 1f, 0.008f);
		}
		if (hasReboundOffPlat)
		{
			platFade -= 0.0007f;
		}
		BeamMainVisuals(Owner, targetDist);
		if (reflecting)
		{
			if (reflectionTimer > 15)
			{
				platPosCenter += (targetCenter - platPosCenter).SafeNormalize(Vector2.UnitX) * -2.7f * Utils.GetLerpValue(15f, 40f, reflectionTimer, clamped: true);
			}
			else
			{
				platPosCenter += (targetCenter - platPosCenter).SafeNormalize(Vector2.UnitX) * 9f * Utils.GetLerpValue(10f, 0f, reflectionTimer, clamped: true);
			}
			base.Projectile.extraUpdates = 0;
			base.Projectile.velocity = Vector2.Zero;
			base.Projectile.Center = platPosWall;
			reflectionTimer--;
			if (reflectionTimer <= 0)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ShadowboltReflect");
				style.Volume = 0.6f;
				style.Pitch = Main.rand.NextFloat(-0.1f, 0.1f);
				style.MaxInstances = -1;
				SoundEngine.PlaySound(in style, platPosWall);
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					base.Projectile.localNPCImmunity[i] = 0;
				}
				base.Projectile.velocity = (targetCenter - platPosCenter).SafeNormalize(Vector2.UnitX) * 12f;
				reflecting = false;
				hasReboundOffPlat = true;
				time = 10;
				base.Projectile.extraUpdates = 100;
			}
		}
		if (spawnPlat && hasSetPlatSpawn)
		{
			chosenTarget = base.Projectile.Center.ClosestNPCAt(2000f);
			if (chosenTarget == null)
			{
				targetCenter = Owner.Calamity().mouseWorld;
			}
			else
			{
				targetCenter = chosenTarget.Center;
			}
			platPosCenter = base.Projectile.Center + base.Projectile.velocity * (float)Main.rand.Next(70, 121);
			platRot = (base.Projectile.Center - platPosCenter).SafeNormalize(Vector2.UnitX).ToRotation();
			platPosWall = platPosCenter + Utils.RotatedBy(new Vector2(0f, 8f), (double)platRot, default(Vector2));
			spawnPlat = false;
		}
		if (!spawnPlat && reflecting && targetCenter != Vector2.Zero)
		{
			if (chosenTarget == null)
			{
				targetCenter = Owner.Calamity().mouseWorld;
			}
			else
			{
				targetCenter = chosenTarget.Center;
			}
			platRot = platRot.AngleLerp((targetCenter - platPosCenter).SafeNormalize(Vector2.UnitX).ToRotation(), 0.1f);
			platPosWall = platPosCenter + Utils.RotatedBy(new Vector2(0f, -30f), (double)(platRot + MathHelper.ToRadians(90f)), default(Vector2));
		}
		if (!spawnPlat && !hasReboundOffPlat && targetCenter != Vector2.Zero && Vector2.Distance(platPosWall, base.Projectile.Center) <= 25f && !reflecting)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(platPosWall, Vector2.Zero, Color.Purple, "CalamityMod/Particles/SmallBloomRing", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.1f, 1.55f, 20, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ShadowboltWallHit");
			style.Volume = 0.5f;
			style.Pitch = 0f;
			style.MaxInstances = -1;
			SoundEngine.PlaySound(in style, platPosWall);
			reflecting = true;
			base.Projectile.extraUpdates = 0;
		}
		if (!hasReboundOffPlat && base.Projectile.numHits == 0 && !hasSetPlatSpawn && time > 70)
		{
			hasSetPlatSpawn = true;
			spawnPlat = true;
		}
		time++;
	}

	private void BeamMainVisuals(Player Owner, float targetDist)
	{
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		if (reflecting)
		{
			for (int i = 0; i < 2; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267);
				dust.velocity = Utils.RotatedByRandom(new Vector2(3f, 3f), 100.0) * Main.rand.NextFloat(0.2f, 0.8f);
				dust.scale = Main.rand.NextFloat(0.35f, 0.8f);
				dust.noGravity = true;
				dust.color = Color.Lerp(Color.White, Main.rand.NextBool(4) ? Color.Indigo : Color.Purple, 0.7f);
			}
			return;
		}
		if (time == 14)
		{
			SoundStyle style = SoundID.Item72 with
			{
				Volume = 0.8f,
				Pitch = Main.rand.NextFloat(-0.1f, 0.1f),
				MaxInstances = -1
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.velocity = base.Projectile.velocity.RotatedByRandom(0.15000000596046448);
			for (int j = 0; j < 2; j++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Indigo, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.3f, 0.75f, 12, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.2f, 0.55f, 12, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			for (int k = 0; k < 6; k++)
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 278);
				dust2.velocity = base.Projectile.velocity.RotatedByRandom(0.25) * Main.rand.NextFloat(0.6f, 2f);
				dust2.scale = Main.rand.NextFloat(0.65f, 0.9f);
				dust2.noGravity = true;
				dust2.color = Color.Lerp(Color.White, Main.rand.NextBool(4) ? Color.Indigo : Color.Purple, 0.7f);
			}
		}
		if (targetDist < 1400f)
		{
			if (time > 30 && Main.rand.NextBool(15))
			{
				GeneralParticleHandler.SpawnParticle(new LineParticle(base.Projectile.Center, base.Projectile.velocity * Main.rand.NextFloat(-1f, 1f), affectedByGravity: false, 50, 1.9f, Color.Lerp(Color.Indigo, Color.Purple, Main.rand.NextFloat(0f, 1f))));
			}
			if (time > 22 && time % 3 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 10f, -base.Projectile.velocity * 0.1f, affectedByGravity: false, 18, 0.05f * (hasReboundOffPlat ? 1.1f : 0.5f), Color.Lerp(Color.Indigo, Color.Orchid, 0.25f), new Vector2(0.7f, 2f), quickShrink: true, glow: false, 0.3f));
			}
			if (time > 30 && Main.rand.NextBool(12))
			{
				Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center, 267);
				dust3.velocity = base.Projectile.velocity * Main.rand.NextFloat(-2f, 2f);
				dust3.scale = Main.rand.NextFloat(0.95f, 1.4f);
				dust3.noGravity = true;
				dust3.color = Color.Lerp(Color.White, Main.rand.NextBool(4) ? Color.Indigo : Color.Purple, 0.7f);
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.25f;
		int hitsToMinMult = 7;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= (hasReboundOffPlat ? 2.5f : 0.8f) * damageMult;
		if (!hasReboundOffPlat && base.Projectile.numHits == 0 && !hasSetPlatSpawn)
		{
			spawnPlat = true;
			hasSetPlatSpawn = true;
		}
	}

	public override bool? CanDamage()
	{
		if (!reflecting)
		{
			return null;
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		Color val;
		if (targetCenter != Vector2.Zero && platFade > 0f)
		{
			Texture2D pTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/ShadowPlatform", (AssetRequestMode)2).Value;
			for (int i = 0; i < 5; i++)
			{
				val = Color.Lerp(Color.Indigo, Color.Purple, Utils.GetLerpValue(0f, 5f, i));
				((Color)(ref val)).A = 0;
				Color auraColor = val * 0.55f * platFade;
				Vector2 rotationalDrawOffset = ((float)Math.PI * 2f * (float)i / 7f + Main.GlobalTimeWrappedHourly * 30f).ToRotationVector2();
				rotationalDrawOffset *= MathHelper.Lerp(3f, 5.25f, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 6f) * 0.5f + 0.5f);
				Main.EntitySpriteDraw(pTexture, platPosCenter - Main.screenPosition + rotationalDrawOffset, null, auraColor, platRot + MathHelper.ToRadians(90f), pTexture.Size() * 0.5f, new Vector2(1f, 0.7f), (SpriteEffects)0);
			}
			for (int j = 0; j < 4; j++)
			{
				Vector2 position = platPosCenter - Main.screenPosition;
				val = Color.Purple;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(pTexture, position, null, val * platFade, platRot + MathHelper.ToRadians(90f), pTexture.Size() * 0.5f, new Vector2(1f, 0.7f), (SpriteEffects)0);
			}
			Vector2 position2 = platPosCenter - Main.screenPosition;
			val = Color.White;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(pTexture, position2, null, val * platFade * 0.6f, platRot + MathHelper.ToRadians(90f), pTexture.Size() * 0.5f, new Vector2(1f, 0.7f) * 0.93f, (SpriteEffects)0);
		}
		if (!reflecting && time >= 22 && !hasReboundOffPlat)
		{
			Asset<Texture2D> tex = ModContent.Request<Texture2D>("CalamityMod/Particles/GlowSpark", (AssetRequestMode)2);
			for (int k = 0; k < 5; k++)
			{
				Texture2D value = tex.Value;
				Vector2 position3 = base.Projectile.Center - Main.screenPosition - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 15f;
				val = Color.White;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(value, position3, null, val * 0.7f, base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f), tex.Size() * 0.5f, new Vector2(0.4f, 1f) * ((float)k * 0.3f) * 0.05f * (hasReboundOffPlat ? 1.1f : 0.5f), (SpriteEffects)0);
			}
		}
		if (reflecting)
		{
			Texture2D rechargeTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
			float randSize = Main.rand.NextFloat(0.8f, 1.2f);
			Vector2 position4 = platPosWall - Main.screenPosition;
			val = Color.Purple;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(rechargeTexture, position4, null, val, base.Projectile.rotation, rechargeTexture.Size() * 0.5f, 0.65f * Utils.GetLerpValue(-25f, 20f, reflectionTimer, clamped: true) * randSize, (SpriteEffects)0);
			Vector2 position5 = platPosWall - Main.screenPosition;
			val = Color.White;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(rechargeTexture, position5, null, val, base.Projectile.rotation, rechargeTexture.Size() * 0.5f, 0.45f * Utils.GetLerpValue(-25f, 20f, reflectionTimer, clamped: true) * randSize, (SpriteEffects)0);
		}
		return false;
	}
}
