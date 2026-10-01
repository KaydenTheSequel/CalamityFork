using System;
using CalamityMod.DataStructures;
using CalamityMod.Graphics.Primitives;
using CalamityMod.Utilities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class RainbowRocket : ModProjectile, ILocalizedModType, IModType
{
	public enum PartyCannonExplosionType
	{
		Pink,
		Orange,
		Yellow,
		White,
		SkyBlue,
		Purple,
		PalePink,
		Count
	}

	public const float SwerveAngle = 0.02f;

	public const float SwerveAngleOffsetMax = 0.04f;

	public const float SwerveTime = 60f;

	public const float HomingAcceleration = 0.4f;

	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float Time => ref base.Projectile.ai[0];

	public PartyCannonExplosionType RocketType
	{
		get
		{
			return (PartyCannonExplosionType)base.Projectile.ai[1];
		}
		set
		{
			base.Projectile.ai[1] = (float)value;
		}
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 28;
		base.Projectile.height = 52;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		Vector2 center = base.Projectile.Center;
		Color val = Main.hslToRgb((float)Math.Sin(Time / 20f) * 0.5f + 0.5f, 0.9f, 0.9f);
		Lighting.AddLight(center, ((Color)(ref val)).ToVector3());
		base.Projectile.tileCollide = Time > 60f;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 4 == 3)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
		}
		NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(2300f, ignoreTiles: true, bossPriority: true);
		if (Time < 60f)
		{
			DoMovement_IdleSwerveFly();
		}
		else if (potentialTarget != null)
		{
			DoMovement_FlyToTarget(potentialTarget);
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
	}

	private void DoMovement_IdleSwerveFly()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		float swerveAngleOffset = MathHelper.Lerp(-0.04f, 0.04f, (float)RocketType / 7f);
		base.Projectile.velocity = base.Projectile.velocity.RotatedBy(swerveAngleOffset + 0.02f);
	}

	private void DoMovement_FlyToTarget(NPC target)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		float angleOffset = MathHelper.WrapAngle(base.Projectile.AngleTo(target.Center) - base.Projectile.velocity.ToRotation());
		base.Projectile.velocity = base.Projectile.velocity.RotatedBy(MathHelper.Clamp(angleOffset, -0.2f, 0.2f));
		base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(-Vector2.UnitY) * (((Vector2)(ref base.Projectile.velocity)).Length() + 0.4f);
		if (Vector2.Dot(base.Projectile.velocity.SafeNormalize(Vector2.Zero), base.Projectile.SafeDirectionTo(target.Center)) < 0.75f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.75f;
		}
	}

	internal Color GetRocketColor()
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		return (Color)(RocketType switch
		{
			PartyCannonExplosionType.Pink => Color.Pink, 
			PartyCannonExplosionType.Orange => Color.Orange, 
			PartyCannonExplosionType.Yellow => Color.LightGoldenrodYellow * 0.8f, 
			PartyCannonExplosionType.White => Color.LightGray, 
			PartyCannonExplosionType.SkyBlue => Color.LightSkyBlue, 
			PartyCannonExplosionType.Purple => Color.Magenta, 
			PartyCannonExplosionType.PalePink => Color.Pink * 0.77f, 
			_ => Color.White, 
		});
	}

	internal Color ColorFunction(float completionRatio, Vector2 vertexPos)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		Color baseColor = Main.hslToRgb(((float)base.Projectile.identity * 0.33f + completionRatio + Main.GlobalTimeWrappedHourly * 2f) % 1f, 1f, 0.54f);
		return Color.Lerp(GetRocketColor(), baseColor, MathHelper.Clamp(completionRatio * 0.8f, 0f, 1f)) * base.Projectile.Opacity;
	}

	internal float WidthFunction(float completionRatio, Vector2 vertexPos)
	{
		float maxWidthOutwardness = 8f;
		float width = ((!(completionRatio < 0.1f)) ? MathHelper.Lerp(maxWidthOutwardness, 0f, Utils.GetLerpValue(0.1f, 1f, completionRatio, clamped: true)) : ((float)Math.Sin(completionRatio / 0.1f * ((float)Math.PI / 2f)) * maxWidthOutwardness + 0.1f));
		return width * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.oldPos[0] = base.Projectile.position + base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 50f;
		PrimitiveRenderer.RenderTrail(base.Projectile.oldPos, new PrimitiveSettings(WidthFunction, ColorFunction, delegate
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Size * 0.5f + base.Projectile.velocity;
		}), 80);
		Texture2D rocketTexture = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(rocketTexture, base.Projectile.Center - Main.screenPosition, rocketTexture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame), GetRocketColor(), base.Projectile.rotation, rocketTexture.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int i = 1; i < base.Projectile.oldPos.Length; i++)
			{
				if (Main.rand.NextBool(3))
				{
					float offsetAngle = MathHelper.Lerp(-(float)Math.PI / 4f, (float)Math.PI / 4f, (float)i / (float)base.Projectile.oldPos.Length);
					Vector2 spawnPosition = base.Projectile.oldPos[i] + base.Projectile.Size * 0.5f;
					Vector2 spawnVelocity = (base.Projectile.oldPos[i - 1] - base.Projectile.oldPos[i]).SafeNormalize(Vector2.Zero);
					spawnVelocity = spawnVelocity.RotatedBy(offsetAngle);
					spawnVelocity *= Main.rand.NextFloat(12f, 18f);
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPosition, spawnVelocity, ModContent.ProjectileType<PartySparkle>(), base.Projectile.damage, 2f, base.Projectile.owner);
				}
			}
		}
		base.Projectile.ExpandHitboxBy(350);
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		if (!Main.dedServ)
		{
			switch (RocketType)
			{
			case PartyCannonExplosionType.Pink:
				PinkMarkExplosionDust();
				break;
			case PartyCannonExplosionType.Orange:
				OrangeMarkExplosionDust();
				break;
			case PartyCannonExplosionType.Yellow:
				YellowMarkExplosionDust();
				break;
			case PartyCannonExplosionType.White:
				WhiteMarkExplosionDust();
				break;
			case PartyCannonExplosionType.SkyBlue:
				SkyBlueMarkExplosionDust();
				break;
			case PartyCannonExplosionType.Purple:
				PurpleMarkExplosionDust();
				break;
			case PartyCannonExplosionType.PalePink:
				PalePinkMarkExplosionDust();
				break;
			}
		}
	}

	public void PinkMarkExplosionDust()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		BalloonExplosionDust(base.Projectile.Center, Main.rand.NextFloat(8f, 15f), Color.Yellow);
		float absoluteOffsetAngle = Main.rand.NextFloat(0.4f, 0.7f) * -1f;
		Vector2 offset = Vector2.UnitY.RotatedBy(absoluteOffsetAngle) * 120f;
		offset += Vector2.UnitX * 40f;
		BalloonExplosionDust(base.Projectile.Center + offset, Main.rand.NextFloat(6f, 11f), Color.DeepPink, absoluteOffsetAngle);
		absoluteOffsetAngle = Main.rand.NextFloat(0.4f, 0.7f);
		offset = Vector2.UnitY.RotatedBy(absoluteOffsetAngle) * 120f;
		offset -= Vector2.UnitX * 40f;
		BalloonExplosionDust(base.Projectile.Center + offset, Main.rand.NextFloat(6f, 11f), Color.DeepPink, absoluteOffsetAngle);
	}

	public void BalloonExplosionDust(Vector2 center, float petalBurstSpeed, Color balloonColor, float absoluteOffsetAngle = 0f)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		float explosionOffsetAngle = Main.rand.NextFloat(-0.3f, 0.3f) + absoluteOffsetAngle;
		for (float angle = 0f; angle <= (float)Math.PI * 2f; angle += MathHelper.ToRadians(4f))
		{
			Vector2 val = angle.ToRotationVector2();
			float unitMultiplier = 2f + (1f + (float)Math.Cos((float)Math.PI + angle));
			Vector2 velocity = val * unitMultiplier;
			velocity.X *= 0.5f;
			velocity = velocity.SafeNormalize(Vector2.Zero);
			velocity = velocity.RotatedBy(1.5707963705062866);
			velocity = velocity.RotatedBy(explosionOffsetAngle);
			Dust dust = Dust.NewDustPerfect(center, 261);
			dust.velocity = velocity * petalBurstSpeed;
			dust.noGravity = true;
			dust.color = balloonColor;
			dust.fadeIn = 1.5f;
		}
		StringExplosionDust(center, petalBurstSpeed, explosionOffsetAngle);
	}

	public void StringExplosionDust(Vector2 center, float petalBurstSpeed, float offsetAngle)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		int evaluationPoints = 40;
		BezierCurve bezierCurve = new BezierCurve(new Vector2(0f, 40f), new Vector2(20f, 76f), new Vector2(-16f, 108f), new Vector2(-20f, 146f), new Vector2(-14f, 180f), new Vector2(10f, 214f));
		for (int i = 0; i < evaluationPoints; i++)
		{
			Dust dust = Dust.NewDustPerfect(center, 261);
			dust.position = center + bezierCurve.Evaluate((float)i / (float)evaluationPoints).RotatedBy(offsetAngle) + Vector2.UnitY * petalBurstSpeed * 2f;
			dust.velocity = Vector2.Zero;
			dust.noGravity = true;
			dust.color = Color.White;
			dust.fadeIn = 1.5f;
		}
	}

	public void OrangeMarkExplosionDust()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		AppleExplosionDust(base.Projectile.Center, Main.rand.NextFloat(8f, 15f));
		float absoluteOffsetAngle = Main.rand.NextFloat(0.4f, 0.7f) * -1f;
		Vector2 offset = Vector2.UnitY.RotatedBy(absoluteOffsetAngle) * 120f;
		offset += Vector2.UnitX * 70f;
		AppleExplosionDust(base.Projectile.Center + offset, Main.rand.NextFloat(6f, 11f), absoluteOffsetAngle);
		absoluteOffsetAngle = Main.rand.NextFloat(0.4f, 0.7f);
		offset = Vector2.UnitY.RotatedBy(absoluteOffsetAngle) * 120f;
		offset -= Vector2.UnitX * 70f;
		AppleExplosionDust(base.Projectile.Center + offset, Main.rand.NextFloat(6f, 11f), absoluteOffsetAngle);
	}

	public void AppleExplosionDust(Vector2 center, float appleBurstSpeed, float offsetAngle = 0f)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		for (float angle = 0f; angle <= (float)Math.PI * 2f; angle += MathHelper.ToRadians(4f))
		{
			Vector2 val = angle.ToRotationVector2();
			float cosineValue = (float)Math.Cos(angle);
			Vector2 velocity = val;
			velocity = velocity.SafeNormalize(Vector2.Zero);
			velocity.X *= 1.333f;
			if (Math.Abs(cosineValue) < 0.35f)
			{
				velocity.Y *= MathHelper.Lerp(0.9f, 1f, Math.Abs(cosineValue) / 0.35f);
				if ((float)Math.Sign(cosineValue) == 1f)
				{
					velocity.Y *= 0.85f;
				}
			}
			velocity = velocity.RotatedBy(offsetAngle);
			Dust dust = Dust.NewDustPerfect(center, 261);
			dust.velocity = velocity * appleBurstSpeed;
			dust.noGravity = true;
			dust.color = Color.Orange;
			dust.fadeIn = 1.5f;
		}
		AppleLeafExplosionDust(center + Utils.RotatedBy(new Vector2(0f, -54f), (double)offsetAngle, default(Vector2)), offsetAngle);
	}

	public void AppleLeafExplosionDust(Vector2 center, float offsetAngle)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		int evaluationPoints = 40;
		BezierCurve bezierCurve = new BezierCurve(new Vector2(0f, 0f), new Vector2(-20f, -26f), new Vector2(-26f, -46f), new Vector2(-30f, -60f), new Vector2(-24f, -70f), new Vector2(-10f, -40f), new Vector2(0f, 0f));
		for (int i = 0; i < evaluationPoints; i++)
		{
			Dust dust = Dust.NewDustPerfect(center, 261);
			dust.position = center + bezierCurve.Evaluate((float)i / (float)evaluationPoints).RotatedBy(offsetAngle);
			dust.velocity = Vector2.Zero;
			dust.noGravity = true;
			dust.color = Color.Brown;
			dust.fadeIn = 1.5f;
		}
	}

	public void YellowMarkExplosionDust()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		ButterflyExplosionDust(base.Projectile.Center, Main.rand.NextFloat(8f, 12f));
		Vector2 offset = Vector2.UnitY.RotatedBy(Main.rand.NextFloat(0.4f, 0.7f) * -1f) * 120f;
		offset += Vector2.UnitX * 84f;
		ButterflyExplosionDust(base.Projectile.Center + offset, Main.rand.NextFloat(6f, 11f));
		offset = Vector2.UnitY.RotatedBy(Main.rand.NextFloat(0.4f, 0.7f)) * 120f;
		offset -= Vector2.UnitX * 84f;
		ButterflyExplosionDust(base.Projectile.Center + offset, Main.rand.NextFloat(6f, 11f));
	}

	public void ButterflyExplosionDust(Vector2 center, float butterflyBurstSpeed, float offsetAngle = 0f)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		int petalCount = 4;
		for (float angle = 0f; angle <= (float)Math.PI * 2f; angle += MathHelper.ToRadians(4f))
		{
			Vector2 val = angle.ToRotationVector2();
			float unitMultiplier = 2f + (1f + (float)Math.Cos(angle * (float)petalCount));
			Vector2 velocity = val * unitMultiplier;
			velocity = velocity.RotatedBy(2.356194496154785);
			velocity = velocity.RotatedBy(offsetAngle);
			velocity *= 0.25f;
			Dust dust = Dust.NewDustPerfect(center, 262);
			dust.velocity = velocity * butterflyBurstSpeed;
			dust.noGravity = true;
			dust.color = Color.Pink;
			dust.fadeIn = 1.5f;
		}
		ButterflyAntennaExplosionDust(center + Utils.RotatedBy(new Vector2(0f, -74f), (double)offsetAngle, default(Vector2)), offsetAngle);
	}

	public void ButterflyAntennaExplosionDust(Vector2 center, float offsetAngle = 0f)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		int evaluationPoints = 40;
		BezierCurve bezierCurve = new BezierCurve(new Vector2(0f, 60f), new Vector2(0f, 30f), new Vector2(10f, -28f), new Vector2(20f, -42f), new Vector2(30f, -48f), new Vector2(42f, -40f), new Vector2(50f, -30f), new Vector2(52f, -24f));
		for (int i = 0; i < evaluationPoints; i++)
		{
			Dust dust = Dust.NewDustPerfect(center, 263);
			dust.position = center + bezierCurve.Evaluate((float)i / (float)evaluationPoints).RotatedBy(offsetAngle);
			dust.velocity = Vector2.Zero;
			dust.noGravity = true;
			dust.color = Color.SkyBlue;
			dust.fadeIn = 1.5f;
			DustExtensions.BetterCloneDust(dust).position = center + bezierCurve.Evaluate((float)i / (float)evaluationPoints).RotatedBy(offsetAngle) * new Vector2(-1f, 1f);
		}
	}

	public void WhiteMarkExplosionDust()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		DiamondExplosionDust(base.Projectile.Center, Main.rand.NextFloat(8f, 12f));
		float absoluteOffsetAngle = Main.rand.NextFloat(0.4f, 0.7f);
		Vector2 offset = Vector2.UnitY.RotatedBy(absoluteOffsetAngle * -1f) * 120f;
		offset += Vector2.UnitX * 84f;
		DiamondExplosionDust(base.Projectile.Center + offset, Main.rand.NextFloat(6f, 11f));
		absoluteOffsetAngle = Main.rand.NextFloat(0.4f, 0.7f);
		offset = Vector2.UnitY.RotatedBy(absoluteOffsetAngle) * 120f;
		offset -= Vector2.UnitX * 84f;
		DiamondExplosionDust(base.Projectile.Center + offset, Main.rand.NextFloat(6f, 11f));
	}

	public void DiamondExplosionDust(Vector2 center, float diamondBurstSpeed, float offsetAngle = 0f)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		int dustCount = 80;
		for (int i = 0; i < dustCount; i++)
		{
			Vector2 startingVelocity = Vector2.Zero;
			Vector2 endingVelocity = Vector2.Zero;
			switch (i / (dustCount / 4))
			{
			case 0:
				startingVelocity = Vector2.UnitY;
				endingVelocity = Vector2.UnitX;
				break;
			case 1:
				startingVelocity = Vector2.UnitX;
				endingVelocity = -Vector2.UnitY;
				break;
			case 2:
				startingVelocity = -Vector2.UnitY;
				endingVelocity = -Vector2.UnitX;
				break;
			case 3:
				startingVelocity = -Vector2.UnitX;
				endingVelocity = Vector2.UnitY;
				break;
			}
			Vector2 velocity = Vector2.Lerp(startingVelocity, endingVelocity, (float)i / ((float)dustCount / 4f) % 1f);
			velocity = velocity.RotatedBy(offsetAngle);
			velocity *= diamondBurstSpeed;
			velocity.X *= 0.667f;
			Dust dust = Dust.NewDustPerfect(center, 263);
			dust.position = center;
			dust.velocity = velocity;
			dust.noGravity = true;
			dust.color = Color.LightSteelBlue;
			dust.fadeIn = 1.5f;
		}
	}

	public void SkyBlueMarkExplosionDust()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		RainbowBoltExplosionDust(base.Projectile.Center, Main.rand.NextFloat(-0.2f, 0.2f));
	}

	public void RainbowBoltExplosionDust(Vector2 center, float offsetAngle = 0f)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 5; i++)
		{
			CloudExplosionDust(center, offsetAngle);
		}
		LightningExplosionDust(center, offsetAngle);
	}

	public void CloudExplosionDust(Vector2 center, float offsetAngle = 0f)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		center += Main.rand.NextVector2CircularEdge(40f, 40f);
		Vector2 offset = Main.rand.NextVector2Square(0.9f, 1.15f);
		for (float angle = 0f; angle <= (float)Math.PI * 2f; angle += MathHelper.ToRadians(2f))
		{
			Vector2 spawnOffset = angle.ToRotationVector2().RotatedBy(offsetAngle);
			spawnOffset = spawnOffset.RotatedByRandom(0.4000000059604645);
			spawnOffset *= new Vector2(54f, 36f) * offset;
			spawnOffset -= Vector2.UnitY.RotatedBy(offsetAngle) * 90f;
			Dust dust = Dust.NewDustPerfect(center + spawnOffset, 263);
			dust.velocity = Vector2.Zero;
			dust.noGravity = true;
			dust.scale = 2.7f;
			dust.fadeIn = 1.5f;
		}
	}

	public void LightningExplosionDust(Vector2 center, float offsetAngle = 0f)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		int evaluationPoints = 65;
		Vector2[] controlPoints = (Vector2[])(object)new Vector2[16]
		{
			new Vector2(-38f, -64f),
			new Vector2(-42f, -44f),
			new Vector2(-26f, -28f),
			new Vector2(-24f, -10f),
			new Vector2(-34f, -4f),
			new Vector2(-48f, -10f),
			new Vector2(-40f, 12f),
			new Vector2(-30f, 30f),
			new Vector2(-20f, 48f),
			new Vector2(-8f, 28f),
			new Vector2(2f, 6f),
			new Vector2(-8f, -4f),
			new Vector2(4f, -16f),
			new Vector2(8f, -36f),
			new Vector2(12f, -56f),
			new Vector2(-38f, -64f)
		};
		for (int i = 0; i < evaluationPoints; i++)
		{
			int currentIndex = (int)((float)i / (float)evaluationPoints * (float)controlPoints.Length);
			Vector2 currentPosition = controlPoints[currentIndex];
			Vector2 nextPosition = controlPoints[(currentIndex + 1) % controlPoints.Length];
			Dust dust = Dust.NewDustPerfect(center, 263);
			dust.position = center + Vector2.Lerp(currentPosition, nextPosition, (float)i / (float)evaluationPoints * (float)controlPoints.Length % 0.999f).RotatedBy(offsetAngle) * 2f;
			dust.position += Vector2.UnitY.RotatedBy(offsetAngle) * 138f;
			dust.velocity = Vector2.Zero;
			dust.noGravity = true;
			dust.color = Main.hslToRgb((float)i / (float)evaluationPoints * 3f % 1f, 0.6f, 0.7f);
			dust.scale = 1.4f;
			dust.fadeIn = 1.5f;
		}
		center += Main.rand.NextVector2CircularEdge(120f, 120f);
		Vector2 offset = Main.rand.NextVector2Square(0.9f, 1.15f);
		for (float angle = 0f; angle <= (float)Math.PI * 2f; angle += MathHelper.ToRadians(2f))
		{
			Vector2 spawnOffset = angle.ToRotationVector2().RotatedBy(offsetAngle);
			spawnOffset = spawnOffset.RotatedByRandom(0.4000000059604645);
			spawnOffset *= new Vector2(130f, 84f) * offset;
			spawnOffset += Vector2.UnitY.RotatedBy(offsetAngle) * 30f;
			Dust dust2 = Dust.NewDustPerfect(center + spawnOffset, 263);
			dust2.velocity = Vector2.Zero;
			dust2.noGravity = true;
			dust2.scale = 4f;
			dust2.fadeIn = 1.5f;
		}
	}

	public void PurpleMarkExplosionDust()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		TwilightStarExplosionDust(base.Projectile.Center, 14f, Color.Magenta);
		for (int i = 0; i < 6; i++)
		{
			Vector2 offset = ((float)Math.PI * 2f * (float)i / 6f).ToRotationVector2() * Main.rand.NextFloat(140f, 180f);
			TwilightStarExplosionDust(base.Projectile.Center + offset, 4f, Color.White, Main.rand.NextFloat(-0.2f, 0.2f));
		}
	}

	public void TwilightStarExplosionDust(Vector2 center, float petalBurstSpeed, Color starColor, float absoluteOffsetAngle = 0f)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		int pointsOnStar = 6;
		for (int i = 0; i < pointsOnStar; i++)
		{
			float angle = 4.712389f - (float)i * ((float)Math.PI * 2f) / (float)pointsOnStar;
			float f = 4.712389f - (float)(i + 2) * ((float)Math.PI * 2f) / (float)pointsOnStar;
			Vector2 start = angle.ToRotationVector2();
			Vector2 end = f.ToRotationVector2();
			int pointsOnStarSegment = 35;
			for (int j = 0; j < pointsOnStarSegment; j++)
			{
				Dust dust = Dust.NewDustPerfect(center, 263);
				dust.scale = 1.8f;
				dust.velocity = Vector2.Lerp(start, end, (float)j / (float)pointsOnStarSegment) * petalBurstSpeed;
				dust.velocity.X *= 0.7f;
				dust.velocity = dust.velocity.RotatedBy(absoluteOffsetAngle);
				dust.fadeIn = 1.5f;
				dust.color = starColor;
				dust.noGravity = true;
			}
		}
	}

	public void PalePinkMarkExplosionDust()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		StarlightMarkExplosionDust(base.Projectile.Center);
		StarlightStarExplosionDust(base.Projectile.Center + new Vector2(16f, 36f), 2f);
	}

	public void StarlightMarkExplosionDust(Vector2 center)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		int evaluationPoints = 60;
		BezierCurve bezierCurve = new BezierCurve(new Vector2(-38f, -58f), new Vector2(-22f, -34f), new Vector2(-10f, -22f), new Vector2(6f, -11f), new Vector2(13f, 1f), new Vector2(-20f, 11f), new Vector2(16f, 11f), new Vector2(26f, 6f), new Vector2(24f, -11f), new Vector2(8f, -28f), new Vector2(-10f, -39f), new Vector2(-38f, -58f));
		for (int i = 0; i < evaluationPoints; i++)
		{
			Dust dust = Dust.NewDustPerfect(center, 263);
			dust.position = center + bezierCurve.Evaluate((float)i / (float)evaluationPoints) * new Vector2(3f, 1.7f);
			dust.velocity = Vector2.Zero;
			dust.color = ((i >= evaluationPoints / 2) ? Color.Cyan : Color.SkyBlue);
			dust.fadeIn = 1.5f;
			dust.scale = 1.4f;
			dust.noGravity = true;
			DustExtensions.BetterCloneDust(dust).position = center + bezierCurve.Evaluate((float)i / (float)evaluationPoints) * new Vector2(1f, -1f) + new Vector2(30f, -12f);
		}
	}

	public void StarlightStarExplosionDust(Vector2 center, float starBurstSpeed)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		int dustCount = 60;
		for (int i = 0; i < dustCount; i++)
		{
			Vector2 startingVelocity = Vector2.Zero;
			Vector2 endingVelocity = Vector2.Zero;
			switch (i / (dustCount / 4))
			{
			case 0:
				startingVelocity = Vector2.UnitY;
				endingVelocity = Vector2.UnitX;
				break;
			case 1:
				startingVelocity = Vector2.UnitX;
				endingVelocity = -Vector2.UnitY;
				break;
			case 2:
				startingVelocity = -Vector2.UnitY;
				endingVelocity = -Vector2.UnitX;
				break;
			case 3:
				startingVelocity = -Vector2.UnitX;
				endingVelocity = Vector2.UnitY;
				break;
			}
			Vector2 velocity = Vector2.Lerp(startingVelocity, endingVelocity, (float)i / ((float)dustCount / 4f) % 1f);
			velocity *= starBurstSpeed;
			velocity.X *= 0.75f;
			Dust dust = Dust.NewDustPerfect(center, 263);
			dust.position = center;
			dust.velocity = velocity;
			dust.noGravity = true;
			dust.color = Color.Purple;
			dust.fadeIn = 1.5f;
			Dust dust2 = DustExtensions.BetterCloneDust(dust);
			dust2.velocity = dust2.velocity.RotatedBy(0.7853981852531433);
			dust2.color = Color.White;
		}
	}
}
