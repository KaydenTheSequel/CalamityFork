using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class FloodtideShark : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetDefaults()
	{
		base.Projectile.width = 66;
		base.Projectile.height = 28;
		base.Projectile.aiStyle = 1;
		base.AIType = 408;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Melee;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		for (int d = 0; d < 15; d++)
		{
			int idx = Dust.NewDust(base.Projectile.Center - Vector2.One * 10f, 50, 50, 5, 0f, -2f);
			Dust obj = Main.dust[idx];
			obj.velocity /= 2f;
		}
	}
}
