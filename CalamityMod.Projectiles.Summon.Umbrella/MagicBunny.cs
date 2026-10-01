using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.Umbrella;

public class MagicBunny : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.friendly = true;
		base.Projectile.width = 28;
		base.Projectile.height = 28;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 3000;
			SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
		}
		if (base.Projectile.velocity.X > 0f)
		{
			base.Projectile.direction = 1;
		}
		else if (base.Projectile.velocity.X < 0f)
		{
			base.Projectile.direction = -1;
		}
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.ai[0]++;
		base.Projectile.rotation += base.Projectile.velocity.X * 0.05f + (float)base.Projectile.direction * 0.05f;
		if (base.Projectile.ai[0] >= 18f)
		{
			base.Projectile.velocity.Y += 0.28f;
			base.Projectile.velocity.X *= 0.99f;
		}
		if (base.Projectile.velocity.Y > 15.9f)
		{
			base.Projectile.velocity.Y = 15.9f;
		}
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 50;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		if (Math.Abs(base.Projectile.velocity.X) < 0.2f && Math.Abs(base.Projectile.velocity.Y) < 0.2f)
		{
			base.Projectile.Kill();
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = (0f - oldVelocity.X) * 0.5f;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = (0f - oldVelocity.Y) * 0.5f;
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0332: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCDeath1, base.Projectile.position);
		int idx = Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.position, new Vector2(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f)), 76);
		Gore gore = Main.gore[idx];
		gore.velocity -= base.Projectile.velocity * 0.5f;
		idx = Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.position, new Vector2(Main.rand.NextFloat(-4f, 4f), Main.rand.NextFloat(-4f, 4f)), 77);
		gore.velocity -= base.Projectile.velocity * 0.5f;
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
		for (int i = 0; i < 20; i++)
		{
			int index = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31, 0f, 0f, 100, default(Color), 1.5f);
			Dust obj = Main.dust[index];
			obj.velocity *= 1.4f;
		}
		for (int j = 0; j < 10; j++)
		{
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 2.5f);
			Dust obj2 = Main.dust[index2];
			obj2.noGravity = true;
			obj2.velocity *= 5f;
			index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
			obj2.velocity *= 3f;
		}
		idx = Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.position, default(Vector2), Main.rand.Next(61, 64));
		gore.velocity *= 0.4f;
		gore.velocity.X++;
		gore.velocity.Y++;
		idx = Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.position, default(Vector2), Main.rand.Next(61, 64));
		gore.velocity *= 0.4f;
		gore.velocity.X--;
		gore.velocity.Y++;
		idx = Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.position, default(Vector2), Main.rand.Next(61, 64));
		gore.velocity *= 0.4f;
		gore.velocity.X++;
		gore.velocity.Y--;
		idx = Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.position, default(Vector2), Main.rand.Next(61, 64));
		gore.velocity *= 0.4f;
		gore.velocity.X--;
		gore.velocity.Y--;
		base.Projectile.ExpandHitboxBy(128);
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Color.White;
	}
}
