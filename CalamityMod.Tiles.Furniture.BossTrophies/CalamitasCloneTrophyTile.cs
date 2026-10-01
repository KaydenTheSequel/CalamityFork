using Terraria.ModLoader;

namespace CalamityMod.Tiles.Furniture.BossTrophies;

[LegacyName(new string[] { "CalamitasTrophyTile" })]
public class CalamitasCloneTrophyTile : ModTile
{
	public override void SetStaticDefaults()
	{
		this.SetUpTrophy();
	}
}
