using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class LunarKunaiProj : ModProjectile, ILocalizedModType, IModType
{
	private bool lunarEnhance;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 2;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = Main.player[base.Projectile.owner].Calamity();
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] == 1f && modPlayer.StealthStrikeAvailable())
		{
			lunarEnhance = true;
		}
		else if (base.Projectile.ai[0] >= 50f)
		{
			lunarEnhance = true;
		}
		if (lunarEnhance)
		{
			base.Projectile.frame = 1;
		}
		else
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, lunarEnhance ? 300f : 150f, lunarEnhance ? 12f : 8f, 20f);
		if (Main.rand.NextBool(6) && lunarEnhance)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 229, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Nightwither>(), 180);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_027b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		if (lunarEnhance)
		{
			SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
			base.Projectile.position = base.Projectile.Center;
			base.Projectile.width = (base.Projectile.height = 28);
			base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
			base.Projectile.damage /= 5;
			base.Projectile.usesLocalNPCImmunity = true;
			base.Projectile.localNPCHitCooldown = 10;
			for (int i = 0; i < 10; i++)
			{
				int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 229, 0f, 0f, 0, default(Color), 1.5f);
				Main.dust[dust].noGravity = true;
				Dust obj = Main.dust[dust];
				obj.velocity *= 3f;
				dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 229, 0f, 0f, 100);
				Dust obj2 = Main.dust[dust];
				obj2.velocity *= 2f;
				Main.dust[dust].noGravity = true;
			}
			base.Projectile.Damage();
		}
		else
		{
			for (int j = 0; j < 5; j++)
			{
				int dusty = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 265, 0f, 0f, 100);
				Main.dust[dusty].noGravity = true;
				Dust obj3 = Main.dust[dusty];
				obj3.velocity *= 1.2f;
				Dust obj4 = Main.dust[dusty];
				obj4.velocity -= base.Projectile.oldVelocity * 0.3f;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}
}
