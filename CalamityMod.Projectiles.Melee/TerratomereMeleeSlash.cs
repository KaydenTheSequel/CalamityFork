using System.Collections.Generic;
using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TerratomereMeleeSlash : ModProjectile, ILocalizedModType, IModType
{
	public Vector2[] ControlPoints;

	public new string LocalizationCategory => "Projectiles.Melee";

	public bool Flipped => base.Projectile.ai[0] == 1f;

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 60;
		base.Projectile.height = 144;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 30;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.Opacity = Utils.GetLerpValue(0f, 26f, base.Projectile.timeLeft, clamped: true);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.91f;
		base.Projectile.scale *= 1.01f;
	}

	public float SlashWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return base.Projectile.scale * 22f;
	}

	public Color SlashColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lime * Utils.GetLerpValue(0.04f, 0.27f, completionRatio, clamped: true) * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.EnterShaderRegion();
		TerratomereHoldoutProj.PrepareSlashShader(Flipped);
		List<Vector2> points = new List<Vector2>();
		for (int i = 0; i < ControlPoints.Length; i++)
		{
			points.Add(ControlPoints[i] + ControlPoints[i].SafeNormalize(Vector2.Zero) * (base.Projectile.scale - 1f) * 40f);
		}
		PrimitiveRenderer.RenderTrail(points, new PrimitiveSettings(SlashWidthFunction, SlashColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ExobladeSlash"]), 65);
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		return base.Projectile.RotatingHitboxCollision(targetHitbox);
	}
}
