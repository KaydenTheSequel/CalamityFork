using System;
using System.Collections.Generic;
using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class DyingSun : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public ref float Time => ref base.Projectile.ai[0];

	public ref float Radius => ref base.Projectile.ai[1];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 164;
		base.Projectile.height = 164;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 100;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.scale = 1f;
	}

	public override void AI()
	{
		base.Projectile.scale += 0.08f;
		Radius = base.Projectile.scale * 42f;
		base.Projectile.Opacity = Utils.GetLerpValue(8f, 42f, base.Projectile.timeLeft, clamped: true);
		Time++;
	}

	public float SunWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return Radius * base.Projectile.scale * (float)Math.Sin((float)Math.PI * completionRatio);
	}

	public Color SunColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(Main.zenithWorld ? (new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB) * 0.8f) : (Main.IsItDay() ? Color.Yellow : Color.Cyan), Color.White, (float)Math.Sin((float)Math.PI * completionRatio) * 0.5f + 0.3f) * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:Flame"].UseSaturation(0.45f);
		GameShaders.Misc["CalamityMod:Flame"].UseImage1("Images/Misc/Perlin");
		List<float> rotationPoints = new List<float>();
		List<Vector2> drawPoints = new List<Vector2>();
		for (float offsetAngle = -(float)Math.PI / 2f; offsetAngle <= (float)Math.PI / 2f; offsetAngle += (float)Math.PI / 80f)
		{
			rotationPoints.Clear();
			drawPoints.Clear();
			float adjustedAngle = offsetAngle + -(float)Math.PI / 5f;
			Vector2 offsetDirection = adjustedAngle.ToRotationVector2();
			for (int i = 0; i < 16; i++)
			{
				rotationPoints.Add(adjustedAngle);
				drawPoints.Add(Vector2.Lerp(base.Projectile.Center - offsetDirection * Radius / 2f, base.Projectile.Center + offsetDirection * Radius / 2f, (float)i / 16f));
			}
			PrimitiveRenderer.RenderTrail(drawPoints, new PrimitiveSettings(SunWidthFunction, SunColorFunction, null, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:Flame"]), 24);
		}
		return false;
	}
}
