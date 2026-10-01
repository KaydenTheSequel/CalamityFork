using CalamityMod.Items.Materials;
using CalamityMod.Tiles.LivingFire;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.LivingFire;

public class LivingHolyFireBlock : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<LivingHolyFireBlockTile>());
	}

	public override void PostUpdate()
	{
		Lighting.AddLight((int)((base.Item.position.X + (float)(base.Item.width / 2)) / 16f), (int)((base.Item.position.Y + (float)(base.Item.height / 2)) / 16f), 1f, 1f, 0f);
	}

	public override void AddRecipes()
	{
		CreateRecipe(20).AddIngredient(2701, 20).AddIngredient<UnholyEssence>().AddTile(125)
			.Register();
	}
}
