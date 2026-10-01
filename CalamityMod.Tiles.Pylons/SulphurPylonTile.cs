using CalamityMod.BiomeManagers;
using CalamityMod.Items.Placeables.Pylons;
using CalamityMod.Systems;
using CalamityMod.Tiles.BaseTiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Pylons;

public class SulphurPylonTile : BasePylonTile
{
	public override int AssociatedItem => ModContent.ItemType<SulphurPylon>();

	public override Color PylonMapColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.YellowGreen;
		}
	}

	public override Color DustColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.GreenYellow;
		}
	}

	public override Color LightColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(1f, 0.8f, 0f);
		}
	}

	public override NPCShop.Entry GetNPCShopEntry()
	{
		return new NPCShop.Entry(AssociatedItem, Condition.AnotherTownNPCNearby, CalamityConditions.InSulph);
	}

	public override bool ValidTeleportCheck_BiomeRequirements(TeleportPylonInfo pylonInfo, SceneMetrics sceneData)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		Point tilePos = pylonInfo.PositionInTiles.ToPoint();
		bool inSpace = (double)tilePos.Y <= Main.worldSurface * 0.35;
		bool inUnderground = (double)tilePos.Y >= Main.worldSurface;
		if (BiomeTileCounterSystem.SulphurTiles < 300 || BiomeTileCounterSystem.Layer1Tiles >= 200 || BiomeTileCounterSystem.Layer2Tiles >= 200 || BiomeTileCounterSystem.Layer3Tiles >= 200 || BiomeTileCounterSystem.Layer4Tiles >= 200)
		{
			if (SulphurousSeaBiome.IsInBiomePosition(tilePos) && !inSpace)
			{
				return !inUnderground;
			}
			return false;
		}
		return true;
	}
}
