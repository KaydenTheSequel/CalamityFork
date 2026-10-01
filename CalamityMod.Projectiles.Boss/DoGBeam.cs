using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class DoGBeam : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.hostile = true;
		base.Projectile.scale = 2f;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 960;
		if (Main.zenithWorld)
		{
			base.Projectile.extraUpdates = 1;
		}
	}

	public override void AI()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 1)
		{
			base.Projectile.frame = 0;
		}
		Lighting.AddLight(base.Projectile.Center, 0f, 0.2f, 0.3f);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		int playerTracker = Player.FindClosest(base.Projectile.Center, 1, 1);
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] < 120f && base.Projectile.ai[1] > 30f)
		{
			float projSpeed = ((Vector2)(ref base.Projectile.velocity)).Length();
			Vector2 playerDistance = Main.player[playerTracker].Center - base.Projectile.Center;
			((Vector2)(ref playerDistance)).Normalize();
			playerDistance *= projSpeed;
			base.Projectile.velocity = (base.Projectile.velocity * 20f + playerDistance) / 21f;
			((Vector2)(ref base.Projectile.velocity)).Normalize();
			Projectile projectile = base.Projectile;
			projectile.velocity *= projSpeed;
		}
		if (base.Projectile.timeLeft == 950)
		{
			base.Projectile.damage = (int)base.Projectile.ai[0];
		}
		if (base.Projectile.timeLeft < 30)
		{
			base.Projectile.damage = 0;
		}
	}

	public override bool CanHitPlayer(Player target)
	{
		if (base.Projectile.timeLeft > 950 || base.Projectile.timeLeft < 30)
		{
			return false;
		}
		return true;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 950)
		{
			return new Color(0, 0, 0, 0);
		}
		if (base.Projectile.timeLeft < 30)
		{
			byte b2 = (byte)((double)base.Projectile.timeLeft * 8.5);
			byte a2 = (byte)(100f * ((float)(int)b2 / 255f));
			return new Color((int)b2, (int)b2, (int)b2, (int)a2);
		}
		return new Color(255, 255, 255, 100);
	}
}
