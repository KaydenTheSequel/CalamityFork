using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class CursorProjSplit : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/Typeless/CursorProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_0576: Unknown result type (might be due to invalid IL or missing references)
		//IL_057b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0603: Unknown result type (might be due to invalid IL or missing references)
		//IL_0608: Unknown result type (might be due to invalid IL or missing references)
		//IL_0610: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_063f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_0660: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0857: Unknown result type (might be due to invalid IL or missing references)
		//IL_088b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0891: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08da: Unknown result type (might be due to invalid IL or missing references)
		//IL_08df: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0706: Unknown result type (might be due to invalid IL or missing references)
		//IL_070c: Unknown result type (might be due to invalid IL or missing references)
		//IL_070e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0713: Unknown result type (might be due to invalid IL or missing references)
		//IL_0718: Unknown result type (might be due to invalid IL or missing references)
		//IL_0720: Unknown result type (might be due to invalid IL or missing references)
		//IL_074a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0767: Unknown result type (might be due to invalid IL or missing references)
		//IL_0771: Unknown result type (might be due to invalid IL or missing references)
		//IL_0776: Unknown result type (might be due to invalid IL or missing references)
		//IL_0789: Unknown result type (might be due to invalid IL or missing references)
		//IL_078e: Unknown result type (might be due to invalid IL or missing references)
		//IL_079c: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_090e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0942: Unknown result type (might be due to invalid IL or missing references)
		//IL_0948: Unknown result type (might be due to invalid IL or missing references)
		//IL_095f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0969: Unknown result type (might be due to invalid IL or missing references)
		//IL_096e: Unknown result type (might be due to invalid IL or missing references)
		//IL_097c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0987: Unknown result type (might be due to invalid IL or missing references)
		//IL_0991: Unknown result type (might be due to invalid IL or missing references)
		//IL_0996: Unknown result type (might be due to invalid IL or missing references)
		//IL_099b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 3;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		float aiTrack = 5f;
		float scaleFactor = 6f;
		int dustType = Utils.SelectRandom<int>(Main.rand, 246, 242, 229, 226, 247);
		int crystalDustType = 255;
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.ai[1] = 1f;
			base.Projectile.localAI[0] = 0f - (float)Main.rand.Next(48);
		}
		else if (base.Projectile.ai[1] == 1f && base.Projectile.owner == Main.myPlayer)
		{
			if (base.Projectile.alpha < 128)
			{
				int targetID = -1;
				float hitDistance = 300f;
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC n = enumerator.Current;
					if (n.CanBeChasedBy(base.Projectile))
					{
						Vector2 targetCenter = n.Center;
						float targetDist = Vector2.Distance(targetCenter, base.Projectile.Center);
						if (targetDist < hitDistance && targetID == -1 && Collision.CanHitLine(base.Projectile.Center, 1, 1, targetCenter, 1, 1))
						{
							hitDistance = targetDist;
							targetID = n.whoAmI;
						}
					}
				}
				if (hitDistance < 4f)
				{
					base.Projectile.Kill();
					return;
				}
				if (targetID != -1)
				{
					base.Projectile.ai[1] = aiTrack + 1f;
					base.Projectile.ai[0] = targetID;
					base.Projectile.netUpdate = true;
				}
			}
		}
		else if (base.Projectile.ai[1] > aiTrack)
		{
			base.Projectile.ai[1]++;
			int npcTrack = (int)base.Projectile.ai[0];
			if (!Main.npc[npcTrack].active || !Main.npc[npcTrack].CanBeChasedBy(base.Projectile))
			{
				base.Projectile.ai[1] = 1f;
				base.Projectile.ai[0] = 0f;
				base.Projectile.netUpdate = true;
			}
			else
			{
				base.Projectile.velocity.ToRotation();
				Vector2 npcDirection = Main.npc[npcTrack].Center - base.Projectile.Center;
				if (((Vector2)(ref npcDirection)).Length() < 10f)
				{
					base.Projectile.Kill();
					return;
				}
				if (npcDirection != Vector2.Zero)
				{
					((Vector2)(ref npcDirection)).Normalize();
					npcDirection *= scaleFactor;
				}
				base.Projectile.velocity = (base.Projectile.velocity * 29f + npcDirection) / 30f;
			}
		}
		if (base.Projectile.ai[1] >= 1f && base.Projectile.ai[1] < aiTrack)
		{
			base.Projectile.ai[1]++;
			if (base.Projectile.ai[1] == aiTrack)
			{
				base.Projectile.ai[1] = 1f;
			}
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] == 48f)
		{
			base.Projectile.localAI[0] = 0f;
		}
		if (Main.rand.NextBool(12))
		{
			Vector2 rotateFirstDust = -Vector2.UnitX.RotatedByRandom(0.19634954631328583).RotatedBy(base.Projectile.velocity.ToRotation());
			int crystalDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, crystalDustType, 0f, 0f, 100);
			Dust obj = Main.dust[crystalDust];
			obj.velocity *= 0.1f;
			Main.dust[crystalDust].position = base.Projectile.Center + rotateFirstDust * (float)base.Projectile.width / 2f + base.Projectile.velocity * 2f;
			Main.dust[crystalDust].fadeIn = 0.9f;
		}
		if (Main.rand.NextBool(18))
		{
			Vector2 rotateSecondDust = -Vector2.UnitX.RotatedByRandom(0.39269909262657166).RotatedBy(base.Projectile.velocity.ToRotation());
			int greenDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 107, 0f, 0f, 155, default(Color), 0.8f);
			Dust obj2 = Main.dust[greenDust];
			obj2.velocity *= 0.3f;
			Main.dust[greenDust].position = base.Projectile.Center + rotateSecondDust * (float)base.Projectile.width / 2f;
			if (Main.rand.NextBool())
			{
				Main.dust[greenDust].fadeIn = 1.4f;
			}
		}
		if (Main.rand.NextBool(8))
		{
			Vector2 rotateThirdDust = -Vector2.UnitX.RotatedByRandom(0.7853981852531433).RotatedBy(base.Projectile.velocity.ToRotation());
			int randomDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType);
			Dust obj3 = Main.dust[randomDust];
			obj3.velocity *= 0.3f;
			Main.dust[randomDust].noGravity = true;
			Main.dust[randomDust].position = base.Projectile.Center + rotateThirdDust * (float)base.Projectile.width / 2f;
			if (Main.rand.NextBool())
			{
				Main.dust[randomDust].fadeIn = 1.4f;
			}
		}
		if (Main.rand.NextBool(6))
		{
			Vector2 value13 = -Vector2.UnitX.RotatedByRandom(0.19634954631328583).RotatedBy(base.Projectile.velocity.ToRotation());
			int crystalDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, crystalDustType, 0f, 0f, 100);
			Dust obj4 = Main.dust[crystalDust2];
			obj4.velocity *= 0.3f;
			Main.dust[crystalDust2].position = base.Projectile.Center + value13 * (float)base.Projectile.width / 2f;
			Main.dust[crystalDust2].fadeIn = 1.2f;
			Main.dust[crystalDust2].scale = 1.5f;
			Main.dust[crystalDust2].noGravity = true;
		}
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0.2f / 255f, (float)(255 - base.Projectile.alpha) * 0.2f / 255f, (float)(255 - base.Projectile.alpha) * 0.2f / 255f);
		int paleDust = Dust.NewDust(base.Projectile.position, base.Projectile.width - 28, base.Projectile.height - 28, 234, 0f, 0f, 100, default(Color), 0.8f);
		Dust obj5 = Main.dust[paleDust];
		obj5.velocity *= 0.1f;
		Dust obj6 = Main.dust[paleDust];
		obj6.velocity += base.Projectile.velocity * 0.5f;
		Main.dust[paleDust].noGravity = true;
		if (Main.rand.NextBool(12))
		{
			int shinyDust = Dust.NewDust(base.Projectile.position, base.Projectile.width - 32, base.Projectile.height - 32, 159, 0f, 0f, 100);
			Dust obj7 = Main.dust[shinyDust];
			obj7.velocity *= 0.25f;
			Dust obj8 = Main.dust[shinyDust];
			obj8.velocity += base.Projectile.velocity * 0.5f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Vaporfied>(), 60);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Vaporfied>(), 60);
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (base.Projectile.alpha >= 128)
		{
			return false;
		}
		return null;
	}

	public override bool CanHitPvp(Player target)
	{
		return base.Projectile.alpha < 128;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		int otherDustType = Utils.SelectRandom<int>(Main.rand, 246, 242, 229, 226, 247);
		int randomDust = 187;
		float crystalDust2 = 1.2f;
		Vector2 dustVel = (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * ((Vector2)(ref base.Projectile.velocity)).Length() * (float)base.Projectile.MaxUpdates;
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		for (int j = 0; j < 20; j++)
		{
			int superRandomDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, otherDustType, 0f, 0f, 200, default(Color), crystalDust2);
			Dust obj = Main.dust[superRandomDust];
			obj.position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
			obj.noGravity = true;
			obj.velocity.Y -= 6f;
			obj.velocity *= 3f;
			obj.velocity += dustVel * Main.rand.NextFloat();
			superRandomDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, randomDust, 0f, 0f, 100, default(Color), 0.6f);
			obj.position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
			obj.velocity.Y -= 6f;
			obj.velocity *= 2f;
			obj.noGravity = true;
			obj.fadeIn = 1f;
			obj.color = Color.Cyan * 0.5f;
			obj.velocity += dustVel * Main.rand.NextFloat();
		}
		for (int k = 0; k < 10; k++)
		{
			int palestDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 234, 0f, 0f, 0, default(Color), 1.5f);
			Main.dust[palestDust].position = base.Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(base.Projectile.velocity.ToRotation()) * (float)base.Projectile.width / 3f;
			Dust obj2 = Main.dust[palestDust];
			obj2.noGravity = true;
			obj2.velocity.Y -= 6f;
			obj2.velocity *= 0.5f;
			obj2.velocity += dustVel * (0.6f + 0.6f * Main.rand.NextFloat());
		}
	}
}
