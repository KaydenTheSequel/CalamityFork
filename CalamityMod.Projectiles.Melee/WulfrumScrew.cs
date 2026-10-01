using System;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class WulfrumScrew : ModProjectile, ILocalizedModType, IModType
{
	internal Color PrimColorMult;

	public static int Lifetime = 950;

	public static float BazingaTimeMax = 120f;

	public static Asset<Texture2D> SheenTex;

	public new string LocalizationCategory => "Projectiles.Melee";

	public float LifetimeCompletion => MathHelper.Clamp((float)(Lifetime - base.Projectile.timeLeft) / (float)Lifetime, 0f, 1f);

	public ref float BazingaTime => ref base.Projectile.ai[0];

	public ref float AlreadyBazinged => ref base.Projectile.ai[1];

	public float BazingaTimeCompletion => (BazingaTimeMax - BazingaTime) / BazingaTimeMax;

	public float FadePercent => Math.Clamp((float)base.Projectile.timeLeft / FadeTime, 0f, 1f);

	public static float FadeTime => 30f;

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 60;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 22;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.extraUpdates = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 1;
		base.Projectile.scale = 1.2f;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.998f;
		if (base.Projectile.timeLeft < Lifetime - 100 && BazingaTime == 0f)
		{
			base.Projectile.velocity.Y += 0.01f;
		}
		if (BazingaTime > 0f)
		{
			BazingaTime--;
			if (BazingaTime == BazingaTimeMax - 2f)
			{
				Owner.Calamity().GeneralScreenShakePower = 0f;
			}
			Vector2 position = base.Projectile.Center + base.Projectile.velocity.RotatedBy(1.5707963705062866).SafeNormalize(Vector2.Zero) * Main.rand.NextFloat(-3f, 3f);
			Vector2? velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.6f, 1.5f);
			float scale = Main.rand.NextFloat(1f, 1.4f);
			Dust chust = Dust.NewDustPerfect(position, 15, velocity, 0, default(Color), scale);
			chust.noGravity = true;
			if (!Main.rand.NextBool(5))
			{
				chust.noLightEmittence = true;
			}
		}
		if (base.Projectile.Center.Distance(Owner.MountedCenter) > 1300f && (float)base.Projectile.timeLeft > FadeTime)
		{
			base.Projectile.timeLeft = (int)FadeTime;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in WulfrumKnife.TileHitSound, base.Projectile.Center);
		bool screwRegained = false;
		if (target.life - hit.Damage <= 0 && Main.rand.NextBool() && Main.myPlayer == Owner.whoAmI && Owner.HeldItem.ModItem is WulfrumScrewdriver { ScrewStored: false } screwdriver)
		{
			WulfrumScrewdriver.ScrewStart = new Vector3(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 4f * Main.rand.NextFloat() - Main.screenPosition, base.Projectile.rotation);
			WulfrumScrewdriver.ScrewTimer = WulfrumScrewdriver.ScrewTime;
			screwdriver.ScrewStored = true;
			SoundEngine.PlaySound(in SoundID.Item156);
			screwRegained = true;
		}
		if (!screwRegained && !Main.dedServ)
		{
			Gore.NewGorePerfect(base.Projectile.GetSource_Death(), base.Projectile.position, -Vector2.UnitY.RotatedByRandom(0.7853981852531433) * Main.rand.NextFloat(4f, 6f) + base.Projectile.velocity * 0.7f, base.Mod.Find<ModGore>("WulfrumScrewGore").Type).timeLeft = 20;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		if (BazingaTime > 0f)
		{
			SoundEngine.PlaySound(in CommonCalamitySounds.WulfrumNPCDeathSound, base.Projectile.Center);
		}
		SoundEngine.PlaySound(in WulfrumKnife.TileHitSound, base.Projectile.Center);
		if (!Main.dedServ)
		{
			Gore.NewGorePerfect(base.Projectile.GetSource_Death(), base.Projectile.position, -Vector2.UnitY.RotatedByRandom(0.7853981852531433) * Main.rand.NextFloat(1f, 3f) + base.Projectile.velocity * 0.7f, base.Mod.Find<ModGore>("WulfrumScrewGore").Type).timeLeft = 20;
		}
		return base.OnTileCollide(oldVelocity);
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		float fadeOpacity = (float)Math.Pow(1f - completionRatio, 2.0) * (float)Math.Pow(1f - BazingaTimeCompletion, 0.4000000059604645);
		return Color.GreenYellow.MultiplyRGB(PrimColorMult) * fadeOpacity;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return 9.4f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0653: Unknown result type (might be due to invalid IL or missing references)
		//IL_0658: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0673: Unknown result type (might be due to invalid IL or missing references)
		//IL_0678: Unknown result type (might be due to invalid IL or missing references)
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_050d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		float distanceFromAim = base.Projectile.Center.ShortestDistanceToLine(Owner.MountedCenter, Main.MouseWorld);
		Vector2 val = Owner.MountedCenter - base.Projectile.Center.ClosestPointOnLine(Owner.MountedCenter, Main.MouseWorld);
		float distanceFromPlayerAcrossSightLine = ((Vector2)(ref val)).Length();
		float opacity = MathHelper.Clamp(1f - distanceFromAim / 90f, 0f, 1f) * (1f - Math.Clamp((float)Math.Pow(distanceFromPlayerAcrossSightLine / 300f, 9.0), 0f, 1f));
		if (Owner.whoAmI == Main.myPlayer && BazingaTime == 0f && opacity > 0f)
		{
			Texture2D empty = ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj", (AssetRequestMode)2).Value;
			Effect laserScopeEffect = Filters.Scene["CalamityMod:PixelatedSightLine"].GetShader().Shader;
			laserScopeEffect.Parameters["sampleTexture2"].SetValue((Texture)(object)ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/CertifiedCrustyNoise", (AssetRequestMode)2).Value);
			laserScopeEffect.Parameters["noiseOffset"].SetValue((float)Main.GameUpdateCount * -0.003f);
			laserScopeEffect.Parameters["mainOpacity"].SetValue((float)Math.Pow(opacity, 0.5));
			laserScopeEffect.Parameters["Resolution"].SetValue(new Vector2(140f));
			laserScopeEffect.Parameters["laserAngle"].SetValue((Main.MouseWorld - Owner.MountedCenter).ToRotation() * -1f);
			laserScopeEffect.Parameters["laserWidth"].SetValue(0.0025f + (float)Math.Pow(opacity, 5.0) * ((float)Math.Sin(Main.GlobalTimeWrappedHourly * 3f) * 0.002f + 0.002f));
			laserScopeEffect.Parameters["laserLightStrenght"].SetValue(3f);
			EffectParameter obj = laserScopeEffect.Parameters["color"];
			Color val2 = Color.GreenYellow;
			obj.SetValue(((Color)(ref val2)).ToVector3());
			EffectParameter obj2 = laserScopeEffect.Parameters["darkerColor"];
			val2 = Color.Black;
			obj2.SetValue(((Color)(ref val2)).ToVector3());
			laserScopeEffect.Parameters["bloomSize"].SetValue(0.06f + (1f - opacity) * 0.1f);
			laserScopeEffect.Parameters["bloomMaxOpacity"].SetValue(0.4f);
			laserScopeEffect.Parameters["bloomFadeStrenght"].SetValue(3f);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, laserScopeEffect, Main.GameViewMatrix.TransformationMatrix);
			Main.EntitySpriteDraw(empty, base.Projectile.Center - Main.screenPosition, null, Color.White, 0f, empty.Size() / 2f, 700f, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		}
		opacity = BazingaTimeCompletion * FadePercent;
		if (BazingaTime > 0f)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/BasicTrail", (AssetRequestMode)2));
			CalamityUtils.DrawChromaticAberration(Vector2.UnitX, 1f, delegate(Vector2 offset, Color colorMod)
			{
				//IL_000e: Unknown result type (might be due to invalid IL or missing references)
				//IL_000f: Unknown result type (might be due to invalid IL or missing references)
				//IL_001a: Unknown result type (might be due to invalid IL or missing references)
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				PrimColorMult = colorMod;
				PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
				{
					//IL_0010: Unknown result type (might be due to invalid IL or missing references)
					//IL_001a: Unknown result type (might be due to invalid IL or missing references)
					//IL_0020: Unknown result type (might be due to invalid IL or missing references)
					//IL_0025: Unknown result type (might be due to invalid IL or missing references)
					return base.Projectile.Size * 0.5f + offset;
				}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 30);
			});
			CalamityUtils.DrawChromaticAberration(Vector2.UnitX, 3f, delegate(Vector2 offset, Color colorMod)
			{
				//IL_0011: Unknown result type (might be due to invalid IL or missing references)
				//IL_0016: Unknown result type (might be due to invalid IL or missing references)
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0020: Unknown result type (might be due to invalid IL or missing references)
				//IL_0021: Unknown result type (might be due to invalid IL or missing references)
				//IL_002f: Unknown result type (might be due to invalid IL or missing references)
				//IL_0034: Unknown result type (might be due to invalid IL or missing references)
				//IL_0035: Unknown result type (might be due to invalid IL or missing references)
				//IL_0040: Unknown result type (might be due to invalid IL or missing references)
				//IL_005b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0065: Unknown result type (might be due to invalid IL or missing references)
				Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition + offset, null, Color.GreenYellow.MultiplyRGB(colorMod) * opacity, base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
			});
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor) * (1f - opacity), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			if (SheenTex == null)
			{
				SheenTex = ModContent.Request<Texture2D>("CalamityMod/Particles/HalfStar", (AssetRequestMode)2);
			}
			Texture2D shineTex = SheenTex.Value;
			Vector2 shineScale = default(Vector2);
			((Vector2)(ref shineScale))._002Ector(1f, 3f - opacity * 2f);
			Main.EntitySpriteDraw(shineTex, base.Projectile.Center - Main.screenPosition, null, Color.GreenYellow * (1f - opacity) * 0.7f, (float)Math.PI / 2f, shineTex.Size() / 2f, shineScale * base.Projectile.scale, (SpriteEffects)0);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		}
		else
		{
			Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor) * FadePercent, base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		}
		return false;
	}

	public WulfrumScrew()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		PrimColorMult = Color.White;
		base._002Ector();
	}
}
