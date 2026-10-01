using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Tiles.SunkenSea;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class PolypSandBallGun : SandBall
{
	public override string Texture => "CalamityMod/Projectiles/Typeless/SandBallPolyp";

	public override int TileType => ModContent.TileType<global::CalamityMod.Tiles.SunkenSea.PolypSand>();

	public override int ItemType => ModContent.ItemType<global::CalamityMod.Items.Placeables.SunkenSea.PolypSand>();

	public override int DustType => 32;

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = true;
		return true;
	}

	public override void SetDefaults()
	{
		base.SetDefaults();
		base.Projectile.penetrate = 1;
		base.Projectile.Calamity().conditionalHomingRange = 160f;
	}
}
