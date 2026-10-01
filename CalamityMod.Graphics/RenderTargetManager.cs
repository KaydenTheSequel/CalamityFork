using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Graphics;

[Obsolete("ManagedRenderTarget is obsolete; developers should use RenderTarget2Ds and RenderTargetLeases")]
public class RenderTargetManager : ModSystem
{
	public delegate void RenderTargetUpdateDelegate();

	public const int TimeBeforeAutoDispose = 600;

	public static event RenderTargetUpdateDelegate RenderTargetUpdateLoopEvent;

	public override void OnModLoad()
	{
		Main.OnPreDraw += HandleTargetUpdateLoop;
	}

	public override void OnModUnload()
	{
		Main.OnPreDraw -= HandleTargetUpdateLoop;
		RenderTargetUpdateLoopEvent = null;
	}

	private void HandleTargetUpdateLoop(GameTime obj)
	{
		RenderTargetUpdateLoopEvent?.Invoke();
	}
}
