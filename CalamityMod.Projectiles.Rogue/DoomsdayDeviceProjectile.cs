using System;
using System.Collections.Generic;
using CalamityMod.Dusts;
using CalamityMod.NPCs;
using CalamityMod.NPCs.Ravager;
using CalamityMod.Particles;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

[PierceResistException(false)]
public class DoomsdayDeviceProjectile : ModProjectile, ILocalizedModType, IModType
{
	public float rotSpeed;

	public int tileHits;

	public bool flung;

	public float charge;

	public bool hasReachedFullCharge;

	public bool hasStoppedHolding;

	public int stealthPenaltyTimer;

	public bool doneHitting;

	public bool giveStealth;

	public int maxStealthHits;

	public Color mainColor;

	public Color c1;

	public Color c2;

	public NPC lastHitTarget;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/DoomsdayDevice";

	public ref float time => ref base.Projectile.ai[0];

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 5;
		base.Projectile.height = 5;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.extraUpdates = 0;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 3000;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override bool ShouldUpdatePosition()
	{
		return flung;
	}

	public override bool? CanDamage()
	{
		if (!flung || tileHits != 0)
		{
			return false;
		}
		return null;
	}

	public override void AI()
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0914: Unknown result type (might be due to invalid IL or missing references)
		//IL_0924: Unknown result type (might be due to invalid IL or missing references)
		//IL_0929: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a2a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a34: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ac7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ad7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0adc: Unknown result type (might be due to invalid IL or missing references)
		//IL_057e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0585: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0591: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_062b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_0743: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0808: Unknown result type (might be due to invalid IL or missing references)
		//IL_081f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0825: Unknown result type (might be due to invalid IL or missing references)
		//IL_0841: Unknown result type (might be due to invalid IL or missing references)
		//IL_0854: Unknown result type (might be due to invalid IL or missing references)
		//IL_085a: Unknown result type (might be due to invalid IL or missing references)
		//IL_085c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0866: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_087d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0882: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c1: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.dead && !flung)
		{
			base.Projectile.Kill();
			return;
		}
		float rate = Main.GlobalTimeWrappedHourly * 6f;
		List<Color> eColors = new List<Color> { c1, c2 };
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		mainColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		if (tileHits == 0 && hasReachedFullCharge && !doneHitting)
		{
			float squash = Utils.GetLerpValue(1f, 3f, ((Vector2)(ref base.Projectile.velocity)).Length(), clamped: true);
			GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, base.Projectile.velocity * 0.01f, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 23, 0.4f, mainColor * 0.4f * squash, new Vector2(1f - 0.15f * squash, 1f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.2f * squash));
		}
		if (flung)
		{
			if (time / (float)base.Projectile.extraUpdates > (float)Owner.HeldItem.useAnimation * 0.45f)
			{
				base.Projectile.localAI[1] = 5f;
			}
			if (!doneHitting)
			{
				if (!Collision.SolidCollision(base.Projectile.Center, 5, 5) && tileHits == 0)
				{
					Projectile projectile = base.Projectile;
					projectile.Center += new Vector2(0f, Utils.Remap(time, 0f, 120f, -3f, 3f, clamped: false) * base.Projectile.ai[1]);
				}
				else
				{
					TileHit();
				}
				rotSpeed += 0.0002f;
			}
			else
			{
				if (rotSpeed != 0f)
				{
					rotSpeed *= 0.99f;
				}
				base.Projectile.velocity.Y += 0.05f;
				charge *= 0.98f;
				base.Projectile.Opacity = charge;
			}
			if (tileHits == 0)
			{
				base.Projectile.rotation += (float)base.Projectile.direction * rotSpeed;
				bool smokey = Main.rand.NextBool(3);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, -10f), (double)base.Projectile.rotation, default(Vector2)), (smokey && Main.rand.NextBool()) ? ModContent.DustType<VoidDust>() : ModContent.DustType<LightDust>(), base.Projectile.rotation.ToRotationVector2() * Main.rand.NextFloat(2f, 3f), 0, default(Color), Main.rand.NextFloat(0.4f, 0.75f));
				dust.noGravity = smokey;
				dust.color = (smokey ? Color.White : mainColor);
				dust.noLight = true;
				dust.alpha = 180;
				dust.scale *= (smokey ? 2.2f : 1f);
				dust.velocity += (smokey ? (Vector2.UnitY * -2f) : Vector2.Zero);
				dust.noLightEmittence = true;
			}
			else
			{
				base.Projectile.timeLeft = (int)((float)base.Projectile.timeLeft * 0.98f);
			}
		}
		else
		{
			base.Projectile.velocity = Owner.velocity;
			float completion = time / ((float)Owner.HeldItem.useAnimation * 0.7f);
			if (completion >= 1f)
			{
				time = -1f;
				base.Projectile.Center = Owner.Center;
				base.Projectile.extraUpdates = (hasReachedFullCharge ? 16 : 5);
				base.Projectile.rotation += Main.rand.NextFloat(-4f, 4f);
				Vector2 velocity = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld);
				float speedMult = (float)Math.Pow(Utils.GetLerpValue(1f, 4f, charge, clamped: true), 2.0) + 0.05f;
				float arcValue = (hasReachedFullCharge ? 0f : ((float)Math.Pow(Utils.Remap(speedMult, 0f, 1f, 1f, 0.8f), 4.0)));
				base.Projectile.ai[1] = arcValue;
				base.Projectile.velocity = velocity * 9f * speedMult;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/SwooshMid");
				style.Volume = 1f;
				style.Pitch = 0.2f;
				style.MaxInstances = 6;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				if (hasReachedFullCharge)
				{
					rotSpeed *= 2f;
					style = new SoundStyle("CalamityMod/Sounds/Item/SwooshMid");
					style.Volume = 1f;
					style.Pitch = -0.4f;
					style.MaxInstances = 6;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				else
				{
					Owner.Calamity().ConsumeStealthByAttacking();
				}
				if (Owner.Calamity().StealthStrikeAvailable() && hasReachedFullCharge)
				{
					base.Projectile.Calamity().stealthStrike = true;
					Owner.Calamity().ConsumeStealthByAttacking();
				}
				base.Projectile.tileCollide = true;
				flung = true;
			}
			else
			{
				if (Main.mouseLeft && !hasStoppedHolding)
				{
					if (completion >= 0.7f && completion <= 0.8f)
					{
						time--;
						if (charge < 4f)
						{
							charge += 0.1f;
						}
						else
						{
							if (stealthPenaltyTimer == 18)
							{
								SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MeldSlice");
								style.Volume = 0.3f;
								style.Pitch = 0.4f;
								SoundEngine.PlaySound(in style, base.Projectile.Center);
							}
							if (stealthPenaltyTimer >= 18)
							{
								Owner.Calamity().rogueStealth -= Owner.Calamity().rogueStealthMax * 0.035f;
								CheckStealth();
							}
							else
							{
								charge = 4f;
							}
							stealthPenaltyTimer++;
						}
						if (charge >= 4f && !hasReachedFullCharge)
						{
							SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/MeldSlice");
							style.Volume = 0.55f;
							style.Pitch = 1f;
							SoundEngine.PlaySound(in style, base.Projectile.Center);
							for (int i = 0; i < 12; i++)
							{
								Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, ModContent.DustType<LightDust>());
								dust2.velocity = ((float)Math.PI * 2f * (float)i / 12f).ToRotationVector2().RotatedBy(base.Projectile.rotation) * 5.5f * ((i % 2 == 0) ? 0.8f : 1f);
								dust2.scale = 0.7f * ((i % 2 == 0) ? 2.2f : 1.8f);
								dust2.noGravity = true;
								dust2.color = ((i % 2 == 0) ? c1 : c2);
								dust2.noLightEmittence = true;
							}
							hasReachedFullCharge = true;
						}
					}
				}
				else
				{
					hasStoppedHolding = true;
				}
				base.Projectile.Opacity = (charge - 1f) / 4f;
				Owner.direction = Math.Sign(Owner.Center.DirectionTo(Owner.Calamity().mouseWorld).X);
				float grenadeRot = 0f;
				if (completion >= 0.7f)
				{
					float completionLerp = (float)Math.Pow(Utils.GetLerpValue(0.7f, 1f, completion, clamped: true), 7.0);
					grenadeRot = MathHelper.ToRadians(MathHelper.Lerp(-75f, 130f, completionLerp) * (float)Owner.direction);
				}
				else
				{
					float completionLerp2 = (float)Math.Pow(Utils.GetLerpValue(0f, 0.7f, completion, clamped: true), 2.0);
					grenadeRot = MathHelper.ToRadians(MathHelper.Lerp(120f, -75f, completionLerp2) * (float)Owner.direction);
				}
				grenadeRot += Owner.Center.DirectionTo(Owner.Calamity().mouseWorld).ToRotation();
				Vector2 grenadePos = Owner.MountedCenter + Utils.RotatedBy(new Vector2(0f, (float)(-24 * Owner.direction)), (double)grenadeRot, default(Vector2));
				float completionLerp3 = (float)Math.Pow(Utils.GetLerpValue(0f, 0.7f, completion, clamped: true), 2.0);
				float grenadeHalfRot = MathHelper.ToRadians(MathHelper.Lerp(120f, -75f, completionLerp3) * (float)Owner.direction);
				base.Projectile.Center = grenadePos;
				base.Projectile.rotation = grenadeRot - MathHelper.ToRadians(25f * grenadeHalfRot);
				Owner.SetCompositeArmBack(enabled: true, Player.CompositeArmStretchAmount.Full, Owner.Center.DirectionTo(Owner.Calamity().mouseWorld).ToRotation() - MathHelper.ToRadians(90f));
				Owner.SetCompositeArmFront(enabled: true, Player.CompositeArmStretchAmount.Full, grenadeRot - ((Owner.direction == 1) ? MathHelper.ToRadians(180f) : MathHelper.ToRadians(0f)));
			}
		}
		time++;
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref mainColor)).ToVector3() * (0.3f + charge * 0.1f));
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		TileHit();
		return false;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_0658: Unknown result type (might be due to invalid IL or missing references)
		//IL_065d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0662: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0377: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0784: Unknown result type (might be due to invalid IL or missing references)
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0792: Unknown result type (might be due to invalid IL or missing references)
		//IL_079f: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07db: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0811: Unknown result type (might be due to invalid IL or missing references)
		//IL_0823: Unknown result type (might be due to invalid IL or missing references)
		//IL_0829: Unknown result type (might be due to invalid IL or missing references)
		//IL_082e: Unknown result type (might be due to invalid IL or missing references)
		//IL_084f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0847: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0907: Unknown result type (might be due to invalid IL or missing references)
		//IL_094b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0950: Unknown result type (might be due to invalid IL or missing references)
		//IL_0952: Unknown result type (might be due to invalid IL or missing references)
		//IL_095c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0971: Unknown result type (might be due to invalid IL or missing references)
		//IL_097b: Unknown result type (might be due to invalid IL or missing references)
		//IL_098a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0854: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1a: Unknown result type (might be due to invalid IL or missing references)
		float finalHitMult = 1f;
		if (base.Projectile.Calamity().stealthStrike && !doneHitting)
		{
			NPC chosenTarget = null;
			float distance = 2500f;
			for (int index = 0; index < Main.npc.Length; index++)
			{
				NPC searchedTarget = Main.npc[index];
				if (searchedTarget.CanBeChasedBy())
				{
					float num = searchedTarget.width / 2 + searchedTarget.height / 2;
					bool canHit = true;
					if (num < distance)
					{
						canHit = Collision.CanHit(base.Projectile.Center, 1, 1, Main.npc[index].Center, 1, 1);
					}
					if ((Vector2.Distance(base.Projectile.Center, searchedTarget.Center) < distance && (lastHitTarget == null || searchedTarget != lastHitTarget) && searchedTarget != target && searchedTarget.active && searchedTarget.life > 0) & canHit)
					{
						distance = Vector2.Distance(base.Projectile.Center, searchedTarget.Center);
						chosenTarget = searchedTarget;
					}
				}
			}
			if (chosenTarget == null)
			{
				if (lastHitTarget != null)
				{
					base.Projectile.localNPCImmunity[lastHitTarget.whoAmI] = 0;
				}
				for (int i = 0; i < Main.npc.Length; i++)
				{
					NPC searchedTarget2 = Main.npc[i];
					if (searchedTarget2.CanBeChasedBy())
					{
						float num2 = searchedTarget2.width / 2 + searchedTarget2.height / 2;
						bool canHit2 = true;
						if (num2 < distance)
						{
							canHit2 = Collision.CanHit(base.Projectile.Center, 1, 1, Main.npc[i].Center, 1, 1);
						}
						if ((Vector2.Distance(base.Projectile.Center, searchedTarget2.Center) < distance && searchedTarget2 != target && searchedTarget2.active && searchedTarget2.life > 0) & canHit2)
						{
							distance = Vector2.Distance(base.Projectile.Center, searchedTarget2.Center);
							chosenTarget = searchedTarget2;
						}
					}
				}
			}
			if (chosenTarget != null && base.Projectile.numHits <= maxStealthHits)
			{
				if (lastHitTarget != null)
				{
					base.Projectile.localNPCImmunity[lastHitTarget.whoAmI] = 0;
				}
				lastHitTarget = target;
				base.Projectile.velocity = base.Projectile.Center.DirectionTo(chosenTarget.Center) * 9f;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/DoomsdayDeviceImpact");
				style.Volume = 0.9f;
				style.Pitch = 0.1f + (float)base.Projectile.numHits * 0.15f;
				style.MaxInstances = 6;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				for (int j = 0; j <= 6; j++)
				{
					float variance = Main.rand.NextFloat(-0.6f, 0.6f);
					int dustStyle = ModContent.DustType<LightDust>();
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center, dustStyle, base.Projectile.velocity);
					dust.scale = Main.rand.NextFloat(1.5f, 1.7f) - Math.Abs(variance);
					dust.velocity = (base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 5f * charge).RotatedBy(variance) * Main.rand.NextFloat(0.3f, 1f) * (1f - Math.Abs(variance)) * finalHitMult;
					dust.noGravity = true;
					dust.color = (Main.rand.NextBool() ? c1 : c2);
				}
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 3f, "CalamityMod/Particles/HollowCircleSoftEdge", affectedByGravity: false, 14, 0.2f, c1 * 0.85f, new Vector2(3f, 1f), useAddativeBlend: true, glowCenter: false, MathHelper.ToRadians(90f), fadeIn: false, affectedByLight: false, 0.9f));
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 2.5f, "CalamityMod/Particles/HollowCircleSoftEdge", affectedByGravity: false, 14, 0.175f * finalHitMult, c2 * 0.7f, new Vector2(2.5f, 2f), useAddativeBlend: true, glowCenter: false, MathHelper.ToRadians(90f), fadeIn: false, affectedByLight: false, 0.9f));
				Owner.SetScreenshake(3f);
			}
			else
			{
				finalHitMult = 2f;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HeliumFlashCoreImpact");
				style.Volume = 0.7f;
				style.Pitch = 0.2f;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				base.Projectile.numHits = maxStealthHits;
			}
		}
		float minMult = 0.2f;
		int hitsToMinMult = maxStealthHits + 2;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult) * (doneHitting ? 0.3f : 1f);
		float finalDamageMult = charge / 4f * (hasReachedFullCharge ? 1.5f : 1f) * damageMult * Utils.Remap(finalHitMult, 1f, 2f, 1f, 5f);
		modifiers.SourceDamage *= finalDamageMult;
		if ((!doneHitting && !base.Projectile.Calamity().stealthStrike) || (!doneHitting && finalHitMult > 1f))
		{
			Vector2 launchDir = base.Projectile.Center.DirectionTo(target.Center);
			float launchPower = ((float)(hasReachedFullCharge ? 9 : 0) + charge * 1.5f) * finalHitMult;
			target.MoveNPC(launchDir, launchPower, ignoreKBImmune: true);
			float extraPitch = ((Owner.Calamity().rogueStealthMax > 0f) ? (0.25f * (Owner.Calamity().rogueStealth / Owner.Calamity().rogueStealthMax)) : 0f);
			if (!base.Projectile.Calamity().LocketClone && finalHitMult == 1f && giveStealth)
			{
				Owner.Calamity().rogueStealth += Owner.Calamity().rogueStealthMax * 0.3f;
				CheckStealth();
				giveStealth = false;
			}
			Owner.SetScreenshake(charge * (hasReachedFullCharge ? 1.2f : 0.8f) * finalHitMult);
			for (int k = 0; (float)k <= 12f * finalHitMult; k++)
			{
				float variance2 = Main.rand.NextFloat(-0.6f, 0.6f);
				int dustStyle2 = ModContent.DustType<LightDust>();
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center, dustStyle2, launchDir * 5f);
				dust2.scale = Main.rand.NextFloat(1.5f, 1.7f) - Math.Abs(variance2);
				dust2.velocity = (launchDir * 5f * charge).RotatedBy(variance2) * Main.rand.NextFloat(0.3f, 1f) * (1f - Math.Abs(variance2)) * finalHitMult;
				dust2.noGravity = true;
				dust2.color = (Main.rand.NextBool() ? c1 : c2);
			}
			SoundStyle style;
			if (hasReachedFullCharge)
			{
				style = new SoundStyle("CalamityMod/Sounds/Item/DoomsdayDeviceImpact");
				style.Volume = 1f;
				style.Pitch = 0.35f + extraPitch;
				style.MaxInstances = 6;
				SoundEngine.PlaySound(in style, base.Projectile.Center);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -launchDir * 3f, "CalamityMod/Particles/HollowCircleSoftEdge", affectedByGravity: false, 14, 0.4f * finalHitMult, c1 * 0.85f, new Vector2(3f, 1f), useAddativeBlend: true, glowCenter: false, MathHelper.ToRadians(90f), fadeIn: false, affectedByLight: false, 0.9f));
				GeneralParticleHandler.SpawnParticle(new CustomSpark(base.Projectile.Center, -launchDir * 2.5f, "CalamityMod/Particles/HollowCircleSoftEdge", affectedByGravity: false, 14, 0.35f * finalHitMult, c2 * 0.7f, new Vector2(2.5f, 2f), useAddativeBlend: true, glowCenter: false, MathHelper.ToRadians(90f), fadeIn: false, affectedByLight: false, 0.9f));
			}
			style = new SoundStyle("CalamityMod/Sounds/Item/DoomsdayDeviceImpact");
			style.Volume = (hasReachedFullCharge ? 1f : 0.7f);
			style.Pitch = 0.1f + extraPitch;
			style.MaxInstances = 6;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			doneHitting = true;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		if (((target.life <= 0 && target.realLife == -1) || target.lifeMax == 1) && base.Projectile.numHits < maxStealthHits && hasReachedFullCharge)
		{
			base.Projectile.numHits--;
			doneHitting = false;
		}
		else if (doneHitting && base.Projectile.extraUpdates != 4)
		{
			base.Projectile.velocity = Vector2.Lerp(base.Projectile.Center.DirectionFrom(target.Center) * 4f, Vector2.UnitY * -2f, 0.75f).RotatedByRandom(0.10000000149011612);
			base.Projectile.extraUpdates = 4;
		}
	}

	public void TileHit()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = Vector2.Zero;
		rotSpeed = 0f;
		base.Projectile.extraUpdates = 3;
		hasReachedFullCharge = false;
		if (tileHits == 0)
		{
			SoundStyle soundStyle = CommonCalamitySounds.VoidstoneMine with
			{
				Volume = 1f
			};
			soundStyle = soundStyle with
			{
				Volume = 0.3f,
				Pitch = -0.55f,
				MaxInstances = 6
			};
			SoundEngine.PlaySound(in soundStyle, base.Projectile.Center);
			soundStyle = RockPillar.HitSound with
			{
				Volume = 0.4f,
				Pitch = -0.2f,
				MaxInstances = 6
			};
			SoundEngine.PlaySound(in soundStyle, base.Projectile.Center);
		}
		tileHits++;
	}

	public void CheckStealth()
	{
		if (Owner.Calamity().rogueStealth > Owner.Calamity().rogueStealthMax)
		{
			Owner.Calamity().rogueStealth = Owner.Calamity().rogueStealthMax;
		}
		if (Owner.Calamity().rogueStealth < 0f)
		{
			Owner.Calamity().rogueStealth = 0f;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 30f, targetHitbox);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		Color glowColor = Color.Lerp(mainColor, Color.Red, Utils.GetLerpValue(60f, 18f, (stealthPenaltyTimer >= 18) ? stealthPenaltyTimer : 60));
		float fade = Utils.GetLerpValue(0f, 300f, base.Projectile.timeLeft, clamped: true);
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		for (int i = 0; i < 25; i++)
		{
			Color val = glowColor;
			((Color)(ref val)).A = 0;
			Color auraColor = val * 0.35f * fade;
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 25f).ToRotationVector2() * (hasReachedFullCharge ? 7f : 3f) * base.Projectile.Opacity;
			Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition + drawOffset + Main.rand.NextVector2Circular(4f, 4f), null, auraColor * base.Projectile.Opacity, base.Projectile.rotation, tex.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)((((!flung) ? Owner.direction : base.Projectile.direction) != 1) ? 2 : 0));
		}
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, lightColor * fade, base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)((((!flung) ? Owner.direction : base.Projectile.direction) != 1) ? 2 : 0));
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/DoomsdayDeviceGlow2", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, null, glowColor * base.Projectile.Opacity * fade, base.Projectile.rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Rogue/DoomsdayDeviceGlow2", (AssetRequestMode)2).Value.Size() * 0.5f, 1f, (SpriteEffects)((((!flung) ? Owner.direction : base.Projectile.direction) != 1) ? 2 : 0));
		return false;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		overPlayers.Add(index);
	}

	public DoomsdayDeviceProjectile()
	{
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		rotSpeed = 0.05f;
		charge = 1f;
		giveStealth = true;
		maxStealthHits = 5;
		mainColor = Color.White;
		c1 = Color.Turquoise;
		c2 = Color.Orchid;
		base._002Ector();
	}
}
