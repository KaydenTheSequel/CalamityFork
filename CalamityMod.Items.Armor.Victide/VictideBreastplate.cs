using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Victide;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class VictideBreastplate : ModItem, IBulkyArmor, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public string BulkTexture => "CalamityMod/Items/Armor/Victide/VictideBreastplate_Bulk";

	public override void Load()
	{
		if (Main.netMode != 2)
		{
			EquipLoader.AddEquipTexture(base.Mod, "CalamityMod/Items/Armor/Victide/VictideFaulds_Waist", EquipType.Waist, null, "VictideFaulds");
		}
	}

	public override void SetStaticDefaults()
	{
		if (Main.netMode != 2)
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
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.defense = 5;
	}

	public override void UpdateEquip(Player player)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		player.endurance += 0.05f;
		player.GetCritChance<GenericDamageClass>() += 5f;
		if (Collision.DrownCollision(player.position, player.width, player.height, player.gravDir))
		{
			player.statDefense += 5;
			player.endurance += 0.1f;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SeaRemains>(5).AddTile(16).Register();
	}
}
