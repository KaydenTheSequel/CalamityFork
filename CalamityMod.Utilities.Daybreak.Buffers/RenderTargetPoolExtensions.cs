using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Utilities.Daybreak.Buffers;

internal static class RenderTargetPoolExtensions
{
	extension(RenderTargetPool pool)
	{
		public RenderTargetLease Rent(GraphicsDevice device, int width, int height)
		{
			return pool.Rent(device, width, height, RenderTargetDescriptor.Default);
		}

		public RenderTargetLease RentScaled(GraphicsDevice device, Point baseSize, float scale)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			return pool.RentScaled(device, baseSize, scale, RenderTargetDescriptor.Default);
		}

		public RenderTargetLease RentScaled(GraphicsDevice device, Point baseSize, float scale, RenderTargetDescriptor descriptor)
		{
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(scale, 0f, "scale");
			int width = Math.Max(1, (int)MathF.Ceiling((float)baseSize.X * scale));
			int height = Math.Max(1, (int)MathF.Ceiling((float)baseSize.Y * scale));
			return pool.Rent(device, width, height, descriptor);
		}
	}
}
