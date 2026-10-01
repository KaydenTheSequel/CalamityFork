using System;
using CalamityMod.Enums;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class PhoenixsPrideFirewall : ModProjectile, ILocalizedModType, IModType, IPixelatedPrimitiveRenderer
{
	public const int MaxTornadoHeight = 270;

	public float Scale;

	public float fadeOut;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public NPC Target => Main.npc[(int)base.Projectile.ai[0]];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 408;
		base.Projectile.height = 102;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.scale = Scale;
		base.Projectile.timeLeft = 180;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = FourSeasonsGalaxia.PhoenixAttunement_FlamePillarLocalIFrames;
	}

	public override void AI()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		if (!Target.active)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.975f;
			if (base.Projectile.timeLeft > 15)
			{
				base.Projectile.timeLeft = 15;
			}
		}
		else
		{
			base.Projectile.velocity = (base.Projectile.velocity * 13f + (Target.Center - base.Projectile.Center).SafeNormalize(Vector2.Zero) * 24f) / 14f;
		}
		if (base.Projectile.timeLeft > 170)
		{
			fadeOut += 0.1f;
		}
		if (base.Projectile.timeLeft < 15)
		{
			fadeOut -= 0.05f;
		}
		if (base.Projectile.timeLeft % 3 == 0)
		{
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, Main.rand.NextVector2Circular(6f, 6f), Color.Orange, Color.DarkOrange, 1.1f, 170f));
			GeneralParticleHandler.SpawnParticle(new SquareParticle(base.Projectile.Center, -Vector2.UnitY.RotatedByRandom(1.5707963705062866) * 8f, affectedByGravity: true, 20, 0.9f, Color.White));
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		float _ = 0f;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), base.Projectile.Bottom, base.Projectile.Bottom - Vector2.UnitY * 270f, 72f, ref _);
	}

	public float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return MathF.Max(50f * Scale * (MathF.Pow(1f - completionRatio, 2f) * 2.75f), 0.35f);
	}

	public Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		return Color.Orange * ((completionRatio > 0.1f) ? 1f : (completionRatio * 10f));
	}

	public void RenderPixelatedPrimitives(SpriteBatch spritebatch, GeneralDrawLayer layer)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:Bordernado"].UseSaturation(-0.2f);
		GameShaders.Misc["CalamityMod:Bordernado"].UseOpacity(fadeOut);
		GameShaders.Misc["CalamityMod:Bordernado"].SetShaderTexture(ModContent.Request<Texture2D>("Terraria/Images/Misc/Perlin", (AssetRequestMode)2));
		Vector2[] drawPoints = (Vector2[])(object)new Vector2[7];
		Vector2 upwardAscent = Vector2.UnitY * 270f * fadeOut;
		Vector2 bottom = base.Projectile.Center;
		Vector2 top = bottom - upwardAscent;
		for (int i = 0; i < drawPoints.Length - 1; i++)
		{
			drawPoints[i] = Vector2.Lerp(top + Vector2.UnitX * (MathF.Sin((float)Main.GameUpdateCount * 0.15f + (float)i) * (float)i * 6f), bottom, (float)i / (float)(drawPoints.Length - 1));
		}
		drawPoints[^1] = bottom;
		PrimitiveRenderer.RenderTrail(drawPoints, new PrimitiveSettings(WidthFunction, ColorFunction, null, smoothen: true, pixelate: true, GameShaders.Misc["CalamityMod:Bordernado"]), 80);
	}
}
