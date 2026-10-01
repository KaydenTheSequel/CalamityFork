using System;
using MonoMod.Cil;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.ILEditing;

internal sealed class ManipulatorManager : ModSystem
{
	private static bool hasAppliedEdits;

	private static event Action<ManipulatorContext>? WrappedApplyEdits;

	public static event Action<ManipulatorContext> ApplyEdits
	{
		add
		{
			if (hasAppliedEdits)
			{
				throw new InvalidOperationException("Cannot add to ApplyEdits after edits have already been applied! Call it earlier.");
			}
			WrappedApplyEdits += value;
		}
		remove
		{
			WrappedApplyEdits -= value;
		}
	}

	public override void OnModLoad()
	{
		base.OnModLoad();
		hasAppliedEdits = true;
		using ManipulatorBatch playerUpdate = ManipulatorBatch.From(delegate(Manipulator manipulator)
		{
			IL_Player.Update += manipulator;
		});
		ManipulatorContext ctx = new ManipulatorContext(playerUpdate);
		WrappedApplyEdits?.Invoke(ctx);
	}
}
