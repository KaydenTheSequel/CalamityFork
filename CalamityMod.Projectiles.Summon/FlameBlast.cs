using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class FlameBlast : ModProjectile, ILocalizedModType, IModType
{
	public float count;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.SentryShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 6);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 3;
		base.Projectile.timeLeft = 240;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		NPC potentialTarget = base.Projectile.Center.MinionHoming(900f, Main.player[base.Projectile.owner]);
		float velPower = Utils.GetLerpValue(2f, 5f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);
		if (count == 0f)
		{
			count++;
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 4f)
		{
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.1f, "CalamityMod/Particles/SmallBloom", affectedByGravity: false, 8, 0.085f, Color.Lerp(Color.OrangeRed, Color.Goldenrod, Utils.GetLerpValue(270f, 230f, base.Projectile.timeLeft)), new Vector2(1f, 1f + 0.8f * velPower), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.5f * velPower));
		}
		if (potentialTarget != null && (base.Projectile.localAI[0] % 100f <= 55f || base.Projectile.localAI[0] < 20f))
		{
			base.Projectile.timeLeft++;
			base.Projectile.velocity = (base.Projectile.velocity * 20f + base.Projectile.SafeDirectionTo(potentialTarget.Center) * 8f) / 21f;
		}
		else
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= Main.rand.NextFloat(0.96f, 0.97f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		for (int j = 0; j < 9; j++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>());
			dust.velocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.5) * Main.rand.NextFloat(7f, 15f);
			dust.scale = Main.rand.NextFloat(0.9f, 1.1f);
			dust.noGravity = true;
			dust.color = (Main.rand.NextBool() ? Color.Orange : Color.Goldenrod);
			dust.noLightEmittence = true;
		}
	}
}
