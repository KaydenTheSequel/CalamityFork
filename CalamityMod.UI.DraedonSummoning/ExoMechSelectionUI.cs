using CalamityMod.NPCs.ExoMechs;
using CalamityMod.Packets;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.UI.DraedonSummoning;

public static class ExoMechSelectionUI
{
	public static float DestroyerIconScale;

	public static float PrimeIconScale;

	public static float TwinsIconScale;

	public static readonly Color HoverTextColor;

	public static readonly SoundStyle ThanatosHoverSound;

	public static readonly SoundStyle AresHoverSound;

	public static readonly SoundStyle TwinsHoverSound;

	public static ExoMech? HoverSoundMechType { get; set; }

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

	public static void Draw()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		Vector2 drawAreaVerticalOffset = Vector2.UnitY * 105f;
		Vector2 baseDrawPosition = Main.LocalPlayer.Top + drawAreaVerticalOffset - Main.screenPosition;
		Vector2 destroyerIconDrawOffset = default(Vector2);
		((Vector2)(ref destroyerIconDrawOffset))._002Ector(-78f, -124f);
		Vector2 primeIconDrawOffset = default(Vector2);
		((Vector2)(ref primeIconDrawOffset))._002Ector(0f, -140f);
		Vector2 twinsIconDrawOffset = default(Vector2);
		((Vector2)(ref twinsIconDrawOffset))._002Ector(78f, -124f);
		if (!(HandleInteractionWithButton(baseDrawPosition + destroyerIconDrawOffset, ExoMech.Destroyer) | HandleInteractionWithButton(baseDrawPosition + primeIconDrawOffset, ExoMech.Prime) | HandleInteractionWithButton(baseDrawPosition + twinsIconDrawOffset, ExoMech.Twins)))
		{
			HoverSoundMechType = null;
		}
	}

	public static bool HandleInteractionWithButton(Vector2 drawPosition, ExoMech exoMech)
	{
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		float uiScale = Main.UIScale;
		Texture2D iconMechTexture;
		string description;
		SoundStyle hoverSound;
		float iconScale;
		switch (exoMech)
		{
		case ExoMech.Destroyer:
			iconScale = DestroyerIconScale;
			iconMechTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/HeadIcon_THanos", (AssetRequestMode)2).Value;
			description = CalamityUtils.GetTextValue("UI.ThanatosIcon");
			hoverSound = ThanatosHoverSound;
			break;
		case ExoMech.Prime:
			iconScale = PrimeIconScale;
			iconMechTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/HeadIcon_Ares", (AssetRequestMode)2).Value;
			description = CalamityUtils.GetTextValue("UI.AresIcon");
			hoverSound = AresHoverSound;
			break;
		default:
			iconScale = TwinsIconScale;
			iconMechTexture = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonSummoning/HeadIcon_ArtemisApollo", (AssetRequestMode)2).Value;
			description = CalamityUtils.GetTextValue("UI.ArtemisApolloIcon");
			hoverSound = TwinsHoverSound;
			break;
		}
		Rectangle clickArea = Utils.CenteredRectangle(drawPosition, iconMechTexture.Size() * iconScale * uiScale * 0.9f);
		Rectangle mouseScreenArea = MouseScreenArea;
		bool hoveringOverIcon = ((Rectangle)(ref mouseScreenArea)).Intersects(clickArea);
		if (hoveringOverIcon)
		{
			iconScale = MathHelper.Clamp(iconScale + 0.035f, 1f, 1.35f);
			if (HoverSoundMechType != exoMech)
			{
				HoverSoundMechType = exoMech;
				SoundStyle style = hoverSound with
				{
					Volume = 1.5f
				};
				SoundEngine.PlaySound(in style);
			}
			if (Main.mouseLeft && Main.mouseLeftRelease)
			{
				CalamityWorld.DraedonMechToSummon = exoMech;
				if (Main.netMode != 0)
				{
					ExoMechSelectionPacket.Send();
				}
			}
			Main.blockMouse = (Main.LocalPlayer.mouseInterface = true);
		}
		else
		{
			iconScale = MathHelper.Clamp(iconScale - 0.05f, 1f, 1.2f);
		}
		Main.spriteBatch.Draw(iconMechTexture, drawPosition, (Rectangle?)null, Color.White, 0f, iconMechTexture.Size() * 0.5f, iconScale * uiScale, (SpriteEffects)0, 0f);
		if (hoveringOverIcon)
		{
			drawPosition.X -= FontAssets.MouseText.Value.MeasureString(description).X * 0.5f * uiScale;
			drawPosition.Y += 36f * uiScale;
			Utils.DrawBorderStringFourWay(Main.spriteBatch, FontAssets.MouseText.Value, description, drawPosition.X, drawPosition.Y, HoverTextColor, Color.Black, Vector2.Zero, uiScale);
		}
		switch (exoMech)
		{
		case ExoMech.Destroyer:
			DestroyerIconScale = iconScale;
			break;
		case ExoMech.Prime:
			PrimeIconScale = iconScale;
			break;
		default:
			TwinsIconScale = iconScale;
			break;
		}
		return hoveringOverIcon;
	}

	static ExoMechSelectionUI()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		HoverSoundMechType = null;
		DestroyerIconScale = 1f;
		PrimeIconScale = 1f;
		TwinsIconScale = 1f;
		HoverTextColor = Draedon.TextColor;
		ThanatosHoverSound = new SoundStyle("CalamityMod/Sounds/Custom/Codebreaker/ThanatosIconHover");
		AresHoverSound = new SoundStyle("CalamityMod/Sounds/Custom/Codebreaker/AresIconHover");
		TwinsHoverSound = new SoundStyle("CalamityMod/Sounds/Custom/Codebreaker/ArtemisApolloIconHover");
	}
}
