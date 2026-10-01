using CalamityMod.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class VeneratedLocket : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 50;
		base.Item.height = 58;
		base.Item.value = Item.buyPrice(10);
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage<ThrowingDamageClass>() += 0.1f;
		player.Calamity().veneratedLocket = true;
	}
}
