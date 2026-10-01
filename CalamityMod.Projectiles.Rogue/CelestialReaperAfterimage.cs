using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class CelestialReaperAfterimage : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/CelestialReaper";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 66;
		base.Projectile.height = 76;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 51;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.timeLeft = 180;
		base.Projectile.Calamity().CannotProc = true;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.timeLeft < 150 && target.CanBeChasedBy(base.Projectile);
	}

	public override void AI()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += MathHelper.ToRadians(30f);
		if (base.Projectile.timeLeft < 150)
		{
			NPC target = base.Projectile.Center.ClosestNPCAt(800f);
			if (target != null)
			{
				base.Projectile.velocity = (base.Projectile.velocity * 20f + base.Projectile.SafeDirectionTo(target.Center) * 20f) / 21f;
			}
		}
		base.Projectile.alpha += 5;
	}
}
