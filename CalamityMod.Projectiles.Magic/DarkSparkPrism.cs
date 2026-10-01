using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class DarkSparkPrism : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Items/Weapons/Magic/DarkSpark";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.NeedsUUID[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 22;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0235: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		float piConditional = 0f;
		if (base.Projectile.spriteDirection == -1)
		{
			piConditional = (float)Math.PI;
		}
		Vector2 playerRotation = player.RotatedRelativePoint(player.MountedCenter);
		float hitCooldown = 30f;
		if (base.Projectile.ai[0] > 360f)
		{
			hitCooldown = 15f;
		}
		if (base.Projectile.ai[0] > 480f)
		{
			hitCooldown = 5f;
		}
		base.Projectile.damage = ((player.HeldItem != null) ? player.GetWeaponDamage(player.HeldItem) : 0);
		base.Projectile.ai[0]++;
		base.Projectile.ai[1]++;
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			base.Projectile.localAI[1] = 255f;
		}
		if (base.Projectile.localAI[1] > 0f)
		{
			base.Projectile.localAI[1]--;
		}
		bool shouldHitNotCharged = false;
		if (base.Projectile.ai[0] % hitCooldown == 0f)
		{
			shouldHitNotCharged = true;
		}
		bool shouldHitChargedUp = false;
		if (base.Projectile.ai[0] % hitCooldown == 0f)
		{
			shouldHitChargedUp = true;
		}
		if (base.Projectile.ai[1] >= 1f)
		{
			base.Projectile.ai[1] = 0f;
			shouldHitChargedUp = true;
			if (Main.myPlayer == base.Projectile.owner)
			{
				float scaleFactor5 = player.HeldItem.shootSpeed * base.Projectile.scale;
				Vector2 projRotation = playerRotation;
				Vector2 gravityAdjustedRotation = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - projRotation;
				if (player.gravDir == -1f)
				{
					gravityAdjustedRotation.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - projRotation.Y;
				}
				Vector2 prismDirection = Vector2.Normalize(gravityAdjustedRotation);
				if (float.IsNaN(prismDirection.X) || float.IsNaN(prismDirection.Y))
				{
					prismDirection = -Vector2.UnitY;
				}
				prismDirection = Vector2.Normalize(Vector2.Lerp(prismDirection, Vector2.Normalize(base.Projectile.velocity), 0.92f));
				prismDirection *= scaleFactor5;
				if (prismDirection.X != base.Projectile.velocity.X || prismDirection.Y != base.Projectile.velocity.Y)
				{
					base.Projectile.netUpdate = true;
				}
				base.Projectile.velocity = prismDirection;
			}
		}
		base.Projectile.frameCounter++;
		int framing = ((!(base.Projectile.ai[0] < 480f)) ? 1 : 3);
		if (base.Projectile.frameCounter >= framing)
		{
			base.Projectile.frameCounter = 0;
			if (++base.Projectile.frame >= 4)
			{
				base.Projectile.frame = 0;
			}
		}
		if (base.Projectile.soundDelay <= 0)
		{
			base.Projectile.soundDelay = 10;
			base.Projectile.soundDelay *= 2;
			if (base.Projectile.ai[0] != 1f)
			{
				SoundEngine.PlaySound(in SoundID.Item15, base.Projectile.Center);
			}
		}
		if (shouldHitChargedUp && Main.myPlayer == base.Projectile.owner)
		{
			bool hasMana = !shouldHitNotCharged || player.CheckMana(player.HeldItem, -1, pay: true);
			if (!player.CantUseHoldout() & hasMana)
			{
				if (base.Projectile.ai[0] == 1f)
				{
					Vector2 projCenter = base.Projectile.Center;
					Vector2 beamDirection = Vector2.Normalize(base.Projectile.velocity);
					if (float.IsNaN(beamDirection.X) || float.IsNaN(beamDirection.Y))
					{
						beamDirection = -Vector2.UnitY;
					}
					for (int l = 0; l < 7; l++)
					{
						Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), projCenter, beamDirection, ModContent.ProjectileType<DarkSparkBeam>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, l, Projectile.GetByUUID(base.Projectile.owner, base.Projectile.whoAmI));
					}
					base.Projectile.netUpdate = true;
				}
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

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0318: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0297: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		Vector2 mountedCenter = Main.player[base.Projectile.owner].MountedCenter;
		Color prismColorArea = Lighting.GetColor((int)((double)base.Projectile.position.X + (double)base.Projectile.width * 0.5) / 16, (int)(((double)base.Projectile.position.Y + (double)base.Projectile.height * 0.5) / 16.0));
		if (base.Projectile.hide && !ProjectileID.Sets.DontAttachHideToAlpha[base.Type])
		{
			prismColorArea = Lighting.GetColor((int)mountedCenter.X / 16, (int)(mountedCenter.Y / 16f));
		}
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Texture2D texture2D14 = TextureAssets.Projectile[base.Type].Value;
		int framing = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y7 = framing * base.Projectile.frame;
		Vector2 drawStart = (base.Projectile.position + new Vector2((float)base.Projectile.width, (float)base.Projectile.height) / 2f + Vector2.UnitY * base.Projectile.gfxOffY - Main.screenPosition).Floor();
		if (Main.player[base.Projectile.owner].shroomiteStealth && Main.player[base.Projectile.owner].inventory[Main.player[base.Projectile.owner].selectedItem].CountsAsClass<RangedDamageClass>())
		{
			float playerStealth = Main.player[base.Projectile.owner].stealth;
			if ((double)playerStealth < 0.03)
			{
				playerStealth = 0.03f;
			}
			_ = (1f + playerStealth * 10f) / 11f;
			prismColorArea *= playerStealth;
		}
		if (Main.player[base.Projectile.owner].setVortex && Main.player[base.Projectile.owner].inventory[Main.player[base.Projectile.owner].selectedItem].CountsAsClass<RangedDamageClass>())
		{
			float playerStealthAgain = Main.player[base.Projectile.owner].stealth;
			if ((double)playerStealthAgain < 0.03)
			{
				playerStealthAgain = 0.03f;
			}
			_ = (1f + playerStealthAgain * 10f) / 11f;
			prismColorArea = prismColorArea.MultiplyRGBA(new Color(Vector4.Lerp(Vector4.One, new Vector4(0.16f, 0.12f, 0f, 0f), 1f - playerStealthAgain)));
		}
		Main.spriteBatch.Draw(texture2D14, drawStart, (Rectangle?)new Rectangle(0, y7, texture2D14.Width, framing), base.Projectile.GetAlpha(prismColorArea), base.Projectile.rotation, new Vector2((float)texture2D14.Width / 2f, (float)framing / 2f), base.Projectile.scale, spriteEffects, 0f);
		float scaleFactor2 = (float)Math.Cos((float)Math.PI * 2f * (base.Projectile.ai[0] / 120f)) * 2f + 2f;
		if (base.Projectile.ai[0] > 480f)
		{
			scaleFactor2 = 4f;
		}
		for (float i = 0f; i < 4f; i++)
		{
			Main.spriteBatch.Draw(texture2D14, drawStart + Vector2.UnitY.RotatedBy(i * ((float)Math.PI * 2f) / 4f) * scaleFactor2, (Rectangle?)new Rectangle(0, y7, texture2D14.Width, framing), base.Projectile.GetAlpha(prismColorArea).MultiplyRGBA(new Color(255, 255, 255, 0)) * 0.03f, base.Projectile.rotation, new Vector2((float)texture2D14.Width / 2f, (float)framing / 2f), base.Projectile.scale, spriteEffects, 0f);
		}
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] < 255f)
		{
			return new Color((int)base.Projectile.ai[0], (int)base.Projectile.ai[0], (int)base.Projectile.ai[0], (int)base.Projectile.localAI[1]);
		}
		return new Color(255, 255, 255, 0);
	}
}
