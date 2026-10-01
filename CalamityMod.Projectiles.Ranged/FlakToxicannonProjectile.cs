using System;
using System.IO;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class FlakToxicannonProjectile : ModProjectile, ILocalizedModType, IModType
{
	public bool HasHitEnemyWithInitialShot;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Enemy/FlakAcid";

	public ref float RocketID => ref base.Projectile.ai[0];

	public ref float DistanceOwnerMouse => ref base.Projectile.ai[1];

	public bool IsShrapnel
	{
		get
		{
			return base.Projectile.ai[2] == 1f;
		}
		set
		{
			base.Projectile.ai[2] = (value ? 1f : 0f);
		}
	}

	public Player Owner { get; set; }

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 12;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.extraUpdates = 1;
		base.Projectile.width = (base.Projectile.height = 23);
		base.Projectile.timeLeft = 600;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(HasHitEnemyWithInitialShot);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		HasHitEnemyWithInitialShot = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		if (Owner == null)
		{
			Player player = (Owner = Main.player[base.Projectile.owner]);
		}
		if (IsShrapnel && base.Projectile.velocity.Y < 25f)
		{
			base.Projectile.velocity.Y += FlakToxicannon.ProjectileGravityStrength;
		}
		if (!IsShrapnel)
		{
			base.Projectile.scale = 1.3f;
			base.Projectile.extraUpdates = 2;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		if (!IsShrapnel && !base.Projectile.WithinRange(Owner.MountedCenter, DistanceOwnerMouse))
		{
			base.Projectile.Kill();
		}
		if (Main.dedServ)
		{
			return;
		}
		base.Projectile.alpha = (int)Utils.Remap(base.Projectile.timeLeft, 30f, 0f, 0f, 255f);
		Color newColor;
		if (IsShrapnel)
		{
			if (base.Projectile.timeLeft % 2 == 0 && base.Projectile.timeLeft <= 590)
			{
				GeneralParticleHandler.SpawnParticle(new AltSparkParticle(base.Projectile.Center - base.Projectile.velocity * 1.5f, base.Projectile.velocity * 0.01f, affectedByGravity: false, 8, 1.3f, FlakToxicannonHoldout.EffectsColor * 0.135f));
			}
		}
		else
		{
			Vector2 position = base.Projectile.Center - base.Projectile.velocity * 3f + Main.rand.NextVector2Circular(12f, 12f);
			int type = (Main.rand.NextBool(3) ? 299 : FlakToxicannonHoldout.DustEffectsID);
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(position, type, null, 0, newColor);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(1.4f, 1.65f);
			dust.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 0.7f);
		}
		Vector2 position2 = base.Projectile.position;
		int width = base.Projectile.width;
		int height = base.Projectile.height;
		int type2 = (Main.rand.NextBool(3) ? 299 : FlakToxicannonHoldout.DustEffectsID);
		float scale = Main.rand.NextFloat(0.5f, 0.8f);
		newColor = default(Color);
		Dust dust2 = Dust.NewDustDirect(position2, width, height, type2, 0f, 0f, 127, newColor, scale);
		dust2.noGravity = true;
		dust2.noLight = true;
		dust2.alpha = (int)Utils.Remap(base.Projectile.timeLeft, 30f, 0f, 127f, 0f);
		Vector2 center = base.Projectile.Center;
		newColor = Color.GreenYellow;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * (IsShrapnel ? 1.5f : 2.5f));
	}

	public override void OnSpawn(IEntitySource source)
	{
		base.Projectile.scale = 1.5f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
		if (!IsShrapnel)
		{
			HasHitEnemyWithInitialShot = true;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
		if (!IsShrapnel)
		{
			HasHitEnemyWithInitialShot = true;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= (IsShrapnel ? 1f : FlakToxicannon.InitialShotDamageMultiplier);
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		modifiers.SourceDamage *= (IsShrapnel ? 1f : FlakToxicannon.InitialShotDamageMultiplier);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_035a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.RocketBehaviorInfo rocketBehaviorInfo = new CalamityUtils.RocketBehaviorInfo((int)RocketID);
		rocketBehaviorInfo.clusterProjectileID = 0;
		rocketBehaviorInfo.destructiveClusterProjectileID = 0;
		CalamityUtils.RocketBehaviorInfo info = rocketBehaviorInfo;
		int blastRadius = base.Projectile.RocketBehavior(info);
		base.Projectile.ExpandHitboxBy((float)blastRadius);
		base.Projectile.Damage();
		if (!IsShrapnel)
		{
			bool usedClusterRockets = RocketID == 4445f || RocketID == 4446f;
			int flakAmount = (usedClusterRockets ? FlakToxicannon.ClusterShrapnelAmount : FlakToxicannon.ShrapnelAmount);
			for (int i = 0; i < flakAmount; i++)
			{
				if (Main.myPlayer != base.Projectile.owner)
				{
					break;
				}
				Projectile shrapnel = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity.SafeNormalize(-Vector2.UnitY).RotatedByRandom(usedClusterRockets ? FlakToxicannon.ClusterShrapnelAngleOffset : FlakToxicannon.ShrapnelAngleOffset) * FlakToxicannon.ProjectileShootSpeed * Main.rand.NextFloat(0.45f, 0.85f), ModContent.ProjectileType<FlakToxicannonProjectile>(), (int)((float)base.Projectile.damage * (HasHitEnemyWithInitialShot ? FlakToxicannon.InitialShotHitShrapnelDamageMultiplier : 0.6f)), base.Projectile.knockBack, base.Projectile.owner, RocketID, 0f, 1f);
				if (HasHitEnemyWithInitialShot)
				{
					shrapnel.ModProjectile<FlakToxicannonProjectile>().HasHitEnemyWithInitialShot = true;
				}
			}
		}
		if (RocketID == 4446f)
		{
			base.Projectile.ExplodeTiles(blastRadius, info.respectStandardBlastImmunity, info.tilesToCheck, info.wallsToCheck);
		}
		if (Main.dedServ)
		{
			return;
		}
		for (int j = 0; j < (IsShrapnel ? 4 : 12); j++)
		{
			int sprayLifetime = Main.rand.Next(15, 21);
			float sprayScale = Main.rand.NextFloat(0.6f, 0.8f);
			Color sprayColor = (Main.rand.NextBool(3) ? Color.Chartreuse : FlakToxicannonHoldout.EffectsColor);
			if (Main.rand.NextBool(14))
			{
				sprayScale *= 2f;
			}
			float randomSpeedMultiplier = Main.rand.NextFloat(1.25f, 2.25f);
			Vector2 sprayVelocity = Main.rand.NextVector2Unit() * 5f * randomSpeedMultiplier;
			sprayVelocity.Y -= 5f;
			GeneralParticleHandler.SpawnParticle(new BloodParticle(base.Projectile.Center, sprayVelocity, sprayLifetime, sprayScale, sprayColor));
		}
		if (!IsShrapnel || (IsShrapnel && !HasHitEnemyWithInitialShot))
		{
			int dustAmount = Main.rand.Next(14, 24);
			for (int k = 0; k < dustAmount; k++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(4) ? 299 : FlakToxicannonHoldout.DustEffectsID, Main.rand.NextVector2Circular(5f, 5f), 0, FlakToxicannonHoldout.EffectsColor, Main.rand.NextFloat(0.8f, 1.2f));
				dust.noLight = true;
				dust.noLightEmittence = true;
			}
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, Color.White * 0.3f, Vector2.One, 0f, (float)base.Projectile.width / 1560f, (float)base.Projectile.width / 156f, 20));
			int mistAmount = Main.rand.Next(3, 6);
			for (int mistIndex = 0; mistIndex < mistAmount; mistIndex++)
			{
				Vector2 velocity = ((float)Math.PI * 2f / (float)mistAmount * (float)mistIndex).ToRotationVector2() * Main.rand.NextFloat(2f, 7f);
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, velocity, FlakToxicannonHoldout.EffectsColor * Main.rand.NextFloat(0.35f, 0.55f), Main.rand.Next(45, 61), Main.rand.NextFloat(0.4f, 1.1f), Main.rand.NextFloat(0.2f, 0.35f), 0f, glowing: true));
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = (IsShrapnel ? ModContent.Request<Texture2D>("CalamityMod/Projectiles/Environment/AcidDrop", (AssetRequestMode)2).Value : TextureAssets.Projectile[base.Type].Value);
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], drawColor * 0.35f, 1, texture);
		return false;
	}
}
