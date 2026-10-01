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

public class SupremeCataclysmFist : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 shootVel;

	public int rotDirection = 1;

	public bool broIsAlive = true;

	public new string LocalizationCategory => "Projectiles.Boss";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 126;
		base.Projectile.height = 54;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 1200;
		base.Projectile.Opacity = 0f;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_0cc9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0cd8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0304: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0fab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0431: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_0496: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d46: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0d55: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ee5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ef3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f11: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f1e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f4a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f50: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f7d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f82: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f8c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0f91: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0750: Unknown result type (might be due to invalid IL or missing references)
		//IL_0755: Unknown result type (might be due to invalid IL or missing references)
		//IL_075f: Unknown result type (might be due to invalid IL or missing references)
		//IL_076d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_078b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07de: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0811: Unknown result type (might be due to invalid IL or missing references)
		//IL_082a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0500: Unknown result type (might be due to invalid IL or missing references)
		//IL_050b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0510: Unknown result type (might be due to invalid IL or missing references)
		//IL_0515: Unknown result type (might be due to invalid IL or missing references)
		//IL_051e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0523: Unknown result type (might be due to invalid IL or missing references)
		//IL_052d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0537: Unknown result type (might be due to invalid IL or missing references)
		//IL_0546: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bfc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c15: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c27: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0c7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0add: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0ae7: Unknown result type (might be due to invalid IL or missing references)
		//IL_089b: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_08bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_08eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0902: Unknown result type (might be due to invalid IL or missing references)
		//IL_0914: Unknown result type (might be due to invalid IL or missing references)
		//IL_091a: Unknown result type (might be due to invalid IL or missing references)
		//IL_091c: Unknown result type (might be due to invalid IL or missing references)
		//IL_092a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0943: Unknown result type (might be due to invalid IL or missing references)
		//IL_0948: Unknown result type (might be due to invalid IL or missing references)
		//IL_0963: Unknown result type (might be due to invalid IL or missing references)
		//IL_0968: Unknown result type (might be due to invalid IL or missing references)
		//IL_096a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0970: Unknown result type (might be due to invalid IL or missing references)
		//IL_0975: Unknown result type (might be due to invalid IL or missing references)
		//IL_0996: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_09ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_09b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_09c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a14: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a19: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a1b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a21: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a26: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a59: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_058a: Unknown result type (might be due to invalid IL or missing references)
		//IL_058f: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_0613: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b1a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b20: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b22: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b32: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b3f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4c: Unknown result type (might be due to invalid IL or missing references)
		//IL_065b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0660: Unknown result type (might be due to invalid IL or missing references)
		//IL_0681: Unknown result type (might be due to invalid IL or missing references)
		//IL_0693: Unknown result type (might be due to invalid IL or missing references)
		//IL_0699: Unknown result type (might be due to invalid IL or missing references)
		//IL_069b: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06da: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b57: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b5c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b72: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b85: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0bb7: Unknown result type (might be due to invalid IL or missing references)
		if (CalamityWorld.death)
		{
			_ = 1;
		}
		else
			_ = BossRushEvent.BossRushActive;
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
		if (base.Projectile.ai[2] >= 3f)
		{
			if (Main.zenithWorld)
			{
				if (base.Projectile.timeLeft > 400)
				{
					shootVel = base.Projectile.velocity * 0.4f;
					base.Projectile.timeLeft = 400;
					rotDirection = ((!Main.rand.NextBool()) ? 1 : (-1));
					Projectile projectile = base.Projectile;
					projectile.velocity *= 6f;
					broIsAlive = NPC.AnyNPCs(ModContent.NPCType<SupremeCatastrophe>());
				}
				else
				{
					if (Time > 10f)
					{
						base.Projectile.tileCollide = true;
					}
					else
					{
						Projectile projectile2 = base.Projectile;
						projectile2.velocity *= 0.99f;
					}
					base.Projectile.rotation += 0.02f;
				}
				if (base.Projectile.timeLeft == 1)
				{
					int points = 25;
					float radians = (float)Math.PI * 2f / (float)points;
					Vector2 spinningPoint = Vector2.Normalize(new Vector2(-4f, -4f));
					for (int b = 0; b < 2; b++)
					{
						float rotRando = Main.rand.NextFloat(0.1f, 2.5f);
						for (int k = 0; k < points; k++)
						{
							Vector2 velocity = spinningPoint.RotatedBy(radians * (float)k).RotatedBy(-0.45f * rotRando);
							Dust dust = Dust.NewDustPerfect(base.Projectile.Center + velocity * (float)((b == 0) ? 7 : 5), 279, velocity * (float)((b == 0) ? 9 : 7));
							dust.noGravity = true;
							dust.scale = Main.rand.NextFloat(1.3f, 1.9f);
							dust.color = Color.Red;
						}
					}
					base.Projectile.Kill();
				}
				Vector2 vel = Utils.RotatedByRandom(new Vector2(14f, 14f), 100.0) * Main.rand.NextFloat(0.1f, 2.5f);
				Dust dust2 = Dust.NewDustPerfect(base.Projectile.Center + vel * 2f, 279, vel);
				dust2.noGravity = true;
				dust2.scale = Main.rand.NextFloat(0.9f, 1.2f);
				dust2.color = Color.Red;
			}
			else
			{
				if (base.Projectile.timeLeft > 240)
				{
					shootVel = base.Projectile.velocity * 0.4f;
					base.Projectile.timeLeft = 240;
					rotDirection = ((!Main.rand.NextBool()) ? 1 : (-1));
					Projectile projectile3 = base.Projectile;
					projectile3.velocity *= 5.5f;
					broIsAlive = NPC.AnyNPCs(ModContent.NPCType<SupremeCatastrophe>());
				}
				else
				{
					float randSize = Main.rand.NextFloat(0.8f, 1.2f);
					for (int i = 0; i < 2; i++)
					{
						GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.Lerp(Color.Red, Color.Magenta, 0.5f), "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 1.7f * randSize, 0f, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					}
					GeneralParticleHandler.SpawnParticle(new CustomPulse(base.Projectile.Center, Vector2.Zero, Color.White * 0.9f, "CalamityMod/Particles/LargeBloom", new Vector2(1f, 1f), Main.rand.NextFloat(-10f, 10f), 1.4f * randSize, 0f, 10, UseAdditiveBlend: true, 1f, fade: true, 1f, (SpriteEffects)0));
					Projectile projectile4 = base.Projectile;
					projectile4.velocity *= 0.945f;
				}
				if (base.Projectile.timeLeft == 205)
				{
					for (int j = 0; j < 28; j++)
					{
						Vector2 vel2 = -shootVel * MathHelper.Clamp(Time * 0.1f, 1f, 1.8f);
						GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + vel2 * (float)(9 + j * 11), vel2, affectedByGravity: false, 7, 0.17f, Color.Lerp(Color.Red, Color.Magenta, 0.5f) * 0.5f, new Vector2(0.9f, 0.4f), quickShrink: true, glow: false));
					}
					if (!broIsAlive)
					{
						for (int l = 0; l < 28; l++)
						{
							Vector2 vel3 = (-shootVel * MathHelper.Clamp(Time * 0.1f, 1f, 1.8f)).RotatedBy(MathHelper.ToRadians(120f));
							GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + vel3 * (float)(9 + l * 11), vel3, affectedByGravity: false, 7, 0.17f, Color.Lerp(Color.Red, Color.Magenta, 0.5f) * 0.5f, new Vector2(0.9f, 0.4f), quickShrink: true, glow: false));
						}
						for (int m = 0; m < 28; m++)
						{
							Vector2 vel4 = (-shootVel * MathHelper.Clamp(Time * 0.1f, 1f, 1.8f)).RotatedBy(MathHelper.ToRadians(-120f));
							GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center + vel4 * (float)(9 + m * 11), vel4, affectedByGravity: false, 7, 0.17f, Color.Lerp(Color.Red, Color.Magenta, 0.5f) * 0.5f, new Vector2(0.9f, 0.4f), quickShrink: true, glow: false));
						}
					}
				}
				if (base.Projectile.timeLeft <= 200 && Time % 3f == 0f)
				{
					Vector2 randPos = (-shootVel * 1.5f).RotatedByRandom(1.0) * Main.rand.NextFloat(5f, 15f);
					int type = ModContent.ProjectileType<SupremeCataclysmFist>();
					SoundStyle style = SupremeCalamitas.BrimstoneShotSound with
					{
						Volume = 1.2f,
						Pitch = 0.55f
					};
					SoundEngine.PlaySound(in style, base.Projectile.Center);
					Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), base.Projectile.Center + randPos, -shootVel * MathHelper.Clamp(Time * 0.02f, 0.1f, 1.8f) * Main.rand.NextFloat(0.75f, 1f), type, base.Projectile.damage, 0f, Main.myPlayer, 0f, Main.rand.Next(0, 2));
				}
				if (base.Projectile.timeLeft <= 200 && Time % 3f == 0f && !broIsAlive)
				{
					Vector2 randPos2 = (-shootVel * 1.5f).RotatedBy(MathHelper.ToRadians(120f)).RotatedByRandom(1.0) * Main.rand.NextFloat(5f, 15f);
					Vector2 randPos3 = (-shootVel * 1.5f).RotatedBy(MathHelper.ToRadians(-120f)).RotatedByRandom(1.0) * Main.rand.NextFloat(5f, 15f);
					int type2 = ModContent.ProjectileType<SupremeCataclysmFist>();
					Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), base.Projectile.Center + randPos2, (-shootVel * MathHelper.Clamp(Time * 0.02f, 0.1f, 1.8f)).RotatedBy(MathHelper.ToRadians(120f)) * Main.rand.NextFloat(0.75f, 1f), type2, base.Projectile.damage, 0f, Main.myPlayer, 0f, Main.rand.Next(0, 2));
					Projectile.NewProjectile(base.Projectile.GetSource_FromAI(), base.Projectile.Center + randPos3, (-shootVel * MathHelper.Clamp(Time * 0.02f, 0.1f, 1.8f)).RotatedBy(MathHelper.ToRadians(-120f)) * Main.rand.NextFloat(0.75f, 1f), type2, base.Projectile.damage, 0f, Main.myPlayer, 0f, Main.rand.Next(0, 2));
				}
				if (base.Projectile.timeLeft == 1)
				{
					int points2 = 25;
					float radians2 = (float)Math.PI * 2f / (float)points2;
					Vector2 spinningPoint2 = Vector2.Normalize(new Vector2(-4f, -4f));
					for (int n = 0; n < 2; n++)
					{
						float rotRando2 = Main.rand.NextFloat(0.1f, 2.5f);
						for (int num = 0; num < points2; num++)
						{
							Vector2 velocity2 = spinningPoint2.RotatedBy(radians2 * (float)num).RotatedBy(-0.45f * rotRando2);
							Dust dust3 = Dust.NewDustPerfect(base.Projectile.Center + velocity2 * (float)((n == 0) ? 7 : 5), 279, velocity2 * (float)((n == 0) ? 9 : 7));
							dust3.noGravity = true;
							dust3.scale = Main.rand.NextFloat(1.3f, 1.9f);
							dust3.color = Color.Red;
						}
					}
					base.Projectile.Kill();
				}
				Vector2 vel5 = Utils.RotatedByRandom(new Vector2(14f, 14f), 100.0) * Main.rand.NextFloat(0.1f, 2.5f);
				Dust dust4 = Dust.NewDustPerfect(base.Projectile.Center + vel5 * 2f, 279, vel5);
				dust4.noGravity = true;
				dust4.scale = Main.rand.NextFloat(0.9f, 1.2f);
				dust4.color = Color.Red;
			}
		}
		else if (base.Projectile.ai[2] <= 2f)
		{
			if (base.Projectile.ai[2] == 1f)
			{
				base.Projectile.extraUpdates = 2;
			}
			else
			{
				Projectile projectile5 = base.Projectile;
				projectile5.velocity *= 1.0055f;
			}
			if (base.Projectile.ai[2] == 0f)
			{
				base.Projectile.scale = 0.8f;
				base.Projectile.extraUpdates = 2;
				base.Projectile.Opacity = 1f;
				if (base.Projectile.timeLeft > 300)
				{
					base.Projectile.timeLeft = 300;
				}
				Projectile projectile6 = base.Projectile;
				projectile6.velocity *= 1.006f;
			}
			if (Main.zenithWorld && base.Projectile.ai[2] >= 3f)
			{
				return;
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
		}
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 5 % Main.projFrames[base.Type];
		if (base.Projectile.ai[2] < 3f)
		{
			base.Projectile.Opacity = Utils.GetLerpValue(0f, 12f, base.Projectile.timeLeft, clamped: true) * Utils.GetLerpValue(1200f, 1193f, base.Projectile.timeLeft, clamped: true);
		}
		Time++;
		if (!NPC.AnyNPCs(ModContent.NPCType<SupremeCataclysm>()) && !Main.zenithWorld)
		{
			base.Projectile.timeLeft = 1;
			for (int num2 = 0; num2 < 10; num2++)
			{
				Vector2 velocity3 = Utils.RotatedByRandom(new Vector2(7f, 7f), 100.0) * Main.rand.NextFloat(0.8f, 1.2f);
				Dust dust5 = Dust.NewDustPerfect(base.Projectile.Center + velocity3, 66, velocity3 * Main.rand.NextFloat(0.2f, 1f));
				dust5.noGravity = true;
				dust5.scale = Main.rand.NextFloat(1.3f, 1.9f);
				dust5.color = Color.Lerp(Color.Red, Color.Magenta, 0.5f);
			}
		}
		Lighting.AddLight(base.Projectile.Center, 0.5f * base.Projectile.Opacity, 0f, 0f);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		if (Main.zenithWorld && base.Projectile.ai[2] >= 3f)
		{
			Texture2D ballTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/Basketball", (AssetRequestMode)2).Value;
			Main.EntitySpriteDraw(ballTexture, base.Projectile.Center - Main.screenPosition, null, lightColor, base.Projectile.rotation, ballTexture.Size() * 0.5f, base.Projectile.scale / 1.8f, (SpriteEffects)0);
			return false;
		}
		((Color)(ref lightColor)).R = (byte)(255f * base.Projectile.Opacity);
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		if (base.Projectile.ai[1] == 1f)
		{
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/SupremeCataclysmFistAlt", (AssetRequestMode)2).Value;
		}
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(texture, drawPosition, frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, direction);
		return false;
	}

	public override bool CanHitPlayer(Player target)
	{
		if (!(base.Projectile.Opacity >= 1f))
		{
			return base.Projectile.ai[2] >= 3f;
		}
		return true;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/Kickball");
		style.PitchVariance = 0.15f;
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = 0f - oldVelocity.X;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y)
		{
			base.Projectile.velocity.Y = 0f - oldVelocity.Y;
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, (base.Projectile.ai[2] >= 3f) ? 90f : (35f * base.Projectile.scale), targetHitbox);
	}
}
