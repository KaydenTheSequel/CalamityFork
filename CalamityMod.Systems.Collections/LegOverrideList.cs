using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Collections;

public sealed class LegOverrideList : ModSystem
{
	public static IList<int> List { get; private set; }

	public override void OnModLoad()
	{
		int num = 3;
		List<int> list = new List<int>(num);
		CollectionsMarshal.SetCount(list, num);
		Span<int> span = CollectionsMarshal.AsSpan(list);
		int num2 = 0;
		span[num2] = EquipLoader.GetEquipSlot(CalamityMod.Instance, "ProfanedSoulCrystal", EquipType.Legs);
		num2++;
		span[num2] = EquipLoader.GetEquipSlot(CalamityMod.Instance, "AquaticHeart", EquipType.Legs);
		num2++;
		span[num2] = EquipLoader.GetEquipSlot(CalamityMod.Instance, "Popo", EquipType.Legs);
		List = list;
	}

	public override void Unload()
	{
		List = null;
	}

	public static bool Includes(int equipSlot)
	{
		return List.Contains(equipSlot);
	}
}
