using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class RespiteblockBlood : ModProjectile, ILocalizedModType, IModType
{
	public Color mainColor;

	public int time;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 45;
		base.Projectile.height = 45;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 5;
		base.Projectile.timeLeft = 900;
		base.Projectile.extraUpdates = 7;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = DamageClass.Melee;
	}

	public override void AI()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		if (mainColor == Color.White)
		{
			mainColor = (Main.rand.NextBool() ? Color.Green : Color.Purple);
		}
		if (Collision.SolidCollision(base.Projectile.Center, 5, 5))
		{
			base.Projectile.Kill();
		}
		Vector2 center = base.Projectile.Center;
		Color newColor = Color.Lerp(mainColor, Color.White, 0.5f);
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3());
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (base.Projectile.timeLeft % 2 == 0 && (float)time >= 1f && targetDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new WaterFlavoredParticle(base.Projectile.Center, -base.Projectile.velocity * 0.05f, affectedByGravity: false, 6, 0.8f, mainColor * 0.65f * Utils.GetLerpValue(0f, 90f, time, clamped: true)));
		}
		if (Main.rand.NextBool(4))
		{
			Vector2 center2 = base.Projectile.Center;
			int type = ((mainColor == Color.Green) ? 89 : 86);
			Vector2? velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 0.8f);
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(center2, type, velocity, 0, newColor, Main.rand.NextFloat(0.8f, 1.1f));
			dust.noGravity = true;
			dust.noLight = true;
			dust.noLightEmittence = true;
			dust.alpha = 90;
		}
		base.Projectile.velocity.X *= 0.998f;
		base.Projectile.velocity.Y += 0.01f;
		time++;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 4; i++)
		{
			GeneralParticleHandler.SpawnParticle(new PointParticle(base.Projectile.Center, (-base.Projectile.velocity * 2f).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.2f, 0.9f) + new Vector2(0f, Main.rand.NextFloat(-5f, 0f)), affectedByGravity: true, 20, Main.rand.NextFloat(0.7f, 1.2f), mainColor * 0.5f, AddativeBlend: false));
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 180);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 45f, targetHitbox);
	}

	public RespiteblockBlood()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		mainColor = Color.White;
		base._002Ector();
	}
}
