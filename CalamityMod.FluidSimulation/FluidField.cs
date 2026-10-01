using System;
using CalamityMod.Effects;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.FluidSimulation;

public class FluidField : IDisposable
{
	internal RenderTargetLease TemporaryAuxilaryTarget;

	internal RenderTargetLease DivergenceField;

	internal RenderTargetLease DivergencePoissonField;

	internal FluidFieldState VelocityField;

	internal FluidFieldState DensityField;

	internal FluidFieldState ColorField;

	internal RenderTargetLease OutputTarget;

	public float Viscosity;

	public float DiffusionFactor;

	public float DissipationFactor;

	public bool ShouldUpdate;

	public bool ShouldSkipDivergenceClearingStep;

	public Action UpdateAction;

	public readonly int Size;

	public readonly float Scale;

	public const float DeltaTime = 0.016666f;

	public const int GaussSeidelIterations = 2;

	internal static BasicEffect basicShader;

	public bool Disposing { get; private set; }

	public static BasicEffect BasicShader
	{
		get
		{
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0024: Unknown result type (might be due to invalid IL or missing references)
			//IL_0030: Expected O, but got Unknown
			if (!Main.dedServ && basicShader == null)
			{
				basicShader = new BasicEffect(((Game)Main.instance).GraphicsDevice)
				{
					VertexColorEnabled = true,
					TextureEnabled = true
				};
			}
			return basicShader;
		}
	}

	public static RenderTargetDescriptor FluidDescriptor => new RenderTargetDescriptor((SurfaceFormat)15, (DepthFormat)2, 0, (RenderTargetUsage)1, GenerateMipmaps: true);

