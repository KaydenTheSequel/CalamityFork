using System;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Turret;

public class DraedonLaser : ModProjectile, ILocalizedModType, IModType
{
	public const int MaxTrailPoints = 50;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/LaserProj";

	public float TrailLength
	{
		get
		{
			return base.Projectile.localAI[0];
		}
		set
		{
			base.Projectile.localAI[0] = value;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 4;
		base.Projectile.extraUpdates = 4;
		base.Projectile.timeLeft = 240;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override bool PreAI()
	{
		if (base.Projectile.knockBack == 0f)
		{
			base.Projectile.hostile = true;
		}
		else
		{
			base.Projectile.friendly = true;
		}
		return true;
	}

	public override void AI()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundStyle style = CommonCalamitySounds.LaserCannonSound with
			{
				Volume = CommonCalamitySounds.LaserCannonSound.Volume * 0.23f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.localAI[0] = 1f;
		}
		base.Projectile.alpha = (int)(Math.Sin((float)base.Projectile.timeLeft / 240f * (float)Math.PI) * 1.600000023841858 * 255.0);
		if (base.Projectile.alpha > 255)
		{
			base.Projectile.alpha = 255;
		}
		TrailLength += 1.5f;
		if (TrailLength > 50f)
		{
			TrailLength = 50f;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return new Color(255, 190, 255, 0);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		return base.Projectile.DrawBeam(50f, 1.5f, lightColor);
	}
}
