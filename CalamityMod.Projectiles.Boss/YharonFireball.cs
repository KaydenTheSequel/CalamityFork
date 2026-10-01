using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class YharonFireball : ModProjectile, ILocalizedModType, IModType
{
	private float speedX = -3f;

	private float speedX2 = -5f;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 34;
		base.Projectile.height = 34;
		base.Projectile.hostile = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 120;
		base.Projectile.aiStyle = 1;
		base.AIType = 686;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.rotation += (float)Math.PI;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(speedX);
		writer.Write(base.Projectile.localAI[1]);
		writer.Write(speedX2);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		speedX = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
		speedX2 = reader.ReadSingle();
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int frameHeight = texture.Height / Main.projFrames[base.Type];
		int frameY = frameHeight * base.Projectile.frame;
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(0, frameY, texture.Width, frameHeight);
		if (CalamityClientConfig.Instance.Afterimages)
		{
			for (int i = 0; i < base.Projectile.oldPos.Length; i++)
			{
				Vector2 drawPos = base.Projectile.oldPos[i] + base.Projectile.Size / 2f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
				Color color2 = base.Projectile.GetAlpha(lightColor) * ((float)(base.Projectile.oldPos.Length - i) / (float)base.Projectile.oldPos.Length);
				Main.spriteBatch.Draw(texture, drawPos, (Rectangle?)rectangle, color2, base.Projectile.rotation, rectangle.Size() / 2f, base.Projectile.scale, (SpriteEffects)0, 0f);
			}
		}
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, frameY, texture.Width, frameHeight), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)frameHeight / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int x = 0; x < 3; x++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, speedX, -50f, ModContent.ProjectileType<YharonFireball2>(), base.Projectile.damage, 0f, Main.myPlayer);
				speedX += 3f;
			}
			for (int i = 0; i < 2; i++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, speedX2, -75f, ModContent.ProjectileType<YharonFireball2>(), base.Projectile.damage, 0f, Main.myPlayer);
				speedX2 += 10f;
			}
		}
		base.Projectile.ExpandHitboxBy(144);
		for (int d = 0; d < 2; d++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 55, 0f, 0f, 50, default(Color), 1.5f);
		}
		for (int j = 0; j < 20; j++)
		{
			int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 55, 0f, 0f, 0, default(Color), 2.5f);
			Main.dust[idx].noGravity = true;
			Dust obj = Main.dust[idx];
			obj.velocity *= 3f;
			idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 55, 0f, 0f, 50, default(Color), 1.5f);
			Dust obj2 = Main.dust[idx];
			obj2.velocity *= 2f;
			Main.dust[idx].noGravity = true;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Dragonfire>(), 60);
		}
	}
}
