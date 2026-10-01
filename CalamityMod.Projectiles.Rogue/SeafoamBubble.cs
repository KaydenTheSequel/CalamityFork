using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SeafoamBubble : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/Typeless/CoralBubble";

	public override void SetDefaults()
	{
		base.Projectile.width = 28;
		base.Projectile.height = 28;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 180;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.scale = 1f;
		base.Projectile.localNPCHitCooldown = 30;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		base.Projectile.ai[0]++;
		if (base.Projectile.alpha < 50)
		{
			base.Projectile.alpha = 50;
		}
		else if (base.Projectile.alpha > 50)
		{
			base.Projectile.alpha -= 10;
		}
		base.Projectile.scale += 0.002f;
		if (base.Projectile.ai[0] % 60f == 0f)
		{
			base.Projectile.damage *= 2;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item54, base.Projectile.position);
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 60);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int i = 0; i < 25; i++)
		{
			int waterDust = Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 154);
			Main.dust[waterDust].position = (Main.dust[waterDust].position + base.Projectile.position) / 2f;
			Main.dust[waterDust].velocity = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			((Vector2)(ref Main.dust[waterDust].velocity)).Normalize();
			Dust obj = Main.dust[waterDust];
			obj.velocity *= (float)Main.rand.Next(1, 30) * 0.1f;
			Main.dust[waterDust].alpha = base.Projectile.alpha;
		}
	}
}
