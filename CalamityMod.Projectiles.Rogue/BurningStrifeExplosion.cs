using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class BurningStrifeExplosion : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 130;
		base.Projectile.height = 130;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 40;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] % 3f == 0f)
		{
			for (int i = 0; i < 3; i++)
			{
				Vector2 center = base.Projectile.Center;
				Vector2? velocity = Main.rand.NextVector2Circular(8f, 8f);
				float scale = Main.rand.NextFloat(1f, 1.5f);
				Dust.NewDustPerfect(center, 27, velocity, 0, default(Color), scale);
			}
			GeneralParticleHandler.SpawnParticle(new FlameParticle(new Vector2(base.Projectile.position.X + Main.rand.NextFloat(base.Projectile.width), base.Projectile.position.Y + Main.rand.NextFloat(base.Projectile.height)), 10, 0.45f, 0.05f, Color.Violet, Color.DarkViolet));
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(153, 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Shadowflame>(), 180);
	}
}
