using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.DraedonsArsenal;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.DraedonsArsenal;

public class Phaseslayer : ModItem, ILocalizedModType, IModType
{
	public const float SizeChargeThreshold = 0.25f;

	public const float SmallDamageMultiplier = 0.9f;

	public new string LocalizationCategory => "Items.Weapons.DraedonsArsenal";

	public override void SetDefaults()
	{
		base.Item.Calamity();
		base.Item.width = 26;
		base.Item.height = 26;
		base.Item.damage = 1350;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.useTime = 24;
		base.Item.useAnimation = 24;
		base.Item.useStyle = 5;
		base.Item.useTurn = false;
		base.Item.knockBack = 7f;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<PhaseslayerProjectile>();
		base.Item.channel = true;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = Projectile.NewProjectileDirect(source, position, Vector2.Zero, type, damage, knockback, player.whoAmI);
		projectile.rotation = projectile.AngleTo(Main.MouseWorld);
		projectile.netUpdate = true;
		return false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 5);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(15).AddIngredient<DubiousPlating>(25).AddIngredient<CosmiliteBar>(8)
			.AddIngredient<AscendantSpiritEssence>(2)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(5, out var condition), condition)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
