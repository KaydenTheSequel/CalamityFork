using CalamityMod.Dusts;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SparkSpreaderFire : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 12);
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 3;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = 42 * base.Projectile.MaxUpdates;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.wet && !base.Projectile.lavaWet)
		{
			base.Projectile.Kill();
			return;
		}
		Time++;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (Time >= 8f)
		{
			float cinderSize = Utils.GetLerpValue(6f, 12f, Time, clamped: true);
			Dust cinder = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<FinalFlame>(), base.Projectile.velocity.X * 0.2f, base.Projectile.velocity.Y * 0.2f, 10, default(Color), 0.75f);
			if (Main.rand.NextBool(3))
			{
				cinder.scale *= 3f;
				cinder.velocity *= 1.5f;
			}
			cinder.noGravity = true;
			cinder.scale *= cinderSize * 0.8f;
			cinder.velocity += base.Projectile.velocity;
		}
		else if (Time == 7f && !Main.rand.NextBool(4))
		{
			SpawnSparks(3, shorts: true);
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		SpawnSparks(8);
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(24, 60 * Main.rand.Next(5, 21));
		SpawnSparks(5);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(24, 60 * Main.rand.Next(5, 21));
		SpawnSparks(5);
	}

	public void SpawnSparks(int count, bool shorts = false)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < count; i++)
		{
			int sparkLifetime = Main.rand.Next(9, 16);
			float sparkScale = Main.rand.NextFloat(0.2f, 0.4f);
			Color sparkColor = Color.Lerp(Color.Gold, Color.OrangeRed, Main.rand.NextFloat(0.1f, 1f));
			if (Main.rand.NextBool(6))
			{
				sparkScale *= 1.5f;
			}
			Vector2 sparkVelocity = base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(30f)) * Main.rand.NextFloat(1.5f, 3f);
			sparkVelocity.Y -= Main.rand.NextFloat(6f, 8f);
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, sparkVelocity * (shorts ? 0.75f : 1f), affectedByGravity: true, sparkLifetime, sparkScale, sparkColor));
		}
	}
}
