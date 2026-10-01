using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class FlurrystormCannon : ModItem, ILocalizedModType, IModType
{
	public static int AmmoSavedPercent = 50;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetDefaults()
	{
		base.Item.width = 68;
		base.Item.height = 38;
		base.Item.damage = 10;
		base.Item.useTime = 16;
		base.Item.useAnimation = 16;
		base.Item.useStyle = 5;
		base.Item.knockBack = 1.2f;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.Calamity().donorItem = true;
		base.Item.UseSound = SoundID.Item11;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.channel = true;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<FlurrystormCannonShooting>();
		base.Item.useAmmo = AmmoID.Snowball;
		base.Item.shootSpeed = 18f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] > 0)
		{
			return Main.rand.Next(100) >= AmmoSavedPercent;
		}
		return false;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		type = base.Item.shoot;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1319).AddIngredient(324).AddIngredient<AerialiteBar>(10)
			.AddIngredient(154, 10)
			.AddIngredient<PearlShard>(10)
			.AddTile(16)
			.AddCondition(Condition.NotRemixWorld)
			.Register();
		CreateRecipe().AddIngredient(725).AddIngredient(324).AddIngredient<AerialiteBar>(10)
			.AddIngredient(154, 10)
			.AddIngredient<PearlShard>(10)
			.AddTile(16)
			.AddCondition(Condition.RemixWorld)
			.Register();
	}
}
