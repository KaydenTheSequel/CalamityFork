using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Face })]
public class MoonstoneCrown : ModItem, ILocalizedModType, IModType
{
	internal static int BaseDamage = 75;

	public new string LocalizationCategory => "Items.Accessories";

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			int equipSlot = EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Face);
			ArmorIDs.Face.Sets.OverrideHelmet[equipSlot] = true;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 46;
		base.Item.height = 40;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.rogueVelocity += 0.15f;
		calamityPlayer.moonCrown = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<FeatherCrown>().AddIngredient(3467, 5).AddIngredient<GalacticaSingularity>(5)
			.AddTile(412)
			.Register();
	}
}
