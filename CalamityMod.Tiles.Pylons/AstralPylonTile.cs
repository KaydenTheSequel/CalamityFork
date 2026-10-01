using CalamityMod.Items.Placeables.Pylons;
using CalamityMod.Systems;
using CalamityMod.Tiles.BaseTiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Pylons;

public class AstralPylonTile : BasePylonTile
{
	public override Color LightColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(0.8f, 0.5f, 0.8f);
		}
	}

	public override int AssociatedItem => ModContent.ItemType<AstralPylon>();

	public override Color PylonMapColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Color.Coral;
		}
	}

	public override Color DustColor
	{
		get
		{
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			if (!Main.rand.NextBool())
			{
				return Color.MediumTurquoise;
			}
			return Color.Coral;
		}
	}

	public override NPCShop.Entry GetNPCShopEntry()
	{
		return new NPCShop.Entry(AssociatedItem, Condition.AnotherTownNPCNearby, CalamityConditions.InAstral);
	}

	public override bool ValidTeleportCheck_BiomeRequirements(TeleportPylonInfo pylonInfo, SceneMetrics sceneData)
	{
		return BiomeTileCounterSystem.AstralTiles >= 100;
	}
}
