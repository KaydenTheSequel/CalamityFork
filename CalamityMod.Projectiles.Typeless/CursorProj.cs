using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class CursorProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

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
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_0578: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		//IL_059f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0600: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_060a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_063b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_0658: Unknown result type (might be due to invalid IL or missing references)
		//IL_0662: Unknown result type (might be due to invalid IL or missing references)
		//IL_0667: Unknown result type (might be due to invalid IL or missing references)
		//IL_0688: Unknown result type (might be due to invalid IL or missing references)
		//IL_068d: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_06af: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_088d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0893: Unknown result type (might be due to invalid IL or missing references)
		//IL_08aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_070e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0710: Unknown result type (might be due to invalid IL or missing references)
		//IL_0715: Unknown result type (might be due to invalid IL or missing references)
		//IL_071a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0722: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0752: Unknown result type (might be due to invalid IL or missing references)
		//IL_0769: Unknown result type (might be due to invalid IL or missing references)
		//IL_0773: Unknown result type (might be due to invalid IL or missing references)
		//IL_0778: Unknown result type (might be due to invalid IL or missing references)
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0790: Unknown result type (might be due to invalid IL or missing references)
		//IL_079e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0910: Unknown result type (might be due to invalid IL or missing references)
		//IL_0944: Unknown result type (might be due to invalid IL or missing references)
		//IL_094a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0961: Unknown result type (might be due to invalid IL or missing references)
		//IL_096b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0970: Unknown result type (might be due to invalid IL or missing references)
		//IL_097e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0989: Unknown result type (might be due to invalid IL or missing references)
		//IL_0993: Unknown result type (might be due to invalid IL or missing references)
		//IL_0998: Unknown result type (might be due to invalid IL or missing references)
		//IL_099d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 15;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.timeLeft > 285)
		{
			return;
		}
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
			int crystalDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, crystalDustType, 0f, 0f, 160);
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
		target.AddBuff(ModContent.BuffType<Vaporfied>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Vaporfied>(), 120);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 4f;
		for (int i = 0; i < 4; i++)
		{
			float offset = MathHelper.ToRadians(MathHelper.Lerp(-22.5f, 22.5f, (float)i / 3f));
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity.RotatedBy(offset), ModContent.ProjectileType<CursorProjSplit>(), base.Projectile.damage / 3, base.Projectile.knockBack * 0.33f, base.Projectile.owner);
		}
		SoundEngine.PlaySound(in SoundID.Item110, base.Projectile.Center);
	}
}
