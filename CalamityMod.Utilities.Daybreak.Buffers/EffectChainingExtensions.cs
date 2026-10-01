using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CalamityMod.Utilities.Daybreak.Buffers;

public static class EffectChainingExtensions
{
	internal static DrawWithEffectsScope DrawWithEffects(this SpriteBatch spriteBatch, RenderTargetPool pool, Point targetSize, bool preserveContents = true, Color? clearColor = null, RenderTargetDescriptor? descriptor = null, SpriteBatchParameters parameters = default(SpriteBatchParameters), params IEnumerable<EffectChainEntry> effects)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		return new DrawWithEffectsScope(spriteBatch, pool, targetSize, preserveContents, clearColor, descriptor, parameters, effects);
	}
}
