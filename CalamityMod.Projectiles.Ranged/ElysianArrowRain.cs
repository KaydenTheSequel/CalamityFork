using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ElysianArrowRain : ModProjectile, ILocalizedModType, IModType
{
	public static int MaxUpdate;

	private int Lifetime = 110;

	private static Color ShaderColorOne;

	private static Color ShaderColorTwo;

	private static Color ShaderEndColor;

	private Vector2 altSpawn;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 21;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 24);
		base.Projectile.arrow = true;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.MaxUpdates = MaxUpdate;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.ArmorPenetration = 15;
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.numHits < 1)
		{
			return null;
		}
		return false;
	}

	public override void AI()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] == 45f)
		{
			altSpawn = base.Projectile.Center;
		}
		if (base.Projectile.timeLeft <= 5)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(9f, 9f) - base.Projectile.velocity * 5f, Main.rand.NextBool() ? 262 : 87, base.Projectile.velocity * 30f * Main.rand.NextFloat(0.1f, 0.95f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.9f, 1.45f);
			dust.alpha = 235;
		}
		if (base.Projectile.timeLeft <= 80)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.96f;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 90);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.damage > 1 && base.Projectile.numHits < 1)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.8f);
		}
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 90);
		base.Projectile.timeLeft = 80;
	}

	private float PrimitiveWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		float arrowheadCutoff = 0.36f;
		float width = 24f;
		float minHeadWidth = 0.03f;
		float maxHeadWidth = width;
		if (completionRatio <= arrowheadCutoff)
		{
			width = MathHelper.Lerp(minHeadWidth, maxHeadWidth, Utils.GetLerpValue(0f, arrowheadCutoff, completionRatio, clamped: true));
		}
		return width;
	}

	private Color PrimitiveColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		float endFadeRatio = 0.41f;
		float completionRatioFactor = 2.7f;
		float globalTimeFactor = 5.3f;
		float endFadeFactor = 3.2f;
		float endFadeTerm = Utils.GetLerpValue(0f, endFadeRatio * 0.5f, completionRatio, clamped: true) * endFadeFactor;
		float startingInterpolant = (float)Math.Cos(completionRatio * completionRatioFactor - Main.GlobalTimeWrappedHourly * globalTimeFactor + endFadeTerm) * 0.5f + 0.5f;
		float colorLerpFactor = 0.6f;
		return Color.Lerp(Color.Lerp(ShaderColorOne, ShaderColorTwo, startingInterpolant * colorLerpFactor), ShaderEndColor, MathHelper.SmoothStep(0f, 1f, Utils.GetLerpValue(0f, endFadeRatio, completionRatio, clamped: true)));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SylvestaffStreak", (AssetRequestMode)2));
		Vector2 overallOffset = base.Projectile.Size * 0.5f;
		overallOffset += base.Projectile.velocity * 1.4f;
		int numPoints = 46;
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(PrimitiveWidthFunction, PrimitiveColorFunction, delegate
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return overallOffset;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), numPoints);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] != 0f)
		{
			return;
		}
		float num = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		NPC target = base.Projectile.Center.ClosestNPCAt(2000f);
		Vector2 targetPosition = ((base.Projectile.numHits != 1) ? altSpawn : (target?.Center ?? base.Projectile.Center));
		Vector2 spawnSpot = ((base.Projectile.numHits != 1) ? altSpawn : (target?.Center ?? base.Projectile.Center)) + new Vector2(Main.rand.NextFloat(-450f, 450f), Main.rand.NextFloat(750f, 950f));
		Vector2 velocity = ((target != null) ? CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(spawnSpot, target, 20f, MaxUpdate) : ((targetPosition - spawnSpot).SafeNormalize(Vector2.UnitX) * 20f));
		if (num < 1400f)
		{
			int Dusts = 8;
			float radians = (float)Math.PI * 2f / (float)Dusts;
			Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f));
			for (int i = 0; i < Dusts; i++)
			{
				Vector2 dustVelocity = spinningPoint.RotatedBy(radians * (float)i) * 3.5f;
				Dust.NewDustPerfect(spawnSpot, Main.rand.NextBool() ? 262 : 87, dustVelocity, 0, default(Color), 0.9f).noGravity = true;
				Dust.NewDustPerfect(spawnSpot, Main.rand.NextBool() ? 262 : 87, dustVelocity * 0.6f, 0, default(Color), 1.2f).noGravity = true;
			}
		}
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnSpot, velocity, ModContent.ProjectileType<ElysianArrowRain>(), base.Projectile.damage, 0f, base.Projectile.owner, 0f, 1f);
	}

	static ElysianArrowRain()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		MaxUpdate = 7;
		ShaderColorOne = Color.Khaki;
		ShaderColorTwo = Color.White;
		ShaderEndColor = Color.Orange;
	}
}
