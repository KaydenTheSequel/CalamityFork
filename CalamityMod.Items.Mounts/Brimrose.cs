using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Mounts;

public class Brimrose : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Mounts";

	public override void SetDefaults()
	{
		base.Item.width = 64;
		base.Item.height = 64;
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item3;
		base.Item.noMelee = true;
		base.Item.mountType = ModContent.MountType<BrimroseChair>();
		base.Item.value = Item.sellPrice(0, 5);
		base.Item.rare = 8;
		base.Item.Calamity().devItem = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<UnholyCore>(5).AddIngredient<Bloodstone>(20).AddTile(134)
			.Register();
	}
}
