using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class IceRain : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.scale = 1.2f;
		base.Projectile.hostile = true;
		base.Projectile.coldDamage = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 600;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight((int)((base.Projectile.position.X + (float)(base.Projectile.width / 2)) / 16f), (int)((base.Projectile.position.Y + (float)(base.Projectile.height / 2)) / 16f), 0f, 0.25f, 0.25f);
		if (base.Projectile.ai[0] == 0f)
		{
			if (((Vector2)(ref base.Projectile.velocity)).Length() < base.Projectile.ai[1])
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 1.015f;
			}
			base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
			for (int i = 0; i < 2; i++)
			{
				int icyDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 92, base.Projectile.velocity.X, base.Projectile.velocity.Y, 50, default(Color), 0.6f);
				Main.dust[icyDust].noGravity = true;
				Dust obj = Main.dust[icyDust];
				obj.velocity *= 0.3f;
			}
		}
		else if (base.Projectile.ai[0] == 1f)
		{
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 10f)
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= ((CalamityWorld.revenge || BossRushEvent.BossRushActive) ? 1.03f : 1.025f);
			}
			base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
			for (int j = 0; j < 2; j++)
			{
				int icyDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 92, base.Projectile.velocity.X, base.Projectile.velocity.Y, 50, default(Color), 0.6f);
				Main.dust[icyDust2].noGravity = true;
				Dust obj2 = Main.dust[icyDust2];
				obj2.velocity *= 0.3f;
			}
		}
		else if (base.Projectile.ai[0] == 2f)
		{
			base.Projectile.velocity.Y += 0.1f;
			base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
			if (base.Projectile.velocity.Y > 6f)
			{
				base.Projectile.velocity.Y = 6f;
			}
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		return new Color(1f, 1f, 1f, 1f) * base.Projectile.Opacity;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.DrawProjectileWithBackglow(Cryogen.BackglowColor, lightColor, 4f, null, null, (SpriteEffects)0);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		for (int j = 0; j < 3; j++)
		{
			int snowDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 76);
			Main.dust[snowDust].noGravity = true;
			Main.dust[snowDust].noLight = true;
			Main.dust[snowDust].scale = 0.7f;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(44, 120);
		}
	}
}
