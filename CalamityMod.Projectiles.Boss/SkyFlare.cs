using System;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class SkyFlare : ModProjectile, ILocalizedModType, IModType
{
	public int blowTimer;

	public static readonly SoundStyle FlareSound = new SoundStyle("CalamityMod/Sounds/Custom/Yharon/YharonInfernado");

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = 1;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.9995f;
		int addStuff = Main.rand.Next(5);
		blowTimer += addStuff;
		if (blowTimer >= 900)
		{
			base.Projectile.Kill();
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 5)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 4)
		{
			base.Projectile.frame = 0;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, Main.DiscoG, 53, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture2D13 = TextureAssets.Projectile[base.Type].Value;
		int framing = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		Main.spriteBatch.Draw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture2D13.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)framing / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		SoundEngine.PlaySound(in FlareSound, base.Projectile.Center);
		int dustAmt = 36;
		for (int i = 0; i < dustAmt; i++)
		{
			Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(i - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.Projectile.Center;
			Vector2 dustDirection = val - base.Projectile.Center;
			int flareDust = Dust.NewDust(val + dustDirection, 0, 0, 244, dustDirection.X * 2f, dustDirection.Y * 2f, 100, default(Color), 1.4f);
			Main.dust[flareDust].noGravity = true;
			Main.dust[flareDust].noLight = true;
			Main.dust[flareDust].velocity = dustDirection;
		}
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		int projTileX = (int)(base.Projectile.Center.Y / 16f);
		int projTileY = (int)(base.Projectile.Center.X / 16f);
		if (projTileY < 10)
		{
			projTileY = 10;
		}
		if (projTileY > Main.maxTilesX - 10)
		{
			projTileY = Main.maxTilesX - 10;
		}
		if (projTileX < 10)
		{
			projTileX = 10;
		}
		if (projTileX > Main.maxTilesY - 110)
		{
			projTileX = Main.maxTilesY - 110;
		}
		for (int j = projTileX; j < projTileX + 100; j++)
		{
			Tile tile = Main.tile[projTileY, j];
			if (tile.HasTile && (Main.tileSolid[tile.TileType] || tile.LiquidAmount != 0))
			{
				projTileX = j;
				break;
			}
		}
		if (Main.rand.Next(6) < 5)
		{
			int nadoDamage = (Main.expertMode ? 180 : 300);
			int nadoSpawn = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), projTileY * 16 + 8, projTileX * 16 - 24, 0f, 0f, ModContent.ProjectileType<Flarenado>(), nadoDamage, 4f, Main.myPlayer, 16f, 15f + (revenge ? 4f : 0f));
			Main.projectile[nadoSpawn].netUpdate = true;
		}
		else
		{
			int nadoDamage2 = (Main.expertMode ? 230 : 400);
			int nadoSpawn2 = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), projTileY * 16 + 8, projTileX * 16 - 24, 0f, 0f, ModContent.ProjectileType<Infernado>(), nadoDamage2, 4f, Main.myPlayer, 16f, 16f + (revenge ? 4f : 0f));
			Main.projectile[nadoSpawn2].netUpdate = true;
		}
	}
}
