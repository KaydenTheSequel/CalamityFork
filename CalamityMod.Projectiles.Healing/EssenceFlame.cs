using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Healing;

public class EssenceFlame : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Healing";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.Projectile.alpha = 255;
		base.Projectile.extraUpdates = 3;
	}

	public override void AI()
	{
		base.Projectile.alpha -= 5;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 16)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.HealingProjectile((int)base.Projectile.ai[1], (int)base.Projectile.ai[0], 3.5f, 15f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item74, base.Projectile.Center);
		base.Projectile.position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2);
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int i = 0; i < 10; i++)
		{
			int godSlay = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[godSlay];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[godSlay].scale = 0.5f;
				Main.dust[godSlay].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 15; j++)
		{
			int godSlay2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100, default(Color), 3f);
			Main.dust[godSlay2].noGravity = true;
			Dust obj2 = Main.dust[godSlay2];
			obj2.velocity *= 5f;
			godSlay2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[godSlay2];
			obj3.velocity *= 2f;
		}
	}
}
