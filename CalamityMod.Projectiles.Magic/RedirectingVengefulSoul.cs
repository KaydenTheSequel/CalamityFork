using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class RedirectingVengefulSoul : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float BurstIntensity => ref base.Projectile.ai[0];

	public ref float Time => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.scale = 0.8f;
		base.Projectile.width = (base.Projectile.height = 46);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		DoSoulAI(base.Projectile, ref Time, 2f);
	}

	public static void DoSoulAI(Projectile projectile, ref float time, float soulPower)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		if (time >= 20f)
		{
			NPC potentialTarget = projectile.Center.ClosestNPCAt(1250f, ignoreTiles: false);
			if (potentialTarget != null)
			{
				HomeInOnTarget(projectile, potentialTarget, time);
			}
			float accelerationFactor = MathHelper.SmoothStep(1.03f, 1.015f, Utils.GetLerpValue(6f, 24f, ((Vector2)(ref projectile.velocity)).Length(), clamped: true));
			if (((Vector2)(ref projectile.velocity)).Length() < 21f + soulPower * 2f)
			{
				projectile.velocity *= accelerationFactor;
			}
		}
		projectile.Opacity = Utils.GetLerpValue(0f, 15f, time, clamped: true);
		projectile.frameCounter++;
		if ((float)projectile.frameCounter % 5f == 4f)
		{
			projectile.frame = (projectile.frame + 1) % Main.projFrames[projectile.type];
		}
		projectile.rotation = projectile.velocity.ToRotation();
		projectile.spriteDirection = (projectile.velocity.X > 0f).ToDirectionInt();
		if (projectile.spriteDirection == -1)
		{
			projectile.rotation += (float)Math.PI;
		}
		time++;
	}

	public static void HomeInOnTarget(Projectile projectile, NPC target, float time)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		float oldSpeed = ((Vector2)(ref projectile.velocity)).Length();
		float delayFactor = Utils.GetLerpValue(20f, 35f, time, clamped: true);
		float homeSpeed = MathHelper.Lerp(0f, 0.075f, delayFactor);
		projectile.velocity = Vector2.Lerp(projectile.velocity, projectile.SafeDirectionTo(target.Center) * 16f, homeSpeed);
		projectile.velocity = (projectile.velocity + projectile.SafeDirectionTo(target.Center) * 3f).SafeNormalize(Vector2.UnitY) * oldSpeed;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		Color baseColor = Color.Lerp(Color.IndianRed, Color.DarkViolet, (float)base.Projectile.identity % 5f / 5f * 0.5f);
		Color color = Color.Lerp(baseColor * 1.5f, baseColor, BurstIntensity * 0.5f + (float)Math.Cos(Main.GlobalTimeWrappedHourly * 2.7f) * 0.04f);
		((Color)(ref color)).A = 0;
		return color * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection != 1);
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < base.Projectile.oldPos.Length / 2; j++)
			{
				float fade = (float)Math.Pow(1f - Utils.GetLerpValue(0f, base.Projectile.oldPos.Length / 2, j, clamped: true), 2.0);
				Color drawColor = Color.Lerp(base.Projectile.GetAlpha(lightColor), Color.White * base.Projectile.Opacity, (float)(j / base.Projectile.oldPos.Length)) * fade;
				Vector2 drawPosition = base.Projectile.oldPos[j] + base.Projectile.Size * 0.5f + ((float)Math.PI * 2f * (float)i / 4f).ToRotationVector2() * 1.5f - Main.screenPosition;
				float rotation = base.Projectile.oldRot[j];
				Main.EntitySpriteDraw(texture, drawPosition, frame, drawColor, rotation, frame.Size() * 0.5f, base.Projectile.scale, direction);
			}
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound((BurstIntensity >= 1f) ? SoundID.NPCDeath52 : SoundID.NPCHit35, base.Projectile.Center);
		if (!Main.dedServ)
		{
			for (int i = 0; i < 45; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(100f, 100f) * (float)Math.Pow(BurstIntensity, 2.0), 264);
				dust.velocity = Main.rand.NextVector2Circular(2f, 2f);
				dust.color = base.Projectile.GetAlpha(Color.White);
				dust.scale = MathHelper.Lerp(1.2f, 1.9f, BurstIntensity);
				dust.noGravity = true;
				dust.noLight = true;
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 264);
				dust2.velocity = ((float)Math.PI * 2f * (float)i / 45f).ToRotationVector2() * 6f;
				dust2.color = base.Projectile.GetAlpha(Color.White);
				dust2.scale = MathHelper.Lerp(1.2f, 1.9f, BurstIntensity);
				dust2.noGravity = true;
				dust2.noLight = true;
			}
		}
	}
}
