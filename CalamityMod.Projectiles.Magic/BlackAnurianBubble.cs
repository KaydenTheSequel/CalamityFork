using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class BlackAnurianBubble : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		if (base.Projectile.localAI[0] > 2f)
		{
			base.Projectile.alpha -= 20;
			if (base.Projectile.alpha < 100)
			{
				base.Projectile.alpha = 100;
			}
		}
		else
		{
			base.Projectile.localAI[0]++;
		}
		if (base.Projectile.ai[0] > 30f)
		{
			if (base.Projectile.velocity.Y > -8f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - 0.05f;
			}
			base.Projectile.velocity.X = base.Projectile.velocity.X * 0.98f;
		}
		else
		{
			base.Projectile.ai[0]++;
		}
		base.Projectile.rotation = base.Projectile.velocity.X * 0.1f;
		if (base.Projectile.wet)
		{
			if (base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y * 0.98f;
			}
			if (base.Projectile.velocity.Y > -8f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - 0.2f;
			}
			base.Projectile.velocity.X = base.Projectile.velocity.X * 0.94f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item54, base.Projectile.Center);
		for (int i = 0; i < 25; i++)
		{
			int blackDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 14);
			Main.dust[blackDust].position = (Main.dust[blackDust].position + base.Projectile.position) / 2f;
			Main.dust[blackDust].velocity = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			((Vector2)(ref Main.dust[blackDust].velocity)).Normalize();
			Dust obj = Main.dust[blackDust];
			obj.velocity *= (float)Main.rand.Next(1, 30) * 0.1f;
			Main.dust[blackDust].alpha = base.Projectile.alpha;
		}
	}
}
