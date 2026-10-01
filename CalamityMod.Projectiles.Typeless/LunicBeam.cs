using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Typeless;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class LunicBeam : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 8);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.timeLeft = 120 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 0f)
		{
			for (int i = 0; i < 4; i++)
			{
				Vector2 velocity = ((float)Math.PI * 2f * (float)i / 4f + base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f).ToRotationVector2() * 0.8f;
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, velocity, affectedByGravity: false, 6, 0.015f, Color.DarkOrange, Vector2.One, quickShrink: true));
			}
			base.Projectile.ai[0] = 1f;
		}
		Color trailColor = default(Color);
		((Color)(ref trailColor))._002Ector(100, 30, 20);
		float range = 240f;
		int targetNPC = -1;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC target = enumerator.Current;
			if (target.CanBeChasedBy(base.Projectile))
			{
				float distance = Vector2.Distance(target.Center, base.Projectile.Center);
				if (distance < range && Collision.CanHit(base.Projectile, target))
				{
					range = distance;
					targetNPC = target.whoAmI;
				}
			}
		}
		if (targetNPC > -1)
		{
			NPC target2 = Main.npc[targetNPC];
			Vector2 idealVelocity = base.Projectile.SafeDirectionTo(target2.Center) * 12f;
			base.Projectile.velocity = (base.Projectile.velocity * 39f + idealVelocity) / 40f;
			base.Projectile.velocity = base.Projectile.velocity.MoveTowards(idealVelocity, 1f);
			trailColor = Color.Lerp(new Color(100, 30, 20), Color.Indigo, Utils.GetLerpValue(240f, 0f, Vector2.Distance(base.Projectile.Center, target2.Center), clamped: true));
		}
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref trailColor)).ToVector3() * 0.5f);
		Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>(), base.Projectile.velocity * 0.05f);
		dust.noGravity = true;
		dust.scale = Main.rand.NextFloat(1f, 1.6f);
		dust.color = trailColor;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<MarkedforDeath>(), 480);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<MarkedforDeath>(), 480);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in LunicEye.ImpactSound, base.Projectile.Center);
		float offset = Main.rand.NextFloat((float)Math.PI * 2f);
		for (int i = 0; i < 4; i++)
		{
			Vector2 velocity = ((float)Math.PI * 2f * (float)i / 4f + offset).ToRotationVector2() * 0.5f;
			GeneralParticleHandler.SpawnParticle(new AltSparkParticle(base.Projectile.Center + velocity * 50f, velocity, affectedByGravity: false, 12, 2f, Color.Black));
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + velocity * 50f, velocity, affectedByGravity: false, 12, 1f, Color.Indigo));
		}
		for (int j = 0; j < 25; j++)
		{
			Vector2 velocity2 = ((float)Math.PI * 2f * (float)j / 25f).ToRotationVector2() * 6f;
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<VoidDust>(), velocity2);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(1.6f, 1.8f);
			dust.color = Color.Indigo * 0.5f;
		}
	}
}
