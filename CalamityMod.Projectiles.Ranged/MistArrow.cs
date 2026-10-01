using System;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class MistArrow : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.aiStyle = 1;
		base.Projectile.timeLeft = 300;
		base.AIType = 1;
		base.Projectile.arrow = true;
		base.Projectile.coldDamage = true;
	}

	public override void AI()
	{
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 9)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 2)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.alpha > 5)
		{
			base.Projectile.alpha -= 15;
		}
		if (base.Projectile.alpha < 5)
		{
			base.Projectile.alpha = 5;
		}
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI)) + MathHelper.ToRadians(90f) * (float)base.Projectile.direction;
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 4f)
		{
			Vector2 dspeed = -base.Projectile.velocity * Main.rand.NextFloat(0.3f, 0.6f);
			int whiteDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31, 0f, 0f, 100, new Color(237, 242, 242, 200), 1.2f);
			Main.dust[whiteDust].noGravity = true;
			Main.dust[whiteDust].velocity = dspeed;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 31, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f, 100, new Color(237, 242, 242, 200));
		}
		int mistAmt = 2;
		for (int m = 0; m < mistAmt; m++)
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				int frostMist = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, new Vector2(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f)), ModContent.ProjectileType<MistArrowFrostMist>(), (int)((float)base.Projectile.damage * 0.35f), (int)(base.Projectile.knockBack * 0.5f), Main.myPlayer, Main.rand.Next(3));
				Main.projectile[frostMist].rotation = Main.rand.NextFloat(-(float)Math.PI, (float)Math.PI);
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(324, 180);
		target.AddBuff(ModContent.BuffType<GlacialState>(), 60);
	}
}
