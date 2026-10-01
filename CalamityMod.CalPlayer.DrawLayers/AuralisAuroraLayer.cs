using System.Linq;
using CalamityMod.Items.Dyes;
using CalamityMod.Items.Weapons.Ranged;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.DrawLayers;

public class AuralisAuroraLayer : PlayerDrawLayer
{
	public override Position GetDefaultPosition()
	{
		return new AfterParent(PlayerDrawLayers.BackAcc);
	}

	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		if (drawInfo.shadow != 0f)
		{
			return false;
		}
		Player drawPlayer = drawInfo.drawPlayer;
		if (drawPlayer.Calamity().auralisAuroraCounter >= 300)
		{
			return drawPlayer.Calamity().auralisAuroraCounter <= 1500;
		}
		return false;
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		Player drawPlayer = drawInfo.drawPlayer;
		drawPlayer.dye.Count((Item dyeItem) => dyeItem.type == ModContent.ItemType<ProfanedMoonlightDye>());
		drawInfo.DrawDataCache.AddRange(CalamityUtils.DrawAuroras(drawPlayer, 7f, 0.4f, CalamityUtils.ColorSwap(Auralis.blueColor, Auralis.greenColor, 3f)));
	}
}
