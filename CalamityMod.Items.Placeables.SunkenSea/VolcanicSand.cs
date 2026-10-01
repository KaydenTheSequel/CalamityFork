using CalamityMod.Projectiles.Typeless;
using CalamityMod.Tiles.SunkenSea;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.SunkenSea;

public class VolcanicSand : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<Dunesand>();
		ItemID.Sets.SandgunAmmoProjectileData[base.Type] = new ItemID.Sets.SandgunAmmoInfo(ModContent.ProjectileType<VolcanicSandBallGun>());
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.VolcanicSand>());
		base.Item.ammo = AmmoID.Sand;
		base.Item.notAmmo = true;
	}
}
