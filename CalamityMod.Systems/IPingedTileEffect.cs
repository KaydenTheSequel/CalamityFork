using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;

namespace CalamityMod.Systems;

public interface IPingedTileEffect
{
	BlendState BlendState => BlendState.AlphaBlend;

	bool Active => true;

	Effect SetupEffect();

	void PerTileSetup(Point pos, ref Effect effect)
	{
	}

	void DrawTile(Point pos);

	bool TryAddPing(Vector2 position, Player pinger);

	bool ShouldRegisterTile(int x, int y);

	void ModifyTileLight(int x, int y, Color tileLight, ref Color resultColor);

	void EditDrawData(int i, int j, ref TileDrawInfo drawData)
	{
	}

	void UpdateEffect()
	{
	}
}
