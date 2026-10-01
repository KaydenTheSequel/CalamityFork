using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Victide;

[AutoloadEquip(new EquipType[] { EquipType.Legs })]
[LegacyName(new string[] { "VictideLeggings" })]
public class VictideGreaves : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Armor.PreHardmode";

	public override void SetStaticDefaults()
	{
		if (Main.netMode != 2)
		{
			int equipSlot = EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Legs);
			ArmorIDs.Legs.Sets.HidesBottomSkin[equipSlot] = true;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.defense = 4;
	}

	public override void UpdateEquip(Player player)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		player.moveSpeed += (Collision.DrownCollision(player.position, player.width, player.height, player.gravDir) ? 0.3f : 0.08f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<SeaRemains>(4).AddTile(16).Register();
	}
}
