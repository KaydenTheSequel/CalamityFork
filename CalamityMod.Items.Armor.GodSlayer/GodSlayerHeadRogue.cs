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
[LegacyName(new string[] { "GodSlayerMask" })]
public class GodSlayerHeadRogue : ModItem, ILocalizedModType, IModType
{
	public static float RogueDamageBoost = 0.1f;

	public static int RogueCritBoost = 12;

	public static float MoveSpeedBoost = 0.05f;

	public static float SetBonusRogueStealth = 1.2f;

	public static float RogueDamageBoostAtFullHealth = 0.1f;

	public static int RogueCritBoostAtFullHealth = 10;

	public static float RogueVelocityBoostAtFullHealth = 0.1f;

	public static int SetBonusHurtDamageThreshold = 80;

	public static int ExtraIFrames = 30;

	public new string LocalizationCategory => "Items.Armor.PostMoonLord";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(RogueDamageBoost.ToPercent(), RogueCritBoost, MoveSpeedBoost.ToPercent());

	public override void SetDefaults()
	{
		base.Item.width = 18;
		base.Item.height = 18;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.defense = 30;
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
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer modPlayer = player.Calamity();
		modPlayer.godSlayer = true;
		modPlayer.godSlayerThrowing = true;
		modPlayer.rogueStealthMax += SetBonusRogueStealth;
		modPlayer.wearingRogueArmor = true;
		string hotkey = CalamityKeybinds.GodSlayerDashHotKey.TooltipHotkeyString();
		player.setBonus = this.GetLocalization("SetBonus").Format(SetBonusRogueStealth.ToStealth(), RogueDamageBoostAtFullHealth.ToPercent(), SetBonusHurtDamageThreshold, hotkey, GodSlayerChestplate.DashCooldown.FramesToSeconds());
		player.setBonus = player.setBonus.Replace("ff00ff", DevourerofGodsHead.SpecialMoveColor.Hex3());
		if (modPlayer.godSlayerDashHotKeyPressed || (player.dashDelay != 0 && modPlayer.LastUsedDashID == GodslayerArmorDash.ID))
		{
			modPlayer.DeferredDashID = GodslayerArmorDash.ID;
			player.dash = 0;
		}
	}

	public override void UpdateEquip(Player player)
	{
		player.GetDamage<ThrowingDamageClass>() += RogueDamageBoost;
		player.GetCritChance<ThrowingDamageClass>() += RogueCritBoost;
		player.moveSpeed += MoveSpeedBoost;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CosmiliteBar>(7).AddIngredient<AscendantSpiritEssence>(2).AddTile<CosmicAnvil>()
			.SortBeforeFirstRecipesOf(ModContent.ItemType<GodSlayerChestplate>())
			.Register();
	}
}
