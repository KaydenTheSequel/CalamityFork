using System;
using System.Collections.Generic;
using MonoMod.Cil;

namespace CalamityMod.ILEditing;

internal sealed class ManipulatorBatch : IDisposable
{
	private readonly List<Manipulator> manipulators = new List<Manipulator>();

	public required Action<Manipulator> ManipulatorProvider { get; init; }

	public void Add(Manipulator manipulator)
	{
		manipulators.Add(manipulator);
	}

	public void Dispose()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Expected O, but got Unknown
		List<Manipulator> theManipulators = manipulators;
		ManipulatorProvider?.Invoke((Manipulator)delegate(ILContext il)
		{
			foreach (Manipulator item in theManipulators)
			{
				if (item != null)
				{
					item.Invoke(il);
				}
			}
		});
	}

	public static ManipulatorBatch From(Action<Manipulator> manipulatorProvider)
	{
		ArgumentNullException.ThrowIfNull(manipulatorProvider, "manipulatorProvider");
		return new ManipulatorBatch
		{
			ManipulatorProvider = manipulatorProvider
		};
	}
}
