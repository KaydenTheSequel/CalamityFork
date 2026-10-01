using System;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Utilities.Daybreak.Buffers;

public sealed class RenderTargetLease(RenderTarget2D target, RenderTargetPool pool) : IDisposable
{
	public RenderTarget2D Target { get; set; } = target;

	public void Dispose()
	{
		pool.Return(this);
	}
}
