using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Shield })]
public class AsgardianAegis : ModItem, ILocalizedModType, IModType
{
	public const int ShieldSlamDamage = 1000;

	public const float ShieldSlamKnockback = 15f;

	public const int ShieldSlamIFrames = 12;

	public const int RamExplosionDamage = 300;

	public const float RamExplosionKnockback = 20f;

	public new string LocalizationCategory => "Items.Accessories";

	public Color? TooltipExtensionColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(195, 223, 255);
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 60;
		base.Item.height = 54;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.defense = 6;
		base.Item.accessory = true;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().DashID = AsgardianAegisDash.ID;
		player.dashType = 0;
		player.noKnockback = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AsgardsValor>().AddIngredient<ElysianAegis>().AddIngredient<CosmiliteBar>(10)
			.AddIngredient<AscendantSpiritEssence>(4)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
