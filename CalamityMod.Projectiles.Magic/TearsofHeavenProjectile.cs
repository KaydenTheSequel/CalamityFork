using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class TearsofHeavenProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = 2;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 240;
		base.Projectile.light = 0.5f;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
	}

	public override void AI()
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 25;
			if (base.Projectile.alpha < 0)
			{
				base.Projectile.alpha = 0;
			}
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 4f)
		{
			int Smalldust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 100);
			Main.dust[Smalldust].noGravity = true;
			int Bigdust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 100, default(Color), 2f);
			Main.dust[Bigdust].noGravity = true;
		}
		CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 250f, 8f, 20f);
		base.Projectile.rotation += 0.3f * (float)base.Projectile.direction;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(323, 180);
		target.AddBuff(324, 180);
	}
}
