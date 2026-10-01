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

namespace CalamityMod.Projectiles.Summon;

public class CosmilampBeam : ModProjectile, ILocalizedModType, IModType
{
	public const int SlowdownTime = 45;

	public const int Lifetime = 120;

	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float Timer => ref base.Projectile.ai[0];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 32;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.penetrate = 3;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.timeLeft = base.Projectile.MaxUpdates * 120;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 15;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.2f, 0.01f, 0.1f);
		base.Projectile.Opacity = Utils.GetLerpValue(0f, (float)base.Projectile.MaxUpdates * 10f, base.Projectile.timeLeft, clamped: true);
		Lighting.AddLight(base.Projectile.Center, Vector3.One * base.Projectile.Opacity * 0.7f);
		NPC potentialTarget = base.Projectile.Center.MinionHoming(1360f, Main.player[base.Projectile.owner]);
		if (Timer < 45f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.995f;
		}
		else if (potentialTarget != null)
		{
			Vector2 idealVelocity = base.Projectile.SafeDirectionTo(potentialTarget.Center) * 17f;
			if (!base.Projectile.WithinRange(potentialTarget.Center, 160f))
			{
				base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, idealVelocity, 0.036f);
				base.Projectile.velocity = base.Projectile.velocity.ToRotation().AngleTowards(idealVelocity.ToRotation(), 0.12f).ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length();
			}
			else
			{
				Vector2 center = potentialTarget.Size;
				float angularTurnSpeed = (float)Math.PI * ((Vector2)(ref center)).Length() / 12000f;
				if (angularTurnSpeed > (float)Math.PI / 20f)
				{
					angularTurnSpeed = (float)Math.PI / 20f;
				}
				Projectile projectile2 = base.Projectile;
				Vector2 velocity = base.Projectile.velocity;
				double radians = angularTurnSpeed;
				center = default(Vector2);
				projectile2.velocity = velocity.RotatedBy(radians, center);
			}
		}
		if (base.Projectile.FinalExtraUpdate())
		{
			Timer++;
		}
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		float streakOpacity = Utils.GetLerpValue(0.8f, 0.54f, completionRatio, clamped: true) * base.Projectile.Opacity;
		Color endColor = Color.Lerp(Color.Fuchsia, Color.DarkViolet, (float)Math.Sin(completionRatio * (float)Math.PI * 1.6f - Main.GlobalTimeWrappedHourly * 4f) * 0.5f + 0.5f);
		endColor = CalamityUtils.MulticolorLerp(completionRatio * completionRatio, endColor, Color.MediumPurple, Color.Red);
		if (base.Projectile.localAI[0] == 1f)
		{
			endColor = Color.Lerp(endColor, Color.White, 0.5f) * streakOpacity;
		}
		return Color.Lerp(endColor, Color.Black, 0.3f) * streakOpacity;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		float expansionCompletion = 1f - (float)Math.Pow(1f - Utils.GetLerpValue(0f, 0.3f, completionRatio, clamped: true), 2.0);
		float maxWidth = base.Projectile.Opacity * (float)base.Projectile.width * 1.65f;
		if (base.Projectile.localAI[0] == 1f)
		{
			maxWidth *= 0.4f;
		}
		return MathHelper.Lerp(0f, maxWidth, expansionCompletion);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Nightwither>(), 240);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		base.Projectile.localAI[0] = 0f;
		GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
		PrimitiveSettings settings = new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ImpFlameTrail"]);
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, settings, 42);
		base.Projectile.localAI[0] = 1f;
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, settings, 42);
		return false;
	}
}
