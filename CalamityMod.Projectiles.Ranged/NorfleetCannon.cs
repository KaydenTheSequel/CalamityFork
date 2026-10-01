using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class NorfleetCannon : ModProjectile
{
	public Color StaticEffectsColor;

	private float PostFireCooldown;

	private bool HasLetGo;

	private SlotId NorfleetRecharge;

	private Player Owner;

	private float MaxOffsetLength;

	private const float MaxCharge = 237f;

	public bool recharging;

	public Color variedColor;

	public Color mainColor;

	public Color randomColor;

	public int colorTimer;

	public bool hasFired;

	public bool PUNISHMENTMODE;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Norfleet>();

	public override string Texture => "CalamityMod/Items/Weapons/Ranged/Norfleet";

	private ref float ShootingTimer => ref base.Projectile.ai[0];

	private ref float OffsetLength => ref base.Projectile.localAI[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 142);
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.netImportant = true;
	}

	public override void AI()
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		Item heldItem = Owner.HeldItem;
		if (Owner.dead || Owner == null)
		{
			base.Projectile.Kill();
		}
		randomColor = (Color)(Main.rand.Next(3) switch
		{
			0 => Color.OrangeRed, 
			1 => Color.Aqua, 
			_ => Color.GreenYellow, 
		});
		if (mainColor == Color.White)
		{
			mainColor = randomColor;
			if (base.Projectile.ai[1] == 1000f)
			{
				PUNISHMENTMODE = true;
			}
		}
		if (ShootingTimer % 15f == 0f)
		{
			variedColor = (Color)(colorTimer switch
			{
				0 => Color.OrangeRed, 
				1 => Color.Aqua, 
				_ => Color.GreenYellow, 
			});
			colorTimer++;
			if (colorTimer >= 3)
			{
				colorTimer = 0;
			}
		}
		mainColor = Color.Lerp(mainColor, variedColor, (PostFireCooldown <= 20f || recharging) ? 0.07f : 0f);
		if (Owner.CantUseHoldout() && !HasLetGo)
		{
			if (SoundEngine.TryGetActiveSound(NorfleetRecharge, out ActiveSound hum) && hum.IsPlaying)
			{
				hum?.Stop();
			}
			if (Owner.Calamity().NorfleetCounter < 2)
			{
				ShootRocket(heldItem);
				PostFireCooldown = (PUNISHMENTMODE ? 1000 : 55);
				ShootingTimer = 1f;
				mainColor = (PUNISHMENTMODE ? Color.Red : Color.MediumOrchid);
			}
			else
			{
				ShootRocket(heldItem);
				PostFireCooldown = (PUNISHMENTMODE ? 1000 : 262);
				ShootingTimer = 1f;
				mainColor = (PUNISHMENTMODE ? Color.Red : Color.MediumOrchid);
			}
			NetUpdate();
			HasLetGo = true;
		}
		if (HasLetGo)
		{
			PostFiringCooldown();
		}
		Vector2 ownerPosition = Owner.MountedCenter;
		Vector2 ownerToMouse = Owner.Calamity().mouseWorld - ownerPosition;
		ManageHoldout(ownerPosition, ownerToMouse);
		if (OffsetLength != MaxOffsetLength)
		{
			OffsetLength = MathHelper.Lerp(OffsetLength, MaxOffsetLength, 0.1f);
		}
		ShootingTimer++;
		if (!Main.dedServ)
		{
			_ = base.Projectile.velocity.SafeNormalize(Vector2.Zero) * 20f;
		}
	}

	private void ManageHoldout(Vector2 mountedCenter, Vector2 ownerToMouse)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = base.Projectile.rotation.ToRotationVector2();
		float velocityRotation = base.Projectile.velocity.ToRotation();
		float proximityLookingUpwards = Vector2.Dot(ownerToMouse.SafeNormalize(Vector2.Zero), -Vector2.UnitY);
		int direction = MathF.Sign(ownerToMouse.X);
		Vector2 armPosition = Owner.RotatedRelativePoint(mountedCenter, reverseRotation: true);
		Vector2 lengthOffset = val * OffsetLength;
		Vector2 armOffset = default(Vector2);
		((Vector2)(ref armOffset))._002Ector(Utils.Remap(proximityLookingUpwards, -1f, 1f, 0f, -12f) * (float)direction, -10f + Utils.Remap(MathF.Abs(proximityLookingUpwards), 0f, 1f, 0f, (proximityLookingUpwards > 0f) ? 15f : 0f));
		base.Projectile.Center = armPosition + lengthOffset + armOffset;
		base.Projectile.velocity = velocityRotation.AngleTowards(ownerToMouse.ToRotation(), 0.2f).ToRotationVector2();
		base.Projectile.rotation = velocityRotation;
		base.Projectile.timeLeft = 2;
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = (Owner.itemAnimation = 2);
		Owner.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
		base.Projectile.spriteDirection = (base.Projectile.direction = direction);
		Owner.ChangeDir(direction);
		float armRotation = base.Projectile.rotation - (float)Math.PI / 2f;
		Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Quarter, armRotation);
		Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, armRotation + MathHelper.ToRadians(15f) * (float)direction);
		float rumble = 1f;
		if (!Owner.CantUseHoldout() && PostFireCooldown <= 0f)
		{
			Projectile projectile = base.Projectile;
			projectile.Center += Main.rand.NextVector2Circular(rumble, rumble);
		}
		if (PostFireCooldown < 297f && PostFireCooldown > 15f && recharging)
		{
			rumble = 5f * Utils.GetLerpValue(297f, 0f, PostFireCooldown, clamped: true);
			Projectile projectile2 = base.Projectile;
			projectile2.Center += Main.rand.NextVector2Circular(rumble, rumble);
		}
	}

	private void ShootRocket(Item item)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		if (!hasFired)
		{
			Vector2 shootDirection = base.Projectile.velocity.SafeNormalize(Vector2.Zero);
			Vector2 tipPosition = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(-0.05f * (float)base.Projectile.direction) * 73f;
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/NorfleetFire");
			style.Volume = 0.9f;
			style.PitchVariance = 0.25f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			Owner.Calamity().NorfleetCounter++;
			for (int i = 0; i < 3; i++)
			{
				Vector2 firingVelocity = shootDirection.RotatedByRandom(0.1f * (float)i + 0.02f) * 5f * Main.rand.NextFloat(0.8f, 1.2f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), tipPosition, firingVelocity, ModContent.ProjectileType<NorfleetComet>(), base.Projectile.damage / 3, base.Projectile.knockBack, base.Projectile.owner, 0f, i, PUNISHMENTMODE ? 1 : 0);
			}
			NetUpdate();
			if (Main.dedServ)
			{
				return;
			}
			for (int k = 0; k < 30; k++)
			{
				Vector2 shootVel = (shootDirection * 10f).RotatedByRandom(0.5) * Main.rand.NextFloat(0.1f, 1.8f);
				Dust dust2 = Dust.NewDustPerfect(tipPosition, Main.rand.NextBool(4) ? 264 : 66, shootVel);
				dust2.scale = Main.rand.NextFloat(1.15f, 1.45f);
				dust2.noGravity = true;
				if (dust2.type == 66)
				{
					dust2.color = (Main.rand.NextBool() ? Color.DarkViolet : Color.MediumOrchid);
				}
				else
				{
					dust2.color = Color.White;
				}
			}
			for (int j = 0; j < 15; j++)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(tipPosition - shootDirection * 14f + Main.rand.NextVector2Circular(12f, 12f), shootDirection * 17f * Main.rand.NextFloat(0.35f, 1.35f), affectedByGravity: false, Main.rand.Next(7, 12), 0.025f * Main.rand.NextFloat(0.45f, 1.25f), Color.MediumOrchid, new Vector2(1.5f, 0.9f), quickShrink: true));
			}
			OffsetLength -= 34f;
			hasFired = true;
		}
		else
		{
			if (Owner.Calamity().NorfleetCounter >= 1000)
			{
				Owner.Calamity().NorfleetCounter = 4;
			}
			else
			{
				Owner.Calamity().NorfleetCounter = 1000;
			}
			base.Projectile.Kill();
		}
	}

	private void PostFiringCooldown()
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0421: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		Owner.channel = true;
		Vector2 tipPosition = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(-0.05f * (float)base.Projectile.direction) * 73f;
		if (PUNISHMENTMODE)
		{
			PostFireCooldown = 1000f;
			Owner.AddBuff(ModContent.BuffType<MiracleBlight>(), 900);
			Owner.AddBuff(ModContent.BuffType<VulnerabilityHex>(), 900);
			if (Owner.statLife > 1)
			{
				Owner.statLife = (int)((float)Owner.statLife * 0.99f);
			}
		}
		if (PostFireCooldown == 207f)
		{
			OffsetLength -= 24f;
			SoundStyle charge = new SoundStyle("CalamityMod/Sounds/Item/NorfleetRecharge");
			SoundStyle style = charge with
			{
				Volume = 1f
			};
			NorfleetRecharge = SoundEngine.PlaySound(in style, base.Projectile.Center);
			recharging = true;
		}
		if (PostFireCooldown > 0f && !recharging)
		{
			Vector2 smokeVel = new Vector2(0f, -8f) * Main.rand.NextFloat(0.1f, 1.1f);
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(tipPosition, smokeVel, PUNISHMENTMODE ? Color.Red : StaticEffectsColor, Main.rand.Next(40, 61), Main.rand.NextFloat(0.3f, 0.6f), 0.5f, Main.rand.NextFloat(-0.2f, 0.2f), Main.rand.NextBool(), 0f, required: true));
			Dust dust = Dust.NewDustPerfect(tipPosition, 303, smokeVel.RotatedByRandom(0.10000000149011612), 80, default(Color), Main.rand.NextFloat(0.4f, 1.3f));
			dust.noGravity = false;
			dust.color = (PUNISHMENTMODE ? Color.Red : Color.White);
		}
		else if (!recharging)
		{
			if (SoundEngine.TryGetActiveSound(NorfleetRecharge, out ActiveSound hum) && hum.IsPlaying)
			{
				hum?.Stop();
			}
			base.Projectile.Kill();
			NetUpdate();
		}
		else
		{
			if (PostFireCooldown == 297f)
			{
				OffsetLength -= 18f;
			}
			if (PostFireCooldown == 15f)
			{
				OffsetLength -= 11f;
				int points = 3;
				float radians = (float)Math.PI * 2f / (float)points;
				float randRot = Main.rand.NextFloat(-5f, 5f);
				Vector2 spinningPoint = Vector2.Normalize(Utils.RotatedBy(new Vector2(-1f, -1f), (double)randRot, default(Vector2)));
				for (int k = 0; k < points; k++)
				{
					Color glowColor = (Color)(k switch
					{
						1 => Color.Aqua, 
						0 => Color.OrangeRed, 
						_ => Color.GreenYellow, 
					});
					Vector2 velocity = spinningPoint.RotatedBy(radians * (float)k).RotatedBy(-0.44999998807907104);
					GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(tipPosition, velocity, affectedByGravity: false, 10, 0.065f, glowColor, new Vector2(1.3f, 0.2f), quickShrink: true));
				}
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DudFire");
				style.Volume = 1f;
				style.Pitch = 0.7f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				Owner.Calamity().NorfleetCounter = 0;
			}
			if (PostFireCooldown > 15f)
			{
				Vector2 dustVel = base.Projectile.velocity.RotatedByRandom(100.0) * Main.rand.NextFloat(5.1f, 25.8f);
				Dust dust2 = Dust.NewDustPerfect(tipPosition + dustVel * 5f, 267, -dustVel * 0.5f, 0, default(Color), Main.rand.NextFloat(0.5f, 1f));
				dust2.noGravity = true;
				dust2.color = mainColor;
			}
			if (PostFireCooldown <= 0f)
			{
				recharging = false;
			}
		}
		PostFireCooldown--;
		if (SoundEngine.TryGetActiveSound(NorfleetRecharge, out ActiveSound hum2) && hum2.IsPlaying)
		{
			hum2.Position = base.Projectile.Center;
		}
	}

	private void NetUpdate()
	{
		base.Projectile.ForceNetUpdate(ignoreCurrentNetSpam: false);
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		Owner = Main.player[base.Projectile.owner];
		OffsetLength = MaxOffsetLength;
		if (Main.zenithWorld && Main.rand.NextBool(1000))
		{
			PUNISHMENTMODE = true;
			SoundEngine.PlaySound(new SoundStyle("CalamityMod/Sounds/Item/NuhUhUh"), Owner.Center);
		}
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		if (ShootingTimer <= 0f)
		{
			return false;
		}
		Vector2 tipPosition = base.Projectile.Center + base.Projectile.velocity.SafeNormalize(Vector2.Zero).RotatedBy(-0.05f * (float)base.Projectile.direction) * 73f;
		Texture2D glowTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/NorfleetGhostGlowmask", (AssetRequestMode)2).Value;
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Ranged/Norfleet", (AssetRequestMode)2).Value;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = texture.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		if (recharging)
		{
			for (int i = 0; i < 3; i++)
			{
				Color auraColor = mainColor * 0.25f;
				Texture2D centerTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/NorfleetGhost", (AssetRequestMode)2).Value;
				Vector2 rotationalDrawOffset = ((float)Math.PI * 2f * (float)i / 7f + Main.GlobalTimeWrappedHourly * 50f).ToRotationVector2();
				rotationalDrawOffset *= MathHelper.Lerp(3f, 5.25f, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 4f) * 0.5f + 0.5f);
				Main.EntitySpriteDraw(centerTexture, base.Projectile.Center - Main.screenPosition + rotationalDrawOffset, null, auraColor, drawRotation, centerTexture.Size() * 0.5f, base.Projectile.scale * MathHelper.Clamp(1.35f * Utils.GetLerpValue(-200f, 297f, PostFireCooldown, clamped: true), 1f, 1.35f), flipSprite);
			}
		}
		Texture2D rechargeTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(texture, drawPosition, null, drawColor, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		Main.EntitySpriteDraw(glowTexture, drawPosition, null, PUNISHMENTMODE ? Color.Red : mainColor, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		if (PostFireCooldown > 15f && recharging)
		{
			float randSize = Main.rand.NextFloat(0.8f, 1.2f);
			Vector2 position = tipPosition - Main.screenPosition;
			Color white = mainColor;
			((Color)(ref white)).A = 0;
			Main.EntitySpriteDraw(rechargeTexture, position, null, white, base.Projectile.rotation, rechargeTexture.Size() * 0.5f, 0.55f * Utils.GetLerpValue(-100f, 297f, PostFireCooldown, clamped: true) * randSize, (SpriteEffects)0);
			Vector2 position2 = tipPosition - Main.screenPosition;
			white = Color.White;
			((Color)(ref white)).A = 0;
			Main.EntitySpriteDraw(rechargeTexture, position2, null, white, base.Projectile.rotation, rechargeTexture.Size() * 0.5f, 0.35f * Utils.GetLerpValue(-100f, 297f, PostFireCooldown, clamped: true) * randSize, (SpriteEffects)0);
		}
		return false;
	}

	public NorfleetCannon()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		StaticEffectsColor = Color.Gray;
		MaxOffsetLength = 5f;
		variedColor = Color.White;
		mainColor = Color.White;
		randomColor = Color.White;
		base._002Ector();
	}
}
