using CalamityMod.Items.DraedonMisc;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace CalamityMod.UI.DraedonLogs;

public class DraedonLogSunkenSeaGUI : DraedonsLogGUI
{
	public override int TotalPages => 3;

	public override string GetTextByPage()
	{
		return CalamityUtils.GetTextValueFromModItem<DraedonsLogSunkenSea>("ContentPage" + (Page + 1));
	}

	public override Texture2D GetTextureByPage()
	{
		return (Texture2D)(Page switch
		{
			0 => ModContent.Request<Texture2D>("CalamityMod/UI/DraedonLogs/DraedonsLogSunkenSeaBiome", (AssetRequestMode)2).Value, 
			1 => ModContent.Request<Texture2D>("CalamityMod/UI/DraedonLogs/DraedonsLogEutrophicRayGhostBell", (AssetRequestMode)2).Value, 
			_ => ModContent.Request<Texture2D>("CalamityMod/UI/DraedonLogs/DraedonsLogGiantClam", (AssetRequestMode)2).Value, 
		});
	}
}
