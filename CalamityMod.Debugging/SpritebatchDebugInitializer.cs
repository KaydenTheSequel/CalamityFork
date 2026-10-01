using System;
using System.Reflection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoMod.RuntimeDetour;
using Terraria.ModLoader;

namespace CalamityMod.Debugging;

[Autoload(false)]
public class SpritebatchDebugInitializer : ILoadable
{
	private static Hook _beginHook;

	private static Hook _endHook;

	public void Load(Mod mod)
	{
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Expected O, but got Unknown
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Expected O, but got Unknown
		MethodInfo? method = typeof(SpriteBatch).GetMethod("Begin", BindingFlags.Instance | BindingFlags.Public, null, new Type[7]
		{
			typeof(SpriteSortMode),
			typeof(BlendState),
			typeof(SamplerState),
			typeof(DepthStencilState),
			typeof(RasterizerState),
			typeof(Effect),
			typeof(Matrix)
		}, null);
		MethodInfo end = typeof(SpriteBatch).GetMethod("End", BindingFlags.Instance | BindingFlags.Public);
		_beginHook = new Hook((MethodBase)method, (Delegate)new Action<Action<SpriteBatch, SpriteSortMode, BlendState, SamplerState, DepthStencilState, RasterizerState, Effect, Matrix>, SpriteBatch, SpriteSortMode, BlendState, SamplerState, DepthStencilState, RasterizerState, Effect, Matrix>(Begin_Impl));
		_endHook = new Hook((MethodBase)end, (Delegate)new Action<Action<SpriteBatch>, SpriteBatch>(End_Impl));
	}

	private static void Begin_Impl(Action<SpriteBatch, SpriteSortMode, BlendState, SamplerState, DepthStencilState, RasterizerState, Effect, Matrix> orig, SpriteBatch self, SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, Matrix transformMatrix)
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (self.beginCalled)
		{
			ModLoader.GetMod("CalamityMod").Logger.Debug((object)("[SPRITEBATCH DEBUG] Begin was last called here: " + SpritebatchDebug.Trace));
		}
		SpritebatchDebug.Trace = Environment.StackTrace;
		orig(self, sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
	}

	private static void End_Impl(Action<SpriteBatch> orig, SpriteBatch self)
	{
		if (!self.beginCalled)
		{
			ModLoader.GetMod("CalamityMod").Logger.Debug((object)("[SPRITEBATCH DEBUG] Doesn't seem like Begin was called. Here's the last end call: " + SpritebatchDebug.Trace));
		}
		SpritebatchDebug.Trace = Environment.StackTrace;
		orig(self);
	}

	public void Unload()
	{
		_beginHook.Dispose();
		_endHook.Dispose();
	}
}
