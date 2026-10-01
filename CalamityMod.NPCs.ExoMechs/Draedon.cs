using System;
using System.IO;
using CalamityMod.Events;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.NPCs.ExoMechs.Apollo;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.ExoMechs.Artemis;
using CalamityMod.NPCs.ExoMechs.Thanatos;
using CalamityMod.Packets;
using CalamityMod.Projectiles.Turret;
using CalamityMod.Sounds;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.GameContent.Bestiary;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.ExoMechs;

[LongDistanceNetSync]
public class Draedon : ModNPC
{
	public int KillReappearTextCountdown;

	public float DefeatTimer;

	public float ProjectorOffset = 1000f;

	public float ProjFrameCounter;

	public float ProjFrameChangeCounter;

	public bool ShouldStartStandingUp;

	public bool exoMechdusa;

	public static readonly Color TextColor;

	public static readonly Color TextColorEdgy;

	public const int HologramFadeinTime = 45;

	public const int TalkDelay = 150;

	public const int DelayPerDialogLine = 130;

	public const int ExoMechChooseDelay = 680;

	public const int ExoMechShakeTime = 100;

	public const int ExoMechPhaseDialogueTime = 780;

	public const int DelayBeforeDefeatStandup = 30;

	public static readonly SoundStyle LaughSound;

	public static readonly SoundStyle TeleportSound;

	public static readonly SoundStyle SelectionSound;

	public static Asset<Texture2D> Texture_Glow;

	public static Asset<Texture2D> HoloTexture;

	public static Asset<Texture2D> ProjectorTexture;

	public static Asset<Texture2D> ProjectorTexture_Glow;

	public static int PulseRifleDamage;

