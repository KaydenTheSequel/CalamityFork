using System;
using System.IO;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class StellarTorusSummon : ModProjectile, ILocalizedModType, IModType
{
	public int LaserSwingDirection;

	public bool DecidedSwingDirection;

	public bool HasReachedSwingRotationStart;

	public bool HasShotLaser;

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer ModdedOwner => Owner.Calamity();

	public NPC Target
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center.MinionHoming(StellarTorusStaff.EnemyDetectionDistance, Owner);
		}
	}

	public ref float TimerToShoot => ref base.Projectile.ai[0];

	public ref float RotationInterpolant => ref base.Projectile.ai[1];

	public ref float AnimationSpeed => ref base.Projectile.ai[2];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 12000;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.minionSlots = 1f;
		base.Projectile.penetrate = -1;
		base.Projectile.width = (base.Projectile.height = 52);
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(LaserSwingDirection);
		writer.Write(DecidedSwingDirection);
		writer.Write(HasReachedSwingRotationStart);
		writer.Write(HasShotLaser);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		LaserSwingDirection = reader.ReadInt32();
		DecidedSwingDirection = reader.ReadBoolean();
		HasReachedSwingRotationStart = reader.ReadBoolean();
		HasShotLaser = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		CheckMinionExistence();
		DoAnimation();
		base.Projectile.MinionAntiClump(0.1f);
		if (Target != null)
		{
			TimerToShoot++;
			if (TimerToShoot <= StellarTorusStaff.TimeBeforeCharging)
			{
				FollowPlayer();
				base.Projectile.rotation = base.Projectile.rotation.AngleTowards(base.Projectile.DirectionTo(Target.Center).ToRotation(), 0.05f);
				AnimationSpeed = 5f;
				SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Custom/ExoMechs/AresLaserArmCharge");
				soundStyle.Volume = 0.1f;
				soundStyle.Pitch = 0.8f;
				soundStyle.PitchVariance = 0.2f;
				SoundStyle laserCharge = soundStyle;
				SoundEngine.PlaySound(in laserCharge, base.Projectile.Center);
				return;
			}
			Color val;
			if (TimerToShoot > StellarTorusStaff.TimeBeforeCharging && TimerToShoot <= StellarTorusStaff.TimeBeforeCharging + StellarTorusStaff.TimeCharging)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.95f;
				AnimationSpeed = Utils.Remap(TimerToShoot, StellarTorusStaff.TimeBeforeCharging, StellarTorusStaff.TimeBeforeCharging + StellarTorusStaff.TimeCharging, 5f, 2f);
				float scale = (float)base.Projectile.width * base.Projectile.scale;
				float relativeScale = scale / 72f;
				Vector2 center = base.Projectile.Center;
				Vector2 velocity = base.Projectile.velocity;
				val = Color.Cyan;
				((Color)(ref val)).A = 10;
				GeneralParticleHandler.SpawnParticle(new GenericBloom(center, velocity, val, Utils.Remap(TimerToShoot, StellarTorusStaff.TimeBeforeCharging, StellarTorusStaff.TimeBeforeCharging + StellarTorusStaff.TimeCharging, relativeScale * 2f, relativeScale / 2f), (int)StellarTorusStaff.TimeCharging));
				Vector2 chargeDustSpawn = base.Projectile.Center + Main.rand.NextVector2Circular(scale * 1.2f, scale * 1.2f);
				Vector2? velocity2 = chargeDustSpawn.DirectionTo(base.Projectile.Center) * Main.rand.NextFloat(5f, 8f);
				val = default(Color);
				Dust dust = Dust.NewDustPerfect(chargeDustSpawn, 307, velocity2, 0, val);
				dust.noGravity = true;
				dust.scale = ((Vector2)(ref dust.velocity)).Length() * 0.15f;
				return;
			}
			AnimationSpeed = 2f;
			if (!DecidedSwingDirection)
			{
				LaserSwingDirection = Main.rand.NextBool().ToDirectionInt();
				DecidedSwingDirection = true;
				base.Projectile.netUpdate = true;
			}
			float swingRotStart = base.Projectile.DirectionTo(Target.Center).ToRotation() + (float)Math.PI / 8f * (float)LaserSwingDirection;
			if (!HasReachedSwingRotationStart)
			{
				base.Projectile.rotation = MathHelper.Lerp(base.Projectile.rotation, swingRotStart, RotationInterpolant);
				RotationInterpolant += 1f / 30f;
				if (RotationInterpolant < 1f)
				{
					return;
				}
				HasReachedSwingRotationStart = true;
				RotationInterpolant = 0f;
				int sparkAmount = 10;
				for (int sparkIndex = 0; sparkIndex < sparkAmount; sparkIndex++)
				{
					Vector2 velocity3 = ((float)Math.PI * 2f / (float)sparkAmount * (float)sparkIndex).ToRotationVector2().RotatedByRandom(MathHelper.ToRadians(15f)) * Main.rand.NextFloat(10f, 15f);
					GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, velocity3, affectedByGravity: true, 10, ((Vector2)(ref velocity3)).Length() * 0.1f, Color.Cyan));
				}
				SoundEngine.PlaySound(CommonCalamitySounds.LaserCannonSound with
				{
					Volume = 0.3f,
					Pitch = 0.8f,
					PitchVariance = 0.2f
				}, base.Projectile.Center);
				base.Projectile.netUpdate = true;
			}
			if (HasReachedSwingRotationStart)
			{
				ShootLaser();
				base.Projectile.rotation = MathHelper.Lerp(swingRotStart, swingRotStart + (float)Math.PI / 4f * (float)(-LaserSwingDirection), RotationInterpolant);
				RotationInterpolant += 1f / StellarTorusStaff.TimeShooting;
				Vector2 center2 = base.Projectile.Center;
				Vector2? velocity4 = Main.rand.NextVector2Circular(Main.rand.NextFloat(10f, 15f), Main.rand.NextFloat(10f, 15f));
				val = default(Color);
				Dust dust2 = Dust.NewDustPerfect(center2, 307, velocity4, 0, val);
				dust2.noGravity = true;
				dust2.scale = ((Vector2)(ref dust2.velocity)).Length() * 0.2f;
				if (RotationInterpolant >= 1f)
				{
					ResetTargettingVariables();
				}
			}
		}
		else
		{
			FollowPlayer();
			base.Projectile.rotation = ((MathF.Sign(base.Projectile.velocity.X) == 1) ? 0f : ((float)Math.PI));
			AnimationSpeed = 5f;
			ResetTargettingVariables();
		}
	}

	public void CheckMinionExistence()
	{
		Owner.AddBuff(ModContent.BuffType<StellarTorusBuff>(), 2);
		if (base.Type == ModContent.ProjectileType<StellarTorusSummon>())
		{
			if (Owner.dead)
			{
				ModdedOwner.StellarTorus = false;
			}
			if (ModdedOwner.StellarTorus)
			{
				base.Projectile.timeLeft = 2;
			}
		}
	}

	public void DoAnimation()
	{
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % (1 + (int)AnimationSpeed) == 0)
		{
			base.Projectile.frame = (base.Projectile.frame + 1) % Main.projFrames[base.Type];
		}
	}

	public void FollowPlayer()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		if (!base.Projectile.WithinRange(Owner.Center, StellarTorusStaff.EnemyDetectionDistance))
		{
			base.Projectile.Center = Owner.Center;
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.3f;
			base.Projectile.netUpdate = true;
		}
		else if (!base.Projectile.WithinRange(Owner.Center, StellarTorusStaff.EnemyDetectionDistance / 3f))
		{
			base.Projectile.velocity = (Owner.Center - base.Projectile.Center) / 30f;
			base.Projectile.netUpdate = true;
		}
		else if (!base.Projectile.WithinRange(Owner.Center, StellarTorusStaff.EnemyDetectionDistance / 8f))
		{
			base.Projectile.velocity = (base.Projectile.velocity * 37f + base.Projectile.SafeDirectionTo(Owner.Center) * 17f) / 40f;
			base.Projectile.netUpdate = true;
		}
	}

	public void ShootLaser()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		if (!HasShotLaser && Main.myPlayer == base.Projectile.owner)
		{
			int laser = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.rotation.ToRotationVector2(), ModContent.ProjectileType<StellarTorusBeam>(), base.Projectile.damage, base.Projectile.knockBack, Owner.whoAmI, 0f, base.Projectile.whoAmI, Target.whoAmI);
			if (Main.projectile.IndexInRange(laser))
			{
				Main.projectile[laser].originalDamage = base.Projectile.originalDamage;
			}
			HasShotLaser = true;
			base.Projectile.netUpdate = true;
		}
	}

	public void ResetTargettingVariables()
	{
		TimerToShoot = 0f;
		LaserSwingDirection = 0;
		RotationInterpolant = 0f;
		DecidedSwingDirection = false;
		HasReachedSwingRotationStart = false;
		HasShotLaser = false;
		base.Projectile.netUpdate = true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(origin: frame.Size() * 0.5f, texture: value, position: drawPosition, sourceRectangle: frame, color: base.Projectile.GetAlpha(lightColor), rotation: base.Projectile.rotation, scale: base.Projectile.scale, effects: (SpriteEffects)0);
		return false;
	}
}
