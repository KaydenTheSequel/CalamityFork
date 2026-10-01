using System;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class TheStormLightningShot : ModProjectile, ILocalizedModType, IModType
{
	public int time;

	public bool homing = true;

	public bool hasZaged;

	public int zagDirection = 1;

	public Vector2 effectVel;

	public NPC closestTarget;

	public float colorValue;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 60;
		base.Projectile.height = 60;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 3;
		base.Projectile.extraUpdates = 4;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.ArmorPenetration = 20;
		base.Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		if (time == 0)
		{
			if (base.Projectile.ai[0] == 5f)
			{
				base.Projectile.extraUpdates = 60;
				base.Projectile.penetrate = -1;
			}
			colorValue += Main.rand.Next(0, 20);
			zagDirection = (Main.rand.NextBool() ? 1 : (-1));
			effectVel = base.Projectile.velocity;
		}
		colorValue = MathHelper.Lerp(colorValue, 50f, 0.035f);
		Color usedColor = Color.Lerp(Color.Cyan, Color.Orchid, Utils.GetLerpValue(0f, 50f, colorValue));
		float num = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (num < 1400f)
		{
			if (base.Projectile.timeLeft % 2 == 0)
			{
				GeneralParticleHandler.SpawnParticle(new BoltParticle(base.Projectile.Center, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 7, 0.3f, usedColor, new Vector2(1.8f, 0.8f), glowCenter: true, glowFade: true, fadeIn: false, 0.7f));
			}
			if (Main.rand.NextBool(35))
			{
				GeneralParticleHandler.SpawnParticle(new BoltParticle(base.Projectile.Center, base.Projectile.velocity.RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.3f, 1.9f), affectedByGravity: false, 23, Main.rand.NextFloat(0.2f, 0.3f), usedColor, new Vector2(1.8f, 0.8f), glowCenter: true, glowFade: true, fadeIn: false, 0.3f));
			}
		}
		if (time % 15 == 0)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 278, effectVel * 10f * Main.rand.NextFloat(-0.4f, -0.7f), 0, default(Color), Main.rand.NextFloat(0.45f, 0.6f));
			dust.noGravity = true;
			dust.color = usedColor;
			effectVel = base.Projectile.velocity.RotatedBy(0.2f * (float)zagDirection * (hasZaged ? 1f : 0.5f)) * 0.08f;
		}
		if (time % 20 == 0)
		{
			hasZaged = true;
			zagDirection *= -1;
		}
		closestTarget = base.Projectile.Center.ClosestNPCAt(900f);
		if (closestTarget != null && homing && base.Projectile.ai[0] < 5f)
		{
			float moveSpeed = Utils.GetLerpValue(300f, 250f, base.Projectile.timeLeft, clamped: true);
			CalamityUtils.HomeInOnSelectedNPC(base.Projectile, closestTarget, ignoreTiles: true, moveSpeed, 12f, 0.97f, 0.95f, accelerate: true);
			colorValue = MathHelper.Lerp(50f, 0f, Utils.GetLerpValue(500f, 0f, Vector2.Distance(closestTarget.Center, base.Projectile.Center), clamped: true));
		}
		if (!homing && ((Vector2)(ref base.Projectile.velocity)).Length() < 12f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.01f;
		}
		time++;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (target != closestTarget && base.Projectile.ai[0] < 5f)
		{
			base.Projectile.numHits--;
			base.Projectile.penetrate++;
		}
		else
		{
			colorValue = Main.rand.Next(0, 10);
			homing = false;
		}
		target.AddBuff(144, 90);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 2; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 278, (base.Projectile.velocity * 4f).RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(0.3f, 1.8f), 0, default(Color), Main.rand.NextFloat(0.6f, 0.8f));
			dust.noGravity = true;
			dust.color = (Main.rand.NextBool(5) ? Color.Cyan : Color.Orchid);
		}
	}
}
