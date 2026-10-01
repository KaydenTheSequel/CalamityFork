using CalamityMod.Items.BaseItems;
using CalamityMod.Items.Materials;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class AbyssalDivingSuit : TransformationAccessory, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override string AssetPath => "CalamityMod/Items/Accessories/";

	public override (EquipType, string, string)[] EquipSlots => new(EquipType, string, string)[4]
	{
		(EquipType.Head, "AbyssalDivingSuit", null),
		(EquipType.Body, "AbyssalDivingSuit", null),
		(EquipType.Legs, "AbyssalDivingSuit", null),
		(EquipType.Face, null, null)
	};

	public override (SoundStyle sound, int delay)? HurtSound(Player p)
	{
		return (SoundID.NPCHit4, 10);
	}

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().abyssalDivingSuit = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AbyssalDivingGear>().AddIngredient<AnechoicPlating>().AddIngredient(3467, 5)
			.AddIngredient<MolluskHusk>(15)
			.AddTile(134)
			.Register();
	}
}
