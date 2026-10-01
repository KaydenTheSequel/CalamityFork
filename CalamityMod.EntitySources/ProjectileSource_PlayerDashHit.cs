using Terraria;
using Terraria.DataStructures;

namespace CalamityMod.EntitySources;

public class ProjectileSource_PlayerDashHit : IEntitySource
{
	public Player player;

	public string? Context => null;

	public ProjectileSource_PlayerDashHit(Player p)
	{
		player = p;
	}
}
