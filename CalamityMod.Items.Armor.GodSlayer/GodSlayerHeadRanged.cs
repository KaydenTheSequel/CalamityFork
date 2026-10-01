using CalamityMod.CalPlayer;
using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Items.Materials;
using CalamityMod.NPCs.DevourerofGods;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Armor.GodSlayer;

[AutoloadEquip(new EquipType[] { EquipType.Head })]
[LegacyName(new string[] { "GodSlayerHelmet" })]
public class GodSlayerHeadRanged : ModItem, ILocalizedModType, IModType
{
	public static float RangedDamageBoost = 0.1f;

	public static int RangedCritBoost = 12;

	public static float AmmoReduction = 0.7f;

	public static int ShrapnelRoundCooldown = CalamityUtils.SecondsToFrames(2.5f);

	public static double ShrapnelRoundDamageRatio = 1.0;

	public static int ShrapnelRoundDamageSoftcap = 800;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RangedDamageBoost.ToPercent(), RangedCritBoost, (1f - AmmoReduction).ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.defense = 35;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override bool IsArmorSet(Item head, Item body, Item legs)
	{
		if (body.type == ModContent.ItemType<GodSlayerChestplate>())
		{
			return legs.type == ModContent.ItemType<GodSlayerLeggings>();
		}
		return false;
	}

	public override void ArmorSetShadows(Player player)
	{
		player.armorEffectDrawShadow = true;
	}

	public override void UpdateArmorSet(Player player)
	{
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = player.Calamity();
		modPlayer.godSlayer = true;
		modPlayer.godSlayerRanged = true;
		string hotkey = CalamityKeybinds.GodSlayerDashHotKey.TooltipHotkeyString();
		player.setBonus = this.GetLocalization("SetBonus").Format(ShrapnelRoundCooldown.FramesToSeconds(), hotkey, GodSlayerChestplate.DashCooldown.FramesToSeconds());
		player.setBonus = player.setBonus.Replace("ff00ff", DevourerofGodsHead.SpecialMoveColor.Hex3());
		if (modPlayer.godSlayerDashHotKeyPressed || (player.dashDelay != 0 && modPlayer.LastUsedDashID == GodslayerArmorDash.ID))
		{
			modPlayer.DeferredDashID = GodslayerArmorDash.ID;
			player.dash = 0;
		}
	}

	public override void UpdateEquip(Player player)
	{
		player.Calamity().ammoCost *= AmmoReduction;
		player.GetDamage<RangedDamageClass>() += RangedDamageBoost;
		player.GetCritChance<RangedDamageClass>() += RangedCritBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CosmiliteBar>(7).AddIngredient<AscendantSpiritEssence>(2).AddTile<CosmicAnvil>()
			.SortBeforeFirstRecipesOf(ModContent.ItemType<GodSlayerChestplate>())
			.Register();
	}
}
