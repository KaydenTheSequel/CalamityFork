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
[LegacyName(new string[] { "GodSlayerHelm" })]
public class GodSlayerHeadMelee : ModItem, ILocalizedModType, IModType
{
	public static float MeleeDamageBoost = 0.1f;

	public static int MeleeCritBoost = 5;

	public static float MeleeSpeedBoost = 0.2f;

	public static int SetBonusAggroBoost = 1000;

	public static int SetBonusHurtDamageThreshold = 80;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(MeleeDamageBoost.ToPercent(), MeleeCritBoost, MeleeSpeedBoost.ToPercent());

	public static int DartDamage => 350.ScaleWithDifficulty();

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.defense = 50;
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
		modPlayer.godSlayerDamage = true;
		string hotkey = CalamityKeybinds.GodSlayerDashHotKey.TooltipHotkeyString();
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusHurtDamageThreshold, hotkey, GodSlayerChestplate.DashCooldown.FramesToSeconds());
		player.setBonus = player.setBonus.Replace("ff00ff", DevourerofGodsHead.SpecialMoveColor.Hex3());
		player.aggro += SetBonusAggroBoost;
		if (modPlayer.godSlayerDashHotKeyPressed || (player.dashDelay != 0 && modPlayer.LastUsedDashID == GodslayerArmorDash.ID))
		{
			modPlayer.DeferredDashID = GodslayerArmorDash.ID;
			player.dash = 0;
		}
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<MeleeDamageClass>() += MeleeDamageBoost;
		player.GetCritChance<MeleeDamageClass>() += MeleeCritBoost;
		player.GetAttackSpeed<MeleeDamageClass>() += MeleeSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CosmiliteBar>(7).AddIngredient<AscendantSpiritEssence>(2).AddTile<CosmicAnvil>()
			.SortBeforeFirstRecipesOf(ModContent.ItemType<GodSlayerChestplate>())
			.Register();
	}
}
