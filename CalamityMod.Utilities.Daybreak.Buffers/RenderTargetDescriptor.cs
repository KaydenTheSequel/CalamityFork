using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Utilities.Daybreak.Buffers;

public readonly record struct RenderTargetDescriptor(SurfaceFormat Format, DepthFormat Depth, int MultiSampleCount, RenderTargetUsage Usage, bool GenerateMipmaps)
{
	public static RenderTargetDescriptor Default { get; } = new RenderTargetDescriptor((SurfaceFormat)0, (DepthFormat)0, 0, (RenderTargetUsage)0, GenerateMipmaps: false);

	public static RenderTargetDescriptor DefaultPreserveContents { get; } = new RenderTargetDescriptor((SurfaceFormat)0, (DepthFormat)0, 0, (RenderTargetUsage)1, GenerateMipmaps: false);

	public int MultiSampleCount { get; }

	public RenderTarget2D Create(GraphicsDevice device, int width, int height)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Expected O, but got Unknown
		return new RenderTarget2D(device, width, height, GenerateMipmaps, Format, Depth, MultiSampleCount, Usage);
	}

	public static RenderTargetDescriptor From(RenderTarget2D target)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return new RenderTargetDescriptor(((Texture)target).Format, target.DepthStencilFormat, target.MultiSampleCount, (RenderTargetUsage)0, ((Texture)target).LevelCount > 1);
	}

	[CompilerGenerated]
	public void Deconstruct(out SurfaceFormat Format, out DepthFormat Depth, out int MultiSampleCount, out RenderTargetUsage Usage, out bool GenerateMipmaps)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Expected I4, but got Unknown
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Expected I4, but got Unknown
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Expected I4, but got Unknown
		Format = (SurfaceFormat)(int)this.Format;
		Depth = (DepthFormat)(int)this.Depth;
		MultiSampleCount = this.MultiSampleCount;
		Usage = (RenderTargetUsage)(int)this.Usage;
		GenerateMipmaps = this.GenerateMipmaps;
	}
}
