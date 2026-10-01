using CalamityMod.Rarities;
using CalamityMod.UI;
using CalamityMod.UI.DraedonLogs;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.DraedonMisc;

public class DraedonsLogPlanetoid : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.DraedonItems";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 28;
		base.Item.rare = ModContent.RarityType<DarkOrange>();
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.useStyle = 4;
	}

	public override bool? UseItem(Player player)
	{
		if (Main.myPlayer == player.whoAmI)
		{
			PopupGUIManager.FlipActivityOfGUIWithType(typeof(DraedonLogPlanetoidGUI));
		}
		return true;
	}
}
