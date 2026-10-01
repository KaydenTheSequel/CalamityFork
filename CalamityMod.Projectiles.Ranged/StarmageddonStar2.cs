using System;
using System.IO;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class StarmageddonStar2 : ModProjectile, ILocalizedModType, IModType
{
	private bool start = true;

	public new string LocalizationCategory => "Projectiles.Ranged";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 38);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 3600;
		base.Projectile.DamageType = DamageClass.Ranged;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(start);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		start = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0243: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0411: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0419: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0450: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_0499: Unknown result type (might be due to invalid IL or missing references)
		//IL_049b: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c1: Unknown result type (might be due to invalid IL or missing references)
		Projectile hostProjectile = Main.projectile[(int)base.Projectile.ai[0]];
		if (base.Projectile.type != ModContent.ProjectileType<StarmageddonStar2>() || !hostProjectile.active || hostProjectile.type != ModContent.ProjectileType<StarmageddonBinaryStarCenter>())
		{
			base.Projectile.Kill();
			return;
		}
		Lighting.AddLight(base.Projectile.Center, 0.05f, 0.4f, 0.5f);
		bool num = hostProjectile.ai[1] == 1f;
		if (start)
		{
			base.Projectile.ai[2] = base.Projectile.ai[1];
			start = false;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		double rad = (double)base.Projectile.ai[2] * (Math.PI / 180.0);
		double dist = 32.0;
		base.Projectile.position.X = hostProjectile.Center.X - (float)(int)(Math.Cos(rad) * dist) - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = hostProjectile.Center.Y - (float)(int)(Math.Sin(rad) * dist) - (float)(base.Projectile.height / 2);
		base.Projectile.ai[2] += 2f;
		if (!num)
		{
			return;
		}
		base.Projectile.localAI[0]++;
		Vector2 dustVelocity = default(Vector2);
		Vector2 cloudVelocity = default(Vector2);
		for (int i = 0; i < 2; i++)
		{
			bool top = i == 0;
			if (base.Projectile.localAI[0] % 4f == 0f)
			{
				int numParticles = Main.rand.Next(2, 4);
				for (int j = 0; j < numParticles; j++)
				{
					float dustVelocityX = Main.rand.NextFloat(-8f, 8f);
					float dustVelocityY = (top ? (-32f) : 32f);
					((Vector2)(ref dustVelocity))._002Ector(dustVelocityX, dustVelocityY);
					GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + Vector2.Normalize(dustVelocity) * 19f, dustVelocity, affectedByGravity: false, 60, 1.2f, Color.Cyan));
				}
			}
			if (base.Projectile.localAI[0] % 16f == 0f)
			{
				float cloudVelocityX = Main.rand.NextFloat(-2f, 2f);
				float cloudVelocityY = (top ? (-8f) : 8f);
				((Vector2)(ref cloudVelocity))._002Ector(cloudVelocityX, cloudVelocityY);
				GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center + Vector2.Normalize(cloudVelocity) * 19f, cloudVelocity, Color.Cyan, Color.Blue, 1f, 255f));
			}
			if (Main.myPlayer != base.Projectile.owner || base.Projectile.localAI[0] % 15f != 0f)
			{
				continue;
			}
			float suckYDistanceMax = 300f;
			Vector2 position = base.Projectile.Center + new Vector2(600f * (top ? (-1f) : 1f), Main.rand.NextFloat(0f - suckYDistanceMax, suckYDistanceMax));
			Vector2 speed = Vector2.Normalize(base.Projectile.Center - position) * 12f;
			for (int l = 0; l < 12; l++)
			{
				Vector2 dustVel = Vector2.UnitX * (0f - (float)base.Projectile.width) / 2f;
				dustVel += -Vector2.UnitY.RotatedBy((float)l * (float)Math.PI / 6f) * new Vector2(8f, 16f);
				dustVel = dustVel.RotatedBy(speed.ToRotation());
				int starDust = Dust.NewDust(position, 0, 0, 221);
				Main.dust[starDust].noGravity = true;
				Main.dust[starDust].position = position + dustVel;
				Main.dust[starDust].velocity = speed * 0.1f;
				Main.dust[starDust].velocity = Vector2.Normalize(position - speed * 3f - Main.dust[starDust].position) * 1.25f;
			}
			int type = Utils.SelectRandom<int>(Main.rand, ModContent.ProjectileType<StarfleetStar>(), ModContent.ProjectileType<AstralStar>(), 955, 9);
			Projectile starCenter = base.Projectile;
			ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Projectile p = enumerator.Current;
				if (p.type == ModContent.ProjectileType<StarmageddonBinaryStarCenter>() && p.owner == base.Projectile.owner)
				{
					starCenter = p;
					break;
				}
			}
			Vector2 predictSpeed = CalamityUtils.CalculatePredictiveAimToTargetMaxUpdates(position, Main.npc[(int)starCenter.ai[2]], 12f, 5);
			int star = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), position, predictSpeed, type, base.Projectile.damage / 2, base.Projectile.knockBack * 0.5f, base.Projectile.owner);
			if (star.WithinBounds(Main.maxProjectiles))
			{
				if (type == ModContent.ProjectileType<StarfleetStar>() || type == ModContent.ProjectileType<AstralStar>())
				{
					Main.projectile[star].ai[0] = 1f;
				}
				Main.projectile[star].extraUpdates = 4;
				Main.projectile[star].penetrate = 1;
				Main.projectile[star].timeLeft = 300;
				Main.projectile[star].DamageType = DamageClass.Ranged;
				Main.projectile[star].tileCollide = false;
				Main.projectile[star].netUpdate = true;
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int height = texture.Height / Main.projFrames[base.Type];
		int drawStart = height * base.Projectile.frame;
		Vector2 origin = base.Projectile.Size / 2f;
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, drawStart, texture.Width, height), Color.White, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0284: Unknown result type (might be due to invalid IL or missing references)
		//IL_0289: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_034b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0356: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0386: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_045e: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_049f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0524: Unknown result type (might be due to invalid IL or missing references)
		//IL_0529: Unknown result type (might be due to invalid IL or missing references)
		//IL_0533: Unknown result type (might be due to invalid IL or missing references)
		//IL_0538: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0545: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0574: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(176);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		for (int i = 0; i < 4; i++)
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 135, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[dust].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
		}
		for (int j = 0; j < 30; j++)
		{
			int dust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 135, 0f, 0f, 200, default(Color), 3.7f);
			Main.dust[dust2].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
			Main.dust[dust2].noGravity = true;
			Dust obj = Main.dust[dust2];
			obj.velocity *= 3f;
			dust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 135, 0f, 0f, 100, default(Color), 1.5f);
			Main.dust[dust2].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
			Dust obj2 = Main.dust[dust2];
			obj2.velocity *= 2f;
			Main.dust[dust2].noGravity = true;
			Main.dust[dust2].fadeIn = 2.5f;
		}
		for (int k = 0; k < 10; k++)
		{
			int dust3 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 135, 0f, 0f, 0, default(Color), 2.7f);
			Main.dust[dust3].position = base.Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(base.Projectile.velocity.ToRotation()) * (float)base.Projectile.width / 2f;
			Main.dust[dust3].noGravity = true;
			Dust obj3 = Main.dust[dust3];
			obj3.velocity *= 3f;
		}
		for (int l = 0; l < 10; l++)
		{
			int dust4 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 303, 0f, 0f, 0, default(Color), 1.5f);
			Main.dust[dust4].position = base.Projectile.Center + Vector2.UnitX.RotatedByRandom(3.1415927410125732).RotatedBy(base.Projectile.velocity.ToRotation()) * (float)base.Projectile.width / 2f;
			Main.dust[dust4].noGravity = true;
			Dust obj4 = Main.dust[dust4];
			obj4.velocity *= 3f;
		}
		for (int m = 0; m < 2; m++)
		{
			int gore = Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.position + new Vector2((float)(base.Projectile.width * Main.rand.Next(100)) / 100f, (float)(base.Projectile.height * Main.rand.Next(100)) / 100f) - Vector2.One * 10f, default(Vector2), Main.rand.Next(61, 64));
			Main.gore[gore].position = base.Projectile.Center + Vector2.UnitY.RotatedByRandom(3.1415927410125732) * (float)Main.rand.NextDouble() * (float)base.Projectile.width / 2f;
			Gore obj5 = Main.gore[gore];
			obj5.velocity *= 0.3f;
			Main.gore[gore].velocity.X += (float)Main.rand.Next(-10, 11) * 0.05f;
			Main.gore[gore].velocity.Y += (float)Main.rand.Next(-10, 11) * 0.05f;
		}
	}
}
