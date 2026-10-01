using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class HydrothermalSmoke : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 42;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 6;
	}

	public override void AI()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft == 6)
		{
			base.Projectile.Center = Main.player[base.Projectile.owner].Center;
		}
		if (Main.rand.NextBool(6))
		{
			int fieryDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 127, 0f, 0f, 100, default(Color), 0.7f);
			if (Main.rand.NextBool(4))
			{
				Main.dust[fieryDust].scale *= 0.35f;
			}
			Dust obj = Main.dust[fieryDust];
			obj.velocity *= 0f;
		}
		if (Main.rand.NextBool(9) && !Main.dedServ)
		{
			float upwardVariation = Main.rand.NextFloat(-4.5f, -8f);
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.position, -base.Projectile.velocity + new Vector2(0.5f, upwardVariation), Main.rand.NextBool(3) ? Color.LightSteelBlue : Color.SteelBlue, Color.LightSlateGray, Main.rand.NextFloat(0.4f, 0.65f), 130f));
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
