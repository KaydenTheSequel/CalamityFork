using System;
using System.Linq;
using CalamityMod.Enums;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class SHPL : ModProjectile, ILocalizedModType, IModType, IPixelatedPrimitiveRenderer
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public bool ShouldStopAndDie
	{
		get
		{
			return base.Projectile.ai[1] == 1f;
		}
		set
		{
			base.Projectile.ai[1] = value.ToInt();
		}
	}

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public GeneralDrawLayer LayerToRenderTo => GeneralDrawLayer.BeforeProjectiles;

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 14;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 5;
		base.Projectile.height = 5;
		base.Projectile.scale = 1.85f;
		base.Projectile.friendly = true;
		base.Projectile.extraUpdates = 3;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override bool? CanDamage()
	{
		return !ShouldStopAndDie;
	}

	public override void AI()
	{
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		if (ShouldStopAndDie)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0f;
			if (base.Projectile.oldPos.Last() == base.Projectile.position)
			{
				base.Projectile.scale -= 0.15f;
				if (base.Projectile.scale <= 0f)
				{
					base.Projectile.Kill();
					return;
				}
			}
			for (int i = 0; i < 2; i++)
			{
				Vector2 dustVelocity = Main.rand.NextVector2Circular(1f, 1f) * 6f;
				float dustScale = Main.rand.NextFloat(1.8f, 2.4f);
				Color dustColor = Color.Lerp(SHPB.FindColorForSoul((int)base.Projectile.ai[0]), Color.White, Main.rand.NextFloat());
				Dust dust = Dust.NewDustDirect(base.Projectile.Center, 1, 1, 43, dustVelocity.X, dustVelocity.Y, 0, dustColor, dustScale);
				dust.noGravity = true;
				dust.noLight = false;
				dust.noLightEmittence = false;
			}
		}
		base.Projectile.rotation += (float)Math.PI / 30f;
		Lighting.AddLight((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16, 0.5f, 0.2f, 0.5f);
		float timerIncr = 3f;
		base.Projectile.localAI[0] += timerIncr;
		if (base.Projectile.localAI[0] > 100f)
		{
			base.Projectile.localAI[0] = 100f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		ShouldStopAndDie = true;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		ShouldStopAndDie = true;
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		GeneralParticleHandler.SpawnParticle(new AltLineParticle(base.Projectile.Center, -base.Projectile.velocity.RotatedByRandom(0.5235987901687622), affectedByGravity: false, 20, 1f, SHPB.FindColorForSoul((int)base.Projectile.ai[0])));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		if (ShouldStopAndDie)
		{
			Texture2D starTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/FullStar", (AssetRequestMode)2).Value;
			Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
			Main.spriteBatch.SetBlendState(BlendState.Additive);
			Main.EntitySpriteDraw(starTexture, drawPosition, null, Color.White, base.Projectile.rotation, starTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
			Main.spriteBatch.SetBlendState(BlendState.AlphaBlend);
		}
	}

	public float LaserWidthFunction(float completion, Vector2 vertexPos)
	{
		float num = base.Projectile.scale * (float)base.Projectile.width;
		float scaleInterpolant = Utils.GetLerpValue(0.05f, 0.2f, completion, clamped: true) * Utils.GetLerpValue(0.95f, 0.8f, completion, clamped: true);
		return num * scaleInterpolant * base.Projectile.scale;
	}

	public float LaserCoreEnergyFunction(float completion, Vector2 vertexPos)
	{
		float num = base.Projectile.scale * (float)base.Projectile.width * 0.25f;
		float scaleInterpolant = Utils.GetLerpValue(0.05f, 0.2f, completion, clamped: true) * Utils.GetLerpValue(0.95f, 0.8f, completion, clamped: true);
		return num * scaleInterpolant * base.Projectile.scale;
	}

	public Color LaserColorFunction(float completion, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		Color val = Color.Lerp(Color.White, SHPB.FindColorForSoul((int)base.Projectile.ai[0]), Utils.GetLerpValue(0f, 0.125f, completion, clamped: true));
		Color bodyColor = Color.Lerp(val, SHPB.FindColorForSoul((int)base.Projectile.ai[0]), Utils.GetLerpValue(0.125f, 1f, completion, clamped: true));
		return Color.Lerp(val, bodyColor, completion);
	}

	public void RenderPixelatedPrimitives(SpriteBatch spriteBatch, GeneralDrawLayer layer)
	{
		GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(LaserWidthFunction, LaserColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: true, GameShaders.Misc["CalamityMod:ImpFlameTrail"]), base.Projectile.oldPos.Length * 2);
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(LaserCoreEnergyFunction, delegate
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.White;
		}, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: true, GameShaders.Misc["CalamityMod:ImpFlameTrail"]), base.Projectile.oldPos.Length * 2);
	}
}
