using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using CalamityMod.Sounds;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class TyrannysEnd : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 150;
		base.Item.height = 48;
		base.Item.damage = 2150;
		base.Item.knockBack = 9.5f;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 60;
		base.Item.useAnimation = 60;
		base.Item.shoot = 242;
		base.Item.shootSpeed = 12f;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.UseSound = CommonCalamitySounds.LargeWeaponFireSound;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.Calamity().donorItem = true;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 35f;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-28f, 0f);
	}

	public override void HoldItem(Player player)
	{
		player.scope = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<RubicoPrime>().AddIngredient<AntiMaterielRifle>().AddIngredient<AuricBar>(5)
			.AddIngredient<LifeAlloy>(3)
			.AddTile<CosmicAnvil>()
			.Register();
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (type == 14)
		{
			type = ModContent.ProjectileType<PiercingBullet>();
		}
	}
}
