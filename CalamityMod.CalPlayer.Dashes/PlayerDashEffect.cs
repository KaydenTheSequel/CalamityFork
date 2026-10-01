using System.Runtime.CompilerServices;
using CalamityMod.Enums;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.CalPlayer.Dashes;

public abstract class PlayerDashEffect : ModType
{
	[CompilerGenerated]
	private string _003CDashID_003Ek__BackingField;

	public static string ID { get; }

	public int dashTime { get; set; }

	public virtual int dashStartup => 0;

	public virtual string DashID => _003CDashID_003Ek__BackingField ?? (_003CDashID_003Ek__BackingField = base.FullName);

	public int DashTimeAdjustedForStartup => dashTime - dashStartup;

	public abstract DashCollisionType CollisionType { get; }

	public abstract bool IsOmnidirectional { get; }

	public abstract float CalculateDashSpeed(Player player);

	public virtual void OnDashEffects(Player player)
	{
	}

	public virtual void OnDashStartupEffects(Player player)
	{
	}

	public virtual void DashStartupEffects(Player player)
	{
	}

	public virtual void MidDashEffects(Player player, ref float dashSpeed, ref float dashSpeedDecelerationFactor, ref float runSpeedDecelerationFactor)
	{
	}

	public virtual void OnHitEffects(Player player, NPC npc, IEntitySource source, ref DashHitContext hitContext)
	{
	}

	protected sealed override void Register()
	{
		ModTypeLookup<PlayerDashEffect>.Register(this);
		PlayerDashManager.DashIdentificationTable[DashID] = this;
	}
}
