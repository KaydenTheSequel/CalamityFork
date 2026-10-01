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

[LegacyName(new string[] { "AMR" })]
public class AntiMaterielRifle : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 154;
		base.Item.height = 40;
		base.Item.damage = 1818;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = (base.Item.useAnimation = 60);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 9.5f;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.UseSound = CommonCalamitySounds.LargeWeaponFireSound;
		base.Item.autoReuse = true;
		base.Item.shoot = 14;
		base.Item.shootSpeed = 12f;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-30f, 0f);
	}

	public override void HoldItem(Player player)
	{
		player.scope = true;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 20f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Shroomer>().AddIngredient<CosmiliteBar>(8).AddIngredient<NightmareFuel>(20)
			.AddTile<CosmicAnvil>()
			.Register();
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		if (type == 14)
		{
			type = ModContent.ProjectileType<AMRShot>();
		}
	}
}
