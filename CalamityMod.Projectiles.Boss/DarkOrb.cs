using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class DarkOrb : ModProjectile, ILocalizedModType, IModType
{
	private bool start;

	private Vector2 center;

	private Vector2 velocity;

	private double[] distances;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/LightningProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 2;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 3600;
		base.CooldownSlot = 1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
		writer.Write(start);
		writer.WriteVector2(center);
		writer.Write(distances[0]);
		writer.Write(distances[1]);
		writer.Write(distances[2]);
		writer.Write(distances[3]);
		writer.WriteVector2(velocity);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
		start = reader.ReadBoolean();
		center = reader.ReadVector2();
		distances[0] = reader.ReadDouble();
		distances[1] = reader.ReadDouble();
		distances[2] = reader.ReadDouble();
		distances[3] = reader.ReadDouble();
		velocity = reader.ReadVector2();
	}

	public override void AI()
	{
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0966: Unknown result type (might be due to invalid IL or missing references)
		//IL_096b: Unknown result type (might be due to invalid IL or missing references)
		//IL_097c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0989: Unknown result type (might be due to invalid IL or missing references)
		//IL_0994: Unknown result type (might be due to invalid IL or missing references)
		//IL_0999: Unknown result type (might be due to invalid IL or missing references)
		//IL_099e: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c03: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c0e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c13: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c18: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c24: Unknown result type (might be due to invalid IL or missing references)
		//IL_087f: Unknown result type (might be due to invalid IL or missing references)
		//IL_088c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0897: Unknown result type (might be due to invalid IL or missing references)
		//IL_089c: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_090f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0916: Unknown result type (might be due to invalid IL or missing references)
		//IL_091b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0942: Unknown result type (might be due to invalid IL or missing references)
		//IL_094c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0951: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0544: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0dfe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0710: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_0718: Unknown result type (might be due to invalid IL or missing references)
		//IL_06da: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_0736: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_073a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] != 8f)
		{
			if (base.Projectile.localAI[1] == 0f)
			{
				base.Projectile.scale -= 0.012f;
				if (base.Projectile.scale <= 0.8f)
				{
					base.Projectile.scale = 0.8f;
					base.Projectile.localAI[1] = 1f;
				}
			}
			else
			{
				base.Projectile.scale += 0.012f;
				if (base.Projectile.scale >= 1.2f)
				{
					base.Projectile.scale = 1.2f;
					base.Projectile.localAI[1] = 0f;
				}
			}
		}
		switch ((int)base.Projectile.ai[0])
		{
		case 0:
		case 1:
		{
			if (start)
			{
				center = base.Projectile.Center;
				velocity = Vector2.Normalize(Main.player[Player.FindClosest(base.Projectile.Center, 1, 1)].Center - base.Projectile.Center) * 2f;
				start = false;
			}
			center += velocity;
			double rad4 = MathHelper.ToRadians(base.Projectile.ai[1]);
			float amount4 = 1f - base.Projectile.localAI[0] / 360f;
			if (amount4 < 0f)
			{
				amount4 = 0f;
			}
			distances[0] += MathHelper.Lerp(1f, 6f, amount4);
			if (base.Projectile.ai[0] == 0f)
			{
				base.Projectile.position.X = center.X - (float)(int)(Math.Sin(rad4) * distances[0]) - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = center.Y - (float)(int)(Math.Cos(rad4) * distances[0]) - (float)(base.Projectile.height / 2);
			}
			else
			{
				base.Projectile.position.X = center.X - (float)(int)(Math.Cos(rad4) * distances[0]) - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = center.Y - (float)(int)(Math.Sin(rad4) * distances[0]) - (float)(base.Projectile.height / 2);
			}
			base.Projectile.ai[1] += 0.25f + amount4;
			base.Projectile.localAI[0]++;
			break;
		}
		case 2:
			LurchForward(0.05f, 0.95f, 1.05f);
			break;
		case 3:
		case 4:
		{
			bool useSin3 = base.Projectile.ai[0] == 3f;
			WavyMotion(0.1f, 0f, useSin3, waveWithVelocity: false);
			break;
		}
		case 5:
		case 16:
		{
			float fastGateValue = ((base.Projectile.ai[0] == 5f) ? 30f : 60f);
			float maxVelocity = ((base.Projectile.ai[0] == 5f) ? 12f : 6f);
			LurchForwardOnTimer(90f, fastGateValue, 3f, maxVelocity, 0.95f, 1.2f);
			break;
		}
		case 6:
			base.Projectile.ai[1]++;
			if (!(base.Projectile.ai[1] >= 90f))
			{
				break;
			}
			if (base.Projectile.owner == Main.myPlayer)
			{
				int totalProjectiles = 4;
				float radians = (float)Math.PI * 2f / (float)totalProjectiles;
				for (int i = 0; i < totalProjectiles; i++)
				{
					Vector2 vector = Utils.RotatedBy(new Vector2(0f, -8f), (double)(radians * (float)i), default(Vector2));
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, vector, base.Projectile.type, base.Projectile.damage, 0f, Main.myPlayer, Main.rand.Next(6));
				}
			}
			base.Projectile.Kill();
			break;
		case 7:
			base.Projectile.ai[1]++;
			if (!(base.Projectile.ai[1] >= 120f))
			{
				break;
			}
			if (base.Projectile.owner == Main.myPlayer)
			{
				int totalProjectiles2 = 8;
				float radians2 = (float)Math.PI * 2f / (float)totalProjectiles2;
				for (int j = 0; j < totalProjectiles2; j++)
				{
					Vector2 vector2 = Utils.RotatedBy(new Vector2(0f, -8f), (double)(radians2 * (float)j), default(Vector2));
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, vector2, base.Projectile.type, base.Projectile.damage, 0f, Main.myPlayer, Main.rand.Next(2));
				}
			}
			base.Projectile.Kill();
			break;
		case 8:
		{
			bool splitOnce = base.Projectile.timeLeft < 1800;
			bool splitTwice = base.Projectile.timeLeft < 900;
			bool splitThrice = base.Projectile.timeLeft < 450;
			if (splitOnce)
			{
				WavyMotion(0.05f, 2f, useSin: true, waveWithVelocity: true);
			}
			base.Projectile.localAI[0]++;
			if (!(base.Projectile.localAI[0] >= 180f))
			{
				break;
			}
			if (base.Projectile.owner == Main.myPlayer)
			{
				int totalProjectiles3 = 3;
				float radians3 = (float)Math.PI * 2f / (float)totalProjectiles3;
				int spread = 8;
				if (splitOnce)
				{
					spread *= (splitTwice ? 3 : 0) + (splitThrice ? 3 : 0);
				}
				Vector2 vector3 = Main.player[Player.FindClosest(base.Projectile.Center, 1, 1)].Center - base.Projectile.Center;
				((Vector2)(ref vector3)).Normalize();
				vector3 *= 3f;
				for (int k = 0; k < totalProjectiles3; k++)
				{
					Vector2 vector4 = (splitOnce ? base.Projectile.velocity.RotatedBy(MathHelper.ToRadians((float)(k * spread))) : Utils.RotatedBy(new Vector2(0f, -3f), (double)(radians3 * (float)k), default(Vector2)));
					int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, vector3 + vector4, base.Projectile.type, base.Projectile.damage, 0f, Main.myPlayer, 8f);
					Main.projectile[proj].timeLeft = base.Projectile.timeLeft / 2;
					if (Main.projectile[proj].timeLeft < 150)
					{
						Main.projectile[proj].timeLeft = 150;
					}
					Main.projectile[proj].scale = base.Projectile.scale * 0.6f;
				}
			}
			base.Projectile.Kill();
			break;
		}
		case 9:
		case 10:
		{
			bool useSin2 = base.Projectile.ai[0] == 9f;
			OscillationMotion(0.05f, useSin2);
			break;
		}
		case 11:
			base.Projectile.ai[1]++;
			if (base.Projectile.ai[1] >= 180f)
			{
				base.Projectile.localAI[0]++;
				if (base.Projectile.localAI[0] < 180f)
				{
					Vector2 vector5 = Main.player[Player.FindClosest(base.Projectile.Center, 1, 1)].Center - base.Projectile.Center;
					float scaleFactor = ((Vector2)(ref base.Projectile.velocity)).Length();
					((Vector2)(ref vector5)).Normalize();
					vector5 *= scaleFactor;
					base.Projectile.velocity = (base.Projectile.velocity * 15f + vector5) / 16f;
					((Vector2)(ref base.Projectile.velocity)).Normalize();
					Projectile projectile = base.Projectile;
					projectile.velocity *= scaleFactor;
				}
				else if (((Vector2)(ref base.Projectile.velocity)).Length() < 18f)
				{
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= 1.01f;
				}
			}
			break;
		case 12:
		case 13:
		{
			if (start)
			{
				center = base.Projectile.Center;
				velocity = Vector2.Normalize(Main.player[Player.FindClosest(base.Projectile.Center, 1, 1)].Center - base.Projectile.Center);
				start = false;
			}
			center += velocity;
			float velocityGateValue3 = 240f;
			float amount5 = 1f;
			bool flyOutward = base.Projectile.localAI[0] < velocityGateValue3;
			amount5 = ((!flyOutward) ? ((base.Projectile.localAI[0] - velocityGateValue3) / velocityGateValue3) : (amount5 - base.Projectile.localAI[0] / velocityGateValue3));
			amount5 = MathHelper.Clamp(0f, 1f, amount5);
			double distanceVariable = MathHelper.Lerp(0f, 6f, amount5);
			distances[0] += (flyOutward ? distanceVariable : (0.0 - distanceVariable));
			double rad5 = MathHelper.ToRadians(base.Projectile.ai[1]);
			if (base.Projectile.ai[0] == 12f)
			{
				base.Projectile.position.X = center.X - (float)(int)(Math.Sin(rad5) * distances[0] * 2.0) - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = center.Y - (float)(int)(Math.Cos(rad5) * distances[0]) - (float)(base.Projectile.height / 2);
			}
			else
			{
				base.Projectile.position.X = center.X - (float)(int)(Math.Cos(rad5) * distances[0]) - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = center.Y - (float)(int)(Math.Sin(rad5) * distances[0] * 2.0) - (float)(base.Projectile.height / 2);
			}
			base.Projectile.ai[1] += 0.25f + amount5;
			base.Projectile.localAI[0]++;
			break;
		}
		case 14:
		case 15:
		{
			float velocityMult = 12f;
			if (start)
			{
				center = base.Projectile.Center;
				velocity = Vector2.Normalize(Main.player[Player.FindClosest(base.Projectile.Center, 1, 1)].Center - base.Projectile.Center) * velocityMult;
				start = false;
			}
			center += velocity;
			float flyInwardGateValue = 240f;
			float amount3 = 1f - base.Projectile.localAI[0] / flyInwardGateValue;
			if (amount3 < 0f)
			{
				amount3 = 0f;
			}
			bool changeXPos = base.Projectile.ai[0] == 14f;
			float units = 6f;
			distances[0] += MathHelper.Lerp(1f, units, amount3);
			if (base.Projectile.localAI[0] > flyInwardGateValue)
			{
				distances[2] = (changeXPos ? Math.Abs(center.X - base.Projectile.Center.X) : Math.Abs(center.Y - base.Projectile.Center.Y));
				if (distances[3] == 0.0)
				{
					distances[3] = distances[2] - (double)(units * 10f);
				}
				distances[3] += (changeXPos ? velocity.X : velocity.Y);
				distances[1] -= ((distances[2] < (double)(units * 10f)) ? (distances[2] * 0.10000000149011612) : ((double)MathHelper.Lerp(units, units + 2f, (float)(distances[2] / distances[3]))));
			}
			else
			{
				float slowDownGateValue = 30f;
				if (base.Projectile.localAI[0] > flyInwardGateValue - slowDownGateValue)
				{
					velocity *= 1f - velocityMult / slowDownGateValue;
				}
				distances[1] = distances[0];
			}
			double distanceX = (changeXPos ? distances[1] : distances[0]);
			double distanceY = (changeXPos ? distances[0] : distances[1]);
			double rad3 = MathHelper.ToRadians(base.Projectile.ai[1]);
			base.Projectile.position.X = center.X - (float)(int)(Math.Sin(rad3) * distanceX) - (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = center.Y - (float)(int)(Math.Cos(rad3) * distanceY) - (float)(base.Projectile.height / 2);
			base.Projectile.localAI[0]++;
			if (base.Projectile.localAI[0] > 600f)
			{
				base.Projectile.Kill();
			}
			break;
		}
		case -1:
			break;
		}
	}

	private void WavyMotion(float frequency, float amplitude, bool useSin, bool waveWithVelocity)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		if (start)
		{
			velocity = base.Projectile.velocity;
			start = false;
		}
		base.Projectile.ai[1] += frequency;
		if (amplitude == 0f)
		{
			amplitude = ((Vector2)(ref velocity)).Length();
		}
		float wavyVelocity = (useSin ? ((float)Math.Sin(base.Projectile.ai[1])) : ((float)Math.Cos(base.Projectile.ai[1])));
		if (waveWithVelocity)
		{
			base.Projectile.velocity = velocity + new Vector2(wavyVelocity, wavyVelocity) * amplitude;
		}
		else
		{
			base.Projectile.velocity = velocity + Utils.RotatedBy(new Vector2(wavyVelocity, wavyVelocity), (double)MathHelper.ToRadians(velocity.ToRotation()), default(Vector2)) * amplitude;
		}
	}

	private void LurchForward(float frequncy, float deceleration, float acceleration)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[1] += frequncy;
		Projectile projectile = base.Projectile;
		projectile.velocity *= MathHelper.Lerp(deceleration, acceleration, (float)Math.Abs(Math.Sin(base.Projectile.ai[1])));
	}

	private void LurchForwardOnTimer(float slowGateValue, float fastGateValue, float minVelocity, float maxVelocity, float deceleration, float acceleration)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] <= slowGateValue)
		{
			if (((Vector2)(ref base.Projectile.velocity)).Length() > minVelocity)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= deceleration;
			}
		}
		else if (base.Projectile.ai[1] < slowGateValue + fastGateValue)
		{
			if (((Vector2)(ref base.Projectile.velocity)).Length() < maxVelocity)
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= acceleration;
			}
		}
		else
		{
			base.Projectile.ai[1] = 0f;
		}
	}

	private void OscillationMotion(float frequncy, bool useSin)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[1] += frequncy;
		float oscillation = (useSin ? ((float)Math.Sin(base.Projectile.ai[1])) : ((float)Math.Cos(base.Projectile.ai[1])));
		if (start)
		{
			velocity = base.Projectile.velocity;
			start = false;
		}
		else if (oscillation == 0f)
		{
			Player target = Main.player[Player.FindClosest(base.Projectile.Center, 1, 1)];
			Vector2 vector = target.Center + target.velocity * 20f - base.Projectile.Center;
			((Vector2)(ref vector)).Normalize();
			vector *= ((Vector2)(ref velocity)).Length();
			base.Projectile.velocity = vector;
		}
		else
		{
			float amplitude = ((Vector2)(ref velocity)).Length();
			((Vector2)(ref base.Projectile.velocity)).Normalize();
			Projectile projectile = base.Projectile;
			projectile.velocity *= amplitude * oscillation;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		switch ((int)base.Projectile.ai[0])
		{
		case -1:
			return new Color(0, 100, 255, base.Projectile.alpha);
		case 0:
		case 1:
			return new Color(0, 100, 255, base.Projectile.alpha);
		case 2:
			return new Color(255, 200, 0, base.Projectile.alpha);
		case 3:
		case 4:
			return new Color(0, 255, 200, base.Projectile.alpha);
		case 5:
		case 16:
			return new Color(0, 255, 100, base.Projectile.alpha);
		case 6:
			return new Color(200, 0, 255, base.Projectile.alpha);
		case 7:
			return new Color(255, 0, 150, base.Projectile.alpha);
		case 8:
			return new Color(200, 0, 0, base.Projectile.alpha);
		case 9:
		case 10:
			return new Color(255, 255, 0, base.Projectile.alpha);
		case 11:
			return new Color(150, 0, 200, base.Projectile.alpha);
		case 12:
		case 13:
			return new Color(0, 100, 255, base.Projectile.alpha);
		case 14:
		case 15:
			return new Color(0, 100, 255, base.Projectile.alpha);
		default:
			return null;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<GodSlayerInferno>(), 200);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public DarkOrb()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		start = true;
		center = Vector2.Zero;
		velocity = Vector2.Zero;
		distances = new double[4];
		base._002Ector();
	}
}
