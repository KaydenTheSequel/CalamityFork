using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Rarities;

public sealed class ColorTool : GlobalItem
{
	public static int RarityCosmicPurple => ModContent.GetInstance<CosmicPurple>().Type;

	public static int RarityBurnishedAuric => ModContent.GetInstance<BurnishedAuric>().Type;

	public static int RarityCalamityRed => ModContent.GetInstance<CalamityRed>().Type;

	public override bool PreDrawTooltipLine(Item Item, DrawableTooltipLine line, ref int yOffset)
	{
		if (line.Mod == "Terraria" && line.Name == "ItemName" && CalamityClientConfig.Instance.TextEffects)
		{
			if (Item.rare == RarityCosmicPurple)
			{
				CosmicPurple.Draw(Item, line);
				return false;
			}
			if (Item.rare == RarityBurnishedAuric)
			{
				BurnishedAuric.Draw(Item, line);
				return false;
			}
			if (Item.rare == RarityCalamityRed)
			{
				CalamityRed.Draw(Item, line);
				return false;
			}
		}
		return true;
	}

	public static Color colorLerps(Color[] colors, float time)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		int index = (int)time;
		return Color.Lerp(colors[index % colors.Length], colors[(index + 1) % colors.Length], time % 1f);
	}

	public static Color Rainbowing(float position)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		return colorLerps((Color[])(object)new Color[7]
		{
			new Color(255, 50, 50, 255),
			new Color(255, 128, 50, 255),
			new Color(230, 255, 50, 255),
			new Color(80, 255, 60, 255),
			new Color(50, 80, 250, 255),
			new Color(200, 50, 250, 255),
			new Color(255, 50, 230, 255)
		}, position);
	}
}
