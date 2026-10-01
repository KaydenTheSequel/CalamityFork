using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class StellarStrikerMeteor : ModProjectile, ILocalizedModType, IModType
{
	public Color mainColor;

	public int fallTime;

	public bool spawnMet;

	public int direction;

	public float wavePower;

	public NPC chosenTarget;

	public new string LocalizationCategory => "Projectiles.Melee";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 800;
		base.Projectile.penetrate = 1;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		float num = Vector2.Distance(Owner.Center, base.Projectile.Center);
		if (time % 5f == 0f && base.Projectile.extraUpdates < 12)
		{
			base.Projectile.extraUpdates++;
		}
		if (time == 0f)
		{
			chosenTarget = Owner.ClampedMouseWorld().ClosestNPCAt(700f);
			if (chosenTarget != null)
			{
				base.Projectile.velocity = (chosenTarget.Center - base.Projectile.Center + chosenTarget.velocity * 8f).SafeNormalize(Vector2.UnitX) * 3f;
			}
			else
			{
				base.Projectile.velocity = (Owner.Calamity().mouseWorld - base.Projectile.Center).SafeNormalize(Vector2.UnitX) * 3f;
			}
		}
		if (base.Projectile.numHits < 1)
		{
			if (chosenTarget == null || chosenTarget.life <= 0)
			{
				chosenTarget = Owner.ClampedMouseWorld().ClosestNPCAt(700f);
			}
			CalamityUtils.HomeInOnSelectedNPC(base.Projectile, chosenTarget, ignoreTiles: true, 0.09f, 5f, 0.99f, 0.95f, accelerate: true);
		}
		if (num < 1400f)
		{
			if (time % 11f == 0f)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + Main.rand.NextVector2Circular(20f, 20f), -base.Projectile.velocity * 2f, affectedByGravity: false, 11, 0.03f, mainColor * Main.rand.NextFloat(0.7f, 1f), new Vector2(1f, 1f), quickShrink: true, glow: false, 0.6f));
			}
			if (time % 4f == 0f)
			{
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 1.5f), Main.rand.NextBool(4) ? Color.PaleTurquoise : Color.Turquoise, 6, Main.rand.NextFloat(0.3f, 0.7f), 0.65f, 0f, glowing: true));
			}
		}
		if (time % 2f == 0f)
		{
			float sine = (float)Math.Sin((float)base.Projectile.timeLeft * 0.575f / (float)Math.PI);
			Vector2 offset = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * wavePower;
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset * (float)direction, 267, -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.3f));
			dust.scale = Main.rand.NextFloat(0.85f, 0.95f);
			dust.noGravity = true;
			dust.color = (Main.rand.NextBool(3) ? Color.PaleTurquoise : Color.Turquoise);
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		time++;
		if (time == 20f && spawnMet && base.Projectile.ai[2] > 0f)
		{
			spawnMet = false;
			Vector2 spawnSpot = Owner.Center + new Vector2(Main.rand.NextFloat(-550f, 550f), Main.rand.NextFloat(-750f, -950f));
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnSpot, Vector2.Zero, ModContent.ProjectileType<StellarStrikerMeteor>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 0f, base.Projectile.ai[2] - 1f);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], Color.White, 1, tex);
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.2f;
		int hitsToMinMult = 6;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
		target.AddBuff(ModContent.BuffType<Nightwither>(), 120);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		SoundEngine.PlaySound(in SoundID.Item89, base.Projectile.position);
		if (base.Projectile.ai[2] > 0f && spawnMet)
		{
			Vector2 spawnSpot = Owner.Center + new Vector2(Main.rand.NextFloat(-550f, 550f), Main.rand.NextFloat(-750f, -950f));
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnSpot, Vector2.Zero, ModContent.ProjectileType<StellarStrikerMeteor>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 0f, base.Projectile.ai[2] - 1f);
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.3f);
			base.Projectile.ExpandHitboxBy((int)(285f * base.Projectile.scale));
			base.Projectile.penetrate = -1;
			base.Projectile.Damage();
		}
		for (int i = 0; i < 6; i++)
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, Vector2.One.RotatedByRandom(100.0) * Main.rand.NextFloat(3.5f, 14f), affectedByGravity: false, 20, Main.rand.NextFloat(0.3f, 0.8f), Main.rand.NextBool(5) ? Color.PaleTurquoise : Color.Turquoise));
		}
		for (int j = 0; j < 18; j++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267);
			dust.velocity = ((float)Math.PI * 2f * (float)j / 18f).ToRotationVector2() * 10f + Owner.velocity * 0.5f;
			dust.scale = Main.rand.NextFloat(0.8f, 0.9f);
			dust.noGravity = true;
			dust.color = (Main.rand.NextBool(3) ? Color.PaleTurquoise : Color.Turquoise);
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, mainColor * 0.7f, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 2f, 1f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White * 0.7f, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 1f, 0.3f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
	}

	public override bool? CanCutTiles()
	{
		return false;
	}

	public StellarStrikerMeteor()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		mainColor = Color.Turquoise;
		fallTime = 60;
		spawnMet = true;
		direction = 1;
		wavePower = 7f;
		base._002Ector();
	}
}
