using System;
using CalamityMod.Dusts;
using CalamityMod.Enums;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class AntumbraShardProjectile : ModProjectile, ILocalizedModType, IModType
{
	public NPC chosenTarget;

	public bool stuckInTarget;

	public bool stuckInGround;

	public bool canDamage = true;

	public bool canStick = true;

	public int stuckTimer = 180;

	public Vector2 placementCenter;

	private float placementDistance;

	private Vector2 placementVelocity;

	public Vector2 storedVelocity;

	public bool collideWithTiles = true;

	public Vector2 startingVel;

	public bool jitter;

	public Vector2 portalSpot;

	public Vector2 shadowPlacement;

	public int clones = 6;

	private NPC closestTarget;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/ShardofAntumbra";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 2;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0681: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d24: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d29: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ddb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0de6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0df3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06db: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0701: Unknown result type (might be due to invalid IL or missing references)
		//IL_0706: Unknown result type (might be due to invalid IL or missing references)
		//IL_070b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_0817: Unknown result type (might be due to invalid IL or missing references)
		//IL_0739: Unknown result type (might be due to invalid IL or missing references)
		//IL_0746: Unknown result type (might be due to invalid IL or missing references)
		//IL_0753: Unknown result type (might be due to invalid IL or missing references)
		//IL_0759: Unknown result type (might be due to invalid IL or missing references)
		//IL_0789: Unknown result type (might be due to invalid IL or missing references)
		//IL_0797: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e40: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e45: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e6d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e7b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0e94: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ea7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ec8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ecd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0877: Unknown result type (might be due to invalid IL or missing references)
		//IL_088b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a74: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b31: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0af5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b00: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b05: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0975: Unknown result type (might be due to invalid IL or missing references)
		//IL_0989: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_063e: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_059f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ba4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0baa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bde: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c09: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c31: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c69: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc7: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		float targetDist = Vector2.Distance(Owner.Center, base.Projectile.Center);
		if (closestTarget != null && closestTarget.life <= 0)
		{
			closestTarget = null;
		}
		if (base.Projectile.ai[2] > 0f && time == 0f)
		{
			closestTarget = base.Projectile.Center.ClosestNPCAt(2000f);
			storedVelocity = (((closestTarget == null) ? Owner.Calamity().mouseWorld : closestTarget.Center) - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
			base.Projectile.alpha = 255;
			base.Projectile.velocity = Vector2.Zero;
			stuckInTarget = true;
			collideWithTiles = false;
			canDamage = false;
			portalSpot = base.Projectile.Center;
		}
		if (time == 0f)
		{
			startingVel = base.Projectile.velocity;
		}
		float fading = (stuckInTarget ? Utils.GetLerpValue(0f, 180f, stuckTimer) : 0.5f);
		if (stuckInTarget && time % (float)((int)(10f * fading) + 1) == 0f)
		{
			shadowPlacement = Main.rand.NextVector2Circular(30f, 30f);
			jitter = !jitter;
		}
		if (stuckInGround)
		{
			base.Projectile.rotation = storedVelocity.ToRotation() + MathHelper.ToRadians(45f * (float)((storedVelocity.X > 0f) ? 1 : (-1)));
		}
		if (!stuckInTarget && !stuckInGround)
		{
			storedVelocity = base.Projectile.velocity;
			base.Projectile.rotation = storedVelocity.ToRotation() + MathHelper.ToRadians(45f * (float)((storedVelocity.X > 0f) ? 1 : (-1)));
			if (base.Projectile.spriteDirection == -1)
			{
				base.Projectile.rotation -= (float)Math.PI / 2f;
			}
			if (time > 5f && !stuckInTarget)
			{
				if (Main.rand.NextBool(4) && canStick)
				{
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(6) ? 278 : 263, -base.Projectile.velocity);
					dust.scale = ((dust.type == 278) ? Main.rand.NextFloat(0.3f, 0.6f) : Main.rand.NextFloat(0.6f, 1.4f));
					dust.velocity = -base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.1f, 0.7f);
					dust.noGravity = true;
					dust.color = Color.LightGreen;
				}
				if ((base.Projectile.Calamity().stealthStrike || base.Projectile.ai[1] == 1f) && targetDist < 1400f)
				{
					float randVel = Main.rand.NextFloat(0.3f, 1.2f);
					int lifetime = (base.Projectile.Calamity().stealthStrike ? 24 : 8);
					GeneralParticleHandler.SpawnParticle(new AltSparkParticle(base.Projectile.Center, -base.Projectile.velocity * randVel, affectedByGravity: false, lifetime, 1.2f, Color.Black));
					SparkParticle sparkParticle = new SparkParticle(base.Projectile.Center, -base.Projectile.velocity * randVel, affectedByGravity: false, lifetime, 0.9f, Color.LightGreen);
					GeneralParticleHandler.SpawnParticle(sparkParticle);
					sparkParticle.DrawLayer = GeneralDrawLayer.AfterEverything;
					if (Main.rand.NextBool() && !base.Projectile.Calamity().stealthStrike)
					{
						Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, 278);
						dust2.noGravity = true;
						dust2.velocity = base.Projectile.velocity * randVel;
						dust2.scale = Main.rand.NextFloat(0.6f, 0.8f);
						dust2.color = Color.LightGreen;
					}
				}
				if (targetDist < 1400f && time % 3f == 0f && !canStick && base.Projectile.ai[1] == 0f)
				{
					Vector2 relativePosition = base.Projectile.Center + Main.rand.NextVector2Circular(15f, 15f);
					float speed = Main.rand.NextFloat(0.2f, 0.7f);
					GeneralParticleHandler.SpawnParticle(new AltSparkParticle(relativePosition, -base.Projectile.velocity * speed, affectedByGravity: false, 11, 0.8f, Color.Black));
					SparkParticle sparkParticle2 = new SparkParticle(relativePosition, -base.Projectile.velocity * speed, affectedByGravity: false, 11, 0.5f, Color.LightGreen);
					GeneralParticleHandler.SpawnParticle(sparkParticle2);
					sparkParticle2.DrawLayer = GeneralDrawLayer.AfterEverything;
				}
				if (closestTarget != null && base.Projectile.numHits < 1 && closestTarget.CanBeChasedBy(base.Projectile))
				{
					CalamityUtils.HomeInOnSelectedNPC(base.Projectile, base.Projectile.Center.ClosestNPCAt(2000f), ignoreTiles: true, 0.95f, 16f, 0.96f);
				}
			}
		}
		else if (stuckInTarget)
		{
			base.Projectile.rotation = storedVelocity.SafeNormalize(Vector2.UnitX).ToRotation() + MathHelper.ToRadians(45f * (float)((storedVelocity.X > 0f) ? 1 : (-1)));
			if (base.Projectile.ai[2] == 0f)
			{
				placementCenter = chosenTarget.Center + placementVelocity * placementDistance + storedVelocity * 2f;
				base.Projectile.Center = placementCenter;
				if (Main.rand.NextBool(4))
				{
					int dustStyle = ModContent.DustType<VoidDustInverted>();
					Dust dust3 = Dust.NewDustPerfect(portalSpot, dustStyle, base.Projectile.velocity);
					dust3.scale = Main.rand.NextFloat(0.6f, 1.1f);
					dust3.velocity = Utils.RotatedByRandom(new Vector2(12f, 12f), 100.0) * Main.rand.NextFloat(0.2f, 1f) * Utils.GetLerpValue(180f, 0f, stuckTimer);
					dust3.noGravity = true;
					dust3.color = Color.LightGreen;
				}
				if (stuckTimer <= 20)
				{
					base.Projectile.scale *= 0.97f;
				}
			}
			if (closestTarget == null)
			{
				closestTarget = base.Projectile.Center.ClosestNPCAt(2000f);
			}
			if (base.Projectile.Calamity().stealthStrike && time % 20f == 0f && clones > 0)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + Main.rand.NextVector2CircularEdge(450f, 450f) * Main.rand.NextFloat(0.7f, 1.3f), Vector2.Zero, ModContent.ProjectileType<AntumbraShardProjectile>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 1f, 5f);
				clones--;
			}
			stuckTimer--;
			if (base.Projectile.ai[2] == 0f && (chosenTarget.life <= 0 || chosenTarget == null))
			{
				if (clones > 0 && base.Projectile.Calamity().stealthStrike)
				{
					for (int i = 0; i < clones; i++)
					{
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center + Main.rand.NextVector2CircularEdge(450f, 450f) * Main.rand.NextFloat(0.7f, 1.3f), Vector2.Zero, ModContent.ProjectileType<AntumbraShardProjectile>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 1f, 5f);
					}
					clones = 0;
				}
				stuckTimer = 0;
			}
			if (stuckTimer <= 0)
			{
				for (int j = 0; j < Main.maxNPCs; j++)
				{
					base.Projectile.localNPCImmunity[j] = 0;
				}
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MeldExplosion");
				style.Volume = 0.3f;
				style.Pitch = 0.8f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				base.Projectile.ai[2] = 0f;
				base.Projectile.numHits = 0;
				base.Projectile.scale = 1f;
				jitter = false;
				canDamage = true;
				canStick = false;
				stuckInTarget = false;
				base.Projectile.extraUpdates = 4;
				base.Projectile.Center = portalSpot;
				if (closestTarget == null)
				{
					base.Projectile.velocity = (base.Projectile.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * -16f;
				}
				else
				{
					base.Projectile.velocity = (base.Projectile.Center - closestTarget.Center).SafeNormalize(Vector2.UnitX) * -16f;
				}
				for (int k = 0; k <= 12; k++)
				{
					float variance = Main.rand.NextFloat(-0.6f, 0.6f);
					int dustStyle2 = ModContent.DustType<VoidDustInverted>();
					Dust dust4 = Dust.NewDustPerfect(base.Projectile.Center, dustStyle2, base.Projectile.velocity);
					dust4.scale = Main.rand.NextFloat(1.5f, 1.7f) - Math.Abs(variance);
					dust4.velocity = base.Projectile.velocity.RotatedBy(variance) * Main.rand.NextFloat(0.3f, 1f) * (1f - Math.Abs(variance));
					dust4.noGravity = true;
					dust4.color = Color.LightGreen;
				}
				CustomPulse customPulse = new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.LightGreen * 0.85f, "CalamityMod/Particles/BloomRing", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 1f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0);
				GeneralParticleHandler.SpawnParticle(customPulse);
				customPulse.DrawLayer = GeneralDrawLayer.AfterEverything;
				base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f * (float)((storedVelocity.X > 0f) ? 1 : (-1)));
			}
		}
		time++;
		if (collideWithTiles && Collision.SolidCollision(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 68f, 4, 4))
		{
			canDamage = false;
			if (base.Projectile.timeLeft > 180)
			{
				base.Projectile.timeLeft = 180;
			}
			stuckInGround = true;
			storedVelocity = base.Projectile.velocity;
			base.Projectile.velocity = Vector2.Zero;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/RavagerRockPillarHit1");
			style.Volume = 0.25f;
			style.Pitch = Main.rand.NextFloat(-0.3f, -0.6f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.rotation = storedVelocity.ToRotation() + MathHelper.ToRadians(45f * (float)((storedVelocity.X > 0f) ? 1 : (-1)));
			collideWithTiles = false;
			for (int l = 0; l < 8; l++)
			{
				Dust dust5 = Dust.NewDustPerfect(base.Projectile.Center + storedVelocity.SafeNormalize(Vector2.UnitX) * 68f, ModContent.DustType<VoidDustInverted>(), Utils.RotatedByRandom(new Vector2(4f, 4f), 100.0) * Main.rand.NextFloat(0.2f, 1f), 0, default(Color), Main.rand.NextFloat(1.4f, 1.75f));
				dust5.noGravity = true;
				dust5.color = Color.LightGreen;
			}
		}
		if (base.Projectile.ai[2] == 0f)
		{
			base.Projectile.alpha = (int)Utils.Remap(base.Projectile.timeLeft, 0f, 60f, 255f, 0f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.8f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
		if (!stuckInTarget && canStick)
		{
			if (base.Projectile.timeLeft < 600)
			{
				base.Projectile.timeLeft = 600;
			}
			collideWithTiles = false;
			canDamage = false;
			placementDistance = 0f - Vector2.Distance(target.Center, base.Projectile.Center);
			placementVelocity = (target.Center - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
			placementCenter = placementVelocity * (placementDistance * 0.01f);
			chosenTarget = target;
			stuckInTarget = true;
			storedVelocity = base.Projectile.velocity;
			base.Projectile.velocity = Vector2.Zero;
			portalSpot = base.Projectile.Center + Main.rand.NextVector2CircularEdge(350f, 350f) * Main.rand.NextFloat(0.8f, 1.1f);
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/WulfrumKnifeTileHit2");
			style.Volume = 0.5f;
			style.Pitch = Main.rand.NextFloat(-0.3f, -0.4f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			for (int i = 0; i <= 11; i++)
			{
				int dustStyle = (Main.rand.NextBool() ? 66 : 263);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + storedVelocity.SafeNormalize(Vector2.UnitX) * 68f + Main.rand.NextVector2Circular(12f, 12f), dustStyle, base.Projectile.velocity);
				dust.scale = Main.rand.NextFloat(0.7f, 1.1f);
				dust.velocity = storedVelocity.RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(0.8f, 2.1f);
				dust.noGravity = true;
				dust.color = Color.LimeGreen;
			}
		}
		if (!canStick)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MeldSlice");
			style.Volume = 0.7f;
			style.Pitch = 0.4f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			style = new SoundStyle("CalamityMod/Sounds/Item/ExobladeBeamSlash");
			style.Volume = 0.15f;
			style.Pitch = 0.7f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_041b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0594: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0604: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0619: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		if (time <= 4f)
		{
			return false;
		}
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		Asset<Texture2D> tex2 = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/ShardofAntumbraGhost", (AssetRequestMode)2);
		Asset<Texture2D> portal = ModContent.Request<Texture2D>("CalamityMod/Particles/Light", (AssetRequestMode)2);
		float alpha = Utils.GetLerpValue(255f, 0f, base.Projectile.alpha);
		float fading = (stuckInTarget ? Utils.GetLerpValue(0f, 180f, stuckTimer) : 0.5f);
		float portalFading = Utils.GetLerpValue(75f, 0f, stuckTimer, clamped: true);
		Vector2 generalDrawPos = base.Projectile.Center - Main.screenPosition;
		Vector2 portalDrawPos = portalSpot - Main.screenPosition;
		Color val;
		if (jitter && stuckInTarget)
		{
			Main.EntitySpriteDraw(tex2.Value, generalDrawPos + shadowPlacement * (1f - fading), null, Color.Black * alpha, base.Projectile.rotation, tex2.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)((!(storedVelocity.X > 0f)) ? 2 : 0));
			Main.EntitySpriteDraw(tex2.Value, generalDrawPos + -shadowPlacement * (1f - fading), null, Color.Black * alpha, base.Projectile.rotation, tex2.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)((!(storedVelocity.X > 0f)) ? 2 : 0));
			Main.EntitySpriteDraw(portal.Value, portalDrawPos, null, Color.Black * (1f - fading * 0.3f), 0f, portal.Size() * 0.5f, 1.8f * portalFading, (SpriteEffects)0);
			Texture2D value = portal.Value;
			val = Color.LightGreen;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value, portalDrawPos, null, val * (1f - fading), 0f, portal.Size() * 0.5f, 1.1f * portalFading, (SpriteEffects)0);
			Vector2 expectedVel = (((closestTarget == null) ? Owner.Calamity().mouseWorld : closestTarget.Center) - portalSpot).SafeNormalize(Vector2.UnitX);
			for (int i = 0; i < 4; i++)
			{
				Texture2D value2 = tex2.Value;
				val = Color.LightGreen;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(value2, portalDrawPos, null, val * portalFading * 0.7f, expectedVel.ToRotation() + MathHelper.ToRadians(45f * (float)((expectedVel.X > 0f) ? 1 : (-1))), tex2.Size() * 0.5f, (1f - (float)i * 0.05f) * (1f - fading), (SpriteEffects)((!(expectedVel.X > 0f)) ? 2 : 0));
			}
		}
		else if (stuckInTarget)
		{
			Main.EntitySpriteDraw(portal.Value, portalDrawPos, null, Color.Black * (1f - fading * 0.3f), 0f, portal.Size() * 0.5f, 1.5f * portalFading, (SpriteEffects)0);
		}
		if (!canStick || stuckInTarget)
		{
			for (int j = 0; j < 7; j++)
			{
				Color auraColor = Color.LightGreen * (1f - fading);
				Vector2 rotationalDrawOffset = ((float)Math.PI * 2f * (float)j / 7f).ToRotationVector2() * 3f;
				Texture2D value3 = tex2.Value;
				Vector2 position = base.Projectile.Center - Main.screenPosition + rotationalDrawOffset;
				val = auraColor * alpha;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(value3, position, null, val, base.Projectile.rotation, tex2.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)((!(storedVelocity.X > 0f)) ? 2 : 0));
			}
		}
		Main.EntitySpriteDraw(tex.Value, generalDrawPos, null, lightColor * alpha * (stuckInTarget ? fading : 1f), base.Projectile.rotation, tex.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)((!(storedVelocity.X > 0f)) ? 2 : 0));
		if (stuckInTarget)
		{
			Texture2D value4 = tex2.Value;
			val = Color.LightGreen;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value4, generalDrawPos, null, val * alpha * (1f - fading) * 0.7f, base.Projectile.rotation, tex2.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)((!(storedVelocity.X > 0f)) ? 2 : 0));
			for (int k = 0; k < 3; k++)
			{
				Texture2D value5 = portal.Value;
				val = Color.LightGreen;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(value5, portalDrawPos, null, val * (1f - fading * 0.3f) * 0.7f, 0f, portal.Size() * 0.5f, (0.8f - (float)k * 0.15f) * portalFading, (SpriteEffects)0);
			}
		}
		return false;
	}

	public override bool? CanDamage()
	{
		if (!canDamage)
		{
			return false;
		}
		return null;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 68f, (base.Projectile.ai[1] == 1f) ? 60 : 20, targetHitbox);
	}
}
