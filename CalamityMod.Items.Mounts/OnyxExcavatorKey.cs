using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Mounts;

public class OnyxExcavatorKey : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Mounts";

	public override void SetDefaults()
	{
		base.Item.width = 16;
		base.Item.height = 16;
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.useStyle = 4;
		base.Item.UseSound = SoundID.Item23;
		base.Item.noMelee = true;
		base.Item.mountType = ModContent.MountType<OnyxExcavator>();
		base.Item.value = Item.sellPrice(0, 5);
		base.Item.rare = 8;
	}
}
