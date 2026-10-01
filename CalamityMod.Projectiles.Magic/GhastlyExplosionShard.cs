using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class GhastlyExplosionShard : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 120;
		base.Projectile.penetrate = 1;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.timeLeft < 90 && target.CanBeChasedBy(base.Projectile);
	}

	public override void AI()
	{
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		int dustType = (int)base.Projectile.ai[0];
		base.Projectile.ai[1]++;
		float dustScale = (120f - base.Projectile.ai[1]) / 120f;
		if (base.Projectile.ai[1] > 120f)
		{
			base.Projectile.Kill();
		}
		base.Projectile.velocity.Y += 0.2f;
		if (base.Projectile.velocity.Y > 18f)
		{
			base.Projectile.velocity.Y = 18f;
		}
		base.Projectile.velocity.X *= 0.98f;
		for (int i = 0; i < 2; i++)
		{
			int explodeDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, base.Projectile.velocity.X, base.Projectile.velocity.Y, 50, default(Color), 1.1f);
			Main.dust[explodeDust].position = (Main.dust[explodeDust].position + base.Projectile.Center) / 2f;
			Main.dust[explodeDust].noGravity = true;
			Dust obj = Main.dust[explodeDust];
			obj.velocity *= 0.3f;
			Main.dust[explodeDust].scale *= dustScale;
		}
		for (int j = 0; j < 1; j++)
		{
			int explodeDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, base.Projectile.velocity.X, base.Projectile.velocity.Y, 50, default(Color), 0.6f);
			Main.dust[explodeDust2].position = (Main.dust[explodeDust2].position + base.Projectile.Center * 5f) / 6f;
			Dust obj2 = Main.dust[explodeDust2];
			obj2.velocity *= 0.1f;
			Main.dust[explodeDust2].noGravity = true;
			Main.dust[explodeDust2].fadeIn = 0.9f * dustScale;
			Main.dust[explodeDust2].scale *= dustScale;
		}
		if (base.Projectile.timeLeft < 90)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 600f, 12f, 20f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 10; i++)
		{
			int killDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, (int)base.Projectile.ai[0], base.Projectile.velocity.X * 0.1f, base.Projectile.velocity.Y * 0.1f, 0, default(Color), 0.5f);
			Main.dust[killDust].scale = 1f + (float)Main.rand.Next(-10, 11) * 0.01f;
			Main.dust[killDust].noGravity = true;
			Dust obj = Main.dust[killDust];
			obj.velocity *= 1.25f;
			Dust obj2 = Main.dust[killDust];
			obj2.velocity -= base.Projectile.oldVelocity / 10f;
		}
	}
}
