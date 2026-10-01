using CalamityMod.Items.Placeables.Walls;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Tiles.SunkenSea;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.SunkenSea;

public class EutrophicSand : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<PolypSand>();
		ItemID.Sets.SandgunAmmoProjectileData[base.Type] = new ItemID.Sets.SandgunAmmoInfo(ModContent.ProjectileType<EutrophicSandBallGun>(), -5);
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.EutrophicSand>());
		base.Item.ammo = AmmoID.Sand;
		base.Item.notAmmo = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<EutrophicSandWallSafe>(4).AddTile(18).Register();
	}
}
