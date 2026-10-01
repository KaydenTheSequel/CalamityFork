using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories.Vanity;

public class WulfrumTransformationPlayer : ModPlayer
{
	public bool vanityEquipped;

	public override void ResetEffects()
	{
		vanityEquipped = false;
	}
}
