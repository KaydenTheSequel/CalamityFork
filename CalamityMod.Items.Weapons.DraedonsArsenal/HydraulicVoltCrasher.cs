using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.DraedonsArsenal;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.DraedonsArsenal;

public class HydraulicVoltCrasher : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.DraedonsArsenal";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.Calamity();
		base.Item.width = 56;
		base.Item.height = 24;
		base.Item.damage = 75;
		base.Item.knockBack = 12f;
		base.Item.useTime = 4;
		base.Item.useAnimation = 16;
		base.Item.hammer = 100;
		base.Item.useStyle = 5;
		base.Item.shootSpeed = 46f;
		base.Item.UseSound = SoundID.Item23;
		base.Item.shoot = ModContent.ProjectileType<HydraulicVoltCrasherProjectile>();
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Item.channel = true;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 2);
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			base.Item.hammer = 0;
		}
		else
		{
			base.Item.hammer = 100;
		}
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void HoldItem(Player player)
	{
		if (Main.myPlayer == player.whoAmI)
		{
			player.Calamity().rightClickListener = true;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(8).AddIngredient<DubiousPlating>(12).AddRecipeGroup("AnyMythrilBar", 10)
			.AddIngredient(549, 20)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(2, out var condition), condition)
			.AddTile(134)
			.Register();
	}
}
