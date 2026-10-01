using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Plaguebringer;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class PlaguebringerCarapace : ModItem, ILocalizedModType, IModType
{
	public static int MinionSlotBoost = 1;

	public static float SummonDamageBoost = 0.15f;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MinionSlotBoost, SummonDamageBoost.ToPercent());

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			int equipSlot = EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Body);
			ArmorIDs.Body.Sets.HidesTopSkin[equipSlot] = true;
			ArmorIDs.Body.Sets.HidesArms[equipSlot] = true;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.defense = 13;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.Calamity().donorItem = true;
	}

	public override void UpdateEquip(Player player)
	{
		player.Calamity().plaguebringerCarapace = true;
		player.maxMinions += MinionSlotBoost;
		player.GetDamage<SummonDamageClass>() += SummonDamageBoost;
		player.buffImmune[ModContent.BuffType<Plague>()] = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2362).AddIngredient<AlchemicalDecanter>(2).AddIngredient<InfectedArmorPlating>(7)
			.AddIngredient<PlagueCellCanister>(7)
			.AddTile(134)
			.Register();
	}
}
