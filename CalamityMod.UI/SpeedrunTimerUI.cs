using CalamityMod.CalPlayer;
using CalamityMod.NPCs.AquaticScourge;
using CalamityMod.NPCs.AstrumAureus;
using CalamityMod.NPCs.AstrumDeus;
using CalamityMod.NPCs.BrimstoneElemental;
using CalamityMod.NPCs.Bumblebirb;
using CalamityMod.NPCs.CalClone;
using CalamityMod.NPCs.CeaselessVoid;
using CalamityMod.NPCs.Crabulon;
using CalamityMod.NPCs.Cryogen;
using CalamityMod.NPCs.DesertScourge;
using CalamityMod.NPCs.ExoMechs.Ares;
using CalamityMod.NPCs.Leviathan;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.NPCs.Perforator;
using CalamityMod.NPCs.PlaguebringerGoliath;
using CalamityMod.NPCs.PrimordialWyrm;
using CalamityMod.NPCs.ProfanedGuardians;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.Ravager;
using CalamityMod.NPCs.Signus;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.NPCs.Yharon;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.UI;

public class SpeedrunTimerUI
{
	internal const float DefaultTimerPosX = 46f;

	internal const float DefaultTimerPosY = 1.481f;

	private static readonly float SplitHorizontalOffset = 30f;

	private static readonly float SplitVerticalOffset = 44f;

	public static void Draw(Player player)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		if (Main.gameMenu || !CalamityClientConfig.Instance.SpeedrunTimer)
		{
			return;
		}
		Vector2 screenRatioPosition = default(Vector2);
		((Vector2)(ref screenRatioPosition))._002Ector(CalamityClientConfig.Instance.SpeedrunTimerPosX, CalamityClientConfig.Instance.SpeedrunTimerPosY);
		if (screenRatioPosition.X < 0f || screenRatioPosition.X > 100f)
		{
			screenRatioPosition.X = 46f;
		}
		if (screenRatioPosition.Y < 0f || screenRatioPosition.Y > 100f)
		{
			screenRatioPosition.Y = 1.481f;
		}
		Vector2 screenPos = screenRatioPosition;
		screenPos.X = (int)(screenPos.X * 0.01f * (float)Main.screenWidth);
		screenPos.Y = (int)(screenPos.Y * 0.01f * (float)Main.screenHeight);
		CalamityPlayer calamityPlayer = player.Calamity();
		string text = SpeedrunTimerSystem.GetTimerText(calamityPlayer);
		float scale = 2f;
		Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, text, screenPos.X, screenPos.Y, Color.White, Color.Black, default(Vector2), scale);
		if (calamityPlayer.lastSplitType != -1)
		{
			text = SpeedrunTimerSystem.GetSplitText(calamityPlayer);
			scale = 1f;
			float lineTwoX = screenPos.X + SplitHorizontalOffset;
			float lineTwoY = screenPos.Y + SplitVerticalOffset;
			Texture2D texture = GetSplitIcon(calamityPlayer.lastSplitType);
			if (texture != null)
			{
				Main.spriteBatch.Draw(texture, new Vector2(lineTwoX - (float)texture.Width - 4f, lineTwoY), (Rectangle?)null, Color.White, 0f, default(Vector2), 1f, (SpriteEffects)0, 0f);
			}
			Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, text, lineTwoX, lineTwoY, Color.White, Color.Black, default(Vector2), scale);
		}
	}

	private static Texture2D GetSplitIcon(int magicNumber)
	{
		return (Texture2D)(magicNumber switch
		{
			1 => TextureAssets.NpcHeadBoss[7].Value, 
			2 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<DesertScourgeHead>()]].Value, 
			3 => TextureAssets.NpcHeadBoss[1].Value, 
			4 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<Crabulon>()]].Value, 
			5 => TextureAssets.NpcHeadBoss[2].Value, 
			6 => TextureAssets.NpcHeadBoss[23].Value, 
			7 => ModContent.Request<Texture2D>("CalamityMod/NPCs/HiveMind/HiveMindP2_Head_Boss", (AssetRequestMode)2).Value, 
			8 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<PerforatorHive>()]].Value, 
			9 => TextureAssets.NpcHeadBoss[14].Value, 
			10 => TextureAssets.NpcHeadBoss[19].Value, 
			11 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<SlimeGodCore>()]].Value, 
			12 => TextureAssets.NpcHeadBoss[22].Value, 
			13 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<Cryogen>()]].Value, 
			14 => TextureAssets.NpcHeadBoss[21].Value, 
			15 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<AquaticScourgeHead>()]].Value, 
			16 => TextureAssets.NpcHeadBoss[25].Value, 
			17 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<BrimstoneElemental>()]].Value, 
			18 => TextureAssets.NpcHeadBoss[18].Value, 
			19 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<CalamitasClone>()]].Value, 
			20 => TextureAssets.NpcHeadBoss[12].Value, 
			21 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<Leviathan>()]].Value, 
			22 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<AstrumAureus>()]].Value, 
			23 => TextureAssets.NpcHeadBoss[5].Value, 
			24 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<PlaguebringerGoliath>()]].Value, 
			25 => TextureAssets.NpcHeadBoss[4].Value, 
			26 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<RavagerBody>()]].Value, 
			27 => TextureAssets.NpcHeadBoss[31].Value, 
			28 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<AstrumDeusHead>()]].Value, 
			29 => TextureAssets.NpcHeadBoss[8].Value, 
			30 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<ProfanedGuardianCommander>()]].Value, 
			31 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<Dragonfolly>()]].Value, 
			32 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<Providence>()]].Value, 
			33 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<CeaselessVoid>()]].Value, 
			34 => ModContent.Request<Texture2D>("CalamityMod/NPCs/StormWeaver/StormWeaverHeadNaked_Head_Boss", (AssetRequestMode)2).Value, 
			35 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<Signus>()]].Value, 
			36 => ModContent.Request<Texture2D>("CalamityMod/NPCs/Polterghast/Necroplasm_Head_Boss", (AssetRequestMode)2).Value, 
			37 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<OldDuke>()]].Value, 
			38 => ModContent.Request<Texture2D>("CalamityMod/NPCs/DevourerofGods/DevourerofGodsHead_Head_Boss", (AssetRequestMode)2).Value, 
			39 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<Yharon>()]].Value, 
			40 => ModContent.Request<Texture2D>("CalamityMod/NPCs/SupremeCalamitas/HoodlessHeadIcon", (AssetRequestMode)2).Value, 
			41 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<AresBody>()]].Value, 
			42 => TextureAssets.NpcHeadBoss[NPCID.Sets.BossHeadTextures[ModContent.NPCType<PrimordialWyrmHead>()]].Value, 
			43 => TextureAssets.NpcHeadBoss[38].Value, 
			44 => TextureAssets.NpcHeadBoss[37].Value, 
			45 => TextureAssets.NpcHeadBoss[39].Value, 
			_ => null, 
		});
	}
}
