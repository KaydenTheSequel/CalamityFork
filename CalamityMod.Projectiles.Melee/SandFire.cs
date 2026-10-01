using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class SandFire : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 2;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.timeLeft = 200;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.25f, 0.25f, 0f);
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 4f)
		{
			for (int i = 0; i < 5; i++)
			{
				int shiny = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 159, 0f, 0f, 100, default(Color), 0.5f);
				Main.dust[shiny].noGravity = true;
				Dust obj = Main.dust[shiny];
				obj.velocity *= 0f;
			}
		}
		CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 2000f, 10f, 20f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 159, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(189, 120);
	}
}
