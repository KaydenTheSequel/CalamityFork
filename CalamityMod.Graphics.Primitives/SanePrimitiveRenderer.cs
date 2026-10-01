using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;

namespace CalamityMod.Graphics.Primitives;

public static class SanePrimitiveRenderer
{
	public readonly struct ShaderScope : IDisposable
	{
		private readonly GraphicsDevice _device;

		private readonly Effect _effect;

		private readonly bool _useBasicEffect;

		private readonly Matrix _world;

		private readonly Matrix _view;

		private readonly Matrix _projection;

		private readonly string? _transformMatrixParam;

		private readonly RasterizerState _previousRasterizer;

		private readonly DepthStencilState _previousDepth;

		private readonly BlendState _previousBlend;

		private readonly SamplerState? _previousSampler;

		internal ShaderScope(GraphicsDevice device, Effect effect, bool useBasicEffect, in Matrix world, in Matrix view, in Matrix projection, string? transformMatrixParam, RasterizerState? rasterizerState, DepthStencilState? depthState, BlendState? blendState, SamplerState? samplerState)
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00da: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
			//IL_011b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0122: Unknown result type (might be due to invalid IL or missing references)
			//IL_0127: Unknown result type (might be due to invalid IL or missing references)
			//IL_012e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0133: Unknown result type (might be due to invalid IL or missing references)
			_device = device;
			_effect = effect;
			_useBasicEffect = useBasicEffect;
			_world = world;
			_view = view;
			_projection = projection;
			_transformMatrixParam = transformMatrixParam;
			_previousRasterizer = device.RasterizerState;
			_previousDepth = device.DepthStencilState;
			_previousBlend = device.BlendState;
			_previousSampler = device.SamplerStates[0];
			device.RasterizerState = rasterizerState ?? RasterizerState.CullNone;
			device.DepthStencilState = depthState ?? DepthStencilState.Default;
			device.BlendState = blendState ?? BlendState.AlphaBlend;
			device.SamplerStates[0] = samplerState ?? SamplerState.PointWrap;
			if (_useBasicEffect)
			{
				BasicEffect val = (BasicEffect)_effect;
				val.World = world;
				val.View = view;
				val.Projection = projection;
			}
			else if (transformMatrixParam != null)
			{
				EffectParameter param = _effect.Parameters[transformMatrixParam];
				if (param != null)
				{
					param.SetValue(world * view * projection);
				}
			}
		}

		public void Draw(in PrimitiveMesh mesh)
		{
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
			//IL_0088: Unknown result type (might be due to invalid IL or missing references)
			if (!mesh.IsValid)
			{
				return;
			}
			if (_useBasicEffect)
			{
				BasicEffect val = (BasicEffect)_effect;
				bool textured = (val.TextureEnabled = mesh.UsesTexture);
				val.Texture = (textured ? TextureAssets.MagicPixel.Value : null);
			}
			int primitiveCount = GetPrimitiveCount(mesh.PrimitiveType, mesh.Indices.Length);
			bool usesTexture2 = mesh.UsesTexture;
			foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
			{
				pass.Apply();
				if (usesTexture2)
				{
					_device.DrawUserIndexedPrimitives<VertexPositionColorTexture>(mesh.PrimitiveType, mesh.TexturedVertices, 0, mesh.VertexCount, mesh.Indices, 0, primitiveCount);
				}
				else
				{
					_device.DrawUserIndexedPrimitives<VertexPositionColor>(mesh.PrimitiveType, mesh.ColorVertices, 0, mesh.VertexCount, mesh.Indices, 0, primitiveCount);
				}
			}
		}

		public void Draw(in PrimitiveMeshView mesh)
		{
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_0086: Unknown result type (might be due to invalid IL or missing references)
			if (!mesh.IsValid)
			{
				return;
			}
			if (_useBasicEffect)
			{
				BasicEffect val = (BasicEffect)_effect;
				bool textured = (val.TextureEnabled = mesh.UsesTexture);
				val.Texture = (textured ? TextureAssets.MagicPixel.Value : null);
			}
			int primitiveCount = GetPrimitiveCount(mesh.PrimitiveType, mesh.IndexCount);
			bool usesTexture2 = mesh.UsesTexture;
			foreach (EffectPass pass in _effect.CurrentTechnique.Passes)
			{
				pass.Apply();
				if (usesTexture2)
				{
					_device.DrawUserIndexedPrimitives<VertexPositionColorTexture>(mesh.PrimitiveType, mesh.TexturedVertices, mesh.VertexOffset, mesh.VertexCount, mesh.Indices, mesh.IndexOffset, primitiveCount);
				}
				else
				{
					_device.DrawUserIndexedPrimitives<VertexPositionColor>(mesh.PrimitiveType, mesh.ColorVertices, mesh.VertexOffset, mesh.VertexCount, mesh.Indices, mesh.IndexOffset, primitiveCount);
				}
			}
		}

