using CalamityMod.Items.Placeables.Pylons;
using CalamityMod.Systems;
using CalamityMod.Tiles.BaseTiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Pylons;

public class CragsPylonTile : BasePylonTile
{
	public override Color LightColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(1f, 0.3f, 0f);
		}
	}

	public override int AssociatedItem => ModContent.ItemType<CragsPylon>();

	public override Color PylonMapColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.OrangeRed;
		}
	}

	public override Color DustColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.OrangeRed;
		}
	}

	public override NPCShop.Entry GetNPCShopEntry()
	{
		return new NPCShop.Entry(AssociatedItem, CalamityConditions.InCrag);
	}

	public override bool ValidTeleportCheck_NPCCount(TeleportPylonInfo pylonInfo, int defaultNecessaryNPCCount)
	{
		return true;
	}

	public override bool ValidTeleportCheck_BiomeRequirements(TeleportPylonInfo pylonInfo, SceneMetrics sceneData)
	{
		return BiomeTileCounterSystem.BrimstoneCragTiles >= 500;
	}
}
