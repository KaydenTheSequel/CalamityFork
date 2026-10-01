using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class MythrilFlare : ModProjectile, ILocalizedModType, IModType
{
	public const int AttackDelay = 22;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public ref float Time => ref base.Projectile.ai[0];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 15;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = base.Projectile.MaxUpdates * 210;
	}

	public override void AI()
	{
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			for (int i = 0; i < 15; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267);
				dust.velocity = -Vector2.UnitY.RotatedByRandom(0.8100000023841858) * Main.rand.NextFloat(1.25f, 6f);
				dust.color = Color.Lerp(new Color(104, 183, 136), Color.White, Main.rand.NextFloat(0.5f));
				dust.scale = 1.1f;
				dust.alpha = 185;
				dust.noGravity = true;
			}
			base.Projectile.localAI[0] = 1f;
		}
		NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(176f);
		if (base.Projectile.FinalExtraUpdate())
		{
			Time++;
		}
		if (Time < 22f)
		{
			base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedBy(0.39269909262657166) * 5f;
		}
		if (potentialTarget != null)
		{
			float distanceFromTarget = base.Projectile.Distance(potentialTarget.Center);
			float moveInterpolant = Utils.GetLerpValue(0f, 100f, distanceFromTarget, clamped: true) * Utils.GetLerpValue(600f, 400f, distanceFromTarget, clamped: true);
			Vector2 targetCenterOffsetVec = potentialTarget.Center - base.Projectile.Center;
			float movementSpeed = MathHelper.Min(17.5f, ((Vector2)(ref targetCenterOffsetVec)).Length());
			Vector2 idealVelocity = targetCenterOffsetVec.SafeNormalize(Vector2.Zero) * movementSpeed;
			if (base.Projectile.velocity.HasNaNs())
			{
				base.Projectile.Kill();
			}
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, idealVelocity, moveInterpolant * 0.08f);
			base.Projectile.velocity = base.Projectile.velocity.MoveTowards(idealVelocity, 2f);
		}
	}

	public override bool? CanDamage()
	{
		if (!(Time >= 22f))
		{
			return false;
		}
		return null;
	}

	public Color TrailColor(float completionRatio, Vector2 vertexPos)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		float trailOpacity = Utils.GetLerpValue(0f, 0.13f, completionRatio, clamped: true) * Utils.GetLerpValue(0.7f, 0.58f, completionRatio, clamped: true);
		Color startingColor = Color.Lerp(Color.White, Color.LightGreen, 0.47f);
		Color middleColor = Color.LightGreen;
		Color endColor = Color.Transparent;
		return CalamityUtils.MulticolorLerp(completionRatio, startingColor, middleColor, endColor) * trailOpacity;
	}

	public float TrailWidth(float completionRatio, Vector2 vertexPos)
	{
		return MathHelper.SmoothStep((float)base.Projectile.width, 4.25f, completionRatio);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/EternityStreak", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(TrailWidth, TrailColor, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ImpFlameTrail"]), 74);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 15; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267);
			dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitY).RotatedByRandom(0.9700000286102295) * Main.rand.NextFloat(1f, 8f);
			dust.color = Color.Lerp(new Color(104, 183, 136), Color.White, Main.rand.NextFloat(0.5f));
			dust.scale = 1.1f;
			dust.alpha = 185;
			dust.noGravity = true;
		}
	}
}
