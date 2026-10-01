using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Spears;

public class GildedProboscisProj : BaseSwordHoldoutProjectile, ILocalizedModType, IModType
{
	private bool isChannelable;

	private int channelCharge;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override bool useMeleeSpeed => false;

	public override bool useMeleeSize => false;

	public override int swingWidth => 360;

	public override Item BaseItem => ModContent.GetModItem(ModContent.ItemType<GildedProboscis>()).Item;

	public override int AfterImageLength => 0;

	public override int StartupTime { get; set; }

	public override int CooldownTime { get; set; }

	public override float lineCollisionLength => 196f;

	public override void Defaults()
	{
		base.Projectile.width = (base.Projectile.height = 138);
		base.Projectile.extraUpdates = 5;
		base.Projectile.noEnchantmentVisuals = true;
	}

	public override void Spawn()
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		BaseSwordHoldoutPlayer modPlayer = player.GetModPlayer<BaseSwordHoldoutPlayer>();
		StartupTime = 20;
		CooldownTime = 41;
		swingTime -= StartupTime + CooldownTime;
		modPlayer.swingNum = 0;
		OffsetDistance = 70;
		base.angle = new Vector2((float)((base.angle.X > 0f) ? 1 : (-1)), 0f);
		if (player.dashDelay == -1)
		{
			base.angle = new Vector2((float)(-MathF.Sign(player.velocity.X)), 0f);
		}
		RotateInStartup = 0f;
		RotateInCooldown = 0f;
		UseSound = SoundID.DD2_BetsysWrathImpact;
		if (player.altFunctionUse == 0)
		{
			UseSound = SoundID.DD2_JavelinThrowersAttack;
			isChannelable = true;
			RotateInStartup = 0.5f;
			OffsetDistance = 10;
		}
	}

	public override void AdditionalAI()
	{
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_0807: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0310: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0828: Unknown result type (might be due to invalid IL or missing references)
		//IL_0832: Unknown result type (might be due to invalid IL or missing references)
		//IL_0854: Unknown result type (might be due to invalid IL or missing references)
		//IL_085a: Unknown result type (might be due to invalid IL or missing references)
		//IL_085c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0875: Unknown result type (might be due to invalid IL or missing references)
		//IL_0891: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_090d: Unknown result type (might be due to invalid IL or missing references)
		//IL_091d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0922: Unknown result type (might be due to invalid IL or missing references)
		//IL_0927: Unknown result type (might be due to invalid IL or missing references)
		//IL_0987: Unknown result type (might be due to invalid IL or missing references)
		//IL_098c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0993: Unknown result type (might be due to invalid IL or missing references)
		//IL_0998: Unknown result type (might be due to invalid IL or missing references)
		//IL_099f: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_0482: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_038a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0941: Unknown result type (might be due to invalid IL or missing references)
		//IL_093a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_0950: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a39: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a70: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (isChannelable)
		{
			if (player.channel)
			{
				if (base.timer >= StartupTime - 1)
				{
					base.timer--;
					base.Projectile.timeLeft++;
				}
				if (base.Projectile.FinalExtraUpdate() && channelCharge < CalamityUtils.SecondsToFrames(5))
				{
					channelCharge++;
				}
			}
			else if (base.timer == StartupTime - 1)
			{
				base.Projectile.damage = (int)((double)base.Projectile.damage * 0.75 * (double)((float)channelCharge / 75f));
			}
			base.Projectile.rotation -= (float)Math.PI / 2f * (float)((base.angle.X > 0f) ? 1 : (-1));
			if (base.inStartup)
			{
				OffsetDistance = (int)MathHelper.SmoothStep(40f, 5f, (float)channelCharge / 300f);
			}
			if (base.inCooldown)
			{
				OffsetDistance = (int)MathHelper.SmoothStep(90f, 60f, MathF.Pow(base.CooldownCompletion, 1f));
				if (base.CooldownCompletion > 0.5f && Main.LocalPlayer.whoAmI == base.Projectile.owner && player.HeldItem.type == ModContent.ItemType<GildedProboscis>() && Main.mouseRight)
				{
					player.itemAnimation = 1;
					player.itemTime = 1;
					base.Projectile.timeLeft = 1;
					base.timer = StartupTime + swingTime + CooldownTime;
					return;
				}
			}
			if (!base.inSwing)
			{
				return;
			}
			OffsetDistance = (int)MathHelper.Lerp(-40f, 90f, MathF.Pow(base.SwingCompletion, 1.7f));
			Vector2 veloc = base.oldPlayerOffset - (base.Projectile.Center - Main.player[base.Projectile.owner].Center);
			((Vector2)(ref veloc)).Normalize();
			if (swingTimer % 4 == 0)
			{
				for (int i2 = -1; i2 <= 1; i2 += 2)
				{
					int sparkLifetime = Main.rand.Next(15, 23);
					Vector2 spinningpoint = Vector2.UnitY * -9f;
					float maxRotationDeviance = 0.4f;
					float rotationAngle = Main.rand.NextFloat(0f - maxRotationDeviance, maxRotationDeviance);
					_ = spinningpoint.RotatedBy(rotationAngle) * Main.rand.NextFloat(0.3f, 1f);
					float sparkScale = Main.rand.NextFloat(0.007f, 0.015f);
					Vector2 compensatedSparkVel = base.angle.RotatedBy((float)i2 * -0.15f) * Main.rand.NextFloat(5f, 10f);
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(player.Center + base.angle.RotatedBy(3.1415927410125732) * ((float)OffsetDistance + Main.rand.NextFloat(30f, 100f)) * base.Projectile.scale + Utils.RotatedByRandom(new Vector2(0f, (float)(10 * i2)), 2.9415926933288574), compensatedSparkVel, affectedByGravity: false, sparkLifetime, sparkScale, Main.rand.NextBool() ? Color.Red : Color.Gold, new Vector2(0.5f, 1.3f)));
				}
			}
			Vector2 sparkAngle = base.angle.RotatedBy(3.1415927410125732);
			MathF.Sign(sparkAngle.X);
			Color color = Color.Lerp(Color.Gold, Color.Crimson, base.SwingCompletion);
			if (swingTimer % 2 == 0 || swingTimer == swingTime - 1)
			{
				for (int j = -1; j <= 1; j += 2)
				{
					Vector2 velocity = -sparkAngle.RotatedBy((float)j * -0.3f) * 10f;
					GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center + Utils.RotatedBy(new Vector2(75f, (float)(j * 22)), (double)sparkAngle.ToRotation(), default(Vector2)), velocity, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 10, 0.3f, color, new Vector2(0.3f, 3f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.2f));
				}
			}
			return;
		}
		int dashStrength = 12;
		if ((player.direction == 1 && player.controlLeft) || (player.direction == -1 && player.controlRight))
		{
			dashStrength -= 8;
		}
		if ((player.direction == 1 && player.controlRight) || (player.direction == -1 && player.controlLeft))
		{
			dashStrength += 8;
		}
		if (base.inStartup)
		{
			base.Projectile.scale = baseScale * MathHelper.Lerp(0.9f, 0.65f, MathF.Pow(base.StartupCompletion, 2f));
			OffsetDistance = (int)MathHelper.Lerp(60f, 40f, MathF.Pow(base.StartupCompletion, 2f));
			ref Vector2 vel = ref Main.player[base.Projectile.owner].velocity;
			if (base.StartupCompletion > 0.5f && (0f - vel.X) * base.angle.X < (float)dashStrength)
			{
				vel.X -= base.angle.X;
			}
		}
		else if (base.inCooldown)
		{
			base.Projectile.scale = baseScale * MathHelper.Lerp(1f, 0.9f, 1f - MathF.Pow(1f - base.CooldownCompletion, 2f));
			OffsetDistance = (int)MathHelper.Lerp(70f, 60f, 1f - MathF.Pow(1f - base.CooldownCompletion, 2f));
			ref Vector2 vel2 = ref Main.player[base.Projectile.owner].velocity;
			if (base.CooldownCompletion < 0.25f && (0f - vel2.X) * base.angle.X > 5f)
			{
				vel2.X *= 0.9f;
			}
		}
		else
		{
			float am = MathHelper.Clamp(MathF.Pow((base.SwingCompletion - 0.16666666f) * 3f, 3f), 0f, 1f);
			if (float.IsNaN(am))
			{
				am = 0f;
			}
			base.Projectile.scale = baseScale * MathHelper.Lerp(0.65f, 1f, am);
			am = MathHelper.Clamp(MathF.Pow((base.SwingCompletion - -0.03333333f) * 3f, 3f), 0f, 1f);
			if (float.IsNaN(am))
			{
				am = 0f;
			}
			OffsetDistance = (int)MathHelper.Lerp(40f, 70f, am);
			ref Vector2 vel3 = ref Main.player[base.Projectile.owner].velocity;
			if ((0f - vel3.X) * base.angle.X < (float)dashStrength)
			{
				vel3.X -= base.angle.X;
			}
			Vector2 veloc2 = base.oldPlayerOffset - (base.Projectile.Center - Main.player[base.Projectile.owner].Center);
			((Vector2)(ref veloc2)).Normalize();
			for (int k = 0; k < 2; k++)
			{
				int sparkLifetime2 = Main.rand.Next(15, 23);
				Vector2 spinningpoint2 = Vector2.UnitY * -9f;
				float maxRotationDeviance2 = 0.4f;
				float rotationAngle2 = Main.rand.NextFloat(0f - maxRotationDeviance2, maxRotationDeviance2);
				_ = spinningpoint2.RotatedBy(rotationAngle2) * Main.rand.NextFloat(0.3f, 1f);
				float sparkScale2 = Main.rand.NextFloat(0.007f, 0.015f);
				Vector2 compensatedSparkVel2 = veloc2 * Main.rand.NextFloat(2f, 5f);
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + Utils.RotatedBy(new Vector2(0f - base.angle.X, Main.rand.NextFloat(-0.05f, 0.05f)), (double)base.Projectile.rotation, default(Vector2)) * Main.rand.NextFloat(50f, 120f) * base.Projectile.scale, compensatedSparkVel2, affectedByGravity: false, sparkLifetime2, sparkScale2, Main.rand.NextBool() ? Color.Red : Color.Gold, new Vector2(0.5f, 1.3f)));
			}
			Color[] smokeColor = (Color[])(object)new Color[3]
			{
				Color.Red,
				Color.Goldenrod,
				Color.Crimson
			};
			Vector2 smokePosition = base.Projectile.Center + Utils.RotatedBy(new Vector2(0f - base.angle.X, Main.rand.NextFloat(-0.05f, 0.05f)), (double)base.Projectile.rotation, default(Vector2)) * 80f * base.Projectile.scale;
			for (int l = 0; l < 3; l++)
			{
				GeneralParticleHandler.SpawnParticle(new CustomSpark(smokePosition, smokePosition.DirectionTo(Main.player[base.Projectile.owner].Center), "CalamityMod/ExtraTextures/Trails/ScarletDevilStreak", affectedByGravity: false, 10, 0.6f * base.Projectile.scale, smokeColor[base.timer % 3], new Vector2(1f, 1f)));
			}
		}
		base.Projectile.rotation -= (float)Math.PI / 2f * base.angle.X;
	}

	public override float SwingFunction()
	{
		if (isChannelable)
		{
			if (base.inStartup && channelCharge >= CalamityUtils.SecondsToFrames(5))
			{
				return Main.rand.NextFloat(-0.025f, 0.025f);
			}
			return 0f;
		}
		if (base.inStartup)
		{
			return MathHelper.ToRadians(MathHelper.SmoothStep((float)swingWidth * 0.7f, (float)swingWidth * 0.33f, MathF.Pow(base.StartupCompletion, 4f)));
		}
		if (base.inCooldown)
		{
			return MathHelper.ToRadians(MathHelper.Lerp((float)(-swingWidth) * 0.2f, (float)(-swingWidth) * 0.3f, 1f - MathF.Pow(1f - base.CooldownCompletion, 3f)));
		}
		return MathHelper.ToRadians(MathHelper.SmoothStep((float)swingWidth * 0.33f, (float)(-swingWidth) * 0.2f, base.SwingCompletion));
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<VermillionFlux>(), 900);
		Main.player[base.Projectile.owner].SpawnLifeStealProjectile(target, base.Projectile, 305, (int)Math.Round((double)hit.Damage * 0.0015));
		if (base.Projectile.damage > 1)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.925f);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SetCrit();
		float critDamage = Main.player[base.Projectile.owner].GetTotalCritChance(base.Projectile.DamageType) * 0.02f;
		modifiers.CritDamage += critDamage;
	}
}
