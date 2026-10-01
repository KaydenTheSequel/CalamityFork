using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SeasSearingSecondary : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
	}

	public override void AI()
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.scale <= 3.6f)
		{
			base.Projectile.scale *= 1.01f;
			base.Projectile.width = (int)(16f * base.Projectile.scale);
			base.Projectile.height = (int)(32f * base.Projectile.scale);
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] > 4f)
		{
			for (int i = 0; i < 3; i++)
			{
				int aquaDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 217, 0f, 0f, 100, new Color(60, Main.DiscoG, 190), base.Projectile.scale);
				Main.dust[aquaDust].noGravity = true;
				Dust obj = Main.dust[aquaDust];
				obj.velocity *= 0f;
				int waterDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 202, 0f, 0f, 100, new Color(60, Main.DiscoG, 190), base.Projectile.scale);
				Main.dust[waterDust].noGravity = true;
				Dust obj2 = Main.dust[waterDust];
				obj2.velocity *= 0f;
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			int endoftime = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, 0f, 0f, ModContent.ProjectileType<TyphoonBubble>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
			Main.projectile[endoftime].localAI[1] = 1f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(70, 180);
	}
}
