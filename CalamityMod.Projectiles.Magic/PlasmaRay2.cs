using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class PlasmaRay2 : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 4;
		base.Projectile.height = 4;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 100;
		base.Projectile.timeLeft = 40;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 65, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		if (base.Projectile.velocity.X != base.Projectile.velocity.X)
		{
			base.Projectile.position.X = base.Projectile.position.X + base.Projectile.velocity.X;
			base.Projectile.velocity.X = 0f - base.Projectile.velocity.X;
		}
		if (base.Projectile.velocity.Y != base.Projectile.velocity.Y)
		{
			base.Projectile.position.Y = base.Projectile.position.Y + base.Projectile.velocity.Y;
			base.Projectile.velocity.Y = 0f - base.Projectile.velocity.Y;
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 9f)
		{
			for (int i = 0; i < 4; i++)
			{
				Vector2 projPos = base.Projectile.position;
				projPos -= base.Projectile.velocity * ((float)i * 0.25f);
				base.Projectile.alpha = 255;
				int purpDust = Dust.NewDust(projPos, 1, 1, 173, 0f, 0f, 0, default(Color), 0.25f);
				Main.dust[purpDust].position = projPos;
				Main.dust[purpDust].scale = (float)Main.rand.Next(70, 110) * 0.013f;
				Dust obj = Main.dust[purpDust];
				obj.velocity *= 0.2f;
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 65, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}
}
