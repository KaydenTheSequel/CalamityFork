using System.Runtime.CompilerServices;
using CalamityMod.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Utilities.Daybreak.Buffers;

public class BuffersLoadingSystem : ModSystem
{
	[CompilerGenerated]
	private static class _003C_003EO
	{
		public static hook_DoDraw _003C0_003E__RenderTargetPool_DoDrawHook;

		public static hook_EnsureRenderTargetContent _003C1_003E__ScreenspaceTargetPool_EnsureRenderTargetContentHook;
	}

	public override void Load()
	{
		if (Main.dedServ)
		{
			return;
		}
		Main.RunOnMainThread(delegate
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Expected O, but got Unknown
			//IL_004f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0054: Unknown result type (might be due to invalid IL or missing references)
			//IL_005a: Expected O, but got Unknown
			object obj = _003C_003EO._003C0_003E__RenderTargetPool_DoDrawHook;
			if (obj == null)
			{
				hook_DoDraw val = RenderTargetPool_DoDrawHook;
				_003C_003EO._003C0_003E__RenderTargetPool_DoDrawHook = val;
				obj = (object)val;
			}
			On_Main.DoDraw += (hook_DoDraw)obj;
			Main.graphics.GraphicsDevice.PresentationParameters.RenderTargetUsage = (RenderTargetUsage)1;
			Main.graphics.ApplyChanges();
			object obj2 = _003C_003EO._003C1_003E__ScreenspaceTargetPool_EnsureRenderTargetContentHook;
			if (obj2 == null)
			{
				hook_EnsureRenderTargetContent val2 = ScreenspaceTargetPool_EnsureRenderTargetContentHook;
				_003C_003EO._003C1_003E__ScreenspaceTargetPool_EnsureRenderTargetContentHook = val2;
				obj2 = (object)val2;
			}
			On_Main.EnsureRenderTargetContent += (hook_EnsureRenderTargetContent)obj2;
		});
	}

	public override void Unload()
	{
		if (Main.dedServ)
		{
			return;
		}
		Main.RunOnMainThread(delegate
		{
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Expected O, but got Unknown
			//IL_0030: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Expected O, but got Unknown
			object obj = _003C_003EO._003C0_003E__RenderTargetPool_DoDrawHook;
			if (obj == null)
			{
				hook_DoDraw val = RenderTargetPool_DoDrawHook;
				_003C_003EO._003C0_003E__RenderTargetPool_DoDrawHook = val;
				obj = (object)val;
			}
			On_Main.DoDraw -= (hook_DoDraw)obj;
			object obj2 = _003C_003EO._003C1_003E__ScreenspaceTargetPool_EnsureRenderTargetContentHook;
			if (obj2 == null)
			{
				hook_EnsureRenderTargetContent val2 = ScreenspaceTargetPool_EnsureRenderTargetContentHook;
				_003C_003EO._003C1_003E__ScreenspaceTargetPool_EnsureRenderTargetContentHook = val2;
				obj2 = (object)val2;
			}
			On_Main.EnsureRenderTargetContent -= (hook_EnsureRenderTargetContent)obj2;
			RenderTargetPool.Shared.Dispose();
			ScreenspaceTargetPool.Shared.Dispose();
			ManagedRenderTargetPool.Shared.Dispose();
		});
	}

	private static void RenderTargetPool_DoDrawHook(orig_DoDraw orig, Main self, GameTime time)
	{
		RenderTargetPool.ClearPendingLeases();
		orig.Invoke(self, time);
	}

	private static void ScreenspaceTargetPool_EnsureRenderTargetContentHook(orig_EnsureRenderTargetContent orig, Main self)
	{
		orig.Invoke(self);
		ScreenspaceTargetPool.Shared.ResizeCachedTargets(((Game)self).GraphicsDevice);
		ManagedRenderTargetPool.Shared.ResizeTargets();
	}
}
