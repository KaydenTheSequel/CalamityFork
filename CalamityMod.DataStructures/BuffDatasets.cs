using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.DataStructures;

[ReinitializeDuringResizeArrays]
public static class BuffDatasets
{
	public static DebuffData[] DebuffDataset = BuffID.Sets.Factory.CreateNamedSet("DebuffData").Description("Stores DebuffData for a particular debuff").RegisterCustomSet<DebuffData>(null, new object[24]
	{
		24,
		DebuffData.OnFire,
		323,
		DebuffData.Hellfire,
		39,
		DebuffData.CursedInferno,
		153,
		DebuffData.Shadowflame,
		189,
		DebuffData.Daybroken,
		67,
		DebuffData.Burning,
		44,
		DebuffData.Frostburn,
		324,
		DebuffData.Frostbite,
		20,
		DebuffData.Poisoned,
		70,
		DebuffData.AcidVenom,
		144,
		DebuffData.Electrified,
		204,
		DebuffData.Oiled
	});
}
