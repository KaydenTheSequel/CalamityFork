using System.Collections.Generic;
using CalamityMod.CustomRecipes;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Projectiles.DraedonsArsenal;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.DraedonsArsenal;

public class WavePounder : RogueWeapon, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.DraedonsArsenal";

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 44;
		base.Item.damage = 75;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.useAnimation = (base.Item.useTime = 56);
		base.Item.knockBack = 0.25f;
		base.Item.shoot = ModContent.ProjectileType<WavePounderProjectile>();
		base.Item.shootSpeed = 16f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.useStyle = 1;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
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
		CalamityGlobalItem.InsertKnowledgeTooltip(tooltips, 4);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MysteriousCircuitry>(12).AddIngredient<DubiousPlating>(18).AddIngredient<UelibloomBar>(8)
			.AddIngredient(3467, 4)
			.AddCondition(ArsenalTierGatedRecipe.ConstructRecipeCondition(4, out var condition), condition)
			.AddTile(134)
			.Register();
	}
}
