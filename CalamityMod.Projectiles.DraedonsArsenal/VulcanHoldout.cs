using System;
using CalamityMod.Cooldowns;
using CalamityMod.Dusts;
using CalamityMod.Effects;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class VulcanHoldout : BaseGunHoldoutProjectile
{
	public float offsetBase = 35f;

	public int time;

	public int maxShots = 25;

	public int shotsToFire;

	public bool playRampDown;

	public int shotsFired;

	public float heatVis;

	public float flashVis1;

	public float flashVis2;

	public float spearVis = 1f;

	public int cooldownGiven = 180;

	public bool failedManaCheck;

	public int spearFakeCooldown;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override int AssociatedItemID => ModContent.ItemType<Vulcan>();

	public override float MaxOffsetLengthFromArm => offsetBase;

	public override float RecoilResolveSpeed => 0.2f;

	public override float OffsetXUpwards => 0f;

	public override float BaseOffsetY => 0f;

	public override float OffsetYDownwards => 0f;

	public override float WeaponTurnSpeed
	{
		get
		{
			if (!(shootingTimer < 0f))
			{
				return 0.35f;
			}
			return 0f;
		}
	}

	public bool firingSpear => base.Projectile.ai[2] == 5f;

	public ref float shootingTimer => ref base.Projectile.ai[0];

	public ref float fireRate => ref base.Projectile.ai[1];

	public override void KillHoldoutLogic()
	{
	}

	public override void HoldoutAI()
	{
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		if (base.Owner.dead)
		{
			base.Projectile.Kill();
			return;
		}
		if (flashVis1 > 0f)
		{
			flashVis1 -= 0.2f;
		}
		if (flashVis1 < 0f)
		{
			flashVis1 = 0f;
		}
		if (flashVis2 > 0f)
		{
			flashVis2 -= 0.2f;
		}
		if (flashVis2 < 0f)
		{
			flashVis2 = 0f;
		}
		if (time == 0)
		{
			if (base.Owner.Calamity().arsenalCooldown > 0)
			{
				spearVis = 0f;
			}
			shootingTimer = -20f;
			fireRate = base.Owner.HeldItem.useAnimation;
			shotsToFire = maxShots;
			base.OffsetLengthFromArm = offsetBase;
		}
		if (shootingTimer < 0f)
		{
			if ((shootingTimer == -20f || shootingTimer == -1f) && ((!Main.mouseLeft && !firingSpear) || failedManaCheck))
			{
				base.Projectile.Kill();
				return;
			}
			if (playRampDown)
			{
				SoundStyle sound = new SoundStyle("CalamityMod/Sounds/Item/VulcanRampDown");
				for (int i = 0; i < 2; i++)
				{
					SoundEngine.PlaySound(sound with
					{
						Volume = 0.9f,
						MaxInstances = -1
					}, base.Projectile.Center);
				}
				playRampDown = false;
			}
			if (shootingTimer == -20f)
			{
				SoundStyle sound2 = new SoundStyle("CalamityMod/Sounds/Item/VulcanRampUp");
				for (int j = 0; j < 2; j++)
				{
					SoundEngine.PlaySound(sound2 with
					{
						Volume = 0.9f,
						MaxInstances = -1
					}, base.Projectile.Center);
				}
			}
			if (shootingTimer == -1f)
			{
				if (!firingSpear)
				{
					shootingTimer = fireRate;
				}
				else
				{
					shootingTimer = 0f;
				}
				shotsToFire = maxShots;
				fireRate = base.Owner.HeldItem.useAnimation;
				shotsFired = 0;
				playRampDown = true;
			}
			base.Projectile.velocity = base.Owner.Center.DirectionTo(base.Owner.Calamity().mouseWorld);
		}
		if (base.Owner.Calamity().mouseRight && base.Owner.Calamity().arsenalCooldown <= 0)
		{
			base.Projectile.ai[2] = 5f;
		}
		if (firingSpear && time > 20 && spearFakeCooldown <= 0)
		{
			Shoot(isSpear: true);
			spearFakeCooldown = 5;
			shotsToFire = maxShots;
			base.Projectile.ai[2] = 0f;
			base.Owner.Calamity().arsenalCooldown = cooldownGiven;
			base.Owner.AddCooldown(ArsenalPower.ID, cooldownGiven);
		}
		if (shootingTimer >= 0f)
		{
			if (!Main.mouseLeft && !firingSpear)
			{
				shotsToFire = 0;
			}
			if (!firingSpear)
			{
				if (shotsToFire <= 0)
				{
					shootingTimer = -50f;
				}
				else if (shootingTimer >= fireRate)
				{
					if (base.Owner.CheckMana(base.HeldItem, -1, pay: true))
					{
						Shoot(isSpear: false);
						if (shotsFired % 2 == 0)
						{
							flashVis1 = 1f;
						}
						else
						{
							flashVis2 = 1f;
						}
						shootingTimer = -1f;
						if ((float)shotsToFire > (float)maxShots * 0.2f)
						{
							fireRate--;
							if (fireRate < 3f)
							{
								fireRate = 3f;
							}
						}
						else if (fireRate < (float)base.Owner.HeldItem.useAnimation)
						{
							fireRate++;
						}
						if (fireRate > (float)base.Owner.HeldItem.useAnimation)
						{
							fireRate = base.Owner.HeldItem.useAnimation;
						}
					}
					else
					{
						failedManaCheck = true;
						shotsToFire = 0;
					}
					shotsToFire--;
					shotsFired++;
					if (shotsToFire <= 0)
					{
						shootingTimer = -50f;
					}
				}
			}
		}
		if (shotsFired > 0 && shotsToFire > 0)
		{
			heatVis = MathHelper.Lerp(heatVis, 1f, 0.007f);
		}
		else
		{
			heatVis = MathHelper.Lerp(heatVis, 0f, 0.085f);
		}
		spearVis = Utils.GetLerpValue(cooldownGiven / 2, 0f, base.Owner.Calamity().arsenalCooldown, clamped: true);
		spearFakeCooldown--;
		time++;
		shootingTimer++;
	}

	public void Shoot(bool isSpear)
	{
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style;
		if (isSpear)
		{
			base.Owner.SetScreenshake(3.5f);
			base.OffsetLengthFromArm -= 16f;
			style = new SoundStyle("CalamityMod/Sounds/Item/GunShotHeavy");
			style.Volume = 0.8f;
			style.Pitch = 0.8f;
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			if (Main.myPlayer == base.Projectile.owner)
			{
				Vector2 spearPos = GunTipPosition + base.Projectile.velocity.RotatedBy((float)Math.PI / 2f * (float)base.Projectile.direction) * 12f - base.Projectile.velocity * 8f;
				Vector2 shootVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.0) * 6f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spearPos, shootVelocity, ModContent.ProjectileType<VulcanSpear>(), base.Projectile.damage * 5, 0f, base.Projectile.owner);
			}
			return;
		}
		base.Owner.SetScreenshake(1f);
		base.OffsetLengthFromArm -= 7f;
		style = new SoundStyle("CalamityMod/Sounds/Item/VulcanShot");
		style.Volume = 0.6f;
		style.Pitch = Main.rand.NextFloat(-0.1f, 0f);
		style.MaxInstances = 1;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		Vector2 shootVelocity2 = base.Projectile.velocity.SafeNormalize(Vector2.UnitX).RotatedByRandom(0.0) * 6f;
		Vector2 position = GunTipPosition + base.Projectile.velocity.RotatedBy((float)Math.PI / 2f * (float)((shotsFired % 2 != 0) ? 1 : (-1))) * 4f;
		if (Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), position, shootVelocity2, ModContent.ProjectileType<VulcanProjectile>(), base.Projectile.damage, 0f, base.Projectile.owner);
		}
		for (int i = 0; i < 4; i++)
		{
			Dust dust = Dust.NewDustPerfect(position, Main.rand.NextBool(3) ? ModContent.DustType<SquashDust>() : ArsenalEffects.ArsenalGaussDust, (shootVelocity2 * 4f).RotatedByRandom(0.05000000074505806) * Main.rand.NextFloat(0.1f, 0.8f), 0, default(Color), Main.rand.NextFloat(0.7f, 1.1f));
			dust.noGravity = true;
			dust.color = ArsenalEffects.ArsenalGaussColor;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_047f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_049e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_050e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Unknown result type (might be due to invalid IL or missing references)
		//IL_060e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0616: Unknown result type (might be due to invalid IL or missing references)
		//IL_0620: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_065f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0664: Unknown result type (might be due to invalid IL or missing references)
		if (time < 2)
		{
			return false;
		}
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/VulcanHoldout", (AssetRequestMode)2).Value;
		Texture2D glow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/VulcanHoldoutGlow", (AssetRequestMode)2).Value;
		Texture2D flash = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/VulcanFlash", (AssetRequestMode)2).Value;
		Texture2D spear = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/VulcanSpear", (AssetRequestMode)2).Value;
		Texture2D spearGlow = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/VulcanSpearGlow", (AssetRequestMode)2).Value;
		Texture2D heat = ModContent.Request<Texture2D>("CalamityMod/Projectiles/DraedonsArsenal/VulcanHoldoutHeatedTip", (AssetRequestMode)2).Value;
		_ = base.Owner.Calamity().arsenalCooldown;
		_ = 0;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Vector2 vel = base.Projectile.rotation.ToRotationVector2();
		Main.rand.NextFloat(0.9f, 1f);
		Color drawColor = base.Projectile.GetAlpha(lightColor);
		float drawRotation = base.Projectile.rotation + ((base.Projectile.direction == -1) ? ((float)Math.PI) : 0f);
		Vector2 rotationPoint = texture.Size() * 0.5f;
		SpriteEffects flipSprite = (SpriteEffects)(base.Projectile.direction == -1);
		int draws = 8;
		Vector2 spearPos = GunTipPosition + vel.RotatedBy((float)Math.PI / 2f * (float)base.Projectile.direction) * 12f - vel * 27f - Main.screenPosition;
		Color val;
		for (int i = 0; i < draws; i++)
		{
			float fadeIn = Math.Min(Utils.GetLerpValue(0f, 0.8f, spearVis), (float)Math.Pow(Utils.GetLerpValue(1f, 0.8f, spearVis), 2.0));
			Vector2 offset = ((float)Math.PI * 2f / (float)draws * (float)i).ToRotationVector2() * 6f * (1f - spearVis);
			Vector2 position = spearPos + offset;
			val = ArsenalEffects.ArsenalGaussColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(spear, position, null, val * fadeIn * 0.5f, drawRotation, spear.Size() * 0.5f, base.Projectile.scale, flipSprite);
			val = ArsenalEffects.ArsenalGaussColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(spear, spearPos, null, Color.Lerp(val, drawColor, spearVis) * spearVis, drawRotation, spear.Size() * 0.5f, base.Projectile.scale, flipSprite);
		}
		Main.EntitySpriteDraw(spearGlow, spearPos, null, Color.White * spearVis, drawRotation, spearGlow.Size() * 0.5f, base.Projectile.scale, flipSprite);
		Main.EntitySpriteDraw(texture, drawPosition, null, drawColor, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		Main.EntitySpriteDraw(glow, drawPosition, null, Color.White, drawRotation, rotationPoint, base.Projectile.scale, flipSprite);
		for (int j = 0; j < draws; j++)
		{
			Vector2 offset2 = ((float)Math.PI * 2f / (float)draws * (float)j).ToRotationVector2() * 4f * heatVis;
			Vector2 position2 = drawPosition + offset2;
			val = ArsenalEffects.ArsenalGaussColor;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(heat, position2, null, val * heatVis * 0.35f, drawRotation, heat.Size() * 0.5f, base.Projectile.scale, flipSprite);
			val = Color.White;
			((Color)(ref val)).A = 0;
			Main.EntitySpriteDraw(heat, drawPosition, null, val * heatVis * 0.3f, drawRotation, heat.Size() * 0.5f, base.Projectile.scale, flipSprite);
		}
		float tipSeperation = 4f;
		if (flashVis1 > 0f)
		{
			Vector2 position3 = GunTipPosition + vel * (12f + 30f * (1f - flashVis1)) + vel.RotatedBy(1.5707963705062866) * tipSeperation - Main.screenPosition;
			for (int k = 0; k < 3; k++)
			{
				val = Color.White;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(flash, position3, null, val * flashVis1, drawRotation, flash.Size() * 0.5f, new Vector2(0.5f + 2f * (1f - flashVis1), 1f - 0.8f * flashVis1) * base.Projectile.scale, flipSprite);
			}
		}
		if (flashVis2 > 0f)
		{
			Vector2 position4 = GunTipPosition + vel * (12f + 30f * (1f - flashVis2)) + vel.RotatedBy(-1.5707963705062866) * tipSeperation - Main.screenPosition;
			for (int l = 0; l < 3; l++)
			{
				val = Color.White;
				((Color)(ref val)).A = 0;
				Main.EntitySpriteDraw(flash, position4, null, val * flashVis2, drawRotation, flash.Size() * 0.5f, new Vector2(0.5f + 2f * (1f - flashVis2), 1f - 0.8f * flashVis2) * base.Projectile.scale, flipSprite);
			}
		}
		return false;
	}
}
