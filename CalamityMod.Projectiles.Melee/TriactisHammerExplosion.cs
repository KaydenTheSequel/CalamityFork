using System;
using System.Collections.Generic;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TriactisHammerExplosion : ModProjectile, ILocalizedModType, IModType
{
	public List<List<Vector2>> crackTrails = new List<List<Vector2>>();

	public static int crackCount = 15;

	public static int totalPoints = 12;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float Timer => ref base.Projectile.ai[0];

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 2);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 20;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		if (Timer == 0f)
		{
			float rotation = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, TriactisHammerFlare.GetColor(1f) * 0.7f, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, rotation, 0f, 0.8f, 30, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, TriactisHammerFlare.GetColor(2f) * 0.7f, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, rotation + MathHelper.ToRadians(120f), 0f, 0.6f, 27, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, TriactisHammerFlare.GetColor(3f) * 0.7f, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, rotation + MathHelper.ToRadians(240f), 0f, 0.4f, 24, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			Owner.SetScreenshake(40f);
		}
		if (Timer % 4f == 0f)
		{
			crackTrails.Clear();
			for (int i = 0; i < crackCount; i++)
			{
				List<Vector2> points = new List<Vector2>();
				for (int j = 0; j < totalPoints; j++)
				{
					_ = (float)Math.PI * 2f / (float)crackCount;
					if (j == 0)
					{
						points.Add(base.Projectile.Center + Main.rand.NextVector2Circular(30f, 30f));
						continue;
					}
					Vector2 newPoint = default(Vector2);
					Vector2 jtolookfor = ((j > 1) ? points[j - 2] : base.Projectile.Center);
					float baseDist = ((j == totalPoints - 1) ? 20 : (Main.rand.Next(60, 120) * (1 + (20 - base.Projectile.timeLeft) / 15)));
					newPoint = points[j - 1] + (jtolookfor.DirectionTo(points[j - 1]) * baseDist).RotatedByRandom(1.5707963705062866);
					points.Add(newPoint);
				}
				crackTrails.Add(points);
			}
		}
		Timer++;
		for (int l = 0; l < 3; l++)
		{
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, Vector2.One.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(18f, 60f), affectedByGravity: true, 15, Main.rand.NextFloat(0.02f, 0.05f), TriactisHammerFlare.GetColor(1f + (float)Main.rand.Next(3)), new Vector2(3f, 1f), quickShrink: true));
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 640f, targetHitbox);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		modifiers.HitDirectionOverride = (Owner.Center.X < target.Center.X).ToDirectionInt();
		modifiers.SourceDamage *= Utils.Remap(base.Projectile.numHits, 0f, 10f, 1f, 0.1f);
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return MathHelper.Clamp(CalamityUtils.Convert01To010(completionRatio * 2f), 0.2f, 1f) * 4f;
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}

	internal float BackgroundWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return WidthFunction(completionRatio, vertexPos) * 2f;
	}

	internal Color BackgroundColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		return ColorFunction(completionRatio, vertexPos) * 0.5f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		if (crackTrails.Count <= 0)
		{
			return false;
		}
		Main.spriteBatch.EnterShaderRegion(BlendState.Additive);
		GameShaders.Misc["CalamityMod:TeslaTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ZapTrail", (AssetRequestMode)2));
		foreach (List<Vector2> crackTrail in crackTrails)
		{
			PrimitiveRenderer.RenderTrail(crackTrail, new PrimitiveSettings(WidthFunction, ColorFunction, null, smoothen: false, pixelate: false, GameShaders.Misc["CalamityMod:TeslaTrail"]), 60);
			PrimitiveRenderer.RenderTrail(crackTrail, new PrimitiveSettings(BackgroundWidthFunction, BackgroundColorFunction, null, smoothen: false, pixelate: false, GameShaders.Misc["CalamityMod:TeslaTrail"]), 60);
		}
		Main.spriteBatch.ExitShaderRegion();
		return false;
	}
}
