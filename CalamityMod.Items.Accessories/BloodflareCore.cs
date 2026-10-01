using CalamityMod.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class BloodflareCore : ModItem, ILocalizedModType, IModType
{
	internal static readonly int HealFrameCooldown = 6;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().bloodflareCore = true;
		player.noKnockback = true;
	}
}
