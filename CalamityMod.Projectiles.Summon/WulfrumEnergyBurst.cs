using System;
using CalamityMod.Graphics.Primitives;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class WulfrumEnergyBurst : ModProjectile, ILocalizedModType, IModType
{
	public static float MaxDeviationAngle = (float)Math.PI / 4f;

	public static float HomingRange = 350f;

	public static float HomingAngle = (float)Math.PI / 4f;

	internal Color PrimColorMult;

	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float OriginalRotation => ref base.Projectile.ai[0];

	public NPC Target
	{
		get
		{
			if (base.Projectile.ai[1] < 0f || base.Projectile.ai[1] > (float)Main.maxNPCs)
			{
				return null;
			}
			return Main.npc[(int)base.Projectile.ai[1]];
		}
		set
		{
			if (value == null)
			{
				base.Projectile.ai[1] = -1f;
			}
			else
			{
				base.Projectile.ai[1] = value.whoAmI;
			}
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 8);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.timeLeft = 140;
		base.Projectile.MaxUpdates = 3;
	}

	public NPC FindTarget()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		float bestScore = 0f;
		NPC bestTarget = null;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC potentialTarget = enumerator.Current;
			if (!potentialTarget.CanBeChasedBy())
			{
				continue;
			}
			float distance = potentialTarget.Distance(base.Projectile.Center);
			float angle = base.Projectile.velocity.AngleBetween(potentialTarget.Center - base.Projectile.Center);
			float extraDistance = potentialTarget.width / 2 + potentialTarget.height / 2;
			if (distance - extraDistance < HomingRange && angle < HomingAngle / 2f && (Collision.CanHit(base.Projectile.Center, 1, 1, potentialTarget.Center, 1, 1) || !(extraDistance < distance)))
			{
				float attemptedScore = EvaluatePotentialTarget(distance - extraDistance, angle / 2f);
				if (attemptedScore > bestScore)
				{
					bestTarget = potentialTarget;
					bestScore = attemptedScore;
				}
			}
		}
		return bestTarget;
	}

	public float EvaluatePotentialTarget(float distance, float angle)
	{
		return 1f - distance / HomingRange * 0.5f + (1f - Math.Abs(angle) / (HomingAngle / 2f)) * 0.5f;
	}

	public override void AI()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft == 140)
		{
			if (OriginalRotation == 0f)
			{
				OriginalRotation = base.Projectile.velocity.ToRotation();
				base.Projectile.rotation = OriginalRotation;
			}
			Target = null;
		}
		else
		{
			Target = FindTarget();
		}
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.GreenYellow * 0.8f;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.5f);
		Vector2 center2;
		if (Target != null)
		{
			center2 = Target.Center - base.Projectile.Center;
			float distanceFromTarget = ((Vector2)(ref center2)).Length();
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards((Target.Center - base.Projectile.Center).ToRotation(), 0.07f * (float)Math.Pow(1f - distanceFromTarget / HomingRange, 2.0));
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 1.01f;
		base.Projectile.velocity = base.Projectile.rotation.ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length();
		if (base.Projectile.timeLeft == 140)
		{
			Vector2 dustCenter = base.Projectile.Center + base.Projectile.velocity * 1f;
			for (int i = 0; i < 5; i++)
			{
				Vector2? velocity = base.Projectile.velocity.RotatedByRandom(0.7853981852531433) * Main.rand.NextFloat(0.2f, 0.5f);
				float scale = Main.rand.NextFloat(1f, 1.2f);
				newColor = default(Color);
				Dust.NewDustPerfect(dustCenter, 178, velocity, 0, newColor, scale).noGravity = true;
			}
		}
		if (base.Projectile.timeLeft <= 137)
		{
			if (Main.rand.NextBool(4))
			{
				Vector2 center3 = base.Projectile.Center;
				Vector2 velocity2 = base.Projectile.velocity;
				center2 = default(Vector2);
				Vector2 position = center3 + velocity2.RotatedBy(1.5707963705062866, center2).SafeNormalize(Vector2.Zero) * Main.rand.NextFloat(-3f, 3f);
				Vector2? velocity3 = -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.5f);
				float scale = Main.rand.NextFloat(0.6f, 1.15f);
				newColor = default(Color);
				Dust.NewDustPerfect(position, 178, velocity3, 0, newColor, scale).noGravity = true;
			}
			if (Main.rand.NextBool(4))
			{
				Vector2 center4 = base.Projectile.Center;
				Vector2 velocity4 = base.Projectile.velocity;
				center2 = default(Vector2);
				Vector2 position2 = center4 + velocity4.RotatedBy(1.5707963705062866, center2).SafeNormalize(Vector2.Zero) * Main.rand.NextFloat(-3f, 3f);
				Vector2? velocity5 = -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 0.4f);
				float scale = Main.rand.NextFloat(0.4f, 1f);
				newColor = default(Color);
				Dust dust = Dust.NewDustPerfect(position2, 178, velocity5, 0, newColor, scale);
				dust.noGravity = true;
				dust.noLight = true;
			}
		}
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		float fadeOpacity = (float)Math.Sqrt(1f - completionRatio);
		return Color.Chartreuse.MultiplyRGB(PrimColorMult) * fadeOpacity;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return 9.4f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/ZapTrail", (AssetRequestMode)2));
		CalamityUtils.DrawChromaticAberration(Vector2.UnitX, 1.5f, delegate(Vector2 offset, Color colorMod)
		{
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			PrimColorMult = colorMod;
			PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
			{
				//IL_000b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0015: Unknown result type (might be due to invalid IL or missing references)
				//IL_001b: Unknown result type (might be due to invalid IL or missing references)
				//IL_0020: Unknown result type (might be due to invalid IL or missing references)
				return base.Projectile.Size * 0.5f + offset;
			}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 30);
		});
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		float stretchy = MathHelper.Clamp((((Vector2)(ref base.Projectile.velocity)).Length() - 6f) / 16f, 0f, 1f);
		Vector2 scale = default(Vector2);
		((Vector2)(ref scale))._002Ector(1f + stretchy * -0.2f, stretchy * 0.5f + 1f);
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation + (float)Math.PI / 2f, tex.Size() / 2f, base.Projectile.scale * scale, (SpriteEffects)0);
		return false;
	}

	public WulfrumEnergyBurst()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		PrimColorMult = Color.White;
		base._002Ector();
	}
}
