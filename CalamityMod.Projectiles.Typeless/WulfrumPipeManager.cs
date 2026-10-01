using CalamityMod.Items.Tools;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Terraria;

namespace CalamityMod.Projectiles.Typeless;

public class WulfrumPipeManager : TemporaryTileManager
{
	public override int[] ManagedTypes => new int[1] { WulfrumScaffoldKit.PlacedTileType };

	public override TemporaryTile Setup(Point pos)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 3; i++)
		{
			Vector2 position = pos.ToWorldCoordinates();
			Vector2? velocity = Main.rand.NextVector2Circular(3f, 3f);
			float scale = Main.rand.NextFloat(0.4f, 0.7f);
			Dust.NewDustPerfect(position, 83, velocity, 0, default(Color), scale);
		}
		return new TemporaryTile(pos, this, WulfrumScaffoldKit.TileTime);
	}

	public override void UpdateEffect(TemporaryTile tile)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		if ((float)tile.timeleft < (float)WulfrumScaffoldKit.TileTime * 0.1f && Main.rand.NextBool(10))
		{
			Vector2 position = tile.position.ToWorldCoordinates();
			Vector2? velocity = Main.rand.NextVector2Circular(4f, 4f);
			float scale = Main.rand.NextFloat(0.4f, 1f);
			Dust.NewDustPerfect(position, 226, velocity, 0, default(Color), scale);
		}
	}
}
