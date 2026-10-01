using System;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.DrawLayers;

public class MountsAboveOwnerLayer : PlayerDrawLayer
{
	public override Position GetDefaultPosition()
	{
		return new AfterParent(PlayerDrawLayers.BackAcc);
	}

	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		Player drawPlayer = drawInfo.drawPlayer;
		CalamityPlayer modPlayer = drawPlayer.Calamity();
		if (drawPlayer.mount != null)
		{
			if (!modPlayer.crysthamyr)
			{
				return modPlayer.onyxExcavator;
			}
			return true;
		}
		return false;
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		if (drawInfo.drawPlayer.dead || !drawInfo.drawPlayer.active)
		{
			return;
		}
		try
		{
			drawInfo.drawPlayer.mount.Draw(drawInfo.DrawDataCache, 3, drawInfo.drawPlayer, drawInfo.Center - drawInfo.drawPlayer.Size * 0.5f, drawInfo.colorMount, drawInfo.playerEffect, drawInfo.shadow);
		}
		catch (IndexOutOfRangeException)
		{
		}
	}
}
