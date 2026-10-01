using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class VegaSpark : ModProjectile, ILocalizedModType, IModType
{
	public static int lifetime = 150;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 5;
		base.Projectile.timeLeft = lifetime;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.localAI[0] = 20f;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
		base.Projectile.MaxUpdates = 2;
	}

	public override void AI()
	{
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += (float)base.Projectile.direction * base.Projectile.ai[0];
		if ((float)base.Projectile.timeLeft < (float)lifetime - base.Projectile.ai[1] && base.Projectile.localAI[0] >= 0f)
		{
			((Vector2)(ref base.Projectile.velocity)).Normalize();
			Projectile projectile = base.Projectile;
			projectile.velocity *= base.Projectile.localAI[0];
			base.Projectile.localAI[0]--;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.Kill();
		}
		GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, base.Projectile.velocity * 0.001f, affectedByGravity: false, 10, 1f, new Color(69, 69, 200)));
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position + base.Projectile.velocity, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		base.Projectile.Kill();
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 5; i++)
		{
			int dustToUse = Main.rand.Next(0, 3);
			int dustType = 0;
			switch (dustToUse)
			{
			case 0:
				dustType = 109;
				break;
			case 1:
				dustType = 111;
				break;
			case 2:
				dustType = 132;
				break;
			}
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, dustType, base.Projectile.velocity.X, base.Projectile.velocity.Y, 0, default(Color), 1.5f);
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= 0.75f;
		}
	}
}
