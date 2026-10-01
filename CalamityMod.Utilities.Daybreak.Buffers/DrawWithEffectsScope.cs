using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Utilities.Daybreak.Buffers;

public sealed class DrawWithEffectsScope : IDisposable
{
	private static readonly SpriteBatchSnapshot default_target_snapshot;

	private static readonly SpriteBatchSnapshot effect_target_snapshot;

	private readonly SpriteBatch spriteBatch;

	private readonly GraphicsDevice graphicsDevice;

	private readonly RenderTargetPool pool;

	private readonly IEnumerable<EffectChainEntry> effects;

	private readonly RenderTargetDescriptor renderDesc;

	private readonly int width;

	private readonly int height;

	private readonly SpriteBatchScope sbScope;

	private readonly RenderTargetScope rtScope;

	public RenderTargetLease Lease { get; private set; }

	internal DrawWithEffectsScope(SpriteBatch spriteBatch, RenderTargetPool pool, Point targetSize, bool preserveContents = true, Color? clearColor = null, RenderTargetDescriptor? descriptor = null, SpriteBatchParameters parameters = default(SpriteBatchParameters), params IEnumerable<EffectChainEntry> effects)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		ArgumentNullException.ThrowIfNull(spriteBatch, "spriteBatch");
		ArgumentNullException.ThrowIfNull(pool, "pool");
		ArgumentOutOfRangeException.ThrowIfLessThan(targetSize.X, 1, "targetSize.X");
		ArgumentOutOfRangeException.ThrowIfLessThan(targetSize.Y, 1, "targetSize.Y");
		this.spriteBatch = spriteBatch;
		graphicsDevice = ((GraphicsResource)spriteBatch).graphicsDevice;
		this.pool = pool;
		this.effects = effects;
		renderDesc = descriptor ?? RenderTargetDescriptor.Default;
		width = Math.Max(1, targetSize.X);
		height = Math.Max(1, targetSize.Y);
		Lease = pool.Rent(graphicsDevice, width, height, renderDesc);
		sbScope = spriteBatch.Scope();
		rtScope = Lease.Scope(preserveContents, clearColor);
		spriteBatch.Begin(parameters.ToSnapshot(default_target_snapshot));
	}

	public void Dispose()
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		spriteBatch.End();
		rtScope.Dispose();
		foreach (EffectChainEntry effect in effects)
		{
			RenderTargetLease nextLease = pool.Rent(graphicsDevice, width, height, renderDesc);
			using (nextLease.Scope(preserveContents: true, Color.Transparent))
			{
				spriteBatch.Begin(effect.Parameters.ToSnapshot(effect_target_snapshot));
				effect.ApplyEffect();
				spriteBatch.Draw((Texture2D)(object)Lease.Target, Vector2.Zero, Color.White);
				spriteBatch.End();
			}
			Lease.Dispose();
			Lease = nextLease;
		}
		sbScope.Dispose();
	}

	static DrawWithEffectsScope()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		default_target_snapshot = new SpriteBatchSnapshot((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.Identity);
		effect_target_snapshot = new SpriteBatchSnapshot((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, RasterizerState.CullCounterClockwise, null, Matrix.Identity);
	}
}
