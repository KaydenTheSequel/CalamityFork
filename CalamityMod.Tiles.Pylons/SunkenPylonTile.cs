using CalamityMod.Items.Placeables.Pylons;
using CalamityMod.Systems;
using CalamityMod.Tiles.BaseTiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Pylons;

public class SunkenPylonTile : BasePylonTile
{
	public override Color LightColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(0.2f, 0.8f, 1f);
		}
	}

	public override int AssociatedItem => ModContent.ItemType<SunkenPylon>();

	public override Color PylonMapColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.Turquoise;
		}
	}

	public override Color DustColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.Cyan;
		}
	}

	public override NPCShop.Entry GetNPCShopEntry()
	{
		return new NPCShop.Entry(AssociatedItem, Condition.AnotherTownNPCNearby, CalamityConditions.InSunken);
	}

	public override bool ValidTeleportCheck_BiomeRequirements(TeleportPylonInfo pylonInfo, SceneMetrics sceneData)
	{
		return BiomeTileCounterSystem.SunkenSeaTiles >= 100;
	}
}
