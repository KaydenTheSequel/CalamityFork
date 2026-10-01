using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class LightBead : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 50;
		base.Projectile.scale = 1.2f;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.5f, 0.5f, 0.5f);
		base.Projectile.rotation += base.Projectile.velocity.X * 0.2f;
		base.Projectile.ai[1]++;
		if (Main.rand.NextBool(5))
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244);
			dust.noGravity = true;
			dust.velocity *= 0.5f;
			dust.scale *= 0.9f;
		}
		if (base.Projectile.ai[1] > 300f)
		{
			base.Projectile.scale -= 0.05f;
			if (base.Projectile.scale <= 0.2f)
			{
				base.Projectile.scale = 0.2f;
				base.Projectile.Kill();
				return;
			}
		}
		CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 200f, 15f, 15f);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 255, 200, base.Projectile.alpha);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 4; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 244, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
		int beadAmt = Main.rand.Next(2, 3);
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int b = 0; b < beadAmt; b++)
			{
				Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<LightBeadSplit>(), (int)((double)base.Projectile.damage * 0.5), 0f, base.Projectile.owner);
			}
		}
	}
}
