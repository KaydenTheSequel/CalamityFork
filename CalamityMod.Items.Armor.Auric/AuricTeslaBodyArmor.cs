using System.Collections.Generic;
using CalamityMod.Items.Armor.Bloodflare;
using CalamityMod.Items.Armor.GodSlayer;
using CalamityMod.Items.Armor.Silva;
using CalamityMod.Items.Armor.Tarragon;
using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Auric;

[AutoloadEquip(new EquipType[] { EquipType.Body })]
public class AuricTeslaBodyArmor : ModItem, ILocalizedModType, IModType
{
	public static int setBonusTooltipNumber;

	public static bool holdingShift;

	public static Color tooltipTarragonColor;

	public static Color tooltipBloodflareColor;

	public static Color tooltipSilvaColor;

	public static Color tooltipGodslayerColor;

	public static float DamageBoost;

	public static int CritBoost;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(DamageBoost.ToPercent(), CritBoost);

	public override void Load()
	{
		if (!Main.dedServ)
		{
			EquipLoader.AddEquipTexture(base.Mod, "CalamityMod/Items/Armor/Auric/AuricTeslaBodyArmor_Back", EquipType.Back, this);
		}
	}

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 34;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.defense = 44;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<GenericDamageClass>() += DamageBoost;
		player.GetCritChance<GenericDamageClass>() += CritBoost;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		if (Main.LocalPlayer.Calamity().auricSet)
		{
			Main.LocalPlayer.armor[0].ModItem.ModifyTooltips(tooltips);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<GodSlayerChestplate>().AddIngredient<BloodflareBodyArmor>().AddIngredient<TarragonBreastplate>()
			.AddIngredient<AuricBar>(18)
			.AddTile<CosmicAnvil>()
			.Register();
		CreateRecipe().AddIngredient<SilvaArmor>().AddIngredient<BloodflareBodyArmor>().AddIngredient<TarragonBreastplate>()
			.AddIngredient<AuricBar>(18)
			.AddTile<CosmicAnvil>()
			.Register();
	}

	static AuricTeslaBodyArmor()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		setBonusTooltipNumber = 0;
		holdingShift = false;
		tooltipTarragonColor = new Color(194, 255, 194);
		tooltipBloodflareColor = new Color(255, 195, 194);
		tooltipSilvaColor = new Color(246, 255, 194);
		tooltipGodslayerColor = new Color(204, 194, 255);
		DamageBoost = 0.16f;
		CritBoost = 10;
	}
}
