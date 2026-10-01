using System;
using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

[PierceResistException(false)]
public class EarthHoldout : BaseCustomUseStyleProjectile, ILocalizedModType, IModType
{
	public Vector2 mousePos;

	public Vector2 aimVel;

	public bool doSwing;

	public bool postSwing;

	public float fadeIn;

	public int useAnim;

	public int swingCount;

	public bool spawnBoom;

	public Color mainColor;

	public bool finalFlip;

	public int pause;

	public bool playSwingSound;

	public bool allowSecondHit;

	public float bladeFade;

	public int armoredHits;

	public override int AssignedItemID => ModContent.ItemType<Earth>();

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Earth>();

	public override string Texture => "CalamityMod/Items/Weapons/Melee/Earth";

	public override float HitboxOutset => 135f;

	public override Vector2 HitboxSize
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(288f, 288f) * (1f + bladeFade * 1.2f);
		}
	}

	public override float HitboxRotationOffset => MathHelper.ToRadians(-45f);

	public override Vector2 SpriteOrigin
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(0f, 186f);
		}
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = TrueMeleeDamageClass.Instance;
	}

	public override void WhenSpawned()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.knockBack = 0f;
		base.Projectile.scale = 1f;
		base.Projectile.ai[1] = 1f;
		mousePos = Owner.Calamity().mouseWorld;
		aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
		useAnim = Owner.itemAnimationMax * 2;
		if (mousePos.X < Owner.Center.X)
		{
			Owner.direction = -1;
		}
		else
		{
			Owner.direction = 1;
		}
		FlipAsSword = Owner.direction == -1;
	}

	public override void UseStyle()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0342: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0646: Unknown result type (might be due to invalid IL or missing references)
		//IL_0651: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_070c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0725: Unknown result type (might be due to invalid IL or missing references)
		//IL_072b: Unknown result type (might be due to invalid IL or missing references)
		//IL_072d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0732: Unknown result type (might be due to invalid IL or missing references)
		//IL_0737: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		//IL_0760: Unknown result type (might be due to invalid IL or missing references)
		//IL_077d: Unknown result type (might be due to invalid IL or missing references)
		//IL_078c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0802: Unknown result type (might be due to invalid IL or missing references)
		//IL_0808: Unknown result type (might be due to invalid IL or missing references)
		//IL_080a: Unknown result type (might be due to invalid IL or missing references)
		//IL_080f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0817: Unknown result type (might be due to invalid IL or missing references)
		//IL_0847: Unknown result type (might be due to invalid IL or missing references)
		//IL_0860: Unknown result type (might be due to invalid IL or missing references)
		//IL_0866: Unknown result type (might be due to invalid IL or missing references)
		//IL_0868: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0872: Unknown result type (might be due to invalid IL or missing references)
		//IL_0938: Unknown result type (might be due to invalid IL or missing references)
		//IL_093a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0945: Unknown result type (might be due to invalid IL or missing references)
		//IL_094a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0954: Unknown result type (might be due to invalid IL or missing references)
		//IL_0981: Unknown result type (might be due to invalid IL or missing references)
		//IL_098b: Unknown result type (might be due to invalid IL or missing references)
		//IL_099a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0884: Unknown result type (might be due to invalid IL or missing references)
		//IL_0895: Unknown result type (might be due to invalid IL or missing references)
		//IL_089a: Unknown result type (might be due to invalid IL or missing references)
		//IL_089f: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b81: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b86: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b8e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bcf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bd1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bdf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0be9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bf4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c87: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c95: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c9a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cb4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cf6: Unknown result type (might be due to invalid IL or missing references)
		if (pause > 0)
		{
			pause--;
			Animation--;
			return;
		}
		AnimationProgress = Animation % (float)useAnim;
		DrawUnconditionally = false;
		float rate = Main.GlobalTimeWrappedHourly * 12f;
		List<Color> earthColors = new List<Color>
		{
			Color.OrangeRed,
			Color.MediumTurquoise,
			Color.LimeGreen
		};
		int colorIndex = (int)(rate / 2f % (float)earthColors.Count);
		Color currentColor = earthColors[colorIndex];
		Color nextColor = earthColors[(colorIndex + 1) % earthColors.Count];
		mainColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		if (CanHit || postSwing)
		{
			mousePos = Owner.Center - aimVel;
		}
		else
		{
			mousePos = Owner.Calamity().mouseWorld;
		}
		if (CanHit)
		{
			fadeIn = MathHelper.Lerp(fadeIn, 1f, 0.2f);
		}
		else
		{
			fadeIn = MathHelper.Lerp(fadeIn, 0f, 0.28f);
		}
		if (base.Projectile.ai[1] == -1f)
		{
			bladeFade = MathHelper.Lerp(bladeFade, 1f, 0.15f);
		}
		else
		{
			bladeFade = MathHelper.Lerp(bladeFade, 0f, 0.045f);
		}
		if (!doSwing)
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				base.Projectile.localNPCImmunity[i] = 0;
			}
			allowSecondHit = true;
			playSwingSound = true;
			spawnBoom = true;
			base.Projectile.numHits = 0;
			mousePos = Owner.Calamity().mouseWorld;
			aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
			CanHit = false;
			if (mousePos.X < Owner.Center.X)
			{
				Owner.direction = -1;
			}
			else
			{
				Owner.direction = 1;
			}
			FlipAsSword = Owner.direction == -1;
			if (swingCount % 2 == 0)
			{
				useAnim = Owner.itemAnimationMax * 2;
			}
			else
			{
				useAnim = Owner.itemAnimationMax;
			}
			doSwing = true;
			finalFlip = false;
			armoredHits = 0;
		}
		else
		{
			if (!CanHit && !postSwing)
			{
				if (mousePos.X < Owner.Center.X)
				{
					Owner.direction = -1;
				}
				else
				{
					Owner.direction = 1;
				}
			}
			else if ((Owner.Center - aimVel).X < Owner.Center.X)
			{
				Owner.direction = -1;
			}
			else
			{
				Owner.direction = 1;
			}
			base.Projectile.rotation = base.Projectile.rotation.AngleLerp(Owner.AngleTo(mousePos) + MathHelper.ToRadians(45f), 0.1f);
			if (AnimationProgress < (float)useAnim / ((swingCount % 2 == 0) ? 1.3f : 7f))
			{
				aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
				CanHit = false;
				postSwing = false;
				if (AnimationProgress == 0f)
				{
					Animation = 0f;
					doSwing = false;
					base.Projectile.ai[1] = 0f - base.Projectile.ai[1];
				}
				RotationOffset = MathHelper.Lerp(RotationOffset, MathHelper.ToRadians(120f * base.Projectile.ai[1] * (float)Owner.direction * (1f + Utils.GetLerpValue((float)useAnim * 0.35f, (float)useAnim * 0.6f, Animation, clamped: true) * 0.25f)), 0.2f);
			}
			else
			{
				if (!finalFlip)
				{
					FlipAsSword = Owner.direction < 0;
				}
				float time = AnimationProgress - (float)useAnim / 2.5f;
				float timeMax = (float)useAnim - (float)useAnim / 2.5f;
				if (time >= (float)(int)(timeMax * 0.4f) && playSwingSound)
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/SwingMid");
					style.Volume = 0.8f;
					style.Pitch = ((base.Projectile.ai[1] == 1f) ? (-0.4f) : (-0.1f));
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					style = new SoundStyle("CalamityMod/Sounds/Item/HellkiteSwing", 2);
					style.Volume = 0.8f;
					style.Pitch = ((base.Projectile.ai[1] == 1f) ? 0.4f : 0.7f);
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					swingCount++;
					playSwingSound = false;
				}
				if ((int)time % 2 == 0 && base.Projectile.ai[1] == 1f && !Main.dedServ)
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/SwooshMid");
					style.Volume = 1f;
					style.Pitch = -0.4f;
					style.MaxInstances = -1;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				if (time > (float)(int)(timeMax * 0.45f) && time < (float)(int)(timeMax * 0.9f))
				{
					CanHit = true;
					for (int j = 0; j < 2; j++)
					{
						Vector2 particleVel = Utils.RotatedBy(new Vector2(0f, 10f * (0f - base.Projectile.ai[1]) * (float)Owner.direction), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2));
						GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(Owner.Center + Utils.RotatedBy(new Vector2((float)Main.rand.Next(30, (int)(170f * (1f + bladeFade * 0.6f))), 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)), -particleVel.RotatedByRandom(0.20000000298023224) * Main.rand.NextFloat(0.75f, 0.9f), affectedByGravity: false, 14, Main.rand.NextFloat(0.06f, 0.03f), mainColor, new Vector2(1.3f, 0.2f), quickShrink: true, glow: false, 0.55f));
					}
					for (int k = 0; k < 6; k++)
					{
						Vector2 particleVel2 = Utils.RotatedBy(new Vector2(0f, 10f * (0f - base.Projectile.ai[1]) * (float)Owner.direction), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2));
						Vector2 particlePos = Owner.Center + Utils.RotatedBy(new Vector2((float)Main.rand.Next(30, (int)(270f * (1f + bladeFade * 0.6f))), 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2));
						if (Main.rand.NextBool(3))
						{
							GeneralParticleHandler.SpawnParticle(new CustomSpark(particlePos + Main.rand.NextVector2Circular(15f, 15f), -particleVel2 * Main.rand.NextFloat(0.4f, 0.8f), "CalamityMod/Particles/Sparkle", affectedByGravity: false, 30, Main.rand.NextFloat(1.2f, 2.2f), mainColor, new Vector2(0.4f, Main.rand.NextFloat(0.9f, 1.4f)), useAddativeBlend: true, glowCenter: true));
						}
						else
						{
							GeneralParticleHandler.SpawnParticle(new CustomSpark(particlePos, -particleVel2.RotatedByRandom(0.20000000298023224) * 2f, "CalamityMod/Particles/LargeBloom", affectedByGravity: false, Main.rand.Next(7, 10), Main.rand.NextFloat(0.3f, 0.35f), mainColor * 0.65f, new Vector2(1f, 1.2f), useAddativeBlend: true, glowCenter: false, 0f, fadeIn: false, affectedByLight: false, 0.45f));
						}
					}
				}
				else
				{
					CanHit = false;
				}
				float start = ((swingCount % 2 != 0) ? (150f * base.Projectile.ai[1] * (float)Owner.direction) : (150f * base.Projectile.ai[1] * (float)Owner.direction));
				float end = ((swingCount % 2 != 0) ? (270f * (0f - base.Projectile.ai[1]) * (float)Owner.direction) : (120f * (0f - base.Projectile.ai[1]) * (float)Owner.direction));
				RotationOffset = MathHelper.Lerp(RotationOffset, MathHelper.ToRadians(MathHelper.Lerp(start, end, CalamityUtils.ExpInOutEasing(time / timeMax, 1))), 0.2f);
				if (time > timeMax * 0.8f)
				{
					RotationOffset = RotationOffset.AngleLerp(MathHelper.ToRadians(MathHelper.Lerp(start, end, CalamityUtils.ExpInOutEasing(time / timeMax, 1))), 0.2f);
				}
				if (time >= timeMax)
				{
					doSwing = false;
				}
				if (time < (float)(int)(timeMax * 0.7f))
				{
					postSwing = true;
				}
				if (CanHit)
				{
					for (int l = 0; l < 3; l++)
					{
						float randRot = Main.rand.NextFloat(-30f, -60f);
						Vector2 dustVel = Utils.RotatedBy(new Vector2(0f, 10f * (0f - base.Projectile.ai[1]) * (float)Owner.direction), (double)(base.FinalRotation + MathHelper.ToRadians(randRot)), default(Vector2));
						GeneralParticleHandler.SpawnParticle(new GenericSparkle(Owner.Center + Utils.RotatedBy(new Vector2(270f * (1f + bladeFade * 0.6f), 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)).RotatedByRandom(0.30000001192092896), Vector2.Zero, Color.White, mainColor, Main.rand.NextFloat(0.5f, 0.7f), 11, Main.rand.NextFloat(-0.1f, 0.1f), 2.68f));
						Dust dust = Dust.NewDustPerfect(Owner.Center + Utils.RotatedBy(new Vector2(270f * (1f + bladeFade * 0.6f), 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)).RotatedByRandom(0.30000001192092896), 278, dustVel);
						dust.scale = Main.rand.NextFloat(0.65f, 1.05f);
						dust.noGravity = true;
						dust.color = Color.Lerp(Color.White, mainColor, 0.5f);
					}
				}
			}
		}
		ArmRotationOffset = MathHelper.ToRadians(-140f);
		ArmRotationOffsetBack = MathHelper.ToRadians(-140f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_033a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0614: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0624: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_0677: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0686: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_054e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0553: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0702: Unknown result type (might be due to invalid IL or missing references)
		//IL_0712: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_071f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0741: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0754: Unknown result type (might be due to invalid IL or missing references)
		//IL_0784: Unknown result type (might be due to invalid IL or missing references)
		//IL_078a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0794: Unknown result type (might be due to invalid IL or missing references)
		//IL_0799: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<MiracleBlight>(), 600);
		if (target.life <= 0 && target.realLife == -1 && base.Projectile.numHits > 0)
		{
			base.Projectile.numHits--;
		}
		if (damageDone <= 2)
		{
			armoredHits++;
		}
		Vector2 launchVel = ((base.Projectile.ai[1] != 1f) ? Owner.Center.DirectionTo(Owner.Calamity().mouseWorld) : Owner.Center.DirectionTo(target.Center));
		target.MoveNPC(launchVel, 37f, ignoreKBImmune: true);
		if (spawnBoom)
		{
			for (int i = 0; i < 5; i++)
			{
				GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(target.Center, (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitY) * -25f, affectedByGravity: false, 12, 0.12f - (float)i * 0.025f, mainColor, new Vector2(3.75f, 0.9f), quickShrink: true, glow: false, 1.15f));
			}
			for (int j = 0; j < 15; j++)
			{
				float power = Main.rand.NextFloat(0f, 0.6f);
				GeneralParticleHandler.SpawnParticle(new CustomSpark(target.Center, (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitY).RotatedByRandom(power) * ((0f - Main.rand.NextFloat(18.5f, 50f)) * (1f - power)), "CalamityMod/Particles/Sparkle", affectedByGravity: false, 38, Main.rand.NextFloat(2.2f, 4.8f), mainColor, new Vector2(0.4f, Main.rand.NextFloat(0.9f, 1.4f)), useAddativeBlend: true, glowCenter: true));
			}
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/FinalDawnSlash");
			style.Volume = 0.85f;
			style.Pitch = Main.rand.NextFloat(0.2f, 0.3f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			style = new SoundStyle("CalamityMod/Sounds/NPCHit/ThanatosHitOpen1");
			style.Volume = 0.75f;
			style.Pitch = 0.2f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			style = new SoundStyle("CalamityMod/Sounds/Item/ExobladeBeamSlash");
			style.Volume = 0.35f;
			style.Pitch = Main.rand.NextFloat(0.5f, 0.7f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			if (base.Projectile.ai[1] == -1f)
			{
				NPC chosenTarget = Owner.ClampedMouseWorld().ClosestNPCAt(1000f);
				Projectile.NewProjectile(position: (chosenTarget != null && chosenTarget.active && chosenTarget.life > 0) ? (chosenTarget.Center + new Vector2(Main.rand.NextFloat(-450f, 450f), Main.rand.NextFloat(-450f, -650f))) : (target.Center + new Vector2(Main.rand.NextFloat(-450f, 450f), Main.rand.NextFloat(-450f, -650f))), spawnSource: base.Projectile.GetSource_FromThis(), velocity: Vector2.Zero, Type: ModContent.ProjectileType<EarthMeteor>(), Damage: (int)((float)base.Projectile.damage * 1.2f), KnockBack: base.Projectile.knockBack, Owner: base.Projectile.owner, ai0: 0f, ai1: chosenTarget?.whoAmI ?? 0, ai2: 2f);
			}
			spawnBoom = false;
		}
		int healPower = ((base.Projectile.ai[1] == -1f) ? 60 : 50);
		int heal = MathHelper.Clamp(healPower - base.Projectile.numHits * 35, 1, healPower);
		if (base.Projectile.numHits < 10)
		{
			Owner.DoLifestealDirect(target, heal, 0.2f);
		}
		if (base.Projectile.numHits > 2)
		{
			return;
		}
		float scaleFactor = 1f - (float)base.Projectile.numHits * 0.2f;
		int points = 6;
		float radians = (float)Math.PI * 2f / (float)points;
		Vector2 spinningPoint = Vector2.Normalize(new Vector2(-1f, -1f)).RotatedByRandom(100.0);
		for (int k = 0; k < points; k++)
		{
			Vector2 velocity = spinningPoint.RotatedBy(radians * (float)k).RotatedBy(-0.44999998807907104);
			if (k % 2 == 0)
			{
				velocity *= 5f;
			}
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(target.Center + velocity * 7.5f, velocity * 0.5f, affectedByGravity: false, 11, 0.08f * scaleFactor, mainColor, new Vector2(1f, 0.4f), quickShrink: true));
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(target.Center + velocity * 7.5f, velocity * 0.5f, affectedByGravity: false, 35, 0.06f * scaleFactor, mainColor, new Vector2(0.4f, 1.1f), quickShrink: false, glow: false));
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, Vector2.Zero, mainColor, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 2f * scaleFactor, 1f * scaleFactor, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(target.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat(-10f, 10f), 1f * scaleFactor, 0.5f * scaleFactor, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (int l = 0; l < MathHelper.Clamp(10 - base.Projectile.numHits * 2, 2, 10); l++)
		{
			float power2 = Main.rand.NextFloat(0.2f, 0.8f);
			Dust dust = Dust.NewDustPerfect(target.Center, 278, Owner.Center.DirectionTo(Owner.Calamity().mouseWorld).RotatedByRandom(power2) * (Main.rand.NextFloat(10f, 35f) * (1f - power2)));
			dust.scale = Main.rand.NextFloat(0.55f, 0.85f) * scaleFactor;
			dust.noGravity = true;
			dust.color = Color.Lerp(Color.White, mainColor, 0.5f);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 1f;
		int hitsToMinMult = 1;
		float damageMult = Utils.Remap(base.Projectile.numHits - armoredHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_068f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0694: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0705: Unknown result type (might be due to invalid IL or missing references)
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_053d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_057a: Unknown result type (might be due to invalid IL or missing references)
		//IL_057f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0584: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05da: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0605: Unknown result type (might be due to invalid IL or missing references)
		//IL_060f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		//IL_062e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_0643: Unknown result type (might be due to invalid IL or missing references)
		//IL_0649: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_072c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0665: Unknown result type (might be due to invalid IL or missing references)
		//IL_0742: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_075d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0762: Unknown result type (might be due to invalid IL or missing references)
		//IL_0767: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0781: Unknown result type (might be due to invalid IL or missing references)
		//IL_0797: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07de: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_075d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0762: Unknown result type (might be due to invalid IL or missing references)
		//IL_0767: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0781: Unknown result type (might be due to invalid IL or missing references)
		//IL_0797: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_080f: Unknown result type (might be due to invalid IL or missing references)
		float spriteMult = 1.13f;
		if ((useAnim > 0 || DrawUnconditionally) && Owner.ItemAnimationActive)
		{
			Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
			Asset<Texture2D> glowTex = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/EarthGlow", (AssetRequestMode)2);
			float r = (FlipAsSword ? MathHelper.ToRadians(90f) : 0f);
			Color val;
			for (int i = 0; i < 20; i++)
			{
				Texture2D centerTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/EarthGhost", (AssetRequestMode)2).Value;
				val = mainColor;
				((Color)(ref val)).A = 0;
				Color auraColor = val * 0.15f * fadeIn;
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 20f).ToRotationVector2() * 6f * fadeIn;
				Main.EntitySpriteDraw(centerTexture, base.Projectile.Center - Main.screenPosition + drawOffset + new Vector2(0f, Owner.gfxOffY), centerTexture.Frame(1, FrameCount, 0, Frame), auraColor, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects != 0) ? ((int)spriteEffects) : (FlipAsSword ? 1 : 0)));
			}
			Asset<Texture2D> swoosh = ModContent.Request<Texture2D>("CalamityMod/Particles/VerticalSmearLarge", (AssetRequestMode)2);
			if (swingCount > 0)
			{
				if (swingCount % 2 != 0)
				{
					Texture2D value = swoosh.Value;
					Vector2 position = base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY);
					val = mainColor;
					((Color)(ref val)).A = 0;
					Main.EntitySpriteDraw(value, position, null, val * fadeIn * 0.9f, base.FinalRotation + MathHelper.ToRadians(45f) + MathHelper.ToRadians((float)((swingCount % 2 != 0) ? (-80) : 80)) * (float)(-Owner.direction), swoosh.Size() * 0.5f, spriteMult * base.Projectile.scale * 3.15f / 4f, (SpriteEffects)0);
					Texture2D value2 = swoosh.Value;
					Vector2 position2 = base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY);
					val = mainColor;
					((Color)(ref val)).A = 0;
					Main.EntitySpriteDraw(value2, position2, null, val * fadeIn * 0.9f, base.FinalRotation + MathHelper.ToRadians(45f) + MathHelper.ToRadians((float)((swingCount % 2 != 0) ? (-80) : 80)) * (float)(-Owner.direction), swoosh.Size() * 0.5f, spriteMult * base.Projectile.scale * 3.15f / 4f, (SpriteEffects)2);
				}
				else
				{
					Texture2D value3 = swoosh.Value;
					Vector2 position3 = base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY);
					val = mainColor;
					((Color)(ref val)).A = 0;
					Main.EntitySpriteDraw(value3, position3, null, val * fadeIn * 0.9f, base.FinalRotation + MathHelper.ToRadians(45f) + MathHelper.ToRadians((float)((swingCount % 2 != 0) ? (-85) : 85)) * (float)(-Owner.direction), swoosh.Size() * 0.5f, spriteMult * base.Projectile.scale * 3.15f * 1.6f / 4f, (SpriteEffects)0);
				}
			}
			Asset<Texture2D> tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
			int draws = 24;
			for (int j = 0; j < draws; j++)
			{
				float swordTipScale = (((float)j > (float)draws * 0.73f) ? Utils.Remap(j, (int)((float)draws * 0.73f), draws, 0.9f, 0.35f) : 1f);
				Vector2 offsetDir = Vector2.One.RotatedBy(base.Projectile.rotation + RotationOffset + MathHelper.ToRadians(90f));
				val = Color.Lerp(Color.Lerp(Color.MediumTurquoise, Color.Lerp(Color.LimeGreen, Color.OrangeRed, (float)j * 0.8f / (float)draws), (float)j / ((float)draws * 0.6f)), Color.White, 0.2f);
				((Color)(ref val)).A = 0;
				Color auraColor2 = val * 0.4f * bladeFade;
				Vector2 drawOffset2 = -offsetDir * 9f * (float)j * bladeFade;
				Main.EntitySpriteDraw(tex2.Value, base.Projectile.Center - offsetDir * 70f - Main.screenPosition + drawOffset2 + new Vector2(0f, Owner.gfxOffY) + Main.rand.NextVector2Circular(2f, 2f), tex2.Frame(1, FrameCount, 0, Frame), auraColor2, RotationOffset + base.Projectile.rotation + MathHelper.ToRadians(45f), tex2.Size() * 0.5f, new Vector2(0.7f * swordTipScale, 1f) * (0.75f * swordTipScale) * 0.7f * bladeFade * spriteMult, (SpriteEffects)(((int)spriteEffects != 0) ? ((int)spriteEffects) : (FlipAsSword ? 1 : 0)));
			}
			Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY), tex.Frame(1, FrameCount, 0, Frame), lightColor, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects == 0) ? (FlipAsSword ? 1 : 0) : ((int)spriteEffects)));
			Main.EntitySpriteDraw(glowTex.Value, base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY), glowTex.Frame(1, FrameCount, 0, Frame), Color.White, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)glowTex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects == 0) ? (FlipAsSword ? 1 : 0) : ((int)spriteEffects)));
		}
		return false;
	}

	public override void ResetStyle()
	{
	}

	public EarthHoldout()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		doSwing = true;
		spawnBoom = true;
		mainColor = Color.OrangeRed;
		playSwingSound = true;
		allowSecondHit = true;
		base._002Ector();
	}
}
