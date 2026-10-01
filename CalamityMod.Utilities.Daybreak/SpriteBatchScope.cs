using System;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Utilities.Daybreak;

internal readonly struct SpriteBatchScope : IDisposable
{
	private readonly SpriteBatch spriteBatch;

	private readonly SpriteBatchSnapshot? oldState;

	public SpriteBatchScope(SpriteBatch spriteBatch)
	{
		oldState = null;
		this.spriteBatch = spriteBatch;
		if (spriteBatch.beginCalled)
		{
			spriteBatch.End(out var old);
			oldState = old;
		}
	}

	public void Dispose()
	{
		if (oldState.HasValue)
		{
			spriteBatch.Begin(oldState.Value);
		}
	}
}
