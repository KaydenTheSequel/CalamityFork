using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class PhotosyntheticShard : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 20);
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 60;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.tileCollide = true;
	}

	public override void AI()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 4; i++)
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.Center, 1, 1, 107, 0f, 0f, 0, default(Color), 0.5f);
			dust.scale = 0.42f;
			dust.velocity *= 0.1f;
		}
		CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: false, 500f, 15f, 20f);
	}
}
