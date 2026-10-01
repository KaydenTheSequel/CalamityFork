using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.DraedonsArsenal;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.DraedonsArsenal;

public class SnakeEyes : ModItem, ILocalizedModType, IModType
{
	public static float EnemyDistanceDetection = 2000f;

	public static float TimeToShoot = 30f;

	public static float ProjectileSpeed = 40f;

	public static float TimeToRedirect = 15f;

	public static float TimeToRestart = 45f;

	public new string LocalizationCategory => "Items.Weapons.DraedonsArsenal";

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 24;
		base.Item.damage = 200;
		base.Item.mana = 12;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.useStyle = 4;
		base.Item.noMelee = true;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.UseSound = SoundID.Item15;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<SnakeEyesSummon>();
		base.Item.DamageType = DamageClass.Summon;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectile(source, player.ClampedMouseWorld(), Vector2.Zero, type, damage, knockback, player.whoAmI);
		return false;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 4);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(18).AddIngredient<DubiousPlating>(12).AddIngredient<UelibloomBar>(8)
			.AddIngredient(3467, 4)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(4, out var condition), condition)
			.AddTile(134)
			.Register();
	}
}
