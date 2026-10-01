using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class Waterfall : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.extraUpdates = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 8;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft /= 2;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 2; i++)
		{
			float shortXVel = base.Projectile.velocity.X / 3f * (float)i;
			float shortYVel = base.Projectile.velocity.Y / 3f * (float)i;
			int fourConst = 4;
			int watery = Dust.NewDust(new Vector2(base.Projectile.position.X + (float)fourConst, base.Projectile.position.Y + (float)fourConst), base.Projectile.width - fourConst * 2, base.Projectile.height - fourConst * 2, 56, 0f, 0f, 100, default(Color), 1.2f);
			Main.dust[watery].noGravity = true;
			Dust obj = Main.dust[watery];
			obj.velocity *= 0.25f;
			Dust obj2 = Main.dust[watery];
			obj2.velocity += base.Projectile.velocity * 0.1f;
			Main.dust[watery].position.X -= shortXVel;
			Main.dust[watery].position.Y -= shortYVel;
		}
		for (int j = 0; j < 2; j++)
		{
			float shortXVel2 = base.Projectile.velocity.X / 3f * (float)j;
			float shortYVel2 = base.Projectile.velocity.Y / 3f * (float)j;
			int otherFourConst = 4;
			int superWet = Dust.NewDust(new Vector2(base.Projectile.position.X + (float)otherFourConst, base.Projectile.position.Y + (float)otherFourConst), base.Projectile.width - otherFourConst * 2, base.Projectile.height - otherFourConst * 2, 245, 0f, 0f, 100, default(Color), 1.2f);
			Main.dust[superWet].noGravity = true;
			Dust obj3 = Main.dust[superWet];
			obj3.velocity *= 0.1f;
			Dust obj4 = Main.dust[superWet];
			obj4.velocity += base.Projectile.velocity * 0.25f;
			Main.dust[superWet].position.X -= shortXVel2;
			Main.dust[superWet].position.Y -= shortYVel2;
		}
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] >= 60f)
		{
			base.Projectile.tileCollide = true;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.penetrate--;
		if (base.Projectile.penetrate <= 0)
		{
			base.Projectile.Kill();
		}
		else
		{
			if (base.Projectile.velocity.X != oldVelocity.X)
			{
				base.Projectile.velocity.X = 0f - oldVelocity.X;
			}
			if (base.Projectile.velocity.Y != oldVelocity.Y)
			{
				base.Projectile.velocity.Y = 0f - oldVelocity.Y;
			}
			SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.Center);
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.Center);
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 56, base.Projectile.oldVelocity.X * 0.25f, base.Projectile.oldVelocity.Y * 0.25f);
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 245, base.Projectile.oldVelocity.X * 0.25f, base.Projectile.oldVelocity.Y * 0.25f);
		}
	}
}
