using CalamityMod.Tiles.MusicBoxes;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.MusicBoxes;

[LegacyName(new string[] { "DraedonsAmbienceMusicBox" })]
public class DraedonExoSelectMusicBox : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Placeables";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.CanGetPrefixes[base.Type] = false;
		ItemID.Sets.ShimmerTransformToItem[base.Type] = 576;
		MusicLoader.AddMusicBox(base.Mod, MusicLoader.GetMusicSlot(base.Mod, "Sounds/Music/DraedonExoSelect"), base.Type, ModContent.TileType<global::CalamityMod.Tiles.MusicBoxes.DraedonExoSelectMusicBox>());
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToMusicBox(ModContent.TileType<global::CalamityMod.Tiles.MusicBoxes.DraedonExoSelectMusicBox>());
	}
}
