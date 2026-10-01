using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class DukesDecapitatorBubble : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/Typeless/CoralBubble";

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
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
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item54, base.Projectile.position);
		int rando = Main.rand.Next(5, 9);
		for (int i = 0; i < rando; i++)
		{
			int dust = Dust.NewDust(base.Projectile.Center, 0, 0, 206, 0f, 0f, 100, default(Color), 1.4f);
			Dust obj = Main.dust[dust];
			obj.velocity *= 0.8f;
			Main.dust[dust].position = Vector2.Lerp(Main.dust[dust].position, base.Projectile.Center, 0.5f);
			Main.dust[dust].noGravity = true;
		}
	}
}
