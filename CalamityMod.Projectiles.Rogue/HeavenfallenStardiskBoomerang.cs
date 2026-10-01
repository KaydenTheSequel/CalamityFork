using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class HeavenfallenStardiskBoomerang : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/HeavenfallenStardisk";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 34;
		base.Projectile.height = 34;
		base.Projectile.alpha = 255;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.netImportant = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 150;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_024f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Calamity().stealthStrike && (float)base.Projectile.timeLeft % 20f == 4f)
		{
			CalamityUtils.ProjectileRain(base.Projectile.GetSource_FromThis(), base.Projectile.Center, 400f, 100f, 500f, 800f, 29f, ModContent.ProjectileType<HeavenfallenEnergy>(), base.Projectile.damage / 2, base.Projectile.knockBack * 0.5f, base.Projectile.owner);
		}
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 20;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		for (int i = 0; i < 2; i++)
		{
			int blueDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralBlue>(), 0f, 0f, 100);
			Main.dust[blueDust].noGravity = true;
			Dust obj = Main.dust[blueDust];
			obj.velocity *= 0f;
		}
		for (int j = 0; j < 2; j++)
		{
			int orangeDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100);
			Main.dust[orangeDust].noGravity = true;
			Dust obj2 = Main.dust[orangeDust];
			obj2.velocity *= 0f;
		}
		base.Projectile.rotation += 0.5f;
		base.Projectile.ai[0]++;
		if (Main.myPlayer != base.Projectile.owner || base.Projectile.ai[0] != 20f)
		{
			return;
		}
		if (Owner.channel)
		{
			float constant = 20f;
			float xfactor = (float)Main.mouseX + Main.screenPosition.X - base.Projectile.Center.X;
			float yfactor = (float)Main.mouseY + Main.screenPosition.Y - base.Projectile.Center.Y;
			if (Owner.gravDir == -1f)
			{
				yfactor = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - base.Projectile.Center.Y;
			}
			float factorAdjust = (float)Math.Sqrt(xfactor * xfactor + yfactor * yfactor);
			if (factorAdjust > constant)
			{
				factorAdjust = constant / factorAdjust;
				xfactor *= factorAdjust;
				yfactor *= factorAdjust;
				int num = (int)(xfactor * 1000f);
				int scaledXVel = (int)(base.Projectile.velocity.X * 1000f);
				int scaledY = (int)(yfactor * 1000f);
				int scaledYVel = (int)(base.Projectile.velocity.Y * 1000f);
				if (num != scaledXVel || scaledY != scaledYVel)
				{
					base.Projectile.netUpdate = true;
				}
				base.Projectile.velocity.X = xfactor;
				base.Projectile.velocity.Y = yfactor;
			}
			else
			{
				int num2 = (int)(xfactor * 1000f);
				int scaledXVel2 = (int)(base.Projectile.velocity.X * 1000f);
				int scaledY2 = (int)(yfactor * 1000f);
				int scaledYVel2 = (int)(base.Projectile.velocity.Y * 1000f);
				if (num2 != scaledXVel2 || scaledY2 != scaledYVel2)
				{
					base.Projectile.netUpdate = true;
				}
				base.Projectile.velocity.X = xfactor;
				base.Projectile.velocity.Y = yfactor;
			}
		}
		else if (base.Projectile.ai[0] == 20f)
		{
			base.Projectile.netUpdate = true;
			Vector2 centerPoint = base.Projectile.Center;
			float xfactor2 = (float)Main.mouseX + Main.screenPosition.X - centerPoint.X;
			float yfactor2 = (float)Main.mouseY + Main.screenPosition.Y - centerPoint.Y;
			if (Owner.gravDir == -1f)
			{
				yfactor2 = Main.screenPosition.Y + (float)Main.screenHeight - (float)Main.mouseY - centerPoint.Y;
			}
			float factorAdjust2 = (float)Math.Sqrt(xfactor2 * xfactor2 + yfactor2 * yfactor2);
			if (factorAdjust2 == 0f || base.Projectile.ai[0] < 0f)
			{
				centerPoint = Owner.Center;
				xfactor2 = base.Projectile.Center.X - centerPoint.X;
				yfactor2 = base.Projectile.Center.Y - centerPoint.Y;
				factorAdjust2 = (float)Math.Sqrt(xfactor2 * xfactor2 + yfactor2 * yfactor2);
			}
			factorAdjust2 = 20f / factorAdjust2;
			xfactor2 *= factorAdjust2;
			yfactor2 *= factorAdjust2;
			base.Projectile.velocity.X = xfactor2;
			base.Projectile.velocity.Y = yfactor2;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 240);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 240);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.position);
		for (int i = 0; i < 10; i++)
		{
			int dusty = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralBlue>(), 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[dusty].noGravity = true;
			Dust obj = Main.dust[dusty];
			obj.velocity *= 0f;
		}
		for (int j = 0; j < 10; j++)
		{
			int dusty2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[dusty2].noGravity = true;
			Dust obj2 = Main.dust[dusty2];
			obj2.velocity *= 0f;
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int k = 0; k < 5; k++)
			{
				Vector2 velocity = ((float)Math.PI * 2f * (float)k / 5f - (float)Math.PI / 2f).ToRotationVector2() * 4f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<HeavenfallenEnergy>(), base.Projectile.damage / 4, base.Projectile.knockBack * 0.5f, base.Projectile.owner, 0f, 1f);
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}
}
