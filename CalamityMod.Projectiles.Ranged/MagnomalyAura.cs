using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class MagnomalyAura : ModProjectile, ILocalizedModType, IModType
{
	private int radius = 100;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
		base.Projectile.width = 200;
		base.Projectile.height = 200;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		Projectile parent = Main.projectile[0];
		bool active = false;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if ((float)p.identity == base.Projectile.ai[0] && p.active && p.type == ModContent.ProjectileType<MagnomalyRocket>())
			{
				parent = p;
				active = true;
			}
		}
		if (active)
		{
			base.Projectile.Center = parent.Center;
		}
		else
		{
			base.Projectile.Kill();
		}
		if (!parent.active)
		{
			base.Projectile.Kill();
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, radius, targetHitbox);
	}
}
