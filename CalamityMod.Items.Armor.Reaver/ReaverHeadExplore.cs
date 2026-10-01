using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.Reaver;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "ReaverHeadgear" })]
public class ReaverHeadExplore : ModItem, ILocalizedModType, IModType
{
	public static float MiningSpeedBoost = 0.2f;

	public static float PlacementSpeedBoost = 0.5f;

	public static int SetBonusAggroReduction = 400;

	public static int SetBonusTileRangeBoost = 7;

	public static int SetBonusGrabRangeBoost = 246;

	public new string LocalizationCategory => "Items.Armor.Hardmode";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MiningSpeedBoost.ToPercent(), PlacementSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 22;
		base.Item.height = 22;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.defense = 6;
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<ReaverScaleMail>())
		{
			return legs.type == ModContent.ItemType<ReaverCuisses>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadowSubtle = true;
		player.armorEffectDrawOutlines = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusTileRangeBoost);
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.reaverExplore = true;
		calamityPlayer.wearingRogueArmor = true;
		player.findTreasure = true;
		player.aggro -= SetBonusAggroReduction;
		if (player.Calamity().countsAsAnyWet)
		{
			player.gills = true;
		}
		DelegateMethods.v3_1 = new Vector3(1f, 1f, 1f);
		Utils.PlotTileLine(player.Center, player.Center + player.velocity * 6f, 20f, DelegateMethods.CastLightOpen);
		Utils.PlotTileLine(player.Left, player.Right, 20f, DelegateMethods.CastLightOpen);
		if (player.whoAmI == Main.myPlayer)
		{
			Player.tileRangeX += SetBonusTileRangeBoost;
			Player.tileRangeY += SetBonusTileRangeBoost;
		}
	}

	public override void UpdateEquip(Player player)
	{
		player.ignoreWater = true;
		player.lavaImmune = true;
		player.pickSpeed -= MiningSpeedBoost;
		player.tileSpeed += PlacementSpeedBoost;
		player.wallSpeed += PlacementSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PerennialBar>(7).AddIngredient<LivingShard>().AddTile(134)
			.SortBeforeFirstRecipesOf(ModContent.ItemType<ReaverCuisses>())
			.Register();
	}
}
