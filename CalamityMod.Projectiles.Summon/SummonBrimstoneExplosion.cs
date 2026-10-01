using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SummonBrimstoneExplosion : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 160);
		base.Projectile.friendly = true;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = Main.projFrames[base.Type] * 5;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.DamageType = DamageClass.Summon;
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
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		float randomnessSmoothness = 3f;
		for (int i = 0; i < 10; i++)
		{
			randomnessSmoothness += (float)i;
			int offsetVariance = (int)(randomnessSmoothness * 4f);
			for (int j = 0; j < 30; j++)
			{
				Vector2 velocity = Main.rand.NextVector2Square(0f - randomnessSmoothness, randomnessSmoothness).SafeNormalize(Vector2.UnitY) * randomnessSmoothness * 0.48f;
				Dust dust = Dust.NewDustDirect(base.Projectile.Center, offsetVariance, offsetVariance, 235, 0f, 0f, 100, default(Color), 1.6f);
				dust.position += Main.rand.NextVector2Square(-10f, 10f);
				dust.velocity = velocity;
				dust.noGravity = true;
			}
		}
		base.Projectile.Damage();
		base.Projectile.ExpandHitboxBy(48);
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		modifiers.SourceDamage *= 0.018f;
	}
}
