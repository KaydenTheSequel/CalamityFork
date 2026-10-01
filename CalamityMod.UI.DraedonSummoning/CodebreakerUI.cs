using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CalamityMod.CalPlayer;
using CalamityMod.Fonts;
using CalamityMod.Items.DraedonMisc;
using CalamityMod.Items.Pets;
using CalamityMod.NPCs.ExoMechs;
using CalamityMod.Packets;
using CalamityMod.Systems.Collections;
using CalamityMod.TileEntities;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.UI.Chat;

namespace CalamityMod.UI.DraedonSummoning;

public class CodebreakerUI : ModSystem
{
	public class DialogEntry
	{
		public bool FromDraedon;

		public string Dialog;

		public DialogEntry(string dialog, bool fromDraedon)
		{
			Dialog = dialog;
			FromDraedon = fromDraedon;
		}
	}

	public static float DialogOffYCache = 0f;

	public static readonly SoundStyle DialogOptionHoverSound = new SoundStyle("CalamityMod/Sounds/Custom/Codebreaker/DialogOptionHover");

	public static readonly SoundStyle[] DraedonTalks = new SoundStyle[3]
	{
		new SoundStyle("CalamityMod/Sounds/Custom/Codebreaker/DraedonTalk1"),
		new SoundStyle("CalamityMod/Sounds/Custom/Codebreaker/DraedonTalk2"),
		new SoundStyle("CalamityMod/Sounds/Custom/Codebreaker/DraedonTalk3")
	};

	public static readonly SoundStyle SummonSound = new SoundStyle("CalamityMod/Sounds/Custom/CodebreakerBeam");

	public static readonly SoundStyle BloodSound = new SoundStyle("CalamityMod/Sounds/Custom/Codebreaker/BloodForHekate");

	public static float CommunicationPanelScale { get; set; }

	public static int DraedonTextCreationTimer { get; set; }

	public static string WrittenDraedonText { get; set; } = string.Empty;

	public static string FullDraedonText { get; set; } = string.Empty;

	public static int DialogSoundDelay { get; set; }

	public static float DraedonScreenStaticInterpolant { get; set; } = 1f;

	public static float OptionsTextOpacity { get; set; } = 1f;

	public static float DialogVerticalOffset { get; set; }

	public static float OptionsTextVerticalOffset { get; private set; }

	public static float LatestDialogHeightIncrease { get; set; }

	public static float DialogHeight { get; private set; }

	public static float OptionsTextHeight { get; private set; }

