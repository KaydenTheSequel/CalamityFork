using System;
using CalamityMod.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SevensStrikerHoldout : ModProjectile
{
	public bool rolling = true;

	public bool shotonce;

	public int shottimer;

	public int rolltimer = 60;

	public int soundtimer;

	public SlotId RouletteSoundSlot;

	public SlotId JingleSoundSlot;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<TheSevensStriker>();

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 19;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 160;
		base.Projectile.height = 62;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0697: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0700: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_0712: Unknown result type (might be due to invalid IL or missing references)
		//IL_071c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0721: Unknown result type (might be due to invalid IL or missing references)
		//IL_072c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0736: Unknown result type (might be due to invalid IL or missing references)
		//IL_073b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0740: Unknown result type (might be due to invalid IL or missing references)
		//IL_0751: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_0271: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_061b: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0501: Unknown result type (might be due to invalid IL or missing references)
		//IL_0506: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_055d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_058b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		Vector2 playerpos = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		bool shouldBeHeld = !player.CantUseHoldout();
		float scaleFactor = 14f;
		int weaponDamage = player.GetWeaponDamage(player.HeldItem);
		float weaponKnockback = player.HeldItem.knockBack;
		if (base.Projectile.ai[1] == 0f)
		{
			if (shotonce)
			{
				if (player.PickAmmo(player.HeldItem, out var shot, out scaleFactor, out weaponDamage, out weaponKnockback, out var _))
				{
					base.Projectile.ai[0] = shot;
					base.Projectile.ai[1] = CalculateOutcome();
				}
				else
				{
					base.Projectile.Kill();
				}
			}
			else
			{
				base.Projectile.ai[1] = CalculateOutcome();
				shotonce = true;
			}
		}
		rolltimer--;
		soundtimer++;
		if (rolling)
		{
			base.Projectile.frameCounter++;
		}
		else
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			rolling = false;
			rolltimer = 16;
			base.Projectile.frame = 0;
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			if (shouldBeHeld && base.Projectile.ai[1] != 0f)
			{
				float holdscale = player.HeldItem.shootSpeed * base.Projectile.scale;
				Vector2 playerpos2 = playerpos;
				Vector2 going = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - playerpos2;
				if (player.gravDir == -1f)
				{
					going.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - playerpos2.Y;
				}
				Vector2 normalizedgoing = Vector2.Normalize(going);
				if (float.IsNaN(normalizedgoing.X) || float.IsNaN(normalizedgoing.Y))
				{
					normalizedgoing = -Vector2.UnitY;
				}
				normalizedgoing *= holdscale;
				if (normalizedgoing.X != base.Projectile.velocity.X || normalizedgoing.Y != base.Projectile.velocity.Y)
				{
					base.Projectile.netUpdate = true;
				}
				base.Projectile.velocity = normalizedgoing * 0.55f;
				if (!rolling && rolltimer <= 0)
				{
					if (base.Projectile.ai[1] == 4f)
					{
						shottimer++;
						if (shottimer == 1)
						{
							JingleSoundSlot = SoundEngine.PlaySound(Main.zenithWorld ? TheSevensStriker.JackpotGFB : TheSevensStriker.JackpotSound, player.Center);
							CombatText.NewText(player.getRect(), Color.Gold, CalamityUtils.GetTextValue("Misc.SevensJackpot"), dramatic: true);
						}
						if (shottimer % 7 == 0 && shottimer > 7 && shottimer <= 56)
						{
							int jackpotDamage = (int)((float)weaponDamage * (Main.zenithWorld ? TheSevensStriker.JackpotMultiplierGFB : TheSevensStriker.JackpotMultiplier));
							Shoot(7, ModContent.ProjectileType<SevensStrikerPlatinumCoin>(), jackpotDamage, weaponKnockback, (float)(int)scaleFactor * 2f, 0.2f);
							SoundEngine.PlaySound(in TheSevensStriker.CoinSound, base.Projectile.Center);
						}
						if (shottimer > (Main.zenithWorld ? 88 : 56))
						{
							soundtimer = 0;
							rolling = true;
							base.Projectile.ai[1] = 0f;
							shottimer = 0;
						}
					}
					else
					{
						shottimer++;
						if (shottimer == 1)
						{
							float num = base.Projectile.ai[1];
							if (num != 1f)
							{
								if (num != 2f)
								{
									if (num == 3f)
									{
										int cherryDamage = (int)((float)weaponDamage * TheSevensStriker.TriplesCherryMultiplier);
										int grapeDamage = (int)((float)weaponDamage * TheSevensStriker.TriplesGrapeMultiplier);
										Shoot(7, ModContent.ProjectileType<SevensStrikerCherry>(), cherryDamage, weaponKnockback, 1.5f, 0.1f);
										Shoot(7, ModContent.ProjectileType<SevensStrikerGrape>(), grapeDamage, weaponKnockback, 2f, 0.2f);
										CombatText.NewText(player.getRect(), Color.Red, CalamityUtils.GetTextValue("Misc.SevensTriples"), dramatic: true);
										JingleSoundSlot = SoundEngine.PlaySound(in TheSevensStriker.TriplesSound, player.Center);
									}
								}
								else
								{
									int doublesDamage = (int)((float)weaponDamage * TheSevensStriker.DoublesMultiplier);
									Shoot(7, ModContent.ProjectileType<SevensStrikerOrange>(), doublesDamage, weaponKnockback, 2f, 0.1f);
									CombatText.NewText(player.getRect(), Color.Orange, CalamityUtils.GetTextValue("Misc.SevensDoubles"), dramatic: true);
									JingleSoundSlot = SoundEngine.PlaySound(in TheSevensStriker.DoublesSound, player.Center);
								}
							}
							else
							{
								Shoot(1, ModContent.ProjectileType<SevensStrikerBrick>(), weaponDamage, 0f, 2f, 0f);
								CombatText.NewText(player.getRect(), Color.Gray, CalamityUtils.GetTextValue("Misc.SevensBust"), dramatic: true);
								JingleSoundSlot = SoundEngine.PlaySound(Main.zenithWorld ? TheSevensStriker.BustGFB : TheSevensStriker.BustSound, player.Center);
							}
						}
						if (shottimer > (Main.zenithWorld ? 56 : 16))
						{
							soundtimer = 0;
							rolling = true;
							base.Projectile.ai[1] = 0f;
							shottimer = 0;
						}
					}
				}
				if (SoundEngine.TryGetActiveSound(RouletteSoundSlot, out ActiveSound rouletteSound) && rouletteSound.IsPlaying)
				{
					rouletteSound.Position = base.Projectile.Center;
				}
				if (SoundEngine.TryGetActiveSound(JingleSoundSlot, out ActiveSound jingle) && jingle.IsPlaying)
				{
					jingle.Position = player.Center;
				}
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		if (base.Projectile.frameCounter == 0 && base.Projectile.frame == 2 * (Main.projFrames[base.Type] / 19))
		{
			SoundStyle style = SoundID.Item108 with
			{
				Volume = SoundID.Item108.Volume * 0.9f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			RouletteSoundSlot = SoundEngine.PlaySound(in TheSevensStriker.RouletteSound, base.Projectile.Center);
		}
		if (soundtimer == 92 || soundtimer == 108 || soundtimer == 124)
		{
			SoundEngine.PlaySound(in TheSevensStriker.RouletteTickSound, base.Projectile.Center);
		}
		base.Projectile.position = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true) - base.Projectile.Size / 2f + base.Projectile.velocity * 95f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.timeLeft = 2;
		player.ChangeDir(base.Projectile.direction);
		player.heldProj = base.Projectile.whoAmI;
		player.itemTime = 2;
		player.itemAnimation = 2;
		player.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
	}

	public int CalculateOutcome()
	{
		if (Main.zenithWorld)
		{
			if (Main.rand.Next(100) < 20)
			{
				return 4;
			}
			return 1;
		}
		float num = base.Projectile.ai[0];
		if (num != 158f)
		{
			if (num != 159f)
			{
				if (num != 160f)
				{
					if (num == 161f)
					{
						return 4;
					}
					return 1;
				}
				int roll = Main.rand.Next(100);
				if (roll <= 5)
				{
					return 1;
				}
				if (roll > 5 && roll <= 35)
				{
					return 2;
				}
				if (roll > 35 && roll <= 85)
				{
					return 3;
				}
				return 4;
			}
			int roll2 = Main.rand.Next(100);
			if (roll2 <= 20)
			{
				return 1;
			}
			if (roll2 > 20 && roll2 <= 70)
			{
				return 2;
			}
			if (roll2 > 70 && roll2 <= 90)
			{
				return 3;
			}
			return 4;
		}
		int roll3 = Main.rand.Next(100);
		if (roll3 <= 50)
		{
			return 1;
		}
		if (roll3 > 50 && roll3 <= 80)
		{
			return 2;
		}
		if (roll3 > 80 && roll3 <= 95)
		{
			return 3;
		}
		return 4;
	}

	public void Shoot(int projcount, int type, int damage, float kb, float scaleFactor, float spreadfactor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		Vector2 armPosition = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		armPosition += base.Projectile.velocity.SafeNormalize((float)player.direction * Vector2.UnitX) * 32f;
		armPosition.Y -= 20f;
		Vector2 gunTip = armPosition + base.Projectile.velocity.SafeNormalize((float)player.direction * Vector2.UnitX) * player.HeldItem.scale * 70f;
		for (int i = 0; i < projcount; i++)
		{
			Vector2 perturbedSpeed = base.Projectile.velocity.RotatedBy(MathHelper.Lerp(0f - spreadfactor, spreadfactor, (float)i / 7f)) * scaleFactor;
			int p = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), gunTip, perturbedSpeed, type, damage, kb, Main.player[base.Projectile.owner].whoAmI);
			if (Main.projectile.IndexInRange(p))
			{
				Main.projectile[p].originalDamage = damage;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		Texture2D gun = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Ranged/SevensStrikerHoldout", (AssetRequestMode)2).Value;
		SpriteEffects flip = (SpriteEffects)(base.Projectile.direction < 0);
		float drawAngle = base.Projectile.rotation + ((Owner.direction < 0) ? ((float)Math.PI) : 0f);
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector((Owner.direction < 0) ? ((float)gun.Width - 33f) : 33f, 33f);
		Vector2 drawOffset = Owner.MountedCenter + base.Projectile.rotation.ToRotationVector2() - Main.screenPosition;
		drawOffset.Y -= 10f;
		int indframeheight = gun.Height / Main.projFrames[base.Type];
		int currentframe = indframeheight * base.Projectile.frame;
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(0, currentframe, gun.Width, indframeheight);
		Main.EntitySpriteDraw(gun, drawOffset, frame, Color.White, drawAngle, drawOrigin, base.Projectile.scale, flip);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(RouletteSoundSlot, out ActiveSound dringdring))
		{
			dringdring.Stop();
		}
		if (SoundEngine.TryGetActiveSound(JingleSoundSlot, out ActiveSound jingle))
		{
			jingle.Stop();
		}
		Main.player[base.Projectile.owner].SetDummyItemTime(12);
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}
}
