using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class OrderbringerWaveProj : ModProjectile, ILocalizedModType, IModType
{
	public float hitboxSize = 10f;

	public Color mainColor;

	public float fade = 1f;

	public float damageMult = 1f;

	public float fadeOut = 1f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/JudgementProj";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 336;
		base.Projectile.height = 274;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 450;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.extraUpdates = 22;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		Vector2 topCorner = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(MathHelper.ToRadians(125f)) * base.Projectile.scale * 190f;
		Vector2 bottomCorner = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(MathHelper.ToRadians(-125f)) * base.Projectile.scale * 190f;
		Player player = Main.player[base.Projectile.owner];
		mainColor = player.Calamity().lightRGB;
		if (time == 0f)
		{
			base.Projectile.scale = 0.0875f;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.25f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		if (time < 200f)
		{
			base.Projectile.scale += 0.0026f;
			hitboxSize += 0.4525f;
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.995f;
		}
		else
		{
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= 0.975f;
		}
		if (time > 300f && fade > 0f)
		{
			fade -= 0.0065f;
		}
		if (time < 250f && time > 20f)
		{
			if (Main.rand.NextBool(3))
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(hitboxSize * 1.25f, hitboxSize * 1.25f) - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 25f, ModContent.DustType<SquashDust>());
				dust.scale = Main.rand.NextFloat(1.6f, 1.95f) - ((time < 150f) ? 0f : (time * 0.001f));
				dust.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.85f, 1.5f);
				dust.color = mainColor;
				dust.noGravity = true;
				dust.fadeIn = 2f;
			}
			if (time % 9f == 0f && time < 200f)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(topCorner, base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(MathHelper.ToRadians(-170f)), "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 18, 0.35f, mainColor * 0.75f, new Vector2(1f, 2.5f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.2f, 1f, 0.7f));
				GeneralParticleHandler.SpawnParticle(new CustomSpark(bottomCorner, base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(MathHelper.ToRadians(170f)), "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 18, 0.35f, mainColor * 0.75f, new Vector2(1f, 2.5f), useAddativeBlend: true, glowCenter: true, 0f, fadeIn: false, affectedByLight: false, 0.2f, 1f, 0.7f));
			}
			if (Main.rand.NextBool(12))
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(topCorner, base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(MathHelper.ToRadians(170f)) * Main.rand.NextFloat(5f, 20f), affectedByGravity: false, 15, Main.rand.NextFloat(0.8f, 1.35f), mainColor));
				GeneralParticleHandler.SpawnParticle(new SparkParticle(bottomCorner, base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(MathHelper.ToRadians(-170f)) * Main.rand.NextFloat(5f, 20f), affectedByGravity: false, 15, Main.rand.NextFloat(0.8f, 1.35f), mainColor));
			}
		}
		time++;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		float waveFade = (float)Math.Pow(Utils.GetLerpValue(0f, 250f, base.Projectile.timeLeft, clamped: true), 2.0);
		float waveFade2 = (float)Math.Pow(Utils.GetLerpValue(0f, 80f, base.Projectile.timeLeft, clamped: true), 2.0);
		for (int i = 1; i < 15; i++)
		{
			SpriteBatch spriteBatch = Main.spriteBatch;
			Vector2 val = base.Projectile.Center - Main.screenPosition - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 6f * (float)i;
			Color val2 = mainColor;
			((Color)(ref val2)).A = 0;
			spriteBatch.Draw(tex, val, (Rectangle?)null, val2 * waveFade * (1f - 0.1f * (float)i), base.Projectile.rotation, tex.Size() / 2f, new Vector2(1.1f * MathHelper.Lerp(waveFade2, 1f, 0.3f), 0.6f + (1f - waveFade) * 0.8f) * base.Projectile.scale * 1.1f, (SpriteEffects)0, 0f);
		}
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 300);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= damageMult;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, hitboxSize, targetHitbox);
	}
}
