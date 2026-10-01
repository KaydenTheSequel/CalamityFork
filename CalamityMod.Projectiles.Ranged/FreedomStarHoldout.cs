using System;
using CalamityMod.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class FreedomStarHoldout : ModProjectile
{
	private const float OrbLargeGateValue = 80f;

	private const float LaserGateValue = 180f;

	private const float LaserLargeGateValue = 660f;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<FreedomStar>();

	public override void SetDefaults()
	{
		base.Projectile.width = 54;
		base.Projectile.height = 28;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_095e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0965: Unknown result type (might be due to invalid IL or missing references)
		//IL_0970: Unknown result type (might be due to invalid IL or missing references)
		//IL_097a: Unknown result type (might be due to invalid IL or missing references)
		//IL_097f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0984: Unknown result type (might be due to invalid IL or missing references)
		//IL_0995: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0495: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_036a: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0710: Unknown result type (might be due to invalid IL or missing references)
		//IL_0715: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0541: Unknown result type (might be due to invalid IL or missing references)
		//IL_0543: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0853: Unknown result type (might be due to invalid IL or missing references)
		//IL_085e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0878: Unknown result type (might be due to invalid IL or missing references)
		//IL_087d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0884: Unknown result type (might be due to invalid IL or missing references)
		//IL_0889: Unknown result type (might be due to invalid IL or missing references)
		//IL_088b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0899: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f3: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.ai[0]++;
		int chargeAmt = 0;
		if (base.Projectile.ai[0] >= 80f)
		{
			chargeAmt++;
		}
		if (base.Projectile.ai[0] >= 180f)
		{
			chargeAmt++;
		}
		float lerpAmtForCharge = (base.Projectile.ai[0] - 180f) / 480f;
		if (lerpAmtForCharge > 1f)
		{
			lerpAmtForCharge = 1f;
		}
		bool shootLaser = base.Projectile.ai[0] >= 180f;
		int shootGateValue = 5;
		if (!shootLaser)
		{
			base.Projectile.ai[1]++;
		}
		bool shootThisFrame = false;
		if (base.Projectile.ai[0] == 1f)
		{
			shootThisFrame = true;
		}
		if (shootLaser && base.Projectile.ai[0] % 20f == 0f)
		{
			shootThisFrame = true;
		}
		if ((!shootLaser && base.Projectile.ai[1] >= (float)shootGateValue) | shootLaser)
		{
			if (!shootLaser)
			{
				base.Projectile.ai[1] = 0f;
			}
			shootThisFrame = true;
			float speedScale = player.inventory[player.selectedItem].shootSpeed * base.Projectile.scale;
			Vector2 shootDirection = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - base.Projectile.Center;
			if (player.gravDir == -1f)
			{
				shootDirection.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - base.Projectile.Center.Y;
			}
			Vector2 shootVel = Vector2.Normalize(shootDirection);
			if (float.IsNaN(shootVel.X) || float.IsNaN(shootVel.Y))
			{
				shootVel = -Vector2.UnitY;
			}
			shootVel *= speedScale;
			if (shootVel.X != base.Projectile.velocity.X || shootVel.Y != base.Projectile.velocity.Y)
			{
				base.Projectile.netUpdate = true;
			}
			base.Projectile.velocity = shootVel;
		}
		if (base.Projectile.soundDelay <= 0 && !shootLaser)
		{
			base.Projectile.soundDelay = shootGateValue - chargeAmt;
			base.Projectile.soundDelay *= 2;
			if (base.Projectile.ai[0] != 1f)
			{
				SoundEngine.PlaySound(in SoundID.Item15, base.Projectile.position);
			}
		}
		if (base.Projectile.ai[0] > 5f && !shootLaser)
		{
			Vector2 spinningpoint3 = Vector2.UnitX * 32f;
			spinningpoint3 = spinningpoint3.RotatedBy(base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f));
			Vector2 dustSpawn = base.Projectile.Center + spinningpoint3;
			for (int k = 0; k < chargeAmt + 1; k++)
			{
				float dustScale = 0.4f;
				if (k % 2 == 1)
				{
					dustScale = 0.65f;
				}
				Vector2 randDustSpawn = dustSpawn + ((float)Main.rand.NextDouble() * ((float)Math.PI * 2f)).ToRotationVector2() * (12f - (float)(chargeAmt * 2));
				int electricDust = Dust.NewDust(randDustSpawn - Vector2.One * 8f, 16, 16, 226, base.Projectile.velocity.X / 2f, base.Projectile.velocity.Y / 2f);
				Main.dust[electricDust].velocity = Vector2.Normalize(dustSpawn - randDustSpawn) * 1.5f * (10f - (float)chargeAmt * 2f) / 10f;
				Main.dust[electricDust].noGravity = true;
				Main.dust[electricDust].scale = dustScale;
				Main.dust[electricDust].customData = player;
			}
		}
		if (shootLaser)
		{
			Vector2 spinningpoint4 = Vector2.UnitX * 32f;
			spinningpoint4 = spinningpoint4.RotatedBy(base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f));
			Vector2 dustSpawnLaser = base.Projectile.Center + spinningpoint4;
			for (int l = 0; l < 2; l++)
			{
				float dustScale2 = 0.35f;
				if (l % 2 == 1)
				{
					dustScale2 = 0.45f;
				}
				dustScale2 *= MathHelper.Lerp(1f, 3f, lerpAmtForCharge);
				float randFloat = Main.rand.NextFloatDirection();
				Vector2 randDustSpawnLaser = dustSpawnLaser + (base.Projectile.rotation + ((base.Projectile.spriteDirection == -1) ? (-(float)Math.PI / 2f) : ((float)Math.PI / 2f)) + randFloat * ((float)Math.PI / 4f) * 0.8f - (float)Math.PI / 2f).ToRotationVector2() * 6f;
				int electric2 = Dust.NewDust(randDustSpawnLaser - Vector2.One * 12f, 24, 24, 226, base.Projectile.velocity.X / 2f, base.Projectile.velocity.Y / 2f);
				Main.dust[electric2].velocity = (randDustSpawnLaser - dustSpawnLaser).SafeNormalize(Vector2.Zero) * MathHelper.Lerp(1.5f, 9f, Utils.GetLerpValue(1f, 0f, Math.Abs(randFloat), clamped: true));
				Main.dust[electric2].noGravity = true;
				Main.dust[electric2].scale = dustScale2;
				Main.dust[electric2].customData = player;
				Main.dust[electric2].fadeIn = 0.5f;
			}
		}
		if (shootThisFrame && Main.myPlayer == base.Projectile.owner)
		{
			Item freedomStar = player.HeldItem;
			int currentDamage = player.GetWeaponDamage(freedomStar);
			bool num = !player.CantUseHoldout();
			freedomStar.Calamity();
			bool hasCharge = true;
			if (num & hasCharge)
			{
				if (base.Projectile.ai[0] == 180f)
				{
					SoundEngine.PlaySound(in SoundID.Item124, base.Projectile.position);
					Vector2 actualVelocity = Vector2.Normalize(base.Projectile.velocity);
					if (float.IsNaN(actualVelocity.X) || float.IsNaN(actualVelocity.Y))
					{
						actualVelocity = -Vector2.UnitY;
					}
					int projectileDamage = (int)((float)currentDamage * 1.25f);
					int proj = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, actualVelocity, ModContent.ProjectileType<FreedomStarBeam>(), projectileDamage, base.Projectile.knockBack, base.Projectile.owner, 0f, base.Projectile.whoAmI);
					base.Projectile.ai[1] = proj;
					base.Projectile.netUpdate = true;
				}
				else if (shootLaser)
				{
					Projectile projectile = Main.projectile[(int)base.Projectile.ai[1]];
					if (!projectile.active || projectile.type != ModContent.ProjectileType<FreedomStarBeam>())
					{
						base.Projectile.Kill();
						return;
					}
				}
				else
				{
					bool shootOrb = false;
					if (base.Projectile.ai[0] == 1f)
					{
						shootOrb = true;
					}
					if (base.Projectile.ai[0] <= 50f && base.Projectile.ai[0] % 5f == 0f)
					{
						shootOrb = true;
					}
					if (base.Projectile.ai[0] >= 80f && base.Projectile.ai[0] < 180f && base.Projectile.ai[0] % 10f == 0f)
					{
						shootOrb = true;
					}
					if (shootOrb)
					{
						SoundEngine.PlaySound(in SoundID.Item75, base.Projectile.position);
						int projectileType = 459;
						float projectileVelocity = 10f;
						Vector2 actualVelocity2 = Vector2.Normalize(base.Projectile.velocity) * projectileVelocity;
						if (float.IsNaN(actualVelocity2.X) || float.IsNaN(actualVelocity2.Y))
						{
							actualVelocity2 = -Vector2.UnitY;
						}
						float orbPower = 0.7f + (float)chargeAmt * 0.3f;
						int projectileDamage2 = ((orbPower < 1f) ? currentDamage : ((int)((float)currentDamage * 2.5f)));
						int proj2 = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, actualVelocity2, projectileType, projectileDamage2, base.Projectile.knockBack, base.Projectile.owner, 0f, orbPower);
						Main.projectile[proj2].DamageType = DamageClass.Ranged;
						Main.projectile[proj2].extraUpdates += 2;
					}
				}
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		base.Projectile.position = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true) - base.Projectile.Size / 2f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == -1) ? ((float)Math.PI) : 0f);
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.timeLeft = 2;
		player.ChangeDir(base.Projectile.direction);
		player.heldProj = base.Projectile.whoAmI;
		player.itemTime = 2;
		player.itemAnimation = 2;
		player.itemRotation = (float)Math.Atan2(base.Projectile.velocity.Y * (float)base.Projectile.direction, base.Projectile.velocity.X * (float)base.Projectile.direction);
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
