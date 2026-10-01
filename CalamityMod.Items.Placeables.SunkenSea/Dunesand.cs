using CalamityMod.Projectiles.Typeless;
using CalamityMod.Tiles.SunkenSea;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.SunkenSea;

[LegacyName(new string[] { "RuneSand" })]
public class Dunesand : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = ModContent.ItemType<EutrophicSand>();
		ItemID.Sets.SandgunAmmoProjectileData[base.Type] = new ItemID.Sets.SandgunAmmoInfo(ModContent.ProjectileType<DuneSandBallGun>(), 5);
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.Dunesand>());
		base.Item.ammo = AmmoID.Sand;
		base.Item.notAmmo = true;
	}
}
