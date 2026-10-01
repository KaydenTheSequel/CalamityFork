using System;
using CalamityMod.Enums;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VisNeedle : ModProjectile, ILocalizedModType, IModType, IPixelatedPrimitiveRenderer
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 34;
		base.Projectile.height = 6;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 120;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = 3;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (Main.rand.NextBool(6))
		{
			Vector2 velocity = Vector2.Normalize(base.Projectile.velocity).RotatedBy(Main.rand.NextFloat(-0.07f, 0.07f)) * 0.8f;
			float scale = base.Projectile.scale * 0.33f;
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center, velocity, affectedByGravity: false, 20, scale, Color.Magenta));
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
		return MathHelper.Lerp(0f, MathHelper.Lerp(10f, 0f, completionRatio), MathF.Pow(completionRatio, 0.4f));
	}

	private Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		float offsetTime = Main.GlobalTimeWrappedHourly;
		float fadeOpacity = Utils.GetLerpValue(0.5f, 0f, completionRatio, clamped: true) * (base.Projectile.Opacity * 0.75f);
		return Color.Lerp(Color.Lerp(Color.Magenta, Color.Violet, (float)Math.Sin(completionRatio * (float)Math.PI * 2f - offsetTime * 4f) * 0.5f + 0.5f), Color.White, completionRatio) * fadeOpacity;
	}

	public void RenderPixelatedPrimitives(SpriteBatch spriteBatch, GeneralDrawLayer layer)
	{
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: true, GameShaders.Misc["CalamityMod:TrailStreak"]), 30);
	}
}
