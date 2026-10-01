using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.DrawLayers;

public class ProvidenceBurnFireLayer : PlayerDrawLayer
{
	public override Position GetDefaultPosition()
	{
		return new AfterParent(PlayerDrawLayers.BackAcc);
	}

	public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
	{
		return drawInfo.shadow == 0f;
	}

	protected override void Draw(ref PlayerDrawSet drawInfo)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		if (drawInfo.shadow == 0f)
		{
			Player drawPlayer = drawInfo.drawPlayer;
			CalamityPlayer calamityPlayer = drawPlayer.Calamity();
			calamityPlayer.ProvidenceBurnEffectDrawer.DrawSet(drawPlayer.Bottom - Vector2.UnitY * 10f);
			calamityPlayer.ProvidenceBurnEffectDrawer.SpawnAreaCompactness = 18f;
			calamityPlayer.ProvidenceBurnEffectDrawer.RelativePower = 0.4f;
		}
	}
}
