using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Mounts;

public class ExoThrone : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Mounts";

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 34;
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item94;
		base.Item.noMelee = true;
		base.Item.mountType = ModContent.MountType<DraedonGamerChairMount>();
		base.Item.value = Item.sellPrice(0, 5);
		base.Item.rare = 8;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.IntegrateHotkey(CalamityKeybinds.ExoChairSlowdownHotkey);
	}
}
