using CalamityMod.Buffs.StatBuffs;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class TrinketofChi : ModItem, ILocalizedModType, IModType
{
	internal const int ChiBuffTimerMax = 600;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 34;
		base.Item.height = 32;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().trinketOfChi = true;
		if (player.whoAmI != Main.myPlayer && player.miscCounter % 10 == 0 && Main.LocalPlayer.team == player.team && player.team != 0)
		{
			Main.LocalPlayer.AddBuff(ModContent.BuffType<ChiRegenBuff>(), 20);
		}
	}
}
