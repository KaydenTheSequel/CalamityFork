using System;

namespace CalamityMod.Projectiles.Damageable;

[Flags]
public enum DamageSourceType
{
	HostileProjectiles = 2,
	FriendlyProjectiles = 4,
	HostileNPCs = 8
}
