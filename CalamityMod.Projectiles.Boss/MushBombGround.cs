using System;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class MushBombGround : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/Boss/MushBomb";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		if (Main.expertMode)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
		if (CalamityWorld.death)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
		if (Main.rand.NextBool())
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 56, 0f, 0f, 100, default(Color), 0.8f);
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= 0f;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		Lighting.AddLight(base.Projectile.Center, 0f, 0.15f, 0.3f);
		float xVelocityMultiplier = 1.025f;
		float xVelocityLimit = 12f;
		if (Math.Abs(base.Projectile.velocity.X) < xVelocityLimit)
		{
			base.Projectile.velocity.X *= xVelocityMultiplier;
		}
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		return Main.zenithWorld ? new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, base.Projectile.alpha) : new Color(255, 255, 255, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int height = texture.Height / Main.projFrames[base.Type];
		int drawStart = height * base.Projectile.frame;
		Vector2 origin = base.Projectile.Size / 2f;
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, drawStart, texture.Width, height), Color.White * base.Projectile.Opacity, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int height = texture.Height / Main.projFrames[base.Type];
		int drawStart = height * base.Projectile.frame;
		Vector2 origin = base.Projectile.Size / 2f;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/MushBombGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, drawStart, texture.Width, height), Color.White * base.Projectile.Opacity, base.Projectile.rotation, origin, base.Projectile.scale, spriteEffects, 0f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCDeath1, base.Projectile.Center);
		for (int i = 0; i < 3; i++)
		{
			int shroomDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 56, 0f, 0f, 100);
			Dust obj = Main.dust[shroomDust];
			obj.velocity *= 1.5f;
			if (Main.rand.NextBool())
			{
				Main.dust[shroomDust].scale = 0.5f;
				Main.dust[shroomDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 9; j++)
		{
			int shroomDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 56, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[shroomDust2].noGravity = true;
			Dust obj2 = Main.dust[shroomDust2];
			obj2.velocity *= 2f;
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 56, 0f, 0f, 100, default(Color), 1.5f);
		}
	}
}
