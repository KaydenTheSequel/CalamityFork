using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items;

internal sealed class GrabRangeGlobalItem : GlobalItem
{
	public float grabRangeMultiplier = 1f;

	public override bool InstancePerEntity => true;

	public override bool AppliesToEntity(Item entity, bool lateInstantiation)
	{
		int type = entity.type;
		if ((uint)(type - 71) <= 3u)
		{
			return true;
		}
		return false;
	}

	public override GlobalItem Clone(Item item, Item itemClone)
	{
		GrabRangeGlobalItem obj = (GrabRangeGlobalItem)base.Clone(item, itemClone);
		obj.grabRangeMultiplier = grabRangeMultiplier;
		return obj;
	}
}
