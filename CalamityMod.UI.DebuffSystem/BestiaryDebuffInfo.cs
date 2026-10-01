using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.Bestiary;
using Terraria.GameContent.UI.Elements;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace CalamityMod.UI.DebuffSystem;

public class BestiaryDebuffInfo : IBestiaryInfoElement, IBestiaryPrioritizedElement, ICategorizedBestiaryInfoElement
{
	public string[] elements;

	public bool force;

	public float OrderPriority => -1f;

	public UIBestiaryEntryInfoPage.BestiaryInfoCategory ElementCategory => UIBestiaryEntryInfoPage.BestiaryInfoCategory.Stats;

	public BestiaryDebuffInfo(string[] elements, bool force = false)
	{
		this.elements = elements;
		this.force = force;
	}

	public UIElement ProvideUIElement(BestiaryUICollectionInfo info)
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		if (info.UnlockState < BestiaryEntryUnlockState.CanShowStats_2 && !force)
		{
			return null;
		}
		UIPanel backgroundPanel = new UIPanel(Main.Assets.Request<Texture2D>("Images/UI/Bestiary/Stat_Panel", (AssetRequestMode)2), null, 12, 7)
		{
			Width = new StyleDimension(-11f, 1f),
			Height = new StyleDimension(180f, 0f),
			BackgroundColor = new Color(43, 56, 101),
			BorderColor = Color.Transparent,
			Left = new StyleDimension(2.5f, 0f),
			PaddingLeft = 4f,
			PaddingRight = 4f
		};
		UIText titleText = new UIText(CalamityUtils.GetTextValue("UI.DebuffSystem.Title"))
		{
			HAlign = 0f,
			VAlign = 0f,
			Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
			Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
			IsWrapped = true
		};
		backgroundPanel.Append(titleText);
		for (int i = 0; i < 5; i++)
		{
			string path = "CalamityMod/UI/DebuffSystem/";
			switch (i)
			{
			case 0:
				path += "Cold";
				break;
			case 1:
				path += "Electricity";
				break;
			case 2:
				path += "Heat";
				break;
			case 3:
				path += "Sickness";
				break;
			case 4:
				path += "Water";
				break;
			}
			path += "DebuffType";
			float topPos = 0.2f + (float)i * 0.175f;
			UIImage elementImage = new UIImage(ModContent.Request<Texture2D>(path, (AssetRequestMode)2))
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Top = new StyleDimension(0f, topPos - 0.0525f),
				Left = new StyleDimension(8f, 0f),
				ImageScale = 0.8f
			};
			backgroundPanel.Append(elementImage);
			UIText elementText = new UIText(Language.GetText(elements[i]), 0.8f)
			{
				HAlign = 0f,
				VAlign = 0f,
				Width = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Height = StyleDimension.FromPixelsAndPercent(0f, 1f),
				Top = new StyleDimension(0f, topPos),
				Left = new StyleDimension(0f, 0.05f)
			};
			backgroundPanel.Append(elementText);
		}
		backgroundPanel.OnUpdate += ElementDescription;
		return backgroundPanel;
	}

	public static void ElementDescription(UIElement uelement)
	{
		if (uelement.IsMouseHovering)
		{
			Main.instance.MouseText(CalamityUtils.GetTextValue("UI.DebuffSystem.Description"), 0, 0);
		}
	}
}
