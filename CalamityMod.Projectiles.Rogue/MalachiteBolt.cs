using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class MalachiteBolt : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/Rogue/MalachiteProj";

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
		base.Projectile.penetrate = 2;
		base.Projectile.alpha = 255;
		base.Projectile.extraUpdates = 10;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		base.Projectile.alpha -= 3;
		if (base.Projectile.alpha < 100)
		{
			base.Projectile.alpha = 100;
		}
		base.Projectile.localAI[1]++;
		if (base.Projectile.localAI[1] > 4f)
		{
			for (int i = 0; i < 3; i++)
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
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<Plague>(), 120);
		if (base.Projectile.penetrate > 1)
		{
			return;
		}
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 160);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		for (int j = 0; j < 70; j++)
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
		for (int k = 0; k < 40; k++)
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

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.penetrate > 1)
		{
			return;
		}
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 160);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		for (int j = 0; j < 70; j++)
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
		for (int k = 0; k < 40; k++)
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
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int j = 0; j < 7; j++)
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
		for (int k = 0; k < 3; k++)
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
