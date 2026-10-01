using System;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod;

[Obsolete("Use SpriteBatchParameters/SpriteBatchSnapshot")]
public class BatchSetting(BlendState blend, SamplerState sampler, DepthStencilState depthStencil, RasterizerState rasterizer)
{
	public readonly BlendState blendState = blend;

	public readonly SamplerState samplerState = sampler;

	public readonly DepthStencilState depthStencilState = depthStencil;

	public readonly RasterizerState rasterizerState = rasterizer;

	public static readonly BatchSetting AlphaBlend = new BatchSetting(BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, null);

	public static readonly BatchSetting Additive = new BatchSetting(BlendState.Additive, SamplerState.PointClamp, DepthStencilState.None, null);

	public static readonly BatchSetting NonPremultiplied = new BatchSetting(BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, null);

	public static readonly BatchSetting Opaque = new BatchSetting(BlendState.Opaque, SamplerState.PointClamp, DepthStencilState.None, null);
}
