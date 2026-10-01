using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class EssenceDust : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 46;
		base.Projectile.height = 46;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 60;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.45f, 0f, 0.55f);
		for (int i = 0; i < 5; i++)
		{
			int essenceDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100, default(Color), 0.5f);
			Main.dust[essenceDust].noGravity = true;
			Dust obj = Main.dust[essenceDust];
			obj.velocity *= 0f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item74, base.Projectile.Center);
		base.Projectile.position = base.Projectile.Center;
		int size = ((base.Projectile.ai[0] > 60f) ? 192 : 96);
		base.Projectile.width = (base.Projectile.height = size);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int i = 0; i < 10; i++)
		{
			int godSlay = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100);
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
			int godSlay2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100, default(Color), 2f);
			Main.dust[godSlay2].noGravity = true;
			Dust obj2 = Main.dust[godSlay2];
			obj2.velocity *= 5f;
			godSlay2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100);
			Dust obj3 = Main.dust[godSlay2];
			obj3.velocity *= 2f;
		}
		base.Projectile.Damage();
	}
}
