using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

[AutoloadEquip(new EquipType[] { EquipType.Face })]
public class OccultSkullCrown : ModItem, ILocalizedModType, IModType, IHoldShiftTooltipItem
{
	public new string LocalizationCategory => "Items.Accessories";

	public bool HasFlavorTooltip => true;

	public Color? TooltipExtensionColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(195, 223, 255);
		}
	}

	public override void SetStaticDefaults()
	{
		if (!Main.dedServ)
		{
			int equipSlot = EquipLoader.GetEquipSlot(base.Mod, Name, EquipType.Face);
			ArmorIDs.Face.Sets.PreventHairDraw[equipSlot] = true;
			ArmorIDs.Face.Sets.OverrideHelmet[equipSlot] = true;
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 82;
		base.Item.height = 62;
		base.Item.defense = 5;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.accessory = true;
		base.Item.SetRevExclusive();
	}

	public override void UpdateEquip(Player player)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.laudanum = true;
		calamityPlayer.heartOfDarkness = true;
		calamityPlayer.stressPills = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<HeartofDarkness>().AddIngredient<Laudanum>().AddIngredient<StressPills>()
			.AddIngredient<TwistingNether>(3)
			.AddTile(134)
			.Register();
	}
}