	internal FluidField(int size, float scale, float viscosity, float diffusionFactor, float dissipationFactor)
	{
		Size = size;
		Scale = scale;
		Viscosity = viscosity;
		DiffusionFactor = diffusionFactor;
		DissipationFactor = dissipationFactor;
		VelocityField = new FluidFieldState(size, (SurfaceFormat)15);
		DensityField = new FluidFieldState(size, (SurfaceFormat)0);
		ColorField = new FluidFieldState(size, (SurfaceFormat)0);
		TemporaryAuxilaryTarget = RenderTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, Size, Size, FluidDescriptor);
		DivergenceField = RenderTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, Size, Size, FluidDescriptor);
		DivergencePoissonField = RenderTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, Size, Size, FluidDescriptor);
		OutputTarget = RenderTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, Size, Size, FluidDescriptor);
	}

	internal void ApplyThingToTarget(RenderTarget2D currentField, Action shaderPreparationsAction)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		using (TemporaryAuxilaryTarget.Scope(preserveContents: true, Color.Transparent))
		{
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Matrix.Identity);
			shaderPreparationsAction();
			Main.spriteBatch.Draw((Texture2D)(object)currentField, ((Texture2D)currentField).Bounds, Color.White);
			Main.spriteBatch.End();
		}
		currentField.CopyContentsFrom(TemporaryAuxilaryTarget.Target);
	}

	internal void FlushQueueToTarget(FluidFieldState field)
	{
		ApplyThingToTarget(field.NextState.Target, delegate
		{
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_0042: Unknown result type (might be due to invalid IL or missing references)
			//IL_0090: Unknown result type (might be due to invalid IL or missing references)
			//IL_009c: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00be: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00db: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
			//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0100: Unknown result type (might be due to invalid IL or missing references)
			//IL_010f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0125: Unknown result type (might be due to invalid IL or missing references)
			//IL_0129: Unknown result type (might be due to invalid IL or missing references)
			//IL_0138: Unknown result type (might be due to invalid IL or missing references)
			//IL_014e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0152: Unknown result type (might be due to invalid IL or missing references)
			//IL_0161: Unknown result type (might be due to invalid IL or missing references)
			//IL_0177: Unknown result type (might be due to invalid IL or missing references)
			//IL_017b: Unknown result type (might be due to invalid IL or missing references)
			//IL_018a: Unknown result type (might be due to invalid IL or missing references)
			//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
			//IL_0200: Unknown result type (might be due to invalid IL or missing references)
			int num = 0;
			int count = field.PendingChanges.Count;
			if (count > 0)
			{
				Texture2D value = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Pixel", (AssetRequestMode)2).Value;
				CalamityUtils.CalculatePerspectiveMatricies(out var viewMatrix, out var projectionMatrix);
				BasicShader.View = viewMatrix;
				BasicShader.Projection = projectionMatrix;
				((Effect)BasicShader).CurrentTechnique.Passes[0].Apply();
				FieldVertex2D[] array = new FieldVertex2D[count * 4];
				short[] array2 = new short[count * 6];
				PixelQueueValue result;
				while (field.PendingChanges.TryDequeue(out result))
				{
					new Color(result.Value.X, result.Value.Y, result.Value.Z, result.Value.W);
					Vector2 position = result.Position;
					Vector2 position2 = position + Vector2.UnitX;
					Vector2 position3 = position + Vector2.UnitY;
					Vector2 position4 = position + Vector2.One;
					array[num * 4] = new FieldVertex2D(position, result.Value, new Vector2(0f, 0f));
					array[num * 4 + 1] = new FieldVertex2D(position2, result.Value, new Vector2(1f, 0f));
					array[num * 4 + 2] = new FieldVertex2D(position4, result.Value, new Vector2(1f, 1f));
					array[num * 4 + 3] = new FieldVertex2D(position3, result.Value, new Vector2(0f, 1f));
					array2[num * 6] = (short)(num * 4);
					array2[num * 6 + 1] = (short)(num * 4 + 1);
					array2[num * 6 + 2] = (short)(num * 4 + 2);
					array2[num * 6 + 3] = (short)(num * 4);
					array2[num * 6 + 4] = (short)(num * 4 + 2);
					array2[num * 6 + 5] = (short)(num * 4 + 3);
					num++;
					Main.spriteBatch.Draw(value, Vector2.Zero, (Rectangle?)null, Color.Transparent);
				}
				((Game)Main.instance).GraphicsDevice.DrawUserIndexedPrimitives<FieldVertex2D>((PrimitiveType)0, array, 0, count * 4, array2, 0, count * 2);
			}
		});
	}

	internal void CalculateDiffusion(float diffusionFactor, FluidFieldState field, bool colors = false)
	{
		diffusionFactor *= 0.016666f * (float)Size;
		ApplyThingToTarget(field.NextState.Target, delegate
		{
			((Game)Main.instance).GraphicsDevice.Textures[1] = (Texture)(object)field.PreviousState.Target;
			CalamityShaders.FluidShaders.Value.Parameters["size"].SetValue(Size);
			CalamityShaders.FluidShaders.Value.Parameters["diffusionFactor"].SetValue(diffusionFactor);
			CalamityShaders.FluidShaders.Value.Parameters["handlingColors"].SetValue(colors);
			CalamityShaders.FluidShaders.Value.Parameters["dissipationFactor"].SetValue(DissipationFactor);
			CalamityShaders.FluidShaders.Value.CurrentTechnique.Passes["DiffusionPass"].Apply();
		});
	}

	internal void CalculateAdvection(RenderTarget2D currentField, RenderTarget2D nextField, RenderTarget2D velocities, bool colors = false)
	{
		ApplyThingToTarget(nextField, delegate
		{
			((Game)Main.instance).GraphicsDevice.Textures[1] = (Texture)(object)currentField;
			((Game)Main.instance).GraphicsDevice.Textures[2] = (Texture)(object)velocities;
			CalamityShaders.FluidShaders.Value.Parameters["size"].SetValue(Size);
			CalamityShaders.FluidShaders.Value.Parameters["deltaTime"].SetValue(0.016666f);
			CalamityShaders.FluidShaders.Value.Parameters["handlingColors"].SetValue(colors);
			CalamityShaders.FluidShaders.Value.CurrentTechnique.Passes["AdvectionPass"].Apply();
		});
	}

	internal void ClearDivergence(RenderTarget2D velocities)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		ApplyThingToTarget(DivergenceField.Target, delegate
		{
			((Game)Main.instance).GraphicsDevice.Textures[2] = (Texture)(object)velocities;
			CalamityShaders.FluidShaders.Value.Parameters["size"].SetValue(Size);
			CalamityShaders.FluidShaders.Value.CurrentTechnique.Passes["InitializeDivergencePass"].Apply();
		});
		using (DivergencePoissonField.Scope(preserveContents: true, Color.Transparent))
		{
		}
		for (int i = 0; i < 2; i++)
		{
			ApplyThingToTarget(DivergencePoissonField.Target, delegate
			{
				((Game)Main.instance).GraphicsDevice.Textures[1] = (Texture)(object)DivergencePoissonField.Target;
				((Game)Main.instance).GraphicsDevice.Textures[2] = (Texture)(object)velocities;
				((Game)Main.instance).GraphicsDevice.Textures[4] = (Texture)(object)DivergenceField.Target;
				CalamityShaders.FluidShaders.Value.Parameters["size"].SetValue(Size);
				CalamityShaders.FluidShaders.Value.CurrentTechnique.Passes["PerformPoissonIterationPass"].Apply();
			});
		}
		ApplyThingToTarget(velocities, delegate
		{
			((Game)Main.instance).GraphicsDevice.Textures[1] = (Texture)(object)velocities;
			((Game)Main.instance).GraphicsDevice.Textures[3] = (Texture)(object)DivergencePoissonField.Target;
			CalamityShaders.FluidShaders.Value.CurrentTechnique.Passes["ClearDivergencePass"].Apply();
		});
	}

	internal void Update()
	{
		if (!Main.dedServ && ShouldUpdate)
		{
			ShouldUpdate = false;
			UpdateAction?.Invoke();
			UpdateAction = null;
			FlushQueueToTarget(VelocityField);
			FlushQueueToTarget(ColorField);
			FlushQueueToTarget(DensityField);
			UpdateVelocityFields();
			UpdateDensityFields();
			UpdateOutputTarget();
			ShouldSkipDivergenceClearingStep = false;
		}
	}

	internal void UpdateVelocityFields()
	{
		CalculateDiffusion(Viscosity, VelocityField);
		if (!ShouldSkipDivergenceClearingStep)
		{
			ClearDivergence(VelocityField.NextState.Target);
		}
		CalculateAdvection(VelocityField.NextState.Target, VelocityField.PreviousState.Target, VelocityField.PreviousState.Target);
		if (!ShouldSkipDivergenceClearingStep)
		{
			ClearDivergence(VelocityField.NextState.Target);
		}
	}

	internal void UpdateDensityFields()
	{
		CalculateDiffusion(DiffusionFactor, DensityField);
		DensityField.SwapState();
		CalculateAdvection(DensityField.PreviousState.Target, DensityField.NextState.Target, VelocityField.NextState.Target);
		CalculateDiffusion(DiffusionFactor, ColorField, colors: true);
		ColorField.SwapState();
		CalculateAdvection(ColorField.PreviousState.Target, ColorField.NextState.Target, VelocityField.NextState.Target, colors: true);
	}

	public void UpdateOutputTarget()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		using (OutputTarget.Scope(preserveContents: true, Color.Transparent))
		{
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, Matrix.Identity);
			((Game)Main.instance).GraphicsDevice.Textures[5] = (Texture)(object)ColorField.NextState.Target;
			CalamityShaders.FluidShaders.Value.CurrentTechnique.Passes["DrawFluidPass"].Apply();
			Main.spriteBatch.Draw((Texture2D)(object)DensityField.NextState.Target, Vector2.Zero, (Rectangle?)null, Color.White, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			Main.spriteBatch.End();
		}
	}

	public void Dispose()
	{
		if (!Disposing)
		{
			FluidFieldManager.Fields.Remove(this);
			Disposing = true;
			GC.SuppressFinalize(this);
		}
	}

	public void CreateSource(int x, int y, float density, Color color, Vector2 velocity)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		Vector2 pos = default(Vector2);
		((Vector2)(ref pos))._002Ector((float)x, (float)y);
		if (x >= 0 && y >= 0 && x < Size && y < Size)
		{
			ColorField.PendingChanges.Enqueue(new PixelQueueValue(pos, color));
			if (velocity != Vector2.Zero)
			{
				VelocityField.PendingChanges.Enqueue(new PixelQueueValue(pos, new Vector4(velocity.X, velocity.Y, 0f, 0f)));
			}
			DensityField.PendingChanges.Enqueue(new PixelQueueValue(pos, new Color(density, 0f, 0f)));
		}
	}

	public void Draw(Vector2 drawPosition, bool needsToCallEnd, Matrix drawPerspective, Matrix previousPerspective, Action<RenderTarget2D> shaderPreparations = null)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		if (needsToCallEnd)
		{
			Main.spriteBatch.End();
		}
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, drawPerspective);
		shaderPreparations?.Invoke(OutputTarget.Target);
		Main.spriteBatch.Draw((Texture2D)(object)OutputTarget.Target, drawPosition, (Rectangle?)null, Color.White, 0f, ((Texture2D)(object)OutputTarget.Target).Size() * 0.5f, Scale, (SpriteEffects)0, 0f);
		Main.spriteBatch.End();
		if (needsToCallEnd)
		{
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.None, Main.Rasterizer, (Effect)null, previousPerspective);
		}
	}
}
