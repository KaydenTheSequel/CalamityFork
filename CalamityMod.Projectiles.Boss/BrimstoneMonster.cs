using System;
using System.Collections.Generic;
using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using CalamityMod.Events;
using CalamityMod.NPCs;
using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.Particles;
using CalamityMod.Systems.Collections;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Utilities;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class BrimstoneMonster : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle SpawnSound = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/BrimstoneMonsterSpawn");

	public static readonly SoundStyle DroneSound = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/BrimstoneMonsterDrone");

	public SlotId RumbleSlot;

	public static Asset<Texture2D> screamTex;

	internal static readonly float CircularHitboxRadius = 170f;

	public static int MinimumDamagePerFrame = 4;

	public static int MaximumDamagePerFrame = 14;

	public static float AdrenalineLossPerFrame = 0.04f;

	public static float SpeedToForceMaxDamage = 25f;

	private float speedAdd;

	private float speedLimit;

	private int time;

	private int sitStill = 90;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			screamTex = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/ScreamyFace", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.Projectile.Calamity().DealsDefenseDamage = true;
		base.Projectile.width = 320;
		base.Projectile.height = 320;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.hide = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 36000;
		base.Projectile.Opacity = 0f;
		base.CooldownSlot = 1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(speedAdd);
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(speedLimit);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		speedAdd = reader.ReadSingle();
		base.Projectile.localAI[0] = reader.ReadSingle();
		speedLimit = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0348: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0531: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0593: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06de: Unknown result type (might be due to invalid IL or missing references)
		//IL_0833: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0800: Unknown result type (might be due to invalid IL or missing references)
		//IL_0805: Unknown result type (might be due to invalid IL or missing references)
		//IL_0809: Unknown result type (might be due to invalid IL or missing references)
		//IL_080e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0815: Unknown result type (might be due to invalid IL or missing references)
		//IL_081a: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b4: Unknown result type (might be due to invalid IL or missing references)
		if (time == 0)
		{
			base.Projectile.scale = 0.1f;
			for (int i = 0; i < 2; i++)
			{
				GeneralParticleHandler.SpawnParticle(new BloomParticle(base.Projectile.Center, Vector2.Zero, Color.Lerp(Color.Red, Color.Magenta, 0.3f), 1.45f, 0f, 120, fade: false));
			}
			GeneralParticleHandler.SpawnParticle(new BloomParticle(base.Projectile.Center, Vector2.Zero, Color.White, 1.35f, 0f, 120, fade: false));
		}
		time++;
		if (base.Projectile.scale < 1.9f && base.Projectile.timeLeft > 90)
		{
			if (base.Projectile.scale < 1.5f)
			{
				for (int j = 0; j < 10; j++)
				{
					Vector2 dustVel = Utils.RotatedByRandom(new Vector2(30f, 30f), 100.0) * Main.rand.NextFloat(0.05f, 1.2f);
					Dust dust = Dust.NewDustPerfect(base.Projectile.Center * (base.Projectile.scale * 5f), 235, dustVel);
					dust.noGravity = true;
					dust.scale = Main.rand.NextFloat(1.7f, 2.8f) - base.Projectile.scale * 1.5f;
				}
				for (int k = 0; k < 3; k++)
				{
					Vector2 sparkVel = Utils.RotatedByRandom(new Vector2(20f, 20f), 100.0) * Main.rand.NextFloat(0.1f, 1.1f);
					GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(base.Projectile.Center + sparkVel * 2f * (base.Projectile.scale * 5f), sparkVel, affectedByGravity: false, 60, Main.rand.NextFloat(1.55f, 2.75f) - base.Projectile.scale * 1.5f, Color.Lerp(Color.Red, Color.Magenta, 0.5f), AddativeBlend: true, needed: true));
				}
			}
			base.Projectile.scale += 0.01f;
		}
		if (SoundEngine.TryGetActiveSound(RumbleSlot, out ActiveSound RumbleSound) && RumbleSound.IsPlaying)
		{
			RumbleSound.Position = base.Projectile.Center;
		}
		if (!CalamityPlayer.areThereAnyDamnBosses)
		{
			if (base.Projectile.timeLeft > 90)
			{
				base.Projectile.timeLeft = 90;
			}
			base.Projectile.netUpdate = true;
		}
		int choice = (int)base.Projectile.ai[1];
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.soundDelay = 1125 - choice * 225;
			SoundEngine.PlaySound(in SpawnSound, base.Projectile.Center);
			if (base.Projectile.ai[1] == 0f && base.Projectile.timeLeft >= 90)
			{
				SlotId rumbleSlot;
				if (!Main.zenithWorld)
				{
					SoundStyle style = DroneSound with
					{
						IsLooped = true
					};
					rumbleSlot = SoundEngine.PlaySound(in style, base.Projectile.Center, (ActiveSound _) => new ProjectileAudioTracker(base.Projectile).IsActiveAndInGame());
				}
				else
				{
					SoundStyle style2 = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/GFBDrone")
					{
						IsLooped = true
					};
					rumbleSlot = SoundEngine.PlaySound(in style2, base.Projectile.Center, (ActiveSound _) => new ProjectileAudioTracker(base.Projectile).IsActiveAndInGame());
				}
				RumbleSlot = rumbleSlot;
			}
			base.Projectile.localAI[0]++;
			speedLimit = 23f;
		}
		if (speedAdd < speedLimit)
		{
			speedAdd += 0.04f;
		}
		float targetDist = ((Main.player[choice].dead || !Main.player[choice].active || Main.player[choice] == null) ? 2000f : Vector2.Distance(Main.player[choice].Center, base.Projectile.Center));
		if (base.Projectile.ai[1] == 0f)
		{
			if (targetDist <= 1400f)
			{
				float targetPitchShift = Utils.GetLerpValue(1400f, 700f, targetDist);
				if (SoundEngine.TryGetActiveSound(RumbleSlot, out ActiveSound RumblePitch) && RumblePitch.IsPlaying)
				{
					RumblePitch.Pitch = MathHelper.Lerp(Main.zenithWorld ? (-0.7f) : 0f, Main.zenithWorld ? 0.2f : 0.7f, targetPitchShift);
					RumblePitch.Volume = MathHelper.Lerp(0.3f, 0.8f, targetPitchShift);
				}
			}
			base.Projectile.soundDelay--;
			if (SoundEngine.TryGetActiveSound(RumbleSlot, out ActiveSound RumblePlaying) && RumblePlaying.IsPlaying)
			{
				base.Projectile.soundDelay = 1;
			}
			if (base.Projectile.soundDelay <= 0 && base.Projectile.timeLeft >= 90)
			{
				SlotId rumbleSlot2;
				if (!Main.zenithWorld)
				{
					SoundStyle style = DroneSound with
					{
						IsLooped = true
					};
					rumbleSlot2 = SoundEngine.PlaySound(in style, base.Projectile.Center, (ActiveSound _) => new ProjectileAudioTracker(base.Projectile).IsActiveAndInGame());
				}
				else
				{
					SoundStyle style2 = new SoundStyle("CalamityMod/Sounds/Custom/SCalSounds/GFBDrone")
					{
						IsLooped = true
					};
					rumbleSlot2 = SoundEngine.PlaySound(in style2, base.Projectile.Center, (ActiveSound _) => new ProjectileAudioTracker(base.Projectile).IsActiveAndInGame());
				}
				RumbleSlot = rumbleSlot2;
			}
			if (base.Projectile.timeLeft < 90)
			{
				RumblePlaying?.Stop();
			}
			if (CalamityGlobalNPC.SCal == -1)
			{
				RumblePlaying?.Stop();
			}
		}
		if (base.Projectile.timeLeft < 90)
		{
			base.Projectile.Opacity = MathHelper.Clamp((float)base.Projectile.timeLeft / 90f, 0f, 1f);
		}
		else
		{
			base.Projectile.Opacity = MathHelper.Clamp(1f - (float)(base.Projectile.timeLeft - 35910) / 90f, 0f, 1f);
		}
		if (base.Projectile.scale >= 1.9f)
		{
			sitStill--;
		}
		if (sitStill > 0)
		{
			return;
		}
		bool num = CalamityWorld.revenge || BossRushEvent.BossRushActive;
		bool death = CalamityWorld.death || BossRushEvent.BossRushActive;
		Lighting.AddLight(base.Projectile.Center, 3f * base.Projectile.Opacity, 0f, 0f);
		float inertia = (num ? 5f : 5.5f) + speedAdd;
		float speed = (num ? 2.9f : 2.2f) + speedAdd * 0.25f;
		float minDist = 160f;
		if (NPC.AnyNPCs(ModContent.NPCType<SoulSeekerSupreme>()) || NPC.AnyNPCs(ModContent.NPCType<BrimstoneHeart>()))
		{
			inertia *= 1.5f;
			speed *= 0.8f;
		}
		int target = (int)base.Projectile.ai[0];
		if (target >= 0 && Main.player[target].active && !Main.player[target].dead)
		{
			if (base.Projectile.Distance(Main.player[target].Center) > minDist)
			{
				Vector2 moveDirection = base.Projectile.SafeDirectionTo(Main.player[target].Center, Vector2.UnitY);
				base.Projectile.velocity = (base.Projectile.velocity * (inertia - 1f) + moveDirection * speed) / inertia;
			}
		}
		else
		{
			base.Projectile.ai[0] = (int)Player.FindClosest(base.Projectile.Center, 1, 1);
			base.Projectile.netUpdate = true;
		}
		if (death)
		{
			speedLimit = 15f;
			return;
		}
		float pushForce = 0.05f;
		for (int k2 = 0; k2 < Main.maxProjectiles; k2++)
		{
			Projectile otherProj = Main.projectile[k2];
			if (!otherProj.active || k2 == base.Projectile.whoAmI)
			{
				continue;
			}
			bool num2 = otherProj.type == base.Projectile.type;
			float taxicabDist = Vector2.Distance(base.Projectile.Center, otherProj.Center);
			float distancegate = (Main.zenithWorld ? 360f : 320f);
			if (num2 && taxicabDist < distancegate)
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

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, CircularHitboxRadius * base.Projectile.scale * base.Projectile.Opacity, targetHitbox);
	}

	public override bool CanHitPlayer(Player player)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.Opacity < 1f)
		{
			return false;
		}
		if (player.HasIFrames() || player.creativeGodMode)
		{
			return true;
		}
		float num = base.Projectile.DistanceSQ(player.Center);
		float radiusSQ = CircularHitboxRadius * CircularHitboxRadius * base.Projectile.scale * base.Projectile.scale;
		float radiusRatio = num / radiusSQ;
		if (Colliding(base.Projectile.Hitbox, player.Hitbox) == false)
		{
			return false;
		}
		OnHitPlayer_Internal(player);
		float speedRatio = ((Vector2)(ref player.velocity)).LengthSquared() / (SpeedToForceMaxDamage * SpeedToForceMaxDamage);
		float damageApplicationRatio = MathHelper.Max(radiusRatio, speedRatio);
		int healthToDrain = (int)MathHelper.Lerp((float)MaximumDamagePerFrame, (float)MinimumDamagePerFrame, damageApplicationRatio);
		if (healthToDrain < MinimumDamagePerFrame)
		{
			healthToDrain = MinimumDamagePerFrame;
		}
		player.statLife -= healthToDrain;
		if (time % 6 == 0)
		{
			SoundStyle style = new SoundStyle("CalamityMod/Sounds/Item/WeldingBurn");
			style.Volume = 0.25f;
			style.Pitch = 0.4f;
			SoundEngine.PlaySound(in style, player.Center);
		}
		GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(player.Center, Utils.RotatedByRandom(new Vector2(6f, 6f), 100.0) * Main.rand.NextFloat(0.3f, 1.1f), affectedByGravity: false, 60, Main.rand.NextFloat(1.55f, 3.75f), Main.rand.NextBool() ? Color.Red : Color.Lerp(Color.Red, Color.Magenta, 0.5f), AddativeBlend: true, needed: true));
		if (Main.rand.NextBool())
		{
			GeneralParticleHandler.SpawnParticle(new GlowOrbParticle(player.Center, Utils.RotatedByRandom(new Vector2(6f, 6f), 100.0) * Main.rand.NextFloat(0.3f, 1.1f), affectedByGravity: false, 60, Main.rand.NextFloat(1.55f, 3.75f), Color.Black, AddativeBlend: false, needed: true, GlowCenter: false));
		}
		CalamityPlayer modPlayer = player.Calamity();
		if (modPlayer.AdrenalineEnabled)
		{
			modPlayer.adrenaline *= 1f - AdrenalineLossPerFrame;
		}
		string path = (Main.zenithWorld ? "GFB" : "");
		path += Main.rand.Next(1, 4);
		if (Main.myPlayer == player.whoAmI && player.statLife <= 0)
		{
			player.KillMe(PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.WhisperingMaelstrom" + path).ToNetworkText(player.name)), 1000.0, -1);
		}
		return false;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0 && base.Projectile.Opacity == 1f)
		{
			OnHitPlayer_Internal(target);
		}
	}

	private static void OnHitPlayer_Internal(Player target)
	{
		target.AddBuff(ModContent.BuffType<VulnerabilityHex>(), 360);
		if (CalamityGlobalNPC.SCal == -1 || !Main.npc[CalamityGlobalNPC.SCal].active || !Main.npc[CalamityGlobalNPC.SCal].ModNPC<SupremeCalamitas>().permafrost)
		{
			return;
		}
		for (int l = 0; l < Player.MaxBuffs; l++)
		{
			int buffType = target.buffType[l];
			if (target.buffTime[l] > 0 && CalamityBuffSets.BuffedByAmalgam[buffType])
			{
				target.DelBuff(l);
				l--;
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		if (SoundEngine.TryGetActiveSound(RumbleSlot, out ActiveSound RumblePlaying) && RumblePlaying.IsPlaying)
		{
			RumblePlaying?.Stop();
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0384: Unknown result type (might be due to invalid IL or missing references)
		//IL_0389: Unknown result type (might be due to invalid IL or missing references)
		//IL_0399: Unknown result type (might be due to invalid IL or missing references)
		//IL_039e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03be: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_0457: Unknown result type (might be due to invalid IL or missing references)
		//IL_045c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0470: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		_ = TextureAssets.Projectile[base.Type].Value;
		((Color)(ref lightColor)).R = (byte)(255f * base.Projectile.Opacity);
		Main.spriteBatch.End();
		Effect shieldEffect = Filters.Scene["CalamityMod:HellBall"].GetShader().Shader;
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, shieldEffect, Main.GameViewMatrix.TransformationMatrix);
		float noiseScale = 0.6f;
		shieldEffect.Parameters["time"].SetValue((float)base.Projectile.timeLeft / 60f * 0.24f);
		shieldEffect.Parameters["blowUpPower"].SetValue(3.2f);
		shieldEffect.Parameters["blowUpSize"].SetValue(0.4f);
		shieldEffect.Parameters["noiseScale"].SetValue(noiseScale);
		float opacity = base.Projectile.Opacity;
		shieldEffect.Parameters["shieldOpacity"].SetValue(opacity);
		shieldEffect.Parameters["shieldEdgeBlendStrenght"].SetValue(4f);
		Color edgeColor = Color.Black * opacity;
		Color shieldColor = Color.Lerp(Color.Red, Color.Magenta, 0.5f) * opacity;
		shieldEffect.Parameters["shieldColor"].SetValue(((Color)(ref shieldColor)).ToVector3());
		shieldEffect.Parameters["shieldEdgeColor"].SetValue(((Color)(ref edgeColor)).ToVector3());
		Vector2 pos = base.Projectile.Center - Main.screenPosition;
		float scale = 0.715f;
		Main.spriteBatch.Draw(screamTex.Value, pos, (Rectangle?)null, Color.White, 0f, screamTex.Size() * 0.5f, scale * base.Projectile.scale * base.Projectile.Opacity, (SpriteEffects)0, 0f);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
		if (CalamityGlobalNPC.SCal != -1 && Main.npc[CalamityGlobalNPC.SCal].active && Main.npc[CalamityGlobalNPC.SCal].ModNPC<SupremeCalamitas>().permafrost)
		{
			Texture2D hageTex = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Boss/BrimstoneMonsterII", (AssetRequestMode)2).Value;
			((Color)(ref lightColor)).B = (byte)(255f * base.Projectile.Opacity);
			Main.EntitySpriteDraw(hageTex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, hageTex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		}
		else
		{
			Texture2D vortexTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/SoulVortex", (AssetRequestMode)2).Value;
			Texture2D centerTexture = ModContent.Request<Texture2D>("CalamityMod/Particles/LargeBloom", (AssetRequestMode)2).Value;
			for (int i = 0; i < 10; i++)
			{
				float angle = (float)Math.PI * 2f * (float)i / 3f + Main.GlobalTimeWrappedHourly * ((float)Math.PI * 2f);
				Color drawColor = Color.Lerp(Color.Lerp(Color.Red, Color.Magenta, (float)i * 0.15f), Color.Black, (float)i * 0.2f) * 0.5f;
				((Color)(ref drawColor)).A = 0;
				Vector2 drawPosition = base.Projectile.Center - Main.screenPosition;
				drawPosition += (angle + Main.GlobalTimeWrappedHourly * (float)i / 16f).ToRotationVector2() * 6f;
				Main.EntitySpriteDraw(vortexTexture, drawPosition, null, drawColor * base.Projectile.Opacity, 0f - angle + (float)Math.PI / 2f, vortexTexture.Size() * 0.5f, base.Projectile.scale * (1f - (float)i * 0.05f) * base.Projectile.Opacity, (SpriteEffects)0);
			}
			Main.EntitySpriteDraw(centerTexture, base.Projectile.Center - Main.screenPosition, null, Color.Black * base.Projectile.Opacity, base.Projectile.rotation, centerTexture.Size() * 0.5f, base.Projectile.scale * 0.9f * base.Projectile.Opacity, (SpriteEffects)0);
		}
		return false;
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		behindNPCs.Add(index);
	}
}
