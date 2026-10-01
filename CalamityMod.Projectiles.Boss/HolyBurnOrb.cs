using System;
using CalamityMod.Dusts;
using CalamityMod.NPCs.Providence;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class HolyBurnOrb : ModProjectile, ILocalizedModType, IModType
{
	private bool started;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/StarProj";

	public override void SetDefaults()
	{
		base.Projectile.localAI[1] = Main.rand.NextFloat(30f);
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.CooldownSlot = 1;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 200;
		base.Projectile.Calamity().DealsDefenseDamage = true;
	}

	public override void AI()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		ProvUtils.ApplyGFBDamage(base.Projectile, 120, 50);
		Lighting.AddLight(base.Projectile.Center, 0.45f, 0.35f, 0f);
		if (!started)
		{
			Color cl = ProvUtils.GetProjectileColor(255);
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, cl, "CalamityMod/Particles/BlastCone", new Vector2(Main.rand.NextFloat(4f, 7f), 1.5f), Vector2.Zero.AngleTo(base.Projectile.velocity), 1f, 0f, 30, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			started = true;
		}
		if (base.Projectile.ai[0] < 240f)
		{
			base.Projectile.ai[0]++;
			if (base.Projectile.timeLeft < 160)
			{
				base.Projectile.timeLeft = 160;
			}
		}
		if (Main.getGoodWorld)
		{
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 12f && base.Projectile.ai[1] == 0f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 1.02f;
			}
			else
			{
				base.Projectile.ai[1] += 0.05f;
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= MathHelper.Lerp(0.95f, 1.05f, (float)Math.Abs(Math.Sin(base.Projectile.ai[1])));
			}
		}
		else if (((Vector2)(ref base.Projectile.velocity)).Length() < 16f)
		{
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= 1.01f;
		}
		base.Projectile.localAI[1] += ((Vector2)(ref base.Projectile.velocity)).Length() / 20f;
		Color col = ProvUtils.GetProjectileColor(255);
		float vel = MathHelper.Clamp(((Vector2)(ref base.Projectile.velocity)).Length() / 5f, 0f, 1.5f);
		GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center, base.Projectile.velocity + Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(vel * 2f), 0f), 6.2831854820251465), affectedByGravity: false, 4, 1f, col));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center + base.Projectile.velocity, Vector2.Zero, col, "CalamityMod/Particles/BlastCone", new Vector2(3f, 2f), Vector2.Zero.AngleFrom(base.Projectile.velocity), 1f, 0f, 3, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		float lerpMult = MathHelper.Lerp(0.5f, 1.5f, Math.Abs(MathF.Sin(base.Projectile.localAI[1] / 10f)));
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		Color baseColor = ProvUtils.GetProjectileColor(255, Outline: true) * 4f;
		Color baseColor2 = ProvUtils.GetProjectileColor(255);
		((Color)(ref baseColor)).A = 0;
		baseColor *= lerpMult;
		baseColor2 *= lerpMult;
		Vector2 origin = value.Size() / 2f;
		Vector2 scale = new Vector2(0.5f, 1f) * ((lerpMult - 1f) * 0.5f + 1f);
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		base.Projectile.rotation += MathHelper.ToRadians(lerpMult * 2f);
		float upRight = (float)Math.PI / 4f;
		float up = (float)Math.PI / 2f;
		float upLeft = (float)Math.PI * 3f / 4f;
		float left = (float)Math.PI;
		Main.EntitySpriteDraw(value, drawPos, null, baseColor, upLeft + base.Projectile.rotation, origin, scale, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, baseColor, upRight - base.Projectile.rotation, origin, scale, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, baseColor2, upLeft + base.Projectile.rotation, origin, scale * 0.6f, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, baseColor2, upRight - base.Projectile.rotation, origin, scale * 0.6f, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, baseColor, up + base.Projectile.rotation, origin, scale * 0.6f, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, baseColor, left - base.Projectile.rotation, origin, scale * 0.6f, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, baseColor2, up + base.Projectile.rotation, origin, scale * 0.36f, spriteEffects);
		Main.EntitySpriteDraw(value, drawPos, null, baseColor2, left - base.Projectile.rotation, origin, scale * 0.36f, spriteEffects);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		Color particleColor = ProvUtils.GetProjectileColor(0);
		Color smokeColor = Color.Lerp(particleColor, Color.DarkSlateGray, 0.5f);
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, smokeColor, "CalamityMod/Particles/SoftRoundExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0f, 0.06f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (int i = 0; i < 7; i++)
		{
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, Vector2.UnitX.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(7f), smokeColor, 30, Main.rand.NextFloat(0.6f, 1f), 0.5f, Main.rand.NextFloat(-0.03f, 0.03f), glowing: true));
		}
		for (int j = 0; j < 8; j++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), Vector2.UnitX.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(1.8f, 10f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(1f, 1.8f);
			dust.color = particleColor;
			dust.noLightEmittence = true;
		}
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		base.Projectile.Kill();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && !target.creativeGodMode)
		{
			ProvUtils.ApplyDebuffs(target, 120);
		}
	}
}
