using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class AquaticEmblem : ModItem, ILocalizedModType, IModType
{
	public static int MaxDefenseBoost = 20;

	public static float MaxMoveSpeedReduction = 0.1f;

	public static int TimeToReachMaxBoost = CalamityUtils.SecondsToFrames(10);

	public new string LocalizationCategory => "Items.Accessories";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(TimeToReachMaxBoost.FramesToSeconds(), MaxDefenseBoost, MaxMoveSpeedReduction.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.accessory = true;
		base.Item.expert = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().aquaticEmblem = true;
		player.npcTypeNoAggro[65] = true;
		player.npcTypeNoAggro[220] = true;
		player.npcTypeNoAggro[64] = true;
		player.npcTypeNoAggro[67] = true;
		player.npcTypeNoAggro[221] = true;
		if (player.Calamity().countsAsAnyWet)
		{
			player.gills = true;
		}
	}
}
