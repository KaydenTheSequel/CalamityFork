using CalamityMod.Projectiles.Typeless;
using CalamityMod.Tiles.AstralDesert;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Astral;

public class AstralSand : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 100;
		ItemTrader.ChlorophyteExtractinator.AddOption_OneWay(base.Type, 1, 169, 1);
		ItemID.Sets.SandgunAmmoProjectileData[base.Type] = new ItemID.Sets.SandgunAmmoInfo(ModContent.ProjectileType<AstralSandBallGun>());
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<global::CalamityMod.Tiles.AstralDesert.AstralSand>());
		base.Item.ammo = AmmoID.Sand;
		base.Item.notAmmo = true;
	}
}
