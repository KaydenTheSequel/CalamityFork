using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Weapons.Magic;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class ChronomancersScytheSwing : ModProjectile
{
	public static int IcicleSpeed = 24;

	public static int IcicleVariance = 4;

	public static int IcicleFrequency = 5;

	public static float IcicleDamageMultiplier = 1.2f;

	public static int ClockChance = 30;

	public override LocalizedText DisplayName => CalamityUtils.GetItemName<ChronomancersScythe>();

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 148;
		base.Projectile.height = 68;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 5;
		base.Projectile.coldDamage = true;
	}

	public override void AI()
	{
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0444: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0577: Unknown result type (might be due to invalid IL or missing references)
		//IL_0588: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		Player Owner = Main.player[base.Projectile.owner];
		base.Projectile.damage = (int)(Owner.GetTotalDamage(base.Projectile.DamageType).ApplyTo(base.Projectile.originalDamage) * (1f + Utils.GetLerpValue(0f, 300f, base.Projectile.ai[1], clamped: true)));
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 1)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.soundDelay--;
		if (base.Projectile.soundDelay <= 0)
		{
			SoundEngine.PlaySound(in SoundID.Item71, base.Projectile.Center);
			base.Projectile.soundDelay = 24;
		}
		if (base.Projectile.ai[2] >= 32f)
		{
			if (!Owner.CheckMana(Owner.HeldItem, -1, pay: true))
			{
				base.Projectile.Kill();
				return;
			}
			base.Projectile.ai[2] = 0f;
		}
		base.Projectile.ai[2]++;
		if (Main.myPlayer == base.Projectile.owner)
		{
			if (!Owner.CantUseHoldout())
			{
				float scaleFactor6 = 1f;
				if (Owner.HeldItem.shoot == base.Projectile.type)
				{
					scaleFactor6 = Owner.HeldItem.shootSpeed * base.Projectile.scale;
				}
				Vector2 slashDirection = Main.MouseWorld - Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true);
				((Vector2)(ref slashDirection)).Normalize();
				if (slashDirection.HasNaNs())
				{
					slashDirection = Vector2.UnitX * (float)Owner.direction;
				}
				slashDirection *= scaleFactor6;
				if (slashDirection.X != base.Projectile.velocity.X || slashDirection.Y != base.Projectile.velocity.Y)
				{
					base.Projectile.netUpdate = true;
				}
				base.Projectile.velocity = slashDirection;
			}
			else
			{
				base.Projectile.Kill();
			}
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] % (float)IcicleFrequency == 0f && Main.myPlayer == base.Projectile.owner)
		{
			Vector2 velocity = base.Projectile.Center.DirectionTo(Main.MouseWorld) * (float)IcicleSpeed + new Vector2(Main.rand.NextFloat(-IcicleVariance, IcicleVariance), Main.rand.NextFloat(-IcicleVariance, IcicleVariance));
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<ChronoIcicleSmall>(), (int)((float)base.Projectile.damage * IcicleDamageMultiplier), base.Projectile.knockBack, base.Projectile.owner);
		}
		Vector2 dustSpawn = base.Projectile.Center + base.Projectile.velocity * 3f;
		Color newColor = Color.LightBlue;
		float r = (float)(int)((Color)(ref newColor)).R * 0.001f;
		newColor = Color.LightBlue;
		float g = (float)(int)((Color)(ref newColor)).G * 0.001f;
		newColor = Color.LightBlue;
		Lighting.AddLight(dustSpawn, r, g, (float)(int)((Color)(ref newColor)).B * 0.001f);
		if (Main.rand.NextBool(3))
		{
			Vector2 position = dustSpawn - base.Projectile.Size * 0.5f;
			int width = base.Projectile.width;
			int height = base.Projectile.height;
			float x = base.Projectile.velocity.X;
			float y = base.Projectile.velocity.Y;
			newColor = default(Color);
			int dust = Dust.NewDust(position, width, height, 67, x, y, 100, newColor);
			Main.dust[dust].noGravity = true;
			Dust obj = Main.dust[dust];
			obj.position -= base.Projectile.velocity;
		}
		base.Projectile.position = Owner.RotatedRelativePoint(Owner.MountedCenter, reverseRotation: true) - base.Projectile.Size * 0.5f + Vector2.UnitX * 8f + Vector2.UnitY * 4f;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (base.Projectile.spriteDirection == 1)
		{
			base.Projectile.rotation += (float)Math.PI;
		}
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.timeLeft = 2;
		Owner.ChangeDir(base.Projectile.direction);
		Owner.heldProj = base.Projectile.whoAmI;
		Owner.itemTime = 2;
		Owner.itemAnimation = 2;
		Owner.itemRotation = (base.Projectile.velocity * (float)base.Projectile.direction).ToRotation();
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<TimeDistortion>(), 60);
		if (Main.rand.NextBool(ClockChance))
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), target.Center, Main.rand.NextVector2Circular(-2f, 2f), ModContent.ProjectileType<ChronoClock>(), 0, 0f, base.Projectile.owner);
		}
	}
}
