using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class FleshBallMinion : BaseMinionProjectile
{
	public const float FleshGravity = 0.25f;

	public const float MaxFallSpeed = 12f;

	public override int AssociatedProjectileTypeID => ModContent.ProjectileType<FleshBallMinion>();

	public override int AssociatedBuffTypeID => ModContent.BuffType<FleshBallBuff>();

	public override ref bool AssociatedMinionBool => ref base.ModdedOwner.fleshBall;

	public override bool PreHardmodeMinionTileVision => true;

	public ref float HopTimer => ref base.Projectile.ai[0];

	public ref float HopAmount => ref base.Projectile.ai[1];

	public bool SittingOnGround
	{
		get
		{
			if (Math.Abs(base.Projectile.velocity.X) < 1.55f)
			{
				return base.Projectile.velocity.Y == 0f;
			}
			return false;
		}
	}

	public override void SetStaticDefaults()
	{
		base.SetStaticDefaults();
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.width = 46;
		base.Projectile.height = 38;
		base.Projectile.extraUpdates = 1;
	}

	public override void MinionAI()
	{
		GenerateVisuals();
		HopTimer++;
		SufferFromSeparationAnxiety();
		if (base.Target == null)
		{
			GoNearOwner();
		}
		else
		{
			AttackTarget(base.Target);
		}
		EnforceGravity();
	}

	internal void EnforceGravity()
	{
		if (base.Projectile.velocity.Y < 12f)
		{
			base.Projectile.velocity.Y += 0.25f;
		}
	}

	internal void GenerateVisuals()
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			base.Projectile.rotation += base.Projectile.velocity.X * 0.05f;
			Vector2 shootOffsetDirection = -Vector2.UnitY.RotatedBy(base.Projectile.rotation + (float)base.Projectile.direction * 0.2f);
			Dust dust = Dust.NewDustDirect(base.Projectile.Center + shootOffsetDirection * 10f - new Vector2(4f), 0, 0, 5, 0f, 0f, 0, Color.Transparent);
			dust.velocity = shootOffsetDirection.RotatedByRandom(0.7853981852531433) * Main.rand.NextFloat(3f, 4f);
			dust.noGravity = true;
		}
	}

	internal void SufferFromSeparationAnxiety()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		float teleportPromptDistance = (Collision.CanHitLine(base.Projectile.Center, 1, 1, base.Owner.Center, 1, 1) ? 1900f : 805f);
		if (Main.myPlayer == base.Projectile.owner && !base.Projectile.WithinRange(base.Owner.Center, teleportPromptDistance))
		{
			base.Projectile.Center = base.Owner.Center;
			base.Projectile.netUpdate = true;
		}
	}

	internal void GoNearOwner()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.tileCollide = true;
		if (!base.Projectile.WithinRange(base.Owner.Center, 150f) && SittingOnGround && HopTimer % 30f == 29f)
		{
			base.Projectile.velocity = base.Projectile.SafeDirectionTo(base.Owner.Center) * 9f + new Vector2((float)Math.Sign(base.Projectile.velocity.X) * 2f, -9f);
			base.Projectile.tileCollide = false;
			base.Projectile.netUpdate = true;
		}
	}

	internal void AttackTarget(NPC target)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.tileCollide = true;
		if (!SittingOnGround || HopTimer % 20f != 19f)
		{
			return;
		}
		base.Projectile.velocity = base.Projectile.SafeDirectionTo(target.Center) * 6f + new Vector2((float)Math.Sign(base.Projectile.velocity.X) * 2f, -7f);
		HopAmount++;
		if (Main.myPlayer == base.Projectile.owner && HopAmount % 3f == 2f)
		{
			for (int i = 0; i < 2; i++)
			{
				Vector2 shootVelocity = -Vector2.UnitY.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(6f, 11f);
				int p = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Top, shootVelocity, ModContent.ProjectileType<FleshBlood>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				if (Main.projectile.IndexInRange(p))
				{
					Main.projectile[p].originalDamage = base.Projectile.originalDamage;
				}
			}
		}
		base.Projectile.tileCollide = false;
		base.Projectile.netUpdate = true;
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override void OnSpawn(IEntitySource source)
	{
		base.IFrames = 30;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		base.Projectile.velocity.X *= 0.9f;
		return false;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		fallThrough = base.Projectile.Bottom.Y < base.Owner.Top.Y;
		return true;
	}
}
