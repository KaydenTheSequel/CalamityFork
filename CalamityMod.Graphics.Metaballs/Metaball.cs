using System.Collections.Generic;
using System.Linq;
using CalamityMod.Effects;
using CalamityMod.Enums;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Metaballs;

public abstract class Metaball : ModType
{
	internal List<RenderTargetLease> LayerTargets = new List<RenderTargetLease>();

	public abstract bool AnythingToDraw { get; }

	public abstract IEnumerable<Texture2D> Layers { get; }

	public abstract GeneralDrawLayer DrawLayer { get; }

	public abstract Color EdgeColor { get; }

	public virtual List<Vector4> LayerColors { get; set; } = new List<Vector4>();

	public virtual bool IgnoreFPS => false;

	public virtual bool FixedToScreen => false;

	public virtual void ClearInstances()
	{
	}

	public virtual void Update()
	{
	}

	public virtual Vector2 CalculateManualOffsetForLayer(int layerIndex)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return Vector2.Zero;
	}

	public virtual void PrepareSpriteBatch(SpriteBatch spriteBatch)
	{
	}

	public virtual void PrepareShaderForTarget(int layerIndex)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		Asset<Effect> metaballEdgeShader = CalamityShaders.MetaballEdgeShader;
		GraphicsDevice gd = ((Game)Main.instance).GraphicsDevice;
		Texture2D layerTexture = Layers.ElementAt(layerIndex);
		Vector2 screenSize = default(Vector2);
		((Vector2)(ref screenSize))._002Ector((float)Main.screenWidth, (float)Main.screenHeight);
		Vector2 layerScrollOffset = Main.screenPosition / screenSize + CalculateManualOffsetForLayer(layerIndex);
		if (FixedToScreen)
		{
			layerScrollOffset = Vector2.Zero;
		}
		EffectParameter obj = metaballEdgeShader.Value.Parameters["layerSize"];
		if (obj != null)
		{
			obj.SetValue(layerTexture.Size());
		}
		EffectParameter obj2 = metaballEdgeShader.Value.Parameters["screenSize"];
		if (obj2 != null)
		{
			obj2.SetValue(screenSize);
		}
		EffectParameter obj3 = metaballEdgeShader.Value.Parameters["layerOffset"];
		if (obj3 != null)
		{
			obj3.SetValue(layerScrollOffset);
		}
		EffectParameter obj4 = metaballEdgeShader.Value.Parameters["edgeColor"];
		Color val;
		if (obj4 != null)
		{
			val = EdgeColor;
			obj4.SetValue(((Color)(ref val)).ToVector4());
		}
		EffectParameter obj5 = metaballEdgeShader.Value.Parameters["singleFrameScreenOffset"];
		if (obj5 != null)
		{
			obj5.SetValue((Main.screenLastPosition - Main.screenPosition) / screenSize);
		}
		EffectParameter obj6 = metaballEdgeShader.Value.Parameters["layerColor"];
		if (obj6 != null)
		{
			Vector4 value;
			if (LayerColors.Count() <= layerIndex)
			{
				val = Color.White;
				value = ((Color)(ref val)).ToVector4();
			}
			else
			{
				value = LayerColors[layerIndex];
			}
			obj6.SetValue(value);
		}
		gd.Textures[1] = (Texture)(object)layerTexture;
		gd.SamplerStates[1] = SamplerState.LinearWrap;
		metaballEdgeShader.Value.CurrentTechnique.Passes[0].Apply();
	}

	public abstract void DrawInstances();

	protected sealed override void Register()
	{
		ModTypeLookup<Metaball>.Register(this);
		if (!MetaballManager.metaballs.Contains(this))
		{
			MetaballManager.metaballs.Add(this);
		}
		if (Main.dedServ)
		{
			return;
		}
		Main.QueueMainThreadAction(delegate
		{
			int num = Layers.Count();
			for (int i = 0; i < num; i++)
			{
				LayerTargets.Add(ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice, (int width, int height) => (width + 4, height + 4)));
			}
		});
	}

	public void Dispose()
	{
		for (int i = 0; i < LayerTargets.Count; i++)
		{
			LayerTargets[i]?.Dispose();
		}
	}
}
