using System;
using System.IO;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class HolyBlast : ModProjectile, ILocalizedModType, IModType
{
	public bool started;

	public int FireDamage;

	private const int TimeLeft = 120;

	private const int AccelerationTime = 60;

	private const float Acceleration = 1.05f;

	public static readonly SoundStyle ShootSound = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceHolyBlastShoot");

	public static readonly SoundStyle ImpactSound = new SoundStyle("CalamityMod/Sounds/Custom/Providence/ProvidenceHolyBlastImpact");

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 180;
		base.Projectile.height = 180;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 120;
		base.CooldownSlot = 1;
	}

	public override void OnSpawn(IEntitySource source)
	{
		FireDamage = Providence.FireDamage.CalculateProvidenceDamage();
		if (source is EntitySource_Parent { Entity: NPC parent } && parent.type == ModContent.NPCType<ProfanedGuardianCommander>())
		{
			FireDamage = ProfanedGuardianCommander.FireDamage;
		}
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
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0475: Unknown result type (might be due to invalid IL or missing references)
		//IL_047a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_048d: Unknown result type (might be due to invalid IL or missing references)
		//IL_048e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0493: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_036d: Unknown result type (might be due to invalid IL or missing references)
		ProvUtils.ApplyGFBDamage(base.Projectile, 180, 20);
		Lighting.AddLight(base.Projectile.Center, 0.9f, 0.7f, 0f);
		if (base.Projectile.timeLeft > 60 && base.Projectile.ai[2] == 0f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 1.05f;
		}
		Rectangle hitbox = base.Projectile.Hitbox;
		if (((Rectangle)(ref hitbox)).Intersects(new Rectangle((int)base.Projectile.ai[0], (int)base.Projectile.ai[1], 20, 42)))
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
		if (!started)
		{
			for (int i = 0; i < 13; i++)
			{
				GeneralParticleHandler.SpawnParticle(new PointParticle(base.Projectile.Center, base.Projectile.velocity.RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(-30f, 30f))) * Main.rand.NextFloat(2.4f, 4.2f), affectedByGravity: false, 15, Main.rand.NextFloat(3f, 6f), ProvUtils.GetProjectileColor(255)));
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, base.Projectile.velocity.RotatedBy(MathHelper.ToRadians(Main.rand.NextFloat(-50f, 50f))) * Main.rand.NextFloat(2.4f, 4.2f), affectedByGravity: false, 15, Main.rand.NextFloat(2f, 5f), ProvUtils.GetProjectileColor(255)));
			}
			started = true;
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			int dustType = ProvUtils.GetDustID();
			for (int j = 0; j < 10; j++)
			{
				int holyDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 0f, 0f, 100, default(Color), 2f);
				Dust obj = Main.dust[holyDust];
				obj.velocity *= 3f;
				Main.dust[holyDust].noGravity = true;
				if (Main.rand.NextBool())
				{
					Main.dust[holyDust].scale = 0.5f;
					Main.dust[holyDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			base.Projectile.localAI[0] = 1f;
			SoundEngine.PlaySound(in ShootSound, base.Projectile.Center);
		}
		base.Projectile.direction = (base.Projectile.spriteDirection = ((double)Math.Abs(base.Projectile.velocity.X) > 0.2).ToDirectionInt());
		GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center + Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(50f), 0f), 6.2831854820251465), base.Projectile.velocity.RotatedBy(Math.PI) * 0.6f, affectedByGravity: false, 20, Main.rand.NextFloat(1f, 2f), ProvUtils.GetProjectileColor(255)));
		GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center + Utils.RotatedBy(new Vector2(40f, 0f), (double)Vector2.Zero.AngleTo(-base.Projectile.velocity), default(Vector2)) + Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(20f), 0f), 6.2831854820251465), base.Projectile.velocity.RotatedBy(Math.PI) * 0.6f, Color.LightSlateGray, Color.DarkSlateGray, Main.rand.NextFloat(1f, 3f), 150f));
		if (base.Projectile.velocity.X < 0f)
		{
			base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X);
		}
		else
		{
			base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X);
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return ProvUtils.GetProjectileColor(lightColor);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = (ProvUtils.StandardAI() ? TextureAssets.Projectile[base.Type].Value : ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/HolyBlastNight", (AssetRequestMode)2).Value);
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		SpriteEffects sp = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Projectile projectile = base.Projectile;
		Color projectileColor = ProvUtils.GetProjectileColor(lightColor, Outline: true);
		SpriteEffects effects = sp;
		projectile.DrawBackglow(projectileColor, 4f, texture, null, effects);
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, frame.Size() / 2f, base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_037f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0459: Unknown result type (might be due to invalid IL or missing references)
		//IL_0463: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_030d: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		Color hiColor = ProvUtils.GetProjectileColor(255);
		Color loColor = ProvUtils.GetProjectileColor(0, Outline: true);
		for (int i = 0; i < 30; i++)
		{
			GeneralParticleHandler.SpawnParticle(new FlameParticle(base.Projectile.Center + Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(150f), 0f), 6.2831854820251465), 40, Main.rand.NextFloat(0.5f, 0.75f), Main.rand.NextFloat(1f, 2.5f), hiColor, loColor)
			{
				Velocity = Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(3f, 19f), 0f), 6.2831854820251465)
			});
			GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(12f, 40f), 0f), 6.2831854820251465), loColor, 60, Main.rand.NextFloat(0.75f, 1.75f), 1f, Main.rand.NextFloat(-0.05f, 0.05f), glowing: true));
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, hiColor, "CalamityMod/Particles/BloomCircle", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 1f, 0.1f, 15, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, hiColor, "CalamityMod/Particles/SoftRoundExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.05f, 0.25f, 35, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, hiColor, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.05f, 0.175f, 25, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		if (base.Projectile.owner == Main.myPlayer)
		{
			int totalProjectiles = ((!ProvUtils.StandardAI()) ? 8 : 6);
			if (Main.getGoodWorld)
			{
				totalProjectiles *= 2;
			}
			float radians = (float)Math.PI * 2f / (float)totalProjectiles;
			int type = ModContent.ProjectileType<HolyFire2>();
			float velocity = 5f;
			Vector2 spinningPoint = default(Vector2);
			((Vector2)(ref spinningPoint))._002Ector(0f, 0f - velocity);
			Vector2 additionalVelocity = base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 2.5f;
			for (int k = 0; k < totalProjectiles; k++)
			{
				Vector2 velocity2 = spinningPoint.RotatedBy(radians * (float)k);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity2 + additionalVelocity, type, FireDamage, 0f, base.Projectile.owner);
			}
		}
		SoundEngine.PlaySound(in ImpactSound, base.Projectile.Center);
		SoundEngine.PlaySound(in SoundID.DD2_BetsyFireballImpact, base.Projectile.Center);
		int dustType = ProvUtils.GetDustID();
		for (int j = 0; j < 4; j++)
		{
			int dust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 0f, 0f, 50, default(Color), 2f);
			Main.dust[dust].noGravity = true;
		}
		for (int l = 0; l < 40; l++)
		{
			int profaned = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 0f, 0f, 0, default(Color), 4f);
			Main.dust[profaned].noGravity = true;
			Dust obj = Main.dust[profaned];
			obj.velocity *= 3f;
			profaned = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 0f, 0f, 50, default(Color), 2f);
			Dust obj2 = Main.dust[profaned];
			obj2.velocity *= 2f;
			Main.dust[profaned].noGravity = true;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && !target.creativeGodMode)
		{
			ProvUtils.ApplyDebuffs(target, 180);
		}
	}
}
