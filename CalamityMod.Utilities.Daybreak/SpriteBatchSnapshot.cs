using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Utilities.Daybreak;

internal struct SpriteBatchSnapshot
{
	[CompilerGenerated]
	private SpriteSortMode _003CSortMode_003Ek__BackingField;

	[CompilerGenerated]
	private Matrix _003CTransformMatrix_003Ek__BackingField;

	public SpriteSortMode SortMode
	{
		[CompilerGenerated]
		readonly get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _003CSortMode_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_003CSortMode_003Ek__BackingField = value;
		}
	}

	public BlendState BlendState { get; set; }

	public SamplerState SamplerState { get; set; }

	public DepthStencilState DepthStencilState { get; set; }

	public RasterizerState RasterizerState { get; set; }

	public Effect? CustomEffect { get; set; }

	public Matrix TransformMatrix
	{
		[CompilerGenerated]
		readonly get
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			return _003CTransformMatrix_003Ek__BackingField;
		}
		[CompilerGenerated]
		set
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			_003CTransformMatrix_003Ek__BackingField = value;
		}
	}

	public SpriteBatchSnapshot(SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect? customEffect, Matrix transformMatrix)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		SortMode = sortMode;
		BlendState = blendState;
		SamplerState = samplerState;
		DepthStencilState = depthStencilState;
		RasterizerState = rasterizerState;
		CustomEffect = customEffect;
		TransformMatrix = transformMatrix;
	}

	public SpriteBatchSnapshot(SpriteBatch spriteBatch)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		SortMode = spriteBatch.sortMode;
		BlendState = spriteBatch.blendState;
		SamplerState = spriteBatch.samplerState;
		DepthStencilState = spriteBatch.depthStencilState;
		RasterizerState = spriteBatch.rasterizerState;
		CustomEffect = spriteBatch.customEffect;
		TransformMatrix = spriteBatch.transformMatrix;
	}

	public readonly SpriteBatchParameters ToParameters()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		return new SpriteBatchParameters(SortMode, BlendState, SamplerState, DepthStencilState, RasterizerState, CustomEffect, TransformMatrix);
	}
}
