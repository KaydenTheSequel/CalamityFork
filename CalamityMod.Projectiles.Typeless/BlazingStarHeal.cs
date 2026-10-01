using System;
using CalamityMod.Dusts;
using CalamityMod.Enums;
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

public class BlazingStarHeal : ModProjectile, ILocalizedModType, IModType
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
		ProjectileID.Sets.TrailCacheLength[base.Type] = 25;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 200;
	}

	public override void AI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft == 200)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		}
		base.Projectile.rotation += MathHelper.ToRadians(6f) * Utils.GetLerpValue(0f, 30f, base.Projectile.timeLeft, clamped: true);
		base.Projectile.scale = Utils.GetLerpValue(-10f, 30f, base.Projectile.timeLeft, clamped: true);
		if (base.Projectile.timeLeft % 4 == 0)
		{
			Lighting.AddLight(base.Projectile.Center, 0f, 0.6f, 0f);
		}
		if (base.Projectile.timeLeft > 190)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.1f;
		}
		else if (base.Projectile.timeLeft <= 190)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.99f;
		}
		if (base.Projectile.timeLeft <= 160)
		{
			base.Projectile.velocity = Vector2.Zero;
		}
		int index = Player.FindClosest(base.Projectile.position, base.Projectile.width, base.Projectile.height);
		Player player = Main.player[index];
		if (base.Projectile.timeLeft <= 190 && player != null && Main.player[base.Projectile.owner].team == player.team)
		{
			float playerDist = Vector2.Distance(player.Center, base.Projectile.Center);
			if (!player.immune && playerDist < 30f * base.Projectile.scale && base.Projectile.timeLeft <= 190)
			{
				int healAmt = Utils.Clamp((200 - base.Projectile.timeLeft) / 10, 1, 10);
				player.HealPlayer(healAmt, HealTextType.Local);
				NetMessage.SendData(66, -1, -1, null, index, healAmt);
				base.Projectile.Kill();
			}
		}
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return (1f - completionRatio) * base.Projectile.scale * 16f;
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		return Main.hslToRgb(0.35f + 0.1f * completionRatio * CalamityUtils.Convert01To010(Main.GlobalTimeWrappedHourly * 0.25f % 1f), 0.6f, 0.5f) * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 25);
		Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
		Texture2D sparkleTex = TextureAssets.Projectile[base.Type].Value;
		Texture2D bloomTex = Bloom.Value;
		float bloomScale = (float)sparkleTex.Height / (float)bloomTex.Height * base.Projectile.scale;
		float sparkleScale = (0.5f + CalamityUtils.Convert01To010(Main.GlobalTimeWrappedHourly % 2f / 2f) * 0.2f) * base.Projectile.scale;
		Color color = ColorFunction(0f, Vector2.Zero);
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition;
		Main.EntitySpriteDraw(bloomTex, drawPos, null, color * 0.5f, 0f, bloomTex.Size() * 0.5f, 5f * bloomScale, (SpriteEffects)0);
		Main.EntitySpriteDraw(sparkleTex, drawPos, null, Color.Lerp(color, Color.White, 0.7f), base.Projectile.rotation, sparkleTex.Size() * 0.5f, 2.2f * sparkleScale, (SpriteEffects)0);
		Main.EntitySpriteDraw(sparkleTex, drawPos, null, color, base.Projectile.rotation + (float)Math.PI / 4f, sparkleTex.Size() * 0.5f, 1.6f * sparkleScale, (SpriteEffects)0);
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.Item14 with
		{
			Pitch = -0.3f,
			Volume = 0.7f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		style = new SoundStyle("CalamityMod/Sounds/Custom/PlantyMushMine", 3);
		style.Volume = 0.5f;
		style.Pitch = 0.3f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
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

	public override bool? CanDamage()
	{
		return false;
	}
}
