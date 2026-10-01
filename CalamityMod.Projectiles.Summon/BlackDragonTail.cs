using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class BlackDragonTail : ModProjectile, ILocalizedModType, IModType
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
		base.Projectile.localNPCHitCooldown = 30;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 0;
		base.Projectile.aiStyle = -1;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.netImportant = true;
		base.Projectile.minion = true;
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
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		bool live = false;
		Projectile nextSegment = new Projectile();
		new BlackDragonHead();
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile projectile = enumerator.Current;
			if (projectile.type == ModContent.ProjectileType<BlackDragonBody>() && projectile.owner == base.Projectile.owner && projectile.ModProjectile<BlackDragonBody>().segmentIndex == segmentIndex - 1)
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
		Vector2 destinationOffset = nextSegment.Center - base.Projectile.Center;
		if (nextSegment.rotation != base.Projectile.rotation)
		{
			float angle = MathHelper.WrapAngle(nextSegment.rotation - base.Projectile.rotation);
			destinationOffset = destinationOffset.RotatedBy(angle * 0.1f);
		}
		base.Projectile.rotation = destinationOffset.ToRotation();
		if (destinationOffset != Vector2.Zero)
		{
			base.Projectile.Center = nextSegment.Center - destinationOffset.SafeNormalize(Vector2.Zero) * 20f;
		}
		base.Projectile.velocity = Vector2.Zero;
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
