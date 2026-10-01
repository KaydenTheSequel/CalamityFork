using System;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class GladiatorsLocket : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 48;
		base.Item.height = 54;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		float statPower = (float)Math.Round(0.2f * Utils.GetLerpValue(1f, 0.5f, (float)player.statLife / (float)player.statLifeMax2, clamped: true), 2);
		player.Calamity().gladiatorSword = true;
		player.GetDamage<GenericDamageClass>() += statPower;
		player.moveSpeed += statPower;
	}
}
