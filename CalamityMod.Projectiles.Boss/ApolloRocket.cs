using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class ApolloRocket : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 28;
		base.Projectile.height = 28;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.CooldownSlot = 1;
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
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0303: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0312: Unknown result type (might be due to invalid IL or missing references)
		//IL_033f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_0549: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_0559: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0424: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_0453: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_046b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0484: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0639: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_0650: Unknown result type (might be due to invalid IL or missing references)
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_065c: Unknown result type (might be due to invalid IL or missing references)
		//IL_065e: Unknown result type (might be due to invalid IL or missing references)
		//IL_066b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0693: Unknown result type (might be due to invalid IL or missing references)
		//IL_0698: Unknown result type (might be due to invalid IL or missing references)
		//IL_0731: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.position.Y > base.Projectile.ai[1])
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 4)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			float randDustSpeed1 = 1.8f;
			float randDustSpeed2 = 2.8f;
			float angleRandom = 0.35f;
			for (int i = 0; i < 20; i++)
			{
				Vector2 dustVel = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(randDustSpeed1, randDustSpeed2), 0f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
				dustVel = dustVel.RotatedBy(0f - angleRandom);
				dustVel = dustVel.RotatedByRandom(2f * angleRandom);
				int randomDustType = (Main.rand.NextBool() ? 107 : 110);
				int plasmaDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, randomDustType, dustVel.X, dustVel.Y, 200, default(Color), 1.7f);
				Main.dust[plasmaDust].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
				Main.dust[plasmaDust].noGravity = true;
				Dust obj = Main.dust[plasmaDust];
				obj.velocity *= 3f;
				_ = Main.dust[plasmaDust];
				plasmaDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, randomDustType, dustVel.X, dustVel.Y, 100, default(Color), 0.8f);
				Main.dust[plasmaDust].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
				Dust obj2 = Main.dust[plasmaDust];
				obj2.velocity *= 2f;
				Main.dust[plasmaDust].noGravity = true;
				Main.dust[plasmaDust].fadeIn = 1f;
				Main.dust[plasmaDust].color = Color.Green * 0.5f;
				_ = Main.dust[plasmaDust];
			}
			for (int j = 0; j < 10; j++)
			{
				Vector2 dustVel2 = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(randDustSpeed1, randDustSpeed2), 0f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
				dustVel2 = dustVel2.RotatedBy(0f - angleRandom);
				dustVel2 = dustVel2.RotatedByRandom(2f * angleRandom);
				int randomDustType2 = (Main.rand.NextBool() ? 107 : 110);
				int plasmaDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, randomDustType2, dustVel2.X, dustVel2.Y, 0, default(Color), 2f);
				Main.dust[plasmaDust2].position = base.Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(base.Projectile.velocity.ToRotation()) * (float)base.Projectile.width / 3f;
				Main.dust[plasmaDust2].noGravity = true;
				Dust obj3 = Main.dust[plasmaDust2];
				obj3.velocity *= 0.5f;
				_ = Main.dust[plasmaDust2];
			}
		}
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		Lighting.AddLight(base.Projectile.Center, 0.2f, 1f, 0f);
		int target = Player.FindClosest(base.Projectile.Center, 1, 1);
		Vector2 distanceFromTarget = Main.player[target].Center - base.Projectile.Center;
		float stopHomingDistance = (death ? 160f : (revenge ? 180f : (expertMode ? 200f : 240f)));
		if (((Vector2)(ref distanceFromTarget)).Length() < stopHomingDistance || base.Projectile.ai[0] == 1f || base.Projectile.timeLeft < 480)
		{
			base.Projectile.ai[0] = 1f;
			if (((Vector2)(ref base.Projectile.velocity)).Length() < 24f)
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 1.05f;
			}
			return;
		}
		float scaleFactor = ((Vector2)(ref base.Projectile.velocity)).Length();
		float inertia = (death ? 8f : (revenge ? 9f : (expertMode ? 10f : 12f)));
		((Vector2)(ref distanceFromTarget)).Normalize();
		distanceFromTarget *= scaleFactor;
		base.Projectile.velocity = (base.Projectile.velocity * inertia + distanceFromTarget) / (inertia + 1f);
		((Vector2)(ref base.Projectile.velocity)).Normalize();
		Projectile projectile2 = base.Projectile;
		projectile2.velocity *= scaleFactor;
		float pushForce = (death ? 0.06f : (revenge ? 0.055f : (expertMode ? 0.05f : 0.04f)));
		float pushDistance = (death ? 100f : (revenge ? 90f : (expertMode ? 80f : 60f)));
		for (int k = 0; k < Main.maxProjectiles; k++)
		{
			Projectile otherProj = Main.projectile[k];
			if (!otherProj.active || k == base.Projectile.whoAmI)
			{
				continue;
			}
			bool num = otherProj.type == base.Projectile.type;
			float taxicabDist = Vector2.Distance(base.Projectile.Center, otherProj.Center);
			if (num && taxicabDist < pushDistance)
			{
				if (base.Projectile.position.X < otherProj.position.X)
				{
					base.Projectile.velocity.X -= pushForce;
				}
				else
				{
					base.Projectile.velocity.X += pushForce;
				}
				if (base.Projectile.position.Y < otherProj.position.Y)
				{
					base.Projectile.velocity.Y -= pushForce;
				}
				else
				{
					base.Projectile.velocity.Y += pushForce;
				}
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int height = texture.Height / Main.projFrames[base.Type];
		int drawStart = height * base.Projectile.frame;
		Vector2 origin = base.Projectile.Size / 2f;
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/ApolloRocketGlow", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, drawStart, texture.Width, height), Color.White, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0, 0f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_042c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0436: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		int height = 90;
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = height);
		base.Projectile.Center = base.Projectile.position;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		for (int i = 0; i < 12; i++)
		{
			int randomDustType = (Main.rand.NextBool() ? 107 : 110);
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, randomDustType, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[dust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[dust].scale = 0.5f;
				Main.dust[dust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 15; j++)
		{
			int randomDustType2 = (Main.rand.NextBool() ? 107 : 110);
			int dust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, randomDustType2, 0f, 0f, 100, default(Color), 3f);
			Main.dust[dust2].noGravity = true;
			Dust obj2 = Main.dust[dust2];
			obj2.velocity *= 5f;
			dust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, randomDustType2, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[dust2];
			obj3.velocity *= 2f;
		}
		if (Main.dedServ)
		{
			return;
		}
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
}
