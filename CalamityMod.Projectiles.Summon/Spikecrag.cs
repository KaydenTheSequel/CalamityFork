using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class Spikecrag : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 70;
		base.Projectile.height = 40;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = true;
		base.Projectile.sentry = true;
		base.Projectile.timeLeft = 36000;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
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
		float maxDistance = 1000f;
		bool homeIn = false;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.CanBeChasedBy(base.Projectile))
			{
				float extraDistance = (float)(n.width / 2) + (float)(n.height / 2);
				if (Vector2.Distance(n.Center, base.Projectile.Center) < maxDistance + extraDistance && Collision.CanHit(base.Projectile.Center, base.Projectile.width, base.Projectile.height, n.Center, n.width, n.height))
				{
					homeIn = true;
					break;
				}
			}
		}
		if (!((base.Projectile.owner == Main.myPlayer) & homeIn))
		{
			return;
		}
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] % 10f == 0f)
		{
			int amount = Main.rand.Next(2, 4);
			if (DownedBossSystem.downedProvidence && Main.zenithWorld)
			{
				amount += 6;
			}
			for (int i = 0; i < amount; i++)
			{
				float velocityX = Main.rand.NextFloat(-10f, 10f);
				float velocityY = Main.rand.NextFloat(-15f, -8f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.oldPosition.X + (float)(base.Projectile.width / 2), base.Projectile.oldPosition.Y + (float)(base.Projectile.height / 2), velocityX, velocityY, ModContent.ProjectileType<SpikecragSpike>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			}
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
