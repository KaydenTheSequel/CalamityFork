using CalamityMod.CalPlayer.Dashes;
using CalamityMod.Projectiles.Typeless;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Typeless;

public class RelicOfDeliverance : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Typeless";

	public override void SetDefaults()
	{
		base.Item.width = 32;
		base.Item.height = 46;
		base.Item.damage = 1700;
		base.Item.useAnimation = (base.Item.useTime = 35);
		base.Item.reuseDelay = 15;
		base.Item.useStyle = 5;
		base.Item.DamageType = AverageDamageClass.Instance;
		base.Item.UseSound = SoundID.Item46;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.shoot = ModContent.ProjectileType<RelicOfDeliveranceSpear>();
		base.Item.Calamity().CannotBeEnchanted = true;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override void ModifyResearchSorting(ref ContentSamples.CreativeHelper.ItemGroup itemGroup)
	{
		itemGroup = (ContentSamples.CreativeHelper.ItemGroup)820;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 55f;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] <= 0)
		{
			return player.Calamity().DashID != GodslayerArmorDash.ID;
		}
		return false;
	}
}
