using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class RaidersTalisman : ModItem, ILocalizedModType, IModType
{
	public const float RaiderBonus = 15f;

	public const int RaiderCooldown = 10;

	public static readonly SoundStyle StealthHitSound = new SoundStyle("CalamityMod/Sounds/Custom/RaidersTalismanStealthHit");

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 36;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().raiderTalisman = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(259, 5).AddIngredient(173, 20).AddTile(16)
			.Register();
	}
}
