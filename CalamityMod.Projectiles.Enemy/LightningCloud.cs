using System;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Enemy;

public class LightningCloud : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Enemy";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 54;
		base.Projectile.height = 28;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.hostile = true;
		base.Projectile.timeLeft = 180;
		base.Projectile.Opacity = 0f;
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frameCounter = 0;
			base.Projectile.frame++;
			int maxFrame = ((base.Projectile.timeLeft < 60) ? 6 : 3);
			if (base.Projectile.frame >= maxFrame)
			{
				base.Projectile.frame = 0;
			}
			if (base.Projectile.frame == 5 && Main.myPlayer == base.Projectile.owner)
			{
				SoundEngine.PlaySound(in CommonCalamitySounds.LightningSound, base.Projectile.Center);
				float ai = Main.rand.Next(100);
				Vector2 velocity = Vector2.UnitY * 7f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Bottom, velocity, 466, base.Projectile.damage, 0f, base.Projectile.owner, (float)Math.PI / 2f, ai);
			}
		}
		if (base.Projectile.timeLeft < 30)
		{
			base.Projectile.Opacity = MathHelper.Lerp(base.Projectile.Opacity, 0f, 0.14f);
		}
		else
		{
			base.Projectile.Opacity = MathHelper.Lerp(base.Projectile.Opacity, 1f, 0.33f);
		}
	}
}
