using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[]
{
	EquipType.HandsOn,
	EquipType.HandsOff
})]
public class GloveOfPrecision : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 40;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = 7;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.gloveOfPrecision = true;
		player.GetDamage<RogueDamageClass>() += 0.1f;
		player.GetCritChance<RogueDamageClass>() += 10f;
		calamityPlayer.rogueVelocity += 0.15f;
		player.GetAttackSpeed<RogueDamageClass>() -= 0.15f;
	}
}
