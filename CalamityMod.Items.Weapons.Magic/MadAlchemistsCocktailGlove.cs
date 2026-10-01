using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class MadAlchemistsCocktailGlove : ModItem, ILocalizedModType, IModType
{
	private int flaskIndex;

	private static int[] flaskIDs;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
		flaskIDs = new int[5]
		{
			ModContent.ProjectileType<MadAlchemistsCocktailRed>(),
			ModContent.ProjectileType<MadAlchemistsCocktailBlue>(),
			ModContent.ProjectileType<MadAlchemistsCocktailGreen>(),
			ModContent.ProjectileType<MadAlchemistsCocktailPurple>(),
			ModContent.ProjectileType<MadAlchemistsCocktailAlt>()
		};
	}

	public override void SetDefaults()
	{
		base.Item.width = 26;
		base.Item.height = 36;
		base.Item.damage = 240;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.noUseGraphic = true;
		base.Item.mana = 12;
		base.Item.useTime = 15;
		base.Item.useAnimation = 15;
		base.Item.useStyle = 1;
		base.Item.noMelee = true;
		base.Item.knockBack = 2f;
		base.Item.UseSound = SoundID.Item106;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<MadAlchemistsCocktailRed>();
		base.Item.shootSpeed = 19f;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.Calamity().donorItem = true;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (player.altFunctionUse == 2)
		{
			type = flaskIDs[4];
			return;
		}
		type = flaskIDs[flaskIndex++];
		if (flaskIndex > 3)
		{
			flaskIndex = 0;
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3105).AddIngredient(126, 15).AddIngredient(259, 5)
			.AddIngredient<EffulgentFeather>(5)
			.AddIngredient<CoreofCalamity>()
			.AddTile(355)
			.Register();
	}
}
