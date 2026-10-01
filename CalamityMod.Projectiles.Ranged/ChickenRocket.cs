using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ChickenRocket : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = DamageClass.Ranged;
	}

	public override void AI()
	{
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0334: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		float speed = ((Vector2)(ref base.Projectile.velocity)).Length();
		if (speed >= 12f)
		{
			for (int i = 0; i < 2; i++)
			{
				float dx = ((i == 1) ? (base.Projectile.velocity.X * 0.5f) : 0f);
				float dy = ((i == 1) ? (base.Projectile.velocity.Y * 0.5f) : 0f);
				int d = Dust.NewDust(new Vector2(base.Projectile.position.X + 3f + dx, base.Projectile.position.Y + 3f + dy) - base.Projectile.velocity * 0.5f, base.Projectile.width - 8, base.Projectile.height - 8, 6, 0f, 0f, 100);
				Main.dust[d].scale *= 2f + Main.rand.NextFloat();
				Dust obj = Main.dust[d];
				obj.velocity *= 0.2f;
				Main.dust[d].noGravity = true;
				d = Dust.NewDust(new Vector2(base.Projectile.position.X + 3f + dx, base.Projectile.position.Y + 3f + dy) - base.Projectile.velocity * 0.5f, base.Projectile.width - 8, base.Projectile.height - 8, 244, 0f, 0f, 100, default(Color), 0.5f);
				Main.dust[d].fadeIn = 1f + Main.rand.NextFloat(0.5f);
				Dust obj2 = Main.dust[d];
				obj2.velocity *= 0.05f;
			}
			if (speed < 18f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 1.006f;
			}
			else if (Main.rand.NextBool())
			{
				int d2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 100);
				Main.dust[d2].scale = 0.1f + Main.rand.NextFloat(0.5f);
				Main.dust[d2].fadeIn = 1.5f + Main.rand.NextFloat(0.5f);
				Main.dust[d2].noGravity = true;
				Main.dust[d2].position = base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, (0f - (float)base.Projectile.height) / 2f), (double)base.Projectile.rotation, default(Vector2)) * 1.1f;
				Main.rand.Next(2);
				d2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100);
				Main.dust[d2].scale = 1f + Main.rand.NextFloat(0.5f);
				Main.dust[d2].noGravity = true;
				Main.dust[d2].position = base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, (0f - (float)base.Projectile.height) / 2f - 6f), (double)base.Projectile.rotation, default(Vector2)) * 1.1f;
			}
		}
		base.Projectile.ai[0]++;
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
		base.Projectile.velocity.Y += 0.075f;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		base.Projectile.velocity.X = 0f;
		base.Projectile.velocity.Y = -15f;
		if (base.Projectile.timeLeft > 20)
		{
			base.Projectile.timeLeft = 20;
			return false;
		}
		return true;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ChickenExplosion>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
		}
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
	}
}
