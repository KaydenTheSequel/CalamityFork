using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class HeartofDarkness : ModItem, ILocalizedModType, IModType
{
	public const float RagePerSecond = 0.02f;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(6, 5));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<StressPills>();
	}

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 66;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.accessory = true;
		base.Item.SetRevExclusive();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().heartOfDarkness = true;
	}
}
