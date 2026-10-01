using System;
using CalamityMod.Items.Weapons.Magic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class GhastlyVisageProj : ModProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<GhastlyVisage>();

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0416: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_0477: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_048c: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0522: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_053a: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0562: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.65f, 0f, 0.1f);
		Player player = Main.player[base.Projectile.owner];
		float piConditional = 0f;
		Vector2 playerRotate = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		if (base.Projectile.spriteDirection == -1)
		{
			piConditional = (float)Math.PI;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.ai[0]++;
		int aiSoundDelay = 0;
		if (base.Projectile.ai[0] >= 240f)
		{
			aiSoundDelay++;
		}
		if (base.Projectile.ai[0] >= 480f)
		{
			aiSoundDelay++;
		}
		int soundDelayer = 40;
		int soundDelayMult = 2;
		base.Projectile.ai[1]--;
		bool isActive = false;
		if (base.Projectile.ai[1] <= 0f)
		{
			base.Projectile.ai[1] = soundDelayer - soundDelayMult * aiSoundDelay;
			isActive = true;
		}
		bool canUseItem = !player.CantUseHoldout();
		if (base.Projectile.localAI[0] > 0f)
		{
			base.Projectile.localAI[0]--;
		}
		int manaCost = (int)(20f * player.manaCost);
		if (base.Projectile.localAI[1] == 0f)
		{
			if (player.statMana < manaCost)
			{
				if (player.manaFlower)
				{
					player.QuickMana();
					if (player.statMana >= manaCost)
					{
						player.manaRegenDelay = (int)player.maxRegenDelay;
						player.statMana -= manaCost;
					}
					else
					{
						base.Projectile.Kill();
						isActive = false;
					}
				}
				else
				{
					base.Projectile.Kill();
					isActive = false;
				}
			}
			else if (player.statMana >= manaCost)
			{
				player.statMana -= manaCost;
				player.manaRegenDelay = (int)player.maxRegenDelay;
			}
			base.Projectile.localAI[1]++;
			base.Projectile.soundDelay = soundDelayer - soundDelayMult * aiSoundDelay;
			if (isActive)
			{
				SoundEngine.PlaySound(in SoundID.Item117, base.Projectile.Center);
			}
		}
		else if ((base.Projectile.soundDelay <= 0) & canUseItem)
		{
			if (player.statMana < manaCost)
			{
				if (player.manaFlower)
				{
					player.QuickMana();
					if (player.statMana >= manaCost)
					{
						player.manaRegenDelay = (int)player.maxRegenDelay;
						player.statMana -= manaCost;
					}
					else
					{
						base.Projectile.Kill();
						isActive = false;
					}
				}
				else
				{
					base.Projectile.Kill();
					isActive = false;
				}
			}
			else if (player.statMana >= manaCost)
			{
				player.statMana -= manaCost;
				player.manaRegenDelay = (int)player.maxRegenDelay;
			}
			base.Projectile.soundDelay = soundDelayer - soundDelayMult * aiSoundDelay;
			if ((base.Projectile.ai[0] != 1f) & isActive)
			{
				SoundEngine.PlaySound(in SoundID.Item117, base.Projectile.Center);
			}
			base.Projectile.localAI[0] = 12f;
		}
		if (isActive && Main.myPlayer == base.Projectile.owner)
		{
			float coreVelocity = 11.5f;
			int weaponDamage2 = player.GetWeaponDamage(player.HeldItem);
			float weaponKnockback2 = player.HeldItem.knockBack;
			if (canUseItem)
			{
				weaponKnockback2 = player.GetWeaponKnockback(player.HeldItem, weaponKnockback2);
				float scaleFactor12 = player.HeldItem.shootSpeed * base.Projectile.scale;
				Vector2 playerRotateCopy = playerRotate;
				Vector2 projSpawnDirection = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - playerRotateCopy;
				if (player.gravDir == -1f)
				{
					projSpawnDirection.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - playerRotateCopy.Y;
				}
				Vector2 projSpawnDirectNormalize = Vector2.Normalize(projSpawnDirection);
				if (float.IsNaN(projSpawnDirectNormalize.X) || float.IsNaN(projSpawnDirectNormalize.Y))
				{
					projSpawnDirectNormalize = -Vector2.UnitY;
				}
				projSpawnDirectNormalize *= scaleFactor12;
				if (projSpawnDirectNormalize.X != base.Projectile.velocity.X || projSpawnDirectNormalize.Y != base.Projectile.velocity.Y)
				{
					base.Projectile.netUpdate = true;
				}
				base.Projectile.velocity = projSpawnDirectNormalize * 0.55f;
				Vector2 normalCoreVel = Vector2.Normalize(base.Projectile.velocity) * coreVelocity;
				if (float.IsNaN(normalCoreVel.X) || float.IsNaN(normalCoreVel.Y))
				{
					normalCoreVel = -Vector2.UnitY;
				}
				Vector2 randomSpawnOffset = playerRotateCopy + Utils.RandomVector2(Main.rand, -10f, 10f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), randomSpawnOffset.X, randomSpawnOffset.Y, normalCoreVel.X, normalCoreVel.Y, ModContent.ProjectileType<GhastlyBlast>(), weaponDamage2, weaponKnockback2, base.Projectile.owner);
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		base.Projectile.position = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true) - base.Projectile.Size / 2f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + piConditional;
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.timeLeft = 2;
		player.ChangeDir(base.Projectile.direction);
		player.heldProj = base.Projectile.whoAmI;
		player.itemTime = 2;
		player.itemAnimation = 2;
		player.itemRotation = (float)Math.Atan2(base.Projectile.velocity.Y * (float)base.Projectile.direction, base.Projectile.velocity.X * (float)base.Projectile.direction);
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture2D13 = TextureAssets.Projectile[base.Type].Value;
		int framing = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(13f, 16f);
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Magic/GhastlyVisageProjGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, y6, texture2D13.Width, framing), Color.White, base.Projectile.rotation, origin, base.Projectile.scale, spriteEffects, 0f);
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
