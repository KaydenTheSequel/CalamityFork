using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class ClamBubbleBlast : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = true;
		base.Projectile.timeLeft = 20;
		base.Projectile.extraUpdates = 3;
	}

	public override void AI()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool())
		{
			Gore gore = Gore.NewGorePerfect(base.Projectile.GetSource_FromAI(), base.Projectile.position, base.Projectile.velocity * 0.2f + Main.rand.NextVector2Circular(1f, 1f), 411);
			gore.timeLeft = 9 + Main.rand.Next(7);
			gore.scale = Main.rand.NextFloat(0.6f, 1f);
			gore.type = (Main.rand.NextBool(3) ? 412 : 411);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 10; i++)
		{
			Gore gore = Gore.NewGorePerfect(base.Projectile.GetSource_FromAI(), base.Projectile.position, base.Projectile.velocity.RotatedByRandom(MathHelper.ToRadians(60f)) * 0.3f, 411);
			gore.timeLeft = 9 + Main.rand.Next(7);
			gore.scale = Main.rand.NextFloat(0.6f, 1f);
			gore.type = (Main.rand.NextBool(3) ? 412 : 411);
		}
	}
}
