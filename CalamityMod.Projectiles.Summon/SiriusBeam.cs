using System;
using System.Collections.Generic;
using System.Linq;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Utilities.Daybreak;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SiriusBeam : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 14;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.stopsDealingDamageAfterPenetrateHits = true;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
		base.Projectile.penetrate = 2;
	}

	public override void OnSpawn(IEntitySource source)
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		NPC target = base.Projectile.Center.MinionHoming(5000f, Main.player[base.Projectile.owner]);
		if (target != null && base.Projectile.localNPCImmunity[target.whoAmI] <= 0)
		{
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, (target.Center - base.Projectile.Center).SafeNormalize(Vector2.Zero) * ((base.Projectile.timeLeft < 300) ? 24f : 10f), 0.05f);
			base.Projectile.netUpdate = true;
			base.Projectile.Calamity().HomingTarget = target.whoAmI;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (base.Projectile.damage <= 0)
		{
			base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 4f;
			base.Projectile.timeLeft = (int)MathHelper.Min(8f, (float)base.Projectile.timeLeft);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 12; i++)
		{
			Vector2 dustVelocity = Main.rand.NextVector2Circular(1f, 1f) * 6f;
			float dustScale = Main.rand.NextFloat(3f, 5f);
			Color dustColor = Color.Lerp(Color.Yellow, Color.Gold, Main.rand.NextFloat(0.5f, 1f));
			Dust dust = Dust.NewDustDirect(base.Projectile.Center, 1, 1, 43, dustVelocity.X, dustVelocity.Y, 0, dustColor, dustScale);
			dust.noGravity = true;
			dust.noLight = false;
			dust.noLightEmittence = false;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.End(out var ss);
		GraphicsDevice device = ((Game)Main.instance).GraphicsDevice;
		using RenderTargetLease lease = RenderTargetPool.Shared.Rent(device, Main.screenWidth / 2, Main.screenHeight / 2, RenderTargetDescriptor.Default);
		using (lease.Scope(preserveContents: true, Color.Transparent))
		{
			GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
			PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(FireWidthFunction, FireColorFunction, delegate
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_0010: Unknown result type (might be due to invalid IL or missing references)
				return base.Projectile.Size * 0.5f;
			}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ImpFlameTrail"], useUnscaledMatrices: true), base.Projectile.oldPos.Length + 32);
			Vector2[] fireCoreLength = base.Projectile.oldPos.Take(8).ToArray();
			GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
			PrimitiveRenderer.RenderTrail(fireCoreLength, new PrimitiveSettings(FireCoreWidthFunction, FireCoreColorFunction, delegate
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_0010: Unknown result type (might be due to invalid IL or missing references)
				return base.Projectile.Size * 0.5f;
			}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ImpFlameTrail"], useUnscaledMatrices: true), fireCoreLength.Length + 24);
		}
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		Main.spriteBatch.Draw((Texture2D)(object)lease.Target, Vector2.Zero, (Rectangle?)null, Color.White, 0f, Vector2.Zero, 2f, (SpriteEffects)0, 0f);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin(in ss);
		return false;
	}

	public float FireWidthFunction(float completion, Vector2 pos)
	{
		float maxBodyWidth = 38f * base.Projectile.scale;
		float curveRatio = 0.2f;
		List<Vector2> positions = base.Projectile.oldPos.ToList();
		positions.RemoveAll(delegate(Vector2 x)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return x == Vector2.Zero;
		});
		float width = ((!(completion < curveRatio)) ? Utils.Remap(completion, curveRatio, 1f, maxBodyWidth, 0f) : (MathF.Pow(completion / curveRatio, 0.5f) * maxBodyWidth));
		float pulseInterpolant = MathF.Cos((float)Math.PI * completion - Main.GlobalTimeWrappedHourly * 20f) * 0.5f + 0.5f;
		float additionalPulseWidth = MathHelper.Lerp(0f, 12f, pulseInterpolant);
		return (width + additionalPulseWidth) * (float)positions.Count() / (float)ProjectileID.Sets.TrailCacheLength[base.Type];
	}

	public Color FireColorFunction(float completion, Vector2 pos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		Color val = Color.DarkSlateBlue * 1.3f;
		Color endColor = Color.Lerp(val, Color.Transparent, Utils.GetLerpValue(0.8f, 1f, completion, clamped: true));
		return Color.Lerp(val, endColor, completion) * base.Projectile.Opacity;
	}

	public float FireCoreWidthFunction(float completion, Vector2 pos)
	{
		float maxBodyWidth = base.Projectile.scale * 16f;
		float curveRatio = 0.25f;
		List<Vector2> positions = base.Projectile.oldPos.ToList();
		positions.RemoveAll(delegate(Vector2 x)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return x == Vector2.Zero;
		});
		float width = ((!(completion < curveRatio)) ? Utils.Remap(completion, curveRatio, 1f, maxBodyWidth, 0f) : (MathF.Sin(completion / curveRatio * ((float)Math.PI / 2f)) * maxBodyWidth + curveRatio));
		return width * (float)positions.Count() / (float)ProjectileID.Sets.TrailCacheLength[base.Type];
	}

	public Color FireCoreColorFunction(float completion, Vector2 pos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		Color skyBlue = Color.SkyBlue;
		Color tipColor = Color.Lerp(skyBlue, Color.Transparent, Utils.GetLerpValue(0.8f, 1f, completion, clamped: true));
		return Color.Lerp(Color.Lerp(skyBlue, tipColor, completion), Color.White, 0.175f) * base.Projectile.Opacity;
	}
}
