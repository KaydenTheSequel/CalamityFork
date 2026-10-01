using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Mounts;

[LegacyName(new string[] { "BirdSeed" })]
public class FollyFeed : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Mounts";

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 36;
		base.Item.useAnimation = (base.Item.useTime = 20);
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.NPCHit51;
		base.Item.noMelee = true;
		base.Item.mountType = ModContent.MountType<BUMBLEDOGE>();
		base.Item.value = Item.sellPrice(0, 5);
		base.Item.rare = 8;
		base.Item.Calamity().donorItem = true;
	}
}
