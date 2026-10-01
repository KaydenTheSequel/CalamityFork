using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Mounts;

[LegacyName(new string[] { "SquishyBeanMount" })]
public class SuspiciousLookingJellyBean : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Mounts";

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item3;
		base.Item.noMelee = true;
		base.Item.mountType = ModContent.MountType<SquishyBean>();
		base.Item.value = Item.sellPrice(0, 5);
		base.Item.rare = 8;
		base.Item.Calamity().devItem = true;
	}
}
