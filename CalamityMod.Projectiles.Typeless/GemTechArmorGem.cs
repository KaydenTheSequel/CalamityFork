using System;
using CalamityMod.DataStructures;
using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class GemTechArmorGem : ModProjectile, ILocalizedModType, IModType
{
	public const int UpwardFlyTime = 24;

	public const int RedirectTime = 12;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public ref float Time => ref base.Projectile.ai[0];

	public ref float Variant => ref base.Projectile.ai[1];

	public override string Texture => "CalamityMod/Projectiles/Typeless/GemTechYellowGem";

	public Color GemColor
	{
		get
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			return GemTechArmorState.GetColorFromGemType((GemTechArmorGemType)Variant);
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 14);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 2;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 13;
		base.Projectile.timeLeft = base.Projectile.MaxUpdates * 420;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			for (int i = 0; i < 7; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 267);
				dust.velocity = -Vector2.UnitY.RotatedByRandom(0.8100000023841858) * Main.rand.NextFloat(1.25f, 4.5f);
				dust.color = Color.Lerp(GemColor, Color.White, Main.rand.NextFloat(0.5f));
				dust.scale = 1.1f;
				dust.alpha = 185;
				dust.noGravity = true;
			}
			base.Projectile.localAI[0] = 1f;
		}
		NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(2700f, ignoreTiles: true, bossPriority: true);
		if (base.Projectile.FinalExtraUpdate())
		{
			Time++;
		}
		if (Time < 24f)
		{
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, -Vector2.UnitY * 3f, 0.1f);
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
			return;
		}
		if (Time < 36f)
		{
			if (potentialTarget != null)
			{
				float angleToTarget = base.Projectile.AngleTo(potentialTarget.Center) + (float)Math.PI / 2f;
				base.Projectile.rotation = base.Projectile.rotation.AngleLerp(angleToTarget, 0.225f).AngleTowards(angleToTarget, 0.6f);
				base.Projectile.velocity = base.Projectile.velocity.MoveTowards(Vector2.Zero, 1.9f) * 0.9f;
			}
			return;
		}
		if (Time == 36f && base.Projectile.FinalExtraUpdate())
		{
			SoundEngine.PlaySound(in SoundID.Item72, base.Projectile.Center);
			for (int j = 0; j < 12; j++)
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 267);
				dust2.velocity = ((float)Math.PI * 2f * (float)j / 12f).ToRotationVector2() * 5f;
				dust2.color = GemColor;
				dust2.scale = 1.125f;
				dust2.alpha = 175;
				dust2.noGravity = true;
			}
		}
		if (potentialTarget != null && base.Projectile.penetrate >= base.Projectile.maxPenetrate)
		{
			float distanceFromTarget = base.Projectile.Distance(potentialTarget.Center);
			float moveInterpolant = Utils.GetLerpValue(0f, 100f, distanceFromTarget, clamped: true) * Utils.GetLerpValue(600f, 400f, distanceFromTarget, clamped: true);
			Vector2 targetCenterOffsetVec = potentialTarget.Center - base.Projectile.Center;
			float movementSpeed = MathHelper.Min(37.5f, ((Vector2)(ref targetCenterOffsetVec)).Length());
			Vector2 idealVelocity = targetCenterOffsetVec.SafeNormalize(Vector2.Zero) * movementSpeed;
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 4f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity += base.Projectile.velocity.RotatedBy(0.7853981852531433).SafeNormalize(Vector2.Zero) * 4f;
			}
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
		if (!(Time > 36f))
		{
			return false;
		}
		return null;
	}

	public Color TrailColor(float completionRatio, Vector2 vertexPos)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		float trailOpacity = Utils.GetLerpValue(0f, 0.067f, completionRatio, clamped: true) * Utils.GetLerpValue(0.7f, 0.58f, completionRatio, clamped: true);
		Color startingColor = Color.Lerp(Color.White, GemColor, 0.47f);
		Color middleColor = GemColor;
		Color endColor = Color.Transparent;
		return CalamityUtils.MulticolorLerp(completionRatio, startingColor, middleColor, endColor) * trailOpacity;
	}

	public static float TrailWidth(float completionRatio, Vector2 vertexPos)
	{
		return MathHelper.SmoothStep(12f, 4.25f, completionRatio);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		GameShaders.Misc["CalamityMod:ImpFlameTrail"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", (AssetRequestMode)2));
		float variant = Variant;
		if (variant == 0f)
		{
			goto IL_0059;
		}
		Texture2D texture;
		if (variant != 1f)
		{
			if (variant != 2f)
			{
				if (variant != 3f)
				{
					if (variant != 4f)
					{
						if (variant != 5f)
						{
							goto IL_0059;
						}
						texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/GemTechPinkGem", (AssetRequestMode)2).Value;
					}
					else
					{
						texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/GemTechRedGem", (AssetRequestMode)2).Value;
					}
				}
				else
				{
					texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/GemTechBlueGem", (AssetRequestMode)2).Value;
				}
			}
			else
			{
				texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/GemTechPurpleGem", (AssetRequestMode)2).Value;
			}
		}
		else
		{
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Typeless/GemTechGreenGem", (AssetRequestMode)2).Value;
		}
		goto IL_00ca;
		IL_0059:
		texture = TextureAssets.Projectile[base.Type].Value;
		goto IL_00ca;
		IL_00ca:
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 origin = texture.Size() * 0.5f;
		Main.EntitySpriteDraw(texture, drawPosition, null, base.Projectile.GetAlpha(Color.White), base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		if (base.Projectile.ai[0] > 36f)
		{
			PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(TrailWidth, TrailColor, delegate
			{
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_0010: Unknown result type (might be due to invalid IL or missing references)
				return base.Projectile.Size * 0.5f;
			}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:ImpFlameTrail"]), 71);
		}
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.timeLeft = ProjectileID.Sets.TrailCacheLength[base.Type];
		base.Projectile.netUpdate = true;
	}
}
