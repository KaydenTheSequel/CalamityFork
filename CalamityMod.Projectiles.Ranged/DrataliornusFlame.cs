using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class DrataliornusFlame : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 18;
		base.Projectile.scale = 1.5f;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.arrow = true;
		base.Projectile.hide = true;
		base.Projectile.timeLeft = 180;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (base.Projectile.hide)
		{
			base.Projectile.hide = false;
			base.Projectile.ai[1] = -1f;
			if (base.Projectile.ai[0] != 0f)
			{
				base.Projectile.extraUpdates = 1;
				base.Projectile.localAI[0] = Main.rand.Next(30);
				if (base.Projectile.ai[0] == 2f)
				{
					base.Projectile.timeLeft += 180;
				}
			}
			base.Projectile.netUpdate = true;
		}
		if (!base.Projectile.tileCollide && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
		{
			base.Projectile.tileCollide = true;
			base.Projectile.netUpdate = true;
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 60f)
		{
			base.Projectile.localAI[0] = 0f;
			if (base.Projectile.ai[0] != 0f && base.Projectile.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<DragonDust>(), base.Projectile.damage / 3, base.Projectile.knockBack * 3f, base.Projectile.owner);
			}
		}
		base.Projectile.localAI[1]++;
		if (base.Projectile.localAI[1] > 12f)
		{
			base.Projectile.localAI[1] = 0f;
			if (base.Projectile.ai[0] == 2f && base.Projectile.ai[1] < 0f)
			{
				int possibleTarget = -1;
				float closestDistance = 700f;
				ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
				while (enumerator.MoveNext())
				{
					NPC npc = enumerator.Current;
					if (npc.chaseable && npc.lifeMax > 5 && !npc.dontTakeDamage && !npc.friendly && !npc.immortal && Collision.CanHit(base.Projectile.Center, 0, 0, npc.Center, 0, 0))
					{
						float distance = Vector2.Distance(base.Projectile.Center, npc.Center);
						if (closestDistance > distance)
						{
							closestDistance = distance;
							possibleTarget = npc.whoAmI;
						}
					}
				}
				base.Projectile.ai[1] = possibleTarget;
				base.Projectile.netUpdate = true;
			}
		}
		if (base.Projectile.ai[1] != -1f)
		{
			NPC npc2 = Main.npc[(int)base.Projectile.ai[1]];
			if (npc2.active && npc2.chaseable && !npc2.dontTakeDamage)
			{
				Vector2 distance2 = npc2.Center - base.Projectile.Center;
				double angle = distance2.ToRotation() - base.Projectile.velocity.ToRotation();
				if (angle > Math.PI)
				{
					angle -= Math.PI * 2.0;
				}
				if (angle < -Math.PI)
				{
					angle += Math.PI * 2.0;
				}
				if (Math.Abs(angle) > Math.PI * 3.0 / 4.0)
				{
					base.Projectile.velocity = base.Projectile.velocity.RotatedBy(angle * 0.07);
				}
				else
				{
					float range = ((Vector2)(ref distance2)).Length();
					float difference = 12.7f / range;
					distance2 *= difference;
					distance2 /= 7f;
					Projectile projectile = base.Projectile;
					projectile.velocity += distance2;
					if (range > 70f)
					{
						Projectile projectile2 = base.Projectile;
						projectile2.velocity *= 0.98f;
					}
				}
			}
			else
			{
				base.Projectile.ai[1] = -1f;
				base.Projectile.netUpdate = true;
			}
		}
		int d = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 127, base.Projectile.velocity.X, base.Projectile.velocity.Y, 0, default(Color), 1.5f + Main.rand.NextFloat());
		Main.dust[d].noGravity = true;
		Lighting.AddLight(base.Projectile.Center, 1f, 0.6039216f, 0.22745098f);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 154, 58, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture2D13 = TextureAssets.Projectile[base.Type].Value;
		Main.spriteBatch.Draw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, 0, texture2D13.Width, texture2D13.Height), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)texture2D13.Height / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		if (timeLeft != 0)
		{
			SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
			if (base.Projectile.ai[0] != 0f && base.Projectile.owner == Main.myPlayer)
			{
				Vector2 randomAngle = base.Projectile.Center + Utils.RotatedBy(new Vector2(600f, 0f), (double)MathHelper.ToRadians((float)Main.rand.Next(360)), default(Vector2));
				Vector2 speed = base.Projectile.Center - randomAngle;
				speed /= 30f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), randomAngle.X, randomAngle.Y, speed.X, speed.Y, ModContent.ProjectileType<DrataliornusExoArrow>(), base.Projectile.damage / 2, base.Projectile.knockBack, base.Projectile.owner);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<DragonDust>(), base.Projectile.damage / 3, base.Projectile.knockBack * 2f, base.Projectile.owner);
			}
			base.Projectile.position = base.Projectile.Center;
			base.Projectile.width = 180;
			base.Projectile.height = 180;
			base.Projectile.position.X = base.Projectile.position.X - 90f;
			base.Projectile.position.Y = base.Projectile.position.Y - 90f;
			float modifier = 4f + 8f * Main.rand.NextFloat();
			for (int i = 0; i < 24; i++)
			{
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * modifier).RotatedBy((float)(i - 11) * ((float)Math.PI * 2f) / 24f) + base.Projectile.Center;
				Vector2 faceDirection = val - base.Projectile.Center;
				int dust = Dust.NewDust(val + faceDirection, 0, 0, 174, 0f, 0f, 45, default(Color), 2f);
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity = faceDirection;
			}
			for (int j = 0; j < 4; j++)
			{
				Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 174, 0f, 0f, 50, default(Color), 1.5f);
				int fieryDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 174, 0f, 0f, 50);
				Main.dust[fieryDust].noGravity = true;
				Dust obj = Main.dust[fieryDust];
				obj.velocity *= 2f;
			}
			for (int k = 0; k < 12; k++)
			{
				int fieryDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 127, 0f, 0f, 0, default(Color), 3f);
				Main.dust[fieryDust2].noGravity = true;
				Dust obj2 = Main.dust[fieryDust2];
				obj2.velocity *= 3f;
			}
			base.Projectile.timeLeft = 0;
			base.Projectile.penetrate = -1;
			base.Projectile.usesLocalNPCImmunity = true;
			base.Projectile.localNPCHitCooldown = 10;
			base.Projectile.damage /= 3;
			base.Projectile.Damage();
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 240);
		if (base.Projectile.ai[0] != 0f && base.Projectile.owner == Main.myPlayer && base.Projectile.timeLeft != 0)
		{
			Vector2 randomAngle = target.Center + Utils.RotatedBy(new Vector2(600f, 0f), (double)MathHelper.ToRadians((float)Main.rand.Next(360)), default(Vector2));
			Vector2 speed = target.Center - randomAngle;
			speed /= 30f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), randomAngle.X, randomAngle.Y, speed.X, speed.Y, ModContent.ProjectileType<DrataliornusExoArrow>(), base.Projectile.damage / 2, base.Projectile.knockBack, base.Projectile.owner);
			Vector2 vel = default(Vector2);
			((Vector2)(ref vel))._002Ector((float)Main.rand.Next(-400, 401), (float)Main.rand.Next(500, 801));
			Vector2 pos = target.Center - vel;
			vel.X += Main.rand.Next(-100, 101);
			((Vector2)(ref vel)).Normalize();
			vel *= 30f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), pos, vel + target.velocity, ModContent.ProjectileType<SkyFlareFriendly>(), (int)((float)base.Projectile.damage * 1.5f), base.Projectile.knockBack * 5f, base.Projectile.owner);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 240);
		if (base.Projectile.ai[0] != 0f && base.Projectile.owner == Main.myPlayer && base.Projectile.timeLeft != 0)
		{
			Vector2 randomAngle = target.Center + Utils.RotatedBy(new Vector2(600f, 0f), (double)MathHelper.ToRadians((float)Main.rand.Next(360)), default(Vector2));
			Vector2 speed = target.Center - randomAngle;
			speed /= 30f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), randomAngle.X, randomAngle.Y, speed.X, speed.Y, ModContent.ProjectileType<DrataliornusExoArrow>(), base.Projectile.damage / 2, base.Projectile.knockBack, base.Projectile.owner);
			Vector2 vel = default(Vector2);
			((Vector2)(ref vel))._002Ector((float)Main.rand.Next(-400, 401), (float)Main.rand.Next(500, 801));
			Vector2 pos = target.Center - vel;
			vel.X += Main.rand.Next(-100, 101);
			((Vector2)(ref vel)).Normalize();
			vel *= 30f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), pos, vel + target.velocity, ModContent.ProjectileType<SkyFlareFriendly>(), base.Projectile.damage * 3, base.Projectile.knockBack * 5f, base.Projectile.owner);
		}
	}
}