		public void Dispose()
		{
			_device.RasterizerState = _previousRasterizer;
			_device.DepthStencilState = _previousDepth;
			_device.BlendState = _previousBlend;
			if (_previousSampler != null)
			{
				_device.SamplerStates[0] = _previousSampler;
			}
		}
	}

	private static BasicEffect? _effect;

	private static GraphicsDevice _graphicsDevice => Main.graphics.GraphicsDevice;

	public static bool IsReady
	{
		get
		{
			if (_graphicsDevice != null && !_graphicsDevice.IsDisposed && _effect != null)
			{
				return !((GraphicsResource)_effect).IsDisposed;
			}
			return false;
		}
	}

	public static void Initialize()
	{
		Main.RunOnMainThread(delegate
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_003a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0048: Unknown result type (might be due to invalid IL or missing references)
			//IL_005d: Expected O, but got Unknown
			if (_graphicsDevice == null)
			{
				throw new ArgumentNullException("_graphicsDevice");
			}
			BasicEffect? effect = _effect;
			if (effect != null)
			{
				((GraphicsResource)effect).Dispose();
			}
			_effect = new BasicEffect(_graphicsDevice)
			{
				VertexColorEnabled = true,
				TextureEnabled = true,
				LightingEnabled = false,
				FogEnabled = false,
				Texture = TextureAssets.Logo.Value
			};
		});
	}

	public static void Dispose()
	{
		Main.RunOnMainThread(delegate
		{
			BasicEffect? effect = _effect;
			if (effect != null)
			{
				((GraphicsResource)effect).Dispose();
			}
		});
	}

	public static void DrawMesh(in Matrix world, in Matrix view, in Matrix projection, in PrimitiveMesh mesh, RasterizerState? rasterizerState = null, DepthStencilState? depthState = null, BlendState? blendState = null)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		if (!IsReady)
		{
			throw new InvalidOperationException("SanePrimitiveRenderer is not initialized.");
		}
		if (!mesh.IsValid)
		{
			return;
		}
		bool textured = mesh.UsesTexture;
		_effect.TextureEnabled = textured;
		_effect.Texture = (textured ? TextureAssets.MagicPixel.Value : null);
		_graphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
		_effect.World = world;
		_effect.View = view;
		_effect.Projection = projection;
		GraphicsDevice device = _graphicsDevice;
		RasterizerState previousRasterizer = device.RasterizerState;
		DepthStencilState previousDepth = device.DepthStencilState;
		BlendState previousBlend = device.BlendState;
		device.RasterizerState = rasterizerState ?? RasterizerState.CullNone;
		device.DepthStencilState = depthState ?? DepthStencilState.Default;
		device.BlendState = blendState ?? BlendState.AlphaBlend;
		int primitiveCount = GetPrimitiveCount(mesh.PrimitiveType, mesh.Indices.Length);
		foreach (EffectPass pass in ((Effect)_effect).CurrentTechnique.Passes)
		{
			pass.Apply();
			if (textured)
			{
				device.DrawUserIndexedPrimitives<VertexPositionColorTexture>(mesh.PrimitiveType, mesh.TexturedVertices, 0, mesh.VertexCount, mesh.Indices, 0, primitiveCount);
			}
			else
			{
				device.DrawUserIndexedPrimitives<VertexPositionColor>(mesh.PrimitiveType, mesh.ColorVertices, 0, mesh.VertexCount, mesh.Indices, 0, primitiveCount);
			}
		}
		device.RasterizerState = previousRasterizer;
		device.DepthStencilState = previousDepth;
		device.BlendState = previousBlend;
	}

	public static void DrawMesh(in Matrix world, in Matrix view, in Matrix projection, in PrimitiveMesh mesh, Effect effect, string? transformMatrixParam = "uTransformMatrix", RasterizerState? rasterizerState = null, DepthStencilState? depthState = null, BlendState? blendState = null)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		if (effect == null)
		{
			throw new ArgumentNullException("effect");
		}
		if (!mesh.IsValid)
		{
			return;
		}
		if (transformMatrixParam != null)
		{
			EffectParameter param = effect.Parameters[transformMatrixParam];
			if (param != null)
			{
				param.SetValue(world * view * projection);
			}
		}
		GraphicsDevice device = _graphicsDevice;
		RasterizerState previousRasterizer = device.RasterizerState;
		DepthStencilState previousDepth = device.DepthStencilState;
		BlendState previousBlend = device.BlendState;
		device.RasterizerState = rasterizerState ?? RasterizerState.CullNone;
		device.DepthStencilState = depthState ?? DepthStencilState.Default;
		device.BlendState = blendState ?? BlendState.AlphaBlend;
		int primitiveCount = GetPrimitiveCount(mesh.PrimitiveType, mesh.Indices.Length);
		bool textured = mesh.UsesTexture;
		foreach (EffectPass pass in effect.CurrentTechnique.Passes)
		{
			pass.Apply();
			if (textured)
			{
				device.DrawUserIndexedPrimitives<VertexPositionColorTexture>(mesh.PrimitiveType, mesh.TexturedVertices, 0, mesh.VertexCount, mesh.Indices, 0, primitiveCount);
			}
			else
			{
				device.DrawUserIndexedPrimitives<VertexPositionColor>(mesh.PrimitiveType, mesh.ColorVertices, 0, mesh.VertexCount, mesh.Indices, 0, primitiveCount);
			}
		}
		device.RasterizerState = previousRasterizer;
		device.DepthStencilState = previousDepth;
		device.BlendState = previousBlend;
	}

	public static void DrawTriangleStrip(in Matrix world, in Matrix view, in Matrix projection, VertexPositionColorTexture[] vertices, RasterizerState? rasterizerState = null, DepthStencilState? depthState = null, BlendState? blendState = null)
	{
		if (vertices == null)
		{
			throw new ArgumentNullException("vertices");
		}
		if (vertices.Length >= 3)
		{
			if (vertices.Length > 32767)
			{
				throw new ArgumentOutOfRangeException("vertices", "Vertex count exceeds index buffer range.");
			}
			DrawMesh(in world, in view, in projection, PrimitiveMesh.FromSequential(vertices, (PrimitiveType)1), rasterizerState, depthState, blendState);
		}
	}

	public static void DrawTriangleStrip(in Matrix world, in Matrix view, in Matrix projection, VertexPositionColor[] vertices, RasterizerState? rasterizerState = null, DepthStencilState? depthState = null, BlendState? blendState = null)
	{
		if (vertices == null)
		{
			throw new ArgumentNullException("vertices");
		}
		if (vertices.Length >= 3)
		{
			if (vertices.Length > 32767)
			{
				throw new ArgumentOutOfRangeException("vertices", "Vertex count exceeds index buffer range.");
			}
			DrawMesh(in world, in view, in projection, PrimitiveMesh.FromSequential(vertices, (PrimitiveType)1), rasterizerState, depthState, blendState);
		}
	}

	public static void DrawTriangleList(in Matrix world, in Matrix view, in Matrix projection, VertexPositionColorTexture[] vertices, RasterizerState? rasterizerState = null, DepthStencilState? depthState = null, BlendState? blendState = null)
	{
		if (vertices == null)
		{
			throw new ArgumentNullException("vertices");
		}
		if (vertices.Length >= 3 && vertices.Length % 3 == 0)
		{
			if (vertices.Length > 32767)
			{
				throw new ArgumentOutOfRangeException("vertices", "Vertex count exceeds index buffer range.");
			}
			DrawMesh(in world, in view, in projection, PrimitiveMesh.FromSequential(vertices, (PrimitiveType)0), rasterizerState, depthState, blendState);
		}
	}

	public static void DrawTriangleList(in Matrix world, in Matrix view, in Matrix projection, VertexPositionColor[] vertices, RasterizerState? rasterizerState = null, DepthStencilState? depthState = null, BlendState? blendState = null)
	{
		if (vertices == null)
		{
			throw new ArgumentNullException("vertices");
		}
		if (vertices.Length >= 3 && vertices.Length % 3 == 0)
		{
			if (vertices.Length > 32767)
			{
				throw new ArgumentOutOfRangeException("vertices", "Vertex count exceeds index buffer range.");
			}
			DrawMesh(in world, in view, in projection, PrimitiveMesh.FromSequential(vertices, (PrimitiveType)0), rasterizerState, depthState, blendState);
		}
	}

	public static void DrawMesh(in Matrix world, in Matrix view, in Matrix projection, in PrimitiveMeshView mesh, RasterizerState? rasterizerState = null, DepthStencilState? depthState = null, BlendState? blendState = null)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		if (!IsReady)
		{
			throw new InvalidOperationException("SanePrimitiveRenderer is not initialized.");
		}
		if (!mesh.IsValid)
		{
			return;
		}
		bool textured = mesh.UsesTexture;
		_effect.TextureEnabled = textured;
		_effect.Texture = (textured ? TextureAssets.MagicPixel.Value : null);
		_graphicsDevice.SamplerStates[0] = SamplerState.PointWrap;
		_effect.World = world;
		_effect.View = view;
		_effect.Projection = projection;
		GraphicsDevice device = _graphicsDevice;
		RasterizerState previousRasterizer = device.RasterizerState;
		DepthStencilState previousDepth = device.DepthStencilState;
		BlendState previousBlend = device.BlendState;
		device.RasterizerState = rasterizerState ?? RasterizerState.CullNone;
		device.DepthStencilState = depthState ?? DepthStencilState.Default;
		device.BlendState = blendState ?? BlendState.AlphaBlend;
		int primitiveCount = GetPrimitiveCount(mesh.PrimitiveType, mesh.IndexCount);
		foreach (EffectPass pass in ((Effect)_effect).CurrentTechnique.Passes)
		{
			pass.Apply();
			if (textured)
			{
				device.DrawUserIndexedPrimitives<VertexPositionColorTexture>(mesh.PrimitiveType, mesh.TexturedVertices, mesh.VertexOffset, mesh.VertexCount, mesh.Indices, mesh.IndexOffset, primitiveCount);
			}
			else
			{
				device.DrawUserIndexedPrimitives<VertexPositionColor>(mesh.PrimitiveType, mesh.ColorVertices, mesh.VertexOffset, mesh.VertexCount, mesh.Indices, mesh.IndexOffset, primitiveCount);
			}
		}
		device.RasterizerState = previousRasterizer;
		device.DepthStencilState = previousDepth;
		device.BlendState = previousBlend;
	}

	public static void DrawMesh(in Matrix world, in Matrix view, in Matrix projection, in PrimitiveMeshView mesh, Effect effect, string? transformMatrixParam = "uTransformMatrix", RasterizerState? rasterizerState = null, DepthStencilState? depthState = null, BlendState? blendState = null)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		if (effect == null)
		{
			throw new ArgumentNullException("effect");
		}
		if (!mesh.IsValid)
		{
			return;
		}
		if (transformMatrixParam != null)
		{
			EffectParameter param = effect.Parameters[transformMatrixParam];
			if (param != null)
			{
				param.SetValue(world * view * projection);
			}
		}
		GraphicsDevice device = _graphicsDevice;
		RasterizerState previousRasterizer = device.RasterizerState;
		DepthStencilState previousDepth = device.DepthStencilState;
		BlendState previousBlend = device.BlendState;
		device.RasterizerState = rasterizerState ?? RasterizerState.CullNone;
		device.DepthStencilState = depthState ?? DepthStencilState.Default;
		device.BlendState = blendState ?? BlendState.AlphaBlend;
		int primitiveCount = GetPrimitiveCount(mesh.PrimitiveType, mesh.IndexCount);
		bool textured = mesh.UsesTexture;
		foreach (EffectPass pass in effect.CurrentTechnique.Passes)
		{
			pass.Apply();
			if (textured)
			{
				device.DrawUserIndexedPrimitives<VertexPositionColorTexture>(mesh.PrimitiveType, mesh.TexturedVertices, mesh.VertexOffset, mesh.VertexCount, mesh.Indices, mesh.IndexOffset, primitiveCount);
			}
			else
			{
				device.DrawUserIndexedPrimitives<VertexPositionColor>(mesh.PrimitiveType, mesh.ColorVertices, mesh.VertexOffset, mesh.VertexCount, mesh.Indices, mesh.IndexOffset, primitiveCount);
			}
		}
		device.RasterizerState = previousRasterizer;
		device.DepthStencilState = previousDepth;
		device.BlendState = previousBlend;
	}

	public static ShaderScope BeginShaderScope(in Matrix world, in Matrix view, in Matrix projection, RasterizerState? rasterizerState = null, DepthStencilState? depthState = null, BlendState? blendState = null, SamplerState? samplerState = null)
	{
		if (!IsReady)
		{
			throw new InvalidOperationException("SanePrimitiveRenderer is not initialized.");
		}
		return new ShaderScope(_graphicsDevice, (Effect)(object)_effect, useBasicEffect: true, in world, in view, in projection, null, rasterizerState, depthState, blendState, samplerState);
	}

	public static ShaderScope BeginShaderScope(Effect effect, in Matrix world, in Matrix view, in Matrix projection, string? transformMatrixParam = "uTransformMatrix", RasterizerState? rasterizerState = null, DepthStencilState? depthState = null, BlendState? blendState = null, SamplerState? samplerState = null)
	{
		if (effect == null)
		{
			throw new ArgumentNullException("effect");
		}
		return new ShaderScope(_graphicsDevice, effect, useBasicEffect: false, in world, in view, in projection, transformMatrixParam, rasterizerState, depthState, blendState, samplerState);
	}

	public static void DrawMeshes(in Matrix world, in Matrix view, in Matrix projection, IReadOnlyList<PrimitiveMesh> meshes, RasterizerState? rasterizerState = null, DepthStencilState? depthState = null, BlendState? blendState = null, SamplerState? samplerState = null)
	{
		if (meshes == null)
		{
			throw new ArgumentNullException("meshes");
		}
		using ShaderScope scope = BeginShaderScope(in world, in view, in projection, rasterizerState, depthState, blendState, samplerState);
		for (int i = 0; i < meshes.Count; i++)
		{
			scope.Draw(meshes[i]);
		}
	}

	public static void DrawMeshes(in Matrix world, in Matrix view, in Matrix projection, IReadOnlyList<PrimitiveMeshView> meshes, RasterizerState? rasterizerState = null, DepthStencilState? depthState = null, BlendState? blendState = null, SamplerState? samplerState = null)
	{
		if (meshes == null)
		{
			throw new ArgumentNullException("meshes");
		}
		using ShaderScope scope = BeginShaderScope(in world, in view, in projection, rasterizerState, depthState, blendState, samplerState);
		for (int i = 0; i < meshes.Count; i++)
		{
			scope.Draw(meshes[i]);
		}
	}

	public static void DrawMeshes(Effect effect, in Matrix world, in Matrix view, in Matrix projection, IReadOnlyList<PrimitiveMesh> meshes, string? transformMatrixParam = "uTransformMatrix", RasterizerState? rasterizerState = null, DepthStencilState? depthState = null, BlendState? blendState = null, SamplerState? samplerState = null)
	{
		if (effect == null)
		{
			throw new ArgumentNullException("effect");
		}
		if (meshes == null)
		{
			throw new ArgumentNullException("meshes");
		}
		using ShaderScope scope = BeginShaderScope(effect, in world, in view, in projection, transformMatrixParam, rasterizerState, depthState, blendState, samplerState);
		for (int i = 0; i < meshes.Count; i++)
		{
			scope.Draw(meshes[i]);
		}
	}

	public static void DrawMeshes(Effect effect, in Matrix world, in Matrix view, in Matrix projection, IReadOnlyList<PrimitiveMeshView> meshes, string? transformMatrixParam = "uTransformMatrix", RasterizerState? rasterizerState = null, DepthStencilState? depthState = null, BlendState? blendState = null, SamplerState? samplerState = null)
	{
		if (effect == null)
		{
			throw new ArgumentNullException("effect");
		}
		if (meshes == null)
		{
			throw new ArgumentNullException("meshes");
		}
		using ShaderScope scope = BeginShaderScope(effect, in world, in view, in projection, transformMatrixParam, rasterizerState, depthState, blendState, samplerState);
		for (int i = 0; i < meshes.Count; i++)
		{
			scope.Draw(meshes[i]);
		}
	}

	private unsafe static int GetPrimitiveCount(PrimitiveType primitiveType, int indexCount)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Expected I4, but got Unknown
		return (int)primitiveType switch
		{
			0 => indexCount / 3, 
			1 => Math.Max(indexCount - 2, 0), 
			2 => indexCount / 2, 
			3 => Math.Max(indexCount - 1, 0), 
			4 => indexCount, 
			_ => throw new NotSupportedException(((object)(*(PrimitiveType*)(&primitiveType))/*cast due to constrained. prefix*/).ToString()), 
		};
	}
}
