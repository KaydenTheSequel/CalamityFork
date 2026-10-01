using CalamityMod.Enums;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Graphics.Primitives;

public interface IPixelatedPrimitiveRenderer
{
	GeneralDrawLayer LayerToRenderTo => GeneralDrawLayer.BeforeProjectiles;

	void RenderPixelatedPrimitives(SpriteBatch spriteBatch, GeneralDrawLayer layer);
}
