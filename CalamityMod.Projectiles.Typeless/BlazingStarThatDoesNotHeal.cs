using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class BlazingStarThatDoesNotHeal : ModProjectile, ILocalizedModType, IModType
{
	public static Asset<Texture2D> Bloom;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Particles/Sparkle";

	public override void Load()
	{
		Bloom = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 180;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (base.Projectile.timeLeft % 4 == 0)
		{
			Lighting.AddLight(base.Projectile.Center, 0.45f, 0.35f, 0f);
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 18f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.015f;
		}
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return (1f - completionRatio) * base.Projectile.scale * 16f;
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		return Main.hslToRgb(0.04f * (base.Projectile.ai[0] % 4f) + 0.1f * completionRatio * CalamityUtils.Convert01To010(Main.GlobalTimeWrappedHourly * 0.25f % 1f), 0.8f, 0.6f) * base.Projectile.Opacity * 0.5f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 8);
		Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
		Texture2D sparkleTex = TextureAssets.Projectile[base.Type].Value;
		Texture2D bloomTex = Bloom.Value;
		float bloomScale = (float)sparkleTex.Height / (float)bloomTex.Height;
		float sparkleScale = 0.5f + CalamityUtils.Convert01To010(Main.GlobalTimeWrappedHourly % 2f / 2f) * 0.2f;
		Color color = ColorFunction(0f, Vector2.Zero);
		float rotation = base.Projectile.rotation + Main.GlobalTimeWrappedHourly * 8f;
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(bloomTex, drawPos, null, color * 0.5f, 0f, bloomTex.Size() * 0.5f, 5f * bloomScale, (SpriteEffects)0);
		Main.EntitySpriteDraw(sparkleTex, drawPos, null, Color.Lerp(color, Color.White, 0.7f), rotation, sparkleTex.Size() * 0.5f, 2.2f * sparkleScale, (SpriteEffects)0);
		Main.EntitySpriteDraw(sparkleTex, drawPos, null, color, rotation + (float)Math.PI / 4f, sparkleTex.Size() * 0.5f, 1.6f * sparkleScale, (SpriteEffects)0);
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, ColorFunction(0f, Vector2.Zero), "CalamityMod/Particles/SoftRoundExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0f, 0.04f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		Color smokeColor = Color.Lerp(ColorFunction(0f, Vector2.Zero), Color.DarkSlateGray, 0.5f);
		for (int i = 0; i < 7; i++)
		{
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, Vector2.UnitX.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(7f), smokeColor, 30, Main.rand.NextFloat(0.4f, 1f), 0.5f, Main.rand.NextFloat(-0.03f, 0.03f), glowing: true));
		}
		for (int j = 0; j < 8; j++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), Vector2.UnitX.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(1.8f, 10f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.8f, 1.5f);
			dust.color = ColorFunction(0f, Vector2.Zero);
			dust.noLightEmittence = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), CalamityUtils.SecondsToFrames(15));
	}
}
