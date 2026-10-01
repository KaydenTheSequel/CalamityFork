using System;

namespace CalamityMod.NPCs;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class PierceResistExceptionAttribute : Attribute
{
	public bool OnlyForSingleHitbox { get; }

	public PierceResistExceptionAttribute(bool onlyForSingleHitbox = false)
	{
		OnlyForSingleHitbox = onlyForSingleHitbox;
	}
}
