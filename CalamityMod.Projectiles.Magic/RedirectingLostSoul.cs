using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class RedirectingLostSoul : ModProjectile, ILocalizedModType, IModType
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
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		RedirectingVengefulSoul.DoSoulAI(base.Projectile, ref Time, 1f);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		Color color = Color.Lerp(Color.Lerp(Color.DarkRed, Color.DarkViolet, (float)base.Projectile.identity % 5f / 5f * 0.5f), Color.White, BurstIntensity * 0.5f + (float)Math.Cos(Main.GlobalTimeWrappedHourly * 2.7f) * 0.04f);
		((Color)(ref color)).A = 0;
		return color * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection != 1);
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		for (int j = 0; j < base.Projectile.oldPos.Length / 2; j++)
		{
			float fade = (float)Math.Pow(1f - Utils.GetLerpValue(0f, base.Projectile.oldPos.Length / 2, j, clamped: true), 2.0);
			Color drawColor = base.Projectile.GetAlpha(lightColor) * fade;
			Vector2 drawPosition = base.Projectile.oldPos[j] + base.Projectile.Size * 0.5f - Main.screenPosition;
			float rotation = base.Projectile.oldRot[j];
			Main.EntitySpriteDraw(texture, drawPosition, frame, drawColor, rotation, frame.Size() * 0.5f, base.Projectile.scale, direction);
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound((BurstIntensity >= 1f) ? SoundID.NPCDeath52 : SoundID.NPCHit35, base.Projectile.Center);
		if (!Main.dedServ)
		{
			for (int i = 0; i < 45; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(50f, 50f) * BurstIntensity, 264);
				dust.velocity = Main.rand.NextVector2Circular(2f, 2f);
				dust.color = base.Projectile.GetAlpha(Color.White);
				dust.scale = MathHelper.Lerp(1f, 1.6f, BurstIntensity);
				dust.noGravity = true;
				dust.noLight = true;
			}
		}
	}
}
