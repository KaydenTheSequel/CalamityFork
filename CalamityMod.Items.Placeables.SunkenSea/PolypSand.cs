using CalamityMod.Projectiles.Typeless;
using CalamityMod.Tiles.SunkenSea;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.SunkenSea;

public class PolypSand : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<VolcanicSand>();
		ItemID.Sets.SandgunAmmoProjectileData[base.Type] = new ItemID.Sets.SandgunAmmoInfo(ModContent.ProjectileType<PolypSandBallGun>());
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.PolypSand>());
		base.Item.ammo = AmmoID.Sand;
		base.Item.notAmmo = true;
	}
}
