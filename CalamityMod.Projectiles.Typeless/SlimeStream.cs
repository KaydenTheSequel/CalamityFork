using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class SlimeStream : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 90;
		base.Projectile.MaxUpdates = 3;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		Dust.QuickDust(base.Projectile.Center, (base.Projectile.ai[0] > 0f) ? Color.Magenta : Color.RoyalBlue);
		if (base.Projectile.ai[1] > 0f && base.Projectile.timeLeft < 60)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: false, 320f, 12f, 20f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(137, 600);
	}
}
