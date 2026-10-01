using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class MelterAmp : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 34;
		base.Projectile.height = 38;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.netImportant = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 6000;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		bool num = base.Projectile.type == ModContent.ProjectileType<MelterAmp>();
		Player player = Main.player[base.Projectile.owner];
		if (num)
		{
			if (player.dead)
			{
				base.Projectile.active = false;
				return;
			}
			if (player.ownedProjectileCounts[ModContent.ProjectileType<MelterAmp>()] > 1)
			{
				base.Projectile.active = false;
				return;
			}
			if (!player.HeldItem.CountsAsClass<MagicDamageClass>() || player.HeldItem.shoot != ModContent.ProjectileType<MelterNote1>())
			{
				base.Projectile.active = false;
				return;
			}
		}
		Lighting.AddLight(base.Projectile.Center, 0.75f, 0.75f, 0.75f);
		if (base.Projectile.ai[0] > 0f)
		{
			base.Projectile.ai[0]++;
			if (base.Projectile.ai[0] > 6f)
			{
				base.Projectile.ai[0] = 0f;
			}
		}
		if (Main.myPlayer == base.Projectile.owner && base.Projectile.ai[0] == 0f)
		{
			base.Projectile.ai[0] = 1f;
			int Damage = base.Projectile.damage;
			base.Projectile.netUpdate = true;
			Vector2 projAimDirection = base.Projectile.Center;
			float ampXDirection = (float)Main.mouseX + Main.screenPosition.X - projAimDirection.X;
			float ampYDirection = (float)Main.mouseY + Main.screenPosition.Y - projAimDirection.Y;
			if (player.gravDir == -1f)
			{
				ampYDirection = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - projAimDirection.Y;
			}
			float ampAimDistance = (float)Math.Sqrt(ampXDirection * ampXDirection + ampYDirection * ampYDirection);
			if (ampAimDistance == 0f)
			{
				((Vector2)(ref projAimDirection))._002Ector(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2));
				ampXDirection = base.Projectile.position.X + (float)base.Projectile.width * 0.5f - projAimDirection.X;
				ampYDirection = base.Projectile.position.Y + (float)base.Projectile.height * 0.5f - projAimDirection.Y;
				ampAimDistance = (float)Math.Sqrt(ampXDirection * ampXDirection + ampYDirection * ampYDirection);
			}
			ampAimDistance = 20f / ampAimDistance;
			ampXDirection *= ampAimDistance;
			ampYDirection *= ampAimDistance;
			float VelocityX = ampXDirection;
			float VelocityY = ampYDirection;
			int type;
			if (Main.rand.Next(0, 2) == 0)
			{
				Damage = (int)((float)base.Projectile.damage * 1.5f);
				type = ModContent.ProjectileType<MelterNote1>();
			}
			else
			{
				VelocityX *= 1.5f;
				VelocityY *= 1.5f;
				type = ModContent.ProjectileType<MelterNote2>();
			}
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, VelocityX, VelocityY, type, Damage, base.Projectile.knockBack, base.Projectile.owner);
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 5)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 2)
		{
			base.Projectile.frame = 0;
		}
	}
}
