using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class PressurizedBubbleStream : ModProjectile, ILocalizedModType, IModType
{
	public static int FireTime = 60;

	public static int ChargeupTime = 12;

	public Particle chargeBubble;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public float Progress => 1f - (float)base.Projectile.timeLeft / (float)FireTime;

	public bool ChargingUp => base.Projectile.timeLeft > FireTime;

	public float ChargeupProgress => (float)(ChargeupTime - (base.Projectile.timeLeft - FireTime)) / (float)ChargeupTime;

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float PulseTimer => ref base.Projectile.localAI[0];

	public ref float PrevPulseTimer => ref base.Projectile.localAI[1];

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 3;
		base.Projectile.extraUpdates = 4;
		base.Projectile.timeLeft = FireTime + ChargeupTime;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override bool ShouldUpdatePosition()
	{
		return !ChargingUp;
	}

	public override void AI()
	{
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_0567: Unknown result type (might be due to invalid IL or missing references)
		//IL_056c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		ChargeupTime = 60;
		if (ChargingUp)
		{
			float bubbleRotation = Owner.itemRotation - (float)Math.PI / 12f * (float)Owner.direction * Owner.gravDir;
			if (Owner.direction < 0)
			{
				bubbleRotation += (float)Math.PI;
			}
			Vector2 bubblePosition = ReedBlowgun.getPlayerMouth(Owner) + (bubbleRotation + (float)Math.PI / 60f * (float)Owner.direction * Owner.gravDir).ToRotationVector2() * 48f;
			base.Projectile.Center = bubblePosition;
			base.Projectile.velocity = Vector2.UnitX.RotatedBy(bubbleRotation) * ((Vector2)(ref base.Projectile.velocity)).Length();
			if (chargeBubble == null)
			{
				chargeBubble = new GenericBubbleParticle(bubblePosition, Vector2.Zero, 0.4f, Owner.itemRotation, ChargeupTime / base.Projectile.extraUpdates);
				GeneralParticleHandler.SpawnParticle(chargeBubble);
			}
			else
			{
				chargeBubble.Position = bubblePosition;
				chargeBubble.Rotation = Owner.itemRotation;
				chargeBubble.Scale = MathHelper.Lerp(0.4f, 1.4f, (float)Math.Sqrt(ChargeupProgress));
			}
			return;
		}
		if (base.Projectile.timeLeft == FireTime)
		{
			float bubbleRotation2 = Owner.itemRotation - (float)Math.PI / 12f * (float)Owner.direction;
			if (Owner.direction < 0)
			{
				bubbleRotation2 += (float)Math.PI;
			}
			Vector2 bubblePosition2 = Owner.MountedCenter - Vector2.UnitY * 4f + bubbleRotation2.ToRotationVector2() * 55f;
			SoundEngine.PlaySound(in SoundID.Item64, bubblePosition2);
		}
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.985f;
		PulseTimer--;
		Lighting.AddLight(base.Projectile.Center, 0f, 0f, 0.5f);
		if (PulseTimer <= 0f && Progress < 0.7f)
		{
			if (PrevPulseTimer == 0f)
			{
				PrevPulseTimer = 2f;
			}
			else
			{
				PrevPulseTimer *= 2f;
			}
			PulseTimer = PrevPulseTimer * 2f;
			Color pulseColor = ((!Main.rand.NextBool()) ? (Main.rand.NextBool() ? Color.LightBlue : Color.DeepSkyBlue) : (Main.rand.NextBool() ? Color.SkyBlue : Color.LightSkyBlue));
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, pulseColor, new Vector2(0.5f, 1f), base.Projectile.velocity.ToRotation(), 0.05f, 0.2f + 0.3f * (1f - Progress), 30));
			int numDust = 18;
			for (int i = 0; i < numDust; i++)
			{
				Vector2 ringSpeed = Utils.RotatedBy(new Vector2((float)Math.Cos((float)i / (float)numDust * ((float)Math.PI * 2f)), (float)Math.Sin((float)i / (float)numDust * ((float)Math.PI * 2f)) * 0.5f), (double)(base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f), default(Vector2)) * (3.5f * (1f - Progress) + 3f);
				Dust.NewDustPerfect(base.Projectile.position, 211, ringSpeed, 100, default(Color), 1.25f).noGravity = true;
			}
		}
		if (!Main.rand.NextBool(3))
		{
			Gore gore = Gore.NewGorePerfect(base.Projectile.GetSource_FromAI(), base.Projectile.position, base.Projectile.velocity * 0.2f + Main.rand.NextVector2Circular(1f, 1f) * Progress, 411);
			gore.timeLeft = 8 + Main.rand.Next(6);
			gore.scale = Main.rand.NextFloat(0.6f, 1f) * (1f + Progress * 0.4f);
			gore.type = (Main.rand.NextBool(3) ? 412 : 411);
		}
		for (int j = 0; j < 6; j++)
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 33, 0f, 0f, 100, default(Color), 0.9f);
			dust.noGravity = true;
			dust.velocity *= 0.5f;
			dust.velocity -= base.Projectile.velocity * 0.1f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<RiptideDebuff>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<RiptideDebuff>(), 120);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item21, base.Projectile.position);
		int bubbleCount = 25 + Main.rand.Next(15);
		for (int i = 0; i <= bubbleCount; i++)
		{
			Gore gore = Gore.NewGorePerfect(base.Projectile.GetSource_Death(), base.Projectile.position, base.Projectile.velocity * 0.3f + Main.rand.NextVector2Circular(4f, 4f), 411);
			gore.timeLeft = 8 + Main.rand.Next(6);
			gore.scale = Main.rand.NextFloat(0.6f, 1f) * (1f + Progress * 0.4f);
			gore.type = (Main.rand.NextBool(3) ? 412 : 411);
		}
	}
}
