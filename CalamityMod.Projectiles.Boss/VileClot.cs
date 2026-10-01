using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class VileClot : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.light = 0.6f;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 300;
	}

	public override void AI()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() < 12f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.01f;
		}
		int vileDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 75, base.Projectile.velocity.X * 0.1f, base.Projectile.velocity.Y * 0.1f, 100, default(Color), 1.5f);
		Main.dust[vileDust].noGravity = true;
		base.Projectile.rotation += 0.3f * (float)base.Projectile.direction;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(39, 60);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.Center);
		for (int i = 0; i < 6; i++)
		{
			int killDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 75, (0f - base.Projectile.velocity.X) * 0.2f, (0f - base.Projectile.velocity.Y) * 0.2f, 100, default(Color), 2.5f);
			Main.dust[killDust].noGravity = true;
			Dust obj = Main.dust[killDust];
			obj.velocity *= 2f;
			killDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 75, (0f - base.Projectile.velocity.X) * 0.2f, (0f - base.Projectile.velocity.Y) * 0.2f, 100, default(Color), 1.2f);
			Dust obj2 = Main.dust[killDust];
			obj2.velocity *= 2f;
		}
	}
}
