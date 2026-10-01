using System;
using CalamityMod.Enums;
using CalamityMod.Utilities.Daybreak.Buffers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Graphics.Renderers;

[Obsolete("BaseRenderer is obsolete; developers should explore other avenues for rendering")]
public abstract class BaseRenderer : ModType
{
	public abstract GeneralDrawLayer Layer { get; }

	public abstract bool ShouldDraw { get; }

	public RenderTargetLease MainTarget { get; private set; }

	protected sealed override void Register()
	{
		ModTypeLookup<BaseRenderer>.Register(this);
		if (RendererManager.Renderers.Contains(this))
		{
			throw new Exception("Renderer '" + Name + "' has already been registered!");
		}
		RendererManager.Renderers.Add(this);
	}

	public sealed override void SetupContent()
	{
		SetStaticDefaults();
	}

	public sealed override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			Main.QueueMainThreadAction(delegate
			{
				MainTarget = ScreenspaceTargetPool.Shared.Rent(((Game)Main.instance).GraphicsDevice);
			});
		}
	}

	public virtual void PreUpdate()
	{
	}

	public virtual void PostUpdate()
	{
	}

	public abstract void DrawToTarget(SpriteBatch spriteBatch);

	public virtual void DrawTarget(SpriteBatch spriteBatch)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		spriteBatch.Draw((Texture2D)(object)MainTarget.Target, Vector2.Zero, Color.White);
	}
}
