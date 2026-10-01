using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class ScarletDevil : RogueWeapon
{
	public override void SetDefaults()
	{
		base.Item.width = 108;
		base.Item.height = 108;
		base.Item.damage = 10000;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = (base.Item.useTime = 60);
		base.Item.useStyle = 1;
		base.Item.knockBack = 8f;
		base.Item.UseSound = SoundID.Item60;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<ScarletDevilProjectile>();
		base.Item.shootSpeed = 30f;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		int proj = Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI);
		if (Main.projectile.IndexInRange(proj))
		{
			Main.projectile[proj].Calamity().stealthStrike = player.Calamity().StealthStrikeAvailable();
		}
		return false;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 20f;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Wrathwing>().AddIngredient<RealityRupture>().AddIngredient<ShadowspecBar>(5)
			.AddIngredient<BloodstoneCore>(15)
			.AddIngredient(521, 15)
			.AddTile<DraedonsForge>()
			.Register();
	}
}
