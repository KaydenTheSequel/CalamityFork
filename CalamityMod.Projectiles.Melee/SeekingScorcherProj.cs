using System;
using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class SeekingScorcherProj : ModProjectile, ILocalizedModType, IModType
{
	public static int WindupSwingTime = 24;

	public static int MaxAirtime = 60;

	public static int CrystalTransformationTime = 30;

	public static int Fadetime = 30;

	public static int Lifetime = 600;

	public static float ExplosionDamageMult = 3f;

	public static float CrystalBounceDamageMult = 2f;

	public static float CrystalBounceSpeedMult = 1.25f;

	public static int MaxBounces = 3;

	public const float MaxNPCHomingRange = 960f;

	public const float MaxAxeHomingRange = 640f;

	public const float BonusRangePerEmpowerment = 80f;

	private List<int> PreviousNPCs = new List<int> { -1 };

	public CalamityUtils.CurveSegment pullback = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, -0.9424779f, 2);

	public CalamityUtils.CurveSegment throwout = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0.7f, -0.9424779f, (float)Math.PI * 4f / 5f, 3);

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/SeekingScorcher";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float WindupSwingProgress => ref base.Projectile.ai[0];

	public ref float PhaseTime => ref base.Projectile.ai[1];

	public ref float CrystalTimer => ref base.Projectile.ai[2];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 94);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = Lifetime + WindupSwingTime;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override bool ShouldUpdatePosition()
	{
		return WindupSwingProgress > 1f;
	}

	public override bool? CanDamage()
	{
		if (!(WindupSwingProgress <= 1f) && !(CrystalTimer >= (float)CrystalTransformationTime))
		{
			return base.CanDamage();
		}
		return false;
	}

	internal float ArmAnticipationMovement()
	{
		return CalamityUtils.PiecewiseAnimation(WindupSwingProgress, pullback, throwout);
	}

	public override void AI()
	{
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0654: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_0517: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_053c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06df: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		if (WindupSwingProgress <= 1f)
		{
			if (PhaseTime == 0f)
			{
				base.Projectile.originalDamage = base.Projectile.damage;
			}
			WindupSwingProgress = Utils.GetLerpValue(0f, WindupSwingTime, PhaseTime, clamped: true);
			PhaseTime++;
			if (WindupSwingProgress == 1f)
			{
				WindupSwingProgress = 2f;
				PhaseTime = 0f;
				base.Projectile.Center = Owner.MountedCenter + base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 72f;
				base.Projectile.velocity = (Main.MouseWorld - Owner.Center).SafeNormalize(Vector2.UnitX * (float)Owner.direction) * ((Vector2)(ref base.Projectile.velocity)).Length();
				base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X >= 0f).ToDirectionInt());
				base.Projectile.netUpdate = true;
				SoundEngine.PlaySound(in SeekingScorcher.ThrowSound, base.Projectile.Center);
			}
			else
			{
				float armRotation = ArmAnticipationMovement() * (float)Owner.direction;
				base.Projectile.Center = Owner.MountedCenter + Vector2.UnitY.RotatedBy(armRotation * Owner.gravDir) * -72f * Owner.gravDir;
				base.Projectile.rotation = (((Owner.direction == 1) ? 0f : (-(float)Math.PI / 2f)) - (float)Math.PI / 4f + armRotation) * Owner.gravDir;
				base.Projectile.spriteDirection = (base.Projectile.direction = Owner.direction);
				Owner.heldProj = base.Projectile.whoAmI;
				Owner.ChangeDir(MathF.Sign(Main.MouseWorld.X - Owner.Center.X));
				Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, (float)Math.PI + armRotation);
			}
			return;
		}
		if (CrystalTimer > 0f)
		{
			if (CrystalTimer >= (float)CrystalTransformationTime)
			{
				base.Projectile.velocity = Vector2.Zero;
				ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
				while (enumerator.MoveNext())
				{
					Projectile proj = enumerator.Current;
					if (proj.type != base.Type || !(proj.ai[0] > 1f) || !(proj.ai[2] <= 0f))
					{
						continue;
					}
					Rectangle hitbox = base.Projectile.Hitbox;
					if (((Rectangle)(ref hitbox)).Intersects(proj.Hitbox))
					{
						Explode();
						proj.damage = Math.Clamp((int)((float)proj.damage * CrystalBounceDamageMult), 0, (int)((float)proj.originalDamage * MathF.Pow(CrystalBounceDamageMult, MaxBounces)));
						proj.velocity = proj.velocity.RotatedBy(Main.rand.NextBool() ? MathHelper.ToRadians(Main.rand.NextFloat(108f, 162f)) : MathHelper.ToRadians(Main.rand.NextFloat(-162f, -108f))) * CrystalBounceSpeedMult;
						if (proj.MaxUpdates < 5)
						{
							proj.MaxUpdates++;
						}
						proj.ai[1] -= 30f;
						proj.ai[2] = -1f;
						proj.netUpdate = true;
					}
				}
			}
			else
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 0.85f;
				base.Projectile.rotation += (float)base.Projectile.direction * MathHelper.ToRadians(24f) * Utils.GetLerpValue(CrystalTransformationTime, 0f, CrystalTimer, clamped: true);
				CrystalTimer++;
			}
			base.Projectile.Opacity = Utils.GetLerpValue(0f, Fadetime, base.Projectile.timeLeft, clamped: true);
			if (base.Projectile.timeLeft <= Fadetime || Main.rand.NextBool(20))
			{
				Vector2 velocity = Vector2.UnitX.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(14f, 22f);
				float scale = Main.rand.NextFloat(0.6f, 1.2f);
				GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, velocity, Color.MistyRose, Color.Pink, scale, 30, 0.1f, 3f, Main.rand.NextFloat(0f, 0.01f)));
			}
			return;
		}
		if (CrystalTimer == -1f)
		{
			SharpBounce();
			base.Projectile.numHits++;
			CrystalTimer--;
		}
		PhaseTime++;
		base.Projectile.rotation += (float)base.Projectile.direction * MathHelper.ToRadians(24f);
		if (PhaseTime >= (float)MaxAirtime)
		{
			if (base.Projectile.numHits >= MaxBounces || CrystalTimer < 0f)
			{
				ReturnToOwner();
			}
			else
			{
				CrystalTimer++;
			}
		}
		if (Main.rand.NextBool())
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 244, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
		if (CrystalTimer < 0f)
		{
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, base.Projectile.velocity * 0.5f, Color.MistyRose, 24, Main.rand.NextFloat(1.6f, 2f), 0.25f, Main.rand.NextFloat(-0.1f, 0.1f), glowing: true));
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, 200);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<HolyFlames>(), 180);
		PreviousNPCs.Add(target.whoAmI);
		SharpBounce();
		base.Projectile.netUpdate = true;
		SoundEngine.PlaySound(in SeekingScorcher.HitSound, base.Projectile.Center);
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.OrangeRed * 0.6f, "CalamityMod/Particles/SoftRoundExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0f, 0.1f, 24, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (int i = 0; i < 10; i++)
		{
			Vector2 sparkVelocity = Vector2.UnitX.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(6f, 8f);
			float sparkScale = Main.rand.NextFloat(1f, 1.4f);
			GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, sparkVelocity, affectedByGravity: false, 24, sparkScale, Main.rand.NextBool() ? Color.Gold : Color.OrangeRed));
			Vector2 smokeVelocity = Vector2.UnitX.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(3f, 6f);
			float smokeScale = Main.rand.NextFloat(1.6f, 2f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, smokeVelocity, Color.MistyRose, 24, smokeScale, 0.25f, Main.rand.NextFloat(-0.1f, 0.1f), glowing: true));
		}
		if (CrystalTimer < 0f)
		{
			Vector2 maxScale = default(Vector2);
			for (int j = 0; j < 7; j++)
			{
				((Vector2)(ref maxScale))._002Ector(Main.rand.NextFloat(1.5f, 4f), Main.rand.NextFloat(0.6f, 1.5f));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.One, Color.MistyRose, "CalamityMod/Particles/BlastCone", maxScale, Main.rand.NextFloat((float)Math.PI * 2f), 1f, 0.5f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			for (int k = 0; k < 8; k++)
			{
				Vector2 velocity = Vector2.UnitX.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(16f, 28f);
				float scale = Main.rand.NextFloat(0.6f, 1.6f);
				GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, velocity, Color.MistyRose, Color.Pink, scale, 30, 0.1f, 3f, Main.rand.NextFloat(0f, 0.01f)));
			}
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.8f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		if (CrystalTimer >= (float)CrystalTransformationTime && PhaseTime > 0f)
		{
			SoundEngine.PlaySound(in SeekingScorcher.LightShatterSound, base.Projectile.Center);
			Vector2 maxScale = default(Vector2);
			for (int i = 0; i < 9; i++)
			{
				((Vector2)(ref maxScale))._002Ector(Main.rand.NextFloat(2f, 4.5f), Main.rand.NextFloat(1.4f, 2f));
				GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.One, Color.MistyRose, "CalamityMod/Particles/BlastCone", maxScale, Main.rand.NextFloat((float)Math.PI * 2f), 1f, 0.5f, 18, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
			}
			for (int j = 0; j < 15; j++)
			{
				Vector2 smokeVelocity = Vector2.UnitX.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(3f, 8f);
				float smokeScale = Main.rand.NextFloat(1.6f, 2f);
				GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, smokeVelocity, Color.MistyRose, 24, smokeScale, 0.25f, Main.rand.NextFloat(-0.1f, 0.1f), glowing: true));
			}
		}
	}

	public void SharpBounce()
	{
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.numHits >= MaxBounces)
		{
			PhaseTime = MaxAirtime;
			ReturnToOwner();
			return;
		}
		float range = 960f + 80f * (float)(base.Projectile.MaxUpdates - 2);
		int targetNPC = -1;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC target = enumerator.Current;
			if (target.CanBeChasedBy(base.Projectile) && !PreviousNPCs.Contains(target.whoAmI))
			{
				float distance = Vector2.Distance(target.Center, base.Projectile.Center);
				if (distance < range)
				{
					range = distance;
					targetNPC = target.whoAmI;
				}
			}
		}
		if (targetNPC != -1)
		{
			float range2 = 640f + 80f * (float)(base.Projectile.MaxUpdates - 2);
			int targetAxe = -1;
			ActiveEntityIterator<Projectile>.Enumerator enumerator2 = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator2.MoveNext())
			{
				Projectile proj = enumerator2.Current;
				if (proj.type == base.Type)
				{
					float distance2 = Vector2.Distance(proj.Center, base.Projectile.Center);
					if (distance2 < range2 && proj.ai[2] >= (float)CrystalTransformationTime && proj.timeLeft > Fadetime)
					{
						range2 = distance2;
						targetAxe = proj.whoAmI;
					}
				}
			}
			PhaseTime = -12f;
			if (targetAxe != -1 && base.Projectile.numHits < MaxBounces - 1)
			{
				base.Projectile.velocity = base.Projectile.SafeDirectionTo(Main.projectile[targetAxe].Center) * ((Vector2)(ref base.Projectile.velocity)).Length();
			}
			else
			{
				base.Projectile.velocity = base.Projectile.SafeDirectionTo(Main.npc[targetNPC].Center) * ((Vector2)(ref base.Projectile.velocity)).Length();
			}
		}
		else if (CrystalTimer < 0f || base.Projectile.numHits == MaxBounces - 1)
		{
			PhaseTime = MaxAirtime;
			ReturnToOwner();
		}
	}

	public void ReturnToOwner()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = base.Projectile.SafeDirectionTo(Owner.Center) * ((Vector2)(ref base.Projectile.velocity)).Length();
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = 2;
		Rectangle hitbox = base.Projectile.Hitbox;
		if (((Rectangle)(ref hitbox)).Intersects(Owner.Hitbox) || Vector2.Distance(base.Projectile.Center, Owner.Center) >= 3000f)
		{
			base.Projectile.Kill();
		}
	}

	public void Explode()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		PhaseTime = 0f;
		base.Projectile.Kill();
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<ScorcherExplosion>(), (int)((float)base.Projectile.damage * ExplosionDamageMult), base.Projectile.knockBack, base.Projectile.owner);
		SoundEngine.PlaySound(in SeekingScorcher.HitSound, base.Projectile.Center);
		SoundEngine.PlaySound(in SeekingScorcher.ShatterSound, base.Projectile.Center);
		GeneralParticleHandler.SpawnParticle(new DetailedExplosion(base.Projectile.Center, Vector2.Zero, Color.LightPink, Vector2.One, Main.rand.NextFloat(-5f, 5f), 0f, 0.75f, 25));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.MediumVioletRed, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0f, 0.2f, 30, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Orchid, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0f, 0.2f, 30, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.OrangeRed, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 1f, 6f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.OrangeRed, "CalamityMod/Particles/FlameExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0f, 0.2f, 25, UseAdditiveBlend: true, 0.9f, fade: true, 1f, (SpriteEffects)0));
		for (int i = 0; i < 24; i++)
		{
			GeneralParticleHandler.SpawnParticle(new CritSpark(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(40f, 40f), 100.0) * Main.rand.NextFloat(0.3f, 1f), Color.White, Color.Orchid, 0.9f, 35, 2f, 2.2f));
		}
		for (int j = 0; j < 2; j++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Orchid, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.3f, 1.5f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		for (int k = 0; k < 2; k++)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.2f, 1.1f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Projectile.type].Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Color drawColor = base.Projectile.GetAlpha(lightColor) * base.Projectile.Opacity;
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = texture.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)((float)base.Projectile.spriteDirection * Owner.gravDir == -1f);
		if (CrystalTimer != 0f)
		{
			float fade = ((base.Projectile.Opacity * CrystalTimer > 0f) ? Utils.GetLerpValue(0f, CrystalTransformationTime, CrystalTimer, clamped: true) : 1f);
			for (int i = 0; i < 10; i++)
			{
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 10f).ToRotationVector2() * 5f * fade;
				SpriteBatch spriteBatch = Main.spriteBatch;
				Vector2 val = drawPosition + drawOffset;
				Color mistyRose = Color.MistyRose;
				((Color)(ref mistyRose)).A = 0;
				spriteBatch.Draw(texture, val, (Rectangle?)null, mistyRose * fade, drawRotation, rotationPoint, base.Projectile.scale, flipSprite, 0f);
			}
		}
		Main.EntitySpriteDraw(texture, drawPosition, null, drawColor, drawRotation, rotationPoint, base.Projectile.scale * Owner.gravDir, flipSprite);
		return false;
	}
}
