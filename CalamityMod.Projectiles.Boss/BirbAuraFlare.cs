using System;
using System.IO;
using CalamityMod.NPCs.Bumblebirb;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class BirbAuraFlare : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 1200;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		if (!(base.Projectile.ai[1] > 0f))
		{
			return;
		}
		int playerTracker = (int)base.Projectile.ai[1] - 1;
		if (playerTracker >= 255)
		{
			return;
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 10f)
		{
			base.Projectile.localAI[1] = (float)Math.Abs(Math.Cos(MathHelper.ToRadians(base.Projectile.localAI[0] * 2f)));
			int dustAmt = 18;
			for (int i = 0; i < dustAmt; i++)
			{
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * base.Projectile.localAI[1]).RotatedBy((double)(i - (dustAmt / 2 - 1)) * Math.PI / (double)(float)dustAmt) + base.Projectile.Center;
				Vector2 randomDustPos = ((float)(Main.rand.NextDouble() * Math.PI) - (float)Math.PI / 2f).ToRotationVector2() * (float)Main.rand.Next(3, 8);
				int lightningDust = Dust.NewDust(val + randomDustPos, 0, 0, 60, randomDustPos.X * 2f, randomDustPos.Y * 2f, 100);
				Main.dust[lightningDust].scale = 1.4f;
				Main.dust[lightningDust].noGravity = true;
				Main.dust[lightningDust].noLight = true;
				Dust obj = Main.dust[lightningDust];
				obj.velocity /= 4f;
				Dust obj2 = Main.dust[lightningDust];
				obj2.velocity -= base.Projectile.velocity;
			}
		}
		Vector2 playerDistance = Main.player[playerTracker].Center - base.Projectile.Center;
		float projVelocityMult = 4f;
		float divisor = 60f - 15f * base.Projectile.ai[0];
		projVelocityMult += base.Projectile.localAI[0] / divisor;
		base.Projectile.velocity = Vector2.Normalize(playerDistance) * projVelocityMult;
		if (((Vector2)(ref playerDistance)).Length() < 32f)
		{
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
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.DD2_BetsyFireballImpact, base.Projectile.Center);
		int killDustAmt = 36;
		for (int i = 0; i < killDustAmt; i++)
		{
			Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(i - (killDustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)killDustAmt) + base.Projectile.Center;
			Vector2 vector7 = val - base.Projectile.Center;
			int killLightningDust = Dust.NewDust(val + vector7, 0, 0, 60, vector7.X, vector7.Y, 100, default(Color), 1.4f);
			Main.dust[killLightningDust].noGravity = true;
			Main.dust[killLightningDust].noLight = true;
			Main.dust[killLightningDust].velocity = vector7;
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
		float x = projTileY * 16;
		float y = projTileX * 16 + 900;
		Vector2 laserVelocity = new Vector2(x, 160f) - new Vector2(x, y);
		int type = ModContent.ProjectileType<BirbAura>();
		int damage = Dragonfolly.LightningDamage;
		if (base.Projectile.ai[0] >= 2f)
		{
			x += 1000f;
			if ((int)(x / 16f) > Main.maxTilesX - 10)
			{
				x = (float)(Main.maxTilesX - 10) * 16f;
			}
			laserVelocity = new Vector2(x, 160f) - new Vector2(x, y);
			((Vector2)(ref laserVelocity)).Normalize();
			int thirdPhaseRightLaser = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), x, y, 0f, laserVelocity.Y, type, damage, 0f, Main.myPlayer, x, y);
			Main.projectile[thirdPhaseRightLaser].timeLeft = 900;
			Main.projectile[thirdPhaseRightLaser].netUpdate = true;
			x -= 2000f;
			if ((int)(x / 16f) < 10)
			{
				x = 160f;
			}
			laserVelocity = new Vector2(x, 160f) - new Vector2(x, y);
			((Vector2)(ref laserVelocity)).Normalize();
			int thirdPhaseLeftLaser = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), x, y, 0f, laserVelocity.Y, type, damage, 0f, Main.myPlayer, x, y);
			Main.projectile[thirdPhaseLeftLaser].timeLeft = 900;
			Main.projectile[thirdPhaseLeftLaser].netUpdate = true;
		}
		else
		{
			((Vector2)(ref laserVelocity)).Normalize();
			int secondPhaseLaser = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), x, y, 0f, laserVelocity.Y, type, damage, 0f, Main.myPlayer, x, y);
			Main.projectile[secondPhaseLaser].netUpdate = true;
		}
	}
}
