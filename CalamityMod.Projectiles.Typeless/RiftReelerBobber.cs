using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class RiftReelerBobber : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.aiStyle = 61;
		base.Projectile.bobber = true;
		base.Projectile.friendly = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.penetrate = -1;
		base.Projectile.ArmorPenetration = 100;
	}

	public override bool PreDrawExtras()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[2] == 0f)
		{
			Lighting.AddLight(base.Projectile.Center, 0.5f, 0.25f, 0f);
		}
		else
		{
			Lighting.AddLight(base.Projectile.Center, 0f, 0.45f, 0.46f);
		}
		return true;
	}
}