	public Vector2 HoverDestinationOffset
	{
		get
		{
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(base.NPC.ai[1], base.NPC.ai[2]);
		}
		set
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_001f: Unknown result type (might be due to invalid IL or missing references)
			base.NPC.ai[1] = value.X;
			base.NPC.ai[2] = value.Y;
		}
	}

	public Player PlayerToFollow => Main.player[base.NPC.target];

	public ref float TalkTimer => ref base.NPC.ai[0];

	public ref float GeneralTimer => ref base.NPC.ai[3];

	public ref float DialogueType => ref base.NPC.localAI[0];

	public ref float HologramEffectTimer => ref base.NPC.localAI[1];

	public bool HasBeenKilled
	{
		get
		{
			return base.NPC.localAI[2] == 1f;
		}
		set
		{
			base.NPC.localAI[2] = value.ToInt();
		}
	}

	public ref float KillReappearDelay => ref base.NPC.localAI[3];

	public static bool ExoMechIsPresent
	{
		get
		{
			if (NPC.AnyNPCs(ModContent.NPCType<ThanatosHead>()))
			{
				return true;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<AresBody>()))
			{
				return true;
			}
			if (NPC.AnyNPCs(ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis>()) || NPC.AnyNPCs(ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>()))
			{
				return true;
			}
			return false;
		}
	}

	public ref float BossRushCounter => ref base.NPC.Calamity().newAI[0];

	public override void SetStaticDefaults()
	{
		Main.npcFrameCount[base.Type] = 12;
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.PortraitPositionYOverride = 40f;
		nPCBestiaryDrawModifiers.Scale = 0.7f;
		nPCBestiaryDrawModifiers.PortraitScale = 0.85f;
		nPCBestiaryDrawModifiers.SpriteDirection = 1;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		value.Position.Y += 45f;
		NPCID.Sets.NPCBestiaryDrawOffset[base.Type] = value;
		NPCID.Sets.ShouldBeCountedAsBoss[base.Type] = true;
		NPCID.Sets.MustAlwaysDraw[base.Type] = true;
		if (!Main.dedServ)
		{
			Texture_Glow = ModContent.Request<Texture2D>(Texture + "Glowmask", (AssetRequestMode)2);
			HoloTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/HologramDraedon", (AssetRequestMode)2);
			ProjectorTexture = ModContent.Request<Texture2D>(Texture + "Projector", (AssetRequestMode)2);
			ProjectorTexture_Glow = ModContent.Request<Texture2D>(Texture + "ProjectorGlowmask", (AssetRequestMode)2);
		}
	}

	public override void SetDefaults()
	{
		base.NPC.damage = 0;
		base.NPC.width = (base.NPC.height = 86);
		base.NPC.defense = 100;
		base.NPC.lifeMax = 16000;
		base.NPC.noGravity = true;
		base.NPC.noTileCollide = true;
		base.NPC.dontTakeDamage = true;
		NPC nPC = base.NPC;
		int aiStyle = (base.AIType = -1);
		nPC.aiStyle = aiStyle;
		base.NPC.knockBackResist = 0f;
		base.NPC.DeathSound = SoundID.NPCDeath14;
		base.NPC.chaseable = false;
		base.NPC.Calamity().ProvidesProximityRage = false;
	}

	public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
	{
		bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[1]
		{
			new FlavorTextBestiaryInfoElement("Mods.CalamityMod.Bestiary.Draedon")
		});
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(DialogueType);
		writer.Write(DefeatTimer);
		writer.Write(ProjectorOffset);
		writer.Write(ProjFrameCounter);
		writer.Write(ProjFrameChangeCounter);
		writer.Write(HologramEffectTimer);
		writer.Write(KillReappearDelay);
		writer.Write(ShouldStartStandingUp);
		writer.Write(HasBeenKilled);
		writer.Write(BossRushCounter);
		writer.Write(exoMechdusa);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		DialogueType = reader.ReadSingle();
		DefeatTimer = reader.ReadSingle();
		ProjectorOffset = reader.ReadSingle();
		ProjFrameCounter = reader.ReadSingle();
		ProjFrameChangeCounter = reader.ReadSingle();
		HologramEffectTimer = reader.ReadSingle();
		KillReappearDelay = reader.ReadSingle();
		ShouldStartStandingUp = reader.ReadBoolean();
		HasBeenKilled = reader.ReadBoolean();
		BossRushCounter = reader.ReadSingle();
		exoMechdusa = reader.ReadBoolean();
	}

	public override void AI()
	{
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_036e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_0611: Unknown result type (might be due to invalid IL or missing references)
		//IL_0449: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0756: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0897: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0860: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_096a: Unknown result type (might be due to invalid IL or missing references)
		//IL_09e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b01: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b0c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0792: Unknown result type (might be due to invalid IL or missing references)
		//IL_0825: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_092f: Unknown result type (might be due to invalid IL or missing references)
		//IL_09a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a4b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a17: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b23: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b2e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b33: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b38: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b41: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b48: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b4d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b61: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b66: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b70: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b75: Unknown result type (might be due to invalid IL or missing references)
		//IL_0b7a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a69: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < base.NPC.buffImmune.Length; k++)
		{
			base.NPC.buffImmune[k] = true;
		}
		CalamityGlobalNPC.draedon = base.NPC.whoAmI;
		CalamityGlobalNPC.draedonAmbience = -1;
		base.NPC.timeLeft = 3600;
		bool bossRush = BossRushEvent.BossRushActive;
		if (!ExoMechIsPresent)
		{
			CalamityGlobalNPC.draedonAmbience = base.NPC.whoAmI;
		}
		if (TalkTimer == 0f)
		{
			base.NPC.TargetClosest(faceTarget: false);
			SoundEngine.PlaySound(in TeleportSound, PlayerToFollow.Center);
		}
		if (PlayerToFollow.dead || !PlayerToFollow.active)
		{
			base.NPC.TargetClosest(faceTarget: false);
			if (PlayerToFollow.dead || !PlayerToFollow.active)
			{
				base.NPC.life = 0;
				base.NPC.HitEffect();
				base.NPC.active = false;
				base.NPC.netUpdate = true;
				return;
			}
		}
		base.NPC.position.Y = MathHelper.Clamp(base.NPC.position.Y, 150f, (float)Main.maxTilesY * 16f - 150f);
		base.NPC.spriteDirection = (PlayerToFollow.Center.X < base.NPC.Center.X).ToDirectionInt();
		if (!exoMechdusa && CalamityWorld.DraedonMechdusa && Main.zenithWorld)
		{
			exoMechdusa = true;
			CalamityWorld.DraedonMechdusa = false;
			if (Main.netMode != 0)
			{
				CodebreakerSummonStuffPacket.Send();
			}
		}
		if (KillReappearDelay > 0f)
		{
			if (KillReappearDelay <= 60f)
			{
				ProjectorOffset -= 14.5f;
			}
			base.NPC.Opacity = 0f;
			KillReappearDelay--;
			if (KillReappearDelay <= 0f)
			{
				KillReappearTextCountdown = 96;
				DefeatTimer = MathHelper.Max(DefeatTimer, 450f);
				base.NPC.netUpdate = true;
			}
			return;
		}
		if (KillReappearTextCountdown > 0)
		{
			base.NPC.Opacity = MathHelper.Clamp(base.NPC.Opacity + 0.05f, 0f, 1f);
			KillReappearTextCountdown--;
			if (KillReappearTextCountdown == 20)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonEndKillAttemptText", TextColor);
			}
			return;
		}
		if (TalkTimer <= 45f)
		{
			HologramEffectTimer = TalkTimer;
			if (!HasBeenKilled)
			{
				base.NPC.Opacity = Utils.GetLerpValue(0f, 8f, TalkTimer, clamped: true);
			}
		}
		if (TalkTimer == 50f)
		{
			ShouldStartStandingUp = true;
		}
		if ((CalamityWorld.TalkedToDraedon | bossRush) && TalkTimer > 70f && TalkTimer < 575f && !exoMechdusa)
		{
			TalkTimer = 575f;
			base.NPC.netUpdate = true;
		}
		if (!exoMechdusa)
		{
			if (Main.netMode != 1 && TalkTimer == 150f)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonIntroductionText1", TextColor);
				base.NPC.netUpdate = true;
			}
			if (Main.netMode != 1 && TalkTimer == 280f)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonIntroductionText2", TextColor);
				base.NPC.netUpdate = true;
			}
			if (Main.netMode != 1 && TalkTimer == 410f)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonIntroductionText3", TextColor);
				base.NPC.netUpdate = true;
			}
			if (Main.netMode != 1 && TalkTimer == 540f)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonIntroductionText4", TextColor);
				base.NPC.netUpdate = true;
			}
			if (Main.netMode != 1 && TalkTimer == 670f)
			{
				if (bossRush)
				{
					CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonBossRushText", TextColorEdgy);
				}
				else if (CalamityWorld.TalkedToDraedon)
				{
					CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonResummonText", TextColorEdgy);
				}
				else
				{
					CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonIntroductionText5", TextColorEdgy);
				}
				if (!CalamityWorld.TalkedToDraedon)
				{
					CalamityWorld.TalkedToDraedon = true;
					CalamityNetcode.SyncWorld();
				}
				base.NPC.netUpdate = true;
			}
			if (TalkTimer >= 680f && TalkTimer < 688f && CalamityWorld.DraedonMechToSummon == ExoMech.None)
			{
				PlayerToFollow.Calamity().AbleToSelectExoMech = true;
				TalkTimer = 680f;
				if (bossRush)
				{
					BossRushCounter++;
					if (BossRushCounter > 1200f && CalamityWorld.DraedonMechToSummon == ExoMech.None)
					{
						CalamityWorld.DraedonMechToSummon = (ExoMech)Main.rand.Next(1, 4);
						if (Main.netMode != 0)
						{
							ExoMechSelectionPacket.Send();
						}
					}
				}
			}
		}
		else
		{
			if (Main.netMode != 1 && TalkTimer == 150f)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonMechdusaBeginText", TextColorEdgy);
				base.NPC.netUpdate = true;
			}
			if (Main.netMode != 1 && TalkTimer == 210f)
			{
				if (!CalamityWorld.TalkedToDraedon)
				{
					CalamityWorld.TalkedToDraedon = true;
					CalamityNetcode.SyncWorld();
				}
				base.NPC.netUpdate = true;
			}
		}
		if (ExoMechIsPresent || DefeatTimer > 0f)
		{
			FlyAroundInGamerChair();
			GeneralTimer++;
		}
		if (TalkTimer > 688f && TalkTimer < 780f)
		{
			Main.LocalPlayer.Calamity().GeneralScreenShakePower = Utils.GetLerpValue(4200f, 1400f, Main.LocalPlayer.Distance(PlayerToFollow.Center), clamped: true) * 18f;
			Main.LocalPlayer.Calamity().GeneralScreenShakePower *= Utils.GetLerpValue(685f, 780f, TalkTimer, clamped: true);
		}
		if ((TalkTimer == 690f || (TalkTimer == 210f && exoMechdusa)) && !ExoMechIsPresent)
		{
			if (Main.netMode != 1)
			{
				SummonExoMech();
			}
			if (!Main.dedServ)
			{
				SoundStyle style = CommonCalamitySounds.FlareSound with
				{
					Volume = CommonCalamitySounds.FlareSound.Volume * 1.55f
				};
				SoundEngine.PlaySound(in style, PlayerToFollow.Center);
				if (!exoMechdusa)
				{
					SoundEngine.PlaySound(in SelectionSound, PlayerToFollow.Center);
				}
			}
		}
		if (!bossRush && !exoMechdusa)
		{
			switch ((int)DialogueType)
			{
			case 1:
				if (Main.netMode != 1 && TalkTimer == 780f)
				{
					CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonExoPhase1Text1", TextColor);
					base.NPC.netUpdate = true;
				}
				if (Main.netMode != 1 && TalkTimer == 910f)
				{
					CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonExoPhase1Text2", TextColor);
					base.NPC.netUpdate = true;
				}
				break;
			case 2:
				if (TalkTimer == 780f)
				{
					SoundEngine.PlaySound(in LaughSound, PlayerToFollow.Center);
					if (Main.netMode != 1)
					{
						CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonExoPhase2Text1", TextColor);
						base.NPC.netUpdate = true;
					}
				}
				if (Main.netMode != 1 && TalkTimer == 910f)
				{
					CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonExoPhase2Text2", TextColor);
					base.NPC.netUpdate = true;
				}
				break;
			case 3:
				if (Main.netMode != 1 && TalkTimer == 780f)
				{
					CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonExoPhase3Text1", TextColor);
					base.NPC.netUpdate = true;
				}
				if (TalkTimer == 910f)
				{
					SoundEngine.PlaySound(in LaughSound, PlayerToFollow.Center);
					if (Main.netMode != 1)
					{
						CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonExoPhase3Text2", TextColor);
						base.NPC.netUpdate = true;
					}
				}
				break;
			case 4:
				if (Main.netMode != 1 && TalkTimer == 780f)
				{
					CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonExoPhase4Text1", TextColor);
					base.NPC.netUpdate = true;
				}
				if (Main.netMode != 1 && TalkTimer == 910f)
				{
					CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonExoPhase4Text2", TextColor);
					base.NPC.netUpdate = true;
				}
				break;
			case 5:
				if (Main.netMode != 1 && TalkTimer == 780f)
				{
					CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonExoPhase5Text1", TextColor);
					base.NPC.netUpdate = true;
				}
				if (Main.netMode != 1 && TalkTimer == 910f)
				{
					CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonExoPhase5Text2", TextColor);
					base.NPC.netUpdate = true;
				}
				break;
			case 6:
				if (Main.netMode != 1 && TalkTimer == 780f)
				{
					CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonExoPhase6Text1", TextColor);
					base.NPC.netUpdate = true;
				}
				if (Main.netMode != 1 && TalkTimer == 910f)
				{
					CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonExoPhase6Text2", TextColor);
					base.NPC.netUpdate = true;
				}
				if (TalkTimer == 1040f)
				{
					SoundEngine.PlaySound(in LaughSound, PlayerToFollow.Center);
					if (Main.netMode != 1)
					{
						CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonExoPhase6Text3", TextColor);
						base.NPC.netUpdate = true;
					}
				}
				break;
			}
		}
		if (TalkTimer > 690f && !ExoMechIsPresent)
		{
			HandleDefeatStuff();
			DefeatTimer++;
		}
		TalkTimer++;
		if (!ExoMechIsPresent || !Main.zenithWorld || GeneralTimer % 60f != 0f || exoMechdusa)
		{
			return;
		}
		SoundEngine.PlaySound(in SoundID.Item33, base.NPC.Center);
		if (Main.netMode != 1)
		{
			Vector2 shoot = PlayerToFollow.Center - base.NPC.Center;
			((Vector2)(ref shoot)).Normalize();
			shoot *= 4f;
			int p = Projectile.NewProjectile(base.NPC.GetSource_FromAI(), base.NPC.Center - Vector2.UnitY * 30f, shoot, ModContent.ProjectileType<DraedonLaser>(), PulseRifleDamage, 0f, Main.myPlayer);
			if (p.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[p].timeLeft *= 2;
			}
		}
	}

	public void FlyAroundInGamerChair()
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_0299: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode != 1 && HoverDestinationOffset == Vector2.Zero)
		{
			float factor = ((Main.zenithWorld && !exoMechdusa) ? 300f : 700f);
			HoverDestinationOffset = -Vector2.UnitY * factor;
			base.NPC.netUpdate = true;
		}
		if (Main.netMode != 1 && GeneralTimer % 480f == 479f)
		{
			Vector2 directionToTarget = base.NPC.SafeDirectionTo(PlayerToFollow.Center);
			Vector2 offsetDirection;
			do
			{
				offsetDirection = Main.rand.NextVector2Unit();
			}
			while (Vector2.Dot(directionToTarget, offsetDirection) > 0.2f);
			float factormin = ((Main.zenithWorld && !exoMechdusa) ? 300f : 750f);
			float factormax = ((Main.zenithWorld && !exoMechdusa) ? 700f : 1100f);
			HoverDestinationOffset = offsetDirection * Main.rand.NextFloat(factormin, factormax);
			base.NPC.netUpdate = true;
		}
		if (DefeatTimer > 5f)
		{
			HoverDestinationOffset = Vector2.UnitX * (float)(PlayerToFollow.Center.X < base.NPC.Center.X).ToDirectionInt() * 325f;
		}
		Vector2 hoverDestination = PlayerToFollow.Center + HoverDestinationOffset;
		if (base.NPC.WithinRange(hoverDestination, 300f))
		{
			NPC nPC = base.NPC;
			nPC.velocity *= 0.96f;
			float moveSpeed = MathHelper.Lerp(2f, 8f, Utils.GetLerpValue(45f, 275f, base.NPC.Distance(hoverDestination), clamped: true));
			base.NPC.Center = base.NPC.Center.MoveTowards(hoverDestination, moveSpeed);
			return;
		}
		if (DefeatTimer < 30f)
		{
			base.NPC.spriteDirection = (base.NPC.velocity.X < 0f).ToDirectionInt();
		}
		float flySpeed = ((DefeatTimer > 5f) ? 14f : 32f);
		Vector2 idealVelocity = base.NPC.SafeDirectionTo(hoverDestination) * flySpeed;
		base.NPC.SimpleFlyMovement(idealVelocity, flySpeed / 400f);
		base.NPC.velocity = Vector2.Lerp(base.NPC.velocity, idealVelocity, 0.045f);
	}

	public void SummonExoMech()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		if (exoMechdusa)
		{
			CalamityUtils.SpawnBossBetter(PlayerToFollow.Center - Vector2.UnitY * 1400f, ModContent.NPCType<AresBody>()).ModNPC<AresBody>().exoMechdusa = true;
			return;
		}
		switch (CalamityWorld.DraedonMechToSummon)
		{
		case ExoMech.Destroyer:
		{
			NPC thanatos = CalamityUtils.SpawnBossBetter(PlayerToFollow.Center + Vector2.UnitY * 2100f, ModContent.NPCType<ThanatosHead>());
			if (thanatos != null)
			{
				thanatos.velocity = thanatos.SafeDirectionTo(PlayerToFollow.Center) * 40f;
			}
			break;
		}
		case ExoMech.Prime:
			CalamityUtils.SpawnBossBetter(PlayerToFollow.Center - Vector2.UnitY * 1400f, ModContent.NPCType<AresBody>());
			break;
		case ExoMech.Twins:
		{
			Vector2 relativeSpawnPosition = PlayerToFollow.Center + new Vector2(-1100f, -1600f);
			Vector2 apolloSpawnPosition = PlayerToFollow.Center + new Vector2(1100f, -1600f);
			CalamityUtils.SpawnBossBetter(relativeSpawnPosition, ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Artemis.Artemis>());
			CalamityUtils.SpawnBossBetter(apolloSpawnPosition, ModContent.NPCType<global::CalamityMod.NPCs.ExoMechs.Apollo.Apollo>());
			break;
		}
		}
	}

	public void HandleDefeatStuff()
	{
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01be: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_035f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		base.NPC.dontTakeDamage = DefeatTimer < 350f || HasBeenKilled;
		base.NPC.Calamity().CanHaveBossHealthBar = !base.NPC.dontTakeDamage;
		base.NPC.Calamity().ShouldCloseHPBar = HasBeenKilled;
		base.NPC.chaseable = BossRushEvent.BossRushActive;
		if ((DefeatTimer > 1430f || BossRushEvent.BossRushActive) && !exoMechdusa)
		{
			ProjectorOffset -= 9f;
			float disFactor = (HasBeenKilled ? 0.4f : 1f);
			HologramEffectTimer = MathHelper.Clamp(HologramEffectTimer - disFactor, 0f, 45f);
			if (HologramEffectTimer <= 0f)
			{
				Main.BestiaryTracker.Kills.RegisterKill(base.NPC);
				base.NPC.life = 0;
				base.NPC.HitEffect();
				base.NPC.active = false;
				base.NPC.netUpdate = true;
				if (BossRushEvent.BossRushActive)
				{
					base.NPC.NPCLoot();
				}
			}
		}
		else if (HasBeenKilled)
		{
			if (KillReappearDelay <= 0f)
			{
				Lighting.AddLight(base.NPC.Center, 0.5f, 1.25f, 1.25f);
				if (ProjFrameChangeCounter == 0f)
				{
					Main.dust[Dust.NewDust(new Vector2(base.NPC.Center.X - 45f, base.NPC.Center.Y - 70f), base.NPC.width, (int)((float)base.NPC.height * 1.5f), 229, 0f, Main.rand.Next(-2, -1), 60)].noGravity = true;
				}
			}
			HologramEffectTimer = MathHelper.Clamp(HologramEffectTimer + 1f, 0f, 40f);
		}
		if (!HasBeenKilled)
		{
			base.NPC.Opacity = HologramEffectTimer / 45f;
		}
		if (DefeatTimer > 30f && DefeatTimer < 350f)
		{
			ShouldStartStandingUp = true;
		}
		if (exoMechdusa && Main.netMode != 1)
		{
			if (DefeatTimer == 80f)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonMechdusaEndText1", TextColor);
			}
			if (DefeatTimer == 230f)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonMechdusaEndText2", TextColor);
			}
		}
		else if (Main.netMode != 1)
		{
			if (DefeatTimer == 80f)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonEndText1", TextColor);
			}
			if (DefeatTimer == 230f)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonEndText2", TextColor);
			}
			if (DefeatTimer == 380f)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonEndText3", TextColor);
			}
			if (DefeatTimer == 645f)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonEndText4", TextColor);
			}
			if (DefeatTimer == 795f)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonEndText5", TextColor);
			}
			if (DefeatTimer == 945f)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonEndText6", TextColor);
			}
			if (DefeatTimer == 1095f)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonEndText7", TextColor);
			}
			if (DefeatTimer == 1245f)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonEndText8", TextColor);
			}
			if (DefeatTimer == 1395f)
			{
				CalamityUtils.BroadcastLocalizedText("Mods.CalamityMod.Status.Boss.DraedonEndText9", TextColor);
			}
		}
	}

	public override bool CheckActive()
	{
		return false;
	}

	public override Color? GetAlpha(Color drawColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		float teleportFade = Utils.GetLerpValue(0f, 45f, HologramEffectTimer, clamped: true);
		Color color = Color.Lerp(drawColor, Color.Cyan, 1f - (float)Math.Pow(teleportFade, 5.0));
		((Color)(ref color)).A = (byte)(int)(teleportFade * 255f);
		return color * base.NPC.Opacity;
	}

	public override void FindFrame(int frameHeight)
	{
		base.NPC.frame.Width = 100;
		int num = base.NPC.frame.X / base.NPC.frame.Width;
		int yFrame = base.NPC.frame.Y / frameHeight;
		int frame = num * Main.npcFrameCount[base.Type] + yFrame;
		if (ShouldStartStandingUp && frame > 23)
		{
			frame = 0;
		}
		int frameChangeDelay = 7;
		bool shouldNotSitDown = (DefeatTimer > 30f && DefeatTimer < 310f) || (exoMechdusa && DefeatTimer > 0f);
		base.NPC.frameCounter++;
		if (base.NPC.frameCounter >= (double)frameChangeDelay)
		{
			frame++;
			if (!ShouldStartStandingUp && (frame < 23 || frame > 47))
			{
				frame = 23;
			}
			if (shouldNotSitDown && frame >= 16)
			{
				frame = 11;
			}
			if (frame >= 23 && ShouldStartStandingUp)
			{
				frame = 0;
				ShouldStartStandingUp = false;
			}
			base.NPC.frameCounter = 0.0;
		}
		base.NPC.frame.X = frame / Main.npcFrameCount[base.Type] * base.NPC.frame.Width;
		base.NPC.frame.Y = frame % Main.npcFrameCount[base.Type] * frameHeight;
		ProjFrameChangeCounter++;
		if (ProjFrameChangeCounter >= 6f)
		{
			ProjFrameCounter++;
			ProjFrameChangeCounter = 0f;
		}
		if (ProjFrameCounter > 3f)
		{
			ProjFrameCounter = 0f;
		}
	}

	public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)
	{
		base.NPC.lifeMax = 16000;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life <= 0 && !Main.dedServ && !HasBeenKilled && HologramEffectTimer > 0f)
		{
			for (int i = 1; i <= 4; i++)
			{
				Vector2 goreSpawnOffset = Main.rand.NextVector2Circular(12f, 12f);
				Vector2 draedonPieceVelocity = Main.rand.NextVector2CircularEdge(7f, 7f) - Vector2.UnitY * 8f;
				Vector2 chairPieceVelocity = Vector2.UnitY.RotatedByRandom(0.12999999523162842) * Main.rand.NextFloat(4.45f, 5.4f);
				Gore.NewGoreDirect(base.NPC.GetSource_Death(), base.NPC.Center + goreSpawnOffset, draedonPieceVelocity, base.Mod.Find<ModGore>($"Draedon{i}").Type);
				goreSpawnOffset = Main.rand.NextVector2Circular(18f, 18f);
				Gore.NewGoreDirect(base.NPC.GetSource_Death(), base.NPC.Center + goreSpawnOffset, chairPieceVelocity, base.Mod.Find<ModGore>($"Chair{i}").Type);
			}
		}
	}

	public override bool CheckDead()
	{
		if (BossRushEvent.BossRushActive || exoMechdusa)
		{
			return true;
		}
		if (!HasBeenKilled)
		{
			HologramEffectTimer = 0f;
			KillReappearDelay = 160f;
			base.NPC.dontTakeDamage = true;
			HasBeenKilled = true;
			base.NPC.life = base.NPC.lifeMax;
			base.NPC.active = true;
			base.NPC.netUpdate = true;
		}
		return false;
	}

	public override void ModifyIncomingHit(ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= 56f;
		modifiers.SourceDamage.Flat += (float)base.NPC.lifeMax + Main.rand.NextFloat(50f, 750f);
		modifiers.SetCrit();
	}

	public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
	{
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_038b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0393: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_041e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0425: Unknown result type (might be due to invalid IL or missing references)
		//IL_0435: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0864: Unknown result type (might be due to invalid IL or missing references)
		//IL_0866: Unknown result type (might be due to invalid IL or missing references)
		//IL_086d: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_08df: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_08f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0905: Unknown result type (might be due to invalid IL or missing references)
		//IL_0711: Unknown result type (might be due to invalid IL or missing references)
		//IL_0716: Unknown result type (might be due to invalid IL or missing references)
		//IL_071a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		//IL_073a: Unknown result type (might be due to invalid IL or missing references)
		//IL_073e: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_07d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0838: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_0504: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0513: Unknown result type (might be due to invalid IL or missing references)
		//IL_051f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_0561: Unknown result type (might be due to invalid IL or missing references)
		//IL_0568: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0580: Unknown result type (might be due to invalid IL or missing references)
		//IL_058d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05db: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		if (!base.NPC.IsABestiaryIconDummy)
		{
			spriteBatch.EnterShaderRegion();
		}
		bool holo = HasBeenKilled && KillReappearDelay <= 0f;
		bool leaving = HasBeenKilled && DefeatTimer > 1430f;
		Texture2D texture = ((HasBeenKilled && KillReappearDelay <= 0f) ? HoloTexture.Value : TextureAssets.Npc[base.Type].Value);
		Texture2D glowmask = Texture_Glow.Value;
		Texture2D projector = ProjectorTexture.Value;
		Texture2D projectorglow = ProjectorTexture_Glow.Value;
		Texture2D gun = TextureAssets.Item[ModContent.ItemType<PulseRifle>()].Value;
		Rectangle frame = base.NPC.frame;
		Vector2 drawPosition = base.NPC.Center - screenPos - Vector2.UnitY * 38f;
		Vector2 gunDrawPosition = base.NPC.Center - screenPos - Vector2.UnitY * 56f - Vector2.UnitX * 30f * (float)base.NPC.spriteDirection;
		Vector2 origin = frame.Size() * 0.5f;
		Vector2 projorigin = new Vector2(projector.Size().X, projector.Size().Y / 4f) * 0.5f;
		Vector2 gunorigin = new Vector2(gun.Size().X, gun.Size().Y / 4f) * 0.5f;
		Color color = base.NPC.GetAlpha(drawColor);
		Color holoColor = default(Color);
		((Color)(ref holoColor))._002Ector((int)((Color)(ref color)).R, (int)((Color)(ref color)).B, (int)((Color)(ref color)).G, 0);
		SpriteEffects direction = (SpriteEffects)(base.NPC.spriteDirection != 1);
		SpriteEffects gunDirection = (SpriteEffects)(base.NPC.spriteDirection == 1);
		float hoveroffset = 0f;
		if (HasBeenKilled)
		{
			hoveroffset = (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2.5f) * 5f;
		}
		Vector2 playerToDrae = PlayerToFollow.Center - base.NPC.Center;
		((Vector2)(ref playerToDrae)).Normalize();
		float extraRotation = ((PlayerToFollow.Center.X - base.NPC.Center.X < 0f) ? (-(float)Math.PI) : 0f);
		float gunRotation = playerToDrae.ToRotation() + extraRotation;
		if (!base.NPC.IsABestiaryIconDummy)
		{
			GameShaders.Misc["CalamityMod:TeleportDisplacement"].UseOpacity(MathHelper.Clamp(1f - HologramEffectTimer / 45f, 0f, 1f) * 0.38f);
			GameShaders.Misc["CalamityMod:TeleportDisplacement"].UseSecondaryColor(color);
			GameShaders.Misc["CalamityMod:TeleportDisplacement"].UseSaturation((float)(int)((Color)(ref color)).A / 255f);
			GameShaders.Misc["CalamityMod:TeleportDisplacement"].Shader.Parameters["frameCount"].SetValue(new Vector2(16f, (float)Main.npcFrameCount[base.Type]));
			GameShaders.Misc["CalamityMod:TeleportDisplacement"].Apply();
		}
		if (!leaving)
		{
			spriteBatch.Draw(texture, new Vector2(drawPosition.X, drawPosition.Y + hoveroffset), holo ? ((Rectangle?)null) : new Rectangle?(frame), holo ? holoColor : (drawColor * base.NPC.Opacity), base.NPC.rotation, origin, base.NPC.scale, direction, 0f);
		}
		if (!base.NPC.IsABestiaryIconDummy)
		{
			spriteBatch.ExitShaderRegion();
		}
		if (HologramEffectTimer >= 45f || base.NPC.IsABestiaryIconDummy)
		{
			spriteBatch.Draw(glowmask, drawPosition, (Rectangle?)frame, Color.White * base.NPC.Opacity, base.NPC.rotation, origin, base.NPC.scale, direction, 0f);
		}
		if (Main.zenithWorld && !HasBeenKilled && HologramEffectTimer >= 45f && !exoMechdusa)
		{
			spriteBatch.EnterShaderRegion();
			Color outlineColor = Color.Lerp(Color.Magenta, Color.White, 0.4f);
			Vector3 outlineHSL = Main.rgbToHsl(outlineColor);
			float outlineThickness = MathHelper.Clamp(2f, 0f, 3f);
			GameShaders.Misc["CalamityMod:BasicTint"].UseOpacity(1f);
			GameShaders.Misc["CalamityMod:BasicTint"].UseColor(Main.hslToRgb(1f - outlineHSL.X, outlineHSL.Y, outlineHSL.Z));
			GameShaders.Misc["CalamityMod:BasicTint"].Apply();
			for (float i = 0f; i < 1f; i += 0.125f)
			{
				spriteBatch.Draw(gun, gunDrawPosition + (i * ((float)Math.PI * 2f) + gunRotation).ToRotationVector2() * outlineThickness, (Rectangle?)null, outlineColor, gunRotation, gunorigin, base.NPC.scale, gunDirection, 0f);
			}
			spriteBatch.ExitShaderRegion();
			spriteBatch.Draw(gun, gunDrawPosition, (Rectangle?)null, Color.White * base.NPC.Opacity, gunRotation, gunorigin, base.NPC.scale, gunDirection, 0f);
		}
		if (HasBeenKilled)
		{
			int beamoffset = 6;
			int projHeight = (int)ProjFrameCounter * (projector.Height / 4);
			Rectangle projRectangle = default(Rectangle);
			((Rectangle)(ref projRectangle))._002Ector(0, projHeight, projector.Width, projector.Height / 4);
			drawPosition.Y += ProjectorOffset - (float)beamoffset;
			Color val;
			if (KillReappearDelay <= 0f && !leaving)
			{
				Effect effect = Terraria.Graphics.Effects.Filters.Scene["CalamityMod:SpreadTelegraph"].GetShader().Shader;
				effect.Parameters["centerOpacity"].SetValue(0.7f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2f) * 0.05f);
				effect.Parameters["mainOpacity"].SetValue(1f + (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2f) * 0.05f);
				effect.Parameters["halfSpreadAngle"].SetValue((float)Math.PI / 4f);
				EffectParameter obj = effect.Parameters["edgeColor"];
				val = Color.DarkCyan;
				obj.SetValue(((Color)(ref val)).ToVector3());
				EffectParameter obj2 = effect.Parameters["centerColor"];
				val = Color.Cyan;
				obj2.SetValue(((Color)(ref val)).ToVector3());
				effect.Parameters["edgeBlendLength"].SetValue(0.07f);
				effect.Parameters["edgeBlendStrength"].SetValue(8f);
				Main.spriteBatch.End();
				Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.Additive, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, effect, Main.GameViewMatrix.TransformationMatrix);
				Texture2D invis = ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj", (AssetRequestMode)2).Value;
				Main.EntitySpriteDraw(invis, drawPosition, null, Color.White, -(float)Math.PI / 2f, new Vector2((float)invis.Width / 2f, (float)invis.Height / 2f), 500f, (SpriteEffects)0);
				Main.spriteBatch.End();
				Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
			}
			drawPosition.Y += beamoffset;
			if (KillReappearDelay <= 60f)
			{
				Vector2 val2 = drawPosition;
				Rectangle? val3 = projRectangle;
				Color white = Color.White;
				val = Lighting.GetColor((int)base.NPC.position.X / 16, (int)(base.NPC.position.Y / 16f + ProjectorOffset));
				spriteBatch.Draw(projector, val2, val3, white * (float)(int)((Color)(ref val)).A, base.NPC.rotation, projorigin, base.NPC.scale, direction, 0f);
				spriteBatch.Draw(projectorglow, drawPosition, (Rectangle?)projRectangle, Color.White, base.NPC.rotation, projorigin, base.NPC.scale, direction, 0f);
			}
		}
		return false;
	}

	static Draedon()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		TextColor = new Color(155, 255, 255);
		TextColorEdgy = new Color(213, 4, 11);
		LaughSound = new SoundStyle("CalamityMod/Sounds/Custom/DraedonLaugh");
		TeleportSound = new SoundStyle("CalamityMod/Sounds/Custom/DraedonTeleport");
		SelectionSound = new SoundStyle("CalamityMod/Sounds/Custom/Codebreaker/ExoMechsIconSelect");
		PulseRifleDamage = 100;
	}
}
