using System;
using CalamityMod.Dusts;
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
public class SkytideDragoonHoldout : BaseCustomUseStyleProjectile, ILocalizedModType, IModType
{
	public Vector2 mousePos;

	public Vector2 aimVel;

	public bool doSwing;

	public bool postSwing;

	public float fadeIn;

	public float colorFadeIn;

	public int useAnim;

	public int swingCount;

	public float spearOutset;

	public bool fireProj;

	public bool redirected;

	public int attackPower;

	public float attackMult;

	public Vector2 tipOutset;

	public Color color1;

	public Color color2;

	public override int AssignedItemID => ModContent.ItemType<SkytideDragoon>();

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<SkytideDragoon>();

	public override string Texture => "CalamityMod/Items/Weapons/Melee/SkytideDragoon";

	public override float HitboxOutset => 245f;

	public override Vector2 HitboxSize
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(40f, 40f);
		}
	}

	public override float HitboxRotationOffset => MathHelper.ToRadians(-45f);

	public override Vector2 SpriteOrigin
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(0f, 135f);
		}
	}

	public bool fancySwing => swingCount % 7 == 0;

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = DamageClass.Melee;
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
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.knockBack = 0f;
		base.Projectile.scale = 1f;
		base.Projectile.ai[1] = 1f;
		mousePos = Owner.Calamity().mouseWorld;
		aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
		base.Projectile.rotation = Vector2.UnitY.ToRotation() + MathHelper.ToRadians(-135f);
		useAnim = (fancySwing ? ((int)((float)Owner.itemAnimationMax * 2f)) : Owner.itemAnimationMax);
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
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_067f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_070c: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_087c: Unknown result type (might be due to invalid IL or missing references)
		//IL_088c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0891: Unknown result type (might be due to invalid IL or missing references)
		//IL_0896: Unknown result type (might be due to invalid IL or missing references)
		//IL_089b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0848: Unknown result type (might be due to invalid IL or missing references)
		//IL_0853: Unknown result type (might be due to invalid IL or missing references)
		//IL_076b: Unknown result type (might be due to invalid IL or missing references)
		//IL_077b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0780: Unknown result type (might be due to invalid IL or missing references)
		//IL_0785: Unknown result type (might be due to invalid IL or missing references)
		//IL_078a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_047d: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0490: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0509: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_0512: Unknown result type (might be due to invalid IL or missing references)
		//IL_0913: Unknown result type (might be due to invalid IL or missing references)
		//IL_091e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0936: Unknown result type (might be due to invalid IL or missing references)
		//IL_0945: Unknown result type (might be due to invalid IL or missing references)
		//IL_094a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0959: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_0560: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a08: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a64: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a6e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a73: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a79: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a83: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a8d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b07: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b30: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b44: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b54: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c51: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c6b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c63: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c97: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ca6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cbf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cc6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ce9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cfb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d02: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d10: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d29: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d49: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d53: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d60: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d98: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d9a: Unknown result type (might be due to invalid IL or missing references)
		tipOutset = Vector2.One.RotatedBy(base.Projectile.rotation + MathHelper.ToRadians(90f)) * spearOutset * 12f;
		AbsolutePosition = Owner.MountedCenter + tipOutset;
		AnimationProgress = Animation % (float)useAnim;
		DrawUnconditionally = false;
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
			fadeIn = MathHelper.Lerp(fadeIn, 1f, 0.15f);
		}
		else
		{
			fadeIn = MathHelper.Lerp(fadeIn, 0f, 0.12f);
		}
		colorFadeIn = MathHelper.Lerp(colorFadeIn, 0f, 0.07f);
		attackMult = MathHelper.Lerp(attackMult, (float)attackPower, 0.15f);
		if (!doSwing)
		{
			for (int i = 0; i < Main.maxNPCs; i++)
			{
				base.Projectile.localNPCImmunity[i] = 0;
			}
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
			fireProj = true;
			doSwing = true;
			swingCount++;
			if (swingCount == 1 && Owner.Calamity().mouseRight)
			{
				redirected = true;
			}
			else
			{
				redirected = false;
			}
			useAnim = ((fancySwing || redirected) ? ((int)((float)Owner.itemAnimationMax * 2f)) : Owner.itemAnimationMax);
			AnimationProgress = Animation % (float)useAnim;
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
			if (fancySwing)
			{
				base.Projectile.rotation = base.Projectile.rotation.AngleLerp(Vector2.UnitY.ToRotation() + MathHelper.ToRadians(-135f), redirected ? 0.1f : 0.22f);
			}
			else if (redirected)
			{
				float lerp = Utils.GetLerpValue(0f, (float)useAnim / 2f, AnimationProgress);
				CanHit = true;
				if (AnimationProgress < (float)useAnim / 2f)
				{
					base.Projectile.rotation += 0.81f * (float)Owner.direction * (1f - lerp);
					base.Projectile.rotation = MathHelper.WrapAngle(base.Projectile.rotation);
					for (int j = 0; j < 2; j++)
					{
						Color color = Color.Lerp(Color.White, Main.rand.NextBool() ? color1 : this.color2, 0.65f);
						Vector2 pos = Owner.Center + Utils.RotatedBy(new Vector2(160f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)).RotatedByRandom(0.30000001192092896);
						Vector2 vel = base.Projectile.rotation.ToRotationVector2().RotatedByRandom(0.4000000059604645) * Main.rand.NextFloat(2f, 8f);
						Dust dust = Dust.NewDustPerfect(pos, ModContent.DustType<LightDust>(), vel, 0, default(Color), Main.rand.NextFloat(1.85f, 2.2f));
						dust.noGravity = true;
						dust.color = color;
						GeneralParticleHandler.SpawnParticle(new BoltParticle(pos, -vel.RotatedBy((Owner.direction == 1) ? MathHelper.ToRadians(45f) : MathHelper.ToRadians(-135f)) * Main.rand.NextFloat(1.5f, 1.8f), affectedByGravity: false, 13, Main.rand.NextFloat(0.2f, 0.35f), color * 0.8f, new Vector2(1.2f, 1f), glowCenter: true, glowFade: true, fadeIn: false, 0.25f));
						if (j % 2 == 0)
						{
							GeneralParticleHandler.SpawnParticle(new CustomSpark(pos, Vector2.Zero, "CalamityMod/Particles/BloomCircle", affectedByGravity: false, 35, 0.75f, color * 0.75f, new Vector2(1f, 1f)));
						}
					}
				}
				else
				{
					base.Projectile.rotation = base.Projectile.rotation.AngleLerp(Owner.AngleTo(mousePos) + MathHelper.ToRadians(45f), 0.15f);
				}
			}
			else
			{
				base.Projectile.rotation = base.Projectile.rotation.AngleLerp(Owner.AngleTo(mousePos) + MathHelper.ToRadians(45f), 0.25f);
			}
			if (AnimationProgress < (float)(useAnim / 2))
			{
				_ = AnimationProgress;
				_ = useAnim / 3;
				_ = useAnim;
				_ = useAnim / 3;
				aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
				if (redirected)
				{
					CanHit = true;
				}
				else
				{
					CanHit = false;
				}
				postSwing = false;
				if (AnimationProgress == 0f)
				{
					doSwing = false;
				}
				spearOutset = MathHelper.Lerp(spearOutset, MathHelper.ToRadians(120f), 0.2f);
				FlipAsSword = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX).X > 0f;
			}
			else
			{
				float time = AnimationProgress - (float)(useAnim / 3);
				float timeMax = useAnim - useAnim / 3;
				spearOutset = MathHelper.Lerp(spearOutset, MathHelper.ToRadians(120f), 0.2f);
				if (time == (float)(int)(timeMax * 0.4f))
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/SkytideSwing");
					style.Volume = 1f;
					style.Pitch = Main.rand.NextFloat(-0.1f, 0.1f) - (fancySwing ? 0.3f : 0f);
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				if (time > (float)(int)(timeMax * 0.4f) && time < (float)(int)(timeMax * 0.7f))
				{
					aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
					CanHit = true;
				}
				else if (fireProj && time >= (float)(int)(timeMax * 0.7f))
				{
					if (fancySwing)
					{
						Owner.SetScreenshake(3f);
						SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/SkytideBolt");
						style.Volume = 0.8f;
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), Owner.Center + new Vector2(0f, -600f), new Vector2(0f, 10f), ModContent.ProjectileType<DragoonBigBolt>(), base.Projectile.damage * 10, base.Projectile.knockBack, base.Projectile.owner, 0f, 0.5f).timeLeft = 45;
						swingCount = 0;
						attackPower = 6;
					}
					else if (redirected)
					{
						Owner.SetScreenshake(4.5f);
						SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/SkytideBolt");
						style.Volume = 1f;
						style.Pitch = -0.2f;
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						style = new SoundStyle("CalamityMod/Sounds/Item/AuricBulletHit");
						style.Volume = 0.5f;
						style.Pitch = 0.2f;
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), Owner.Center - aimVel * 2f, aimVel.SafeNormalize(Vector2.UnitX) * -10f, ModContent.ProjectileType<DragoonBigBolt>(), base.Projectile.damage * 10, base.Projectile.knockBack, base.Projectile.owner, 0f, 1f);
						swingCount = -1;
						attackPower = 0;
					}
					else
					{
						for (int k = -2; k < 3; k++)
						{
							float rot = 0.1f * (float)attackPower * (float)k / 6f;
							Vector2 vel2 = rot.ToRotationVector2().RotatedBy(aimVel.ToRotation()) * -7f;
							Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), Owner.Center - aimVel * 2f, vel2, ModContent.ProjectileType<DragoonSmallBolt>(), (int)((double)base.Projectile.damage * 0.5), base.Projectile.knockBack, base.Projectile.owner, 0f, rot);
						}
						attackPower--;
					}
					colorFadeIn = 1f;
					fireProj = false;
				}
				if (time >= (float)(int)(timeMax * 0.85f))
				{
					CanHit = false;
				}
				spearOutset = MathHelper.Lerp(spearOutset, MathHelper.ToRadians(MathHelper.Lerp(450f, 0f, CalamityUtils.ExpInOutEasing(time / timeMax, 1))), 0.2f);
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
					for (int l = 0; l < 2; l++)
					{
						Color color2 = Color.Lerp(Color.White, Main.rand.NextBool() ? color1 : this.color2, 0.65f);
						float bonus = (float)(1 - 7 / (attackPower + 1)) * 0.3f;
						Dust obj = Dust.NewDustPerfect(Owner.Center + Utils.RotatedBy(new Vector2(160f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)).RotatedByRandom(0.05000000074505806), Velocity: base.Projectile.rotation.ToRotationVector2().RotatedBy(MathHelper.ToRadians(-45f)).RotatedByRandom(0.5) * Main.rand.NextFloat(6f, 10f) * (1f + bonus), Type: Main.rand.NextBool(3) ? 278 : ModContent.DustType<LightDust>(), Alpha: 0, newColor: default(Color), Scale: Main.rand.NextFloat(1.05f, 1.4f));
						obj.noGravity = obj.type != 278;
						obj.color = color2;
						obj.scale += bonus;
					}
				}
			}
		}
		ArmRotationOffset = MathHelper.ToRadians(-140f);
		ArmRotationOffsetBack = MathHelper.ToRadians(-140f);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		float _ = float.NaN;
		return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), Owner.Center, Owner.Center + (base.Projectile.rotation - MathHelper.ToRadians(45f)).ToRotationVector2() * HitboxOutset + tipOutset, HitboxSize.X * base.Projectile.scale, ref _);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		if ((damageDone <= 2 || (target.life <= 0 && target.realLife == -1)) && base.Projectile.numHits > 0)
		{
			base.Projectile.numHits--;
		}
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/AuricBulletHit");
		style.Volume = 0.55f;
		style.Pitch = 0.8f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		style = new SoundStyle("CalamityMod/Sounds/Custom/DefenseDamage");
		style.Volume = 0.65f;
		style.Pitch = 0.4f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		Vector2 launchVel = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld);
		target.MoveNPC(launchVel, 23f, ignoreKBImmune: true);
		target.AddBuff(144, 230);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		float minMult = 0.3f;
		int hitsToMinMult = 5;
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0476: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_054b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_056e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0573: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_0583: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c9: Unknown result type (might be due to invalid IL or missing references)
		if ((useAnim > 0 || DrawUnconditionally) && Owner.ItemAnimationActive)
		{
			Asset<Texture2D> tex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SkytideDragoonHoldout", (AssetRequestMode)2);
			Asset<Texture2D> glowTex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SkytideDragoonGlow", (AssetRequestMode)2);
			float fxRot = base.Projectile.rotation + MathHelper.ToRadians(45f);
			float fxPosRot = base.Projectile.rotation - MathHelper.ToRadians(45f);
			float r = (FlipAsSword ? MathHelper.ToRadians(90f) : 0f);
			Vector2 drawPos = base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY) + base.Projectile.velocity.SafeNormalize(Vector2.UnitX);
			Color val = Color.Lerp(color1, color2, colorFadeIn);
			((Color)(ref val)).A = 0;
			Color glowColor = val;
			val = Color.Lerp(color2, color1, colorFadeIn);
			((Color)(ref val)).A = 0;
			Color auraColor = val * 0.15f * fadeIn;
			for (int i = 0; i < 25; i++)
			{
				Texture2D centerTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/SkytideDragoonGhost", (AssetRequestMode)2).Value;
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 25f).ToRotationVector2() * 7.2f * fadeIn;
				Main.EntitySpriteDraw(centerTexture, drawPos + drawOffset, centerTexture.Frame(1, FrameCount, 0, Frame), auraColor, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects != 0) ? ((int)spriteEffects) : (FlipAsSword ? 1 : 0)));
			}
			Main.EntitySpriteDraw(tex.Value, drawPos, tex.Frame(1, FrameCount, 0, Frame), lightColor, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects == 0) ? (FlipAsSword ? 1 : 0) : ((int)spriteEffects)));
			for (int j = 0; j < 2; j++)
			{
				Main.EntitySpriteDraw(glowTex.Value, drawPos, glowTex.Frame(1, FrameCount, 0, Frame), glowColor, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)glowTex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects != 0) ? ((int)spriteEffects) : (FlipAsSword ? 1 : 0)));
			}
			Asset<Texture2D> tex2 = ModContent.Request<Texture2D>("CalamityMod/Particles/ArchSmear", (AssetRequestMode)2);
			Asset<Texture2D> orb = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2);
			float sine = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 55.5f / (float)Math.PI);
			for (int k = 0; k < 6; k++)
			{
				val = Color.Lerp(color2, color1, (float)((k + 4) / 6));
				((Color)(ref val)).A = 0;
				Color tipColor = val * 0.3f * fadeIn;
				Vector2 scale = new Vector2(0.25f - (float)k * 0.04f, (1.5f + (float)k * 0.15f) * colorFadeIn) * (0.75f * colorFadeIn * Main.rand.NextFloat(0.9f, 1.1f) + 0.25f);
				Main.EntitySpriteDraw(tex2.Value, Owner.Center - Main.screenPosition + fxPosRot.ToRotationVector2() * 130f, null, tipColor, fxRot, tex2.Size() * 0.5f, scale, (SpriteEffects)0);
			}
			for (int l = 0; l < 6; l++)
			{
				val = Color.Lerp(Color.Lerp(color2, color1, (float)((l + 2) / 6)), Color.White, (float)(l / 6));
				((Color)(ref val)).A = 0;
				Color orbColor = val * 0.5f;
				Vector2 scale2 = new Vector2(Math.Abs(sine * 0.5f) + 0.1f, 1f) * (0.05f + (float)l * 0.01f) * attackMult * Main.rand.NextFloat(0.9f, 1.1f) * 2f;
				Main.EntitySpriteDraw(orb.Value, Owner.Center - Main.screenPosition + fxPosRot.ToRotationVector2() * 180f + tipOutset, null, orbColor, Main.rand.NextFloat(-5f, 5f), orb.Size() * 0.5f, scale2, (SpriteEffects)0);
			}
		}
		return false;
	}

	public override void ResetStyle()
	{
	}

	public SkytideDragoonHoldout()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		doSwing = true;
		fireProj = true;
		color1 = Color.Orchid;
		color2 = Color.Cyan;
		base._002Ector();
	}
}
