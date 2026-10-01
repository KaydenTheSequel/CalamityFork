using System;
using CalamityMod.Projectiles.Melee;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class FishboneBoomerangProjectile : ModProjectile, ILocalizedModType, IModType
{
	public static int ChargeupTime = 10;

	public static int Lifetime = 240;

	public CalamityUtils.CurveSegment pullback = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, -0.9424779f, 2);

	public CalamityUtils.CurveSegment throwout = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0.7f, -0.9424779f, (float)Math.PI * 4f / 5f, 3);

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/FishboneBoomerang";

	public float OverallProgress => 1f - (float)base.Projectile.timeLeft / (float)Lifetime;

	public float ThrowProgress => 1f - (float)base.Projectile.timeLeft / (float)Lifetime;

	public float ChargeProgress => 1f - (float)(base.Projectile.timeLeft - Lifetime) / (float)ChargeupTime;

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Returning => ref base.Projectile.ai[0];

	public ref float Bouncing => ref base.Projectile.ai[1];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 38;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = Lifetime + ChargeupTime;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ignoreWater = true;
	}

	public override bool ShouldUpdatePosition()
	{
		return ChargeProgress >= 1f;
	}

	public override bool? CanDamage()
	{
		if (ChargeProgress < 1f)
		{
			return false;
		}
		return base.CanDamage();
	}

	internal float ArmAnticipationMovement()
	{
		return CalamityUtils.PiecewiseAnimation(ChargeProgress, pullback, throwout);
	}

	public override void AI()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center;
		if (ChargeProgress < 1f)
		{
			float armRotation = ArmAnticipationMovement() * (float)Owner.direction;
			Owner.heldProj = base.Projectile.whoAmI;
			Projectile projectile = base.Projectile;
			Vector2 mountedCenter = Owner.MountedCenter;
			Vector2 unitY = Vector2.UnitY;
			double radians = armRotation * Owner.gravDir;
			center = default(Vector2);
			projectile.Center = mountedCenter + unitY.RotatedBy(radians, center) * -40f * Owner.gravDir;
			base.Projectile.rotation = (-(float)Math.PI / 2f + armRotation) * Owner.gravDir;
			Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, (float)Math.PI + armRotation);
			return;
		}
		if (base.Projectile.timeLeft == Lifetime)
		{
			SoundEngine.PlaySound(in SoundID.Item1, base.Projectile.Center);
			base.Projectile.Center = Owner.MountedCenter + base.Projectile.velocity * 12f;
			base.Projectile.velocity = base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 17.5f;
			base.Projectile.tileCollide = true;
		}
		if (base.Projectile.soundDelay <= 0)
		{
			SoundEngine.PlaySound(in SoundID.Item7, base.Projectile.Center);
			base.Projectile.soundDelay = 8;
		}
		base.Projectile.rotation += ((float)Math.PI / 16f + (float)Math.PI / 8f * Math.Clamp(ThrowProgress * 2f, 0f, 1f)) * (float)Math.Sign(base.Projectile.velocity.X);
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 2f && Bouncing == 0f)
		{
			Returning = 1f;
			base.Projectile.numHits = 0;
		}
		if (Returning == 0f && Bouncing == 0f && ((Vector2)(ref base.Projectile.velocity)).Length() > 2f && base.Projectile.timeLeft < 205 + ChargeupTime)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.88f;
		}
		if (Returning == 1f && ((Vector2)(ref base.Projectile.velocity)).Length() < 20f)
		{
			Projectile projectile3 = base.Projectile;
			projectile3.velocity *= 1.1f;
		}
		for (int i = 0; i < 2; i++)
		{
			Dust.NewDustPerfect(base.Projectile.Center + ((float)i * (float)Math.PI + base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * 14f, 176, ((float)i * (float)Math.PI + base.Projectile.rotation * (float)Math.Sign(base.Projectile.velocity.X)).ToRotationVector2() * 3f).noGravity = true;
		}
		if (Returning == 1f)
		{
			base.Projectile.tileCollide = false;
			base.Projectile.velocity = ((Vector2)(ref base.Projectile.velocity)).Length() * (Owner.MountedCenter - base.Projectile.Center).SafeNormalize(Vector2.One);
			center = base.Projectile.Center - Owner.MountedCenter;
			if (((Vector2)(ref center)).Length() < 24f)
			{
				base.Projectile.Kill();
			}
			if (base.Projectile.numHits >= 5)
			{
				ImpactEffects();
				base.Projectile.Kill();
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		ImpactEffects();
		for (int i = 0; i < 5; i++)
		{
			float streakRotation = Main.rand.NextFloat((float)Math.PI * 2f);
			for (int j = 0; j < 4; j++)
			{
				Dust.NewDustPerfect(base.Projectile.Center + streakRotation.ToRotationVector2() * (2f + 0.4f * (float)j), 176, streakRotation.ToRotationVector2() * (0.6f * (float)j + 3f), 0, default(Color), 1.4f).noGravity = true;
			}
		}
		if (base.Projectile.numHits > 2)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.3f;
			Returning = 1f;
			return;
		}
		NPC newTarget = null;
		float closestNPCDistance = 10000f;
		float targettingDistance = 400f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.whoAmI != target.whoAmI && n.CanBeChasedBy(base.Projectile))
			{
				Vector2 val = base.Projectile.Center - n.Center;
				float potentialNewDistance = ((Vector2)(ref val)).Length();
				if (potentialNewDistance < targettingDistance && potentialNewDistance < closestNPCDistance)
				{
					closestNPCDistance = potentialNewDistance;
					newTarget = n;
				}
			}
		}
		if (newTarget == null)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.3f;
			Returning = 1f;
			return;
		}
		if (base.Projectile.Calamity().stealthStrike && Returning != 1f && base.Projectile.owner == Main.myPlayer)
		{
			Vector2 velocity = default(Vector2);
			for (int s = 0; s < 3; s++)
			{
				((Vector2)(ref velocity))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
				while (velocity.X == 0f && velocity.Y == 0f)
				{
					((Vector2)(ref velocity))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
				}
				((Vector2)(ref velocity)).Normalize();
				velocity *= (float)Main.rand.Next(70, 101) * 0.1f;
				int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<BonebreakerFragment1>(), (int)((float)base.Projectile.damage * 0.5f), base.Projectile.knockBack * 0.5f, base.Projectile.owner, Main.rand.Next(0, 4));
				if (proj.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[proj].DamageType = RogueDamageClass.Instance;
				}
			}
		}
		Bouncing = 1f;
		base.Projectile.velocity = 15f * (newTarget.Center - base.Projectile.Center).SafeNormalize(Vector2.One);
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		ImpactEffects();
		base.Projectile.velocity = ((Vector2)(ref base.Projectile.oldVelocity)).Length() * 0.3f * (Owner.MountedCenter - base.Projectile.Center).SafeNormalize(Vector2.One);
		Returning = 1f;
		return false;
	}

	public void ImpactEffects()
	{
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound((base.Projectile.numHits < 10) ? SoundID.DD2_SkeletonHurt with
		{
			Volume = SoundID.DD2_SkeletonHurt.Volume * 0.8f,
			Pitch = SoundID.DD2_SkeletonHurt.Pitch + 0.1f * (float)base.Projectile.numHits
		} : Utils.SelectRandom<SoundStyle>(Main.rand, SoundID.DrumClosedHiHat, SoundID.DrumCymbal1, SoundID.DrumCymbal2, SoundID.DrumKick, SoundID.DrumTamaSnare, SoundID.DrumTomHigh, SoundID.DrumHiHat), base.Projectile.Center);
		int goreNumber = Main.rand.Next(4);
		for (int i = 0; i < goreNumber; i++)
		{
			int goreID = (Main.rand.NextBool() ? 266 : (Main.rand.NextBool() ? 971 : 972));
			Gore gore = Gore.NewGorePerfect(base.Projectile.GetSource_FromAI(), base.Projectile.position, base.Projectile.velocity * 0.2f + Main.rand.NextVector2Circular(5f, 5f), goreID);
			gore.scale = Main.rand.NextFloat(0.6f, 1f) * ((goreID == 972) ? 0.7f : 1f);
			gore.type = goreID;
		}
	}
}
