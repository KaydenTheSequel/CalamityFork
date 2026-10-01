using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Empyrean;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
[LegacyName(new string[] { "XerocPlateMail" })]
public class EmpyreanCloak : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.12f;

	public static int RogueCritBoost = 8;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent(), RogueCritBoost);

	public override void Load()
	{
		if (!Main.dedServ)
		{
			EquipLoader.AddEquipTexture(base.Mod, "CalamityMod/Items/Armor/Empyrean/EmpyreanCloak_Neck", EquipType.Neck, this);
			EquipLoader.AddEquipTexture(base.Mod, "CalamityMod/Items/Armor/Empyrean/EmpyreanCloak_Back", EquipType.Back, this);
		}
	}

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			int equipSlot = EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Body);
			ArmorIDs.Body.Sets.HidesArms[equipSlot] = true;
			ArmorIDs.Body.Sets.HidesTopSkin[equipSlot] = true;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.defense = 24;
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
		player.GetCritChance<ThrowingDamageClass>() += RogueCritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MeldConstruct>(20).AddIngredient(3467, 16).AddTile(412)
			.Register();
	}
}
