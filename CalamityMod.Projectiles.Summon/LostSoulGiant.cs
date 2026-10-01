using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class LostSoulGiant : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.SentryShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.scale = 0.8f;
		base.Projectile.width = (base.Projectile.height = 52);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		DoSoulAI(base.Projectile, ref Time);
	}

	public static void DoSoulAI(Projectile projectile, ref float time)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		if (time >= 15f)
		{
			NPC potentialTarget = projectile.Center.MinionHoming(1800f, Main.player[projectile.owner], ignoreTiles: false);
			if (potentialTarget != null)
			{
				HomeInOnTarget(projectile, potentialTarget);
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

	public static void HomeInOnTarget(Projectile projectile, NPC target)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		float speed = MathHelper.Lerp(21f, 31f, (float)Math.Sin((float)projectile.identity % 7f / 7f * ((float)Math.PI * 2f)) * 0.5f + 0.5f);
		projectile.velocity = Vector2.Lerp(projectile.velocity, projectile.SafeDirectionTo(target.Center) * speed, 0.075f);
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
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		Color baseColor = Color.Lerp(Color.IndianRed, Color.DarkViolet, (float)base.Projectile.identity % 5f / 5f * 0.5f);
		Color color = Color.Lerp(baseColor * 1.5f, baseColor, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 2.7f) * 0.04f + 0.45f);
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
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCDeath52, base.Projectile.Center);
		if (!Main.dedServ)
		{
			for (int i = 0; i < 45; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(60f, 60f), 264);
				dust.velocity = Main.rand.NextVector2Circular(2f, 2f);
				dust.color = base.Projectile.GetAlpha(Color.White);
				dust.scale = 1.45f;
				dust.noGravity = true;
				dust.noLight = true;
			}
		}
	}
}
