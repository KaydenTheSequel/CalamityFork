using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class AbyssBladeProjectile : ModProjectile, ILocalizedModType, IModType
{
	public int Time;

	public int ChargeupTime;

	public int Lifetime;

	public int startDamage;

	public bool setDamage;

	public int dustType1;

	public int dustType2;

	public bool spinMode;

	public Vector2 NPCDestination;

	public SlotId SpinSoundSlot;

	public CalamityUtils.CurveSegment pullback;

	public CalamityUtils.CurveSegment throwout;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Items/Weapons/Melee/AbyssBlade";

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
		base.Projectile.width = 80;
		base.Projectile.height = 80;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = Lifetime + ChargeupTime;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0808: Unknown result type (might be due to invalid IL or missing references)
		//IL_080d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0821: Unknown result type (might be due to invalid IL or missing references)
		//IL_0826: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_0853: Unknown result type (might be due to invalid IL or missing references)
		//IL_0859: Unknown result type (might be due to invalid IL or missing references)
		//IL_088c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0891: Unknown result type (might be due to invalid IL or missing references)
		//IL_08aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_08af: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_054c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_0610: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_068e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_069e: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_0656: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		//IL_066c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0671: Unknown result type (might be due to invalid IL or missing references)
		//IL_067b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0680: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0708: Unknown result type (might be due to invalid IL or missing references)
		//IL_070d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_0725: Unknown result type (might be due to invalid IL or missing references)
		//IL_073e: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(SpinSoundSlot, out ActiveSound SpinSound) && SpinSound.IsPlaying)
		{
			SpinSound.Position = base.Projectile.Center;
		}
		Vector2.Distance(Owner.Center, base.Projectile.Center);
		Time++;
		base.Projectile.spriteDirection = base.Projectile.direction;
		Vector3 Light = default(Vector3);
		((Vector3)(ref Light))._002Ector(0.07f, 0.07f, 0.25f);
		Lighting.AddLight(base.Projectile.Center, Light * 3f);
		if (ChargeProgress < 1f)
		{
			Owner.ChangeDir(MathF.Sign(Main.MouseWorld.X - Owner.Center.X));
			float armRotation = ArmAnticipationMovement() * (float)Owner.direction;
			Owner.heldProj = base.Projectile.whoAmI;
			base.Projectile.spriteDirection = Owner.direction;
			base.Projectile.direction = Owner.direction;
			base.Projectile.Center = Owner.MountedCenter + Vector2.UnitY.RotatedBy(armRotation * Owner.gravDir) * -55f * Owner.gravDir;
			base.Projectile.rotation = (-(float)Math.PI / 4f * (float)base.Projectile.direction + armRotation) * Owner.gravDir;
			Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, (float)Math.PI + armRotation);
			return;
		}
		if (base.Projectile.timeLeft == Lifetime)
		{
			base.Projectile.netUpdate = true;
			SoundEngine.PlaySound(in SoundID.Item1, base.Projectile.Center);
			base.Projectile.Center = Owner.MountedCenter + (Main.MouseWorld - Owner.Center).SafeNormalize(Vector2.UnitX * (float)Owner.direction) * 12f;
			base.Projectile.velocity = (Main.MouseWorld - Owner.Center).SafeNormalize(Vector2.UnitX * (float)Owner.direction) * 20f;
			startDamage = base.Projectile.damage;
			base.Projectile.spriteDirection = base.Projectile.direction;
			SpinSoundSlot = SoundEngine.PlaySound(in AbyssBlade.SpinSound, base.Projectile.Center);
			Time = 0;
		}
		if (base.Projectile.velocity.X > 0f)
		{
			base.Projectile.direction = 1;
		}
		else
		{
			base.Projectile.direction = -1;
		}
		if (spinMode)
		{
			base.Projectile.rotation += 0.9f * (MathF.Abs(base.Projectile.velocity.Y) * 0.03f + 0.85f) * (float)base.Projectile.direction;
			base.Projectile.spriteDirection = base.Projectile.direction;
			if (base.Projectile.velocity.Y < 25f)
			{
				base.Projectile.velocity.Y += 0.42f;
			}
			if (base.Projectile.velocity.Y > 0f)
			{
				base.Projectile.velocity.X *= 0.975f;
			}
			for (int i = 0; i < 2; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + ((float)i * (float)Math.PI + base.Projectile.rotation + (float)Math.PI / 2f).ToRotationVector2() * 40f, Main.rand.NextBool(3) ? dustType1 : dustType2, ((float)i * (float)Math.PI + base.Projectile.rotation * (float)Math.Sign(base.Projectile.velocity.X)).ToRotationVector2() * 3f);
				dust.noGravity = true;
				dust.scale = 1.8f;
			}
			if (Collision.SolidCollision(base.Projectile.Center, 10, 10) && Time >= 2)
			{
				base.Projectile.extraUpdates = 2;
				base.Projectile.rotation = 0f;
				spinMode = false;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/CeramicImpact", 2);
				style.Volume = 0.65f;
				style.PitchVariance = 0.3f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int j = 0; j < 3; j++)
				{
					GeneralParticleHandler.SpawnParticle(new GenericSparkle(base.Projectile.Center, Vector2.Zero, Color.DodgerBlue, Color.MediumBlue, Main.rand.NextFloat(2.5f, 2.9f) - (float)j * 0.55f, 14, Main.rand.NextFloat(-0.01f, 0.01f), 2.5f));
				}
				base.Projectile.ResetLocalNPCHitImmunity();
				base.Projectile.penetrate = 1;
				base.Projectile.damage = startDamage * 2;
				Time = 0;
				bool foundTarget = false;
				NPC target = Owner.ClampedMouseWorld().ClosestNPCAt(1000f);
				if (target != null)
				{
					NPCDestination = target.Center + target.velocity * 5f;
					foundTarget = true;
				}
				else
				{
					foundTarget = false;
				}
				if (!foundTarget)
				{
					base.Projectile.velocity = (Owner.Calamity().mouseWorld - base.Projectile.Center).SafeNormalize(Vector2.UnitX * (float)base.Projectile.direction) * 25f;
				}
				else
				{
					base.Projectile.velocity = (NPCDestination - base.Projectile.Center).SafeNormalize(Vector2.UnitX * (float)base.Projectile.direction) * 25f;
				}
				for (int k = 0; k < 6; k++)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, (base.Projectile.velocity.SafeNormalize(Vector2.UnitX * (float)base.Projectile.direction) * 5f).RotatedByRandom(0.75) * Main.rand.NextFloat(1.4f, 2.2f), ModContent.ProjectileType<AbyssBladeSplitProjectile>(), (int)((double)startDamage * 0.3), base.Projectile.knockBack / 4f, base.Projectile.owner);
				}
			}
			return;
		}
		SpinSound?.Stop();
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f * (float)((base.Projectile.direction == 1) ? 1 : 3);
		if (Time > 9)
		{
			for (int l = 0; l < 6; l++)
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center - base.Projectile.velocity * 3f + Main.rand.NextVector2Circular(15f, 15f), Main.rand.NextBool(3) ? dustType2 : dustType1);
				dust2.noGravity = true;
				dust2.scale = Main.rand.NextFloat(1.3f, 1.6f);
				dust2.velocity = -base.Projectile.velocity * Main.rand.NextFloat(0.05f, 0.7f);
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 180);
		SoundStyle soundStyle = new SoundStyle("CalamityMod/Sounds/Custom/AbyssGravelMine2");
		soundStyle.Volume = 0.7f;
		soundStyle.PitchVariance = 0.3f;
		SoundStyle HitSound = soundStyle;
		if (!spinMode)
		{
			SoundEngine.PlaySound(in HitSound, base.Projectile.Center);
			for (int i = 0; i < 30; i++)
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? dustType1 : dustType2);
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(1.1f, 1.8f);
				dust.velocity = Utils.RotatedByRandom(new Vector2(3f, 3f), 100.0) * Main.rand.NextFloat(0.1f, 1.7f);
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(SpinSoundSlot, out ActiveSound SpinSound))
		{
			SpinSound?.Stop();
		}
		for (int i = 0; i < 40; i++)
		{
			float dustMulti = Main.rand.NextFloat(0.3f, 1.5f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, Main.rand.NextBool(3) ? dustType1 : dustType2);
			dust.noGravity = true;
			dust.scale = Main.rand.NextFloat(1.6f, 2.5f) - dustMulti;
			dust.velocity = Utils.RotatedByRandom(new Vector2(5f, 5f), 100.0) * Main.rand.NextFloat(0.3f, 1f) * dustMulti;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> p = ModContent.Request<Texture2D>("CalamityMod/Particles/CircularSmearSmokey", (AssetRequestMode)2);
		Asset<Texture2D> p2 = ModContent.Request<Texture2D>("CalamityMod/Particles/SemiCircularSmearSwipe", (AssetRequestMode)2);
		Vector2 generalDrawPos = base.Projectile.Center - Main.screenPosition;
		if (spinMode && Time < 9000)
		{
			Texture2D value = p2.Value;
			Color val = Color.Blue;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value, generalDrawPos, null, val * 0.55f, base.Projectile.rotation * Main.rand.NextFloat(1.6f, 1.7f), p2.Size() * 0.5f, 1.1f * Main.rand.NextFloat(0.8f, 1.15f), (SpriteEffects)0);
			Texture2D value2 = p.Value;
			val = Color.DodgerBlue;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(value2, generalDrawPos, null, val * 0.75f, base.Projectile.rotation * Main.rand.NextFloat(1.2f, 1.3f), p.Size() * 0.5f, 0.85f, (SpriteEffects)0);
		}
		return true;
	}

	public AbyssBladeProjectile()
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		Time = 9000;
		ChargeupTime = 25;
		Lifetime = 300;
		dustType1 = 104;
		dustType2 = 29;
		spinMode = true;
		NPCDestination = new Vector2(0f, 0f);
		pullback = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0f, 0f, -0.9424779f, 2);
		throwout = new CalamityUtils.CurveSegment(CalamityUtils.EasingType.PolyOut, 0.7f, -0.9424779f, (float)Math.PI * 4f / 5f, 3);
		base._002Ector();
	}
}
