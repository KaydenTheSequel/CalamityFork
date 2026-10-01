using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class PlasmaCasterShot : ModProjectile, ILocalizedModType, IModType
{
	private int dust1 = 107;

	private int dust2 = 110;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 7;
		base.Projectile.timeLeft = 600;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0f, 0.6f, 0f);
		int dustTypeOnTimer = dust1;
		float createDustVar = 10f;
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > createDustVar)
		{
			for (int i = 0; i < 2; i++)
			{
				Vector2 dustRotation = base.Projectile.position;
				dustRotation -= base.Projectile.velocity * ((float)i * 0.25f);
				int dustSpawn = Dust.NewDust(dustRotation, 1, 1, dustTypeOnTimer);
				Main.dust[dustSpawn].noGravity = true;
				Main.dust[dustSpawn].position = dustRotation;
				Main.dust[dustSpawn].scale = (float)Main.rand.Next(70, 110) * 0.013f;
				Dust obj = Main.dust[dustSpawn];
				obj.velocity *= 0.2f;
			}
			base.Projectile.ai[0]++;
			if (base.Projectile.ai[0] == 48f)
			{
				base.Projectile.ai[0] = 0f;
				if (dustTypeOnTimer == dust1)
				{
					dustTypeOnTimer = dust2;
				}
				else
				{
					dustTypeOnTimer = dust1;
				}
			}
			else
			{
				Vector2 dustRotateVector = default(Vector2);
				((Vector2)(ref dustRotateVector))._002Ector(5f, 10f);
				for (int j = 0; j < 2; j++)
				{
					Vector2 dustRotate = Vector2.UnitX * -12f;
					dustRotate = -Vector2.UnitY.RotatedBy(base.Projectile.ai[0] * ((float)Math.PI / 24f) + (float)j * (float)Math.PI) * dustRotateVector * 0.75f;
					int plasmaDust = Dust.NewDust(base.Projectile.Center, 0, 0, dust1, 0f, 0f, 160);
					Main.dust[plasmaDust].scale = 0.6f;
					Main.dust[plasmaDust].noGravity = true;
					Main.dust[plasmaDust].position = base.Projectile.Center + dustRotate;
					Main.dust[plasmaDust].velocity = base.Projectile.velocity;
				}
				for (int k = 0; k < 2; k++)
				{
					Vector2 dustRotate2 = Vector2.UnitX * -12f;
					dustRotate2 = -Vector2.UnitY.RotatedBy(base.Projectile.ai[0] * ((float)Math.PI / 24f) + (float)k * (float)Math.PI) * dustRotateVector * 1.5f;
					int plasmaDust2 = Dust.NewDust(base.Projectile.Center, 0, 0, dust2, 0f, 0f, 160);
					Main.dust[plasmaDust2].scale = 0.6f;
					Main.dust[plasmaDust2].noGravity = true;
					Main.dust[plasmaDust2].position = base.Projectile.Center + dustRotate2;
					Main.dust[plasmaDust2].velocity = base.Projectile.velocity;
				}
			}
		}
		if (base.Projectile.localAI[0] == createDustVar)
		{
			PlasmaBurst(1f, 1.6f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(240);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Item93, base.Projectile.Center);
		PlasmaBurst(1.8f, 3.6f);
		for (int i = 0; i < 400; i++)
		{
			float dustScale = 16f;
			if (i < 300)
			{
				dustScale = 12f;
			}
			if (i < 200)
			{
				dustScale = 8f;
			}
			if (i < 100)
			{
				dustScale = 4f;
			}
			int deathDust = Dust.NewDust(base.Projectile.Center, 6, 6, Main.rand.NextBool() ? dust1 : dust2, 0f, 0f, 100);
			float deathDustX = Main.dust[deathDust].velocity.X;
			float deathDustY = Main.dust[deathDust].velocity.Y;
			if (deathDustX == 0f && deathDustY == 0f)
			{
				deathDustX = 1f;
			}
			float deathDustVel = (float)Math.Sqrt(deathDustX * deathDustX + deathDustY * deathDustY);
			deathDustVel = dustScale / deathDustVel;
			deathDustX *= deathDustVel;
			deathDustY *= deathDustVel;
			float scale = 1f;
			switch ((int)dustScale)
			{
			case 4:
				scale = 1.2f;
				break;
			case 8:
				scale = 1.1f;
				break;
			case 12:
				scale = 1f;
				break;
			case 16:
				scale = 0.9f;
				break;
			}
			Dust dust = Main.dust[deathDust];
			dust.velocity *= 0.5f;
			dust.velocity.X = dust.velocity.X + deathDustX;
			dust.velocity.Y = dust.velocity.Y + deathDustY;
			dust.scale = scale;
			dust.noGravity = true;
		}
	}

	private void PlasmaBurst(float speed1, float speed2)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0357: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		float angleRandom = 0.35f;
		for (int i = 0; i < 40; i++)
		{
			Vector2 dustVel = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(speed1, speed2), 0f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
			dustVel = dustVel.RotatedBy(0f - angleRandom);
			dustVel = dustVel.RotatedByRandom(2f * angleRandom);
			int randomDustType = (Main.rand.NextBool(2) ? dust1 : dust2);
			int plasmaBurstDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, randomDustType, dustVel.X, dustVel.Y, 200, default(Color), 1.7f);
			Main.dust[plasmaBurstDust].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
			Main.dust[plasmaBurstDust].noGravity = true;
			Dust obj = Main.dust[plasmaBurstDust];
			obj.velocity *= 3f;
			_ = Main.dust[plasmaBurstDust];
			plasmaBurstDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, randomDustType, dustVel.X, dustVel.Y, 100, default(Color), 0.8f);
			Main.dust[plasmaBurstDust].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
			Dust obj2 = Main.dust[plasmaBurstDust];
			obj2.velocity *= 2f;
			Main.dust[plasmaBurstDust].noGravity = true;
			Main.dust[plasmaBurstDust].fadeIn = 1f;
			Main.dust[plasmaBurstDust].color = Color.Green * 0.5f;
			_ = Main.dust[plasmaBurstDust];
		}
		for (int j = 0; j < 20; j++)
		{
			Vector2 dustVel2 = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(speed1, speed2), 0f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
			dustVel2 = dustVel2.RotatedBy(0f - angleRandom);
			dustVel2 = dustVel2.RotatedByRandom(2f * angleRandom);
			int randomDustType2 = (Main.rand.NextBool(2) ? dust1 : dust2);
			int plasmaBurstDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, randomDustType2, dustVel2.X, dustVel2.Y, 0, default(Color), 2f);
			Main.dust[plasmaBurstDust2].position = base.Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(base.Projectile.velocity.ToRotation()) * (float)base.Projectile.width / 3f;
			Main.dust[plasmaBurstDust2].noGravity = true;
			Dust obj3 = Main.dust[plasmaBurstDust2];
			obj3.velocity *= 0.5f;
			_ = Main.dust[plasmaBurstDust2];
		}
	}
}
