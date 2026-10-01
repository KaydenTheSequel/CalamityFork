using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing;

public abstract class BaseQuestFish : ModItem, ILocalizedModType, IModType
{
	public virtual bool QuestCondition => true;

	public virtual LocalizedText Location => LocalizedText.Empty;

	public new string LocalizationCategory => "Items.Fishing";

	public override LocalizedText Tooltip => CalamityUtils.GetText("Items.Fishing.LocationTooltip").WithFormatArgs(Location);

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 2;
		ItemID.Sets.CanBePlacedOnWeaponRacks[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToQuestFish();
	}

	public override bool IsQuestFish()
	{
		return true;
	}

	public override bool IsAnglerQuestAvailable()
	{
		return QuestCondition;
	}

	public override void AnglerQuestChat(ref string description, ref string catchLocation)
	{
		description = this.GetLocalizedValue("QuestDescription");
		catchLocation = Location.ToString();
	}
}
