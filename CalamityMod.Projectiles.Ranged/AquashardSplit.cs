using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class AquashardSplit : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Ranged/Aquashard";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.extraUpdates = 1;
		base.Projectile.aiStyle = 1;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.timeLeft < 280 && target.CanBeChasedBy(base.Projectile);
	}

	public override void AI()
	{
		base.Projectile.velocity.X *= 0.9995f;
		base.Projectile.velocity.Y += 0.01f;
		if (base.Projectile.timeLeft < 280)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 450f, (base.Projectile.ai[1] == 1f) ? 8f : 6f, 20f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.position);
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 154, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}
}
