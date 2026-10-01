using CalamityMod.UI.DialogueDisplay.DisplayEffects;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.UI.DialogueDisplay;

public class DialogueDisplayDebugItem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Debug";

	public override string Texture => "CalamityMod/Items/Weapons/Summon/StaffOfNecrosteocytes";

	public override void SetDefaults()
	{
		base.Item.width = 25;
		base.Item.height = 29;
		base.Item.rare = 10;
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.useStyle = 4;
	}

	public override bool? UseItem(Player player)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		int slot = DialogueDisplaySystem.GetSlot("OceanBlue");
		if (slot != -1)
		{
			DialogueDisplaySystem.ProgressDialogue(slot);
		}
		else
		{
			DialogueDisplaySystem.StartDialogue("OceanBlue", player.Center, 0, -1, progressDialogue: true, new WhisperingPearlEffects());
		}
		return true;
	}
}
