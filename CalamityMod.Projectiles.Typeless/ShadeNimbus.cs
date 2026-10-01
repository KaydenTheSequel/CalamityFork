using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class ShadeNimbus : ModProjectile, ILocalizedModType, IModType
{
	public const float Lifespan = 210f;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/Boss/ShadeNimbusHostile";

	public ref float RainTimer => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 54;
		base.Projectile.height = 24;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 8)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame++;
			if (base.Projectile.frame >= Main.projFrames[base.Type])
			{
				base.Projectile.frame = 0;
			}
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() > 0.5f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.9765f;
		}
		else
		{
			base.Projectile.velocity = Vector2.Zero;
		}
		float pushForce = 0.02f;
		for (int k = 0; k < Main.maxProjectiles; k++)
		{
			Projectile otherProj = Main.projectile[k];
			if (!otherProj.active || k == base.Projectile.whoAmI)
			{
				continue;
			}
			bool num = otherProj.type == base.Projectile.type;
			float taxicabDist = Vector2.Distance(base.Projectile.Center, otherProj.Center);
			float distanceGate = 20f;
			if (num && taxicabDist < distanceGate)
			{
				if (base.Projectile.position.X < otherProj.position.X)
				{
					base.Projectile.velocity.X -= pushForce;
				}
				else
				{
					base.Projectile.velocity.X += pushForce;
				}
			}
		}
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] >= 210f)
		{
			base.Projectile.alpha += 5;
			if (base.Projectile.alpha > 255)
			{
				base.Projectile.alpha = 255;
				base.Projectile.Kill();
			}
		}
		else
		{
			if (!(base.Projectile.ai[1] >= 15f))
			{
				return;
			}
			RainTimer++;
			if (RainTimer > 8f)
			{
				RainTimer = 0f;
				if (base.Projectile.owner == Main.myPlayer)
				{
					float rainX = base.Projectile.position.X + Main.rand.NextFloat(14f, (float)base.Projectile.width - 28f);
					float rainY = base.Projectile.position.Y + (float)base.Projectile.height + 4f;
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), rainX, rainY, 0f, 10f, ModContent.ProjectileType<ShadeNimbusRain>(), base.Projectile.damage, 0f, base.Projectile.owner);
				}
			}
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
