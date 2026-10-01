using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Particles;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

internal class AnimosityBullet : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 15;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 18;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = ((!Main.zenithWorld) ? 1 : 3);
		base.Projectile.extraUpdates = 2;
		base.Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.scale = (Main.zenithWorld ? 2f : 1.4f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		Time++;
		Lighting.AddLight(base.Projectile.Center, 0.9f, 0f, 0.15f);
		if (Time > 3f)
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center - base.Projectile.velocity * 1.8f, -base.Projectile.velocity * 0.01f, affectedByGravity: false, 11, 1.6f, (Main.zenithWorld ? Color.MediumPurple : Color.Red) * 0.65f));
		}
		for (int i = 0; i <= 2; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + base.Projectile.velocity * Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextBool(3) ? 90 : 60, -base.Projectile.velocity.RotatedBy(-0.5) * Main.rand.NextFloat(0.05f, 0.2f));
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.5f, 1.1f);
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + base.Projectile.velocity * Main.rand.NextFloat(-0.5f, 0.5f), Main.rand.NextBool(3) ? 90 : 60, -base.Projectile.velocity.RotatedBy(0.5) * Main.rand.NextFloat(0.05f, 0.2f));
			dust2.noGravity = true;
			dust2.scale = Main.rand.NextFloat(0.5f, 1.1f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.NPCDeath55 with
		{
			Pitch = -0.7f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		for (int i = 0; i <= 11; i++)
		{
			GeneralParticleHandler.SpawnParticle(new DesertProwlerSkullParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(2.5f, 2.5f), 100.0) * Main.rand.NextFloat(0.2f, 1f), Main.rand.NextBool() ? Color.Crimson : Color.DarkRed, Color.Red, Main.rand.NextFloat(0.2f, 0.9f), 175f));
		}
		for (int j = 0; j <= 25; j++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 90 : 60, Utils.RotatedByRandom(new Vector2(0f, -5f), MathHelper.ToRadians(35f)) * Main.rand.NextFloat(0.1f, 1.9f));
			dust.noGravity = false;
			dust.scale = Main.rand.NextFloat(0.9f, 1.5f);
			Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? 90 : 60, Utils.RotatedByRandom(new Vector2(0f, -2f), MathHelper.ToRadians(35f)) * Main.rand.NextFloat(0.1f, 1.9f));
			dust2.noGravity = false;
			dust2.scale = Main.rand.NextFloat(0.9f, 1.5f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 300);
		target.AddBuff(ModContent.BuffType<WhisperingDeath>(), 120);
		if (Main.zenithWorld)
		{
			target.AddBuff(ModContent.BuffType<VulnerabilityHex>(), 60);
			target.AddBuff(153, 120);
			GungeonMusicSystem.GUN();
			if (target.life <= 0 && target.type != 46 && target.type != 614 && target.type != 443)
			{
				SoundStyle style = SoundID.NPCDeath7 with
				{
					Pitch = -0.7f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				NPC.NewNPC(target.GetSource_Death(), (int)base.Projectile.Center.X, (int)base.Projectile.Center.Y, 46);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 120);
		if (Main.zenithWorld)
		{
			target.AddBuff(153, 120);
			GungeonMusicSystem.GUN();
			if (target.statLife <= 0)
			{
				SoundStyle style = SoundID.NPCDeath7 with
				{
					Pitch = -0.7f
				};
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				NPC.NewNPC(target.GetSource_Death(), (int)base.Projectile.Center.X, (int)base.Projectile.Center.Y, 46);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], Color.Crimson * 0.45f);
		return true;
	}
}
