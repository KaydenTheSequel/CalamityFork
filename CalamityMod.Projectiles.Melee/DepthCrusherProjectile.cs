using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class DepthCrusherProjectile : ModProjectile, ILocalizedModType, IModType
{
	public int ChargeupTime = 25;

	public int Lifetime = 255;

	public int startDamage;

	public bool setDamage;

	public int dustType1 = 104;

	public int dustType2 = 96;

	public CalamityUtils.CurveSegment pullback = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, -0.9424779f, 2);

	public CalamityUtils.CurveSegment throwout = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0.7f, -0.9424779f, (float)Math.PI * 4f / 5f, 3);

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/DepthCrusher";

	public float OverallProgress => 1f - (float)base.Projectile.timeLeft / (float)Lifetime;

	public float ThrowProgress => 1f - (float)base.Projectile.timeLeft / (float)Lifetime;

	public float ChargeProgress => 1f - (float)(base.Projectile.timeLeft - Lifetime) / (float)ChargeupTime;

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = Lifetime + ChargeupTime;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
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
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.spriteDirection = base.Projectile.direction;
		Vector3 Light = default(Vector3);
		((Vector3)(ref Light))._002Ector(0.05f, 0.05f, 0.25f);
		Lighting.AddLight(base.Projectile.Center, Light * 2f);
		if (ChargeProgress < 1f)
		{
			Owner.ChangeDir(MathF.Sign(Main.MouseWorld.X - Owner.Center.X));
			float armRotation = ArmAnticipationMovement() * (float)Owner.direction;
			Owner.heldProj = base.Projectile.whoAmI;
			base.Projectile.Center = Owner.MountedCenter + Vector2.UnitY.RotatedBy(armRotation * Owner.gravDir) * -45f * Owner.gravDir;
			base.Projectile.rotation = (-(float)Math.PI / 4f * (float)base.Projectile.direction + armRotation) * Owner.gravDir;
			Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, (float)Math.PI + armRotation);
			return;
		}
		if (base.Projectile.timeLeft == Lifetime)
		{
			base.Projectile.netUpdate = true;
			SoundEngine.PlaySound(in SoundID.Item1, base.Projectile.Center);
			base.Projectile.Center = Owner.MountedCenter + (Main.MouseWorld - Owner.Center).SafeNormalize(Vector2.UnitX * (float)Owner.direction) * 12f;
			base.Projectile.velocity = (Main.MouseWorld - Owner.Center).SafeNormalize(Vector2.UnitX * (float)Owner.direction) * 15f;
			startDamage = base.Projectile.damage;
			base.Projectile.spriteDirection = base.Projectile.direction;
		}
		base.Projectile.rotation += 0.4f * (MathF.Abs(base.Projectile.velocity.Y) * 0.2f + 0.6f) * (float)base.Projectile.direction;
		if (base.Projectile.velocity.Y < 16f)
		{
			base.Projectile.velocity.Y += ((base.Projectile.numHits >= 3) ? 0.7f : 0.4f);
		}
		if (base.Projectile.numHits >= 3)
		{
			base.Projectile.penetrate = -1;
			if (!setDamage)
			{
				base.Projectile.damage = (int)((float)base.Projectile.damage * 0.2f);
				setDamage = true;
			}
		}
		if (base.Projectile.velocity.Y > 0f)
		{
			base.Projectile.velocity.X *= 0.975f;
		}
		if (Collision.SolidCollision(base.Projectile.Center, 25, 25))
		{
			base.Projectile.tileCollide = true;
		}
		else
		{
			base.Projectile.tileCollide = false;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.timeLeft = 240;
		if (!setDamage)
		{
			if (base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.Y -= 6f;
			}
			else
			{
				base.Projectile.velocity.Y = -6f;
			}
		}
		for (int i = 0; i < 15; i++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? dustType1 : dustType2);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(0.5f, 1.1f);
			dust.velocity = Utils.RotatedByRandom(new Vector2(3f, 3f), 100.0) * Main.rand.NextFloat(0.5f, 1.5f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 40; i++)
		{
			float dustMulti = Main.rand.NextFloat(0.3f, 1.5f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? dustType1 : dustType2);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(1.6f, 2.5f) - dustMulti;
			dust.velocity = Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.3f, 1f) * dustMulti;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Custom/AbyssGravelMine2");
		soundStyle.Volume = 0.6f;
		soundStyle.PitchVariance = 0.3f;
		SoundStyle HitSound = soundStyle;
		base.Projectile.timeLeft = 240;
		if (base.Projectile.numHits < 3)
		{
			base.Projectile.numHits++;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Utils.RotatedByRandom(new Vector2(0f, -5f), 0.6000000238418579) * Main.rand.NextFloat(0.9f, 1.1f), ModContent.ProjectileType<DepthCrusherSplitProjectile>(), startDamage / 4, base.Projectile.knockBack / 4f, base.Projectile.owner);
			if (base.Projectile.velocity.X != oldVelocity.X)
			{
				base.Projectile.velocity.X = (0f - oldVelocity.X) * 0.8f;
			}
			if (base.Projectile.velocity.Y != oldVelocity.Y)
			{
				base.Projectile.velocity.Y = (0f - oldVelocity.Y) * 0.8f;
			}
			SoundEngine.PlaySound(HitSound with
			{
				Pitch = 0.15f
			}, base.Projectile.Center);
			for (int i = 0; i < 25; i++)
			{
				float dustMulti = Main.rand.NextFloat(0.3f, 1.5f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? dustType1 : dustType2);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.6f, 2.5f) - dustMulti;
				dust.velocity = Utils.RotatedByRandom(new Vector2(3f, 3f), 100.0) * Main.rand.NextFloat(0.3f, 1f) * dustMulti;
			}
		}
		else
		{
			SoundEngine.PlaySound(in HitSound, base.Projectile.Center);
			for (int j = 0; j < 3; j++)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Bottom, Utils.RotatedByRandom(new Vector2(0f, -5f), 0.6000000238418579) * Main.rand.NextFloat(0.9f, 1.1f), ModContent.ProjectileType<DepthCrusherSplitProjectile>(), (int)((float)startDamage * 0.3f), base.Projectile.knockBack / 4f, base.Projectile.owner);
			}
			base.Projectile.Kill();
		}
		return false;
	}
}
