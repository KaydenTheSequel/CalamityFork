using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class Phantom : ModProjectile, ILocalizedModType, IModType
{
	public float sizeVariance = 2f;

	public int time;

	public int spinDir = 100;

	public int waveOften = 40;

	public float scaleVariance = 1f;

	public NPC targeted;

	public bool launched;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.extraUpdates = 5;
		base.Projectile.penetrate = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 900;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 16 * base.Projectile.MaxUpdates;
		base.Projectile.ArmorPenetration = 30;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		float targetDist = Vector2.Distance(Owner.Center, base.Projectile.Center);
		base.Projectile.rotation += Main.rand.NextFloat(0.02f, 0.09f);
		if (spinDir == 100)
		{
			spinDir = (Main.rand.NextBool() ? 1 : (-1));
			waveOften = Main.rand.Next(10, 41);
			base.Projectile.scale = Main.rand.NextFloat(0.95f, 1.1f);
		}
		Vector2 center = base.Projectile.Center;
		Color white = Color.White;
		Lighting.AddLight(center, ((Color)(ref white)).ToVector3() * 0.3f);
		if (time % 2 == 0 && time > 3 && targetDist < 1400f)
		{
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + base.Projectile.velocity * Main.rand.NextFloat(-1f, 1f), -base.Projectile.velocity * 0.3f, affectedByGravity: false, 4, 0.025f, Color.Lerp(Color.White, Color.Aqua, 0.3f) * 0.6f, new Vector2(1f, 0.3f), quickShrink: true, glow: false, 2f));
		}
		if (time >= 500)
		{
			Vector2 mouse = Owner.ClampedMouseWorld();
			if (time == 500)
			{
				base.Projectile.penetrate = 1;
				base.Projectile.velocity = (mouse - base.Projectile.Center).SafeNormalize(Vector2.UnitX) * 6f;
				launched = true;
			}
			if (Vector2.Distance(mouse, base.Projectile.Center) < 30f)
			{
				time = 600;
			}
			if (targeted == null || targeted.life <= 0)
			{
				targeted = base.Projectile.Center.ClosestNPCAt(950f);
			}
			CalamityUtils.HomeInOnSelectedNPC(base.Projectile, targeted, ignoreTiles: true, 0.15f, 6f, 0.98f, 0.95f, accelerate: true);
			if (time < 550 && targeted == null)
			{
				if (((Vector2)(ref base.Projectile.velocity)).Length() < 6f)
				{
					Projectile projectile = base.Projectile;
					projectile.velocity += (mouse - base.Projectile.Center).SafeNormalize(Vector2.UnitX) * 0.35f;
				}
				else
				{
					Projectile projectile2 = base.Projectile;
					projectile2.velocity *= 0.9f;
				}
			}
			if (time % waveOften == 0)
			{
				spinDir *= -1;
			}
			base.Projectile.velocity = base.Projectile.velocity.RotatedBy(Main.rand.NextFloat(0.01f, 0.02f) * (float)spinDir);
		}
		else if (time > 15)
		{
			Vector2 moveToEnemy = (Owner.Center + Utils.RotatedBy(new Vector2(0f, -30f), (double)((float)time * 0.05f), default(Vector2)) - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 8f)
			{
				Projectile projectile3 = base.Projectile;
				projectile3.velocity += moveToEnemy * Main.rand.NextFloat(0.2f, 0.4f);
			}
			else
			{
				Projectile projectile4 = base.Projectile;
				projectile4.velocity *= 0.85f;
			}
		}
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		modifiers.SourceDamage *= (launched ? 1f : 0.3f);
		Vector2 launchVel = Main.player[base.Projectile.owner].Center.DirectionTo(target.Center);
		target.MoveNPC(launchVel, 10f * (launched ? 0.5f : 1f), ignoreKBImmune: true);
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (targeted != null)
		{
			if (target != targeted)
			{
				return false;
			}
			return null;
		}
		return null;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 2; i++)
		{
			Dust.NewDustPerfect(base.Projectile.Center, 175, (base.Projectile.velocity * 3f).RotatedByRandom(0.6000000238418579) * Main.rand.NextFloat(0.1f, 0.8f), 100, default(Color), Main.rand.NextFloat(0.8f, 1.3f)).noGravity = true;
		}
	}
}
