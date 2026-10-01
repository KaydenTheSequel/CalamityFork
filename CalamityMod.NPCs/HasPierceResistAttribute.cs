using System;

namespace CalamityMod.NPCs;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class HasPierceResistAttribute : Attribute
{
	public bool SingleHitbox { get; }

	public HasPierceResistAttribute(bool singleHitbox = false)
	{
		SingleHitbox = singleHitbox;
	}
}
