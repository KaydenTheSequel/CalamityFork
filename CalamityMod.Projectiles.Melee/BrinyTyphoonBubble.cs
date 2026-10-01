using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class BrinyTyphoonBubble : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "Terraria/Images/Projectile_385";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 5;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] > 0f)
		{
			int playerTrack = (int)base.Projectile.ai[1] - 1;
			if (playerTrack < 255)
			{
				base.Projectile.localAI[0]++;
				if (base.Projectile.localAI[0] > 10f)
				{
					int dustAmt = 6;
					for (int i = 0; i < dustAmt; i++)
					{
						Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((double)(i - (dustAmt / 2 - 1)) * Math.PI / (double)(float)dustAmt) + base.Projectile.Center;
						Vector2 faceDirection = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - (float)Math.PI / 2f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
						int bluishDust = Dust.NewDust(val + faceDirection, 0, 0, 187, faceDirection.X * 2f, faceDirection.Y * 2f, 100, new Color(53, Main.DiscoG, 255), 1.4f);
						Main.dust[bluishDust].noGravity = true;
						Main.dust[bluishDust].noLight = true;
						Dust obj = Main.dust[bluishDust];
						obj.velocity /= 4f;
						Dust obj2 = Main.dust[bluishDust];
						obj2.velocity -= base.Projectile.velocity;
					}
					base.Projectile.alpha -= 5;
					if (base.Projectile.alpha < 100)
					{
						base.Projectile.alpha = 100;
					}
					base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
					base.Projectile.frame = (int)(base.Projectile.localAI[0] / 3f) % 3;
				}
				Vector2 playerDirection = Main.player[playerTrack].Center - base.Projectile.Center;
				float projVelocity = 4f;
				projVelocity += base.Projectile.localAI[0] / 20f;
				base.Projectile.velocity = Vector2.Normalize(playerDirection) * projVelocity;
				if (((Vector2)(ref playerDirection)).Length() < 50f)
				{
					base.Projectile.Kill();
				}
			}
		}
		else
		{
			float spoutSpawn = (float)(Math.Cos((float)Math.PI / 15f * base.Projectile.ai[0]) - 0.5) * 4f;
			base.Projectile.velocity.Y = base.Projectile.velocity.Y - spoutSpawn;
			base.Projectile.ai[0]++;
			spoutSpawn = (float)(Math.Cos((float)Math.PI / 15f * base.Projectile.ai[0]) - 0.5) * 4f;
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + spoutSpawn;
			base.Projectile.localAI[0]++;
			if (base.Projectile.localAI[0] > 10f)
			{
				base.Projectile.alpha -= 5;
				if (base.Projectile.alpha < 100)
				{
					base.Projectile.alpha = 100;
				}
				base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
				base.Projectile.frame = (int)(base.Projectile.localAI[0] / 3f) % 3;
			}
		}
		if (base.Projectile.wet)
		{
			base.Projectile.position.Y = base.Projectile.position.Y - 16f;
			base.Projectile.Kill();
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item96, base.Projectile.Center);
		int moreDustAmt = 36;
		for (int j = 0; j < moreDustAmt; j++)
		{
			Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(j - (moreDustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)moreDustAmt) + base.Projectile.Center;
			Vector2 facingDirection = val - base.Projectile.Center;
			int killDust = Dust.NewDust(val + facingDirection, 0, 0, 187, facingDirection.X * 2f, facingDirection.Y * 2f, 100, new Color(53, Main.DiscoG, 255), 1.4f);
			Main.dust[killDust].noGravity = true;
			Main.dust[killDust].noLight = true;
			Main.dust[killDust].velocity = facingDirection;
		}
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		int projTileX = (int)(base.Projectile.Center.Y / 16f);
		int projTileY = (int)(base.Projectile.Center.X / 16f);
		int posModifier = 100;
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
		if (projTileX > Main.maxTilesY - posModifier - 10)
		{
			projTileX = Main.maxTilesY - posModifier - 10;
		}
		for (int k = projTileX; k < projTileX + posModifier; k++)
		{
			Tile tile = Main.tile[projTileY, k];
			if (tile.HasTile && (Main.tileSolid[tile.TileType] || tile.LiquidAmount != 0))
			{
				projTileX = k;
				break;
			}
		}
		int SPOUT = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), projTileY * 16 + 8, projTileX * 16 - 32, 0f, 0f, ModContent.ProjectileType<BrinySpout>(), base.Projectile.damage, 6f, Main.myPlayer, 3f, 7f);
		Main.projectile[SPOUT].netUpdate = true;
	}
}
