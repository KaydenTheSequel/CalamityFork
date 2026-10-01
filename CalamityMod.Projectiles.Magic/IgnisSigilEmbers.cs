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

public class IgnisSigilEmbers : ModProjectile, ILocalizedModType, IModType, IPixelatedPrimitiveRenderer
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 9;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.friendly = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
		base.Projectile.damage = 0;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0.22f / 255f, (float)(255 - base.Projectile.alpha) * 0.05f / 255f, (float)(255 - base.Projectile.alpha) * 0.05f / 255f);
		base.Projectile.scale -= 0.01f;
		if (base.Projectile.scale <= 0f)
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.ai[0] <= 3f)
		{
			base.Projectile.ai[0]++;
		}
		else
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.35f;
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
		return MathHelper.Lerp(0f, MathHelper.Lerp(base.Projectile.scale * 48f, 0f, completionRatio), MathF.Pow(completionRatio, 0.4f));
	}

	private Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.GlobalTimeWrappedHourly;
		float fadeOpacity = Utils.GetLerpValue(0.5f, 0f, completionRatio, clamped: true) * base.Projectile.Opacity;
		return Color.Lerp(Color.DarkOrange, Color.DarkSalmon, completionRatio) * fadeOpacity;
	}

	public void RenderPixelatedPrimitives(SpriteBatch spriteBatch, GeneralDrawLayer layer)
	{
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: true, GameShaders.Misc["CalamityMod:TrailStreak"]), 32);
		GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center, base.Projectile.velocity, affectedByGravity: false, 2, base.Projectile.scale * 1.45f, Color.Salmon), pixelate: true);
	}
}
