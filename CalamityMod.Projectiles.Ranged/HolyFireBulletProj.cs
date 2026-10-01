using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Ammo;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class HolyFireBulletProj : ModProjectile, ILocalizedModType, IModType
{
	public Color col;

	private float SizeVariance;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public ref float time => ref base.Projectile.ai[2];

	public ref float sizeBonus => ref base.Projectile.ai[1];

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 7;
		base.Projectile.timeLeft = 600;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		if (time == 0f)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 1.15f);
			col = (Main.rand.NextBool() ? Color.Orange : Color.Goldenrod);
			SizeVariance = Main.rand.NextFloat(0.95f, 1.05f);
			base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 14f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
		base.Projectile.spriteDirection = base.Projectile.direction;
		sizeBonus = MathHelper.Lerp(1f, 2f, (float)Math.Pow(Utils.GetLerpValue(50f, 15f, time, clamped: true), 2.0));
		if (time > 4f)
		{
			Math.Sin(Main.GlobalTimeWrappedHourly * 15f / (float)Math.PI);
			if (Main.rand.NextBool(3))
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(5f, 5f), ModContent.DustType<SquashDust>(), -base.Projectile.velocity.RotatedBy(Main.rand.NextFloat(0.15f, 0.3f) * (float)((!Main.rand.NextBool()) ? 1 : (-1))) * Main.rand.NextFloat(0.2f, 1f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.5f, 0.85f);
				dust.noLightEmittence = true;
				dust.color = col;
			}
			float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
			if (time > 2f && targetDist < 1400f)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 0.5f, "CalamityMod/Particles/DualTrail", affectedByGravity: false, 4, 0.03f, col * 0.9f, new Vector2(1f, 3f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.8f, 1f, 0.6f));
			}
		}
		time++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		if (time <= 0f)
		{
			return false;
		}
		Asset<Texture2D> tip = ModContent.Request<Texture2D>("CalamityMod/Particles/SquareRotated", (AssetRequestMode)2);
		for (int i = 0; i < 2; i++)
		{
			Texture2D value = tip.Value;
			Vector2 position = base.Projectile.Center - Main.screenPosition;
			Color val = Color.Lerp(col, Color.White, (float)i);
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value, position, null, val * 0.7f, base.Projectile.rotation, tip.Size() / 2f, new Vector2(0.2f, 1.4f) * base.Projectile.scale * (0.28f - 0.1f * (float)i), (SpriteEffects)0);
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		if (base.Projectile.numHits == 0)
		{
			MakeBlast(0, hitTarget: false);
		}
	}

	public void MakeBlast(int target, bool hitTarget)
	{
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		float blastSize = 50f * sizeBonus;
		float minMultiplier = 0.35f;
		int hitsToMinMult = 8;
		int blastDamage = (int)((float)base.Projectile.damage * 0.33f);
		int knockback = -10;
		int debuff = ModContent.BuffType<HolyFlames>();
		int debuffTime = 180;
		if (hitTarget)
		{
			Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BasicBurstExclusive>(), blastDamage, knockback, base.Projectile.owner, blastSize, minMultiplier, hitsToMinMult);
			projectile.timeLeft = 3;
			projectile.DamageType = base.Projectile.DamageType;
			projectile.localAI[0] = target;
			projectile.localAI[1] = debuff;
			projectile.localAI[2] = debuffTime;
		}
		else
		{
			Projectile projectile2 = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<BasicBurst>(), blastDamage, knockback, base.Projectile.owner, blastSize, minMultiplier, hitsToMinMult);
			projectile2.timeLeft = 3;
			projectile2.DamageType = base.Projectile.DamageType;
			projectile2.localAI[0] = debuff;
			projectile2.localAI[1] = debuffTime;
		}
		SoundEngine.PlaySound(HolyFireBullet.Explosion with
		{
			Pitch = -0.2f + 0.3f * sizeBonus,
			Volume = 0.3f + 0.1f * sizeBonus,
			MaxInstances = 10
		}, base.Projectile.Center);
		float fxScale = MathHelper.Lerp(sizeBonus, 1f, 0.25f);
		Vector2 Offset = Main.rand.NextVector2Circular(15f, 15f);
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center + Offset, Vector2.Zero, Color.Lerp(Color.SlateGray, col, 0.8f) * 0.9f, "CalamityMod/Particles/SmokeExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.05f * fxScale, 0.1f * fxScale, Main.rand.Next(8, 11), UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		float rot = Main.rand.NextFloat(-5f, 5f);
		for (int i = -1; i <= 1; i += 2)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Offset, Vector2.UnitY.RotatedBy(rot + (float)Math.PI / 4f * (float)i) * 0.01f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 5, 0.3f * fxScale, Color.White, new Vector2(1f, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 2f));
		}
		for (int k = 0; k < 7; k++)
		{
			if (Main.rand.NextBool())
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Offset, ModContent.DustType<DiamondDust>(), Utils.RotatedByRandom(new Vector2(4f, 4f), 100.0) * Main.rand.NextFloat(0.5f, 1.5f) * fxScale);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.4f, 0.6f) * fxScale;
				dust.alpha = Main.rand.Next(90, 181);
				dust.color = col;
				dust.fadeIn = 10f;
				dust.noLight = true;
				dust.noLightEmittence = true;
			}
			else
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + Offset, ModContent.DustType<LightDust>(), Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.5f, 1.5f) * fxScale);
				dust2.noGravity = true;
				dust2.scale = Main.rand.NextFloat(0.5f, 1f) * fxScale;
				dust2.alpha = Main.rand.Next(160, 231);
				dust2.color = Color.White;
				dust2.noLight = true;
				dust2.noLightEmittence = true;
			}
		}
		for (int j = 0; j < 2; j++)
		{
			Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center + Offset, ModContent.DustType<SquashDust>(), Utils.RotatedByRandom(new Vector2(6f, 6f), 100.0) * Main.rand.NextFloat(0.5f, 1.5f) * fxScale + new Vector2(0f, -5f));
			dust3.noGravity = false;
			dust3.scale = Main.rand.NextFloat(0.75f, 0.95f) * fxScale;
			dust3.color = (Main.rand.NextBool() ? Color.Orange : Color.Goldenrod);
			dust3.fadeIn = 1f;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 300);
		MakeBlast(target.whoAmI, hitTarget: true);
		if (sizeBonus == 2f)
		{
			SoundStyle style = SoundID.DD2_ExplosiveTrapExplode with
			{
				Pitch = 0.3f,
				Volume = 0.4f,
				MaxInstances = 10
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		modifiers.SourceDamage *= MathHelper.Lerp(1f, 1.15f, sizeBonus - 1f);
	}

	public HolyFireBulletProj()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		col = Color.White;
		base._002Ector();
	}
}
