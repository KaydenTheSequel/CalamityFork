using CalamityMod.Projectiles.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class HarvestStaff : ModItem, ILocalizedModType, IModType
{
	public static int PumpkinsPerSentry = 5;

	public static float TimePerPumpkin = 150f;

	public static float PlantedEnemyDistanceDetection = 160f;

	public static float NormalEnemyDistanceDetection = 1200f;

	public static float PumpkinGravityStrength = 0.8f;

	public static float PumpkinMaxGravity = 20f;

	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.damage = 27;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.shoot = ModContent.ProjectileType<HarvestStaffSentry>();
		base.Item.knockBack = 5f;
		base.Item.useAnimation = (base.Item.useTime = 30);
		base.Item.mana = 10;
		base.Item.width = 44;
		base.Item.height = 46;
		base.Item.noMelee = true;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Grass with
		{
			Volume = 0.6f,
			Pitch = -0.4f
		};
		base.Item.shootSpeed = 0.1f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		player.FindSentryRestingSpot(type, out var XPosition, out var YPosition, out var YOffset);
		YOffset -= 10;
		((Vector2)(ref position))._002Ector((float)XPosition, (float)(YPosition - YOffset));
		Projectile.NewProjectile(source, position, Vector2.Zero, type, damage, knockback, player.whoAmI);
		player.UpdateMaxTurrets();
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("Wood", 20).AddIngredient(1725, 20).AddIngredient(1828, 5)
			.AddTile(16)
			.Register();
	}
}
