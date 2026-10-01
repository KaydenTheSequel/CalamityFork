using System.Linq;
using CalamityMod.Items.Dyes;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.DrawLayers;

public class AngelicAllianceAuroraLayer : PlayerDrawLayer
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
		return drawInfo.drawPlayer.Calamity().divineBless;
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		Player drawPlayer = drawInfo.drawPlayer;
		drawPlayer.dye.Count((Item dyeItem) => dyeItem.type == ModContent.ItemType<ProfanedMoonlightDye>());
		drawInfo.DrawDataCache.AddRange(CalamityUtils.DrawAuroras(drawPlayer, 7f, 0.4f, CalamityUtils.ColorSwap(new Color(255, 163, 56), new Color(242, 48, 187), 3f)));
	}
}
