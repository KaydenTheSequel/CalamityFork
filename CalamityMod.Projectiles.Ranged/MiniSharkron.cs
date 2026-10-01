using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class MiniSharkron : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.aiStyle = 1;
		base.AIType = 408;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.arrow = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
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
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		for (int d = 0; d < 15; d++)
		{
			int idx = Dust.NewDust(base.Projectile.Center - Vector2.One * 10f, 50, 50, 5, 0f, -2f);
			Dust obj = Main.dust[idx];
			obj.velocity /= 2f;
		}
		if (!Main.dedServ)
		{
			int tail = Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.Center, base.Projectile.velocity * 0.8f, 584);
			Main.gore[tail].timeLeft /= 10;
			int body = Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.Center, base.Projectile.velocity * 0.9f, 585);
			Main.gore[body].timeLeft /= 10;
			int head = Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.Center, base.Projectile.velocity * 1f, 586);
			Main.gore[head].timeLeft /= 10;
		}
	}
}
