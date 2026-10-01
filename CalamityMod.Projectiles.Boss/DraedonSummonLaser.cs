using System;
using System.Collections.Generic;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class DraedonSummonLaser : ModProjectile, ILocalizedModType, IModType
{
	public const float LaserLength = 3800f;

	private const int Lifetime = 200;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 24);
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.hide = true;
		base.Projectile.timeLeft = 200;
	}

	public override bool? CanDamage()
	{
		return base.Projectile.timeLeft < 170;
	}

	public override void AI()
	{
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		Color newColor;
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundEngine.PlaySound(in TeslaCannon.FireSound, base.Projectile.Center);
			for (int i = 0; i < 36; i++)
			{
				Vector2 bottomRight = base.Projectile.BottomRight;
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(bottomRight, 267, null, 0, newColor);
				dust.color = CalamityUtils.MulticolorLerp((float)i / 36f, CalamityUtils.ExoPalette);
				dust.velocity = ((float)Math.PI * 2f * (float)i / 36f).ToRotationVector2() * new Vector2(3f, 1.45f) - Vector2.UnitY * 2f;
				dust.scale = 2.8f;
				dust.fadeIn = Main.rand.NextFloat(0.8f, 1.85f);
				dust.noGravity = true;
			}
			for (int j = 0; j < 10; j++)
			{
				Vector2 bottomRight2 = base.Projectile.BottomRight;
				newColor = default(Color);
				Dust dust2 = Dust.NewDustPerfect(bottomRight2, 267, null, 0, newColor);
				dust2.color = CalamityUtils.MulticolorLerp(Main.rand.NextFloat(), CalamityUtils.ExoPalette);
				dust2.velocity = Main.rand.NextVector2Circular(2f, 2f);
				dust2.scale = 4f;
				dust2.fadeIn = 2f;
				dust2.noGravity = true;
			}
			base.Projectile.localAI[0] = 1f;
		}
		Vector2 center = base.Projectile.Center;
		newColor = Color.White;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
		base.Projectile.scale = Utils.GetLerpValue(-1f, 15f, base.Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(201f, 185f, base.Projectile.timeLeft, clamped: true);
	}

	private float PrimitiveWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return Utils.GetLerpValue(1f, 0.96f, completionRatio, clamped: true) * Utils.GetLerpValue(0f, 0.016f, completionRatio, clamped: true) * base.Projectile.scale * 20f;
	}

	private Color PrimitiveColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.MulticolorLerp((Main.GlobalTimeWrappedHourly * 0.67f - completionRatio * 3f) % 1f, CalamityUtils.ExoPalette) * 1.2f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:Flame"].UseImage1("Images/Misc/Perlin");
		Vector2[] basePoints = (Vector2[])(object)new Vector2[8];
		for (int i = 0; i < basePoints.Length; i++)
		{
			basePoints[i] = base.Projectile.Center - Vector2.UnitY * (float)i / (float)basePoints.Length * 3800f;
		}
		Vector2 overallOffset = base.Projectile.Size * 0.5f;
		PrimitiveRenderer.RenderTrail(basePoints, new PrimitiveSettings(PrimitiveWidthFunction, PrimitiveColorFunction, delegate
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return overallOffset;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:Flame"]), 92);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Center, base.Projectile.Center - Vector2.UnitY * 3800f);
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindNPCsAndTiles.Add(index);
	}
}
