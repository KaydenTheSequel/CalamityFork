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

namespace CalamityMod.Projectiles.Magic;

public class WarpSigilShot : ModProjectile, ILocalizedModType, IModType, IPixelatedPrimitiveRenderer
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float FixedOffsetX => ref base.Projectile.ai[0];

	public ref float FixedOffsetY => ref base.Projectile.ai[1];

	public ref float DamageStatus => ref base.Projectile.ai[2];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 36;
		base.Projectile.height = 36;
		base.Projectile.friendly = false;
		base.Projectile.hostile = false;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 28;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = 1;
	}

	public override void AI()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		if (DamageStatus == 1f && base.Projectile.timeLeft <= 6)
		{
			base.Projectile.Kill();
		}
		Vector2 fixedOffset = default(Vector2);
		((Vector2)(ref fixedOffset))._002Ector(FixedOffsetX, FixedOffsetY);
		Vector2 val = Main.MouseWorld + fixedOffset;
		Vector2 direction = val - base.Projectile.Center;
		((Vector2)(ref direction)).Length();
		((Vector2)(ref direction)).Normalize();
		float homingStrength = 0f;
		if (base.Projectile.timeLeft <= 22)
		{
			homingStrength = 0.475f;
		}
		float maxSpeed = 60f;
		base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, direction * maxSpeed, homingStrength * 0.4f);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1.05f;
		if (((Vector2)(ref base.Projectile.velocity)).Length() > maxSpeed)
		{
			base.Projectile.velocity = Vector2.Normalize(base.Projectile.velocity) * maxSpeed;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Vector2 vectorToTarget = val - base.Projectile.Center;
		if (Vector2.Dot(base.Projectile.velocity, vectorToTarget) < 0f)
		{
			DamageStatus = 1f;
		}
		base.Projectile.friendly = DamageStatus == 1f;
		float sine = (float)Math.Sin((float)base.Projectile.timeLeft * 0.575f / (float)Math.PI);
		Vector2 offset = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedBy(1.5707963705062866) * sine * 16f;
		if (Main.rand.NextBool(5))
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center - offset, ModContent.DustType<LightDust>(), -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 0.8f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.4f, 0.85f);
			dust.color = Color.Violet;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/PlagueSounds/PlagueBoom4");
		style.Volume = 0.25f;
		style.Pitch = 0f;
		style.MaxInstances = 4;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.DarkMagenta * 2f, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.04f, 14, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (int i = 0; i < 8; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 173, Main.rand.NextVector2Circular(2f, 2f));
			dust.noGravity = true;
			dust.scale = 0.5f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int frameHeight = texture.Height / Main.projFrames[base.Type];
		Rectangle sourceRect = default(Rectangle);
		((Rectangle)(ref sourceRect))._002Ector(0, frameHeight * base.Projectile.frame, texture.Width, frameHeight);
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, sourceRect, Color.White, base.Projectile.rotation, sourceRect.Size() * 0.5f, 1f, (SpriteEffects)0);
		return false;
	}

	private float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return MathHelper.Lerp(0f, MathHelper.Lerp(32f, 0f, completionRatio), MathF.Pow(completionRatio, 0.4f));
	}

	private Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		float offsetTime = Main.GlobalTimeWrappedHourly;
		float fadeOpacity = Utils.GetLerpValue(0.5f, 0f, completionRatio, clamped: true) * base.Projectile.Opacity;
		return Color.Lerp(Color.Lerp(Color.Magenta, Color.DarkMagenta, (float)Math.Sin(completionRatio * (float)Math.PI * 1.6f - offsetTime * 4f) * 0.5f + 0.5f), Color.White, completionRatio) * fadeOpacity;
	}

	public void RenderPixelatedPrimitives(SpriteBatch spriteBatch, GeneralDrawLayer layer)
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: true, GameShaders.Misc["CalamityMod:TrailStreak"]), 32);
		GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(base.Projectile.Center, base.Projectile.velocity, base.Projectile.scale * 0.4f, Color.Magenta, 2, 1f, 0.5f, 1f), pixelate: true);
	}
}
