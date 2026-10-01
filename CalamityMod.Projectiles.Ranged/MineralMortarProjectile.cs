using System;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class MineralMortarProjectile : ModProjectile, ILocalizedModType, IModType
{
	public Color FrontTrailColor;

	public Color BackTrailColor;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public ref float RocketType => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 12;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.width = (base.Projectile.height = 42);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.Y < 25f)
		{
			base.Projectile.velocity.Y += 0.3f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (!Main.dedServ)
		{
			bool randomDust = Main.rand.NextBool();
			Vector2 position = base.Projectile.position;
			int width = base.Projectile.width;
			int height = base.Projectile.height;
			int type = (randomDust ? 109 : 6);
			float speedX = Main.rand.NextFloat(3f);
			float speedY = Main.rand.NextFloat(3f);
			float scale = (randomDust ? Main.rand.NextFloat(1f, 1.5f) : Main.rand.NextFloat(1.5f, 2.5f));
			Dust dust = Dust.NewDustDirect(position, width, height, type, speedX, speedY, 0, default(Color), scale);
			dust.noGravity = true;
			dust.noLight = true;
			dust.noLightEmittence = true;
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, Main.rand.NextVector2Circular(5f, 5f), new Color(255, 100, 0, 100), Color.Transparent, Main.rand.NextFloat(0.3f, 1f), Main.rand.NextFloat(300f, 500f)));
		}
		if (base.Projectile.timeLeft % 60 == 0)
		{
			base.Projectile.ForceNetUpdate(ignoreCurrentNetSpam: false);
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		float rocketType = RocketType;
		if (rocketType != 4459f)
		{
			if (rocketType != 4447f)
			{
				if (rocketType != 4448f)
				{
					if (rocketType == 4449f)
					{
						FrontTrailColor = Color.Yellow;
						BackTrailColor = Color.Orange;
					}
					else
					{
						FrontTrailColor = Color.Orange;
						BackTrailColor = Color.DarkOrange;
					}
				}
				else
				{
					FrontTrailColor = Color.Red;
					BackTrailColor = Color.DarkRed;
				}
			}
			else
			{
				FrontTrailColor = Color.DarkCyan;
				BackTrailColor = Color.Blue;
			}
		}
		else
		{
			FrontTrailColor = Color.Transparent;
			BackTrailColor = Color.Transparent;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.RocketBehaviorInfo rocketBehaviorInfo = new CalamityUtils.RocketBehaviorInfo((int)RocketType);
		rocketBehaviorInfo.smallRadius = 4;
		rocketBehaviorInfo.mediumRadius = 7;
		rocketBehaviorInfo.largeRadius = 10;
		CalamityUtils.RocketBehaviorInfo info = rocketBehaviorInfo;
		int blastRadius = base.Projectile.RocketBehavior(info);
		base.Projectile.ExpandHitboxBy((float)blastRadius);
		base.Projectile.Damage();
		if (!Main.dedServ)
		{
			GeneralParticleHandler.SpawnParticle(new DetailedExplosion(base.Projectile.Center, Vector2.Zero, Color.Orange, Vector2.One, Main.rand.NextFloat((float)Math.PI), 0.1f, radius(0.37f, 0.65f, 0.93f), 15));
			for (int i = 1; i < 3; i++)
			{
				GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, Color.DarkOrange, Vector2.One, 0f, radius(0.06f, 0.1f, 0.14f) * (float)i, radius(0.57f, 1f, 1.43f) * (float)i, 20));
			}
			for (int j = 0; j < 30; j++)
			{
				bool randomDust = Main.rand.NextBool();
				Vector2 center = base.Projectile.Center;
				int type = (randomDust ? 109 : 6);
				Vector2? velocity = Main.rand.NextVector2Circular(10f, 10f);
				float scale = (randomDust ? Main.rand.NextFloat(1f, 2f) : Main.rand.NextFloat(2.5f, 3f));
				Dust dust = Dust.NewDustPerfect(center, type, velocity, 0, default(Color), scale);
				dust.noGravity = true;
				dust.noLight = true;
				dust.noLightEmittence = true;
			}
			int debriAmount = Main.rand.Next(10, 16);
			for (int debriIndex = 0; debriIndex < debriAmount; debriIndex++)
			{
				Vector2 velocity2 = ((float)Math.PI * 2f / (float)debriAmount * (float)debriIndex).ToRotationVector2() * Main.rand.NextFloat(2f, 8f);
				GeneralParticleHandler.SpawnParticle(new StoneDebrisParticle(base.Projectile.Center, velocity2, Color.Lerp(Color.White, Color.LightGray, Main.rand.NextFloat()), Main.rand.NextFloat(0.4f, 0.6f), Main.rand.Next(30, 46), Main.rand.NextFloat((float)Math.PI)));
			}
			int mistAmount = Main.rand.Next(5, 9);
			for (int mistIndex = 0; mistIndex < mistAmount; mistIndex++)
			{
				Vector2 velocity3 = ((float)Math.PI * 2f / (float)mistAmount * (float)mistIndex).ToRotationVector2() * Main.rand.NextFloat(5f, 15f);
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, velocity3, new Color(255, 100, 0), Color.Transparent, Main.rand.NextFloat(0.6f, 1.4f), Main.rand.NextFloat(200f, 400f)));
			}
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MineralMortarExplode");
			style.PitchVariance = 0.2f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		float radius(float smallBoomRadius, float mediumBoomRadius, float bigBoomRadius)
		{
			if (blastRadius == 4)
			{
				return smallBoomRadius;
			}
			if (blastRadius == 7)
			{
				return mediumBoomRadius;
			}
			if (blastRadius == 10)
			{
				return bigBoomRadius;
			}
			return 0f;
		}
	}

	public static float TrailWidthFunction(float completionRatio, Vector2 vertexPos)
	{
		return MathHelper.Lerp(20f, 0f, completionRatio);
	}

	public Color ColorTrailFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(BackTrailColor, FrontTrailColor, completionRatio);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 position = base.Projectile.Center - Main.screenPosition;
		float rotation = base.Projectile.rotation + (float)Math.PI / 2f;
		Vector2 origin = texture.Size() * 0.5f;
		GameShaders.Misc["CalamityMod:TrailStreak"].SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Trails/SwordSlashTexture", (AssetRequestMode)2));
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(TrailWidthFunction, ColorTrailFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f;
		}, smoothen: true, pixelate: false, GameShaders.Misc["CalamityMod:TrailStreak"]), 50);
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 0; i < 3; i++)
			{
				Color darkOrange = Color.DarkOrange;
				((Color)(ref darkOrange)).A = 75;
				Color afterimageDrawColor = darkOrange * (1f - (float)i / (float)base.Projectile.oldPos.Length);
				Vector2 afterimageDrawPosition = base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f - Main.screenPosition;
				Main.EntitySpriteDraw(texture, afterimageDrawPosition, null, afterimageDrawColor, base.Projectile.oldRot[i] - (float)Math.PI / 2f, origin, base.Projectile.scale, (SpriteEffects)0);
			}
		}
		Main.EntitySpriteDraw(texture, position, null, base.Projectile.GetAlpha(lightColor), rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
