using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class PermafrostsConcoction : ModItem, ILocalizedModType, IModType
{
	internal static readonly int EncasedIFrames = 90;

	public static int EncasedDefenseBoost = 30;

	public static float EncasedDamageReductionBoost = 0.3f;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 34;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().permafrostsConcoction = true;
	}
}
