using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class AresGaussNukeProjectileSpark : ModProjectile, ILocalizedModType, IModType
{
	private const int timeLeft = 180;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.Opacity = 0f;
		base.CooldownSlot = 1;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 180;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] >= 15f)
		{
			base.Projectile.localAI[0] = 15f;
			base.Projectile.velocity.Y += 0.1f;
		}
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
		int fadeOutTime = 15;
		int fadeInTime = 3;
		if (base.Projectile.timeLeft < fadeOutTime)
		{
			base.Projectile.Opacity = MathHelper.Clamp((float)base.Projectile.timeLeft / (float)fadeOutTime, 0f, 1f);
		}
		else
		{
			base.Projectile.Opacity = MathHelper.Clamp(1f - (float)(base.Projectile.timeLeft - (180 - fadeInTime)) / (float)fadeInTime, 0f, 1f);
		}
		Lighting.AddLight(base.Projectile.Center, 0.1f * base.Projectile.Opacity, 0.125f * base.Projectile.Opacity, 0.025f * base.Projectile.Opacity);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) - (float)Math.PI / 2f;
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.Opacity == 1f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		((Color)(ref lightColor)).R = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).G = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).B = (byte)(255f * base.Projectile.Opacity);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
