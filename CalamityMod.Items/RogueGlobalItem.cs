using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items;

public sealed class RogueGlobalItem : GlobalItem
{
	internal float StealthStrikePrefixBonus;

	public override bool InstancePerEntity => true;

	public override GlobalItem Clone(Item from, Item to)
	{
		RogueGlobalItem obj = (RogueGlobalItem)base.Clone(from, to);
		obj.StealthStrikePrefixBonus = StealthStrikePrefixBonus;
		return obj;
	}

	public override bool AppliesToEntity(Item entity, bool lateInstantiation)
	{
		return entity.CountsAsClass<RogueDamageClass>();
	}

	public override void PreReforge(Item item)
	{
		StealthStrikePrefixBonus = 0f;
	}
}
