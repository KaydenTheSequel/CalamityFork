using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[]
{
	EquipType.HandsOn,
	EquipType.HandsOff
})]
public class GloveOfRecklessness : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 36;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.accessory = true;
		base.Item.rare = 7;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.gloveOfRecklessness = true;
		calamityPlayer.stealthGenStandstill += 0.15f;
		calamityPlayer.stealthGenMoving += 0.15f;
		player.GetDamage<RogueDamageClass>() -= 0.1f;
		player.GetCritChance<RogueDamageClass>() -= 5f;
		player.GetAttackSpeed<RogueDamageClass>() += 0.15f;
	}
}
