using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.NPCs.AstrumDeus;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class DeusMine : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle ExplodeSound = new SoundStyle("CalamityMod/Sounds/Custom/AstrumDeus/DeusMineExplode");

	private const int MaxTimeLeft = 600;

	private const int FadeTime = 85;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.hostile = true;
		base.Projectile.alpha = 100;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.ai[1] = 1f;
			SoundEngine.PlaySound(in AstrumDeusHead.MineSound, base.Projectile.Center);
		}
		if (base.Projectile.timeLeft < 85 && base.Projectile.ai[0] == 0f)
		{
			base.Projectile.damage = 0;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 12f, targetHitbox);
	}

	public override bool CanHitPlayer(Player target)
	{
		if (base.Projectile.timeLeft <= 515)
		{
			if (base.Projectile.timeLeft < 85)
			{
				return base.Projectile.ai[0] == 1f;
			}
			return true;
		}
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 515)
		{
			base.Projectile.localAI[1]++;
			byte b2 = (byte)((int)base.Projectile.localAI[1] * 3);
			byte a2 = (byte)((float)base.Projectile.alpha * ((float)(int)b2 / 255f));
			return new Color((int)b2, (int)b2, (int)b2, (int)a2);
		}
		if (base.Projectile.timeLeft < 85)
		{
			byte b3 = (byte)(base.Projectile.timeLeft * 3);
			if (base.Projectile.ai[0] == 0f)
			{
				byte a3 = (byte)((float)base.Projectile.alpha * ((float)(int)b3 / 255f));
				return new Color((int)b3, (int)b3, (int)b3, (int)a3);
			}
			return new Color(255, (int)b3, (int)b3, base.Projectile.alpha);
		}
		return new Color(255, 255, 255, base.Projectile.alpha);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] != 1f)
		{
			return;
		}
		SoundEngine.PlaySound(in ExplodeSound, base.Projectile.Center);
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 96);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int i = 0; i < 5; i++)
		{
			int purpleDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 173, 0f, 0f, 100, default(Color), 1.2f);
			Dust obj = Main.dust[purpleDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[purpleDust].scale = 0.5f;
				Main.dust[purpleDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 10; j++)
		{
			int astralDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100, default(Color), 1.7f);
			Main.dust[astralDust].noGravity = true;
			Dust obj2 = Main.dust[astralDust];
			obj2.velocity *= 1.5f;
			Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralOrange>(), 0f, 0f, 100);
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			int totalProjectiles = 4;
			float radians = (float)Math.PI * 2f / (float)totalProjectiles;
			int type = ModContent.ProjectileType<AstralShot2>();
			float velocity = 1f;
			double angleA = (double)radians * 0.5;
			double angleB = (double)MathHelper.ToRadians(90f) - angleA;
			float velocityX2 = (float)((double)velocity * Math.Sin(angleA) / Math.Sin(angleB));
			Vector2 spinningPoint = default(Vector2);
			((Vector2)(ref spinningPoint))._002Ector(0f - velocityX2, 0f - velocity);
			for (int k = 0; k < totalProjectiles; k++)
			{
				Vector2 velocity2 = spinningPoint.RotatedBy(radians * (float)k);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity2, type, AstrumDeusBody.LaserDamage, 0f, Main.myPlayer, 1f);
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && base.Projectile.timeLeft <= 515 && (base.Projectile.timeLeft >= 85 || base.Projectile.ai[0] != 0f))
		{
			target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 120);
		}
	}
}
