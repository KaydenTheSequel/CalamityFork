using System.Collections.Generic;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee.Spears;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "SpatialLance", "ElementalLance" })]
public class VanishingPoint : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 88;
		base.Item.height = 88;
		base.Item.damage = 240;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.noMelee = true;
		base.Item.useTurn = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 45;
		base.Item.useStyle = 5;
		base.Item.useTime = 45;
		base.Item.knockBack = 9.5f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.shoot = ModContent.ProjectileType<VanishingPointProjectile>();
		base.Item.shootSpeed = 12f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		tooltips.FindAndReplace("[GFB]", this.GetLocalizedValue(Main.zenithWorld ? "TooltipGFB" : "TooltipNormal"));
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (!Main.zenithWorld)
		{
			return 1f;
		}
		return 2f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BotanicPiercer>().AddIngredient(3467, 5).AddIngredient<LifeAlloy>(5)
			.AddIngredient(3458, 5)
			.AddTile(134)
			.Register();
	}
}
