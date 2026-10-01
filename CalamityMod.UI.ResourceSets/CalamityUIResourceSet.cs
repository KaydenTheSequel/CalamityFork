using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;

namespace CalamityMod.UI.ResourceSets;

public record CalamityUIResourceSet(Asset<Texture2D> Bar, Asset<Texture2D> Obj)
{
	public Asset<Texture2D> Heart => Obj;

	public Asset<Texture2D> Star => Obj;
}
