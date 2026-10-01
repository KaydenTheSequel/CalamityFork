using CalamityMod.Items.Materials;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Wings;

[AutoloadEquip(new EquipType[] { EquipType.Wings })]
public class SkylineWings : BaseWings
{
	public override void SetStaticDefaults()
	{
		ArmorIDs.Wing.Sets.Stats[base.Item.wingSlot] = new WingStats(80, 6.25f);
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Item.width = 22;
		base.Item.height = 20;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AerialiteBar>(5).AddIngredient(824, 5).AddIngredient(320, 5)
			.AddTile(16)
			.Register();
	}
}
