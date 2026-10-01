using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.NPCs;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class DoGBeamPortal : ModProjectile, ILocalizedModType, IModType
{
	public bool start = true;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 80;
		base.Projectile.height = 80;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 600;
		base.Projectile.penetrate = -1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(start);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		start = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d7: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityGlobalNPC.voidBoss < 0 || !Main.npc[CalamityGlobalNPC.voidBoss].active)
		{
			base.Projectile.active = false;
			base.Projectile.netUpdate = true;
			return;
		}
		Player player = Main.player[Main.npc[CalamityGlobalNPC.voidBoss].target];
		if (start)
		{
			SoundEngine.PlaySound(in SoundID.Item92, base.Projectile.Center);
			for (int i = 0; i < 15; i++)
			{
				int ectoDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 180, 0f, 0f, 100, default(Color), 1.2f);
				Dust obj = Main.dust[ectoDust];
				obj.velocity *= 3f;
				Main.dust[ectoDust].noGravity = true;
				if (Main.rand.NextBool())
				{
					Main.dust[ectoDust].scale = 0.5f;
					Main.dust[ectoDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
				}
			}
			for (int j = 0; j < 30; j++)
			{
				int ectoDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 180, 0f, 0f, 100, default(Color), 1.7f);
				Main.dust[ectoDust2].noGravity = true;
				Dust obj2 = Main.dust[ectoDust2];
				obj2.velocity *= 5f;
				ectoDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 180, 0f, 0f, 100);
				Main.dust[ectoDust2].noGravity = true;
				Dust obj3 = Main.dust[ectoDust2];
				obj3.velocity *= 2f;
			}
			base.Projectile.ai[1] = base.Projectile.ai[0];
			start = false;
		}
		bool expertMode = Main.expertMode || BossRushEvent.BossRushActive;
		bool revenge = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		Lighting.AddLight(base.Projectile.Center, 0f, 0.95f, 1.15f);
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
		double rad = (double)base.Projectile.ai[1] * (Math.PI / 180.0);
		double dist = (death ? 400.0 : (revenge ? 420.0 : (expertMode ? 440.0 : 480.0)));
		base.Projectile.position.X = player.Center.X - (float)(int)(Math.Cos(rad) * dist) - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = player.Center.Y - (float)(int)(Math.Sin(rad) * dist) - (float)(base.Projectile.height / 2);
		base.Projectile.ai[1]++;
		if (base.Projectile.timeLeft <= 30)
		{
			return;
		}
		base.Projectile.localAI[0]++;
		if (!(base.Projectile.localAI[0] >= 240f))
		{
			return;
		}
		base.Projectile.localAI[0] = 0f;
		if (base.Projectile.owner == Main.myPlayer)
		{
			SoundEngine.PlaySound(in SoundID.Item33, base.Projectile.Center);
			float speed = (death ? 5f : (revenge ? 4.5f : (expertMode ? 4f : 3f)));
			int totalProjectiles = 3;
			float radians = (float)Math.PI * 2f / (float)totalProjectiles;
			for (int k = 0; k < totalProjectiles; k++)
			{
				Vector2 velocity = Utils.RotatedBy(new Vector2(0f, 0f - speed), (double)(radians * (float)k), default(Vector2));
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<DoGBeam>(), 0, 0f, Main.myPlayer, base.Projectile.damage);
			}
		}
	}

	public override bool CanHitPlayer(Player target)
	{
		return false;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft < 30)
		{
			byte b2 = (byte)((double)base.Projectile.timeLeft * 8.5);
			byte a2 = (byte)(100f * ((float)(int)b2 / 255f));
			return new Color((int)b2, (int)b2, (int)b2, (int)a2);
		}
		return new Color(255, 255, 255, 100);
	}
}
