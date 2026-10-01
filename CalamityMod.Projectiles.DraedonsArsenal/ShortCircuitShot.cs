using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Effects;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class ShortCircuitShot : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public Vector2 position;

	public Vector2 oldPos;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.timeLeft = 180;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 2;
		base.Projectile.extraUpdates = 5;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.ArmorPenetration = 10;
	}

	public override void AI()
	{
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		if (time == 0)
		{
			for (int i = 0; i < 7; i++)
			{
				bool is278 = Main.rand.NextBool(4);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 30f, is278 ? ArsenalEffects.ArsenalDust : ArsenalEffects.ArsenalElectricDust);
				dust.scale = (is278 ? 0.7f : (Main.rand.NextBool(7) ? 1.2f : 0.8f));
				dust.noGravity = true;
				if (!is278)
				{
					dust.fadeIn = 2f;
				}
				dust.color = ArsenalEffects.ArsenalElectricColor;
				dust.velocity = base.Projectile.velocity.RotatedByRandom(0.699999988079071) * Main.rand.NextFloat(0.6f, 2.3f) * (is278 ? 0.3f : 1f);
			}
		}
		if (time > 4)
		{
			if (base.Projectile.timeLeft % 2 == 0)
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, ArsenalEffects.ArsenalElectricDust);
				dust2.scale = (Main.rand.NextBool(7) ? 1f : 0.55f);
				dust2.noGravity = true;
				dust2.fadeIn = 2f;
				dust2.color = ArsenalEffects.ArsenalElectricColor;
				dust2.velocity = base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.2f, 1.5f);
			}
			if (base.Projectile.timeLeft % 2 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 5f, base.Projectile.velocity, affectedByGravity: false, (base.Projectile.timeLeft < 50) ? 20 : 6, 0.6f, ArsenalEffects.ArsenalElectricColor));
			}
			if (base.Projectile.timeLeft % 3 == 0)
			{
				float squash = Utils.GetLerpValue(1f, 3f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);
				GeneralParticleHandler.SpawnParticle(new BoltParticle(base.Projectile.Center - base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 10f, base.Projectile.velocity * 0.01f, affectedByGravity: false, 7, 0.2f, ArsenalEffects.ArsenalElectricColor * 0.8f * squash, new Vector2(1f - 0.15f * squash, 1f), glowCenter: true, glowFade: false, fadeIn: false, 0.5f * squash));
			}
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.987f;
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		Player obj = Main.player[base.Projectile.owner];
		target.AddBuff(ModContent.BuffType<StaticDischarge>(), 40);
		Vector2 launchVel = obj.Center.DirectionTo(target.Center);
		target.MoveNPC(launchVel, 8f);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.7f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}
}
