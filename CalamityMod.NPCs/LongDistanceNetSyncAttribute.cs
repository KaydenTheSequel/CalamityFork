using System;

namespace CalamityMod.NPCs;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class LongDistanceNetSyncAttribute : Attribute
{
	public Type SyncWith { get; set; }
}
