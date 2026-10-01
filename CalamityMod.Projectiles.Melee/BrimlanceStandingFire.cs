using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class BrimlanceStandingFire : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 240;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.X != base.Projectile.velocity.X)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X * -0.1f;
		}
		if (base.Projectile.velocity.X != base.Projectile.velocity.X)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X * -0.5f;
		}
		if (base.Projectile.velocity.Y != base.Projectile.velocity.Y && base.Projectile.velocity.Y > 1f)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y * -0.5f;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 5f)
		{
			base.Projectile.ai[0] = 5f;
			if (base.Projectile.velocity.Y == 0f && base.Projectile.velocity.X != 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X * 0.97f;
				if ((double)base.Projectile.velocity.X > -0.01 && (double)base.Projectile.velocity.X < 0.01)
				{
					base.Projectile.velocity.X = 0f;
					base.Projectile.netUpdate = true;
				}
			}
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.2f;
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
		if (base.Projectile.ai[1] == 0f && base.Projectile.type >= 326 && base.Projectile.type <= 328)
		{
			base.Projectile.ai[1] = 1f;
			SoundEngine.PlaySound(in SoundID.Item13, base.Projectile.position);
		}
		int brimDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 235, 0f, 0f, 100);
		Main.dust[brimDust].position.X -= 2f;
		Main.dust[brimDust].position.Y += 2f;
		Main.dust[brimDust].scale += (float)Main.rand.Next(50) * 0.01f;
		Main.dust[brimDust].noGravity = true;
		Main.dust[brimDust].velocity.Y -= 2f;
		if (Main.rand.NextBool())
		{
			int brimDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 235, 0f, 0f, 100);
			Main.dust[brimDust2].position.X -= 2f;
			Main.dust[brimDust2].position.Y += 2f;
			Main.dust[brimDust2].scale += 0.3f + (float)Main.rand.Next(50) * 0.01f;
			Main.dust[brimDust2].noGravity = true;
			Dust obj = Main.dust[brimDust2];
			obj.velocity *= 0.1f;
		}
		if ((double)base.Projectile.velocity.Y < 0.25 && (double)base.Projectile.velocity.Y > 0.15)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X * 0.8f;
		}
		base.Projectile.rotation = (0f - base.Projectile.velocity.X) * 0.05f;
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 240);
	}
}
