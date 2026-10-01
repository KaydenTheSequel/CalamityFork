using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Environment;

public class BrimstoneGeyser : ModProjectile, ILocalizedModType, IModType
{
	private int dustType = 235;

	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 120;
		base.Projectile.hostile = true;
		base.Projectile.penetrate = -1;
		base.Projectile.trap = true;
	}

	public override void AI()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_06be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_0534: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0262: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_054f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0581: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_05aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_062a: Unknown result type (might be due to invalid IL or missing references)
		//IL_062f: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_06eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0713: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0718: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_0759: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_087c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0881: Unknown result type (might be due to invalid IL or missing references)
		//IL_0898: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0826: Unknown result type (might be due to invalid IL or missing references)
		//IL_0902: Unknown result type (might be due to invalid IL or missing references)
		//IL_0926: Unknown result type (might be due to invalid IL or missing references)
		//IL_092c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0953: Unknown result type (might be due to invalid IL or missing references)
		//IL_0962: Unknown result type (might be due to invalid IL or missing references)
		//IL_0967: Unknown result type (might be due to invalid IL or missing references)
		//IL_096c: Unknown result type (might be due to invalid IL or missing references)
		int isActive = Math.Sign(base.Projectile.velocity.Y);
		int dustCustomData = ((isActive != -1) ? 1 : 0);
		if (base.Projectile.ai[0] == 0f)
		{
			if (!Collision.SolidCollision(base.Projectile.position + new Vector2(0f, (isActive == -1) ? ((float)(base.Projectile.height - 48)) : 0f), base.Projectile.width, 48) && !Collision.WetCollision(base.Projectile.position + new Vector2(0f, (isActive == -1) ? ((float)(base.Projectile.height - 20)) : 0f), base.Projectile.width, 20))
			{
				base.Projectile.velocity = new Vector2(0f, (float)Math.Sign(base.Projectile.velocity.Y) * 0.001f);
				base.Projectile.ai[0] = 1f;
				base.Projectile.ai[1] = 0f;
				base.Projectile.timeLeft = 60;
			}
			base.Projectile.ai[1]++;
			if (base.Projectile.ai[1] >= 60f)
			{
				base.Projectile.Kill();
			}
			for (int i = 0; i < 3; i++)
			{
				int brimDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 0f, 0f, 100);
				Main.dust[brimDust].scale = 0.1f + (float)Main.rand.Next(5) * 0.1f;
				Main.dust[brimDust].fadeIn = 1.5f + (float)Main.rand.Next(5) * 0.1f;
				Main.dust[brimDust].noGravity = true;
				Main.dust[brimDust].position = base.Projectile.Center + Utils.RotatedBy(new Vector2(0f, (float)(-base.Projectile.height / 2)), (double)base.Projectile.rotation, default(Vector2)) * 1.1f;
			}
		}
		if (base.Projectile.ai[0] != 1f)
		{
			return;
		}
		base.Projectile.velocity = new Vector2(0f, (float)Math.Sign(base.Projectile.velocity.Y) * 0.001f);
		if (isActive != 0)
		{
			int heightIncrease = 16;
			for (int maxHeight = 320; heightIncrease < maxHeight && !Collision.SolidCollision(base.Projectile.position + new Vector2(0f, (isActive == -1) ? ((float)(base.Projectile.height - heightIncrease - 16)) : 0f), base.Projectile.width, heightIncrease + 16); heightIncrease += 16)
			{
			}
			if (isActive == -1)
			{
				base.Projectile.position.Y += base.Projectile.height;
				base.Projectile.height = heightIncrease;
				base.Projectile.position.Y -= heightIncrease;
			}
			else
			{
				base.Projectile.height = heightIncrease;
			}
		}
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] >= 60f)
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			for (int j = 0; j < 60; j++)
			{
				int brimDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 0f, -2.5f * (float)(-isActive));
				Dust dust = Main.dust[brimDust2];
				dust.alpha = 200;
				dust.velocity *= new Vector2(0.3f, 2f);
				dust.velocity.Y += 2 * isActive;
				dust.scale += Main.rand.NextFloat();
				dust.position = new Vector2(base.Projectile.Center.X, base.Projectile.Center.Y + (float)base.Projectile.height * 0.5f * (float)(-isActive));
				dust.customData = dustCustomData;
				if (isActive == -1 && !Main.rand.NextBool(4))
				{
					dust.velocity.Y -= 0.2f;
				}
			}
			SoundEngine.PlaySound(in SoundID.Item34, base.Projectile.Center);
		}
		if (isActive == 1)
		{
			for (int k = 0; k < 9; k++)
			{
				int brimDust3 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 0f, -2.5f * (float)(-isActive));
				Dust obj = Main.dust[brimDust3];
				obj.alpha = 200;
				obj.velocity *= new Vector2(0.3f, 2f);
				obj.velocity.Y += 2 * isActive;
				obj.scale += Main.rand.NextFloat();
				obj.position = new Vector2(base.Projectile.Center.X, base.Projectile.Center.Y + (float)base.Projectile.height * 0.5f * (float)(-isActive));
				obj.customData = dustCustomData;
				if (isActive == -1 && !Main.rand.NextBool(4))
				{
					Main.dust[brimDust3].velocity.Y -= 0.2f;
				}
			}
		}
		int Height = (int)(base.Projectile.ai[1] / 60f * (float)base.Projectile.height) * 3;
		if (Height > base.Projectile.height)
		{
			Height = base.Projectile.height;
		}
		Vector2 Position = base.Projectile.position + (Vector2)((isActive == -1) ? new Vector2(0f, (float)(base.Projectile.height - Height)) : Vector2.Zero);
		Vector2 vector2 = base.Projectile.position + (Vector2)((isActive == -1) ? new Vector2(0f, (float)base.Projectile.height) : Vector2.Zero);
		for (int l = 0; l < 6; l++)
		{
			if (Main.rand.Next(3) < 2)
			{
				int brimDust4 = Dust.NewDust(Position, base.Projectile.width, Height, dustType, 0f, 0f, 90, default(Color), 2.5f);
				Dust dust2 = Main.dust[brimDust4];
				dust2.noGravity = true;
				dust2.fadeIn = 1f;
				if (dust2.velocity.Y > 0f)
				{
					dust2.velocity.Y *= -1f;
				}
				if (Main.rand.Next(6) < 3)
				{
					dust2.position.Y = MathHelper.Lerp(dust2.position.Y, vector2.Y, 0.5f);
					dust2.velocity *= 5f;
					dust2.velocity.Y -= 3f;
					dust2.position.X = base.Projectile.Center.X;
					dust2.noGravity = false;
					dust2.noLight = true;
					dust2.fadeIn = 0.4f;
					dust2.scale *= 0.3f;
				}
				else
				{
					Main.dust[brimDust4].velocity = base.Projectile.DirectionFrom(Main.dust[brimDust4].position) * ((Vector2)(ref Main.dust[brimDust4].velocity)).Length() * 0.25f;
				}
				Main.dust[brimDust4].velocity.Y *= -isActive;
				Main.dust[brimDust4].customData = dustCustomData;
			}
		}
		for (int m = 0; m < 6; m++)
		{
			if (Main.rand.NextFloat() >= 0.5f)
			{
				int brimDust5 = Dust.NewDust(Position, base.Projectile.width, Height, dustType, 0f, -2.5f * (float)(-isActive));
				Dust dust3 = Main.dust[brimDust5];
				dust3.alpha = 200;
				dust3.velocity *= new Vector2(0.6f, 1.5f);
				dust3.scale += Main.rand.NextFloat();
				if (isActive == -1 && !Main.rand.NextBool(4))
				{
					dust3.velocity.Y -= 0.2f;
				}
				dust3.customData = dustCustomData;
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 90);
		}
	}
}
