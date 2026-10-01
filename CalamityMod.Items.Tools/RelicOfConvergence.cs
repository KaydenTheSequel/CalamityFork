using CalamityMod.Projectiles.Typeless;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class RelicOfConvergence : ModItem, ILocalizedModType, IModType
{
	public static int HealValue = 50;

	public static float IncomingDamageMultiplier = 1.5f;

	public static float DefenseMultiplier = 0.5f;

	public new string LocalizationCategory => "Items.Tools";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(HealValue);

	public override void SetStaticDefaults()
	{
		ItemID.Sets.CanBePlacedOnWeaponRacks[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 46;
		base.Item.useAnimation = (base.Item.useTime = 25);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.shoot = ModContent.ProjectileType<RelicOfConvergenceCrystal>();
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = (ContentSamples.CreativeHelper.ItemGroup)820;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] <= 0)
		{
			return player.ownedProjectileCounts[ModContent.ProjectileType<RelicOfDeliveranceSpear>()] <= 0;
		}
		return false;
	}
}
