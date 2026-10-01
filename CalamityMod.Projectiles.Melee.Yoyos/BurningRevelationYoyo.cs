using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Yoyos;

public class BurningRevelationYoyo : ModProjectile
{
	public const int MaxUpdates = 3;

	public bool canDamage = true;

	public bool firing;

	public NPC targeted;

	public float fade;

	public int hitCooldown;

	public int timer;

	public int yoyoPower;

	public int yoyoPowerMax = 1000;

	public bool cloneYoyo;

	public bool setCloneDamage;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<BurningRevelation>();

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.YoyosLifeTimeMultiplier[base.Type] = -1f;
		ProjectileID.Sets.YoyosMaximumRange[base.Type] = BurningRevelation.Reach;
		ProjectileID.Sets.YoyosTopSpeed[base.Type] = BurningRevelation.Speed / 3f;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.aiStyle = 99;
		base.Projectile.width = 30;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.penetrate = -1;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 54;
	}

	public override void AI()
	{
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		if (!cloneYoyo)
		{
			int MainYoyo = -1;
			for (int x = 0; x < Main.maxProjectiles; x++)
			{
				Projectile proj = Main.projectile[x];
				if (proj.active && proj.type == base.Type && proj.owner == base.Projectile.owner)
				{
					MainYoyo = x;
					break;
				}
			}
			if (base.Projectile.whoAmI != MainYoyo)
			{
				cloneYoyo = true;
			}
		}
		if (!setCloneDamage && cloneYoyo)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 1.1f);
			setCloneDamage = true;
		}
		_ = Main.player[base.Projectile.owner];
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.Gold;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.9f);
		fade = MathHelper.Lerp(fade, (float)(firing ? 1 : 0), 0.03f);
		Vector2 center2;
		if (firing)
		{
			if (targeted == null || targeted.life <= 0)
			{
				targeted = base.Projectile.Center.ClosestNPCAt(800f);
			}
			if (timer % (cloneYoyo ? 20 : 10) == 0)
			{
				int stardamage = (int)((float)base.Projectile.damage * 0.24f);
				Vector2 spinningpoint = new Vector2(0f, 10f);
				double radians = (float)timer * 0.025f * (float)((!cloneYoyo) ? 1 : (-1));
				center2 = default(Vector2);
				Vector2 vel = Utils.RotatedBy(spinningpoint, radians, center2);
				Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, vel, ModContent.ProjectileType<HolyStarDamage>(), stardamage, base.Projectile.knockBack, base.Projectile.owner, 0f, 5f, (targeted == null) ? (-1) : targeted.whoAmI);
				projectile.extraUpdates = 1;
				projectile.scale = 0.5f;
				Projectile projectile2 = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, -vel, ModContent.ProjectileType<HolyStarDamage>(), stardamage, base.Projectile.knockBack, base.Projectile.owner, 0f, 5f, (targeted == null) ? (-1) : targeted.whoAmI);
				projectile2.extraUpdates = 1;
				projectile2.scale = 0.5f;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceHolyBlastShoot");
				style.Volume = 0.6f;
				style.PitchVariance = 0.2f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			if (timer > 0)
			{
				timer--;
				if (timer <= 0)
				{
					firing = false;
					canDamage = true;
					yoyoPower = 0;
				}
			}
		}
		else
		{
			if (yoyoPower >= yoyoPowerMax)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/ProfanedGuardians/GuardianRay");
				style.Volume = 0.9f;
				style.Pitch = 0.3f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				canDamage = false;
				firing = true;
				timer = 240;
			}
			if (Main.rand.NextBool(7))
			{
				Vector2 center3 = base.Projectile.Center;
				int type = ModContent.DustType<LightDust>();
				Vector2? velocity = (Utils.RotatedByRandom(new Vector2(4f, 4f), 100.0) + base.Projectile.velocity) * Main.rand.NextFloat(0.2f, 1f);
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(center3, type, velocity, 0, newColor);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.85f, 1.15f);
				dust.color = (Main.rand.NextBool(5) ? Color.Khaki : Color.Goldenrod);
				dust.noLightEmittence = true;
			}
			yoyoPower++;
		}
		if (hitCooldown > 0)
		{
			hitCooldown--;
		}
		center2 = base.Projectile.Center - Main.player[base.Projectile.owner].Center;
		if (((Vector2)(ref center2)).Length() > 3200f && !firing)
		{
			base.Projectile.Kill();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		if (hitCooldown == 0)
		{
			hitCooldown = base.Projectile.localNPCHitCooldown;
			targeted = target;
			yoyoPower += 15;
			if (base.Projectile.owner == Main.myPlayer)
			{
				float power = Utils.GetLerpValue(-100f, yoyoPowerMax, yoyoPower, clamped: true);
				int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BurningHolyBlast>(), (int)((double)base.Projectile.damage * 0.5), base.Projectile.knockBack, base.Projectile.owner, power);
				if (proj.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[proj].DamageType = DamageClass.MeleeNoSpeed;
				}
				for (int i = 0; i < (int)(30f * power); i++)
				{
					if (Main.rand.NextBool())
					{
						GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, (new Vector2(19f, 19f) * power).RotatedByRandom(100.0) * Main.rand.NextFloat(0.2f, 1f), "CalamityMod/Particles/ProvidenceMarkParticle", affectedByGravity: false, 27, Main.rand.NextFloat(1.15f, 1.3f), Main.rand.NextBool(4) ? Color.Khaki : Color.Orange, new Vector2(1.3f, 0.5f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, Main.rand.NextFloat(0.1f, 0.2f)));
						continue;
					}
					bool isSpark = Main.rand.NextBool(5);
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, isSpark ? 278 : ModContent.DustType<LightDust>(), (new Vector2(15f, 15f) * power).RotatedByRandom(100.0) * Main.rand.NextFloat(0.2f, 1f));
					dust.noGravity = true;
					dust.scale = Main.rand.NextFloat(1.85f, 2.15f) * power * (isSpark ? 0.5f : 1f);
					dust.color = (Main.rand.NextBool(5) ? Color.Khaki : Color.Goldenrod);
					if (isSpark)
					{
						dust.noGravity = false;
					}
					else
					{
						dust.noLightEmittence = true;
					}
				}
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Goldenrod, "CalamityMod/Particles/SoftRoundExplosion", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 0.14f * power, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Khaki, "CalamityMod/Particles/BloomRing", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0f, 2.1f * power, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceHolyBlastImpact");
				style.Volume = 0.5f;
				style.Pitch = 0.3f * power;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				style = new SoundStyle("CalamityMod/Sounds/Item/HeliumFlashReady");
				style.Volume = 0.7f;
				style.Pitch = 0.6f * power;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
		}
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 480);
	}

	public override bool? CanDamage()
	{
		if (!canDamage)
		{
			return false;
		}
		return null;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		Texture2D bloomTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Texture2D shineTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/HalfStar", (AssetRequestMode)2).Value;
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition;
		float power = Utils.GetLerpValue(-100f, yoyoPowerMax, yoyoPower, clamped: true);
		float randSize = Main.rand.NextFloat(0.9f, 1.1f);
		Color val = Color.Goldenrod;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(bloomTexture, drawPos, null, val, base.Projectile.rotation, bloomTexture.Size() * 0.5f, 0.65f * randSize * (1f - fade) * power, (SpriteEffects)0);
		val = Color.White;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(bloomTexture, drawPos, null, val * 0.65f, base.Projectile.rotation, bloomTexture.Size() * 0.5f, 0.45f * randSize * (1f - fade) * power, (SpriteEffects)0);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Projectile projectile = base.Projectile;
		val = Color.Goldenrod;
		((Color)(ref val)).A = 0;
		projectile.DrawProjectileWithBackglow(val * fade, lightColor, 4f * fade, texture, null, (SpriteEffects)0);
		val = Color.Goldenrod;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(shineTexture, drawPos, null, val, 0f, shineTexture.Size() * 0.5f, new Vector2(0.4f, 1f) * 4.25f * randSize * fade, (SpriteEffects)0);
		val = Color.White;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(shineTexture, drawPos, null, val * 0.65f, 0f, shineTexture.Size() * 0.5f, new Vector2(0.4f, 1f) * 4.05f * randSize * fade, (SpriteEffects)0);
		val = Color.Goldenrod;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(shineTexture, drawPos, null, val, (float)Math.PI / 2f, shineTexture.Size() * 0.5f, new Vector2(0.4f, 1f) * 4.25f * randSize * fade, (SpriteEffects)0);
		val = Color.White;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(shineTexture, drawPos, null, val * 0.65f, (float)Math.PI / 2f, shineTexture.Size() * 0.5f, new Vector2(0.4f, 1f) * 4.05f * randSize * fade, (SpriteEffects)0);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 50f, targetHitbox);
	}
}
