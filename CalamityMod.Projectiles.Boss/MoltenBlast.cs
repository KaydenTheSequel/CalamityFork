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

public class MoltenBlast : ModProjectile, ILocalizedModType, IModType
{
	public int BlobDamage;

	private const int TimeLeft = 90;

	private const int AccelerationTime = 60;

	private const float Acceleration = 1.05f;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 42;
		base.Projectile.height = 42;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 90;
		base.CooldownSlot = 1;
	}

	public override void OnSpawn(IEntitySource source)
	{
		BlobDamage = Providence.BlobDamage.CalculateProvidenceDamage();
		if (source is EntitySource_Parent { Entity: NPC parent } && parent.type == ModContent.NPCType<ProfanedGuardianDefender>())
		{
			BlobDamage = ProfanedGuardianDefender.BlobDamage;
		}
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_0528: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0532: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0309: Unknown result type (might be due to invalid IL or missing references)
		//IL_0322: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e5: Unknown result type (might be due to invalid IL or missing references)
		ProvUtils.ApplyGFBDamage(base.Projectile, 120, 20);
		Lighting.AddLight(base.Projectile.Center, 0.45f, 0.35f, 0f);
		if (base.Projectile.timeLeft > 30 && base.Projectile.ai[2] == 0f)
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
		int dustType = ProvUtils.GetDustID();
		if (base.Projectile.localAI[1] == 0f)
		{
			for (int d = 0; d < 10; d++)
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
			base.Projectile.localAI[1] = 1f;
			SoundEngine.PlaySound(in SoundID.Item73, base.Projectile.Center);
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] == 30f)
		{
			base.Projectile.localAI[0] = 0f;
			for (int l = 0; l < 12; l++)
			{
				Vector2 dustRotate = Vector2.UnitX * (float)(-base.Projectile.width) / 2f;
				dustRotate += -Vector2.UnitY.RotatedBy((float)l * (float)Math.PI / 6f) * new Vector2(8f, 16f);
				dustRotate = dustRotate.RotatedBy(base.Projectile.rotation - (float)Math.PI / 2f);
				int profaned = Dust.NewDust(base.Projectile.Center, 0, 0, dustType, 0f, 0f, 160);
				Main.dust[profaned].scale = 1.1f;
				Main.dust[profaned].noGravity = true;
				Main.dust[profaned].position = base.Projectile.Center + dustRotate;
				Main.dust[profaned].velocity = base.Projectile.velocity * 0.1f;
				Main.dust[profaned].velocity = Vector2.Normalize(base.Projectile.Center - base.Projectile.velocity * 3f - Main.dust[profaned].position) * 1.25f;
			}
		}
		base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X) + (float)Math.PI / 2f;
		float vel = Math.Clamp((Math.Abs(base.Projectile.velocity.X) + Math.Abs(base.Projectile.velocity.Y)) / 2f, 0f, 1f);
		GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center + Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(15f), 0f), 6.2831854820251465), base.Projectile.velocity.RotatedBy(Math.PI) * 0.5f, affectedByGravity: false, 10, Main.rand.NextFloat(0.8f, 1.2f), ProvUtils.GetProjectileColor(255)));
		if (Main.rand.NextBool())
		{
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, Vector2.Zero, Color.LightSlateGray, Color.DarkSlateGray, Main.rand.NextFloat(vel), 150f, MathHelper.ToRadians(Main.rand.NextFloat(-1f, 1f))));
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return ProvUtils.GetProjectileColor(0);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = (ProvUtils.StandardAI() ? TextureAssets.Projectile[base.Type].Value : ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/MoltenBlastNight", (AssetRequestMode)2).Value);
		int framing = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		base.Projectile.DrawBackglow(ProvUtils.GetProjectileColor(base.Projectile.alpha, Outline: true), 4f, texture, null, (SpriteEffects)0);
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)framing / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		Color hiColor = ProvUtils.GetProjectileColor(255);
		ProvUtils.GetProjectileColor(0, Outline: true);
		for (int i = 0; i < 25; i++)
		{
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center, Utils.RotatedByRandom(new Vector2(Main.rand.NextFloat(10f), 0f), 6.2831854820251465), affectedByGravity: false, 10, Main.rand.NextFloat(0.8f, 1.2f), hiColor));
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White, "CalamityMod/Particles/BloomCircle", Vector2.One, 0f, 0.5f, 0.1f, 4, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		for (float i2 = 0f; i2 < 1f; i2 += 0.25f)
		{
			GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, hiColor, "CalamityMod/Particles/SoftRoundExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.02f * i2, 0.125f * i2, 8, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		}
		GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, hiColor, "CalamityMod/Particles/ShatteredExplosion", Vector2.One, Main.rand.NextFloat((float)Math.PI * 2f), 0.02f, 0.045f, 5, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
		int blobAmt = ((!ProvUtils.StandardAI()) ? 9 : 6);
		if (base.Projectile.owner == Main.myPlayer)
		{
			Vector2 additionalBlobVelocity = default(Vector2);
			((Vector2)(ref additionalBlobVelocity))._002Ector(base.Projectile.velocity.X * 0.1f, base.Projectile.velocity.Y);
			for (int b = 0; b < blobAmt; b++)
			{
				Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f) + additionalBlobVelocity;
				if (Main.getGoodWorld)
				{
					velocity *= 2f;
				}
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<MoltenBlob>(), BlobDamage, 0f, base.Projectile.owner);
			}
		}
		SoundEngine.PlaySound(in SoundID.Item74, base.Projectile.Center);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 18f, targetHitbox);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && !target.creativeGodMode)
		{
			ProvUtils.ApplyDebuffs(target, 120);
		}
	}
}
