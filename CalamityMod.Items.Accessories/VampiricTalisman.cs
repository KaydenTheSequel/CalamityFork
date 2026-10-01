using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class VampiricTalisman : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	internal const int ArmorCrunchDebuffTime = 150;

	internal const int HeavyBleedingDebuffTime = 300;

	public const float RaiderBonus = 15f;

	public bool ShowExtensionIndicator => false;

	public string TooltipExtensionKey => "YearningForBlood";

	public Color? TooltipExtensionColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.Red;
		}
	}

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 86;
		base.Item.height = 48;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = 7;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.vampiricTalisman = true;
		calamityPlayer.raiderTalisman = true;
		calamityPlayer.rottenDogTooth = true;
		if (Main.zenithWorld)
		{
			player.lifeRegen -= 10;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<RaidersTalisman>().AddIngredient<RottenDogtooth>().AddIngredient<SolarVeil>(10)
			.AddIngredient(1520)
			.AddTile(134)
			.Register();
	}
}
