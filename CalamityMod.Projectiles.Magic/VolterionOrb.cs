using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VolterionOrb : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle FireSound = new SoundStyle("CalamityMod/Sounds/Item/VolterionOrbShot")
	{
		Volume = 0.6f
	};

	public static readonly SoundStyle ExplosionSound = new SoundStyle("CalamityMod/Sounds/Item/ArcNovaDiffuserChargeImpact")
	{
		Volume = 0.5f
	};

	public static float ExplosionTime = 150f;

	public static float ExplosionLifetime = 18f;

	public static float MaxScale = 7.5f;

	public static float AttackRate = 60f;

	public static float AttackRange = 800f;

	public static float LightningDamageMult = 0.25f;

	public static Asset<Texture2D> Bloom;

	public static Asset<Texture2D> Explosion;

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float OrbType => ref base.Projectile.ai[0];

	public ref float AttackTimer => ref base.Projectile.ai[1];

	public ref float ExplosionTimer => ref base.Projectile.ai[2];

	public Player Owner => Main.player[base.Projectile.owner];

	public override void Load()
	{
		Bloom = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		Explosion = ModContent.Request<Texture2D>("CalamityMod/Particles/PlasmaExplosion", (AssetRequestMode)2);
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 38);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 4 % Main.projFrames[base.Type];
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.8f;
		base.Projectile.rotation += 0.01f;
		Vector2 center = base.Projectile.Center;
		Color newColor = GetColor(OrbType);
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
		AttackTimer++;
		float offset = 9f * OrbType;
		if (AttackTimer > AttackRate * 2f)
		{
			if (AttackTimer > ExplosionTime)
			{
				if (ExplosionTimer == 0f)
				{
					SoundEngine.PlaySound(in ExplosionSound, base.Projectile.Center);
				}
				float scaleLevel = CalamityUtils.PiecewiseAnimation(ExplosionTimer / ExplosionLifetime, new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, 1f, 4));
				base.Projectile.scale = MathHelper.Lerp(1f, MaxScale, scaleLevel);
				base.Projectile.Opacity = MathF.Sin((float)Math.PI / 2f + (float)Math.PI / 2f * ExplosionTimer / ExplosionLifetime);
				if (ExplosionTimer++ > ExplosionLifetime)
				{
					base.Projectile.Kill();
				}
			}
			else if (AttackTimer > MathHelper.Lerp(AttackRate * 2f, ExplosionTime, 0.8f) || AttackTimer % 3f == 2f)
			{
				Vector2 velocity = Main.rand.NextVector2Unit() * Main.rand.NextFloat(8f, 10f);
				Vector2 center2 = base.Projectile.Center;
				Vector2? velocity2 = velocity;
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(center2, 278, velocity2, 0, newColor);
				dust.noLight = true;
				dust.noGravity = Main.rand.NextBool();
				dust.color = GetColor(OrbType);
			}
		}
		else
		{
			if (AttackTimer % AttackRate != AttackRate - offset - 1f)
			{
				return;
			}
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0f;
			NPC target = base.Projectile.Center.ClosestNPCAt(AttackRange);
			if (target != null)
			{
				SoundEngine.PlaySound(in FireSound, base.Projectile.Center);
				Vector2 velocity3 = base.Projectile.SafeDirectionTo(target.Center) * 16f;
				Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity3, ModContent.ProjectileType<VolterionShot>(), (int)((float)base.Projectile.damage * LightningDamageMult), base.Projectile.knockBack * LightningDamageMult, base.Projectile.owner, OrbType + 1f).tileCollide = false;
				GeneralParticleHandler.SpawnParticle(new CrackParticle(base.Projectile.Center, velocity3 * 0.5f, GetColor(OrbType), Vector2.One, 0f, 0f, Main.rand.NextFloat(0.8f, 1f), 12));
				for (int i = 1; i < 4; i++)
				{
					GeneralParticleHandler.SpawnParticle(new CrackParticle(base.Projectile.Center, velocity3.RotatedBy((float)Math.PI / 2f * (float)i + MathHelper.ToRadians(Main.rand.NextFloat(-40f, 40f))) * 0.5f, GetColor(4f - OrbType) * 0.6f, Vector2.One, 0f, 0f, Main.rand.NextFloat(0.5f, 0.6f), 12));
				}
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(144, 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(144, 120);
	}

	public static Color GetColor(float type)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(new Color(51, 197, 255), new Color(143, 51, 255), 0.2f + 0.15f * type + 0.2f * MathF.Sin(Main.GlobalTimeWrappedHourly * 10f));
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return GetColor(OrbType);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition;
		Color color = base.Projectile.GetAlpha(lightColor);
		if (ExplosionTimer > 0f)
		{
			Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
			Texture2D explosionTex = Explosion.Value;
			Main.EntitySpriteDraw(explosionTex, drawPos, null, color * base.Projectile.Opacity, base.Projectile.rotation, explosionTex.Size() * 0.5f, 0.02f * base.Projectile.scale, (SpriteEffects)0);
			Main.spriteBatch.ExitShaderRegion();
			return false;
		}
		Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
		Texture2D bloomTex = Bloom.Value;
		Main.EntitySpriteDraw(bloomTex, drawPos, null, color * 0.5f, 0f, bloomTex.Size() * 0.5f, 0.42f, (SpriteEffects)0);
		Main.spriteBatch.ExitShaderRegion();
		return true;
	}

	public override bool? CanDamage()
	{
		if (!(ExplosionTimer > 0f))
		{
			return false;
		}
		return base.CanDamage();
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 20f * base.Projectile.scale, targetHitbox);
	}
}
