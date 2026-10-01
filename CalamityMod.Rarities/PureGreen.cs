using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Rarities;

public class PureGreen : ModRarity
{
	public override Color RarityColor
	{
		get
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			return new Color(0, 255, 0);
		}
	}

	public override int GetPrefixedRarity(int offset, float valueMult)
	{
		return offset switch
		{
			-2 => 11, 
			-1 => ModContent.RarityType<Turquoise>(), 
			_ => base.Type, 
		};
	}
}
