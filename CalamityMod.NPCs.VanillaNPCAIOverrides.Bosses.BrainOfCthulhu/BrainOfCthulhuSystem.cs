using System;
using System.Collections.Generic;
using CalamityMod.DataStructures;
using CalamityMod.Particles;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.VanillaNPCAIOverrides.Bosses.BrainOfCthulhu;

public class BrainOfCthulhuSystem : ModSystem
{
	private static bool _vanillaBoCTexture = true;

	internal static Asset<Texture2D> tendril;

	private static Texture2D tendrilGlow = null;

	private static Texture2D brainGlow = null;

	private static Texture2D creeperGlow = null;

	internal static float ScreenBlurStrength = 0f;

	internal static (int creeper, List<VerletSimulatedSegment> tendril, int reelInTimer)[] VerletTendrils;

	private static int previousMusic = -1;

	public static bool IsBrainOfCthulhuTextureVanilla => _vanillaBoCTexture;

	public static int PreviousMusic => previousMusic;

	public override void OnModLoad()
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Expected O, but got Unknown
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Expected O, but got Unknown
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Expected O, but got Unknown
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Expected O, but got Unknown
		if (!Main.dedServ)
		{
			tendril = ModContent.Request<Texture2D>("Terraria/Images/Chain12", (AssetRequestMode)2);
		}
		On_NPC.SpawnBoss += new hook_SpawnBoss(SpawnBrainNoMessage);
		On_Player.ItemCheck_UseBossSpawners += new hook_ItemCheck_UseBossSpawners(BlockRoar);
		On_Main.UpdateAudio_DecideOnNewMusic += new hook_UpdateAudio_DecideOnNewMusic(StopBoss3FromStarting);
		On_Main.UpdateAudio_DecideOnTOWMusic += new hook_UpdateAudio_DecideOnTOWMusic(StopOWBoss1FromStarting);
	}

	private void StopBoss3FromStarting(orig_UpdateAudio_DecideOnNewMusic orig, Main self)
	{
		orig.Invoke(self);
		if (NPC.crimsonBoss == -1 || !CalamityWorld.revenge || !Main.npc[NPC.crimsonBoss].active || !Main.npc[NPC.crimsonBoss].TryGetAIOverride<BrainOfCthulhuAI>(out var brainAI) || previousMusic < 0 || previousMusic >= Main.musicFade.Length)
		{
			return;
		}
		if ((int)brainAI.AIState < 0 && brainAI.Time - Math.Abs(brainAI.SpawnTime) < 420f)
		{
			if (Main.newMusic == 13)
			{
				Main.newMusic = previousMusic;
			}
			if (Main.curMusic == 13)
			{
				Main.curMusic = previousMusic;
			}
		}
		if (Main.newMusic == 5)
		{
			Main.newMusic = previousMusic;
		}
		if (Main.curMusic == 5)
		{
			Main.curMusic = previousMusic;
		}
	}

	private void StopOWBoss1FromStarting(orig_UpdateAudio_DecideOnTOWMusic orig, Main self)
	{
		orig.Invoke(self);
		if (NPC.crimsonBoss != -1 && CalamityWorld.revenge && Main.npc[NPC.crimsonBoss].TryGetAIOverride<BrainOfCthulhuAI>(out var brainAI) && previousMusic >= 0 && previousMusic < Main.musicFade.Length && (int)brainAI.AIState < 0 && brainAI.Time - Math.Abs(brainAI.SpawnTime) < 420f)
		{
			if (Main.newMusic == 81)
			{
				Main.newMusic = previousMusic;
			}
			if (Main.curMusic == 81)
			{
				Main.curMusic = previousMusic;
			}
		}
	}

	private void BlockRoar(orig_ItemCheck_UseBossSpawners orig, Player self, int onWhichPlayer, Item sItem)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		if (sItem.type != 1331 || !CalamityWorld.revenge || !self.ItemTimeIsZero || self.itemAnimation <= 0)
		{
			orig.Invoke(self, onWhichPlayer, sItem);
			return;
		}
		SoundEngine.PlaySound(in SoundID.NPCDeath1, Main.LocalPlayer.Center);
		if (self.ZoneCrimson)
		{
			self.ApplyItemTime(sItem);
			CalamityUtils.SpawnBossUsingItem(self, 266, (SoundStyle?)null);
		}
	}

	private void SpawnBrainNoMessage(orig_SpawnBoss orig, int spawnPositionX, int spawnPositionY, int Type, int targetPlayerIndex)
	{
		if (Type != 266 || !CalamityWorld.revenge)
		{
			orig.Invoke(spawnPositionX, spawnPositionY, Type, targetPlayerIndex);
			return;
		}
		int num = NPC.NewNPC(NPC.GetBossSpawnSource(targetPlayerIndex), spawnPositionX, spawnPositionY, Type, 1);
		if (num != 200 && num != -1)
		{
			if (Main.player[targetPlayerIndex].HeldItem.type == 1331)
			{
				BrainOfCthulhuAI.SummonedViaItem = true;
			}
			NPC.crimsonBoss = num;
			Main.npc[num].target = targetPlayerIndex;
			Main.npc[num].timeLeft *= 20;
			previousMusic = Main.curMusic;
			if (Main.netMode == 2 && num < 200)
			{
				NetMessage.SendData(23, -1, -1, null, num);
			}
		}
	}

	internal static Texture2D GetTendrilGlow()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Expected O, but got Unknown
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		if (tendrilGlow == null)
		{
			Texture2D tex = new Texture2D(Main.graphics.GraphicsDevice, tendril.Value.Width, tendril.Value.Height);
			Color[] BaseArray = (Color[])(object)new Color[tex.Width * tex.Height];
			Color[] ColorArray = (Color[])(object)new Color[tex.Width * tex.Height];
			tendril.Value.GetData<Color>(BaseArray);
			for (int i = 0; i < BaseArray.Length; i++)
			{
				ColorArray[i] = new Color(255, 255, 255) * ((float)(int)((Color)(ref BaseArray[i])).A / 255f);
			}
			tex.SetData<Color>(ColorArray);
			tendrilGlow = tex;
		}
		return tendrilGlow;
	}

	internal static Texture2D GetBrainGlow()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		if (brainGlow == null)
		{
			Texture2D tex = new Texture2D(Main.graphics.GraphicsDevice, TextureAssets.Npc[266].Value.Width, TextureAssets.Npc[266].Value.Height);
			Color[] BaseArray = (Color[])(object)new Color[tex.Width * tex.Height];
			Color[] ColorArray = (Color[])(object)new Color[tex.Width * tex.Height];
			TextureAssets.Npc[266].Value.GetData<Color>(BaseArray);
			for (int i = 0; i < BaseArray.Length; i++)
			{
				ColorArray[i] = new Color(255, 255, 255) * ((float)(int)((Color)(ref BaseArray[i])).A / 255f);
			}
			tex.SetData<Color>(ColorArray);
			brainGlow = tex;
		}
		return brainGlow;
	}

	internal static Texture2D GetCreeperGlow()
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		if (creeperGlow == null)
		{
			Texture2D tex = new Texture2D(Main.graphics.GraphicsDevice, TextureAssets.Npc[267].Value.Width, TextureAssets.Npc[267].Value.Height);
			Color[] BaseArray = (Color[])(object)new Color[tex.Width * tex.Height];
			Color[] ColorArray = (Color[])(object)new Color[tex.Width * tex.Height];
			TextureAssets.Npc[267].Value.GetData<Color>(BaseArray);
			for (int i = 0; i < BaseArray.Length; i++)
			{
				ColorArray[i] = new Color(255, 255, 255) * ((float)(int)((Color)(ref BaseArray[i])).A / 255f);
			}
			tex.SetData<Color>(ColorArray);
			creeperGlow = tex;
		}
		return creeperGlow;
	}

	public override void OnWorldLoad()
	{
		if (Main.dedServ)
		{
			return;
		}
		Main.QueueMainThreadAction(delegate
		{
			//IL_0070: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			Texture2D value = ModContent.Request<Texture2D>("CalamityMod/NPCs/VanillaNPCAIOverrides/Bosses/BrainOfCthulhu/ReferenceBrainOfCthulhu", (AssetRequestMode)1).Value;
			Main.instance.LoadNPC(266);
			Texture2D value2 = TextureAssets.Npc[266].Value;
			Color[] array = (Color[])(object)new Color[value.Width * value.Height];
			value.GetData<Color>(array);
			Color[] array2 = (Color[])(object)new Color[value2.Width * value2.Height];
			value2.GetData<Color>(array2);
			bool vanillaBoCTexture = true;
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i] != array2[i])
				{
					vanillaBoCTexture = false;
					break;
				}
			}
			_vanillaBoCTexture = vanillaBoCTexture;
		});
	}

	public override void PostUpdateNPCs()
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_046e: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_021e: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_026d: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_028f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		if (VerletTendrils == null)
		{
			return;
		}
		if (!NPC.AnyNPCs(266))
		{
			BrainOfCthulhuAI.SummonedViaItem = false;
		}
		if (Main.netMode == 2 || NPC.crimsonBoss == -1 || !CalamityWorld.revenge || !(Main.npc[NPC.crimsonBoss].ai[0] <= 10f))
		{
			return;
		}
		bool shouldSpawnTendrilIfNeeded = Main.npc[NPC.crimsonBoss].ai[0] == 8f;
		int index = 0;
		(int, List<VerletSimulatedSegment>, int)[] verletTendrils = VerletTendrils;
		for (int i = 0; i < verletTendrils.Length; i++)
		{
			(int, List<VerletSimulatedSegment>, int) member = verletTendrils[i];
			NPC creeper = Main.npc[member.Item1];
			Vector2 startPoint = Main.npc[NPC.crimsonBoss].Center + Main.npc[NPC.crimsonBoss].netOffset + Vector2.UnitY * 32f;
			float creeperRatio = (float)index / (float)BrainOfCthulhuAI.GetBrainOfCthuluCreepersCountRevDeath();
			startPoint = ((index % 2 != 0) ? (startPoint + new Vector2(MathHelper.Lerp(24f, 0f, creeperRatio), 0f)) : (startPoint + new Vector2(MathHelper.Lerp(-24f, 0f, creeperRatio), 0f)));
			List<VerletSimulatedSegment> vTendril = VerletTendrils[index].tendril;
			if (!creeper.active || creeper.type != 267)
			{
				float reelInTime = 180f;
				VerletTendrils[index].reelInTimer++;
				float reelRatio = CalamityUtils.CircInEasing((float)VerletTendrils[index].reelInTimer / reelInTime, 1);
				float reelInSegementedRatio = reelRatio * 28f;
				float segmentRatio = MathF.Truncate(reelInSegementedRatio);
				if (reelRatio >= 1f || VerletTendrils[index].tendril.Count <= 1)
				{
					VerletTendrils[index].tendril.Clear();
					index++;
					continue;
				}
				for (int j = 0; j < 28; j++)
				{
					VerletSimulatedSegment seg = vTendril[j];
					if ((float)j <= reelInSegementedRatio)
					{
						seg.position = startPoint;
						seg.oldPosition = startPoint;
						seg.locked = true;
						continue;
					}
					seg.position += Main.npc[NPC.crimsonBoss].velocity;
					seg.oldPosition += Main.npc[NPC.crimsonBoss].velocity;
					if ((float)j == MathF.Ceiling(reelRatio))
					{
						seg.position = startPoint - Vector2.unitYVector * MathHelper.Lerp(0f, 16f, segmentRatio);
					}
					seg.locked = false;
				}
				if (reelRatio < 0.75f && Main.rand.NextBool(3))
				{
					Vector2 dir = (vTendril[vTendril.Count - 1].position - vTendril[vTendril.Count - 2].position).SafeNormalize(Vector2.unitYVector);
					GeneralParticleHandler.SpawnParticle(new BloodParticle(vTendril[vTendril.Count - 2].position, dir.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 10f, (float)Math.PI / 10f)) * Main.rand.NextFloat(4f, 8f), 16, Main.rand.NextFloat(0.5f, 0.75f), Color.Red * 0.75f));
				}
				VerletSimulatedSegment.SimpleSimulation(vTendril, 16f, 10, 1.5f);
				index++;
				continue;
			}
			VerletTendrils[index].reelInTimer = -1;
			if ((vTendril.Count < 28) & shouldSpawnTendrilIfNeeded)
			{
				VerletTendrils[index] = (creeper: creeper.whoAmI, tendril: new List<VerletSimulatedSegment>(), reelInTimer: -1);
				for (int k = 0; k < 28; k++)
				{
					VerletTendrils[index].tendril.Add(new VerletSimulatedSegment(creeper.Center));
				}
			}
			Vector2 endPoint = creeper.Center + creeper.netOffset;
			index++;
			if (vTendril != null && vTendril.Count != 0)
			{
				vTendril[0].position = startPoint;
				vTendril[0].locked = true;
				vTendril[vTendril.Count - 1].position = endPoint;
				vTendril[vTendril.Count - 1].locked = true;
				VerletSimulatedSegment.SimpleSimulation(vTendril, 16f, 10, 3f);
			}
		}
	}

	public override void PostDrawTiles()
	{
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0282: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_049d: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode == 2)
		{
			return;
		}
		if (NPC.crimsonBoss == -1 || !CalamityWorld.revenge || Main.npc[NPC.crimsonBoss].ai[0] >= 9f || !Main.npc[NPC.crimsonBoss].TryGetAIOverride<BrainOfCthulhuAI>(out var ai))
		{
			Filters.Scene["CalamityMod:BrainOfCthulhuForcefield"].GetShader().UseOpacity(0f);
			if (Filters.Scene["CalamityMod:BrainOfCthulhuForcefield"].IsActive())
			{
				Filters.Scene.Deactivate("CalamityMod:BrainOfCthulhuForcefield");
			}
		}
		else
		{
			if (!Filters.Scene["CalamityMod:BrainOfCthulhuForcefield"].IsActive())
			{
				Filters.Scene.Activate("CalamityMod:BrainOfCthulhuForcefield", default(Vector2));
			}
			NPC target = Main.npc[NPC.crimsonBoss];
			Vector2 targetPos = target.Center + target.netOffset;
			float shieldOpacity = ai.ShieldOpacity;
			float shieldScale = ai.ShieldScale;
			targetPos = Vector2.Transform(targetPos - Main.screenPosition, Main.GameViewMatrix.ZoomMatrix) / Main.ScreenSize.ToVector2();
			Texture2D voronoi = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/VoronoiShapes3", (AssetRequestMode)2).Value;
			Texture2D depthNoise = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Veins", (AssetRequestMode)2).Value;
			Filters.Scene["CalamityMod:BrainOfCthulhuForcefield"].GetShader().Shader.Parameters["voronoi"].SetValue((Texture)(object)voronoi);
			Filters.Scene["CalamityMod:BrainOfCthulhuForcefield"].GetShader().Shader.Parameters["depthNoise"].SetValue((Texture)(object)depthNoise);
			EffectParameter obj = Filters.Scene["CalamityMod:BrainOfCthulhuForcefield"].GetShader().Shader.Parameters["uScreenResolution"];
			Viewport viewport = Main.graphics.GraphicsDevice.Viewport;
			float num = ((Viewport)(ref viewport)).Width;
			viewport = Main.graphics.GraphicsDevice.Viewport;
			obj.SetValue(new Vector2(num, (float)((Viewport)(ref viewport)).Height));
			Filters.Scene["CalamityMod:BrainOfCthulhuForcefield"].GetShader().UseProgress(0.15f * shieldScale);
			Filters.Scene["CalamityMod:BrainOfCthulhuForcefield"].GetShader().UseOpacity(shieldOpacity);
			Filters.Scene["CalamityMod:BrainOfCthulhuForcefield"].GetShader().UseColor(Color.Red);
			Filters.Scene["CalamityMod:BrainOfCthulhuForcefield"].GetShader().UseSecondaryColor(new Color(255, 0, 90));
			Filters.Scene["CalamityMod:BrainOfCthulhuForcefield"].GetShader().UseDirection(targetPos);
		}
		if (ScreenBlurStrength == 0f)
		{
			Filters.Scene["CalamityMod:RadialBlurShader"].GetShader().UseIntensity(0f);
			if (Filters.Scene["CalamityMod:RadialBlurShader"].IsActive())
			{
				Filters.Scene.Deactivate("CalamityMod:RadialBlurShader");
			}
		}
		else if (NPC.crimsonBoss == -1)
		{
			ScreenBlurStrength = Filters.Scene["CalamityMod:RadialBlurShader"].GetShader().Intensity * 0.9f;
			Filters.Scene["CalamityMod:RadialBlurShader"].GetShader().UseIntensity(ScreenBlurStrength);
			if (ScreenBlurStrength < 0.01f)
			{
				ScreenBlurStrength = 0f;
			}
		}
		else if (Filters.Scene["CalamityMod:RadialBlurShader"].IsLoaded)
		{
			if (!Filters.Scene["CalamityMod:RadialBlurShader"].IsActive())
			{
				Filters.Scene.Activate("CalamityMod:RadialBlurShader", default(Vector2));
			}
			NPC boss = Main.npc[NPC.crimsonBoss];
			float counter = boss.ai[1] - boss.ai[2] - 240f;
			float distSQ = Main.LocalPlayer.DistanceSQ(boss.Center);
			float distanceScaleFactor = 1f;
			if (distSQ > 592900f)
			{
				distanceScaleFactor = 1f / (1f + ((float)Math.Sqrt(distSQ) - 770f) / 32f);
			}
			Filters.Scene["CalamityMod:RadialBlurShader"].GetShader().UseIntensity((ScreenBlurStrength + ((float)Math.Cos(counter * ((float)Math.PI * 2f) / 15f) / 2f + 0.5f) * (0.4f * ScreenBlurStrength)) * distanceScaleFactor);
			Filters.Scene["CalamityMod:RadialBlurShader"].GetShader().Shader.Parameters["uSaturation"].SetValue(20);
			Vector2 targetPos2 = Vector2.Transform(boss.Center - Main.screenPosition, Main.GameViewMatrix.ZoomMatrix) / Main.ScreenSize.ToVector2();
			Filters.Scene["CalamityMod:RadialBlurShader"].GetShader().UseDirection(targetPos2);
		}
	}

	public override void PostUpdateEverything()
	{
		bool allowBossMusic = false;
		if (NPC.crimsonBoss != -1 && Main.npc[NPC.crimsonBoss].active && Main.npc[NPC.crimsonBoss].TryGetAIOverride<BrainOfCthulhuAI>(out var brainAI) && (int)brainAI.AIState > 1)
		{
			allowBossMusic = true;
		}
		if (((Main.curMusic != 13 && Main.curMusic != 81) | allowBossMusic) && Main.curMusic != 5)
		{
			previousMusic = Main.curMusic;
		}
		if (previousMusic == -1)
		{
			previousMusic = 16;
		}
	}
}
