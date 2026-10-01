using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class RedirectingGildedSoul : ModProjectile, ILocalizedModType, IModType
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
		base.Projectile.scale = 0.6f;
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		RedirectingVengefulSoul.DoSoulAI(base.Projectile, ref Time, 5f);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		Color color = Color.Lerp(Color.Lerp(Color.DarkGoldenrod, Color.Gold, (float)base.Projectile.identity % 5f / 5f), Color.White, BurstIntensity * 0.5f + (float)Math.Cos(Main.GlobalTimeWrappedHourly * 2.7f) * 0.04f);
		((Color)(ref color)).A = 127;
		return color * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection != 1);
		for (int i = 0; i < 4; i++)
		{
			for (int j = 0; j < base.Projectile.oldPos.Length / 2; j++)
			{
				Color drawColor = base.Projectile.GetAlpha(lightColor);
				drawColor = Color.Lerp(drawColor, Color.White * base.Projectile.Opacity, 2f * (float)j / (float)base.Projectile.oldPos.Length) * (float)Math.Pow(1f - Utils.GetLerpValue(0f, base.Projectile.oldPos.Length / 2, j, clamped: true), 2.0);
				Vector2 drawPosition = base.Projectile.oldPos[j] + base.Projectile.Size * 0.5f + ((float)Math.PI * 2f * (float)i / 4f).ToRotationVector2() * 0.5f - Main.screenPosition;
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
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound((BurstIntensity >= 1f) ? SoundID.NPCDeath52 : SoundID.NPCHit35, base.Projectile.Center);
		SoundEngine.PlaySound(in SoundID.NPCDeath55, base.Projectile.Center);
		if (!Main.dedServ)
		{
			for (int i = 0; i < 45; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(80f, 80f) * (float)Math.Pow(BurstIntensity, 2.0), 264);
				dust.velocity = Main.rand.NextVector2Circular(2f, 2f);
				dust.color = base.Projectile.GetAlpha(Color.White);
				dust.scale = MathHelper.Lerp(1f, 1.6f, BurstIntensity);
				dust.noGravity = true;
				dust.noLight = true;
			}
		}
	}
}
