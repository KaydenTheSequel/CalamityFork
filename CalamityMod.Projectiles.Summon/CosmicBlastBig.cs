using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class CosmicBlastBig : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 24);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 20 + Main.rand.Next(40);
			if (Main.rand.NextBool(5))
			{
				SoundEngine.PlaySound(in SoundID.Item9, base.Projectile.position);
			}
		}
		base.Projectile.rotation += (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) * 0.01f * (float)base.Projectile.direction;
		if (Main.rand.NextBool(8))
		{
			Vector2 value3 = Vector2.UnitX.RotatedByRandom(1.5707963705062866).RotatedBy(base.Projectile.velocity.ToRotation());
			int rainbowDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 66, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f, 150, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.2f);
			Main.dust[rainbowDust].noGravity = true;
			Main.dust[rainbowDust].velocity = value3 * 0.66f;
			Main.dust[rainbowDust].position = base.Projectile.Center + value3 * 12f;
		}
		if (Main.rand.NextBool(24) && !Main.dedServ)
		{
			int rainbowGore = Gore.NewGore(base.Projectile.GetSource_FromAI(), base.Projectile.Center, new Vector2(base.Projectile.velocity.X * 0.2f, base.Projectile.velocity.Y * 0.2f), 16);
			Gore obj = Main.gore[rainbowGore];
			obj.velocity *= 0.66f;
			Gore obj2 = Main.gore[rainbowGore];
			obj2.velocity += base.Projectile.velocity * 0.3f;
		}
		if (base.Projectile.ai[1] == 1f)
		{
			if (Main.rand.NextBool(5))
			{
				int rainbowDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 66, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f, 150, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.2f);
				Main.dust[rainbowDust2].noGravity = true;
			}
			if (Main.rand.NextBool(10) && !Main.dedServ)
			{
				Gore.NewGore(base.Projectile.GetSource_FromAI(), base.Projectile.position, new Vector2(base.Projectile.velocity.X * 0.2f, base.Projectile.velocity.Y * 0.2f), Main.rand.Next(16, 18));
			}
		}
		NPC potentialTarget = base.Projectile.Center.MinionHoming(600f, Main.player[base.Projectile.owner]);
		if (potentialTarget != null)
		{
			base.Projectile.velocity = (base.Projectile.velocity * 20f + base.Projectile.SafeDirectionTo(potentialTarget.Center) * 45f) / 21f;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB, 255);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 300);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 288);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Zombie103, base.Projectile.Center);
		for (int i = 0; i < 3; i++)
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 66, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.5f);
			Main.dust[dust].noGravity = true;
		}
		for (int j = 0; j < 30; j++)
		{
			int rainbowDusty = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 66, 0f, 0f, 0, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 2.5f);
			Main.dust[rainbowDusty].noGravity = true;
			Dust obj = Main.dust[rainbowDusty];
			obj.velocity *= 3f;
			rainbowDusty = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 66, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.5f);
			Dust obj2 = Main.dust[rainbowDusty];
			obj2.velocity *= 2f;
			Main.dust[rainbowDusty].noGravity = true;
		}
	}
}
