using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Utilities.Daybreak;

internal static class SpriteBatchScopeExtensions
{
	public static SpriteBatchScope Scope(this SpriteBatch @this)
	{
		return new SpriteBatchScope(@this);
	}
}
