using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class LightGodsBrilliance : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 48;
		base.Item.damage = 111;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 6;
		base.Item.useAnimation = (base.Item.useTime = 6);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.Calamity().donorItem = true;
		base.Item.UseSound = SoundID.Item9;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<LightBead>();
		base.Item.shootSpeed = 25f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 beadVelocity = velocity + Main.rand.NextVector2Square(-2.5f, 2.5f);
		Projectile.NewProjectile(source, position, beadVelocity, type, damage, knockback, player.whoAmI);
		if (Main.rand.NextBool(3))
		{
			Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<LightBall>(), damage * 2, knockback, player.whoAmI);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ShadecrystalBarrage>().AddIngredient<AbyssalTome>().AddIngredient(422, 10)
			.AddIngredient<CosmiliteBar>(8)
			.AddIngredient<NightmareFuel>(20)
			.AddIngredient<EffulgentFeather>(5)
			.AddIngredient(520, 30)
			.AddTile(101)
			.Register();
	}
}
