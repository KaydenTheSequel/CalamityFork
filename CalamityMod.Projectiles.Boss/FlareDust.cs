using System;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class FlareDust : ModProjectile, ILocalizedModType, IModType
{
	private bool start = true;

	private float startingPosX;

	private float startingPosY;

	private double distance;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/Boss/FlareBomb";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 64;
		base.Projectile.height = 66;
		base.Projectile.scale = 1.5f;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 680;
		base.CooldownSlot = 1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(start);
		writer.Write(startingPosX);
		writer.Write(startingPosY);
		writer.Write(distance);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		start = reader.ReadBoolean();
		startingPosX = reader.ReadSingle();
		startingPosY = reader.ReadSingle();
		distance = reader.ReadDouble();
	}

	public override void AI()
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		Lighting.AddLight(base.Projectile.Center, 0.5f, 0.25f, 0f);
		if (base.Projectile.ai[0] >= 2f)
		{
			return;
		}
		if (start)
		{
			startingPosX = base.Projectile.Center.X;
			startingPosY = base.Projectile.Center.Y;
			start = false;
		}
		float earlyTimeProjSpeed = base.Projectile.localAI[0] / 120f;
		if (earlyTimeProjSpeed > 1f)
		{
			earlyTimeProjSpeed = 1f;
		}
		distance += MathHelper.Lerp(1f, 11f, earlyTimeProjSpeed);
		if (base.Projectile.timeLeft < 380)
		{
			float longTimeProjSpeed = (base.Projectile.localAI[0] - 300f) / 240f;
			if (longTimeProjSpeed > 1f)
			{
				longTimeProjSpeed = 1f;
			}
			distance += MathHelper.Lerp(1f, 11f, longTimeProjSpeed);
		}
		double rad = MathHelper.ToRadians(base.Projectile.ai[1]);
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.position.X = startingPosX - (float)(int)(Math.Sin(rad) * distance) - (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = startingPosY - (float)(int)(Math.Cos(rad) * distance) - (float)(base.Projectile.height / 2);
		}
		else
		{
			base.Projectile.position.X = startingPosX - (float)(int)(Math.Cos(rad) * distance) - (float)(base.Projectile.width / 2);
			base.Projectile.position.Y = startingPosY - (float)(int)(Math.Sin(rad) * distance) - (float)(base.Projectile.height / 2);
		}
		base.Projectile.ai[1] += (1.1f - earlyTimeProjSpeed) * 0.5f;
		base.Projectile.localAI[0]++;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, base.Projectile.alpha);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int framing = texture.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)framing / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0278: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		base.Projectile.ExpandHitboxBy(48);
		for (int d = 0; d < 2; d++)
		{
			int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 100);
			Dust obj = Main.dust[idx];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[idx].scale = 0.5f;
				Main.dust[idx].fadeIn = 1f + Main.rand.NextFloat(0.1f, 1f);
			}
		}
		for (int i = 0; i < 4; i++)
		{
			int idx2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 100, default(Color), 2f);
			Main.dust[idx2].noGravity = true;
			Dust obj2 = Main.dust[idx2];
			obj2.velocity *= 5f;
			idx2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 244, 0f, 0f, 100);
			Dust obj3 = Main.dust[idx2];
			obj3.velocity *= 2f;
		}
		if (!Main.dedServ)
		{
			Vector2 goreSource = base.Projectile.Center;
			int goreAmt = 3;
			Vector2 source = default(Vector2);
			((Vector2)(ref source))._002Ector(goreSource.X - 24f, goreSource.Y - 24f);
			for (int goreIndex = 0; goreIndex < goreAmt; goreIndex++)
			{
				float velocityMult = 0.33f;
				if (goreIndex < goreAmt / 3)
				{
					velocityMult = 0.66f;
				}
				if (goreIndex >= 2 * goreAmt / 3)
				{
					velocityMult = 1f;
				}
				ModContent.GetInstance<CalamityMod>();
				int type = Main.rand.Next(61, 64);
				int smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				Gore obj4 = Main.gore[smoke];
				obj4.velocity *= velocityMult;
				obj4.velocity.X++;
				obj4.velocity.Y++;
				type = Main.rand.Next(61, 64);
				smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				Gore obj5 = Main.gore[smoke];
				obj5.velocity *= velocityMult;
				obj5.velocity.X--;
				obj5.velocity.Y++;
				type = Main.rand.Next(61, 64);
				smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				Gore obj6 = Main.gore[smoke];
				obj6.velocity *= velocityMult;
				obj6.velocity.X++;
				obj6.velocity.Y--;
				type = Main.rand.Next(61, 64);
				smoke = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
				Gore obj7 = Main.gore[smoke];
				obj7.velocity *= velocityMult;
				obj7.velocity.X--;
				obj7.velocity.Y--;
			}
		}
		base.Projectile.Damage();
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 16f * base.Projectile.scale, targetHitbox);
	}

	public override bool CanHitPlayer(Player target)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Rectangle targetHitbox = target.Hitbox;
		float num = Vector2.Distance(base.Projectile.Center, targetHitbox.TopLeft());
		float dist2 = Vector2.Distance(base.Projectile.Center, targetHitbox.TopRight());
		float dist3 = Vector2.Distance(base.Projectile.Center, targetHitbox.BottomLeft());
		float dist4 = Vector2.Distance(base.Projectile.Center, targetHitbox.BottomRight());
		float minDist = num;
		if (dist2 < minDist)
		{
			minDist = dist2;
		}
		if (dist3 < minDist)
		{
			minDist = dist3;
		}
		if (dist4 < minDist)
		{
			minDist = dist4;
		}
		return minDist <= 16f * base.Projectile.scale;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Dragonfire>(), 60);
		}
	}
}
