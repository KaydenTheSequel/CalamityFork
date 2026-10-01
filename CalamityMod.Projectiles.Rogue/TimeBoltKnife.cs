using System;
using System.IO;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class TimeBoltKnife : ModProjectile, ILocalizedModType, IModType
{
	private int maxPenetrate = 6;

	private int penetrationAmt = 6;

	private bool initialized;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/TimeBolt";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 28;
		base.Projectile.height = 28;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = penetrationAmt;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(penetrationAmt);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		penetrationAmt = reader.ReadInt32();
	}

	public override void AI()
	{
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0647: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_051a: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_059a: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_06af: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0705: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0727: Unknown result type (might be due to invalid IL or missing references)
		//IL_072c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_0733: Unknown result type (might be due to invalid IL or missing references)
		//IL_0743: Unknown result type (might be due to invalid IL or missing references)
		//IL_0749: Unknown result type (might be due to invalid IL or missing references)
		//IL_074b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0752: Unknown result type (might be due to invalid IL or missing references)
		//IL_0754: Unknown result type (might be due to invalid IL or missing references)
		//IL_0759: Unknown result type (might be due to invalid IL or missing references)
		//IL_0763: Unknown result type (might be due to invalid IL or missing references)
		//IL_0768: Unknown result type (might be due to invalid IL or missing references)
		//IL_077f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0785: Unknown result type (might be due to invalid IL or missing references)
		//IL_079d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			if (base.Projectile.Calamity().stealthStrike)
			{
				maxPenetrate = 11;
				penetrationAmt = maxPenetrate;
			}
			initialized = true;
		}
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.03f;
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.tileCollide = true;
			base.Projectile.localAI[0]++;
			if (base.Projectile.localAI[0] > 7f)
			{
				int dustType = Utils.SelectRandom<int>(Main.rand, 226, 229);
				Vector2 center = base.Projectile.Center;
				Vector2 fourVector = default(Vector2);
				((Vector2)(ref fourVector))._002Ector(-4f, 4f);
				fourVector += new Vector2(-4f, 4f);
				fourVector = fourVector.RotatedBy(base.Projectile.rotation);
				int dust = Dust.NewDust(center + fourVector + Vector2.One * -4f, 8, 8, dustType, 0f, 0f, 100);
				Dust dust2 = Main.dust[dust];
				dust2.velocity *= 0.1f;
				if (!Main.rand.NextBool(6))
				{
					dust2.noGravity = true;
				}
			}
			float scalar = 0.01f;
			int alphaAmt = 5;
			int alphaCeiling = alphaAmt * 15;
			int alphaFloor = 0;
			if (base.Projectile.localAI[0] > 7f)
			{
				if (base.Projectile.localAI[1] == 0f)
				{
					base.Projectile.scale -= scalar;
					base.Projectile.alpha += alphaAmt;
					if (base.Projectile.alpha > alphaCeiling)
					{
						base.Projectile.alpha = alphaCeiling;
						base.Projectile.localAI[1] = 1f;
					}
				}
				else if (base.Projectile.localAI[1] == 1f)
				{
					base.Projectile.scale += scalar;
					base.Projectile.alpha -= alphaAmt;
					if (base.Projectile.alpha <= alphaFloor)
					{
						base.Projectile.alpha = alphaFloor;
						base.Projectile.localAI[1] = 0f;
					}
				}
			}
		}
		else if (base.Projectile.ai[0] >= 1f && base.Projectile.ai[0] < (float)(1 + penetrationAmt))
		{
			base.Projectile.tileCollide = false;
			base.Projectile.alpha += 15;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.98f;
			base.Projectile.localAI[0] = 0f;
			if (base.Projectile.alpha >= 255)
			{
				if (base.Projectile.ai[0] == 1f)
				{
					base.Projectile.Kill();
					return;
				}
				int whoAmI = -1;
				Vector2 targetSpot = base.Projectile.Center;
				float detectRange = 1000f;
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC npc = enumerator.Current;
					if (npc.CanBeChasedBy(base.Projectile))
					{
						float targetDist = Vector2.Distance(npc.Center, base.Projectile.Center);
						if (targetDist < detectRange)
						{
							detectRange = targetDist;
							targetSpot = npc.Center;
							whoAmI = npc.whoAmI;
						}
					}
				}
				if (whoAmI >= 0)
				{
					base.Projectile.netUpdate = true;
					base.Projectile.ai[0] += penetrationAmt;
					base.Projectile.position = targetSpot + ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * 100f - new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f;
					base.Projectile.velocity = Vector2.Normalize(targetSpot - base.Projectile.Center) * 18f;
				}
				else
				{
					base.Projectile.Kill();
				}
			}
			if (Main.rand.NextBool(3))
			{
				int dustType2 = Utils.SelectRandom<int>(Main.rand, 226, 229);
				Vector2 center2 = base.Projectile.Center;
				Vector2 otherFourVector = default(Vector2);
				((Vector2)(ref otherFourVector))._002Ector(-4f, 4f);
				otherFourVector += new Vector2(-4f, 4f);
				otherFourVector = otherFourVector.RotatedBy(base.Projectile.rotation);
				int dust3 = Dust.NewDust(center2 + otherFourVector + Vector2.One * -4f, 8, 8, dustType2, 0f, 0f, 100, default(Color), 0.6f);
				Dust obj = Main.dust[dust3];
				obj.velocity *= 0.1f;
				obj.noGravity = true;
			}
		}
		else if (base.Projectile.ai[0] >= (float)(1 + penetrationAmt) && base.Projectile.ai[0] < (float)(1 + penetrationAmt * 2))
		{
			base.Projectile.scale = 0.9f;
			base.Projectile.tileCollide = false;
			base.Projectile.ai[1]++;
			if (base.Projectile.ai[1] >= 15f)
			{
				base.Projectile.alpha += 51;
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 0.8f;
				if (base.Projectile.alpha >= 255)
				{
					base.Projectile.Kill();
				}
			}
			else
			{
				base.Projectile.alpha -= 125;
				if (base.Projectile.alpha < 0)
				{
					base.Projectile.alpha = 0;
				}
				Projectile projectile3 = base.Projectile;
				projectile3.velocity *= 0.98f;
			}
			base.Projectile.localAI[0]++;
			int dustType3 = Utils.SelectRandom<int>(Main.rand, 226, 229);
			Vector2 center3 = base.Projectile.Center;
			Vector2 thirdFourVector = default(Vector2);
			((Vector2)(ref thirdFourVector))._002Ector(-4f, 4f);
			thirdFourVector += new Vector2(-4f, 4f);
			thirdFourVector = thirdFourVector.RotatedBy(base.Projectile.rotation);
			int dust4 = Dust.NewDust(center3 + thirdFourVector + Vector2.One * -4f, 8, 8, dustType3, 0f, 0f, 100, default(Color), 0.6f);
			Dust obj2 = Main.dust[dust4];
			obj2.velocity *= 0.1f;
			obj2.noGravity = true;
		}
		float colorScale = (float)base.Projectile.alpha / 255f;
		Lighting.AddLight((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16, 0.3f * colorScale, 0.4f * colorScale, 1f * colorScale);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 255, 255, 200) * ((255f - (float)base.Projectile.alpha) / 255f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0] = 1f;
		base.Projectile.ai[1] = 0f;
		base.Projectile.netUpdate = true;
		base.Projectile.velocity = oldVelocity / 2f;
		if (penetrationAmt == maxPenetrate)
		{
			SlowTime();
		}
		penetrationAmt = 2;
		return false;
	}

	public override bool? CanDamage()
	{
		if ((((int)(base.Projectile.ai[0] - 1f) / penetrationAmt == 0 && penetrationAmt < 3) || base.Projectile.ai[1] < 5f) && base.Projectile.ai[0] != 0f)
		{
			return false;
		}
		return null;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (penetrationAmt == maxPenetrate)
		{
			SlowTime();
		}
		if (base.Projectile.ai[0] >= (float)(1 + penetrationAmt) && base.Projectile.ai[0] < (float)(1 + penetrationAmt * 2))
		{
			base.Projectile.ai[0] = 0f;
		}
		penetrationAmt--;
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.ai[0] += penetrationAmt;
		}
		else
		{
			base.Projectile.ai[0] -= penetrationAmt + 1;
		}
		base.Projectile.ai[1] = 0f;
		base.Projectile.netUpdate = true;
	}

	private void SlowTime()
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item114, base.Projectile.Center);
		float radius = (base.Projectile.Calamity().stealthStrike ? 500f : 300f);
		int numDust = (int)((float)Math.PI * 2f / 5f * radius);
		float angleIncrement = (float)Math.PI * 2f / (float)numDust;
		Vector2 dustOffset = default(Vector2);
		((Vector2)(ref dustOffset))._002Ector(radius, 0f);
		dustOffset = dustOffset.RotatedByRandom(6.2831854820251465);
		for (int i = 0; i < numDust; i++)
		{
			dustOffset = dustOffset.RotatedBy(angleIncrement);
			int dustType = Utils.SelectRandom<int>(Main.rand, 226, 229);
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, dustType);
			Main.dust[dust].position = base.Projectile.Center + dustOffset;
			if (!Main.rand.NextBool(6))
			{
				Main.dust[dust].noGravity = true;
			}
			Main.dust[dust].fadeIn = 1f;
			Dust obj = Main.dust[dust];
			obj.velocity *= 0f;
			Main.dust[dust].scale = 0.3f;
		}
		int buffType = ModContent.BuffType<TimeDistortion>();
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (!npc.dontTakeDamage && !npc.buffImmune[buffType] && Vector2.Distance(base.Projectile.Center, npc.Center) <= radius && npc.FindBuffIndex(buffType) == -1)
			{
				npc.AddBuff(buffType, 60);
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.Calamity().stealthStrike)
		{
			target.AddBuff(ModContent.BuffType<TimeDistortion>(), 120);
		}
	}
}
