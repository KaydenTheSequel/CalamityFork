using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class TyphoonBubble : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

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
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 1;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
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
			int playerOwner = (int)base.Projectile.ai[1] - 1;
			if (playerOwner < 255)
			{
				base.Projectile.localAI[0]++;
				if (base.Projectile.localAI[0] > 10f)
				{
					int six = 6;
					for (int i = 0; i < six; i++)
					{
						Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((double)(i - (six / 2 - 1)) * Math.PI / (double)(float)six) + base.Projectile.Center;
						Vector2 dustPos = ((float)(Main.rand.NextDouble() * 3.1415927410125732) - (float)Math.PI / 2f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
						int typhoonDust = Dust.NewDust(val + dustPos, 0, 0, 172, dustPos.X * 2f, dustPos.Y * 2f, 100, default(Color), 1.4f);
						Main.dust[typhoonDust].noGravity = true;
						Main.dust[typhoonDust].noLight = true;
						Dust obj = Main.dust[typhoonDust];
						obj.velocity /= 4f;
						Dust obj2 = Main.dust[typhoonDust];
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
				Vector2 playerDist = Main.player[playerOwner].Center - base.Projectile.Center;
				float velocityMult = 4f;
				velocityMult += base.Projectile.localAI[0] / 20f;
				base.Projectile.velocity = Vector2.Normalize(playerDist) * velocityMult;
				if (((Vector2)(ref playerDist)).Length() < 50f)
				{
					base.Projectile.Kill();
				}
			}
		}
		else
		{
			float smolWidth = 4f;
			float projXChange = (float)(Math.Cos((float)Math.PI / 15f * base.Projectile.ai[0]) - 0.5) * smolWidth;
			base.Projectile.velocity.Y = base.Projectile.velocity.Y - projXChange;
			base.Projectile.ai[0]++;
			projXChange = (float)(Math.Cos((float)Math.PI / 15f * base.Projectile.ai[0]) - 0.5) * smolWidth;
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + projXChange;
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
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCDeath19, base.Projectile.position);
		int constant = 36;
		for (int j = 0; j < constant; j++)
		{
			Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(j - (constant / 2 - 1)) * ((float)Math.PI * 2f) / (float)constant) + base.Projectile.Center;
			Vector2 faceDirection = val - base.Projectile.Center;
			int waterDust = Dust.NewDust(val + faceDirection, 0, 0, 172, faceDirection.X * 2f, faceDirection.Y * 2f, 100, default(Color), 1.4f);
			Main.dust[waterDust].noGravity = true;
			Main.dust[waterDust].noLight = true;
			Main.dust[waterDust].velocity = faceDirection;
		}
		if (base.Projectile.owner == Main.myPlayer && base.Projectile.ai[1] < 1f)
		{
			if (base.Projectile.localAI[1] == 1f)
			{
				int nextSegment = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X - (float)(base.Projectile.direction * 30), base.Projectile.Center.Y - 4f, (0f - (float)base.Projectile.direction) * 0.01f, 0f, ModContent.ProjectileType<SeasSearingSpout>(), base.Projectile.damage, 3f, base.Projectile.owner, 16f, 8f);
				Main.projectile[nextSegment].netUpdate = true;
			}
			else
			{
				int nextSegment2 = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X - (float)(base.Projectile.direction * 30), base.Projectile.Center.Y - 4f, (0f - (float)base.Projectile.direction) * 0.01f, 0f, ModContent.ProjectileType<WaterSpout>(), base.Projectile.damage, 3f, base.Projectile.owner, 16f, 8f);
				Main.projectile[nextSegment2].netUpdate = true;
			}
		}
	}
}
