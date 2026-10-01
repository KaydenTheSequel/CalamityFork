using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace CalamityMod.Projectiles.Summon;

public class PerditionBeacon : BaseMinionProjectile
{
	public override int AssociatedProjectileTypeID => ModContent.ProjectileType<PerditionBeacon>();

	public override int AssociatedBuffTypeID => ModContent.BuffType<PerditionBuff>();

	public override ref bool AssociatedMinionBool => ref base.ModdedOwner.perditionBeacon;

	public ref float AttackTime => ref base.Projectile.ai[0];

	public ref float AttackTimer => ref base.Projectile.ai[1];

	public ref float DownwardCrossFade => ref base.Projectile.localAI[1];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 16;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.width = 48;
		base.Projectile.height = 90;
		base.Projectile.minion = false;
		base.Projectile.sentry = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.timeLeft = 36000;
	}

	public override void SetOwnerTarget()
	{
		base.SetOwnerTarget();
		base.Target = (base.Owner.HasMinionAttackTargetNPC ? Main.npc[base.Owner.MinionAttackTargetNPC] : null);
	}

	public override void MinionAI()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.alpha = Utils.Clamp(base.Projectile.alpha - 8, 0, 255);
		FollowOwner();
		if (base.Target != null && base.Projectile.WithinRange(base.Target.Center, 2200f))
		{
			AttackTarget();
			DownwardCrossFade = MathHelper.Clamp(DownwardCrossFade + 0.025f, 0f, 1f);
			AttackTime++;
		}
		else
		{
			DownwardCrossFade = MathHelper.Clamp(DownwardCrossFade - 0.025f, 0f, 1f);
			AttackTime = 0f;
		}
		AttackTime++;
	}

	internal void FollowOwner()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		Vector2 destination = base.Owner.Top - Vector2.UnitY * MathHelper.Lerp(20f, 40f, (float)Math.Cos((float)base.Projectile.timeLeft / 24f) * 0.5f + 0.5f);
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, destination, 0.025f);
		Projectile projectile = base.Projectile;
		projectile.Center += (destination - base.Projectile.Center).SafeNormalize(Vector2.Zero) * 3f;
		if (base.Projectile.WithinRange(destination, 5f) || !base.Projectile.WithinRange(destination, 2200f))
		{
			base.Projectile.Center = destination;
		}
	}

	internal void AttackTarget()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		Dust cinder = Dust.NewDustPerfect(base.Target.Center + Main.rand.NextVector2Circular(800f, 800f), 6);
		cinder.velocity = Vector2.UnitY * (0f - Main.rand.NextFloat(3f, 7f));
		cinder.scale = 1f + ((Vector2)(ref cinder.velocity)).Length() * 0.17f;
		cinder.noGravity = true;
		int shootRate = (int)MathHelper.Lerp(24f, 6f, Utils.GetLerpValue(0f, 300f, AttackTime, clamped: true));
		AttackTimer++;
		if (!(AttackTimer < (float)shootRate) && Main.myPlayer == base.Projectile.owner)
		{
			AttackTimer = 0f;
			base.Projectile.netUpdate = true;
			WeightedRandom<int> rng = new WeightedRandom<int>(base.Projectile.identity * 2167 + (int)(Main.GlobalTimeWrappedHourly * 20f));
			rng.Add(ModContent.ProjectileType<LostSoulGold>(), 0.5);
			rng.Add(ModContent.ProjectileType<LostSoulGiant>(), 0.6000000238418579);
			rng.Add(ModContent.ProjectileType<LostSoulLarge>(), 0.800000011920929);
			rng.Add(ModContent.ProjectileType<LostSoulSmall>());
			Vector2 spawnPosition = base.Target.Center + Vector2.UnitY.RotatedByRandom(0.27000001072883606) * 1150f;
			Vector2 shootVelocity = (base.Target.Center - spawnPosition).SafeNormalize(-Vector2.UnitY).RotatedByRandom(0.09000000357627869) * Main.rand.NextFloat(19f, 31f);
			int soul = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPosition, shootVelocity, rng.Get(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			if (Main.projectile.IndexInRange(soul))
			{
				Main.projectile[soul].originalDamage = base.Projectile.originalDamage;
				Main.projectile[soul].DamageType = DamageClass.Summon;
			}
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		Main.player[base.Projectile.owner].UpdateMaxTurrets();
		if (!Main.dedServ)
		{
			for (int i = 0; i < 55; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(35f, 35f), 267);
				dust.velocity = Vector2.Lerp(dust.velocity, Vector2.UnitY * (0f - Main.rand.NextFloat(3.5f, 6f)), 0.5f);
				dust.color = Color.Lerp(Color.Orange, Color.Red, Main.rand.NextFloat(0f, 0.67f));
				dust.scale = Main.rand.NextFloat(1.2f, 1.5f);
				dust.noGravity = true;
			}
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		if (base.Target != null)
		{
			Texture2D crossTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/PerditionCross", (AssetRequestMode)2).Value;
			Vector2 drawPosition = base.Target.Bottom - Main.screenPosition;
			drawPosition.Y -= 12f;
			Color drawColor = Color.White * DownwardCrossFade;
			Main.EntitySpriteDraw(crossTexture, drawPosition, null, drawColor, base.Projectile.rotation, crossTexture.Size() * 0.5f, base.Projectile.scale * 0.85f, (SpriteEffects)0);
		}
	}
}
