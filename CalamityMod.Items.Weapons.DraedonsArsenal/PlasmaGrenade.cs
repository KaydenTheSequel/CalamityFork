using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Projectiles.DraedonsArsenal;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.DraedonsArsenal;

public class PlasmaGrenade : RogueWeapon, ILocalizedModType, IModType
{
	public static readonly SoundStyle ExplosionSound = new SoundStyle("CalamityMod/Sounds/Item/PlasmaGrenadeExplosion");

	public new string LocalizationCategory => "Items.Weapons.DraedonsArsenal";

	public override float StealthVelocityMultiplier => 1.2f;

	public override float StealthKnockbackMultiplier => 1.5f;

	public override void SetDefaults()
	{
		base.Item.Calamity();
		base.Item.width = 22;
		base.Item.height = 28;
		base.Item.damage = 860;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.consumable = false;
		base.Item.useStyle = 1;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.knockBack = 3f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.shoot = ModContent.ProjectileType<PlasmaGrenadeProjectile>();
		base.Item.shootSpeed = 11f;
		base.Item.DamageType = RogueDamageClass.Instance;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (proj.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[proj].Calamity().stealthStrike = player.Calamity().StealthStrikeAvailable();
		}
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
