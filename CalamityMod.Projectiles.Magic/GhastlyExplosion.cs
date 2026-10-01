using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class GhastlyExplosion : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		int dustType = (int)base.Projectile.ai[0];
		for (int i = 0; i < 3; i++)
		{
			int ghostlyRed = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, base.Projectile.velocity.X, base.Projectile.velocity.Y, dustType, default(Color), 1.2f);
			Main.dust[ghostlyRed].position = (Main.dust[ghostlyRed].position + base.Projectile.Center) / 2f;
			Main.dust[ghostlyRed].noGravity = true;
			Dust obj = Main.dust[ghostlyRed];
			obj.velocity *= 0.5f;
		}
		for (int j = 0; j < 2; j++)
		{
			int ghostlyRed2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 60, base.Projectile.velocity.X, base.Projectile.velocity.Y, dustType, default(Color), 0.4f);
			switch (j)
			{
			case 0:
				Main.dust[ghostlyRed2].position = (Main.dust[ghostlyRed2].position + base.Projectile.Center * 5f) / 6f;
				break;
			case 1:
				Main.dust[ghostlyRed2].position = (Main.dust[ghostlyRed2].position + (base.Projectile.Center + base.Projectile.velocity / 2f) * 5f) / 6f;
				break;
			}
			Dust obj2 = Main.dust[ghostlyRed2];
			obj2.velocity *= 0.1f;
			Main.dust[ghostlyRed2].noGravity = true;
			Main.dust[ghostlyRed2].fadeIn = 1f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item50, base.Projectile.Center);
		for (int i = 0; i < 20; i++)
		{
			int killDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, (int)base.Projectile.ai[0], base.Projectile.velocity.X * 0.1f, base.Projectile.velocity.Y * 0.1f, 0, default(Color), 0.5f);
			Main.dust[killDust].scale = 1.2f + (float)Main.rand.Next(-10, 11) * 0.01f;
			Main.dust[killDust].noGravity = true;
			Dust obj = Main.dust[killDust];
			obj.velocity *= 2.5f;
			Dust obj2 = Main.dust[killDust];
			obj2.velocity -= base.Projectile.oldVelocity / 10f;
		}
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		Vector2 randShardRotation = default(Vector2);
		for (int j = 0; j < 3; j++)
		{
			((Vector2)(ref randShardRotation))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			while (randShardRotation.X == 0f && randShardRotation.Y == 0f)
			{
				((Vector2)(ref randShardRotation))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			}
			((Vector2)(ref randShardRotation)).Normalize();
			randShardRotation *= (float)Main.rand.Next(70, 101) * 0.1f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.oldPosition.X + (float)(base.Projectile.width / 2), base.Projectile.oldPosition.Y + (float)(base.Projectile.height / 2), randShardRotation.X, randShardRotation.Y, ModContent.ProjectileType<GhastlyExplosionShard>(), (int)((double)base.Projectile.damage * 0.8), base.Projectile.knockBack * 0.8f, base.Projectile.owner, base.Projectile.ai[0]);
		}
	}
}
