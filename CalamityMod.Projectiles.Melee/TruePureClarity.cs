using System;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Particles;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TruePureClarity : BaseCustomUseStyleProjectile, ILocalizedModType, IModType
{
	public Vector2 mousePos;

	public Vector2 aimPos;

	public bool doSwing;

	public bool postSwing;

	public int useAnimation;

	public static readonly SoundStyle FullChargeSound = new SoundStyle("CalamityMod/Sounds/Item/MagicRockSound");

	public const int dashTime = 30;

	public float dashRotation;

	public Vector2 lastDisplacement;

	public Vector2 dashDirection;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override int AssignedItemID => ModContent.ItemType<TrueBiomeBlade>();

	public override string Texture => "CalamityMod/Items/Weapons/Melee/TrueBiomeBlade";

	public override float HitboxOutset => 65f;

	public override Vector2 HitboxSize
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(100f, 100f);
		}
	}

	public override float HitboxRotationOffset => MathHelper.ToRadians(-45f);

	public override Vector2 SpriteOrigin
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(-2f, 70f);
		}
	}

	public ref float State => ref base.Projectile.ai[0];

	public ref float SwingDir => ref base.Projectile.ai[1];

	public Color outlineColorGreen
	{
		get
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			return (Main.player[base.Projectile.owner].HeldItem.ModItem as TrueBiomeBlade).mainAttunement.tooltipColor;
		}
	}

	public Color outlineColorBlue
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(110, 216, 255);
		}
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.scale = 1.25f;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void WhenSpawned()
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		SwingDir = 1f;
		mousePos = Owner.Calamity().mouseWorld;
		aimPos = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
		useAnimation = Owner.itemAnimationMax;
		Owner.direction = ((!(mousePos.X < Owner.Center.X)) ? 1 : (-1));
		FlipAsSword = Owner.direction == -1;
	}

	public override void UseStyle()
	{
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0729: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0748: Unknown result type (might be due to invalid IL or missing references)
		//IL_074e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_075e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0763: Unknown result type (might be due to invalid IL or missing references)
		//IL_0768: Unknown result type (might be due to invalid IL or missing references)
		//IL_0772: Unknown result type (might be due to invalid IL or missing references)
		//IL_0777: Unknown result type (might be due to invalid IL or missing references)
		//IL_077f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0784: Unknown result type (might be due to invalid IL or missing references)
		//IL_078e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07be: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_083f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0849: Unknown result type (might be due to invalid IL or missing references)
		//IL_084e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0488: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0565: Unknown result type (might be due to invalid IL or missing references)
		//IL_0570: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.CantUseHoldout() && State == 0f && base.Projectile.numHits > 2)
		{
			State = 1f;
			SoundStyle style = SoundID.Item120 with
			{
				Volume = SoundID.Item120.Volume * 0.5f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			AnimationProgress = 0f;
			base.Projectile.rotation = (Owner.Calamity().mouseWorld - Owner.Center).SafeNormalize(Vector2.UnitX).ToRotation();
			base.Projectile.timeLeft = 30;
			lastDisplacement = base.Projectile.Center - Owner.Center;
			base.Projectile.ForceNetUpdate();
		}
		if (State == 0f)
		{
			dashDirection = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld, Vector2.Zero).SafeNormalize(Vector2.UnitX);
			if (CanHit || postSwing)
			{
				mousePos = Owner.Center - aimPos;
			}
			else
			{
				mousePos = Owner.Calamity().mouseWorld;
			}
			if (!doSwing)
			{
				for (int i = 0; i < Main.maxNPCs; i++)
				{
					base.Projectile.localNPCImmunity[i] = 0;
				}
				mousePos = Owner.Calamity().mouseWorld;
				aimPos = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
				CanHit = false;
				Owner.direction = ((!(mousePos.X < Owner.Center.X)) ? 1 : (-1));
				FlipAsSword = Owner.direction == -1;
				doSwing = true;
			}
			else
			{
				if (!CanHit && !postSwing)
				{
					Owner.direction = ((!(mousePos.X < Owner.Center.X)) ? 1 : (-1));
				}
				else
				{
					Owner.direction = ((!((Owner.Center - aimPos).X < Owner.Center.X)) ? 1 : (-1));
				}
				base.Projectile.rotation = base.Projectile.rotation.AngleLerp(Owner.AngleTo(mousePos) + MathHelper.ToRadians(65f), 0.1f);
				if (AnimationProgress < (float)(useAnimation / 3))
				{
					aimPos = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
					CanHit = false;
					postSwing = false;
					if (AnimationProgress == 0f)
					{
						doSwing = false;
						SwingDir = 0f - SwingDir;
					}
					RotationOffset = MathHelper.Lerp(RotationOffset, MathHelper.ToRadians(120f * SwingDir * (float)Owner.direction), 0.2f);
				}
				else
				{
					float swingTime = AnimationProgress - (float)(useAnimation / 3);
					float swingTimeMax = useAnimation - useAnimation / 3;
					if (swingTime > (float)(int)(swingTimeMax * 0.2f) && swingTime < (float)(int)(swingTimeMax * 0.85f))
					{
						CanHit = true;
						Vector2 particleVel = Utils.RotatedBy(new Vector2(0f, 10f * (0f - SwingDir) * (float)Owner.direction), (double)(base.FinalRotation - (float)Math.PI / 4f), default(Vector2));
						Vector2 particlePos = Owner.Center + Utils.RotatedBy(new Vector2((float)Main.rand.Next(0, 80), 0f), (double)(base.FinalRotation - (float)Math.PI / 4f), default(Vector2));
						Color particleColor = (Main.rand.NextBool() ? outlineColorBlue : outlineColorGreen);
						bool empowered = base.Projectile.numHits > 2;
						if (Main.rand.NextBool())
						{
							GeneralParticleHandler.SpawnParticle(new GenericBloom(particlePos, particleVel, particleColor, empowered ? 0.12f : 0.08f, 20));
						}
						else
						{
							GeneralParticleHandler.SpawnParticle(new GenericSparkle(particlePos, particleVel, particleColor, particleColor, empowered ? 0.78f : 0.55f, 20));
						}
					}
					else
					{
						CanHit = false;
					}
					if (swingTime == (float)(int)(swingTimeMax * 0.4f))
					{
						SoundStyle style = SoundID.Item43 with
						{
							Volume = 0.65f
						};
						SoundEngine.PlaySound(in style, base.Projectile.Center);
						Vector2 projVel = -aimPos.SafeNormalize(Vector2.UnitX) * 12f;
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), Owner.Center, projVel, ModContent.ProjectileType<TruePurityProjection>(), base.Projectile.damage, base.Projectile.knockBack, Owner.whoAmI);
					}
					RotationOffset = MathHelper.Lerp(RotationOffset, MathHelper.ToRadians(MathHelper.Lerp(150f * SwingDir * (float)Owner.direction, 120f * (0f - SwingDir) * (float)Owner.direction, CalamityUtils.ExpInOutEasing(swingTime / swingTimeMax, 1))), 0.2f);
					if (swingTime >= swingTimeMax)
					{
						doSwing = false;
					}
					if (swingTime < (float)(int)(swingTimeMax * 0.7f))
					{
						postSwing = true;
					}
				}
			}
			ArmRotationOffset = MathHelper.ToRadians(-140f);
			ArmRotationOffsetBack = MathHelper.ToRadians(-140f);
		}
		else
		{
			CanHit = true;
			Owner.direction = ((Owner.velocity.X > 0f) ? 1 : (-1));
			FlipAsSword = Owner.direction == -1;
			RotationOffset = (float)Math.PI / 4f * (float)Owner.direction;
			Owner.itemAnimation = 2;
			Owner.fallStart = (int)(Owner.position.Y / 16f);
			Owner.Calamity().LungingDown = true;
			Vector2 waterVel = Owner.velocity.RotatedBy((float)Math.PI / 2f * (float)(Main.rand.NextBool() ? 1 : (-1))).RotatedByRandom(0.19634954631328583).SafeNormalize(Vector2.UnitX) * 5f;
			GeneralParticleHandler.SpawnParticle(new WaterFoamParticle(Owner.Center, waterVel, 20, 0.45f, outlineColorBlue));
			if (Collision.SolidCollision(Owner.Center + dashDirection * 120f * base.Projectile.scale - Vector2.One * 5f, 10, 10))
			{
				base.Projectile.timeLeft = 0;
				Owner.itemAnimation = 0;
				Owner.Calamity().LungingDown = false;
				base.Projectile.active = false;
				base.Projectile.ForceNetUpdate();
			}
			Owner.velocity = dashDirection * 30f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.numHits == 2)
		{
			SoundEngine.PlaySound(in FullChargeSound, Owner.Center);
			for (int i = 0; i < 10; i++)
			{
				Vector2 particleVel = Vector2.UnitX.RotatedBy((float)Math.PI * 2f * ((float)i / 10f)) * Main.rand.NextFloat(3.5f, 4f);
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(Owner.Center, particleVel, outlineColorGreen, Color.Brown, 1f, 192f));
			}
		}
		if (State != 1f)
		{
			return;
		}
		Owner.GiveUniversalIFrames(TrueBiomeBlade.DefaultAttunement_LungeIFrames);
		Projectile[] projectile = Main.projectile;
		foreach (Projectile proj in projectile)
		{
			if (proj.active && proj.type == ModContent.ProjectileType<PurityProjectionSigil>() && proj.owner == Owner.whoAmI)
			{
				proj.ai[0] = target.whoAmI;
				proj.timeLeft = TrueBiomeBlade.DefaultAttunement_SigilTime;
				return;
			}
		}
		Projectile.NewProjectile(Owner.GetSource_ItemUse(Owner.HeldItem), target.Center, Vector2.Zero, ModContent.ProjectileType<PurityProjectionSigil>(), 0, 0f, Owner.whoAmI, target.whoAmI);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (State == 1f)
		{
			modifiers.SourceDamage *= TrueBiomeBlade.DefaultAttunement_LungeDamageMult;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		if (State == 1f)
		{
			Player owner = Owner;
			owner.velocity *= 0.33f;
		}
		Owner.itemAnimation = 0;
		Owner.Calamity().LungingDown = false;
		base.Projectile.active = false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		if (Owner.itemAnimation > 0)
		{
			Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
			float r = ((!FlipAsSword) ? 0f : ((State == 1f) ? ((float)Math.PI) : ((float)Math.PI / 2f)));
			if (base.Projectile.numHits > 0)
			{
				for (int i = 0; i < 5; i++)
				{
					Color outlineColor = Color.Lerp(outlineColorGreen, outlineColorBlue, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 1.5f)) * MathHelper.Clamp((float)base.Projectile.numHits / 3f, 0f, 1f);
					Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/MendedBiomeBlade_TruePureClarityGhost", (AssetRequestMode)2).Value;
					Vector2 rotationalDrawOffset = ((float)Math.PI * 2f * (float)i / 7f + Main.GlobalTimeWrappedHourly * 17f).ToRotationVector2();
					rotationalDrawOffset *= MathHelper.Lerp(3.25f, 6f, (float)Math.Cos(Main.GlobalTimeWrappedHourly * 4f) * 0.5f + 1.5f);
					Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition + rotationalDrawOffset + new Vector2(0f, Owner.gfxOffY), null, outlineColor, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects != 0) ? ((int)spriteEffects) : (FlipAsSword ? 1 : 0)));
				}
			}
			Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY), tex.Frame(1, FrameCount, 0, Frame), lightColor, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects == 0) ? (FlipAsSword ? 1 : 0) : ((int)spriteEffects)));
		}
		return false;
	}

	public override void ResetStyle()
	{
	}

	public TruePureClarity()
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		doSwing = true;
		dashDirection = Vector2.Zero;
		base._002Ector();
	}
}
