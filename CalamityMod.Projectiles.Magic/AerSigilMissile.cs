using System;
using System.Linq;
using CalamityMod.Dusts;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using CalamityMod.Projectiles.DraedonsArsenal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class AerSigilMissile : ModProjectile, ILocalizedModType, IModType
{
	public static float MaxWidth = 32f;

	private int wingAnimationTimer;

	private const int wingAnimationDuration = 80;

	public static Asset<Texture2D> BloomTex;

	public static Asset<Texture2D> SlashTex;

	public static Asset<Texture2D> TrailTex;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 12;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = true;
		base.Projectile.penetrate = 1;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 420;
		base.Projectile.usesLocalNPCImmunity = true;
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			base.Projectile.oldPos[i] = base.Projectile.position;
		}
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = base.Projectile.Center;
		Color lightGoldenrodYellow = Color.LightGoldenrodYellow;
		Lighting.AddLight(center, ((Color)(ref lightGoldenrodYellow)).ToVector3() * 0.4f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (base.Projectile.FinalExtraUpdate())
		{
			Time++;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			for (int i = 0; i < base.Projectile.oldPos.Length; i++)
			{
				base.Projectile.oldPos[i] = base.Projectile.position;
			}
			base.Projectile.localAI[0] = 1f;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 8; i++)
		{
			float variance = Main.rand.NextFloat(-0.6f, 0.6f);
			int dustStyle = ModContent.DustType<SquashDust>();
			Dust dust = Dust.NewDustPerfect(target.Center, dustStyle);
			dust.scale = Main.rand.NextFloat(1.2f, 1.9f) - Math.Abs(variance);
			dust.velocity = (base.Projectile.velocity * 1.5f).RotatedBy(variance) * Main.rand.NextFloat(1f, 1.6f) * (1f - Math.Abs(variance));
			dust.noGravity = true;
			dust.color = Color.Lerp(Color.LightGoldenrodYellow, Color.Orange, Main.rand.NextFloat(0f, 1f));
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/UnstableCastersGauntlet/AerSigilGust");
		style.Volume = 0.6f;
		style.PitchVariance = 0.1f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		_ = base.Projectile.Center - Main.screenPosition + Utils.RotatedBy(new Vector2(0f, -16f), (double)(base.Projectile.rotation - (float)Math.PI / 2f), default(Vector2));
		Vector2 vel = default(Vector2);
		for (int i = 0; i < 11; i++)
		{
			float randomAngle = Main.rand.NextFloat((float)Math.PI * 2f);
			float randomSpeed = Main.rand.NextFloat(6f, 10f);
			((Vector2)(ref vel))._002Ector((float)Math.Cos(randomAngle) * randomSpeed, (float)Math.Sin(randomAngle) * randomSpeed);
			vel.X *= 3f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, vel, ModContent.ProjectileType<AerSigilFeather>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<AerSigilBlast>(), (int)((float)base.Projectile.damage * 4.25f), base.Projectile.knockBack * 6f, base.Projectile.owner);
			projectile.ai[1] = 600f;
			projectile.localAI[1] = Main.rand.NextFloat(0.1f, 0.2f);
			projectile.netUpdate = true;
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Goldenrod * 0.75f, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.1f, 1.5f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White * 0.75f, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 0.1f, 1.25f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		Color val = Color.Lerp(Color.LightGoldenrodYellow, Color.Orange, 0.5f);
		((Color)(ref val)).A = 0;
		return val * base.Projectile.Opacity;
	}

	public float TrailWidth(float completionRatio, Vector2 vertexPos)
	{
		return Utils.GetLerpValue(1f, 0.4f, completionRatio, clamped: true) * (float)Math.Sin(Math.Acos(1f - Utils.GetLerpValue(0f, 0.08f, completionRatio, clamped: true))) * Utils.GetLerpValue(0f, 0.1f, (float)base.Projectile.timeLeft / 600f, clamped: true) * (MaxWidth * 0.265f);
	}

	public Color TrailColor(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(Color.LightGoldenrodYellow, Color.Orange, completionRatio) * 0.2f;
	}

	public float MiniTrailWidth(float completionRatio, Vector2 vertexPos)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return TrailWidth(completionRatio, vertexPos) * 5.5f;
	}

	public Color MiniTrailColor(float completionRatio, Vector2 vertexPos)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(Color.LightGoldenrodYellow, Color.Orange, completionRatio);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		Color mainColor = Color.Lerp(Color.LightGoldenrodYellow, Color.Orange, ((float)Main.timeForVisualEffects * 0.5f + (float)base.Projectile.whoAmI * 0.12f) % 1f);
		Color secondaryColor = Color.Lerp(Color.LightGoldenrodYellow, Color.Orange, ((float)Main.timeForVisualEffects * 0.5f + (float)base.Projectile.whoAmI * 0.12f + 0.2f) % 1f);
		Vector2 adjustedCenter = base.Projectile.Center - Main.screenPosition + Utils.RotatedBy(new Vector2(0f, -16f), (double)(base.Projectile.rotation - (float)Math.PI / 2f), default(Vector2));
		wingAnimationTimer++;
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/SimpleWings", (AssetRequestMode)2).Value;
		Vector2 origin = value.Size() * 0.5f;
		float stretchFactorY = 0f;
		if (wingAnimationTimer <= 80)
		{
			float progress = (float)wingAnimationTimer / 80f;
			if (progress < 0.25f)
			{
				float stretchProgress = progress / 0.25f;
				stretchFactorY = MathHelper.Lerp(-1.4f, 0.5f, (float)Math.Sin(stretchProgress * ((float)Math.PI / 2f)));
			}
			else if (progress < 0.5f)
			{
				float squashProgress = (progress - 0.25f) / 0.3f;
				stretchFactorY = MathHelper.Lerp(0.5f, -0.25f, squashProgress * squashProgress);
			}
			else if (progress < 0.8f)
			{
				float stretchProgress2 = (progress - 0.6f) / 0.2f;
				stretchFactorY = MathHelper.Lerp(-0.25f, 0f, stretchProgress2 * stretchProgress2);
			}
			else
			{
				stretchFactorY = 0f;
			}
		}
		float baseScale = 1f;
		float stretchScaleY = 1f + stretchFactorY;
		float stretchScaleX = 1f - stretchFactorY * 0.5f;
		Vector2 finalScale = new Vector2(baseScale * stretchScaleX, baseScale * stretchScaleY) * 0.6f;
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive);
		Color val = Color.LightGoldenrodYellow;
		((Color)(ref val)).A = 0;
		Color glowColor = val * 0.8f;
		Main.EntitySpriteDraw(value, adjustedCenter, null, glowColor, base.Projectile.rotation + (float)Math.PI / 2f, origin, finalScale * 1.5f, (SpriteEffects)0);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend);
		Main.spriteBatch.EnterShaderRegion();
		if (TrailTex == null)
		{
			TrailTex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/BasicTrail", (AssetRequestMode)2);
		}
		GameShaders.Misc["CalamityMod:ExobladePierce"].SetShaderTexture(TrailTex);
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseImage2("Images/Extra_189");
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseColor(mainColor);
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseSecondaryColor(secondaryColor);
		GameShaders.Misc["CalamityMod:ExobladePierce"].Apply();
		Vector2 offset = base.Projectile.Size * 0.5f;
		Vector2[] positions = base.Projectile.oldPos.Select(delegate(Vector2 p)
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			return p - offset;
		}).ToArray();
		PrimitiveRenderer.RenderTrail(positions, new PrimitiveSettings(TrailWidth, TrailColor, delegate
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 1f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ExobladePierce"]), 30);
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseColor(mainColor);
		GameShaders.Misc["CalamityMod:ExobladePierce"].UseSecondaryColor(secondaryColor);
		PrimitiveRenderer.RenderTrail(positions, new PrimitiveSettings(MiniTrailWidth, MiniTrailColor, delegate
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 1f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ExobladePierce"]), 30);
		Main.spriteBatch.ExitShaderRegion();
		Color drawColor = Color.LightGoldenrodYellow * 0.5f;
		Main.EntitySpriteDraw(value, adjustedCenter, null, drawColor, base.Projectile.rotation + (float)Math.PI / 2f, origin, finalScale, (SpriteEffects)0);
		if (BloomTex == null)
		{
			BloomTex = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		}
		Texture2D bloomTex = BloomTex.Value;
		val = Color.White * 4f;
		((Color)(ref val)).A = 0;
		Main.EntitySpriteDraw(bloomTex, adjustedCenter, null, val, 0f, bloomTex.Size() / 2f, 0.45f * base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
