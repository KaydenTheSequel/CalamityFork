using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class TeslaCannonShot : ModProjectile, ILocalizedModType, IModType
{
	private int[] dustArray = new int[7] { 56, 111, 137, 160, 206, 229, 226 };

	private int arcs;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 100;
		base.Projectile.timeLeft = 600;
	}

	public override void AI()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		bool notArcingProjectile = base.Projectile.ai[1] >= 0f;
		Lighting.AddLight(base.Projectile.Center, 0f, 0.3f, 0.4f);
		float createDustVar = 10f;
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > createDustVar)
		{
			for (int i = 0; i < 2; i++)
			{
				Vector2 projPos = base.Projectile.position;
				projPos -= base.Projectile.velocity * ((float)i * 0.25f);
				int teslaDust = Dust.NewDust(projPos, 1, 1, dustArray[3]);
				Main.dust[teslaDust].noGravity = true;
				Main.dust[teslaDust].position = projPos;
				Main.dust[teslaDust].scale = (float)Main.rand.Next(70, 110) * 0.026f;
			}
			if (Main.rand.NextBool(6))
			{
				Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, dustArray[6]);
			}
			if (notArcingProjectile)
			{
				if (base.Projectile.ai[1] > 0f)
				{
					base.Projectile.ai[1]--;
				}
				base.Projectile.ai[0]++;
				if (base.Projectile.ai[0] == 48f)
				{
					base.Projectile.ai[0] = 0f;
				}
				else
				{
					Vector2 dustRotateVector = default(Vector2);
					((Vector2)(ref dustRotateVector))._002Ector(5f, 10f);
					Vector2 dustRotate = Vector2.UnitX * -12f;
					float scale = 1f;
					for (int j = 0; j < 2; j++)
					{
						dustRotate = -Vector2.UnitY.RotatedBy(base.Projectile.ai[0] * ((float)Math.PI / 24f) + (float)j * (float)Math.PI) * dustRotateVector;
						int lightBlueDust = Dust.NewDust(base.Projectile.Center, 0, 0, dustArray[2], 0f, 0f, 160);
						Main.dust[lightBlueDust].scale = scale;
						Main.dust[lightBlueDust].noGravity = true;
						Main.dust[lightBlueDust].position = base.Projectile.Center + dustRotate;
					}
					for (int k = 0; k < 2; k++)
					{
						dustRotate = -Vector2.UnitY.RotatedBy(base.Projectile.ai[0] * ((float)Math.PI / 24f) + (float)k * (float)Math.PI) * dustRotateVector * 1.5f;
						int lightBlueDust2 = Dust.NewDust(base.Projectile.Center, 0, 0, dustArray[1], 0f, 0f, 160);
						Main.dust[lightBlueDust2].scale = scale;
						Main.dust[lightBlueDust2].noGravity = true;
						Main.dust[lightBlueDust2].position = base.Projectile.Center + dustRotate;
					}
					for (int l = 0; l < 2; l++)
					{
						dustRotate = -Vector2.UnitY.RotatedBy(base.Projectile.ai[0] * ((float)Math.PI / 24f) + (float)l * (float)Math.PI) * dustRotateVector * 2f;
						int lightBlueDust3 = Dust.NewDust(base.Projectile.Center, 0, 0, dustArray[0], 0f, 0f, 160);
						Main.dust[lightBlueDust3].scale = scale;
						Main.dust[lightBlueDust3].noGravity = true;
						Main.dust[lightBlueDust3].position = base.Projectile.Center + dustRotate;
					}
				}
				if (base.Projectile.ai[1] == 0f && Main.myPlayer == base.Projectile.owner && arcs < 10)
				{
					ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
					while (enumerator.MoveNext())
					{
						NPC n = enumerator.Current;
						if (n.CanBeChasedBy(base.Projectile) && Collision.CanHit(base.Projectile.Center, 1, 1, n.Center, 1, 1) && base.Projectile.Center.ManhattanDistance(n.Center) < 600f)
						{
							Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.SafeDirectionTo(n.Center) * 2f, base.Projectile.type, (int)((double)base.Projectile.damage * 0.6), base.Projectile.knockBack * 0.6f, base.Projectile.owner, 0f, -1f);
							base.Projectile.ai[1] = 60f;
							arcs++;
							break;
						}
					}
				}
			}
		}
		if ((base.Projectile.localAI[0] == createDustVar) & notArcingProjectile)
		{
			ElectricalBurst(5f, 9f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		bool num = base.Projectile.ai[1] >= 0f;
		int height = (num ? 120 : 60);
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = height);
		base.Projectile.Center = base.Projectile.position;
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Item125, base.Projectile.Center);
		int dustAmt = (num ? 400 : 100);
		int fourth = (num ? 100 : 25);
		int half = (num ? 200 : 50);
		int threeFourths = (num ? 300 : 75);
		float dustSpeed = (num ? 12f : 6f);
		for (int i = 0; i < dustAmt; i++)
		{
			int dustType = dustArray[4];
			float deathDustSpeed = dustSpeed;
			if (i > fourth)
			{
				deathDustSpeed = dustSpeed * 0.6875f;
				dustType = dustArray[5];
			}
			if (i > half)
			{
				deathDustSpeed = dustSpeed * 0.5f;
				dustType = dustArray[4];
			}
			if (i > threeFourths)
			{
				deathDustSpeed = dustSpeed * 0.3125f;
				dustType = dustArray[5];
			}
			float scale = ((dustType == dustArray[4]) ? 3f : 1.5f);
			int deathDarkBlue = Dust.NewDust(base.Projectile.Center, 6, 6, dustType, 0f, 0f, 100, default(Color), scale);
			float deathDustX = Main.dust[deathDarkBlue].velocity.X;
			float deathDustY = Main.dust[deathDarkBlue].velocity.Y;
			if (deathDustX == 0f && deathDustY == 0f)
			{
				deathDustX = 1f;
			}
			float deathDustVel = (float)Math.Sqrt(deathDustX * deathDustX + deathDustY * deathDustY);
			deathDustVel = deathDustSpeed / deathDustVel;
			if (i > threeFourths)
			{
				deathDustX = deathDustX * deathDustVel * 0.7f;
				deathDustY *= deathDustVel;
			}
			else if (i > half)
			{
				deathDustX *= deathDustVel;
				deathDustY = deathDustY * deathDustVel * 0.7f;
			}
			else if (i > fourth)
			{
				deathDustX = deathDustX * deathDustVel * 0.7f;
				deathDustY *= deathDustVel;
			}
			else
			{
				deathDustX *= deathDustVel;
				deathDustY = deathDustY * deathDustVel * 0.7f;
			}
			Dust dust8 = Main.dust[deathDarkBlue];
			dust8.velocity *= 0.5f;
			dust8.velocity.X = dust8.velocity.X + deathDustX;
			dust8.velocity.Y = dust8.velocity.Y + deathDustY;
			dust8.noGravity = true;
			if (i > threeFourths)
			{
				int tealDust = Dust.NewDust(base.Projectile.Center, 6, 6, dustArray[5], 0f, 0f, 100, default(Color), 1.3f);
				float tealDustX = Main.dust[tealDust].velocity.X;
				float tealDustY = Main.dust[tealDust].velocity.Y;
				if (tealDustX == 0f && tealDustY == 0f)
				{
					tealDustX = 1f;
				}
				float tealDustVel = (float)Math.Sqrt(tealDustX * tealDustX + tealDustY * tealDustY);
				tealDustVel = 16f / tealDustVel;
				tealDustX = tealDustX * tealDustVel * 1.25f;
				tealDustY = tealDustY * tealDustVel * 0.75f;
				Dust dust9 = Main.dust[tealDust];
				dust9.velocity *= 0.5f;
				dust9.velocity.X = dust9.velocity.X + tealDustX;
				dust9.velocity.Y = dust9.velocity.Y + tealDustY;
				dust9.noGravity = true;
			}
		}
	}

	private void ElectricalBurst(float speed1, float speed2)
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
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0388: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		float angleRandom = 0.05f;
		for (int i = 0; i < 40; i++)
		{
			Vector2 dustVel = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(speed1, speed2), 0f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
			dustVel = dustVel.RotatedBy(0f - angleRandom);
			dustVel = dustVel.RotatedByRandom(2f * angleRandom);
			int randomDustType = (Main.rand.NextBool(2) ? dustArray[4] : dustArray[5]);
			float scale = ((randomDustType == dustArray[4]) ? 1.5f : 1f);
			int electricDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, randomDustType, dustVel.X, dustVel.Y, 200, default(Color), 2.5f * scale);
			Main.dust[electricDust].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
			Main.dust[electricDust].noGravity = true;
			Dust obj = Main.dust[electricDust];
			obj.velocity *= 3f;
			_ = Main.dust[electricDust];
			electricDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, randomDustType, dustVel.X, dustVel.Y, 100, default(Color), 1.5f * scale);
			Main.dust[electricDust].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
			Dust obj2 = Main.dust[electricDust];
			obj2.velocity *= 2f;
			Main.dust[electricDust].noGravity = true;
			Main.dust[electricDust].fadeIn = 1f;
			Main.dust[electricDust].color = Color.Cyan * 0.5f;
			_ = Main.dust[electricDust];
		}
		for (int j = 0; j < 20; j++)
		{
			Vector2 dustVel2 = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(speed1, speed2), 0f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
			dustVel2 = dustVel2.RotatedBy(0f - angleRandom);
			dustVel2 = dustVel2.RotatedByRandom(2f * angleRandom);
			int randomDustType2 = (Main.rand.NextBool(2) ? dustArray[4] : dustArray[5]);
			float scale2 = ((randomDustType2 == dustArray[4]) ? 1.5f : 1f);
			int electricDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, randomDustType2, dustVel2.X, dustVel2.Y, 0, default(Color), 3f * scale2);
			Main.dust[electricDust2].position = base.Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(base.Projectile.velocity.ToRotation()) * (float)base.Projectile.width / 3f;
			Main.dust[electricDust2].noGravity = true;
			Dust obj3 = Main.dust[electricDust2];
			obj3.velocity *= 0.5f;
			_ = Main.dust[electricDust2];
		}
	}
}
