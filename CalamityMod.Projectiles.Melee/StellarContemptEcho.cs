using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class StellarContemptEcho : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle SlamHamSound = new SoundStyle("CalamityMod/Sounds/Item/StellarContemptImpact")
	{
		Volume = 0.6f
	};

	public static readonly SoundStyle Kunk = new SoundStyle("CalamityMod/Sounds/Item/TF2PanHit")
	{
		Volume = 1.1f
	};

	public float rotatehammer = 35f;

	public int ColorAlpha = 225;

	public float speed;

	public NPC targeted;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 15;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 74;
		base.Projectile.height = 76;
		base.Projectile.aiStyle = 0;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		rotatehammer--;
		base.Projectile.rotation += MathHelper.ToRadians(rotatehammer) * (float)base.Projectile.direction;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] < 42f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.95f;
			base.Projectile.rotation += MathHelper.ToRadians(base.Projectile.ai[0] * 0.5f) * base.Projectile.localAI[0];
		}
		else if (base.Projectile.ai[0] >= 42f)
		{
			base.Projectile.extraUpdates = 5;
			if (base.Projectile.ai[1] != -5f)
			{
				targeted = Main.npc[(int)base.Projectile.ai[1]];
			}
			if (targeted == null || !targeted.CanBeChasedBy(base.Projectile) || !targeted.active)
			{
				base.Projectile.ai[1] = -5f;
				targeted = base.Projectile.Center.ClosestNPCAt(2000f);
			}
			if (targeted != null)
			{
				CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targeted, ignoreTiles: true, 0.65f, 25f, 0.98f);
			}
			else
			{
				base.Projectile.Kill();
			}
			if (Main.rand.NextBool(6))
			{
				Vector2 offset = Utils.RotatedByRandom(new Vector2(7f, 0f), MathHelper.ToRadians(360f));
				Vector2 velOffset = Utils.RotatedBy(new Vector2(3f, 0f), (double)offset.ToRotation(), default(Vector2));
				Dust dust = Dust.NewDustPerfect(new Vector2(base.Projectile.Center.X, base.Projectile.Center.Y) + offset, ModContent.DustType<LightDust>(), (Vector2?)new Vector2(base.Projectile.velocity.X * 0.5f + velOffset.X, base.Projectile.velocity.Y * 0.5f + velOffset.Y), 0, default(Color), 1f);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.5f, 1.9f);
				dust.color = (Main.rand.NextBool(3) ? Color.PaleTurquoise : Color.Turquoise);
			}
		}
		if (base.Projectile.ai[0] == 42f && targeted != null)
		{
			for (int i = 0; i < 20; i++)
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 278, ((targeted.Center - base.Projectile.Center).SafeNormalize(Vector2.UnitX) * 25f).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.2f, 1f));
				dust2.noGravity = true;
				dust2.scale = Main.rand.NextFloat(0.8f, 1.9f);
				dust2.color = (Main.rand.NextBool(3) ? Color.PaleTurquoise : Color.Turquoise);
			}
		}
		for (int j = 0; j < 2; j++)
		{
			Vector2 offset2 = Utils.RotatedBy(new Vector2(0f, -30f), -0.39269909262657166, default(Vector2)).RotatedBy(base.Projectile.rotation);
			Vector2 velOffset2 = Utils.RotatedBy(new Vector2(0f, -5f), -0.7853981852531433, default(Vector2)).RotatedBy(base.Projectile.rotation) * Main.rand.NextFloat(0.5f, 1f);
			Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center + offset2 + Main.rand.NextVector2Circular(6f, 6f), Main.rand.NextBool(3) ? 278 : ModContent.DustType<LightDust>(), velOffset2);
			dust3.noGravity = true;
			dust3.scale = Main.rand.NextFloat(0.9f, 1.3f);
			dust3.color = (Main.rand.NextBool(3) ? Color.PaleTurquoise : Color.Turquoise);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (targeted != null && target != targeted)
		{
			modifiers.SourceDamage *= 0.3f;
		}
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.ai[0] <= 42f)
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Nightwither>(), 180);
		if (target == targeted)
		{
			base.Projectile.Kill();
		}
	}

	public override bool PreKill(int timeLeft)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		Main.player[base.Projectile.owner].SetScreenshake(7f);
		if (Main.zenithWorld)
		{
			SoundEngine.PlaySound(in Kunk, base.Projectile.Center);
		}
		else
		{
			SoundEngine.PlaySound(in SlamHamSound, base.Projectile.Center);
		}
		float numberOfDusts = 156f;
		float rotFactor = 360f / numberOfDusts;
		for (int i = 0; (float)i < numberOfDusts; i++)
		{
			float rot = MathHelper.ToRadians((float)i * rotFactor);
			float intensity = Main.rand.NextFloat(0.2f, 1f);
			Vector2 offset = Utils.RotatedBy(new Vector2(30f, 5.8f), (double)rot, default(Vector2));
			Vector2 velOffset = Utils.RotatedBy(new Vector2(40.8f, 10.5f), (double)rot, default(Vector2));
			if (i % 2 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + offset, velOffset * intensity * 0.7f, "CalamityMod/Particles/Sparkle", affectedByGravity: false, (int)(40f * intensity), intensity, Main.rand.NextBool(3) ? Color.PaleTurquoise : Color.Turquoise, new Vector2(1f, 2f), useAddativeBlend: true, glowCenter: true));
				continue;
			}
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + offset, ModContent.DustType<LightDust>(), velOffset);
			dust.noGravity = true;
			dust.velocity = velOffset * intensity;
			dust.scale = Main.rand.NextFloat(2.1f, 2.8f) * intensity;
			dust.color = (Main.rand.NextBool(3) ? Color.PaleTurquoise : Color.Turquoise);
		}
		for (int j = 0; j < 40; j++)
		{
			int dustID = ModContent.DustType<LightDust>();
			float intensity2 = Main.rand.NextFloat(0.5f, 1.5f);
			Vector2 dustVel = Utils.RotatedBy(new Vector2(45f, 0f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
			dustVel = dustVel.RotatedByRandom((1.5f - intensity2) * 0.4f) * intensity2;
			float scale = Main.rand.NextFloat(2.8f, 3.5f) - intensity2;
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, dustID, dustVel, 0, default(Color), scale);
			dust2.noGravity = true;
			dust2.position = base.Projectile.Center;
			dust2.color = (Main.rand.NextBool(3) ? Color.PaleTurquoise : Color.Turquoise);
		}
		GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + base.Projectile.velocity, base.Projectile.velocity * 2f, affectedByGravity: false, 12, 0.17f, Color.Turquoise, new Vector2(2.5f, 0.7f), quickShrink: true, glow: true, 0.85f));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Turquoise, "CalamityMod/Particles/BloomRing", new Vector2(0.7f, 1f), base.Projectile.velocity.ToRotation(), 0f, 3f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.PaleTurquoise, "CalamityMod/Particles/BloomRing", new Vector2(0.4f, 1f), base.Projectile.velocity.ToRotation(), 0f, 4f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity * 0f, ModContent.ProjectileType<StellarContemptBlast>(), base.Projectile.damage / 2, base.Projectile.knockBack, base.Projectile.owner);
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Projectile projectile = base.Projectile;
		int mode = ProjectileID.Sets.TrailingMode[base.Type];
		Color turquoise = Color.Turquoise;
		((Color)(ref turquoise)).A = 0;
		CalamityUtils.DrawAfterimagesCentered(projectile, mode, turquoise * 0.5f, 1, texture, drawCentered: true, shrink: true);
		Projectile projectile2 = base.Projectile;
		turquoise = Color.Turquoise;
		((Color)(ref turquoise)).A = 0;
		projectile2.DrawProjectileWithBackglow(turquoise, Color.White, 12f * Utils.GetLerpValue(0f, 42f, base.Projectile.ai[0], clamped: true), texture, null, (SpriteEffects)0);
		return true;
	}
}
