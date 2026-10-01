using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class ChaosStone : ModItem, ILocalizedModType, IModType
{
	public static float LostRegenPer100Mana => 8f;

	public static float DamageMultPer100Mana => 0.04f;

	public new string LocalizationCategory => "Items.Accessories";

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		float regenAmount = ((Main.LocalPlayer.statManaMax2 == 0) ? 0f : ((float)(int)((float)Main.LocalPlayer.statManaMax2 / 100f * LostRegenPer100Mana) * 0.5f));
		float dmgAmount = ((Main.LocalPlayer.statManaMax2 == 0) ? 0f : ((float)Main.LocalPlayer.statManaMax2 / 100f * DamageMultPer100Mana * 100f));
		tooltips.FindAndReplaceAll("[REGEN]", regenAmount.ToString("0.#"));
		tooltips.FindAndReplaceAll("[DAMAGE]", dmgAmount.ToString("0.#"));
	}

	public override void SetStaticDefaults()
	{
		Main.RegisterItemAnimation(base.Item.type, new DrawAnimationVertical(8, 7));
		ItemID.Sets.AnimatesAsSoul[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 20;
		base.Item.height = 20;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().ChaosStone = true;
	}
}
