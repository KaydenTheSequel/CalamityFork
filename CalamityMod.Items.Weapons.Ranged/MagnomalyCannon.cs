using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class MagnomalyCannon : ModItem, ILocalizedModType, IModType
{
	public static int AmmoSavedPercent = 50;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetDefaults()
	{
		base.Item.width = 84;
		base.Item.height = 30;
		base.Item.damage = 279;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 15;
		base.Item.useAnimation = 15;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 9.5f;
		base.Item.UseSound = SoundID.Item11;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<MagnomalyRocket>();
		base.Item.shootSpeed = 15f;
		base.Item.useAmmo = AmmoID.Rocket;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-30f, -10f);
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		type = base.Item.shoot;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return Main.rand.Next(100) >= AmmoSavedPercent;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ThePack>().AddIngredient<ScorchedEarth>().AddIngredient(2796)
			.AddIngredient<MiracleMatter>()
			.AddTile<DraedonsForge>()
			.Register();
	}
}
