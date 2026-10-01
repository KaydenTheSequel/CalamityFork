using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.Rarities;

public class Turquoise : ModRarity
{
	public override Color RarityColor
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return new Color(0, 255, 200);
		}
	}

	public override int GetPrefixedRarity(int offset, float valueMult)
	{
		return offset switch
		{
			-2 => 10, 
			-1 => 11, 
			1 => ModContent.RarityType<PureGreen>(), 
			2 => ModContent.RarityType<PureGreen>(), 
			_ => base.Type, 
		};
	}
}
