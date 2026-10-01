using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Face })]
public class FeatherCrown : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			int equipSlot = EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Face);
			ArmorIDs.Face.Sets.OverrideHelmet[equipSlot] = true;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 38;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.rogueVelocity += 0.15f;
		calamityPlayer.featherCrown = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyGoldCrown").AddIngredient<AerialiteBar>(6).AddIngredient(320, 8)
			.AddTile(16)
			.Register();
	}
}
