using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class InfernadoMarkFriendly : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle FlareSound = new SoundStyle("CalamityMod/Sounds/Custom/Yharon/YharonInfernado");

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 5;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
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
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
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
						Vector2 randDustOffset = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - (float)Math.PI / 2f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
						int fiery = Dust.NewDust(val + randDustOffset, 0, 0, 244, randDustOffset.X * 2f, randDustOffset.Y * 2f, 100, default(Color), 1.4f);
						Main.dust[fiery].noGravity = true;
						Main.dust[fiery].noLight = true;
						Dust obj = Main.dust[fiery];
						obj.velocity /= 4f;
						Dust obj2 = Main.dust[fiery];
						obj2.velocity -= base.Projectile.velocity;
					}
					base.Projectile.alpha -= 5;
					if (base.Projectile.alpha < 100)
					{
						base.Projectile.alpha = 100;
					}
				}
				Vector2 projDirection = Main.player[playerTrack].Center - base.Projectile.Center;
				float velocityMult = 4f;
				velocityMult += base.Projectile.localAI[0] / 20f;
				base.Projectile.velocity = Vector2.Normalize(projDirection) * velocityMult;
				if (((Vector2)(ref projDirection)).Length() < 50f)
				{
					base.Projectile.Kill();
				}
			}
		}
		else
		{
			float XChangeMult = 4f;
			float projXChange = (float)(Math.Cos((float)Math.PI / 15f * base.Projectile.ai[0]) - 0.5) * XChangeMult;
			base.Projectile.velocity.Y = base.Projectile.velocity.Y - projXChange;
			base.Projectile.ai[0]++;
			projXChange = (float)(Math.Cos((float)Math.PI / 15f * base.Projectile.ai[0]) - 0.5) * XChangeMult;
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + projXChange;
			base.Projectile.localAI[0]++;
			if (base.Projectile.localAI[0] > 10f)
			{
				base.Projectile.alpha -= 5;
				if (base.Projectile.alpha < 100)
				{
					base.Projectile.alpha = 100;
				}
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
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in FlareSound, base.Projectile.Center);
		int dustAmt = 36;
		for (int i = 0; i < dustAmt; i++)
		{
			Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(i - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.Projectile.Center;
			Vector2 faceDirection = val - base.Projectile.Center;
			int infernadoDust = Dust.NewDust(val + faceDirection, 0, 0, 244, faceDirection.X * 2f, faceDirection.Y * 2f, 100, default(Color), 1.4f);
			Main.dust[infernadoDust].noGravity = true;
			Main.dust[infernadoDust].noLight = true;
			Main.dust[infernadoDust].velocity = faceDirection;
		}
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		int projTileY = (int)(base.Projectile.Center.Y / 16f);
		int projTileX = (int)(base.Projectile.Center.X / 16f);
		int offsetUpwards = 100;
		if (projTileX < 10)
		{
			projTileX = 10;
		}
		if (projTileX > Main.maxTilesX - 10)
		{
			projTileX = Main.maxTilesX - 10;
		}
		if (projTileY < 10)
		{
			projTileY = 10;
		}
		if (projTileY > Main.maxTilesY - offsetUpwards - 10)
		{
			projTileY = Main.maxTilesY - offsetUpwards - 10;
		}
		for (int j = projTileY; j < projTileY + offsetUpwards; j++)
		{
			Tile tile = Main.tile[projTileX, j];
			if (tile.HasTile && (Main.tileSolid[tile.TileType] || tile.LiquidAmount != 0))
			{
				projTileY = j;
				break;
			}
		}
		int infernado = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), projTileX * 16 + 8, projTileY * 16 - 24, 0f, 0f, ModContent.ProjectileType<InfernadoFriendly>(), base.Projectile.damage, base.Projectile.knockBack * 30f, Main.myPlayer, 16f, 16f);
		Main.projectile[infernado].netUpdate = true;
	}
}
