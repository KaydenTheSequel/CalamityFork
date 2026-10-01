using System;
using CalamityMod.Events;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.Particles;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class SupremeCatastropheSlash : ModProjectile, ILocalizedModType, IModType
{
	public bool dashSlashExplode;

	public new string LocalizationCategory => "Projectiles.Boss";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 100;
		base.Projectile.height = 60;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.extraUpdates = 1;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 1500;
		base.Projectile.Opacity = 0f;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a34: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0379: Unknown result type (might be due to invalid IL or missing references)
		//IL_037e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0414: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_097e: Unknown result type (might be due to invalid IL or missing references)
		//IL_098c: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_09aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0726: Unknown result type (might be due to invalid IL or missing references)
		//IL_0730: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		//IL_0740: Unknown result type (might be due to invalid IL or missing references)
		//IL_074d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0757: Unknown result type (might be due to invalid IL or missing references)
		//IL_0778: Unknown result type (might be due to invalid IL or missing references)
		//IL_0783: Unknown result type (might be due to invalid IL or missing references)
		//IL_078d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0792: Unknown result type (might be due to invalid IL or missing references)
		//IL_079d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0801: Unknown result type (might be due to invalid IL or missing references)
		//IL_080c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0819: Unknown result type (might be due to invalid IL or missing references)
		//IL_0823: Unknown result type (might be due to invalid IL or missing references)
		//IL_0550: Unknown result type (might be due to invalid IL or missing references)
		//IL_055b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_0572: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_0881: Unknown result type (might be due to invalid IL or missing references)
		//IL_088c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0903: Unknown result type (might be due to invalid IL or missing references)
		//IL_092f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0934: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0601: Unknown result type (might be due to invalid IL or missing references)
		//IL_061a: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0627: Unknown result type (might be due to invalid IL or missing references)
		//IL_062c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0633: Unknown result type (might be due to invalid IL or missing references)
		//IL_0638: Unknown result type (might be due to invalid IL or missing references)
		//IL_0642: Unknown result type (might be due to invalid IL or missing references)
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_067e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0683: Unknown result type (might be due to invalid IL or missing references)
		Time++;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		if (CalamityWorld.revenge)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
		if (Main.expertMode)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 7 % Main.projFrames[base.Type];
		if (base.Projectile.ai[2] < 4f && base.Projectile.ai[2] < 50f)
		{
			base.Projectile.Opacity = Utils.GetLerpValue(0f, 8f, base.Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(1500f, 1492f, base.Projectile.timeLeft, clamped: true);
		}
		if (base.Projectile.velocity.X < 0f)
		{
			base.Projectile.spriteDirection = -1;
			base.Projectile.rotation = (float)Math.Atan2(0f - base.Projectile.velocity.Y, 0f - base.Projectile.velocity.X);
		}
		else
		{
			base.Projectile.spriteDirection = 1;
			base.Projectile.rotation = (float)Math.Atan2(base.Projectile.velocity.Y, base.Projectile.velocity.X);
		}
		if (base.Projectile.ai[2] == 50f)
		{
			base.Projectile.extraUpdates = 0;
			if (base.Projectile.timeLeft > 30)
			{
				base.Projectile.timeLeft = 30;
			}
			base.Projectile.Opacity = 0f;
			if (Main.rand.NextBool())
			{
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 66, -base.Projectile.velocity * Main.rand.NextFloat(0.1f, 1.5f));
				dust.noGravity = true;
				dust.scale = Main.rand.NextFloat(0.5f, 0.7f);
				dust.color = Color.DeepSkyBlue;
				dust.alpha = 100;
			}
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 0.1f, ModContent.ProjectileType<SupremeCatastropheSlash>(), base.Projectile.damage, 0f, Main.myPlayer, 0f, 5f, 3f + Time);
			return;
		}
		if (base.Projectile.ai[2] == 1f || base.Projectile.ai[2] == 2f)
		{
			if (base.Projectile.ai[2] == 1f)
			{
				base.Projectile.extraUpdates = 2;
			}
			else
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 1.0045f;
			}
		}
		else if (base.Projectile.ai[2] == 3f)
		{
			base.Projectile.extraUpdates = 5;
			if (Time > 30f)
			{
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 1.015f;
			}
			if (Main.rand.NextBool(3))
			{
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(50f, 50f) - base.Projectile.velocity.SafeNormalize(Vector2.UnitY) * 8.5f, 66, -base.Projectile.velocity * Main.rand.NextFloat(0.2f, 1.2f));
				dust2.noGravity = true;
				dust2.scale = Main.rand.NextFloat(0.5f, 0.7f);
				dust2.color = Color.DeepSkyBlue;
			}
		}
		else if (base.Projectile.ai[2] >= 4f && base.Projectile.ai[2] < 50f)
		{
			base.Projectile.extraUpdates = 0;
			if (base.Projectile.ai[1] == 5f)
			{
				if (base.Projectile.timeLeft == 1500)
				{
					base.Projectile.timeLeft = (death ? 35 : 45) - (int)(base.Projectile.ai[2] - 4f);
					GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, base.Projectile.velocity, affectedByGravity: false, 25, 5f, Color.DeepSkyBlue * 0.35f));
				}
				else if (base.Projectile.timeLeft == 1 && NPC.AnyNPCs(ModContent.NPCType<SupremeCatastrophe>()))
				{
					dashSlashExplode = true;
					GeneralParticleHandler.SpawnParticle(new VoidSparkParticle(base.Projectile.Center, base.Projectile.velocity, affectedByGravity: false, 9, 0.7f, Color.Cyan * 0.7f));
					if (base.Projectile.ai[2] >= 4f)
					{
						SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ExobladeBeamSlash");
						style.Volume = 0.65f;
						style.Pitch = 0.8f;
						SoundEngine.PlaySound(in style, base.Projectile.Center);
					}
					for (int i = 0; i < 3; i++)
					{
						Vector2 vel = Utils.RotatedByRandom(new Vector2(14f, 14f), 100.0) * Main.rand.NextFloat(0.1f, 2.5f);
						Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center + vel * 2f, 279, vel);
						dust3.noGravity = true;
						dust3.scale = Main.rand.NextFloat(1.2f, 1.8f);
						dust3.color = Color.DeepSkyBlue;
					}
				}
			}
			else if (base.Projectile.timeLeft == 1500)
			{
				base.Projectile.timeLeft = 40 - (int)(base.Projectile.ai[2] - 4f);
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center, base.Projectile.velocity, affectedByGravity: false, 25, 5f, Color.DeepSkyBlue * 0.35f));
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center + base.Projectile.velocity * 50f, base.Projectile.velocity, affectedByGravity: false, 25, 5f, Color.DeepSkyBlue * 0.35f));
				GeneralParticleHandler.SpawnParticle(new SparkParticle(base.Projectile.Center - base.Projectile.velocity * 50f, base.Projectile.velocity, affectedByGravity: false, 25, 5f, Color.DeepSkyBlue * 0.35f));
			}
			else if (base.Projectile.timeLeft == 1 && NPC.AnyNPCs(ModContent.NPCType<SupremeCatastrophe>()))
			{
				dashSlashExplode = true;
				GeneralParticleHandler.SpawnParticle(new VoidSparkParticle(base.Projectile.Center, base.Projectile.velocity, affectedByGravity: false, 9, 1.3f, Color.Cyan * 0.7f));
				if (base.Projectile.ai[2] >= 4f)
				{
					SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/ExobladeBeamSlash");
					style.Volume = 0.65f;
					style.Pitch = 0.8f;
					SoundEngine.PlaySound(in style, base.Projectile.Center);
				}
				for (int j = 0; j < 3; j++)
				{
					Vector2 vel2 = Utils.RotatedByRandom(new Vector2(14f, 14f), 100.0) * Main.rand.NextFloat(0.1f, 2.5f);
					Dust dust4 = Dust.NewDustPerfect(base.Projectile.Center + vel2 * 2f, 279, vel2);
					dust4.noGravity = true;
					dust4.scale = Main.rand.NextFloat(1.2f, 1.8f);
					dust4.color = Color.DeepSkyBlue;
				}
			}
		}
		if (!NPC.AnyNPCs(ModContent.NPCType<SupremeCatastrophe>()) && !Main.zenithWorld)
		{
			base.Projectile.timeLeft = 1;
			for (int k = 0; k < 10; k++)
			{
				Vector2 velocity = Utils.RotatedByRandom(new Vector2(7f, 7f), 100.0) * Main.rand.NextFloat(0.8f, 1.2f);
				Dust dust5 = Dust.NewDustPerfect(base.Projectile.Center + velocity, 66, velocity * Main.rand.NextFloat(0.2f, 1f));
				dust5.noGravity = true;
				dust5.scale = Main.rand.NextFloat(1.3f, 1.9f);
				dust5.color = Color.Cyan;
			}
		}
		Lighting.AddLight(base.Projectile.Center, 0.5f * base.Projectile.Opacity, 0f, 0f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		if (base.Projectile.ai[1] == 0f)
		{
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/SupremeCatastropheSlashAlt", (AssetRequestMode)2).Value;
		}
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		drawPosition -= base.Projectile.velocity.SafeNormalize(Vector2.UnitX) * 38f;
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		for (int i = 0; i < 3; i++)
		{
			Color afterimageColor = base.Projectile.GetAlpha(lightColor) * (1f - (float)i / 3f) * 0.5f;
			Vector2 afterimageOffset = base.Projectile.velocity * (float)(-i) * 4f;
			Main.EntitySpriteDraw(texture, drawPosition + afterimageOffset, frame, afterimageColor, base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, direction);
		}
		Main.EntitySpriteDraw(texture, drawPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, direction);
		return false;
	}

	public override bool CanHitPlayer(Player target)
	{
		if (!(base.Projectile.Opacity >= 1f) || !(base.Projectile.ai[2] < 4f))
		{
			if (dashSlashExplode)
			{
				return base.Projectile.ai[2] >= 4f;
			}
			return false;
		}
		return true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && (base.Projectile.Opacity == 1f || !(base.Projectile.ai[2] < 4f)) && !dashSlashExplode)
		{
			_ = base.Projectile.ai[2];
			_ = 4f;
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (base.Projectile.ai[1] == 5f) ? 70 : ((base.Projectile.ai[2] >= 4f) ? 100 : 43), targetHitbox);
	}
}