	public static Vector2 DialogTextScale
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			return Vector2.One * GeneralScale * 0.75f;
		}
	}

	public static char PreviousTextCharacter
	{
		get
		{
			if (WrittenDraedonText.Length < 1)
			{
				return ' ';
			}
			return FullDraedonText[WrittenDraedonText.Length - 1];
		}
	}

	public static char NextTextCharacter
	{
		get
		{
			if (WrittenDraedonText.Length >= FullDraedonText.Length)
			{
				return ' ';
			}
			return FullDraedonText[WrittenDraedonText.Length];
		}
	}

	public static int DraedonTextCreationRate
	{
		get
		{
			if (PreviousTextCharacter == '\n')
			{
				return 80;
			}
			char previousTextCharacter = PreviousTextCharacter;
			if ((previousTextCharacter == '.' || previousTextCharacter == '?') ? true : false)
			{
				return 9;
			}
			return 1;
		}
	}

	public static string InquiryText
	{
		get
		{
			CalamityPlayer mp = Main.LocalPlayer.Calamity();
			return CalamityUtils.GetTextValue((!mp.HasTalkedAtCodebreaker) ? "UI.CommunicationStartInitial" : (mp.HasCraftedDraedonsForge ? "UI.CommunicationStartNormal" : "UI.CommunicationStartNoForge"));
		}
	}

	public static string HoverSoundDialogType { get; set; } = null;

	public static List<DialogEntry> DialogHistory { get; set; } = new List<DialogEntry>();

	public static CodebreakerUIScroller TopicOptionsScroller { get; internal set; } = new CodebreakerUIScroller();

	public static CodebreakerUIScroller DialogScroller { get; internal set; } = new CodebreakerUIScroller();

	public static int ViewedTileEntityID { get; set; } = -1;

	public static bool AwaitingCloseConfirmation { get; set; } = false;

	public static bool AwaitingDecryptionTextClose { get; set; } = false;

	public static bool DisplayingCommunicationText { get; set; } = false;

	public static float VerificationButtonScale { get; set; } = 1f;

	public static float ExitButtonScale { get; set; } = 1f;

	public static float ContactButtonScale { get; set; } = 1f;

	public static float CommunicateButtonScale { get; set; } = 1f;

	public static float CancelButtonScale { get; set; } = 1f;

	public static float MechIconScale { get; set; } = 1f;

	public static Vector2 BackgroundCenter
	{
		get
		{
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(500f, (float)Main.screenHeight * 0.5f + 115f);
		}
	}

	public static float GeneralScale => MathHelper.Lerp(1f, 0.7f, Utils.GetLerpValue(1325f, 750f, Main.screenWidth, clamped: true)) * Main.UIScale;

	public static Rectangle MouseScreenArea
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0014: Unknown result type (might be due to invalid IL or missing references)
			return Utils.CenteredRectangle(Main.MouseScreen, Vector2.One * 2f);
		}
	}

	public static void DisplayCommunicationPanel()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		float panelWidthScale = Utils.Remap(CommunicationPanelScale, 0f, 0.5f, 0.085f, 1f);
		float panelHeightScale = Utils.Remap(CommunicationPanelScale, 0.5f, 1f, 0.085f, 1f);
		Vector2 panelScale = GeneralScale * new Vector2(panelWidthScale, panelHeightScale) * 1.4f;
		Texture2D panelTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/DraedonContactPanel", (AssetRequestMode)2).Value;
		float basePanelHeight = GeneralScale * (float)panelTexture.Height * 1.4f;
		Vector2 panelCenter = default(Vector2);
		((Vector2)(ref panelCenter))._002Ector((float)Main.screenWidth * 0.5f, (float)Main.screenHeight * 0.5f + (float)panelTexture.Height * panelScale.Y * 0.5f - basePanelHeight * 0.5f);
		Rectangle panelArea = Utils.CenteredRectangle(panelCenter, panelTexture.Size() * panelScale);
		Main.spriteBatch.Draw(panelTexture, panelCenter, (Rectangle?)null, Color.White, 0f, panelTexture.Size() * 0.5f, panelScale, (SpriteEffects)0, 0f);
		if (DraedonScreenStaticInterpolant > 0f)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Matrix.Identity);
			MiscShaderData miscShaderData = GameShaders.Misc["CalamityMod:BlueStatic"];
			miscShaderData.SetShaderTexture(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/SharpNoise", (AssetRequestMode)2));
			miscShaderData.Shader.Parameters["useStaticLine"].SetValue(false);
			miscShaderData.Shader.Parameters["coordinateZoomFactor"].SetValue(0.5f);
			miscShaderData.Shader.Parameters["useTrueNoise"].SetValue(true);
			miscShaderData.Apply();
			float readjustedInterpolant = Utils.GetLerpValue(0.42f, 1f, DraedonScreenStaticInterpolant, clamped: true);
			Color staticColor = Color.White * (float)Math.Pow(CalamityUtils.AperiodicSin(readjustedInterpolant * 2.94f) * 0.5f + 0.5f, 0.54) * (float)Math.Pow(readjustedInterpolant, 0.51);
			Main.spriteBatch.Draw(panelTexture, panelCenter, (Rectangle?)null, staticColor, 0f, panelTexture.Size() * 0.5f, panelScale, (SpriteEffects)0, 0f);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Matrix.Identity);
		}
		if (((Rectangle)(ref panelArea)).Intersects(MouseScreenArea))
		{
			Main.blockMouse = (Main.LocalPlayer.mouseInterface = true);
		}
		DisplayDraedonFacePanel(panelCenter, panelScale);
		DisplayTextSelectionOptions(panelArea, panelScale);
		DisplayDialogHistory(panelArea, panelScale);
		if (OptionsTextOpacity > 0f && DraedonScreenStaticInterpolant <= 0f)
		{
			DrawExitButton(panelCenter + new Vector2(10f, 150f) * GeneralScale, OptionsTextOpacity);
		}
	}

	public static void DisplayDraedonFacePanel(Vector2 panelCenter, Vector2 panelScale)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0275: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		Texture2D iconTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/DraedonIconBorder", (AssetRequestMode)2).Value;
		Texture2D iconTextureInner = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/DraedonIconBorderInner", (AssetRequestMode)2).Value;
		float draedonIconDrawInterpolant = Utils.GetLerpValue(0.51f, 0.36f, DraedonScreenStaticInterpolant, clamped: true);
		Vector2 draedonIconDrawTopRight = panelCenter + new Vector2(-204f, -125f) * panelScale;
		draedonIconDrawTopRight += new Vector2(24f, 4f) * panelScale;
		Vector2 draedonIconScale = panelScale * 0.5f;
		Vector2 draedonIconCenter = draedonIconDrawTopRight + iconTexture.Size() * new Vector2(0.5f, 0.5f) * draedonIconScale;
		Rectangle draedonIconArea = Utils.CenteredRectangle(draedonIconCenter, iconTexture.Size() * draedonIconScale * 0.9f);
		Main.spriteBatch.Draw(iconTexture, draedonIconDrawTopRight, (Rectangle?)null, Color.White * draedonIconDrawInterpolant, 0f, Vector2.Zero, draedonIconScale, (SpriteEffects)0, 0f);
		Main.spriteBatch.EnforceCutoffRegion(draedonIconArea, Matrix.Identity, (SpriteSortMode)1);
		MiscShaderData miscShaderData = GameShaders.Misc["CalamityMod:TeleportDisplacement"];
		miscShaderData.UseOpacity(0.04f);
		miscShaderData.UseSecondaryColor(Color.White * 0.75f);
		miscShaderData.UseSaturation(0.75f);
		miscShaderData.Shader.Parameters["frameCount"].SetValue(Vector2.One);
		miscShaderData.Apply();
		Vector2 draedonScale = new Vector2(draedonIconDrawInterpolant, 1f) * Main.UIScale * 1.32f;
		SpriteEffects draedonDirection = (SpriteEffects)1;
		Texture2D draedonFaceTexture = ModContent.Request<Texture2D>("CalamityMod/NPCs/ExoMechs/HologramDraedon", (AssetRequestMode)2).Value;
		Main.spriteBatch.Draw(draedonFaceTexture, draedonIconCenter, (Rectangle?)null, Color.White * draedonIconDrawInterpolant, 0f, draedonFaceTexture.Size() * 0.5f, draedonScale, draedonDirection, 0f);
		Main.spriteBatch.ReleaseCutoffRegion(Matrix.Identity, (SpriteSortMode)1);
		MiscShaderData miscShaderData2 = GameShaders.Misc["CalamityMod:BlueStatic"];
		miscShaderData2.UseColor(Color.Cyan);
		miscShaderData2.UseImage1("Images/Misc/noise");
		miscShaderData2.Shader.Parameters["useStaticLine"].SetValue(true);
		miscShaderData2.Shader.Parameters["coordinateZoomFactor"].SetValue(1f);
		miscShaderData2.Apply();
		Main.spriteBatch.Draw(iconTextureInner, draedonIconDrawTopRight, (Rectangle?)null, Color.White * draedonIconDrawInterpolant, 0f, Vector2.Zero, draedonIconScale, (SpriteEffects)0, 0f);
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Matrix.Identity);
	}

	public static void DisplayTextSelectionOptions(Rectangle panelArea, Vector2 panelScale)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_08b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0437: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0461: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_0487: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0497: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0552: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0571: Unknown result type (might be due to invalid IL or missing references)
		//IL_0582: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_07df: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0803: Unknown result type (might be due to invalid IL or missing references)
		//IL_0807: Unknown result type (might be due to invalid IL or missing references)
		//IL_0811: Unknown result type (might be due to invalid IL or missing references)
		//IL_0813: Unknown result type (might be due to invalid IL or missing references)
		//IL_0831: Unknown result type (might be due to invalid IL or missing references)
		//IL_0833: Unknown result type (might be due to invalid IL or missing references)
		//IL_0837: Unknown result type (might be due to invalid IL or missing references)
		//IL_0841: Unknown result type (might be due to invalid IL or missing references)
		//IL_0846: Unknown result type (might be due to invalid IL or missing references)
		//IL_0850: Unknown result type (might be due to invalid IL or missing references)
		//IL_085a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0869: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0628: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0732: Unknown result type (might be due to invalid IL or missing references)
		//IL_0737: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0753: Unknown result type (might be due to invalid IL or missing references)
		//IL_076f: Unknown result type (might be due to invalid IL or missing references)
		float selectionOptionsDrawInterpolant = Utils.GetLerpValue(0.3f, 0f, DraedonScreenStaticInterpolant, clamped: true);
		Texture2D selectionOutline = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/DraedonSelectionOutline", (AssetRequestMode)2).Value;
		Vector2 selectionCenter = panelArea.BottomLeft() - new Vector2((float)selectionOutline.Width * -0.5f - 42f, (float)selectionOutline.Height * 0.5f + 13f) * panelScale;
		Rectangle selectionArea = Utils.CenteredRectangle(selectionCenter, selectionOutline.Size() * panelScale);
		Main.spriteBatch.Draw(selectionOutline, selectionCenter, (Rectangle?)null, Color.White * selectionOptionsDrawInterpolant, 0f, selectionOutline.Size() * 0.5f, panelScale, (SpriteEffects)0, 0f);
		bool canChooseQuery = WrittenDraedonText.Length == FullDraedonText.Length;
		OptionsTextOpacity = MathHelper.Clamp(OptionsTextOpacity + (float)canChooseQuery.ToDirectionInt() * 0.1f, 0f, 1f);
		Rectangle textCutoffRegion = selectionArea;
		textCutoffRegion.Y += 6;
		textCutoffRegion.Height -= 10;
		RasterizerState rasterizer = Main.Rasterizer;
		rasterizer.ScissorTestEnable = true;
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, rasterizer, (Effect)null, Matrix.Identity);
		((GraphicsResource)Main.spriteBatch).GraphicsDevice.ScissorRectangle = textCutoffRegion;
		Vector2 textTopLeft = selectionArea.TopLeft() + new Vector2(20f, 12f) * panelScale + Vector2.UnitY * OptionsTextVerticalOffset;
		float bottomPadding = panelScale.Y * 24f;
		float cutoffDistance = OptionsTextHeight - (float)selectionOutline.Height * panelScale.Y + bottomPadding;
		Rectangle mouseScreenArea;
		if (cutoffDistance > 0f && OptionsTextOpacity > 0f)
		{
			mouseScreenArea = MouseScreenArea;
			if (((Rectangle)(ref mouseScreenArea)).Intersects(selectionArea))
			{
				OptionsTextVerticalOffset += (float)PlayerInput.ScrollWheelDeltaForUI * 0.1f;
			}
			TopicOptionsScroller.PositionYInterpolant = MathHelper.Clamp(OptionsTextVerticalOffset / (0f - cutoffDistance), 0f, 1f);
			TopicOptionsScroller.Draw((float)((Rectangle)(ref selectionArea)).Top + GeneralScale * 62f, (float)((Rectangle)(ref selectionArea)).Bottom - GeneralScale * 62f, (float)((Rectangle)(ref selectionArea)).Right - GeneralScale * 12f, GeneralScale * 0.8f, OptionsTextOpacity);
			OptionsTextVerticalOffset = TopicOptionsScroller.PositionYInterpolant * (0f - cutoffDistance);
		}
		OptionsTextHeight = 0f;
		bool hoveringOverAnyOption = false;
		float opacity = OptionsTextOpacity * (1f - DraedonScreenStaticInterpolant);
		Texture2D markerTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/DraedonInquirySelector", (AssetRequestMode)2).Value;
		Vector2 markerScale = panelScale * 0.24f;
		Vector2 markerDrawPositionOffset = Vector2.UnitX * (float)markerTexture.Width * markerScale.X * 0.52f;
		Vector2 markerTextureSize = markerTexture.Size() * markerScale;
		float verticalOffsetPerOption = panelScale.Y * 12f;
		Rectangle textAreaRect = default(Rectangle);
		foreach (DraedonDialogEntry dialog in DraedonDialogRegistry.DialogOptions.Where((DraedonDialogEntry d) => d.Condition()))
		{
			string inquiry = dialog.Inquiry;
			if (!Main.LocalPlayer.Calamity().HasCraftedDraedonsForge)
			{
				if (inquiry != DraedonDialogRegistry.DialogOptions[0].Inquiry)
				{
					continue;
				}
				while (DialogHistory.Count < 1)
				{
					DialogHistory.Add(new DialogEntry(string.Empty, fromDraedon: true));
				}
			}
			else if (!DraedonDialogRegistry.DialogOptions[1].HasBeenSeen)
			{
				if (inquiry != DraedonDialogRegistry.DialogOptions[1].Inquiry)
				{
					continue;
				}
				while (DialogHistory.Count < 1)
				{
					DialogHistory.Add(new DialogEntry(string.Empty, fromDraedon: true));
				}
			}
			else if (inquiry == DraedonDialogRegistry.DialogOptions[0].Inquiry || inquiry == DraedonDialogRegistry.DialogOptions[1].Inquiry)
			{
				continue;
			}
			Vector2 markerDrawPosition = textTopLeft - markerDrawPositionOffset;
			markerDrawPosition.Y += markerScale.Y * 22f;
			Color textColor = Color.Cyan;
			Color markerColor = Color.White;
			Vector2 textArea = FontAssetSystem.CodebreakerDialog.Value.MeasureString(inquiry) * GeneralScale;
			((Rectangle)(ref textAreaRect))._002Ector((int)textTopLeft.X, (int)textTopLeft.Y, (int)(textArea.X * 0.9f), (int)textArea.Y);
			Rectangle markerArea = Utils.CenteredRectangle(markerDrawPosition, markerTextureSize);
			textAreaRect.Y = markerArea.Y;
			textAreaRect.Height = markerArea.Height;
			dialog.Update();
			if (dialog.BloomOpacity > 0f)
			{
				Main.spriteBatch.End();
				Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.Additive, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Matrix.Identity);
				Texture2D bloomTex = ModContent.Request<Texture2D>("CalamityMod/UI/ModeIndicator/BloomFlare", (AssetRequestMode)2).Value;
				float bloomTexScale = MathF.Sin(Main.GlobalTimeWrappedHourly) * 0.05f + 0.26f;
				float bloomTexRotation = Main.GlobalTimeWrappedHourly * 0.5f;
				Main.spriteBatch.Draw(bloomTex, markerDrawPosition, (Rectangle?)null, Color.SteelBlue * dialog.BloomOpacity * opacity, bloomTexRotation, new Vector2(123f, 124f), bloomTexScale, (SpriteEffects)0, 0f);
				Main.spriteBatch.End();
				Main.spriteBatch.Begin((SpriteSortMode)0, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Matrix.Identity);
			}
			mouseScreenArea = MouseScreenArea;
			if (!((Rectangle)(ref mouseScreenArea)).Intersects(textAreaRect))
			{
				mouseScreenArea = MouseScreenArea;
				if (!((Rectangle)(ref mouseScreenArea)).Intersects(markerArea))
				{
					goto IL_07dd;
				}
			}
			if (opacity >= 1f)
			{
				textColor = Color.Lerp(textColor, Color.Yellow, 0.5f);
				markerColor = Color.Yellow;
				hoveringOverAnyOption = true;
				if (HoverSoundDialogType != inquiry)
				{
					HoverSoundDialogType = inquiry;
					SoundEngine.PlaySound(in DialogOptionHoverSound);
				}
				if (Main.mouseLeft && Main.mouseLeftRelease)
				{
					if (!Main.LocalPlayer.Calamity().HasTalkedAtCodebreaker)
					{
						DialogHistory.Insert(0, new DialogEntry(string.Empty, fromDraedon: true));
						OptionsTextOpacity = 0f;
					}
					if (inquiry == DraedonDialogRegistry.DialogOptions[0].Inquiry)
					{
						DraedonTextCreationTimer = -72;
					}
					if (DialogHistory.Count <= 0)
					{
						DialogHistory.Add(new DialogEntry(inquiry, fromDraedon: false));
						DialogHistory.Add(new DialogEntry(string.Empty, fromDraedon: true));
					}
					else
					{
						List<DialogEntry> dialogHistory = DialogHistory;
						dialogHistory[dialogHistory.Count - 1] = new DialogEntry(inquiry, fromDraedon: false);
						DialogHistory.Add(new DialogEntry(string.Empty, fromDraedon: true));
					}
					FullDraedonText = dialog.Response.Replace("\\n", "\n");
					WrittenDraedonText = string.Empty;
					Texture2D dialogOutline = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/DraedonDialogOutline", (AssetRequestMode)2).Value;
					Utils.CenteredRectangle(selectionCenter, dialogOutline.Size() * panelScale);
					bottomPadding = (float)FontAssetSystem.CodebreakerDialog.Value.LineSpacing * panelScale.Y * 3f;
					DialogVerticalOffset = 0f - (DialogHeight - (float)dialogOutline.Height * panelScale.Y + bottomPadding);
					DialogScroller.PositionYInterpolant = 1f;
					if (DialogVerticalOffset > 0f)
					{
						DialogVerticalOffset = 0f;
					}
					if (!Main.LocalPlayer.Calamity().SeenDraedonDialogs.Contains(dialog.ID))
					{
						Main.LocalPlayer.Calamity().SeenDraedonDialogs.Add(dialog.ID);
					}
				}
			}
			goto IL_07dd;
			IL_07dd:
			Vector2 markerTextureOrigin = markerTexture.Size() * 0.5f;
			Main.spriteBatch.Draw(markerTexture, markerDrawPosition, (Rectangle?)null, markerColor * opacity, 0f, markerTextureOrigin, markerScale, (SpriteEffects)0, 0f);
			ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssetSystem.CodebreakerDialog.Value, inquiry, textTopLeft, textColor * opacity, 0f, Vector2.Zero, Vector2.One * GeneralScale * 0.85f);
			textTopLeft.Y += verticalOffsetPerOption;
			OptionsTextHeight += verticalOffsetPerOption;
		}
		if (!hoveringOverAnyOption)
		{
			HoverSoundDialogType = null;
		}
		Main.spriteBatch.ReleaseCutoffRegion(Matrix.Identity, (SpriteSortMode)0);
	}

	public static void DisplayDialogHistory(Rectangle panelArea, Vector2 panelScale)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_039d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_050a: Unknown result type (might be due to invalid IL or missing references)
		//IL_050f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_0520: Unknown result type (might be due to invalid IL or missing references)
		//IL_0525: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_0547: Unknown result type (might be due to invalid IL or missing references)
		//IL_0551: Unknown result type (might be due to invalid IL or missing references)
		//IL_0556: Unknown result type (might be due to invalid IL or missing references)
		//IL_0558: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_040b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_07eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0602: Unknown result type (might be due to invalid IL or missing references)
		//IL_0607: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_060b: Unknown result type (might be due to invalid IL or missing references)
		//IL_060d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0612: Unknown result type (might be due to invalid IL or missing references)
		//IL_061c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0621: Unknown result type (might be due to invalid IL or missing references)
		//IL_0626: Unknown result type (might be due to invalid IL or missing references)
		//IL_0636: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_064c: Unknown result type (might be due to invalid IL or missing references)
		//IL_065a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0661: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_0684: Unknown result type (might be due to invalid IL or missing references)
		//IL_068b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0692: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0738: Unknown result type (might be due to invalid IL or missing references)
		//IL_073a: Unknown result type (might be due to invalid IL or missing references)
		//IL_073d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0747: Unknown result type (might be due to invalid IL or missing references)
		//IL_074c: Unknown result type (might be due to invalid IL or missing references)
		//IL_075b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0776: Unknown result type (might be due to invalid IL or missing references)
		//IL_0786: Unknown result type (might be due to invalid IL or missing references)
		//IL_06fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0707: Unknown result type (might be due to invalid IL or missing references)
		//IL_070d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_0719: Unknown result type (might be due to invalid IL or missing references)
		//IL_071b: Unknown result type (might be due to invalid IL or missing references)
		float dialogHistoryDrawInterpolant = Utils.GetLerpValue(0.3f, 0f, DraedonScreenStaticInterpolant, clamped: true);
		Texture2D dialogOutline = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/DraedonDialogOutline", (AssetRequestMode)2).Value;
		Vector2 selectionCenter = panelArea.TopRight() - new Vector2((float)dialogOutline.Width * 0.5f + 12f, (float)dialogOutline.Height * -0.5f - 12f) * panelScale;
		Rectangle dialogArea = Utils.CenteredRectangle(selectionCenter, dialogOutline.Size() * panelScale);
		Main.spriteBatch.Draw(dialogOutline, selectionCenter, (Rectangle?)null, Color.White * dialogHistoryDrawInterpolant, 0f, dialogOutline.Size() * 0.5f, panelScale, (SpriteEffects)0, 0f);
		Rectangle textCutoffRegion = dialogArea;
		textCutoffRegion.Y += 6;
		textCutoffRegion.Height -= 10;
		RasterizerState rasterizer = Main.Rasterizer;
		rasterizer.ScissorTestEnable = true;
		Main.spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, rasterizer, (Effect)null, Matrix.Identity);
		((GraphicsResource)Main.spriteBatch).GraphicsDevice.ScissorRectangle = textCutoffRegion;
		if (string.IsNullOrEmpty(FullDraedonText))
		{
			FullDraedonText = InquiryText;
		}
		if (DialogSoundDelay > 0)
		{
			DialogSoundDelay--;
		}
		if (DraedonScreenStaticInterpolant <= 0f)
		{
			DraedonTextCreationTimer++;
		}
		if (DraedonTextCreationTimer >= DraedonTextCreationRate && WrittenDraedonText.Length < FullDraedonText.Length)
		{
			char nextLetter = FullDraedonText[WrittenDraedonText.Length];
			WrittenDraedonText += nextLetter;
			DraedonTextCreationTimer = 0;
			if (DialogHistory.Count <= 0)
			{
				DialogHistory.Add(new DialogEntry(string.Empty, fromDraedon: true));
			}
			List<DialogEntry> dialogHistory = DialogHistory;
			dialogHistory[dialogHistory.Count - 1].Dialog += nextLetter;
			if (DialogHeight * 1.2f >= (float)textCutoffRegion.Height && !string.IsNullOrEmpty(nextLetter.ToString()) && LatestDialogHeightIncrease > 0f)
			{
				DialogVerticalOffset -= LatestDialogHeightIncrease;
				DialogVerticalOffset += DialogOffYCache;
				DialogHeight -= DialogOffYCache;
				DialogOffYCache = 0f;
			}
			if (WrittenDraedonText.Length >= FullDraedonText.Length)
			{
				if (WrittenDraedonText == DraedonDialogRegistry.DialogOptions[0].Response)
				{
					Main.LocalPlayer.Calamity().HasTalkedAtCodebreaker = true;
				}
				DialogHistory.Add(new DialogEntry(string.Empty, fromDraedon: true));
			}
			if (DialogSoundDelay <= 0)
			{
				switch (nextLetter)
				{
				default:
				{
					DialogHeight -= DialogOffYCache;
					DialogOffYCache = 0f;
					SoundStyle style = Main.rand.Next(DraedonTalks)with
					{
						Volume = 0.4f
					};
					SoundEngine.PlaySound(in style, Main.LocalPlayer.Center);
					DialogSoundDelay = 4;
					break;
				}
				case '\n':
					if (WrittenDraedonText.Length < FullDraedonText.Length - 1 && FullDraedonText[WrittenDraedonText.Length] == '\n')
					{
						DialogOffYCache += panelScale.Y * 10f;
					}
					break;
				case ' ':
					break;
				}
			}
		}
		Vector2 textTopLeft = dialogArea.TopLeft() + new Vector2(20f, 14f) * panelScale;
		float bottomPadding = (float)FontAssetSystem.CodebreakerDialog.Value.LineSpacing * panelScale.Y * 3f;
		float cutoffDistance = DialogHeight - ((float)dialogOutline.Height * panelScale.Y - bottomPadding);
		if (cutoffDistance > 0f && OptionsTextOpacity > 0f)
		{
			Rectangle mouseScreenArea = MouseScreenArea;
			if (((Rectangle)(ref mouseScreenArea)).Intersects(dialogArea))
			{
				DialogVerticalOffset += (float)PlayerInput.ScrollWheelDeltaForUI * 0.2f;
			}
			DialogScroller.PositionYInterpolant = MathHelper.Clamp(DialogVerticalOffset / (0f - cutoffDistance), 0f, 1f);
			DialogScroller.Draw((float)((Rectangle)(ref dialogArea)).Top + GeneralScale * 66f, (float)((Rectangle)(ref dialogArea)).Bottom - GeneralScale * 66f, (float)((Rectangle)(ref dialogArea)).Right - GeneralScale * 12f, GeneralScale * 0.8f, OptionsTextOpacity);
			DialogVerticalOffset = DialogScroller.PositionYInterpolant * (0f - cutoffDistance);
		}
		IEnumerable<DialogEntry> enumerable = DialogHistory.Where((DialogEntry d) => !string.IsNullOrEmpty(d.Dialog));
		Texture2D markerTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/DraedonInquirySelector", (AssetRequestMode)2).Value;
		Vector2 markerScale = panelScale * 0.24f;
		Vector2 markerDrawPositionOffset = Vector2.UnitX * (float)markerTexture.Width * markerScale.X * 0.6f;
		float markerDrawPositionOffsetY = markerScale.Y * 24f;
		float localTextOffsetY = markerScale.Y * 4f;
		Vector2 markerTextureOrigin = markerTexture.Size() * 0.5f;
		float panelOffsetPerLine = panelScale.Y * 10f;
		float panelOffsetPerEntry = panelScale.Y * 16f;
		float top = float.MaxValue;
		float bottom = float.MinValue;
		Vector2 anchorPoint = default(Vector2);
		foreach (DialogEntry entry in enumerable)
		{
			int lineIndex = 0;
			string[] array = Utils.WordwrapString(entry.Dialog, FontAssetSystem.CodebreakerDialog.Value, 336, 1000, out var _);
			foreach (string line in array)
			{
				if (!string.IsNullOrEmpty(line))
				{
					bool fromDraedon = entry.FromDraedon;
					Color dialogColor = Draedon.TextColor * 1.25f;
					Vector2 localTextTopLeft = textTopLeft + Vector2.UnitY * DialogVerticalOffset;
					Vector2 markerDrawPosition = textTopLeft - markerDrawPositionOffset + Vector2.UnitY * DialogVerticalOffset;
					markerDrawPosition.Y += markerDrawPositionOffsetY;
					SpriteEffects markerDirection = (SpriteEffects)0;
					if (!fromDraedon)
					{
						((Vector2)(ref anchorPoint))._002Ector((float)((Rectangle)(ref dialogArea)).Center.X, markerDrawPosition.Y);
						markerDrawPosition.X = anchorPoint.X + (anchorPoint.X - markerDrawPosition.X) - GeneralScale * 12f;
						localTextTopLeft.X = anchorPoint.X + (anchorPoint.X - localTextTopLeft.X) - GeneralScale * 14f;
						localTextTopLeft.X -= FontAssetSystem.CodebreakerDialog.Value.MeasureString(line).X * DialogTextScale.X;
						localTextTopLeft.Y -= localTextOffsetY;
						dialogColor = Color.LightGray;
						markerDirection = (SpriteEffects)1;
					}
					if (lineIndex <= 0)
					{
						Main.spriteBatch.Draw(markerTexture, markerDrawPosition, (Rectangle?)null, Color.White * dialogHistoryDrawInterpolant, 0f, markerTextureOrigin, markerScale, markerDirection, 0f);
					}
					ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssetSystem.CodebreakerDialog.Value, line, localTextTopLeft, dialogColor * dialogHistoryDrawInterpolant, 0f, Vector2.Zero, DialogTextScale);
					textTopLeft.Y += panelOffsetPerLine;
					lineIndex++;
					top = MathF.Min(top, localTextTopLeft.Y);
					bottom = MathF.Max(bottom, localTextTopLeft.Y);
				}
			}
			textTopLeft.Y += panelOffsetPerEntry;
		}
		LatestDialogHeightIncrease = bottom - top - DialogHeight;
		DialogHeight = bottom - top;
		Main.spriteBatch.ReleaseCutoffRegion(Matrix.Identity, (SpriteSortMode)0);
	}

	public static void Draw(SpriteBatch spriteBatch)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0315: Unknown result type (might be due to invalid IL or missing references)
		//IL_031a: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0363: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		//IL_0367: Unknown result type (might be due to invalid IL or missing references)
		//IL_0371: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0380: Unknown result type (might be due to invalid IL or missing references)
		//IL_0385: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0485: Unknown result type (might be due to invalid IL or missing references)
		//IL_048f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0494: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0423: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0466: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_061f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0623: Unknown result type (might be due to invalid IL or missing references)
		//IL_062d: Unknown result type (might be due to invalid IL or missing references)
		//IL_05e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0609: Unknown result type (might be due to invalid IL or missing references)
		//IL_067d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0687: Unknown result type (might be due to invalid IL or missing references)
		//IL_068c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0691: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b9: Unknown result type (might be due to invalid IL or missing references)
		if (!TileEntity.ByID.ContainsKey(ViewedTileEntityID) || !(TileEntity.ByID[ViewedTileEntityID] is TECodebreaker codebreakerTileEntity) || !Main.LocalPlayer.WithinRange(codebreakerTileEntity.Center, 270f) || !Main.playerInventory || Main.LocalPlayer.channel)
		{
			VerificationButtonScale = 1f;
			CancelButtonScale = 0.75f;
			ContactButtonScale = 1f;
			CommunicateButtonScale = 1f;
			ExitButtonScale = 1f;
			CommunicationPanelScale = 0f;
			ViewedTileEntityID = -1;
			AwaitingCloseConfirmation = false;
			DisplayingCommunicationText = false;
			MechIconScale = 1f;
			DialogScroller.Reset();
			TopicOptionsScroller.Reset();
			DialogVerticalOffset = 0f;
			DialogOffYCache = 0f;
			OptionsTextVerticalOffset = 0f;
			DialogHeight = 0f;
			LatestDialogHeightIncrease = 0f;
			return;
		}
		Texture2D backgroundTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/DraedonDecrypterBackground", (AssetRequestMode)2).Value;
		spriteBatch.Draw(backgroundTexture, BackgroundCenter, (Rectangle?)null, Color.White, 0f, backgroundTexture.Size() * 0.5f, GeneralScale * (1f - CommunicationPanelScale), (SpriteEffects)0, 0f);
		Rectangle backgroundArea = Utils.CenteredRectangle(BackgroundCenter, backgroundTexture.Size() * GeneralScale);
		Rectangle mouseScreenArea = MouseScreenArea;
		if (((Rectangle)(ref mouseScreenArea)).Intersects(backgroundArea) && !DisplayingCommunicationText)
		{
			Main.blockMouse = (Main.LocalPlayer.mouseInterface = true);
		}
		if (DisplayingCommunicationText && CommunicationPanelScale == 0f)
		{
			CommunicationPanelScale = 1f;
			DraedonScreenStaticInterpolant = 1f;
		}
		if (!DisplayingCommunicationText && CommunicationPanelScale != 0f)
		{
			CommunicationPanelScale = 0f;
			DraedonScreenStaticInterpolant = 0f;
		}
		if (DisplayingCommunicationText)
		{
			DisplayCommunicationPanel();
			DraedonScreenStaticInterpolant = MathHelper.Clamp(DraedonScreenStaticInterpolant - 0.01408f, 0f, 1f);
			return;
		}
		DraedonTextCreationTimer = 0;
		if (!string.IsNullOrEmpty(WrittenDraedonText) && FullDraedonText == DraedonDialogRegistry.DialogOptions[0].Inquiry)
		{
			Main.LocalPlayer.Calamity().HasTalkedAtCodebreaker = true;
		}
		WrittenDraedonText = (FullDraedonText = string.Empty);
		DialogHistory.Clear();
		bool canSummonDraedon = codebreakerTileEntity.ReadyToSummonDraedon && CalamityWorld.AbleToSummonDraedon;
		bool num = codebreakerTileEntity.ReadyToSummonDraedon && DownedBossSystem.downedExoMechs;
		Vector2 backgroundTopLeft = BackgroundCenter - backgroundTexture.Size() * GeneralScale * 0.5f;
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonsArsenal/PowerCellSlot_Empty", (AssetRequestMode)2).Value;
		Texture2D occupiedCellIconTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonsArsenal/PowerCellSlot_Filled", (AssetRequestMode)2).Value;
		Texture2D bloodyVeinIconTexture = ModContent.Request<Texture2D>("CalamityMod/Items/Pets/BloodyVein", (AssetRequestMode)2).Value;
		Texture2D cellTexture = value;
		if (codebreakerTileEntity.InputtedCellCount > 0)
		{
			cellTexture = ((!codebreakerTileEntity.ContainsBloodyVein) ? occupiedCellIconTexture : bloodyVeinIconTexture);
		}
		Vector2 cellDrawCenter = backgroundTopLeft + Vector2.One * GeneralScale * 60f;
		Vector2 schematicSlotDrawCenter = cellDrawCenter + Vector2.UnitY * GeneralScale * 70f;
		Vector2 costDisplayLocation = schematicSlotDrawCenter + Vector2.UnitY * GeneralScale * 20f;
		Vector2 costVerificationLocation = costDisplayLocation + Vector2.UnitY * GeneralScale * 60f;
		Vector2 summonButtonCenter = backgroundTopLeft + new Vector2(58f, (float)backgroundTexture.Height - 48f) * GeneralScale;
		Vector2 talkButtonCenter = summonButtonCenter + Vector2.UnitX * GeneralScale * 172f;
		if (codebreakerTileEntity.HeldSchematicID != 0 && !codebreakerTileEntity.CanDecryptHeldSchematic)
		{
			DisplayNotStrongEnoughErrorText(schematicSlotDrawCenter + new Vector2(-24f, 56f));
		}
		else if (codebreakerTileEntity.HeldSchematicID != 0 && codebreakerTileEntity.DecryptionCountdown == 0 && !codebreakerTileEntity.ContainsBloodyVein)
		{
			int cost = codebreakerTileEntity.DecryptionCellCost;
			DisplayCostText(costDisplayLocation, cost);
			if (codebreakerTileEntity.InputtedCellCount >= cost)
			{
				if (canSummonDraedon)
				{
					costVerificationLocation.X -= GeneralScale * 15f;
					summonButtonCenter.X += GeneralScale * 15f;
				}
				DrawCostVerificationButton(codebreakerTileEntity, costVerificationLocation);
			}
		}
		else if (codebreakerTileEntity.DecryptionCountdown > 0)
		{
			DisplayDecryptCancelButton(codebreakerTileEntity, costVerificationLocation - Vector2.UnitY * GeneralScale * 30f);
		}
		if (canSummonDraedon)
		{
			HandleDraedonSummonButton(codebreakerTileEntity, summonButtonCenter);
		}
		if (num)
		{
			HandleDraedonTalkButton(talkButtonCenter);
		}
		if (codebreakerTileEntity.DecryptionCountdown > 0 || AwaitingDecryptionTextClose)
		{
			HandleDecryptionStuff(codebreakerTileEntity, backgroundTexture, backgroundTopLeft, schematicSlotDrawCenter + Vector2.UnitY * GeneralScale * 80f);
		}
		if (codebreakerTileEntity.DecryptionCountdown > 0 && AwaitingCloseConfirmation)
		{
			DrawDecryptCancelConfirmationText(costVerificationLocation);
		}
		Texture2D schematicIconBG = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/EncryptedSchematicSlotBackground", (AssetRequestMode)2).Value;
		Texture2D schematicIconTexture = schematicIconBG;
		int schematicType = 0;
		if (codebreakerTileEntity.HeldSchematicID > 0)
		{
			schematicType = EncryptedSchematicIDRelationshipDict.Dict[codebreakerTileEntity.HeldSchematicID];
		}
		if (schematicType == ModContent.ItemType<EncryptedSchematicPlanetoid>())
		{
			schematicIconTexture = ModContent.Request<Texture2D>("CalamityMod/Items/DraedonMisc/EncryptedSchematicPlanetoid", (AssetRequestMode)2).Value;
		}
		if (schematicType == ModContent.ItemType<EncryptedSchematicJungle>())
		{
			schematicIconTexture = ModContent.Request<Texture2D>("CalamityMod/Items/DraedonMisc/EncryptedSchematicJungle", (AssetRequestMode)2).Value;
		}
		if (schematicType == ModContent.ItemType<EncryptedSchematicHell>())
		{
			schematicIconTexture = ModContent.Request<Texture2D>("CalamityMod/Items/DraedonMisc/EncryptedSchematicHell", (AssetRequestMode)2).Value;
		}
		if (schematicType == ModContent.ItemType<EncryptedSchematicIce>())
		{
			schematicIconTexture = ModContent.Request<Texture2D>("CalamityMod/Items/DraedonMisc/EncryptedSchematicIce", (AssetRequestMode)2).Value;
		}
		spriteBatch.Draw(schematicIconBG, schematicSlotDrawCenter, (Rectangle?)null, Color.White, 0f, schematicIconBG.Size() * 0.5f, GeneralScale, (SpriteEffects)0, 0f);
		if (codebreakerTileEntity.HeldSchematicID != 0)
		{
			spriteBatch.Draw(schematicIconTexture, schematicSlotDrawCenter, (Rectangle?)null, Color.White, 0f, schematicIconTexture.Size() * 0.5f, GeneralScale, (SpriteEffects)0, 0f);
		}
		HandleSchematicSlotInteractions(codebreakerTileEntity, schematicSlotDrawCenter, cellTexture.Size() * GeneralScale);
		Item temporaryPowerCell = new Item();
		if (codebreakerTileEntity.ContainsBloodyVein)
		{
			temporaryPowerCell.SetDefaults(ModContent.ItemType<BloodyVein>());
		}
		else
		{
			temporaryPowerCell.SetDefaults(ModContent.ItemType<DraedonPowerCell>());
		}
		temporaryPowerCell.stack = codebreakerTileEntity.InputtedCellCount;
		Vector2 cellInteractionArea = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonsArsenal/PowerCellSlot_Empty", (AssetRequestMode)2).Value.Size() * GeneralScale;
		CalamityUtils.DrawPowercellSlot(spriteBatch, temporaryPowerCell, cellDrawCenter, GeneralScale);
		HandleCellSlotInteractions(codebreakerTileEntity, temporaryPowerCell, cellDrawCenter, cellInteractionArea);
		if (!AwaitingCloseConfirmation)
		{
			DrawExitButton(Vector2.Lerp(summonButtonCenter, talkButtonCenter, 0.5f), 1f);
		}
	}

	public static void DrawExitButton(Vector2 drawPosition, float opacity)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D cancelButton = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/DecryptCancelIcon", (AssetRequestMode)2).Value;
		Rectangle clickArea = Utils.CenteredRectangle(drawPosition, cancelButton.Size() * VerificationButtonScale);
		Rectangle mouseScreenArea = MouseScreenArea;
		if (((Rectangle)(ref mouseScreenArea)).Intersects(clickArea))
		{
			ExitButtonScale = MathHelper.Clamp(ExitButtonScale + 0.035f, 1f, 1.4f);
			if (Main.mouseLeft && Main.mouseLeftRelease)
			{
				ViewedTileEntityID = -1;
			}
		}
		else
		{
			ExitButtonScale = MathHelper.Clamp(ExitButtonScale - 0.05f, 1f, 1.4f);
		}
		Main.spriteBatch.Draw(cancelButton, drawPosition, (Rectangle?)null, Color.White, 0f, cancelButton.Size() * 0.5f, ExitButtonScale * GeneralScale, (SpriteEffects)0, 0f);
		string exitText = CalamityUtils.GetTextValue("UI.Exit");
		drawPosition.X -= FontAssets.MouseText.Value.MeasureString(exitText).X * GeneralScale * 0.5f;
		drawPosition.Y += GeneralScale * 20f;
		Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, exitText, drawPosition.X, drawPosition.Y, Color.Red * opacity, Color.Black * opacity, Vector2.Zero, GeneralScale);
	}

	public static void HandleCellSlotInteractions(TECodebreaker codebreakerTileEntity, Item temporaryItem, Vector2 cellIconCenter, Vector2 area)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		Rectangle clickArea = Utils.CenteredRectangle(cellIconCenter, area);
		Rectangle mouseScreenArea = MouseScreenArea;
		if (!((Rectangle)(ref mouseScreenArea)).Intersects(clickArea))
		{
			return;
		}
		if (!temporaryItem.IsAir)
		{
			Main.HoverItem = temporaryItem;
		}
		if (Main.mouseLeft && Main.mouseLeftRelease && codebreakerTileEntity.DecryptionCountdown <= 0)
		{
			int powercellID = ModContent.ItemType<DraedonPowerCell>();
			int sampleID = ModContent.ItemType<BloodyVein>();
			short cellStackDiff = 0;
			bool shouldPlaySound = true;
			if (Main.keyState.PressingShift() && Main.LocalPlayer.ItemSpace(temporaryItem).CanTakeItemToPersonalInventory)
			{
				cellStackDiff = (short)(-Math.Min(temporaryItem.stack, temporaryItem.maxStack));
				Player localPlayer = Main.LocalPlayer;
				IEntitySource source = localPlayer.GetSource_TileInteraction(codebreakerTileEntity.Position.X, codebreakerTileEntity.Position.Y);
				localPlayer.QuickSpawnItem(source, codebreakerTileEntity.ContainsBloodyVein ? sampleID : powercellID, -cellStackDiff);
				shouldPlaySound = false;
			}
			else
			{
				bool num = Main.mouseItem.type == powercellID || (Main.mouseItem.type == sampleID && Main.zenithWorld);
				bool powercellsinserted = !codebreakerTileEntity.ContainsBloodyVein && temporaryItem.stack > 0;
				bool cansummon = codebreakerTileEntity.ReadyToSummonDraedon && CalamityWorld.AbleToSummonDraedon;
				if (num && temporaryItem.stack < 9999)
				{
					if ((Main.mouseItem.type == sampleID && Main.zenithWorld && !powercellsinserted) & cansummon)
					{
						if (temporaryItem.stack == 0)
						{
							SoundEngine.PlaySound(in BloodSound, codebreakerTileEntity.Center);
						}
						codebreakerTileEntity.ContainsBloodyVein = true;
						int spaceLeft = 9999 - temporaryItem.stack;
						int cellsToInsert = Math.Min(Main.mouseItem.stack, spaceLeft);
						cellStackDiff = (short)cellsToInsert;
						Main.mouseItem.stack -= cellsToInsert;
						if (Main.mouseItem.stack == 0)
						{
							Main.mouseItem.TurnToAir();
						}
						AwaitingDecryptionTextClose = false;
					}
					if (Main.mouseItem.type == powercellID && ((temporaryItem.stack == 0) | powercellsinserted))
					{
						codebreakerTileEntity.ContainsBloodyVein = false;
						int spaceLeft2 = 9999 - temporaryItem.stack;
						int cellsToInsert2 = Math.Min(Main.mouseItem.stack, spaceLeft2);
						cellStackDiff = (short)cellsToInsert2;
						Main.mouseItem.stack -= cellsToInsert2;
						if (Main.mouseItem.stack == 0)
						{
							Main.mouseItem.TurnToAir();
						}
						AwaitingDecryptionTextClose = false;
					}
				}
				else if (Main.mouseItem.IsAir && temporaryItem.stack > 0)
				{
					cellStackDiff = (short)(-temporaryItem.stack);
					if (cellStackDiff < -temporaryItem.maxStack)
					{
						cellStackDiff = (short)(-temporaryItem.maxStack);
					}
					Main.mouseItem.SetDefaults(temporaryItem.type);
					Main.mouseItem.stack = -cellStackDiff;
					temporaryItem.TurnToAir();
					AwaitingDecryptionTextClose = false;
					codebreakerTileEntity.ContainsBloodyVein = false;
				}
			}
			if (cellStackDiff != 0)
			{
				if (shouldPlaySound)
				{
					SoundEngine.PlaySound(in SoundID.Grab);
				}
				AwaitingDecryptionTextClose = false;
				codebreakerTileEntity.InputtedCellCount += cellStackDiff;
				codebreakerTileEntity.SyncContainedStuff();
			}
		}
		if (temporaryItem.stack > 0)
		{
			Main.instance.MouseTextHackZoom(string.Empty);
		}
	}

	public static void HandleSchematicSlotInteractions(TECodebreaker codebreakerTileEntity, Vector2 schematicIconCenter, Vector2 area)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		Rectangle clickArea = Utils.CenteredRectangle(schematicIconCenter, area);
		Rectangle mouseScreenArea = MouseScreenArea;
		if (!((Rectangle)(ref mouseScreenArea)).Intersects(clickArea))
		{
			return;
		}
		if (!Main.mouseLeft || !Main.mouseLeftRelease || codebreakerTileEntity.DecryptionCountdown > 0)
		{
			return;
		}
		bool isAir = Main.mouseItem.IsAir;
		bool countdownDone = codebreakerTileEntity.DecryptionCountdown <= 0;
		bool hasValidSchematicID = EncryptedSchematicIDRelationshipDict.TryGet(codebreakerTileEntity.HeldSchematicID, out var schematicItemType);
		bool hasHandHoldingValidSchematicItem = EncryptedSchematicIDRelationshipDict.TryGetKey(Main.mouseItem.type, out var _);
		bool hasMouseHoldingValidSchematicItem = EncryptedSchematicIDRelationshipDict.TryGetKey(Main.mouseItem.type, out var mouseHoldSchematicID);
		if (isAir & countdownDone & hasValidSchematicID)
		{
			Main.mouseItem.SetDefaults(schematicItemType);
			codebreakerTileEntity.HeldSchematicID = 0;
			codebreakerTileEntity.DecryptionCountdown = 0;
			codebreakerTileEntity.SyncContainedStuff();
			SoundEngine.PlaySound(in SoundID.Grab);
			AwaitingDecryptionTextClose = false;
		}
		else if ((hasHandHoldingValidSchematicItem & hasMouseHoldingValidSchematicItem) && codebreakerTileEntity.HeldSchematicID == 0)
		{
			codebreakerTileEntity.HeldSchematicID = mouseHoldSchematicID;
			Main.mouseItem.TurnToAir();
			codebreakerTileEntity.SyncContainedStuff();
			SoundEngine.PlaySound(in SoundID.Grab);
			AwaitingDecryptionTextClose = false;
		}
		else if (hasHandHoldingValidSchematicItem & hasValidSchematicID)
		{
			int previouslyHeldSchematic = schematicItemType;
			SoundEngine.PlaySound(in SoundID.Grab);
			if ((Main.mouseItem.type != previouslyHeldSchematic) & hasMouseHoldingValidSchematicItem)
			{
				codebreakerTileEntity.HeldSchematicID = mouseHoldSchematicID;
				Main.mouseItem.SetDefaults(previouslyHeldSchematic);
				codebreakerTileEntity.SyncContainedStuff();
				AwaitingDecryptionTextClose = false;
			}
		}
	}

	public static void DisplayCostText(Vector2 drawPosition, int totalCellsCost)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		string text = CalamityUtils.GetTextValue("UI.Cost");
		drawPosition.X -= GeneralScale * 30f;
		Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, text, drawPosition.X, drawPosition.Y + GeneralScale * 20f, Color.White * ((float)(int)Main.mouseTextColor / 255f), Color.Black, Vector2.Zero, GeneralScale);
		Texture2D cellTexture = ModContent.Request<Texture2D>("CalamityMod/Items/DraedonMisc/DraedonPowerCell", (AssetRequestMode)2).Value;
		Vector2 offsetDrawPosition = default(Vector2);
		((Vector2)(ref offsetDrawPosition))._002Ector(drawPosition.X + ChatManager.GetStringSize(FontAssets.MouseText.Value, text, Vector2.One).X * GeneralScale + GeneralScale * 15f, drawPosition.Y + GeneralScale * 30f);
		Main.spriteBatch.Draw(cellTexture, offsetDrawPosition, (Rectangle?)null, Color.White, 0f, cellTexture.Size() * 0.5f, GeneralScale, (SpriteEffects)0, 0f);
		Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.ItemStack.Value, totalCellsCost.ToString(), offsetDrawPosition.X - GeneralScale * 11f, offsetDrawPosition.Y, Color.White, Color.Black, new Vector2(0.3f), GeneralScale * 0.75f);
	}

	public static void DrawCostVerificationButton(TECodebreaker codebreakerTileEntity, Vector2 drawPosition)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		Texture2D confirmationTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/DecryptIcon", (AssetRequestMode)2).Value;
		Rectangle clickArea = Utils.CenteredRectangle(drawPosition, confirmationTexture.Size() * VerificationButtonScale);
		Rectangle mouseScreenArea = MouseScreenArea;
		if (((Rectangle)(ref mouseScreenArea)).Intersects(clickArea))
		{
			VerificationButtonScale = MathHelper.Clamp(VerificationButtonScale + 0.035f, 1f, 1.35f);
			if (Main.mouseLeft && Main.mouseLeftRelease)
			{
				SoundEngine.PlaySound(in SoundID.Zombie67, Main.LocalPlayer.Center);
				AwaitingDecryptionTextClose = true;
				codebreakerTileEntity.InitialCellCountBeforeDecrypting = codebreakerTileEntity.InputtedCellCount;
				codebreakerTileEntity.DecryptionCountdown = codebreakerTileEntity.DecryptionTotalTime;
				codebreakerTileEntity.SyncContainedStuff();
				codebreakerTileEntity.SyncDecryptCountdown();
			}
		}
		else
		{
			VerificationButtonScale = MathHelper.Clamp(VerificationButtonScale - 0.05f, 1f, 1.35f);
		}
		Main.spriteBatch.Draw(confirmationTexture, drawPosition, (Rectangle?)null, Color.White, 0f, confirmationTexture.Size() * 0.5f, VerificationButtonScale * GeneralScale, (SpriteEffects)0, 0f);
	}

	public static void DisplayDecryptCancelButton(TECodebreaker codebreakerTileEntity, Vector2 drawPosition)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		bool clickingMouse = Main.mouseLeft && Main.mouseLeftRelease;
		Texture2D cancelTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/DecryptCancelIcon", (AssetRequestMode)2).Value;
		Rectangle clickArea = Utils.CenteredRectangle(drawPosition, cancelTexture.Size() * CancelButtonScale * 1.2f);
		Rectangle mouseScreenArea = MouseScreenArea;
		if (((Rectangle)(ref mouseScreenArea)).Intersects(clickArea))
		{
			CancelButtonScale = MathHelper.Clamp(CancelButtonScale + 0.035f, 0.9f, 1.2f);
			if (clickingMouse)
			{
				if (AwaitingCloseConfirmation)
				{
					SoundEngine.PlaySound(in SoundID.Item94, Main.LocalPlayer.Center);
					AwaitingDecryptionTextClose = false;
					codebreakerTileEntity.InitialCellCountBeforeDecrypting = 0;
					codebreakerTileEntity.DecryptionCountdown = 0;
					codebreakerTileEntity.SyncContainedStuff();
					codebreakerTileEntity.SyncDecryptCountdown();
					AwaitingCloseConfirmation = false;
				}
				else
				{
					AwaitingCloseConfirmation = true;
				}
			}
		}
		else
		{
			CancelButtonScale = MathHelper.Clamp(CancelButtonScale - 0.05f, 0.9f, 1.2f);
			if (clickingMouse)
			{
				AwaitingCloseConfirmation = false;
			}
		}
		Main.spriteBatch.Draw(cancelTexture, drawPosition, (Rectangle?)null, Color.White, 0f, cancelTexture.Size() * 0.5f, CancelButtonScale * GeneralScale, (SpriteEffects)0, 0f);
	}

	public static void DrawDecryptCancelConfirmationText(Vector2 drawPosition)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		Texture2D textPanelTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/DraedonDecrypterScreen", (AssetRequestMode)2).Value;
		drawPosition.X += GeneralScale * 196f;
		Vector2 scale = new Vector2(1f, 0.3f) * GeneralScale;
		Main.spriteBatch.Draw(textPanelTexture, drawPosition, (Rectangle?)null, Color.White, 0f, textPanelTexture.Size() * 0.5f, scale, (SpriteEffects)0, 0f);
		string confirmationText = CalamityUtils.GetTextValue("UI.ConfirmationText");
		Vector2 confirmationTextPosition = drawPosition - FontAssets.MouseText.Value.MeasureString(confirmationText) * GeneralScale * 0.5f + Vector2.UnitY * GeneralScale * 4f;
		ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, confirmationText, confirmationTextPosition, Color.Red, 0f, Vector2.Zero, Vector2.One * GeneralScale);
	}

	public static void HandleDecryptionStuff(TECodebreaker codebreakerTileEntity, Texture2D backgroundTexture, Vector2 backgroundTopLeft, Vector2 barCenter)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0290: Unknown result type (might be due to invalid IL or missing references)
		//IL_0291: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_034a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0360: Unknown result type (might be due to invalid IL or missing references)
		//IL_0365: Unknown result type (might be due to invalid IL or missing references)
		Texture2D textPanelTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/DraedonDecrypterScreen", (AssetRequestMode)2).Value;
		Vector2 textPanelCenter = backgroundTopLeft + Vector2.UnitX * (float)backgroundTexture.Width * GeneralScale + textPanelTexture.Size() * new Vector2(-0.5f, 0.5f) * GeneralScale;
		Main.spriteBatch.Draw(textPanelTexture, textPanelCenter, (Rectangle?)null, Color.White, 0f, textPanelTexture.Size() * 0.5f, GeneralScale, (SpriteEffects)0, 0f);
		int textPadding = 6;
		string trueMessage = codebreakerTileEntity.UnderlyingSchematicText;
		StringBuilder text = new StringBuilder((codebreakerTileEntity.DecryptionCountdown == 0) ? trueMessage : CalamityUtils.GenerateRandomAlphanumericString(500));
		for (int i = 0; i < trueMessage.Length; i++)
		{
			if (char.IsWhiteSpace(trueMessage[i]))
			{
				text[i] = trueMessage[i];
			}
		}
		for (int j = 0; j < (int)((float)trueMessage.Length * codebreakerTileEntity.DecryptionCompletion); j++)
		{
			text[j] = trueMessage[j];
		}
		Vector2 currentTextDrawPosition = backgroundTopLeft + new Vector2((float)(backgroundTexture.Width - textPanelTexture.Width + textPadding), 6f) * GeneralScale;
		string[] array = Utils.WordwrapString(text.ToString(), FontAssets.MouseText.Value, (int)((double)textPanelTexture.Width * 1.5 - (double)(textPadding * 2)), 10, out var _);
		foreach (string line in array)
		{
			if (!string.IsNullOrEmpty(line))
			{
				ChatManager.DrawColorCodedStringWithShadow(Main.spriteBatch, FontAssets.MouseText.Value, line, currentTextDrawPosition, Color.Cyan, 0f, Vector2.Zero, new Vector2(0.6f) * GeneralScale);
				currentTextDrawPosition.Y += GeneralScale * 16f;
			}
		}
		if (codebreakerTileEntity.DecryptionCountdown > 0)
		{
			Texture2D borderTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/CodebreakerDecyptionBar", (AssetRequestMode)2).Value;
			Texture2D barTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/CodebreakerDecyptionBarCharge", (AssetRequestMode)2).Value;
			Main.spriteBatch.Draw(borderTexture, barCenter, (Rectangle?)null, Color.White, 0f, borderTexture.Size() * 0.5f, GeneralScale, (SpriteEffects)0, 0f);
			Rectangle barRectangle = default(Rectangle);
			((Rectangle)(ref barRectangle))._002Ector(0, 0, (int)((float)barTexture.Width * codebreakerTileEntity.DecryptionCompletion), barTexture.Width);
			Main.spriteBatch.Draw(barTexture, barCenter, (Rectangle?)barRectangle, Color.White, 0f, barTexture.Size() * 0.5f, GeneralScale, (SpriteEffects)0, 0f);
			string completionText = $"{codebreakerTileEntity.DecryptionCompletion * 100f:n2}%";
			Vector2 textDrawPosition = barCenter + new Vector2((0f - FontAssets.MouseText.Value.MeasureString(completionText).X) * 0.5f, 10f) * GeneralScale;
			Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, completionText, textDrawPosition.X, textDrawPosition.Y, Color.Cyan * 1.2f, Color.Black, Vector2.Zero, GeneralScale);
		}
	}

	public static void HandleDraedonSummonButton(TECodebreaker codebreakerTileEntity, Vector2 drawPosition)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		Texture2D contactButton = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/ContactIcon", (AssetRequestMode)2).Value;
		Rectangle clickArea = Utils.CenteredRectangle(drawPosition, contactButton.Size() * VerificationButtonScale);
		float iconrotation = (codebreakerTileEntity.ContainsBloodyVein ? (Main.GlobalTimeWrappedHourly * 20f) : 0f);
		Rectangle mouseScreenArea = MouseScreenArea;
		if (((Rectangle)(ref mouseScreenArea)).Intersects(clickArea))
		{
			ContactButtonScale = MathHelper.Clamp(ContactButtonScale + 0.035f, 1f, 1.35f);
			if (Main.mouseLeft && Main.mouseLeftRelease)
			{
				CalamityWorld.DraedonSummonCountdown = 260;
				CalamityWorld.DraedonSummonPosition = codebreakerTileEntity.Center + new Vector2(-8f, -100f);
				if (Main.zenithWorld && codebreakerTileEntity.ContainsBloodyVein)
				{
					CalamityWorld.DraedonMechdusa = true;
				}
				SoundEngine.PlaySound(in SummonSound, CalamityWorld.DraedonSummonPosition);
				if (Main.netMode != 0)
				{
					CodebreakerSummonStuffPacket.Send();
				}
			}
		}
		else
		{
			ContactButtonScale = MathHelper.Clamp(ContactButtonScale - 0.05f, 1f, 1.35f);
		}
		Main.spriteBatch.Draw(contactButton, drawPosition, (Rectangle?)null, Color.White, iconrotation, contactButton.Size() * 0.5f, ContactButtonScale * GeneralScale, (SpriteEffects)0, 0f);
		string contactTextKey = "Contact";
		if (DownedBossSystem.downedExoMechs)
		{
			contactTextKey = "Summon";
		}
		if (codebreakerTileEntity.ContainsBloodyVein)
		{
			contactTextKey = "Evoke";
		}
		string contactText = CalamityUtils.GetTextValue("UI." + contactTextKey);
		Color contactTextColor = CalamityUtils.MulticolorLerp((float)Math.Cos(Main.GlobalTimeWrappedHourly * 0.7f) * 0.5f + 0.5f, CalamityUtils.ExoPalette);
		drawPosition.X -= FontAssets.MouseText.Value.MeasureString(contactText).X * GeneralScale * 0.5f;
		drawPosition.Y += GeneralScale * 20f;
		Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, contactText, drawPosition.X, drawPosition.Y, contactTextColor, Color.Black, Vector2.Zero, GeneralScale);
	}

	public static void HandleDraedonTalkButton(Vector2 drawPosition)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		Texture2D communicateButton = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/CommunicateIcon", (AssetRequestMode)2).Value;
		Rectangle clickArea = Utils.CenteredRectangle(drawPosition, communicateButton.Size() * VerificationButtonScale);
		Rectangle mouseScreenArea = MouseScreenArea;
		if (((Rectangle)(ref mouseScreenArea)).Intersects(clickArea))
		{
			CommunicateButtonScale = MathHelper.Clamp(CommunicateButtonScale + 0.035f, 1f, 1.35f);
			if (Main.mouseLeft && Main.mouseLeftRelease)
			{
				DisplayingCommunicationText = true;
			}
		}
		else
		{
			CommunicateButtonScale = MathHelper.Clamp(CommunicateButtonScale - 0.05f, 1f, 1.35f);
		}
		Main.spriteBatch.Draw(communicateButton, drawPosition, (Rectangle?)null, Color.White, 0f, communicateButton.Size() * 0.5f, CommunicateButtonScale * GeneralScale, (SpriteEffects)0, 0f);
		string communicateText = CalamityUtils.GetTextValue("UI.Communicate");
		drawPosition.X -= FontAssets.MouseText.Value.MeasureString(communicateText).X * GeneralScale * 0.5f;
		drawPosition.Y += GeneralScale * 20f;
		Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, communicateText, drawPosition.X, drawPosition.Y, Draedon.TextColor, Color.Black, Vector2.Zero, GeneralScale);
	}

	public static void DisplayNotStrongEnoughErrorText(Vector2 drawPosition)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		string text = CalamityUtils.GetTextValue("UI.UpgradesRequired");
		Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, text, drawPosition.X, drawPosition.Y, Color.IndianRed * ((float)(int)Main.mouseTextColor / 255f), Color.Black, Vector2.Zero, GeneralScale);
	}
}
