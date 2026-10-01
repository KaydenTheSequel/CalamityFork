using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class BigFlare2 : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle FlareSound = new SoundStyle("CalamityMod/Sounds/Custom/Yharon/YharonInfernado");

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 100;
		base.Projectile.height = 100;
		base.Projectile.hostile = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 1200;
		base.Projectile.scale = 1.5f;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
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
		if (base.Projectile.ai[1] > 0f)
		{
			int playerTracker = (int)base.Projectile.ai[1] - 1;
			if (playerTracker < 255)
			{
				base.Projectile.localAI[0]++;
				if (base.Projectile.localAI[0] > 10f)
				{
					int dustAmt = 6;
					for (int i = 0; i < dustAmt; i++)
					{
						Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((double)(i - (dustAmt / 2 - 1)) * Math.PI / (double)(float)dustAmt) + base.Projectile.Center;
						Vector2 randomDustPos = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - (float)Math.PI / 2f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
						int flareDust = Dust.NewDust(val + randomDustPos, 0, 0, 244, randomDustPos.X * 2f, randomDustPos.Y * 2f, 100, default(Color), 1.4f);
						Main.dust[flareDust].noGravity = true;
						Main.dust[flareDust].noLight = true;
						Dust obj = Main.dust[flareDust];
						obj.velocity /= 4f;
						Dust obj2 = Main.dust[flareDust];
						obj2.velocity -= base.Projectile.velocity;
					}
					base.Projectile.alpha -= 5;
					if (base.Projectile.alpha < 100)
					{
						base.Projectile.alpha = 100;
					}
				}
				Vector2 playerDistance = Main.player[playerTracker].Center - base.Projectile.Center;
				float projVelocityMult = 4f;
				projVelocityMult += base.Projectile.localAI[0] / 60f;
				base.Projectile.velocity = Vector2.Normalize(playerDistance) * projVelocityMult;
				if (((Vector2)(ref playerDistance)).Length() < 64f)
				{
					base.Projectile.Kill();
				}
			}
		}
		if (base.Projectile.wet)
		{
			base.Projectile.position.Y = base.Projectile.position.Y - 16f;
			base.Projectile.Kill();
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, Main.DiscoG, 53, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
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
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		SoundEngine.PlaySound(in FlareSound, base.Projectile.Center);
		int killDustAmt = 36;
		for (int i = 0; i < killDustAmt; i++)
		{
			Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(i - (killDustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)killDustAmt) + base.Projectile.Center;
			Vector2 killDustDirection = val - base.Projectile.Center;
			int killFlareDust = Dust.NewDust(val + killDustDirection, 0, 0, 244, killDustDirection.X * 2f, killDustDirection.Y * 2f, 100, default(Color), 1.4f);
			Main.dust[killFlareDust].noGravity = true;
			Main.dust[killFlareDust].noLight = true;
			Main.dust[killFlareDust].velocity = killDustDirection;
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
		int spawnAreaY = Main.maxTilesY - projTileX;
		for (int j = projTileX; j < projTileX + spawnAreaY; j++)
		{
			Tile tile = Main.tile[projTileY, j + 10];
			if (tile.HasTile && !TileID.Sets.Platforms[tile.TileType] && (Main.tileSolid[tile.TileType] || tile.LiquidAmount != 0))
			{
				projTileX = j;
				break;
			}
		}
		int spawnLimitY = (int)(Main.player[base.Projectile.owner].Center.Y / 16f) + 75;
		if (projTileX > spawnLimitY)
		{
			projTileX = spawnLimitY;
		}
		int infernadoSpawn = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), projTileY * 16 + 8, projTileX * 16 - 24, 0f, 0f, ModContent.ProjectileType<Infernado2>(), 0, 4f, Main.myPlayer, 11f, 24f + (revenge ? 2f : 0f));
		Main.projectile[infernadoSpawn].netUpdate = true;
	}
}
