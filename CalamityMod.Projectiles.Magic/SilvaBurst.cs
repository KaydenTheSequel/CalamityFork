using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class SilvaBurst : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 96;
		base.Projectile.height = 96;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 2;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		float brightness = 1.6f;
		Lighting.AddLight(base.Projectile.Center, 0.27f * brightness, 0.82f * brightness, 0.157f * brightness);
		int dustID = 157;
		for (int d = 0; d < 3; d++)
		{
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 1.5f);
		}
		for (int i = 0; i < 30; i++)
		{
			int explode = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID, 0f, 0f, 0, new Color(Main.DiscoR, 203, 103), 2.5f);
			Main.dust[explode].noGravity = true;
			Dust obj = Main.dust[explode];
			obj.velocity *= 3f;
			explode = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustID, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 1.5f);
			Dust obj2 = Main.dust[explode];
			obj2.velocity *= 2f;
			Main.dust[explode].noGravity = true;
		}
	}
}
