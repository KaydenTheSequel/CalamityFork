using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class VehemenceSkull : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 10;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 45);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 9;
		base.Projectile.DamageType = DamageClass.Magic;
	}

	public override void AI()
	{
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.ai[0] < 125f)
		{
			if (base.Projectile.frame >= 4)
			{
				base.Projectile.frame = 0;
			}
		}
		else if (base.Projectile.owner == Main.myPlayer && base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.Kill();
		}
		Lighting.AddLight(base.Projectile.Center, 0.36f, 0.09f, 0.09f);
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.972f;
		if (base.Projectile.alpha > 110)
		{
			base.Projectile.alpha -= 30;
			if (base.Projectile.alpha < 70)
			{
				base.Projectile.alpha = 70;
			}
		}
		if (Math.Abs(base.Projectile.velocity.X) > 0.1f)
		{
			base.Projectile.spriteDirection = -base.Projectile.direction;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		return new Color(191, 63, 54, 100);
	}
}
