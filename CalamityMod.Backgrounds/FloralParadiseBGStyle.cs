using Terraria.ModLoader;

namespace CalamityMod.Backgrounds;

public class FloralParadiseBGStyle : ModUndergroundBackgroundStyle
{
	public override void FillTextureArray(int[] textureSlots)
	{
		for (int i = 0; i <= 3; i++)
		{
			textureSlots[i] = i + 153;
		}
	}
}
