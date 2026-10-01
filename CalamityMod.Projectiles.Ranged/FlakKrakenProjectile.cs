using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class FlakKrakenProjectile : ModProjectile, ILocalizedModType, IModType
{
	public bool HasHitEnemyWithInitialShot;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/Summon/CalamarisLamentProjectile";

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
		Main.projFrames[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 12;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.extraUpdates = 1;
		base.Projectile.width = (base.Projectile.height = 28);
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
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		if (Owner == null)
		{
			Player player = (Owner = Main.player[base.Projectile.owner]);
		}
		if (IsShrapnel && base.Projectile.velocity.Y < 25f)
		{
			base.Projectile.velocity.Y += FlakKraken.ProjectileGravityStrength;
		}
		if (!IsShrapnel)
		{
			base.Projectile.scale = 1.5f;
			base.Projectile.extraUpdates = 3;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 4)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
			base.Projectile.frameCounter = 0;
		}
		if (!IsShrapnel && !base.Projectile.WithinRange(Owner.MountedCenter, DistanceOwnerMouse))
		{
			base.Projectile.Kill();
		}
		if (Main.dedServ)
		{
			return;
		}
		base.Projectile.alpha = (int)Utils.Remap(base.Projectile.timeLeft, 30f, 0f, 0f, 255f);
		if (IsShrapnel)
		{
			if (base.Projectile.timeLeft % 2 == 0 && base.Projectile.timeLeft <= 590)
			{
				GeneralParticleHandler.SpawnParticle(new AltSparkParticle(base.Projectile.Center - base.Projectile.velocity * 1.5f, base.Projectile.velocity * 0.01f, affectedByGravity: false, 8, 1.3f, FlakKrakenHoldout.EffectsColor * 0.135f));
			}
		}
		else
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity * 3f + Main.rand.NextVector2Circular(12f, 12f), Main.rand.NextBool(4) ? 191 : FlakKrakenHoldout.DustEffectsID);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(1.4f, 1.65f);
			dust.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 0.7f);
		}
		Vector2 position = base.Projectile.position;
		int width = base.Projectile.width;
		int height = base.Projectile.height;
		int dustEffectsID = FlakKrakenHoldout.DustEffectsID;
		float scale = Main.rand.NextFloat(0.5f, 0.8f);
		Dust dust2 = Dust.NewDustDirect(position, width, height, dustEffectsID, 0f, 0f, 127, default(Color), scale);
		dust2.noGravity = true;
		dust2.noLight = true;
		dust2.alpha = (int)Utils.Remap(base.Projectile.timeLeft, 30f, 0f, 127f, 0f);
	}

	public override void OnSpawn(IEntitySource source)
	{
		base.Projectile.scale = 1.5f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 180);
		if (!IsShrapnel)
		{
			HasHitEnemyWithInitialShot = true;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 180);
		if (!IsShrapnel)
		{
			HasHitEnemyWithInitialShot = true;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= (IsShrapnel ? 1f : FlakKraken.InitialShotDamageMultiplier);
	}

	public override void ModifyHitPlayer(Player target, ref Player.HurtModifiers modifiers)
	{
		modifiers.SourceDamage *= (IsShrapnel ? 1f : FlakKraken.InitialShotDamageMultiplier);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
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
			int flakAmount = (usedClusterRockets ? FlakKraken.ClusterShrapnelAmount : FlakKraken.ShrapnelAmount);
			for (int i = 0; i < flakAmount; i++)
			{
				if (Main.myPlayer != base.Projectile.owner)
				{
					break;
				}
				Projectile shrapnel = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity.SafeNormalize(-Vector2.UnitY).RotatedByRandom(usedClusterRockets ? FlakKraken.ClusterShrapnelAngleOffset : FlakKraken.ShrapnelAngleOffset) * FlakKraken.ProjectileShootSpeed * Main.rand.NextFloat(0.45f, 0.85f), ModContent.ProjectileType<FlakKrakenProjectile>(), (int)((float)base.Projectile.damage * (HasHitEnemyWithInitialShot ? FlakKraken.InitialShotHitShrapnelDamageMultiplier : 0.6f)), base.Projectile.knockBack, base.Projectile.owner, RocketID, 0f, 1f);
				if (HasHitEnemyWithInitialShot)
				{
					shrapnel.ModProjectile<FlakKrakenProjectile>().HasHitEnemyWithInitialShot = true;
				}
			}
		}
		if (RocketID == 4446f)
		{
			base.Projectile.ExplodeTiles(blastRadius, info.respectStandardBlastImmunity, info.tilesToCheck, info.wallsToCheck);
		}
		if (!Main.dedServ && (!IsShrapnel || (IsShrapnel && !HasHitEnemyWithInitialShot)))
		{
			int dustAmount = Main.rand.Next(20, 31);
			for (int j = 0; j < dustAmount; j++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, FlakKrakenHoldout.DustEffectsID, Main.rand.NextVector2Circular(5f, 5f), 0, FlakKrakenHoldout.EffectsColor, Main.rand.NextFloat(0.8f, 1.2f));
				dust.noLight = true;
				dust.noLightEmittence = true;
			}
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, Color.White * 0.3f, Vector2.One, 0f, (float)base.Projectile.width / 1560f, (float)base.Projectile.width / 156f, 20));
			int blobAmount = Main.rand.Next(4, 7);
			for (int k = 0; k < blobAmount; k++)
			{
				GeneralParticleHandler.SpawnParticle(new WaterGlobParticle(base.Projectile.Center, Main.rand.NextVector2Circular(5f, 5f), Main.rand.NextFloat(0.8f, 1.2f))
				{
					Color = FlakKrakenHoldout.EffectsColor
				});
			}
			int mistAmount = Main.rand.Next(3, 6);
			for (int mistIndex = 0; mistIndex < mistAmount; mistIndex++)
			{
				Vector2 velocity = ((float)Math.PI * 2f / (float)mistAmount * (float)mistIndex).ToRotationVector2() * Main.rand.NextFloat(2f, 7f);
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, velocity, FlakKrakenHoldout.EffectsColor * Main.rand.NextFloat(0.15f, 0.25f), Main.rand.Next(45, 61), Main.rand.NextFloat(0.4f, 1.1f), Main.rand.NextFloat(0.2f, 0.35f)));
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Color drawColor = (IsShrapnel ? base.Projectile.GetAlpha(lightColor) : Color.Black);
		Vector2 origin = frame.Size() * 0.5f;
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor * 0.3f);
		Main.EntitySpriteDraw(value, drawPosition, frame, drawColor, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
