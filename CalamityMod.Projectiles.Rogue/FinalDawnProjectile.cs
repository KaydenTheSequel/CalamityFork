using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class FinalDawnProjectile : ModProjectile, ILocalizedModType, IModType
{
	public const float MaxChargeTime = 20f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 2;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
	}

	public override bool ShouldUpdatePosition()
	{
		return false;
	}

	public override void AI()
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_021c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0221: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (player == null || player.dead)
		{
			base.Projectile.Kill();
		}
		if (Main.myPlayer == player.whoAmI)
		{
			base.Projectile.velocity = Main.MouseWorld - player.Center;
			((Vector2)(ref base.Projectile.velocity)).Normalize();
		}
		player.ChangeDir(base.Projectile.direction);
		player.heldProj = base.Projectile.whoAmI;
		base.Projectile.Center = player.Center;
		base.Projectile.position.Y -= 44f;
		base.Projectile.position.X -= 46 * player.direction;
		player.bodyFrame.Y = player.bodyFrame.Height;
		if (base.Projectile.ai[0] < 20f)
		{
			Vector2 dustCenter = default(Vector2);
			((Vector2)(ref dustCenter))._002Ector(base.Projectile.Center.X, base.Projectile.Center.Y - 40f);
			int flame = Dust.NewDust(dustCenter, base.Projectile.width, base.Projectile.height, ModContent.DustType<FinalFlame>(), 0f, 0f, 100, default(Color), 2f);
			Main.dust[flame].noGravity = true;
			Main.dust[flame].fadeIn = 1.5f;
			Main.dust[flame].scale = 1.4f;
			Vector2 offsetVector = Main.rand.NextVector2CircularEdge(100f, 100f);
			Main.dust[flame].position = dustCenter - offsetVector;
			Vector2 newVelocity = dustCenter - Main.dust[flame].position;
			Main.dust[flame].velocity = newVelocity * 0.1f;
		}
		if (base.Projectile.ai[0] == 20f)
		{
			int dustCount = 36;
			for (int i = 0; i < dustCount; i++)
			{
				Vector2 val = base.Projectile.Center + new Vector2(0f, -40f);
				Vector2 offset = Vector2.UnitX * (float)base.Projectile.width * 0.1875f;
				offset = offset.RotatedBy((float)(i - (dustCount / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustCount);
				int dustIdx = Dust.NewDust(val + offset, 0, 0, ModContent.DustType<FinalFlame>(), offset.X * 2f, offset.Y * 2f, 100, default(Color), 3.4f);
				Main.dust[dustIdx].noGravity = true;
				Main.dust[dustIdx].noLight = true;
				Main.dust[dustIdx].velocity = Vector2.Normalize(offset) * 5f;
			}
			base.Projectile.frame = 1;
		}
		base.Projectile.ai[0]++;
		base.Projectile.spriteDirection = (base.Projectile.velocity.X > 0f).ToDirectionInt();
		if (Main.myPlayer == base.Projectile.owner && player.CantUseHoldout())
		{
			AttemptExecuteAttacks(player);
			base.Projectile.Kill();
		}
	}

	public void AttemptExecuteAttacks(Player player)
	{
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] >= 20f && !player.noItems && !player.CCed)
		{
			if (player.controlUp)
			{
				if (player.Calamity().StealthStrikeAvailable() && base.Projectile.ai[1] != 1f)
				{
					int stealth = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), player.Center, player.SafeDirectionTo(Main.MouseWorld) * 28f, ModContent.ProjectileType<FinalDawnThrow2>(), (int)((float)base.Projectile.damage * 1f), base.Projectile.knockBack, base.Projectile.owner);
					Main.projectile[stealth].Calamity().stealthStrike = true;
					player.Calamity().ConsumeStealthByAttacking();
					player.immuneNoBlink = true;
					player.immuneTime += 20;
				}
				else
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.SafeDirectionTo(Main.MouseWorld) * 38f, ModContent.ProjectileType<FinalDawnThrow>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
					if (base.Projectile.ai[1] != 1f)
					{
						player.Calamity().ConsumeStealthByAttacking();
					}
				}
			}
			else if (player.Calamity().StealthStrikeAvailable() && base.Projectile.ai[1] != 1f)
			{
				int stealth2 = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity, ModContent.ProjectileType<FinalDawnHorizontalSlash>(), (int)((float)base.Projectile.damage * 1.275f), base.Projectile.knockBack, base.Projectile.owner);
				Main.projectile[stealth2].Calamity().stealthStrike = true;
				player.Calamity().ConsumeStealthByAttacking();
			}
			else
			{
				int p = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity, ModContent.ProjectileType<FinalDawnFireSlash>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				if (p.WithinBounds(Main.maxProjectiles) && base.Projectile.Calamity().LocketClone)
				{
					Main.projectile[p].Calamity().LocketClone = true;
				}
			}
		}
		else
		{
			player.Calamity().ConsumeStealthByAttacking();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		Texture2D scytheTexture = TextureAssets.Projectile[base.Type].Value;
		Texture2D glowmask = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/FinalDawnProjectile_Glow", (AssetRequestMode)2).Value;
		int height = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int yStart = height * base.Projectile.frame;
		Main.spriteBatch.Draw(scytheTexture, base.Projectile.Center - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY, (Rectangle?)new Rectangle(0, yStart, scytheTexture.Width, height), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)scytheTexture.Width / 2f, (float)height / 2f), base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1), 0f);
		Main.spriteBatch.Draw(glowmask, base.Projectile.Center - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY, (Rectangle?)new Rectangle(0, yStart, scytheTexture.Width, height), base.Projectile.GetAlpha(Color.White), base.Projectile.rotation, new Vector2((float)scytheTexture.Width / 2f, (float)height / 2f), base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1), 0f);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 300);
	}
}
