using System.Collections.Generic;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.FluidSimulation;

public class FluidFieldState
{
	public RenderTargetLease PreviousState;

	public RenderTargetLease NextState;

	public Queue<PixelQueueValue> PendingChanges;

	public readonly int Size;

	public readonly SurfaceFormat FieldContents;

	public void SwapState()
	{
		Utils.Swap(ref PreviousState, ref NextState);
	}

	public FluidFieldState(int size, SurfaceFormat fieldContents = (SurfaceFormat)0)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		PendingChanges = new Queue<PixelQueueValue>();
		base._002Ector();
		if (!Main.dedServ)
		{
			Size = size;
			FieldContents = fieldContents;
			RenderTargetDescriptor descriptor = new RenderTargetDescriptor(FieldContents, (DepthFormat)2, 0, (RenderTargetUsage)1, GenerateMipmaps: true);
			PreviousState = RenderTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, Size, Size, descriptor);
			NextState = RenderTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, Size, Size, descriptor);
		}
	}
}
