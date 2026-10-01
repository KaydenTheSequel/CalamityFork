using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Tiles.Abyss;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Abyss;

public class SulphurousSand : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<SulphurousShale>();
		ItemID.Sets.SandgunAmmoProjectileData[base.Type] = new ItemID.Sets.SandgunAmmoInfo(ModContent.ProjectileType<SulphurousSandBallGun>());
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.Abyss.SulphurousSand>());
		base.Item.ammo = AmmoID.Sand;
		base.Item.notAmmo = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SulphurousSandWall>(4).AddTile(18).DisableDecraft()
			.Register();
	}
}
