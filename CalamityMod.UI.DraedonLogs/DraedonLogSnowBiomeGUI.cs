using CalamityMod.Items.DraedonMisc;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace CalamityMod.UI.DraedonLogs;

public class DraedonLogSnowBiomeGUI : DraedonsLogGUI
{
	public override int TotalPages => 3;

	public override string GetTextByPage()
	{
		return CalamityUtils.GetTextValueFromModItem<DraedonsLogSnowBiome>("ContentPage" + (Page + 1));
	}

	public override Texture2D GetTextureByPage()
	{
		return (Texture2D)(Page switch
		{
			0 => ModContent.Request<Texture2D>("CalamityMod/UI/DraedonLogs/DraedonsLogIceBiome", (AssetRequestMode)2).Value, 
			1 => null, 
			_ => ModContent.Request<Texture2D>("CalamityMod/UI/DraedonLogs/DraedonsLogPermafrost", (AssetRequestMode)2).Value, 
		});
	}
}
