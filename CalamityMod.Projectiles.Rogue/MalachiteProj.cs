using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class MalachiteProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 10;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.alpha = 255;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 3;
		base.Projectile.alpha = 255;
		base.Projectile.extraUpdates = 3;
		base.Projectile.aiStyle = 93;
		base.AIType = 514;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.alpha -= 3;
		if (base.Projectile.alpha < 30)
		{
			base.Projectile.alpha = 30;
		}
		base.Projectile.localAI[1]++;
		if (base.Projectile.localAI[1] > 4f)
		{
			for (int k = 0; k < 3; k++)
			{
				int dusty = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 107, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 0.75f);
				Main.dust[dusty].noGravity = true;
				Dust obj = Main.dust[dusty];
				obj.velocity *= 0f;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return new Color(Main.DiscoR, 203, 103, base.Projectile.alpha);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 120);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0203: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 48);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int i = 0; i < 7; i++)
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 107, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 1.2f);
			Dust obj = Main.dust[dust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[dust].scale = 0.5f;
				Main.dust[dust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 15; j++)
		{
			int dust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 107, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103), 1.7f);
			Main.dust[dust2].noGravity = true;
			Dust obj2 = Main.dust[dust2];
			obj2.velocity *= 5f;
			dust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 107, 0f, 0f, 100, new Color(Main.DiscoR, 203, 103));
			Dust obj3 = Main.dust[dust2];
			obj3.velocity *= 2f;
		}
	}
}
