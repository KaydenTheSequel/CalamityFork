using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class SolarNeedle : ModProjectile, ILocalizedModType, IModType
{
	private const float MaxTime = 30f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public float Timer => 30f - (float)base.Projectile.timeLeft;

	public ref float Empowered => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 30;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.tileCollide = false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		float collisionPoint = 0f;
		float bladeLength = 44f * base.Projectile.scale;
		Vector2 start = -base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 16f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center + start, base.Projectile.Center + start + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * bladeLength, 24f, ref collisionPoint);
	}

	public override void AI()
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.scale = 2.4f;
		base.Projectile.Opacity = 0.6f;
		Lighting.AddLight(base.Projectile.Center, 0.75f, 1f, 0.24f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1f - (float)Math.Pow(Timer / 30f, 3.0);
		if (Main.rand.NextBool(3))
		{
			int dustTrail = Dust.NewDust(base.Projectile.Center, 14, 14, 66, base.Projectile.velocity.X * 0.05f, base.Projectile.velocity.Y * 0.05f, 150, new Color(Main.DiscoR, 100, 255), 1.2f);
			Main.dust[dustTrail].noGravity = true;
		}
		if (Main.rand.NextBool(3))
		{
			int dustType = Main.rand.Next(3);
			Dust.NewDust(base.Projectile.Center, 14, 14, dustType switch
			{
				1 => 57, 
				0 => 15, 
				_ => 58, 
			}, base.Projectile.velocity.X * 0.1f, base.Projectile.velocity.Y * 0.1f, 150, default(Color), 1.3f);
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 1f)
		{
			base.Projectile.Kill();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 35)
		{
			return false;
		}
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SolarNeedle", (AssetRequestMode)2).Value;
		if (Empowered == 1f)
		{
			Main.spriteBatch.EnterShaderRegion();
			Color outlineColor = Color.Lerp(Color.White, Color.OrangeRed, Timer / 30f);
			Vector3 outlineHSL = Main.rgbToHsl(outlineColor);
			float outlineThickness = MathHelper.Clamp(Timer / 30f * 4f, 0f, 3f);
			GameShaders.Misc["CalamityMod:BasicTint"].UseOpacity(1f);
			GameShaders.Misc["CalamityMod:BasicTint"].UseColor(Main.hslToRgb(1f - outlineHSL.X, outlineHSL.Y, outlineHSL.Z));
			GameShaders.Misc["CalamityMod:BasicTint"].Apply();
			for (float i = 0f; i < 1f; i += 0.125f)
			{
				Main.EntitySpriteDraw(texture, base.Projectile.Center + (i * ((float)Math.PI * 2f) + base.Projectile.rotation).ToRotationVector2() * outlineThickness - Main.screenPosition, null, outlineColor, base.Projectile.rotation, texture.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
			}
			Main.spriteBatch.ExitShaderRegion();
		}
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		Texture2D starTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/Sparkle", (AssetRequestMode)2).Value;
		Texture2D bloomTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		float properBloomSize = (float)starTexture.Height / (float)bloomTexture.Height;
		Color color = Main.hslToRgb(Main.GlobalTimeWrappedHourly * 0.6f % 1f, 1f, 0.85f);
		float rotation = Main.GlobalTimeWrappedHourly * 8f;
		Vector2 sparkCenter = base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 30.5f - Main.screenPosition;
		Main.EntitySpriteDraw(bloomTexture, sparkCenter, null, color * 0.5f, 0f, bloomTexture.Size() / 2f, 4f * properBloomSize, (SpriteEffects)0);
		Main.EntitySpriteDraw(starTexture, sparkCenter, null, color * 0.5f, rotation + (float)Math.PI / 4f, starTexture.Size() / 2f, 1.5f, (SpriteEffects)0);
		Main.EntitySpriteDraw(starTexture, sparkCenter, null, Color.White, rotation, starTexture.Size() / 2f, 2f, (SpriteEffects)0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.DD2_WitherBeastDeath, base.Projectile.Center);
		for (int i = 0; i < 10; i++)
		{
			Vector2 particleSpeed = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(1.2f, 2.3f);
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 40f, particleSpeed, Main.rand.NextFloat(0.3f, 0.6f), Color.Cyan, 60, 1f, 1.5f, 3f, 0.02f));
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 60);
		Vector2 val = target.Hitbox.Size();
		Vector2 particleOrigin = ((((Vector2)(ref val)).Length() < 140f) ? target.Center : (base.Projectile.Center + base.Projectile.rotation.ToRotationVector2() * 60f));
		for (int i = 0; i < 10; i++)
		{
			Vector2 particleSpeed = Main.rand.NextVector2CircularEdge(1f, 1f) * Main.rand.NextFloat(2.6f, 4f);
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(particleOrigin, particleSpeed, Main.rand.NextFloat(0.3f, 0.6f), Color.Cyan, 60, 1f, 1.5f, 3f, 0.02f));
		}
	}
}
