using CalamityMod.Items.DraedonMisc;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria.ModLoader;

namespace CalamityMod.UI.DraedonLogs;

public class DraedonLogJungleGUI : DraedonsLogGUI
{
	public override int TotalPages => 3;

	public override string GetTextByPage()
	{
		return CalamityUtils.GetTextValueFromModItem<DraedonsLogJungle>("ContentPage" + (Page + 1));
	}

	public override Texture2D GetTextureByPage()
	{
		return (Texture2D)(Page switch
		{
			0 => ModContent.Request<Texture2D>("CalamityMod/UI/DraedonLogs/DraedonsLogJungleBiome", (AssetRequestMode)2).Value, 
			1 => ModContent.Request<Texture2D>("CalamityMod/UI/DraedonLogs/DraedonsLogPlagueCell", (AssetRequestMode)2).Value, 
			_ => ModContent.Request<Texture2D>("CalamityMod/UI/DraedonLogs/DraedonsLogPlaguebringerGoliath", (AssetRequestMode)2).Value, 
		});
	}
}
