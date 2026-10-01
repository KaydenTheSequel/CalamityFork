using System;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Healing;

public class ShinobiHealOrb : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Healing";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 30;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.MaxUpdates = 5;
		base.Projectile.timeLeft = 120 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		base.Projectile.HealingProjectile((int)base.Projectile.ai[1], base.Projectile.owner, 6f, 15f);
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(Color.Blue, Color.CornflowerBlue, completionRatio);
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return 10f * ((completionRatio < 0.2f) ? MathHelper.Clamp(1f - MathF.Pow(5f * completionRatio - 1f, 2f), 0f, 1f) : Utils.GetLerpValue(1f, 0.2f, completionRatio, clamped: true));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/BasicTrail", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 30);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 7; i++)
		{
			Vector2 velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.7853981852531433) * Main.rand.NextFloat(12f, 20f);
			float scale = Main.rand.NextFloat(0.4f, 1.2f);
			GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, velocity, Color.White, Color.DarkBlue, scale, 24, 0.1f, scale * 2f, Main.rand.NextFloat(0f, 0.01f)));
		}
	}
}
