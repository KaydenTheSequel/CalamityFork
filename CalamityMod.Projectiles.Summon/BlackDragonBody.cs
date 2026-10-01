using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class BlackDragonBody : ModProjectile, ILocalizedModType, IModType
{
	public int segmentIndex = 1;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[base.Type] = false;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.timeLeft = 10;
		base.Projectile.width = 26;
		base.Projectile.height = 26;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 180;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 8;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 0;
		base.Projectile.aiStyle = -1;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.netImportant = true;
		base.Projectile.minion = true;
	}

	public override void OnSpawn(IEntitySource source)
	{
		Projectile[] projectile = Main.projectile;
		foreach (Projectile projectile2 in projectile)
		{
			if (projectile2.type == ModContent.ProjectileType<BlackDragonTail>() && projectile2.owner == base.Projectile.owner && projectile2.active)
			{
				segmentIndex = projectile2.ModProjectile<BlackDragonTail>().segmentIndex;
				projectile2.ModProjectile<BlackDragonTail>().segmentIndex++;
			}
		}
	}

	public override void AI()
	{
		if (Main.player[base.Projectile.owner].Calamity().celestialDragons)
		{
			base.Projectile.timeLeft = 2;
		}
	}

	internal void SegmentMove()
	{
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		bool live = false;
		Projectile nextSegment = new Projectile();
		new BlackDragonHead();
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile projectile = enumerator.Current;
			if (projectile.type == base.Type && projectile.owner == base.Projectile.owner && projectile.ModProjectile<BlackDragonBody>().segmentIndex == segmentIndex - 1)
			{
				live = true;
				nextSegment = projectile;
			}
			if (projectile.type == ModContent.ProjectileType<BlackDragonHead>() && projectile.owner == base.Projectile.owner)
			{
				if (segmentIndex == 1)
				{
					live = true;
					nextSegment = projectile;
				}
				projectile.ModProjectile<BlackDragonHead>();
			}
		}
		if (!live)
		{
			base.Projectile.Kill();
		}
		Vector2 destinationOffset = nextSegment.Center + nextSegment.velocity - base.Projectile.Center;
		if (nextSegment.rotation != base.Projectile.rotation)
		{
			float angle = MathHelper.WrapAngle(nextSegment.rotation - base.Projectile.rotation);
			destinationOffset = destinationOffset.RotatedBy(angle * 0.1f);
		}
		base.Projectile.rotation = destinationOffset.ToRotation();
		if (destinationOffset != Vector2.Zero)
		{
			base.Projectile.Center = nextSegment.Center + nextSegment.velocity - destinationOffset.SafeNormalize(Vector2.Zero) * 20f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		if (Main.player[base.Projectile.owner].ownedProjectileCounts[ModContent.ProjectileType<BlackDragonHead>()] <= 0)
		{
			return;
		}
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile projectile = enumerator.Current;
			if (projectile.type == ModContent.ProjectileType<BlackDragonHead>() && projectile.owner == base.Projectile.owner)
			{
				projectile.Kill();
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(153, 300);
	}
}
