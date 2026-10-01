using System;
using CalamityMod.Items.Weapons.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class ContagionBow : ModProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Contagion>();

	public override string Texture => "CalamityMod/Items/Weapons/Ranged/Contagion";

	public override void SetDefaults()
	{
		base.Projectile.width = 42;
		base.Projectile.height = 84;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Ranged;
	}

	public override void AI()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_0539: Unknown result type (might be due to invalid IL or missing references)
		//IL_0540: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_042a: Unknown result type (might be due to invalid IL or missing references)
		//IL_042f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_043d: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_048a: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a4: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		if (base.Projectile.type == ModContent.ProjectileType<ContagionBow>())
		{
			base.Projectile.ai[0]++;
			int fireSpeed = 0;
			if (base.Projectile.ai[0] >= 40f)
			{
				fireSpeed++;
			}
			if (base.Projectile.ai[0] >= 80f)
			{
				fireSpeed++;
			}
			if (base.Projectile.ai[0] >= 120f)
			{
				fireSpeed++;
			}
			int delayCompare = 24;
			int fireSpeedCompare = 6;
			base.Projectile.ai[1]++;
			bool fullSpeed = false;
			if (base.Projectile.ai[1] >= (float)(delayCompare - fireSpeedCompare * fireSpeed))
			{
				base.Projectile.ai[1] = 0f;
				fullSpeed = true;
			}
			base.Projectile.frameCounter += 1 + fireSpeed;
			if (base.Projectile.frameCounter >= 4)
			{
				base.Projectile.frameCounter = 0;
				base.Projectile.frame++;
				if (base.Projectile.frame >= 3)
				{
					base.Projectile.frame = 0;
				}
			}
			if (base.Projectile.soundDelay <= 0)
			{
				base.Projectile.soundDelay = delayCompare - fireSpeedCompare * fireSpeed;
				if (base.Projectile.ai[0] != 1f)
				{
					SoundEngine.PlaySound(in SoundID.Item5, base.Projectile.Center);
				}
			}
			if (base.Projectile.ai[1] == 1f && base.Projectile.ai[0] != 1f)
			{
				Vector2 rotate = Vector2.UnitX * 24f;
				rotate = rotate.RotatedBy(base.Projectile.rotation - (float)Math.PI / 2f);
				Vector2 value = base.Projectile.Center + rotate;
				for (int i = 0; i < 2; i++)
				{
					int dust = Dust.NewDust(value - Vector2.One * 8f, 16, 16, 44, base.Projectile.velocity.X / 2f, base.Projectile.velocity.Y / 2f, 100, default(Color), 0.25f);
					Dust obj = Main.dust[dust];
					obj.velocity *= 0.66f;
					Main.dust[dust].noGravity = true;
					Main.dust[dust].scale = 1.4f;
				}
			}
			if (fullSpeed && Main.myPlayer == base.Projectile.owner)
			{
				if (!player.CantUseHoldout())
				{
					float speed = player.HeldItem.shootSpeed * base.Projectile.scale;
					Vector2 spawnPos = vector;
					Vector2 direction = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - spawnPos;
					if (player.gravDir == -1f)
					{
						direction.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - spawnPos.Y;
					}
					Vector2 velocity = Vector2.Normalize(direction);
					if (float.IsNaN(velocity.X) || float.IsNaN(velocity.Y))
					{
						velocity = -Vector2.UnitY;
					}
					velocity *= speed;
					if (velocity.X != base.Projectile.velocity.X || velocity.Y != base.Projectile.velocity.Y)
					{
						base.Projectile.netUpdate = true;
					}
					base.Projectile.velocity = velocity;
					int projType = ModContent.ProjectileType<ContagionArrow>();
					float velocityMult = 14f;
					float randNum = 7f;
					spawnPos += new Vector2(Main.rand.NextFloat(0f - randNum, randNum), Main.rand.NextFloat(0f - randNum, randNum));
					Vector2 spinningpoint = Vector2.Normalize(base.Projectile.velocity) * velocityMult;
					spinningpoint = spinningpoint.RotatedBy(Main.rand.NextDouble() * 0.2 - 0.1);
					if (float.IsNaN(spinningpoint.X) || float.IsNaN(spinningpoint.Y))
					{
						spinningpoint = -Vector2.UnitY;
					}
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPos, spinningpoint, projType, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				}
				else
				{
					base.Projectile.Kill();
				}
			}
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		Vector2 displayOffset = Utils.RotatedBy(new Vector2(5f, 0f), (double)base.Projectile.rotation, default(Vector2));
		base.Projectile.Center = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true) + displayOffset;
		if (base.Projectile.spriteDirection == -1)
		{
			base.Projectile.rotation += (float)Math.PI;
		}
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

	public override bool PreDraw(ref Color lightColor)
	{
		return base.Projectile.ai[0] > 0f;
	}
}
