using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.Graphics.Shaders;

namespace CalamityMod.Utilities.Daybreak.Buffers;

internal readonly record struct EffectChainEntry(Action ApplyEffect, SpriteBatchParameters Parameters = default(SpriteBatchParameters))
{
	public EffectChainEntry(ShaderData shader, SpriteBatchParameters parameters = default(SpriteBatchParameters))
		: this((Action)shader.Apply, parameters)
	{
	}

	public EffectChainEntry(Effect effect, SpriteBatchParameters parameters)
		: this(ApplyEffectFunc(effect), parameters)
	{
	}

	public static EffectChainEntry FromPlayerShader(Player player, PlayerShader shader, DrawData? drawData = null, SpriteBatchParameters parameters = default(SpriteBatchParameters))
	{
		return new EffectChainEntry(shader.ShaderType switch
		{
			PlayerDrawHelper.ShaderConfiguration.ArmorShader => delegate
			{
				GameShaders.Armor.Apply(shader.LocalIndex, player, drawData);
			}, 
			PlayerDrawHelper.ShaderConfiguration.HairShader => delegate
			{
				GameShaders.Hair.Apply(shader.LocalIndex, player, drawData);
			}, 
			PlayerDrawHelper.ShaderConfiguration.TileShader => Main.tileShader.CurrentTechnique.Passes[shader.LocalIndex].Apply, 
			PlayerDrawHelper.ShaderConfiguration.TilePaintID => Main.tileShader.CurrentTechnique.Passes[Main.ConvertPaintIdToTileShaderIndex(shader.LocalIndex, isUsedForPaintingGrass: false, useWallShaderHacks: false)].Apply, 
			_ => throw new ArgumentOutOfRangeException("shader"), 
		}, parameters);
	}

	private static Action ApplyEffectFunc(Effect effect, string pass = null)
	{
		return delegate
		{
			effect.CurrentTechnique.Passes[pass ?? ((IEnumerable<EffectPass>)effect.CurrentTechnique.Passes).First().Name].Apply();
		};
	}
}
