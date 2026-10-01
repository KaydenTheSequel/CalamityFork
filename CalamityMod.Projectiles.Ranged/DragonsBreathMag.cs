using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class DragonsBreathMag : ModProjectile, ILocalizedModType, IModType
{
	public int Time;

	public bool TouchedGrass;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Ranged/DragonsBreathMag";

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.aiStyle = 14;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 700;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void SetStaticDefaults()
	{
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return true;
	}

	public override void AI()
	{
		base.Projectile.extraUpdates = 0;
		Time++;
		_ = Main.player[base.Projectile.owner];
		if (!TouchedGrass)
		{
			base.Projectile.rotation += 0.5f * (float)base.Projectile.direction;
		}
		base.Projectile.velocity.Y -= 0.055f;
		base.Projectile.velocity.X *= 0.992f;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.damage = 0;
		TouchedGrass = true;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.98f;
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld)
		{
			for (int i = 0; i <= 30; i++)
			{
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(4f, 4f), 100.0) * Main.rand.NextFloat(0.5f, 2.8f), affectedByGravity: false, 45, 1.4f, Main.rand.NextBool(4) ? Color.Orange : Color.OrangeRed));
			}
			GeneralParticleHandler.SpawnParticle(new DetailedExplosion(base.Projectile.Center, Vector2.Zero, Color.DarkOrange, Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, 2.8f, 33));
			GeneralParticleHandler.SpawnParticle(new DetailedExplosion(base.Projectile.Center, Vector2.Zero, Color.OrangeRed, Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, 3f, 33, UseAdditiveBlend: false));
		}
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 20);
	}
}
