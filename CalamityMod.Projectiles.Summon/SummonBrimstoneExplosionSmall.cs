using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SummonBrimstoneExplosionSmall : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 108);
		base.Projectile.friendly = true;
		base.Projectile.hostile = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = Main.projFrames[base.Type] * 5;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			CreateInitialDust();
			base.Projectile.localAI[0] = 1f;
		}
		Vector2 center = base.Projectile.Center;
		Color red = Color.Red;
		Lighting.AddLight(center, ((Color)(ref red)).ToVector3() * 1.1f);
		if ((float)base.Projectile.timeLeft % 5f == 4f)
		{
			base.Projectile.frame++;
		}
	}

	public void CreateInitialDust()
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		float randomnessSmoothness = 3f;
		for (int i = 0; i < 6; i++)
		{
			randomnessSmoothness += (float)i * 0.36f;
			for (int j = 0; j < 30; j++)
			{
				Vector2 velocity = Main.rand.NextVector2Square(0f - randomnessSmoothness, randomnessSmoothness).SafeNormalize(Vector2.UnitY) * randomnessSmoothness * 0.48f;
				Dust dust = Dust.NewDustDirect(base.Projectile.position + Vector2.One * 22f, 1, 1, 235, 0f, 0f, 100, default(Color), 1.6f);
				dust.position += Main.rand.NextVector2Square(-10f, 10f);
				dust.velocity = velocity;
				dust.noGravity = true;
			}
		}
		base.Projectile.Damage();
	}
}
