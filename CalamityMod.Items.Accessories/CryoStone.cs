using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class CryoStone : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(4, 4));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 40;
		base.Item.height = 40;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().CryoStone = true;
		player.Calamity().ColdDebuffMultiplier += 0.5f;
	}

	public override void UpdateVanity(Player player)
	{
		player.Calamity().CryoStoneVanity = true;
	}
}
