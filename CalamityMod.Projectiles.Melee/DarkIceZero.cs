using System;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class DarkIceZero : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetDefaults()
	{
		base.Projectile.width = 28;
		base.Projectile.height = 28;
		base.Projectile.aiStyle = 1;
		base.AIType = 14;
		base.Projectile.timeLeft = 600;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 1;
		base.Projectile.ignoreWater = true;
		base.Projectile.extraUpdates = 1;
		base.Projectile.coldDamage = true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		if (base.Projectile.timeLeft > 595)
		{
			return false;
		}
		return true;
	}

	public override void AI()
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y) < 16f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.045f;
		}
		int dustSpawns = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 172, base.Projectile.velocity.X, base.Projectile.velocity.Y, 0, default(Color), 1.25f);
		Main.dust[dustSpawns].noGravity = true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(324, 180);
		target.AddBuff(ModContent.BuffType<GlacialState>(), 30);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return new Color(198, 197, 246);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		if (timeLeft > 0)
		{
			base.Projectile.position = base.Projectile.Center;
			base.Projectile.width = (base.Projectile.height = 192);
			base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
			base.Projectile.maxPenetrate = -1;
			base.Projectile.penetrate = -1;
			base.Projectile.usesLocalNPCImmunity = true;
			base.Projectile.localNPCHitCooldown = 10;
			base.Projectile.damage /= 2;
			base.Projectile.Damage();
			SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.Center);
			for (int i = 0; i < 30; i++)
			{
				int dustSpawns = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 172, 0f, 0f, 0, default(Color), Main.rand.NextFloat(1f, 2f));
				Main.dust[dustSpawns].noGravity = true;
				Dust obj = Main.dust[dustSpawns];
				obj.velocity *= 4f;
			}
			for (int j = 0; j < 20; j++)
			{
				int dustSpawns2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 68, 0f, 0f, 0, default(Color), 1.3f);
				Main.dust[dustSpawns2].noGravity = true;
				Dust obj2 = Main.dust[dustSpawns2];
				obj2.velocity *= 1.5f;
			}
		}
	}
}
