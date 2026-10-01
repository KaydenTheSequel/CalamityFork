using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class HandheldTank : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/TankCannon")
	{
		PitchVariance = 0.5f
	};

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 110;
		base.Item.height = 46;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.damage = 740;
		base.Item.knockBack = 16f;
		base.Item.useTime = (base.Item.useAnimation = 74);
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = UseSound;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.Calamity().donorItem = true;
		base.Item.shoot = ModContent.ProjectileType<HandheldTankShell>();
		base.Item.shootSpeed = 6f;
		base.Item.useAmmo = AmmoID.Rocket;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 15f;
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		position += velocity.SafeNormalize(Vector2.UnitX) * 48f;
		type = base.Item.shoot;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-33f, 0f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(759).AddRecipeGroup("IronBar", 50).AddIngredient<DivineGeode>(5)
			.AddIngredient(2281)
			.AddTile(134)
			.Register();
	}
}
