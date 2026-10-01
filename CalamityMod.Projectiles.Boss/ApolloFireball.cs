using System;
using System.IO;
using CalamityMod.NPCs;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.Sounds;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class ApolloFireball : ModProjectile, ILocalizedModType, IModType
{
	private const int timeLeft = 60;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 4;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 48;
		base.Projectile.height = 48;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.Opacity = 0f;
		base.CooldownSlot = 1;
		base.Projectile.timeLeft = 60;
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
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0216: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0366: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0400: Unknown result type (might be due to invalid IL or missing references)
		//IL_040d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0441: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_048b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_0505: Unknown result type (might be due to invalid IL or missing references)
		//IL_0521: Unknown result type (might be due to invalid IL or missing references)
		//IL_052b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		if (Vector2.Distance(new Vector2(base.Projectile.ai[0], base.Projectile.ai[1]), base.Projectile.Center) < 80f)
		{
			base.Projectile.tileCollide = true;
		}
		int fadeInTime = 3;
		base.Projectile.Opacity = MathHelper.Clamp(1f - (float)(base.Projectile.timeLeft - (60 - fadeInTime)) / (float)fadeInTime, 0f, 1f);
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
			float speed1 = 1.8f;
			float speed2 = 2.8f;
			float angleRandom = 0.35f;
			for (int i = 0; i < 40; i++)
			{
				Vector2 dustVel = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(speed1, speed2), 0f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
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
			for (int j = 0; j < 20; j++)
			{
				Vector2 dustVel2 = Utils.RotatedBy(new Vector2(Main.rand.NextFloat(speed1, speed2), 0f), (double)base.Projectile.velocity.ToRotation(), default(Vector2));
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
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		int height = 90;
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = height);
		base.Projectile.Center = base.Projectile.position;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in CommonCalamitySounds.ExoPlasmaExplosionSound, base.Projectile.Center);
		if (Main.myPlayer == base.Projectile.owner && base.Projectile.ai[2] == 0f)
		{
			bool splitNormal = true;
			if (CalamityGlobalNPC.draedonExoMechTwinGreen != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].active)
			{
				splitNormal = Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].ai[2] % 2f == 0f;
			}
			int totalProjectiles = 4;
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
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity2, type, Apollo.BoltDamage, 0f, Main.myPlayer);
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
			int plasmaDeathDust = Dust.NewDust(base.Projectile.Center, 6, 6, Main.rand.NextBool() ? 107 : 110, 0f, 0f, 100);
			float plasmaDeathDustX = Main.dust[plasmaDeathDust].velocity.X;
			float plasmaDeathDustY = Main.dust[plasmaDeathDust].velocity.Y;
			if (plasmaDeathDustX == 0f && plasmaDeathDustY == 0f)
			{
				plasmaDeathDustX = 1f;
			}
			float plasmaDeathDustVel = (float)Math.Sqrt(plasmaDeathDustX * plasmaDeathDustX + plasmaDeathDustY * plasmaDeathDustY);
			plasmaDeathDustVel = dustScale / plasmaDeathDustVel;
			plasmaDeathDustX *= plasmaDeathDustVel;
			plasmaDeathDustY *= plasmaDeathDustVel;
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
			Dust dust = Main.dust[plasmaDeathDust];
			dust.velocity *= 0.5f;
			dust.velocity.X = dust.velocity.X + plasmaDeathDustX;
			dust.velocity.Y = dust.velocity.Y + plasmaDeathDustY;
			dust.scale = scale;
			dust.noGravity = true;
		}
	}
}
