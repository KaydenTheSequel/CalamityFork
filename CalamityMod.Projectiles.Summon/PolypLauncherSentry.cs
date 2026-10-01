using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class PolypLauncherSentry : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 42;
		base.Projectile.height = 25;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = true;
		base.Projectile.sentry = true;
		base.Projectile.timeLeft = 36000;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.velocity.Y += 0.5f;
		if (base.Projectile.velocity.Y > 10f)
		{
			base.Projectile.velocity.Y = 10f;
		}
		if (base.Projectile.ai[0] > 0f)
		{
			base.Projectile.ai[0]--;
			return;
		}
		base.Projectile.ai[1] += Main.rand.Next(1, 3);
		NPC potentialTarget = base.Projectile.Center.MinionHoming(800f, player, ignoreTiles: false);
		if (base.Projectile.owner == Main.myPlayer && potentialTarget != null && base.Projectile.ai[1] > 40f)
		{
			Vector2 spawnPosition = default(Vector2);
			((Vector2)(ref spawnPosition))._002Ector(base.Projectile.oldPosition.X + (float)(base.Projectile.width / 2), base.Projectile.oldPosition.Y + (float)(base.Projectile.height / 2));
			float shootSpeed = 16f;
			float gravity = -0.4f;
			float distance = Vector2.Distance(spawnPosition, potentialTarget.Center);
			float angle = 0.25f * (float)Math.Asin(MathHelper.Clamp(gravity * distance * 1.5f / (float)Math.Pow(shootSpeed, 2.0), -1f, 1f));
			Vector2 velocity = Utils.RotatedBy(new Vector2(0f, 0f - shootSpeed), (double)angle, default(Vector2)).RotatedByRandom(0.10000000149011612);
			velocity.X *= (potentialTarget.Center.X - base.Projectile.Center.X < 0f).ToDirectionInt();
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPosition, velocity, ModContent.ProjectileType<PolypLauncherProjectile>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
