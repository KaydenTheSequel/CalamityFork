using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Rubblemaker.Abyss;

public class AbyssGiantKelp2Echo : AbyssGiantKelp1Echo
{
	public override string Texture => "CalamityMod/Tiles/Abyss/AbyssAmbient/AbyssGiantKelp2";

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = Framing.GetTileSafely(i, j);
		if (!tile.IsTileActuallyInvisible())
		{
			if (GlowTexture == null)
			{
				GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Abyss/AbyssAmbient/AbyssGiantKelp2Glow", (AssetRequestMode)2);
			}
			Texture2D tex = GlowTexture.Value;
			Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange));
			spriteBatch.Draw(tex, new Vector2((float)(i * 16), (float)(j * 16 + 2)) - Main.screenPosition + zero, (Rectangle?)new Rectangle((int)tile.TileFrameX, (int)tile.TileFrameY, 16, 16), Color.White);
		}
	}
}
