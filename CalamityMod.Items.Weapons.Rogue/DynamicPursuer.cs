using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.DraedonsArsenal;
using CalamityMod.Projectiles.Rogue;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Rogue;

public class DynamicPursuer : RogueWeapon
{
	public static float StealthDmgMult = 0.3f;

	public static float ReturnAcceleration = 0.75f;

	public static float ReturnMaxSpeed = 24f;

	public static float RicochetShootingCooldown = 1000f;

	public static float RicochetVelocityCap = 28f;

	public static float ElectricityDmgMult = 0.4f;

	public static float ElectricityCooldown = 500f;

	public static float LaserDmgMult = 0.25f;

	public static float LaserCooldown = 300f;

	public override float StealthDamageMultiplier => StealthDmgMult;

	public override float StealthVelocityMultiplier => 0.8f;

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 34;
		base.Item.damage = 2850;
		base.Item.DamageType = RogueDamageClass.Instance;
		base.Item.useTime = 42;
		base.Item.useAnimation = 42;
		base.Item.useStyle = 1;
		base.Item.useTurn = false;
		base.Item.knockBack = 3f;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<DynamicPursuerProjectile>();
		base.Item.shootSpeed = 17f;
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

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<DimensionTearingDisk>().AddIngredient<AerialTracker>().AddIngredient<AuricBar>(5)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
