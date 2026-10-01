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

public class Flare : ModProjectile, ILocalizedModType, IModType
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
		base.Projectile.timeLeft = 600;
		base.Projectile.extraUpdates = 1;
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
		float projVelocityMult = 4f;
		float projFloatDirection = (float)(Math.Cos((float)Math.PI / 15f * base.Projectile.ai[0]) - 0.5) * projVelocityMult;
		base.Projectile.velocity.Y = base.Projectile.velocity.Y - projFloatDirection;
		base.Projectile.ai[0]++;
		projFloatDirection = (float)(Math.Cos((float)Math.PI / 15f * base.Projectile.ai[0]) - 0.5) * projVelocityMult;
		base.Projectile.velocity.Y = base.Projectile.velocity.Y + projFloatDirection;
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 10f)
		{
			base.Projectile.alpha -= 5;
			if (base.Projectile.alpha < 100)
			{
				base.Projectile.alpha = 100;
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
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
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
		int spawnLimitY = (int)(Main.player[base.Projectile.owner].Center.Y / 16f) + 25;
		if (projTileX > spawnLimitY)
		{
			projTileX = spawnLimitY;
		}
		int flarenadoSpawn = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), projTileY * 16 + 8, projTileX * 16 - 24, 0f, 0f, ModContent.ProjectileType<Flarenado>(), 0, 4f, Main.myPlayer, 11f, 10f + (revenge ? 1f : 0f));
		Main.projectile[flarenadoSpawn].netUpdate = true;
	}
}
