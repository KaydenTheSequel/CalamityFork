using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class DragonScales : ModItem, ILocalizedModType, IModType
{
	public static int ShitBaseDamage = 57;

	public static int TornadoBaseDamage = 210;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 34;
		base.Item.value = Item.buyPrice(15);
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().dragonScales = true;
		player.buffImmune[ModContent.BuffType<Dragonfire>()] = true;
	}
}
