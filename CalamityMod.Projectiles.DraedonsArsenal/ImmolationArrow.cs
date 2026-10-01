using System;
using CalamityMod.Dusts;
using CalamityMod.Effects;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class ImmolationArrow : ModProjectile, ILocalizedModType, IModType
{
	public NPC chosenTarget;

	public bool stuckInTarget;

	public bool stuckInGround;

	public bool canDamage = true;

	public bool canStick = true;

	public int stuckTimer = 90;

	public Vector2 placementCenter;

	private float placementDistance;

	private Vector2 placementVelocity;

	public Vector2 storedVelocity;

	public bool collideWithTiles = true;

	public Vector2 startingVel;

	private NPC closestTarget;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public ref float time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Projectile.type] = 15;
		ProjectileID.Sets.TrailingMode[base.Projectile.type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.timeLeft = 180;
		base.Projectile.extraUpdates = 4;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0575: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Unknown result type (might be due to invalid IL or missing references)
		//IL_066e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0679: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		float targetDist = Vector2.Distance(Main.player[base.Projectile.owner].Center, base.Projectile.Center);
		if (closestTarget != null && closestTarget.life <= 0)
		{
			closestTarget = null;
		}
		if (time == 0f)
		{
			startingVel = base.Projectile.velocity;
			closestTarget = (base.Projectile.Center + base.Projectile.velocity * 2f).ClosestNPCAt(150f);
		}
		else
		{
			NPC attemptGetTarget = (base.Projectile.Center + base.Projectile.velocity * 2f).ClosestNPCAt(150f);
			if (attemptGetTarget != null)
			{
				closestTarget = attemptGetTarget;
			}
		}
		if (stuckInGround)
		{
			base.Projectile.extraUpdates = 2;
			base.Projectile.rotation = storedVelocity.ToRotation() + (float)Math.PI / 2f;
			stuckTimer--;
			if (stuckTimer <= 0)
			{
				base.Projectile.Kill();
			}
		}
		if (!stuckInTarget && !stuckInGround)
		{
			storedVelocity = base.Projectile.velocity;
			base.Projectile.rotation = storedVelocity.ToRotation() + (float)Math.PI / 2f;
			if (time > 5f && !stuckInTarget)
			{
				if (Main.rand.NextBool(3) && canStick)
				{
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, ArsenalEffects.ArsenalPlasmaDust, -base.Projectile.velocity);
					dust.scale = Main.rand.NextFloat(0.6f, 1.4f);
					dust.velocity = -base.Projectile.velocity.RotatedByRandom(0.30000001192092896) * Main.rand.NextFloat(0.1f, 0.7f);
					dust.noGravity = true;
					dust.color = ArsenalEffects.ArsenalPlasmaColor;
				}
				if (targetDist < 1400f)
				{
					GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, -base.Projectile.velocity, affectedByGravity: false, 13, 1.3f, ArsenalEffects.ArsenalPlasmaColor * 0.7f));
					if (Main.rand.NextBool(6))
					{
						Vector2 relativePosition = base.Projectile.Center + Main.rand.NextVector2Circular(12f, 12f);
						float speed = Main.rand.NextFloat(0.2f, 0.7f);
						GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(relativePosition, -base.Projectile.velocity * speed, affectedByGravity: false, 7, Main.rand.NextFloat(0.4f, 0.7f), ArsenalEffects.ArsenalPlasmaColor));
					}
				}
			}
		}
		else if (stuckInTarget)
		{
			base.Projectile.extraUpdates = 2;
			base.Projectile.rotation = storedVelocity.SafeNormalize(Vector2.UnitX).ToRotation() + (float)Math.PI / 2f;
			placementCenter = chosenTarget.Center + placementVelocity * placementDistance;
			base.Projectile.Center = placementCenter;
			stuckTimer--;
			if (chosenTarget.life <= 0)
			{
				stuckTimer = 0;
			}
			if (stuckTimer <= 0)
			{
				base.Projectile.Kill();
			}
		}
		if (stuckInTarget || stuckInGround)
		{
			if (Main.rand.NextBool(8))
			{
				float speed2 = Main.rand.NextFloat(0.2f, 1.5f);
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, -storedVelocity * speed2, affectedByGravity: false, 23, 0.7f * speed2, ArsenalEffects.ArsenalPlasmaColor * 0.7f));
			}
			if (Main.rand.NextBool())
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<SquashDust>(), -base.Projectile.velocity);
				dust2.scale = Main.rand.NextFloat(0.6f, 1.4f);
				dust2.velocity = Utils.RotatedByRandom(new Vector2(35f, 35f), 100.0) * Main.rand.NextFloat(0.1f, 0.7f) * Utils.GetLerpValue(90f, 0f, stuckTimer);
				dust2.noGravity = true;
				dust2.color = ArsenalEffects.ArsenalPlasmaColor;
			}
		}
		time++;
		if (collideWithTiles && Collision.SolidCollision(base.Projectile.Center, 4, 4))
		{
			canDamage = false;
			stuckInGround = true;
			storedVelocity = base.Projectile.velocity;
			base.Projectile.velocity = Vector2.Zero;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/RavagerRockPillarHit1");
			style.Volume = 0.25f;
			style.Pitch = Main.rand.NextFloat(-0.3f, -0.6f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.rotation = storedVelocity.ToRotation() + (float)Math.PI / 2f;
			collideWithTiles = false;
			style = new SoundStyle("CalamityMod/Sounds/Item/ImmolatorPreExplode");
			style.Volume = 0.3f;
			style.Pitch = 0.5f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			style = HolofibreImmolator.PlasmaSound with
			{
				Volume = 0.8f,
				Pitch = Main.rand.NextFloat(-0.3f, -0.4f)
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
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
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.88f);
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
			for (int i = 0; i < 12; i++)
			{
				int dustStyle = ArsenalEffects.ArsenalPlasmaDust;
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + storedVelocity.SafeNormalize(Vector2.UnitX) * 38f + Main.rand.NextVector2Circular(12f, 12f), dustStyle, base.Projectile.velocity);
				dust.scale = Main.rand.NextFloat(0.7f, 1.3f);
				dust.velocity = storedVelocity.RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(0.4f, 1.5f);
				dust.noGravity = false;
				dust.color = ArsenalEffects.ArsenalPlasmaColor;
				dust.fadeIn = 1.5f;
			}
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ImmolatorPreExplode");
			style.Volume = 0.3f;
			style.Pitch = 0.5f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			SoundEngine.PlaySound(HolofibreImmolator.PlasmaSound with
			{
				Volume = 0.8f,
				Pitch = Main.rand.NextFloat(-0.3f, -0.4f)
			}, base.Projectile.Center);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		float bonus = (stuckInGround ? 3f : 1.5f);
		float explosionDamage = (stuckInGround ? 2.3f : 0.5f);
		Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ImmolationBurst>(), (int)((float)base.Projectile.damage * explosionDamage), base.Projectile.knockBack * 2f, base.Projectile.owner, 0f, stuckInGround ? 1 : 0).scale = ((!stuckInGround) ? 1 : 2);
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, ArsenalEffects.ArsenalPlasmaColor * 0.75f, "CalamityMod/Particles/BloomRing", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 1.2f * bonus, 26, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, ArsenalEffects.ArsenalPlasmaColor * 0.75f, "CalamityMod/Particles/WaterFoam", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0f, 0.77f * bonus, 16, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (int i = 0; i < (int)(15f * bonus); i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, stuckInGround ? ModContent.DustType<SquashDust>() : ArsenalEffects.ArsenalPlasmaDust, -base.Projectile.velocity);
			dust.scale = Main.rand.NextFloat(0.9f, 1.8f);
			dust.velocity = Utils.RotatedByRandom(new Vector2(35f, 35f), 100.0) * Main.rand.NextFloat(0.1f, 0.7f) * Utils.GetLerpValue(90f, 0f, stuckTimer);
			dust.noGravity = false;
			dust.color = ArsenalEffects.ArsenalPlasmaColor;
			if (dust.type == ArsenalEffects.ArsenalPlasmaDust)
			{
				dust.fadeIn = 2f;
			}
		}
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/PlasmaBig");
		style.Volume = 1f;
		style.Pitch = Main.rand.NextFloat(-0.1f, 0.1f);
		SoundEngine.PlaySound(in style, base.Projectile.Center);
	}

	public override bool? CanDamage()
	{
		if (!canDamage)
		{
			return false;
		}
		return null;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		if (time < 1f)
		{
			return false;
		}
		Asset<Texture2D> tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/ImmolationArrow", (AssetRequestMode)2);
		Asset<Texture2D> bloom = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
		float randSize = Main.rand.NextFloat(0.9f, 1f);
		float fadeIn = (float)Math.Pow(Utils.GetLerpValue(90f, 5f, stuckTimer, clamped: true), 3.0);
		Color val;
		for (int i = 0; i < 6; i++)
		{
			Texture2D value = tex.Value;
			Vector2 position = base.Projectile.Center - Main.screenPosition - (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * 6f * (float)i;
			val = Color.Lerp(Color.White, ArsenalEffects.ArsenalPlasmaColor, (float)i * 0.35f);
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value, position, null, val * 0.5f, base.Projectile.rotation, tex.Size() * 0.5f, new Vector2(0.7f - 0.15f * (float)i, 0.7f + 0.15f * (float)i) * randSize * (0.6f + 0.25f * (float)i) * 2f, (SpriteEffects)0);
		}
		for (int j = 0; j < 3; j++)
		{
			Texture2D value2 = bloom.Value;
			Vector2 position2 = base.Projectile.Center - Main.screenPosition + (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * 3f;
			val = Color.Lerp(Color.White, ArsenalEffects.ArsenalPlasmaColor, 0.5f * (float)j);
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value2, position2, null, val * fadeIn * 0.3f, base.Projectile.rotation, bloom.Size() * 0.5f, new Vector2(1f - 0.2f * (float)j, 1f + 0.35f * (float)j) * randSize * (0.5f + 0.15f * (float)j), (SpriteEffects)0);
		}
		_ = 1.1f * new Vector2(0.5f, 1f) * 1.5f * randSize;
		return false;
	}
}
