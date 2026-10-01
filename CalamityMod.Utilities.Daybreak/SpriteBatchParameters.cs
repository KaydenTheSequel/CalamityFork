using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Utilities.Daybreak;

internal struct SpriteBatchParameters
{
	public SpriteSortMode? SortMode { get; set; }

	public BlendState? BlendState { get; set; }

	public SamplerState? SamplerState { get; set; }

	public DepthStencilState? DepthStencilState { get; set; }

	public RasterizerState? RasterizerState { get; set; }

	public Effect? CustomEffect { get; set; }

	public Matrix? TransformMatrix { get; set; }

	public SpriteBatchParameters(SpriteSortMode? sortMode = null, BlendState? blendState = null, SamplerState? samplerState = null, DepthStencilState? depthStencilState = null, RasterizerState? rasterizerState = null, Effect? customEffect = null, Matrix? transformMatrix = null)
	{
		SortMode = sortMode;
		BlendState = blendState;
		SamplerState = samplerState;
		DepthStencilState = depthStencilState;
		RasterizerState = rasterizerState;
		CustomEffect = customEffect;
		TransformMatrix = transformMatrix;
	}

	public readonly SpriteBatchSnapshot ToSnapshot(SpriteBatchSnapshot defaultValues)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		return new SpriteBatchSnapshot((SpriteSortMode)(((_003F?)SortMode) ?? defaultValues.SortMode), BlendState ?? defaultValues.BlendState, SamplerState ?? defaultValues.SamplerState, DepthStencilState ?? defaultValues.DepthStencilState, RasterizerState ?? defaultValues.RasterizerState, CustomEffect ?? defaultValues.CustomEffect, (Matrix)(((_003F?)TransformMatrix) ?? defaultValues.TransformMatrix));
	}
}
