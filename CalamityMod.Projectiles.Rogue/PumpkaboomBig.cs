using System;
using System.Collections.Generic;
using CalamityMod.Dusts;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

[PierceResistException(false)]
public class PumpkaboomBig : ModProjectile, ILocalizedModType, IModType
{
	public float time;

	public int tileHits;

	public float charge;

	public bool hasReachedFullCharge;

	public bool hasStoppedHolding;

	public float midAirRot;

	public int initialCastDirection;

	private Vector2 placementDistance;

	private Vector2 placementVelocity;

	private bool beginStretchAnim;

	private float progress;

	public Color mainColor;

	public Color c1;

	public Color c2;

	private bool hasDealtDamage;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/Rogue/PumpkaboomBig";

	public ref float stuckNPC => ref base.Projectile.ai[2];

	public ref float stuckState => ref base.Projectile.ai[1];

	public ref float flungState => ref base.Projectile.ai[0];

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 0;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 1200;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override bool ShouldUpdatePosition()
	{
		if (flungState != 0f)
		{
			return stuckState == 0f;
		}
		return false;
	}

	public override bool? CanDamage()
	{
		if (flungState == 0f || tileHits != 0)
		{
			return false;
		}
		return null;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (hasDealtDamage)
		{
			return false;
		}
		return null;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0511: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_061d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0600: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_063e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_066c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0676: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		//IL_0688: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0723: Unknown result type (might be due to invalid IL or missing references)
		float rate = Main.GlobalTimeWrappedHourly * 6f;
		List<Color> eColors = new List<Color> { c1, c2 };
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		mainColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		if (progress >= 0f && progress < 1f)
		{
			progress += 0.08f;
		}
		if (stuckState == 1f)
		{
			if (stuckNPC >= 0f && Main.npc[(int)stuckNPC].active)
			{
				base.Projectile.Center = Main.npc[(int)stuckNPC].Center + placementVelocity * placementDistance;
			}
			else if (Main.netMode != 1)
			{
				base.Projectile.Kill();
			}
			if (base.Projectile.timeLeft == 109)
			{
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/PumpkaboomStealthTicking");
				style.Pitch = 0f;
				style.Volume = 1f;
				style.MaxInstances = 4;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			if (base.Projectile.timeLeft >= 110)
			{
				return;
			}
			float pullStrength = 9f;
			float centerShift = 1f;
			float maxPullDistance = 240f;
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				NPC target = Main.npc[i];
				if (target != null && target.active && target.CanBeMoved(ignoreKBImmune: true) && Collision.CanHit(base.Projectile.Center, 1, 1, target.Center, 1, 1) && Vector2.Distance(target.Center, base.Projectile.Center) < maxPullDistance && (float)target.whoAmI != stuckNPC)
				{
					Vector2 moveDir = target.Center.DirectionTo(base.Projectile.Center).SafeNormalize(Vector2.UnitX);
					target.velocity = Vector2.Lerp(target.velocity, moveDir * pullStrength, 0.15f);
					target.Center += moveDir * centerShift;
				}
			}
			if (Main.rand.NextBool(3))
			{
				Vector2 dustVel2 = Vector2.UnitX.RotatedByRandom(100.0) * Main.rand.NextFloat(9.5f, 13f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + dustVel2.SafeNormalize(Vector2.UnitX) * 160f, ModContent.DustType<SquashDust>(), -dustVel2 * 1.2f, 0, default(Color), Main.rand.NextFloat(0.9f, 1.4f));
				dust.noGravity = true;
				dust.fadeIn = 0.8f;
				dust.color = c1;
			}
			return;
		}
		if (flungState != 0f)
		{
			if (initialCastDirection == 0)
			{
				initialCastDirection = Owner.direction;
			}
			base.Projectile.localAI[0]++;
			base.Projectile.localAI[1] = 5f;
			base.Projectile.velocity.Y += 0.22f;
			if (stuckState == 0f)
			{
				midAirRot += 0.03f * (float)initialCastDirection;
			}
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f + midAirRot;
		}
		else
		{
			base.Projectile.velocity = Owner.velocity;
			int useAnim = ((Owner.HeldItem.useAnimation > 0) ? Owner.HeldItem.useAnimation : 30);
			float completion = time / ((float)useAnim * 0.7f);
			if (Main.myPlayer == base.Projectile.owner)
			{
				if (completion >= 1f)
				{
					time = -1f;
					base.Projectile.Center = Owner.Center;
					Vector2 velocity = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld);
					base.Projectile.velocity = velocity * 16f;
					base.Projectile.tileCollide = true;
					flungState = 1f;
					base.Projectile.netUpdate = true;
				}
				Owner.direction = Math.Sign(Owner.Center.DirectionTo(Owner.Calamity().mouseWorld).X);
			}
			float grenadeRot = 0f;
			if (completion <= 0.75f)
			{
				float completionLerp = (float)Math.Pow(Utils.GetLerpValue(0f, 0.75f, completion, clamped: true), 2.0);
				grenadeRot = MathHelper.ToRadians(MathHelper.Lerp(120f, -75f, completionLerp) * (float)Owner.direction);
			}
			else
			{
				float releaseLerp = (float)Math.Pow(Utils.GetLerpValue(0.75f, 1f, completion, clamped: true), 3.0);
				grenadeRot = MathHelper.ToRadians(MathHelper.Lerp(-75f, 30f, releaseLerp) * (float)Owner.direction);
			}
			Vector2 mouseDir = ((Main.myPlayer == base.Projectile.owner) ? Owner.Center.DirectionTo(Owner.Calamity().mouseWorld) : base.Projectile.velocity.SafeNormalize(Vector2.UnitX));
			grenadeRot += mouseDir.ToRotation();
			Vector2 grenadePos = Owner.GetFrontHandPosition(Player.CompositeArmStretchAmount.Full, grenadeRot) + Utils.RotatedBy(new Vector2((float)((Owner.direction == 1) ? 5 : (-3)), (float)((Owner.direction == 1) ? (-24) : (-4))), (double)grenadeRot, default(Vector2));
			float completionLerp2 = (float)Math.Pow(Utils.GetLerpValue(0f, 0.7f, completion, clamped: true), 2.0);
			float grenadeHalfRot = MathHelper.ToRadians(MathHelper.Lerp(120f, -75f, completionLerp2) * (float)Owner.direction);
			base.Projectile.Center = grenadePos;
			base.Projectile.rotation = grenadeRot - MathHelper.ToRadians(25f * grenadeHalfRot) + ((Owner.direction == 1) ? MathHelper.ToRadians(180f) : 0f);
			Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, mouseDir.ToRotation() - MathHelper.ToRadians(90f));
			Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, grenadeRot - ((Owner.direction == 1) ? MathHelper.ToRadians(180f) : MathHelper.ToRadians(0f)));
		}
		time++;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		stuckState = 1f;
		stuckNPC = target.whoAmI;
		base.Projectile.localAI[1] = 5f;
		beginStretchAnim = true;
		base.Projectile.netUpdate = true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/WulfrumScrewdriverThud");
		style.Volume = 0.7f;
		style.Pitch = 0f;
		style.MaxInstances = 6;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		stuckState = 1f;
		stuckNPC = target.whoAmI;
		placementDistance = new Vector2(0f - Vector2.Distance(target.Center, base.Projectile.Center));
		placementVelocity = (target.Center - base.Projectile.Center).SafeNormalize(Vector2.UnitX);
		base.Projectile.velocity = Vector2.Zero;
		base.Projectile.tileCollide = false;
		base.Projectile.rotation = base.Projectile.oldVelocity.ToRotation() + (float)Math.PI / 2f + midAirRot;
		flungState = base.Projectile.rotation;
		hasDealtDamage = true;
		base.Projectile.localAI[1] = 5f;
		base.Projectile.timeLeft = 110;
		for (int i = 0; i < 6; i++)
		{
			int sparkLifetime = Main.rand.Next(13, 22);
			float sparkScale = Main.rand.NextFloat(0.55f, 1.1f);
			Color sparkColor = (Main.rand.NextBool() ? c1 : c2);
			Vector2 burstDirection = base.Projectile.oldVelocity + new Vector2(Main.rand.NextFloat(-2.25f, 2.25f), Main.rand.NextFloat(-6.75f, 6.75f));
			Vector2 spawnPos = target.Center + burstDirection * (float)target.width * 0.15f;
			Vector2 sparkVelocity = burstDirection * Main.rand.NextFloat(0.7f, 1.3f);
			if (Main.rand.NextBool())
			{
				GeneralParticleHandler.SpawnParticle(new AltSparkParticle(spawnPos, sparkVelocity, affectedByGravity: false, sparkLifetime, sparkScale, sparkColor));
			}
			else
			{
				GeneralParticleHandler.SpawnParticle(new LineParticle(spawnPos, sparkVelocity, affectedByGravity: false, sparkLifetime, sparkScale, sparkColor));
			}
		}
		base.Projectile.netUpdate = true;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[1] = 5f;
		if (flungState != -1f)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/PumpkinExplode1");
			style.Pitch = 0f;
			style.Volume = 0.75f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			style = new SoundStyle("CalamityMod/Sounds/Item/FlakKrakenShoot");
			style.Pitch = 0f;
			style.Volume = 0.6f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			if (Main.myPlayer == base.Projectile.owner)
			{
				Owner.SetScreenshake(5f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<PumpkaboomBoomBig>(), base.Projectile.damage * 2, base.Projectile.knockBack * 2f, base.Projectile.owner);
			}
			float scale = 0.18f;
			Vector2 center = base.Projectile.Center;
			GeneralParticleHandler.SpawnParticle(new CustomPulse(center, Vector2.Zero, c1, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.07f, scale, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			GeneralParticleHandler.SpawnParticle(new CustomPulse(center, Vector2.Zero, c1 * 0.33f, "CalamityMod/Particles/HighResHollowCircleHardEdge", Vector2.One, Main.rand.NextFloat(-10f, 10f), 0.07f, scale * 1.66f, 11, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			for (int k = 0; k < 16; k++)
			{
				Vector2 velocity = Utils.RotatedByRandom(new Vector2(12f, 12f), 100.0) * Main.rand.NextFloat(0.7f, 1.3f);
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + velocity, velocity, affectedByGravity: false, 45, Main.rand.NextFloat(0.95f, 1.35f), c1));
			}
			for (int i = 0; i < 30; i++)
			{
				Vector2 velocity2 = Utils.RotatedByRandom(new Vector2(10f, 10f), 100.0) * Main.rand.NextFloat(0.3f, 1.2f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + velocity2, 259, velocity2);
				dust.scale = Main.rand.NextFloat(1.15f, 1.45f);
				dust.noGravity = true;
			}
		}
		base.Projectile.netUpdate = true;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 21f, targetHitbox);
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		overPlayers.Add(index);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		if (beginStretchAnim)
		{
			progress = 0f;
			beginStretchAnim = false;
		}
		if (progress >= 0f)
		{
			float stretchFactorX;
			float stretchFactorY;
			if (progress < 0.5f)
			{
				float completion = progress / 0.5f;
				stretchFactorX = MathHelper.Lerp(1.7f, 0.7f, completion);
				stretchFactorY = MathHelper.Lerp(0.7f, 1.7f, completion);
			}
			else
			{
				float completion2 = (progress - 0.5f) / 0.5f;
				stretchFactorX = MathHelper.Lerp(0.7f, 1f, completion2);
				stretchFactorY = MathHelper.Lerp(1.7f, 1f, completion2);
			}
			if (progress >= 1f)
			{
				stretchFactorX = 1f;
				stretchFactorY = 1f;
				progress = -1f;
			}
			Texture2D texture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
			Vector2 origin = default(Vector2);
			((Vector2)(ref origin))._002Ector((float)texture.Width * 0.5f, (float)texture.Height * 0.5f);
			Vector2 finalSyringeScale = default(Vector2);
			((Vector2)(ref finalSyringeScale))._002Ector(stretchFactorX, stretchFactorY);
			Vector2 finalPos = base.Projectile.Center - Main.screenPosition;
			Main.spriteBatch.Draw(texture, finalPos, (Rectangle?)null, lightColor, base.Projectile.rotation, origin, finalSyringeScale, (SpriteEffects)0, 0f);
			return false;
		}
		Texture2D mainTexture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		Vector2 drawOrigin = mainTexture.Size() / 2f;
		float drawScale = base.Projectile.scale;
		float drawRotation = base.Projectile.rotation;
		Vector2 drawPosition;
		if (stuckState == 1f && base.Projectile.timeLeft <= 110)
		{
			float glowSine = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 25f);
			float num = MathHelper.Lerp(0.7f, 1f, glowSine);
			float lifeFadeIn = Utils.GetLerpValue(110f, 0f, base.Projectile.timeLeft, clamped: true);
			float finalGlowIntensity = num * lifeFadeIn;
			drawPosition = base.Projectile.Center - Main.screenPosition;
			for (int i = 0; i < 20; i++)
			{
				Vector2 glowOffset = ((float)Math.PI * 2f * (float)i / 15f).ToRotationVector2() * (3f + glowSine * 1f) * finalGlowIntensity;
				SpriteBatch spriteBatch = Main.spriteBatch;
				Vector2 val = drawPosition + glowOffset;
				Color val2 = mainColor;
				((Color)(ref val2)).A = 0;
				spriteBatch.Draw(mainTexture, val, (Rectangle?)null, val2 * finalGlowIntensity * 0.4f, flungState, drawOrigin, drawScale, spriteEffects, 0f);
			}
		}
		if (flungState == 0f)
		{
			spriteEffects = (SpriteEffects)2;
			int useAnim = ((Owner.HeldItem.useAnimation > 0) ? Owner.HeldItem.useAnimation : 30);
			float completion3 = time / ((float)useAnim * 0.7f);
			float grenadeRot;
			if (completion3 <= 0.75f)
			{
				float completionLerp = (float)Math.Pow(Utils.GetLerpValue(0f, 0.75f, completion3, clamped: true), 2.0);
				grenadeRot = MathHelper.ToRadians(MathHelper.Lerp(120f, -75f, completionLerp) * (float)Owner.direction);
			}
			else
			{
				float releaseLerp = (float)Math.Pow(Utils.GetLerpValue(0.75f, 1f, completion3, clamped: true), 3.0);
				grenadeRot = MathHelper.ToRadians(MathHelper.Lerp(-75f, 30f, releaseLerp) * (float)Owner.direction);
			}
			Vector2 mouseDir = ((Main.myPlayer == base.Projectile.owner) ? Owner.Center.DirectionTo(Owner.Calamity().mouseWorld) : base.Projectile.velocity.SafeNormalize(Vector2.UnitX));
			grenadeRot += mouseDir.ToRotation();
			drawPosition = Owner.GetFrontHandPosition(Player.CompositeArmStretchAmount.Full, grenadeRot) + Utils.RotatedBy(new Vector2((float)((Owner.direction == 1) ? 5 : 5), (float)((Owner.direction == 1) ? (-38) : 22)), (double)grenadeRot, default(Vector2)) - Main.screenPosition;
			drawRotation = grenadeRot + ((Owner.direction == 1) ? MathHelper.ToRadians(180f) : 0f);
		}
		else
		{
			if (stuckState == 1f)
			{
				drawRotation = flungState;
			}
			drawPosition = base.Projectile.Center - Main.screenPosition;
		}
		Main.spriteBatch.Draw(mainTexture, drawPosition, (Rectangle?)null, lightColor * (1f - (float)base.Projectile.alpha / 255f), drawRotation, drawOrigin, drawScale, spriteEffects, 0f);
		return false;
	}

	public PumpkaboomBig()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		progress = -1f;
		mainColor = Color.White;
		c1 = new Color(255, 117, 24);
		c2 = new Color(168, 47, 57);
		base._002Ector();
	}
}
