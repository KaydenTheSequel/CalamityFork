using System;
using System.IO;
using CalamityMod.NPCs;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class AresPlasmaFireball : ModProjectile, ILocalizedModType, IModType
{
	private const int timeLeft = 120;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 36;
		base.Projectile.height = 36;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.Opacity = 0f;
		base.CooldownSlot = 1;
		base.Projectile.timeLeft = 120;
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
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_027d: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_034c: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_040f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0502: Unknown result type (might be due to invalid IL or missing references)
		//IL_0507: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] != -1f && Vector2.Distance(new Vector2(base.Projectile.ai[0], base.Projectile.ai[1]), base.Projectile.Center) < 80f)
		{
			base.Projectile.tileCollide = true;
		}
		int fadeInTime = 3;
		base.Projectile.Opacity = MathHelper.Clamp(1f - (float)(base.Projectile.timeLeft - (120 - fadeInTime)) / (float)fadeInTime, 0f, 1f);
		Lighting.AddLight(base.Projectile.Center, 0f, 0.6f * base.Projectile.Opacity, 0f);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 5)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) - (float)Math.PI / 2f;
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			float randDustSpeed1 = 1.8f;
			float randDustSpeed2 = 2.8f;
			float angleRandom = 0.35f;
			for (int i = 0; i < 40; i++)
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
				plasmaDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, randomDustType, dustVel.X, dustVel.Y, 100, default(Color), 0.8f);
				Main.dust[plasmaDust].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
				Dust obj2 = Main.dust[plasmaDust];
				obj2.velocity *= 2f;
				Main.dust[plasmaDust].noGravity = true;
				Main.dust[plasmaDust].fadeIn = 1f;
				Main.dust[plasmaDust].color = Color.Green * 0.5f;
			}
			for (int j = 0; j < 20; j++)
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
			}
		}
	}

	public override bool CanHitPlayer(Player target)
	{
		return base.Projectile.Opacity == 1f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		((Color)(ref lightColor)).R = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).G = (byte)(255f * base.Projectile.Opacity);
		((Color)(ref lightColor)).B = (byte)(255f * base.Projectile.Opacity);
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		int height = 90;
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = height);
		base.Projectile.Center = base.Projectile.position;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in CommonCalamitySounds.ExoPlasmaExplosionSound, base.Projectile.Center);
		if (Main.myPlayer == base.Projectile.owner && base.Projectile.ai[1] != -1f)
		{
			bool splitNormal = true;
			if (CalamityGlobalNPC.draedonExoMechPrimePlasmaCannon != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechPrimePlasmaCannon].active)
			{
				splitNormal = Main.npc[CalamityGlobalNPC.draedonExoMechPrimePlasmaCannon].ai[3] % 2f == 0f;
			}
			int totalProjectiles = 8;
			if (base.Projectile.ai[0] == -1f)
			{
				totalProjectiles /= 2;
			}
			float radians = (float)Math.PI * 2f / (float)totalProjectiles;
			int type = ModContent.ProjectileType<AresPlasmaBolt>();
			float velocity = 0.5f;
			double angleA = (double)radians * 0.5;
			double angleB = (double)MathHelper.ToRadians(90f) - angleA;
			float velocityX2 = (float)((double)velocity * Math.Sin(angleA) / Math.Sin(angleB));
			Vector2 spinningPoint = (splitNormal ? new Vector2(0f, 0f - velocity) : new Vector2(0f - velocityX2, 0f - velocity));
			for (int k = 0; k < totalProjectiles; k++)
			{
				Vector2 velocity2 = spinningPoint.RotatedBy(radians * (float)k);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity2, type, AresPlasmaFlamethrower.BoltDamage, 0f, Main.myPlayer);
			}
		}
		for (int i = 0; i < 200; i++)
		{
			float dustScale = 16f;
			if (i < 150)
			{
				dustScale = 12f;
			}
			if (i < 100)
			{
				dustScale = 8f;
			}
			if (i < 50)
			{
				dustScale = 4f;
			}
			int deathPlasmaDust = Dust.NewDust(base.Projectile.Center, 6, 6, Main.rand.NextBool() ? 107 : 110, 0f, 0f, 100);
			float deathPlasmaDustX = Main.dust[deathPlasmaDust].velocity.X;
			float deathPlasmaDustY = Main.dust[deathPlasmaDust].velocity.Y;
			if (deathPlasmaDustX == 0f && deathPlasmaDustY == 0f)
			{
				deathPlasmaDustX = 1f;
			}
			float deathPlasmaDustVel = (float)Math.Sqrt(deathPlasmaDustX * deathPlasmaDustX + deathPlasmaDustY * deathPlasmaDustY);
			deathPlasmaDustVel = dustScale / deathPlasmaDustVel;
			deathPlasmaDustX *= deathPlasmaDustVel;
			deathPlasmaDustY *= deathPlasmaDustVel;
			float scale = 1f;
			switch ((int)dustScale)
			{
			case 4:
				scale = 1.2f;
				break;
			case 8:
				scale = 1.1f;
				break;
			case 12:
				scale = 1f;
				break;
			case 16:
				scale = 0.9f;
				break;
			}
			Dust dust = Main.dust[deathPlasmaDust];
			dust.velocity *= 0.5f;
			dust.velocity.X = dust.velocity.X + deathPlasmaDustX;
			dust.velocity.Y = dust.velocity.Y + deathPlasmaDustY;
			dust.scale = scale;
			dust.noGravity = true;
		}
	}
}
