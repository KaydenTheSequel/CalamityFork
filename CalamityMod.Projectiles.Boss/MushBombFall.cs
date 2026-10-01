using System;
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

public class MushBombFall : ModProjectile, ILocalizedModType, IModType
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
		base.Projectile.tileCollide = false;
		base.Projectile.Opacity = 0.25f;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
	}

	public override void AI()
	{
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.Y > 0f)
		{
			if (base.Projectile.Opacity < 1f)
			{
				base.Projectile.Opacity = 1f;
				SoundEngine.PlaySound(in SoundID.Item21, base.Projectile.Center);
				int dustAmount = 36;
				for (int i = 0; i < dustAmount; i++)
				{
					Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.5f).RotatedBy((float)(i - (dustAmount / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmount) + base.Projectile.Center;
					Vector2 dustVelocity = val - base.Projectile.Center;
					int dust = Dust.NewDust(val + dustVelocity, 0, 0, 56, dustVelocity.X, dustVelocity.Y);
					Main.dust[dust].noGravity = true;
					Main.dust[dust].noLight = true;
					Main.dust[dust].velocity = dustVelocity;
				}
			}
		}
		else if (Main.rand.NextBool())
		{
			int dust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 56, 0f, 0f, 100, default(Color), 0.8f);
			Main.dust[dust2].noGravity = true;
			Dust obj = Main.dust[dust2];
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
		if (base.Projectile.position.Y > base.Projectile.ai[1] && base.Projectile.velocity.Y > 0f)
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
		Lighting.AddLight(base.Projectile.Center, 0f, 0.15f, 0.3f);
		float velocityYLimit = ((base.Projectile.ai[0] == 1f) ? 1f : (CalamityWorld.death ? 6f : 5f));
		float velocityYIncrement = ((base.Projectile.ai[0] == 1f) ? 0.1f : (CalamityWorld.death ? 0.12f : 0.1f));
		if (base.Projectile.velocity.Y < velocityYLimit)
		{
			base.Projectile.velocity.Y += velocityYIncrement;
		}
		if (Math.Abs(base.Projectile.velocity.X) > ((base.Projectile.ai[0] == 1f) ? 0f : 2f))
		{
			base.Projectile.velocity.X *= 0.995f;
		}
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.Opacity == 1f;
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
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
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
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, drawStart, texture.Width, height), Color.White * base.Projectile.Opacity, base.Projectile.rotation, origin, base.Projectile.scale, spriteEffects, 0f);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int height = texture.Height / Main.projFrames[base.Type];
		int drawStart = height * base.Projectile.frame;
		Vector2 origin = base.Projectile.Size / 2f;
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/MushBombGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, drawStart, texture.Width, height), Color.White * base.Projectile.Opacity, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0, 0f);
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
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
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
		if (Main.zenithWorld && NPC.CountNPCS(67) < 20 && Main.netMode != 1)
		{
			int crab = NPC.NewNPC(base.Projectile.GetSource_FromAI(), (int)base.Projectile.Center.X, (int)base.Projectile.Center.Y, 67);
			if (crab.WithinBounds(Main.maxNPCs))
			{
				Main.projectile[crab].timeLeft = 1200;
			}
		}
	}
}
