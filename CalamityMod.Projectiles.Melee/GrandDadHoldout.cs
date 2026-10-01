using System;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs;
using CalamityMod.NPCs.PrimordialWyrm;
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
public class GrandDadHoldout : BaseCustomUseStyleProjectile, ILocalizedModType, IModType
{
	public Vector2 mousePos;

	public Vector2 aimVel;

	public bool doSwing = true;

	public bool postSwing;

	public float fadeIn;

	public int useAnim;

	public int swingCount;

	public bool finalFlip;

	public bool swingSound = true;

	public int armoredHits;

	public override int AssignedItemID => ModContent.ItemType<GrandDad>();

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<GrandDad>();

	public override string Texture => "CalamityMod/Items/Weapons/Melee/GrandDad";

	public override float HitboxOutset => 125f;

	public override Vector2 HitboxSize
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(190f, 190f);
		}
	}

	public override float HitboxRotationOffset => MathHelper.ToRadians(-45f);

	public override Vector2 SpriteOrigin
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(0f, 124f);
		}
	}

	private NPC target => Main.npc[(int)base.Projectile.ai[0]];

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.DamageType = TrueMeleeDamageClass.Instance;
	}

	public override void WhenSpawned()
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.timeLeft = Owner.HeldItem.useAnimation + 1;
		base.Projectile.knockBack = 0f;
		base.Projectile.scale = 1f;
		base.Projectile.ai[1] = -1f;
		mousePos = Owner.Calamity().mouseWorld;
		aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
		useAnim = Owner.itemAnimationMax;
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
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04db: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_054a: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_056b: Unknown result type (might be due to invalid IL or missing references)
		//IL_079a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_07dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_081c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0821: Unknown result type (might be due to invalid IL or missing references)
		//IL_082c: Unknown result type (might be due to invalid IL or missing references)
		//IL_083b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0854: Unknown result type (might be due to invalid IL or missing references)
		//IL_085a: Unknown result type (might be due to invalid IL or missing references)
		//IL_085c: Unknown result type (might be due to invalid IL or missing references)
		//IL_086a: Unknown result type (might be due to invalid IL or missing references)
		//IL_086f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0874: Unknown result type (might be due to invalid IL or missing references)
		//IL_067c: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0703: Unknown result type (might be due to invalid IL or missing references)
		//IL_070e: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0736: Unknown result type (might be due to invalid IL or missing references)
		//IL_073c: Unknown result type (might be due to invalid IL or missing references)
		//IL_073e: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0751: Unknown result type (might be due to invalid IL or missing references)
		//IL_0756: Unknown result type (might be due to invalid IL or missing references)
		//IL_0772: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_089d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0913: Unknown result type (might be due to invalid IL or missing references)
		//IL_0929: Unknown result type (might be due to invalid IL or missing references)
		//IL_092f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0931: Unknown result type (might be due to invalid IL or missing references)
		//IL_0936: Unknown result type (might be due to invalid IL or missing references)
		//IL_093e: Unknown result type (might be due to invalid IL or missing references)
		//IL_094d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0966: Unknown result type (might be due to invalid IL or missing references)
		//IL_096c: Unknown result type (might be due to invalid IL or missing references)
		//IL_096e: Unknown result type (might be due to invalid IL or missing references)
		//IL_097c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0981: Unknown result type (might be due to invalid IL or missing references)
		//IL_098b: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_09f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_09fa: Unknown result type (might be due to invalid IL or missing references)
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
			fadeIn = MathHelper.Lerp(fadeIn, 1f, 0.3f);
		}
		else
		{
			fadeIn = MathHelper.Lerp(fadeIn, 0f, 0.35f);
		}
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
			doSwing = true;
			swingCount++;
			finalFlip = false;
			swingSound = true;
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
			if (AnimationProgress < (float)useAnim / 1.5f)
			{
				aimVel = (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitX) * 65f;
				CanHit = false;
				postSwing = false;
				if (AnimationProgress == 0f)
				{
					doSwing = false;
					base.Projectile.ai[1] = 0f - base.Projectile.ai[1];
				}
				RotationOffset = MathHelper.Lerp(RotationOffset, MathHelper.ToRadians(120f * base.Projectile.ai[1] * (float)Owner.direction * (1f + Utils.GetLerpValue((float)useAnim * 0.8f, useAnim, Animation, clamped: true) * 0.35f)), 0.2f);
			}
			else
			{
				if (!finalFlip)
				{
					FlipAsSword = Owner.direction < 0;
				}
				float time = AnimationProgress - (float)(useAnim / 3);
				float timeMax = useAnim - useAnim / 3;
				if (time >= (float)(int)(timeMax * 0.4f) && swingSound)
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/HeavySwing");
					style.Volume = 0.8f;
					style.Pitch = Main.rand.NextFloat(0.2f, 0.32f);
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					swingSound = false;
				}
				if (time > (float)(int)(timeMax * 0.4f) && time < (float)(int)(timeMax * 0.7f))
				{
					CanHit = true;
					for (int j = 0; j < 3; j++)
					{
						Vector2 particleVel = Utils.RotatedBy(new Vector2(0f, 10f * (0f - base.Projectile.ai[1]) * (float)Owner.direction), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2));
						Vector2 val = Owner.Center + Utils.RotatedBy(new Vector2((float)Main.rand.Next(30, 165), 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2));
						GeneralParticleHandler.SpawnParticle(new LineParticle(val, -particleVel.RotatedByRandom(0.20000000298023224) * 2f, affectedByGravity: false, 19, Main.rand.NextFloat(0.5f, 1f), Main.rand.NextBool(4) ? Color.DodgerBlue : Color.Blue));
						GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(val, -particleVel.RotatedByRandom(0.20000000298023224) * 2f, Main.rand.NextBool(4) ? Color.Black : Color.DarkBlue, 23, Main.rand.NextFloat(0.5f, 1f), 0.65f));
					}
				}
				else
				{
					CanHit = false;
				}
				RotationOffset = MathHelper.Lerp(RotationOffset, MathHelper.ToRadians(MathHelper.Lerp(150f * base.Projectile.ai[1] * (float)Owner.direction, 120f * (0f - base.Projectile.ai[1]) * (float)Owner.direction, CalamityUtils.ExpInOutEasing(time / timeMax, 1))), 0.2f);
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
					for (int k = 0; k < 6; k++)
					{
						if (Main.rand.NextBool(3))
						{
							Dust dust = Dust.NewDustPerfect(Owner.Center + Utils.RotatedBy(new Vector2(180f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)).RotatedByRandom(0.30000001192092896), ModContent.DustType<VoidDust>(), Vector2.Zero, 0, default(Color), Main.rand.NextFloat(1.15f, 1.5f));
							dust.noGravity = true;
							dust.color = Color.DodgerBlue;
							GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(Owner.Center + Utils.RotatedBy(new Vector2(180f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)).RotatedByRandom(0.30000001192092896), Vector2.Zero, affectedByGravity: false, 23, Main.rand.NextFloat(0.5f, 1f), Color.Black, AddativeBlend: false, needed: false, GlowCenter: false));
						}
						else
						{
							Dust dust2 = Dust.NewDustPerfect(Owner.Center + Utils.RotatedBy(new Vector2(180f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)).RotatedByRandom(0.30000001192092896), ModContent.DustType<LightDust>(), Vector2.Zero, 0, default(Color), Main.rand.NextFloat(1.15f, 1.5f));
							dust2.noGravity = true;
							dust2.color = Color.Blue;
							GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(Owner.Center + Utils.RotatedBy(new Vector2(180f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)).RotatedByRandom(0.30000001192092896), Vector2.Zero, affectedByGravity: false, 23, Main.rand.NextFloat(0.5f, 1f), Main.rand.NextBool(4) ? Color.DodgerBlue : Color.Blue));
						}
					}
					for (int l = 0; l < 3; l++)
					{
						float randRot = Main.rand.NextFloat(-30f, -60f);
						Vector2 dustVel = Utils.RotatedBy(new Vector2(0f, 15f * (0f - base.Projectile.ai[1]) * (float)Owner.direction), (double)(base.FinalRotation + MathHelper.ToRadians(randRot)), default(Vector2));
						Dust dust3 = Dust.NewDustPerfect(Owner.Center + Utils.RotatedBy(new Vector2(185f, 0f), (double)(base.FinalRotation + MathHelper.ToRadians(-45f)), default(Vector2)).RotatedByRandom(0.30000001192092896), 278, dustVel * Main.rand.NextFloat(0.1f, 0.5f));
						dust3.scale = Main.rand.NextFloat(0.55f, 1.05f);
						dust3.noGravity = true;
						dust3.color = (Main.rand.NextBool(3) ? Color.Goldenrod : Color.Gold);
					}
				}
			}
		}
		ArmRotationOffset = MathHelper.ToRadians(-140f);
		ArmRotationOffsetBack = MathHelper.ToRadians(-140f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0426: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_046d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		if (target.life <= 0 && target.realLife == -1 && base.Projectile.numHits > 0)
		{
			base.Projectile.numHits--;
		}
		if (damageDone <= 2)
		{
			armoredHits++;
		}
		if (Main.zenithWorld && base.Projectile.numHits == 0 && target.type != ModContent.NPCType<PrimordialWyrmHead>() && Main.rand.NextBool(5))
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/GFBScreams/Scream", 8);
			style.Volume = 0.8f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		if (base.Projectile.numHits == 0)
		{
			Owner.SetScreenshake(6.5f);
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/NPCHit/ThanatosHitOpen1");
			style.Volume = 0.75f;
			style.Pitch = -0.1f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			style = new SoundStyle("CalamityMod/Sounds/Item/FinalDawnSlash");
			style.Volume = 0.65f;
			style.Pitch = Main.rand.NextFloat(-0.2f, -0.3f);
			SoundEngine.PlaySound(in style, base.Projectile.Center);
		}
		int heal = MathHelper.Clamp(20 - base.Projectile.numHits * 12, 1, 20);
		if (base.Projectile.numHits < 10)
		{
			Owner.DoLifestealDirect(target, heal, 0.5f);
		}
		if (target.CanBeMoved(ignoreKBImmune: true) || Main.zenithWorld || target.type == ModContent.NPCType<PrimordialWyrmHead>() || (DownedBossSystem.downedCalamitas && DownedBossSystem.downedExoMechs))
		{
			if (target.type == ModContent.NPCType<PrimordialWyrmHead>())
			{
				CombatText.NewText(target.Hitbox, Color.Aqua, CalamityUtils.GetTextValue("Misc.HecBoop"));
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/SnootBooped");
				style.Pitch = Main.rand.NextFloat(-0.15f, 0.15f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
			bool rightClicked = Owner.Calamity().mouseRight;
			target.noTileCollide = false;
			Vector2 launchVel = Owner.Center.DirectionTo(Owner.Calamity().mouseWorld);
			float launchPower = 30 * ((!rightClicked) ? 1 : 2);
			target.MoveNPC(launchVel, launchPower * 0.5f, ignoreKBImmune: true);
			target.knockBackResist = 1f;
			float damageMults = ((!DownedBossSystem.downedCalamitas || !DownedBossSystem.downedExoMechs) ? 1 : 5) * ((!rightClicked) ? 1 : 3);
			int damage = (int)((float)base.Projectile.damage * damageMults);
			target.GetGlobalNPC<CalamityTileCollisionHarmNPC>().ApplyCollisionDamage(target, Owner, damage, launchVel * launchPower, 5f, checkTiles: true);
		}
		if (base.Projectile.numHits < 3)
		{
			GeneralParticleHandler.SpawnParticle(new VoidSparkParticle(target.Center, (Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitY) * (float)(-45 + base.Projectile.numHits * 5), affectedByGravity: false, 16 - base.Projectile.numHits * 3, 0.6f - (float)base.Projectile.numHits * 0.15f, Color.DodgerBlue, 0.45f));
		}
		for (int i = 0; i < MathHelper.Clamp(10 - base.Projectile.numHits * 2, 2, 10); i++)
		{
			GeneralParticleHandler.SpawnParticle(new SparkParticle(target.Center, ((Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitY) * -35f).RotatedByRandom(0.7) * Main.rand.NextFloat(0.2f, 1f), affectedByGravity: false, 55, Main.rand.NextFloat(0.4f, 1.5f), Main.rand.NextBool(4) ? Color.DodgerBlue : Color.Blue));
			Dust dust = Dust.NewDustPerfect(target.Center, ModContent.DustType<VoidDust>(), ((Owner.Center - Owner.Calamity().mouseWorld).SafeNormalize(Vector2.UnitY) * -35f).RotatedByRandom(0.7) * Main.rand.NextFloat(0.2f, 1f), 0, default(Color), Main.rand.NextFloat(1.55f, 2.2f));
			dust.noGravity = true;
			dust.color = (Main.rand.NextBool() ? Color.DodgerBlue : Color.Blue);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (Owner.Calamity().mouseRight)
		{
			modifiers.SourceDamage *= 0f;
			modifiers.FinalDamage.Flat = 0.1f;
			return;
		}
		float minMult = 0.5f;
		int hitsToMinMult = 15;
		float damageMult = Utils.Remap(base.Projectile.numHits - armoredHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0375: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		if ((useAnim > 0 || DrawUnconditionally) && Owner.ItemAnimationActive)
		{
			Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
			Asset<Texture2D> glowTex = ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/GrandDadGlow", (AssetRequestMode)2);
			float r = (FlipAsSword ? MathHelper.ToRadians(90f) : 0f);
			Asset<Texture2D> swoosh = ModContent.Request<Texture2D>("CalamityMod/Particles/VerticalSmearLarge", (AssetRequestMode)2);
			Color val;
			if (Animation > (float)useAnim * 0.2f)
			{
				Texture2D value = swoosh.Value;
				Vector2 position = base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY);
				val = Color.DodgerBlue;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(value, position, null, val * fadeIn * 0.65f, base.FinalRotation + MathHelper.ToRadians(45f) + MathHelper.ToRadians((float)((base.Projectile.ai[1] == 1f) ? (-90) : 90)) * (float)(-Owner.direction), swoosh.Size() * 0.5f, base.Projectile.scale * 0.6f, (SpriteEffects)0);
			}
			for (int i = 0; i < 25; i++)
			{
				Texture2D centerTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/GrandDadGhost", (AssetRequestMode)2).Value;
				val = Color.Gold;
				((Color)(ref val)).A = 0;
				Color auraColor = val * 0.15f * fadeIn;
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 25f).ToRotationVector2() * 6f * fadeIn;
				Main.EntitySpriteDraw(centerTexture, base.Projectile.Center - Main.screenPosition + drawOffset + new Vector2(0f, Owner.gfxOffY), centerTexture.Frame(1, FrameCount, 0, Frame), auraColor, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects != 0) ? ((int)spriteEffects) : (FlipAsSword ? 1 : 0)));
			}
			Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY), tex.Frame(1, FrameCount, 0, Frame), lightColor, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)tex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects == 0) ? (FlipAsSword ? 1 : 0) : ((int)spriteEffects)));
			Main.EntitySpriteDraw(glowTex.Value, base.Projectile.Center - Main.screenPosition + new Vector2(0f, Owner.gfxOffY), glowTex.Frame(1, FrameCount, 0, Frame), Color.White, base.Projectile.rotation + RotationOffset + r, (Vector2)(FlipAsSword ? new Vector2((float)glowTex.Width() - SpriteOrigin.X, SpriteOrigin.Y) : SpriteOrigin), base.Projectile.scale, (SpriteEffects)(((int)spriteEffects == 0) ? (FlipAsSword ? 1 : 0) : ((int)spriteEffects)));
		}
		return false;
	}
}
