using System;
using System.Collections.Generic;
using CalamityMod.CalPlayer;
using CalamityMod.Dusts;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class Luxor : ModProjectile, ILocalizedModType, IModType
{
	public Color usedColor;

	public DamageClass lastDamageClass;

	public int classType;

	public float fxFade;

	public float postFireBoost;

	public float followSpeed;

	public bool rogueChain;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public Player Owner => Main.player[base.Projectile.owner];

	public CalamityPlayer moddedOwner => Owner.Calamity();

	public ref float time => ref base.Projectile.ai[0];

	public ref float attackTimer => ref base.Projectile.ai[1];

	public ref float idleTimer => ref base.Projectile.localAI[0];

	public Vector2 tipPosition
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center + (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2() * 15f;
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 114;
		base.Projectile.height = 38;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08af: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_08da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0903: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0478: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_078f: Unknown result type (might be due to invalid IL or missing references)
		//IL_079a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_0589: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		//IL_0631: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_0663: Unknown result type (might be due to invalid IL or missing references)
		Color classColor = Color.White;
		DamageClass itemClass = Owner.HeldItem.DamageType;
		if ((Owner.HeldItem.axe > 0 || Owner.HeldItem.hammer > 0 || Owner.HeldItem.pick > 0) && lastDamageClass != null)
		{
			itemClass = lastDamageClass;
		}
		if (itemClass == null)
		{
			base.Projectile.Kill();
			return;
		}
		if (itemClass.CountsAsClass<MeleeDamageClass>())
		{
			classColor = Color.Red;
			classType = 1;
		}
		else if (itemClass.CountsAsClass<RangedDamageClass>())
		{
			classColor = Color.Cyan;
			classType = 2;
		}
		else if (itemClass.CountsAsClass<MagicDamageClass>())
		{
			classColor = Color.Gold;
			classType = 3;
		}
		else if (itemClass.CountsAsClass<SummonDamageClass>())
		{
			classColor = Color.Lime;
			classType = 4;
		}
		else if (itemClass.CountsAsClass<RogueDamageClass>())
		{
			classColor = Color.Magenta;
			classType = 5;
		}
		else
		{
			classColor = Color.Gray;
			classType = 0;
		}
		if (time == 0f || itemClass != lastDamageClass)
		{
			base.Projectile.netUpdate = true;
			attackTimer = 60f;
		}
		lastDamageClass = itemClass;
		float rate = Main.GlobalTimeWrappedHourly * 7f;
		List<Color> eColors = new List<Color>
		{
			Color.Lerp(classColor, Color.White, 0.15f),
			Color.Lerp(classColor, Color.Black, 0.15f)
		};
		int colorIndex = (int)(rate / 2f % (float)eColors.Count);
		Color currentColor = eColors[colorIndex];
		Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
		usedColor = Color.Lerp(currentColor, nextColor, (rate % 2f > 1f) ? 1f : (rate % 1f));
		base.Projectile.velocity = base.Projectile.Center.DirectionTo(Owner.Calamity().mouseWorld);
		Vector2 destination = Vector2.Lerp(Owner.Center - base.Projectile.velocity * (70f + 100f * Utils.GetLerpValue(0f, 110f, attackTimer, clamped: true)), Owner.Center + Vector2.UnitY * -40f, Utils.GetLerpValue(280f, 300f, idleTimer, clamped: true));
		base.Projectile.velocity = (destination - base.Projectile.Center) / followSpeed;
		base.Projectile.rotation = base.Projectile.Center.DirectionTo(Owner.Calamity().mouseWorld).ToRotation() + (float)Math.PI / 2f;
		fxFade = (moddedOwner.luxorsGiftVanity ? 1f : ((float)Math.Pow(Utils.GetLerpValue(30f, 0f, attackTimer, clamped: true), 3.0) + postFireBoost));
		if (idleTimer >= 280f)
		{
			if (moddedOwner.luxorHit)
			{
				if (idleTimer > 320f)
				{
					idleTimer = 320f;
				}
				idleTimer--;
			}
			else
			{
				fxFade = 1f;
				attackTimer = 40f;
			}
		}
		Color newColor;
		if (!moddedOwner.luxorsGiftVanity)
		{
			if (attackTimer == 0f && moddedOwner.luxorHit)
			{
				int attackSpeed = 0;
				int attackDamage = 0;
				int projType = 0;
				switch (classType)
				{
				case 0:
					attackSpeed = 50;
					attackDamage = 9;
					projType = ModContent.ProjectileType<LuxorsGiftClassless>();
					break;
				case 1:
					attackSpeed = 100;
					attackDamage = 10;
					projType = ModContent.ProjectileType<LuxorsGiftMelee>();
					break;
				case 2:
					attackSpeed = 25;
					attackDamage = 5;
					projType = ModContent.ProjectileType<LuxorsGiftRanged>();
					break;
				case 3:
					attackSpeed = 75;
					attackDamage = 13;
					projType = ModContent.ProjectileType<LuxorsGiftMagic>();
					break;
				case 4:
					attackSpeed = 140;
					attackDamage = 22;
					projType = ModContent.ProjectileType<LuxorsGiftSummon>();
					break;
				case 5:
					attackSpeed = 36;
					attackDamage = 8;
					projType = ModContent.ProjectileType<LuxorsGiftRogue>();
					break;
				}
				float powerMult = Utils.GetLerpValue(-120f, 140f, attackSpeed, clamped: true);
				for (int i = 0; i < (int)(20f * powerMult); i++)
				{
					Vector2 dustVel = (base.Projectile.rotation - (float)Math.PI / 2f).ToRotationVector2().RotatedByRandom(0.5) * 12f * powerMult;
					Vector2 position = tipPosition;
					int type = ModContent.DustType<LightDust>();
					Vector2? velocity = dustVel * Main.rand.NextFloat(0.5f, 1.2f);
					newColor = default(Color);
					Dust dust = Dust.NewDustPerfect(position, type, velocity, 0, newColor);
					dust.noGravity = !Main.rand.NextBool(3);
					dust.scale = Main.rand.NextFloat(0.75f, 1.4f) * powerMult;
					dust.color = usedColor;
					dust.noLightEmittence = true;
				}
				if (classType == 1)
				{
					for (int j = -1; j <= 1; j++)
					{
						Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), tipPosition, tipPosition.DirectionTo(Owner.Calamity().mouseWorld).RotatedBy(-0.12f * (float)j * Main.rand.NextFloat(0.7f, 1f)) * (12f - Main.rand.NextFloat(0f, 0.6f) - (float)Math.Abs(j)), projType, (int)Owner.GetDamage(itemClass).ApplyTo(attackDamage), 0f, Owner.whoAmI).ArmorPenetration = 35;
					}
					for (int k = 0; k < 5; k++)
					{
						Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), tipPosition, tipPosition.DirectionTo(Owner.Calamity().mouseWorld).RotatedByRandom(0.4000000059604645) * (12f - Main.rand.NextFloat(2f, 5f)), projType, (int)Owner.GetDamage(itemClass).ApplyTo(attackDamage), 0f, Owner.whoAmI, 5f).timeLeft = 95;
					}
				}
				else
				{
					Projectile proj = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), tipPosition, tipPosition.DirectionTo(Owner.Calamity().mouseWorld) * 12f, projType, (int)Owner.GetDamage(itemClass).ApplyTo(attackDamage), 0f, Owner.whoAmI);
					proj.ArmorPenetration = 35;
					if (classType == 5)
					{
						proj.velocity.Y -= 1.5f;
					}
				}
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/GunShotSmall");
				style.Volume = 0.35f;
				style.Pitch = Main.rand.NextFloat(0.6f, 0.8f);
				SoundEngine.PlaySound(in style, tipPosition);
				SoundEngine.PlaySound(SoundID.DD2_WitherBeastCrystalImpact with
				{
					Volume = 0.7f,
					Pitch = 0.2f
				}, tipPosition);
				if (classType == 5 && rogueChain)
				{
					rogueChain = false;
					attackTimer = attackSpeed / 3;
				}
				else
				{
					attackTimer = attackSpeed;
					moddedOwner.luxorHit = false;
					if (!CalamityClientConfig.Instance.Photosensitivity)
					{
						postFireBoost = 1.7f;
					}
					idleTimer = 0f;
					rogueChain = true;
				}
				base.Projectile.netUpdate = true;
			}
			if (attackTimer > 0f)
			{
				attackTimer--;
			}
			if (postFireBoost > 0f)
			{
				postFireBoost -= 0.2f;
			}
		}
		_ = time % 2f;
		_ = 0f;
		float idleFade = Utils.GetLerpValue(280f, 300f, idleTimer, clamped: true);
		Vector2 position2 = tipPosition;
		newColor = Color.Lerp(usedColor, Color.Lerp(usedColor, Color.White, 0.5f), idleFade);
		Lighting.AddLight(position2, ((Color)(ref newColor)).ToVector3() * (0.5f * MathHelper.Lerp(fxFade, 1f, 0.55f) + 0.35f * idleFade));
		if (moddedOwner.luxorsGift || moddedOwner.luxorsGiftVanity)
		{
			base.Projectile.timeLeft++;
		}
		else
		{
			base.Projectile.Kill();
		}
		if (Owner.dead)
		{
			base.Projectile.Kill();
		}
		time++;
		if (!moddedOwner.luxorHit && !moddedOwner.luxorsGiftVanity)
		{
			idleTimer++;
		}
		else if (moddedOwner.luxorsGiftVanity)
		{
			idleTimer = 0f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		Texture2D bTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/BloomCircle", (AssetRequestMode)2).Value;
		Texture2D cTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/Light", (AssetRequestMode)2).Value;
		float idleFade = (moddedOwner.luxorsGiftVanity ? 0f : ((float)Math.Pow(Utils.GetLerpValue(280f, 300f, idleTimer, clamped: true), 5.0)));
		Color drawColor = usedColor;
		Color val = lightColor;
		Color val2 = Color.Gold;
		((Color)(ref val2)).A = 0;
		Color bodyColor = Color.Lerp(val, val2, idleFade);
		float drawMult = Math.Max(idleFade, fxFade);
		if (idleFade == 0f)
		{
			for (int i = 0; i < 18; i++)
			{
				Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 18f).ToRotationVector2() * 6f * drawMult;
				Texture2D value = tex.Value;
				Vector2 position = base.Projectile.Center - Main.screenPosition + drawOffset;
				val2 = drawColor;
				((Color)(ref val2)).A = 0;
				Main.EntitySpriteDraw(value, position, null, val2 * 0.2f * drawMult, base.Projectile.rotation, tex.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
			}
		}
		Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition, null, bodyColor * (1f - idleFade), base.Projectile.rotation, tex.Size() * 0.5f, new Vector2(1f - 0.85f * idleFade, 1f) * base.Projectile.scale, (SpriteEffects)0);
		for (int j = 0; j < 4; j++)
		{
			Vector2 position2 = Vector2.Lerp(tipPosition, base.Projectile.Center, idleFade) - Main.screenPosition;
			val2 = drawColor;
			((Color)(ref val2)).A = 0;
			Main.EntitySpriteDraw(bTexture, position2, null, val2 * 0.35f * drawMult, time * 0.13f * (float)(j + 1), bTexture.Size() * 0.5f, new Vector2(1f + (float)j * 0.15f, (float)j - 0.3f * (float)j) * base.Projectile.scale * MathHelper.Lerp(0.33f, 0.22f, idleFade) * drawMult, (SpriteEffects)0);
		}
		Vector2 position3 = Vector2.Lerp(tipPosition, base.Projectile.Center, idleFade) - Main.screenPosition;
		val2 = Color.White;
		((Color)(ref val2)).A = 0;
		Main.EntitySpriteDraw(cTexture, position3, null, val2 * 0.75f * drawMult, base.Projectile.rotation, cTexture.Size() * 0.5f, base.Projectile.scale * MathHelper.Lerp(0.4f, 0.3f, idleFade) * drawMult, (SpriteEffects)0);
		return false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public Luxor()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		usedColor = Color.White;
		followSpeed = 6f;
		rogueChain = true;
		base._002Ector();
	}
}
