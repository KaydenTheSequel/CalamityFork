using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class StormSurge : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.IsRangedSpecialistWeapon[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 58;
		base.Item.height = 22;
		base.Item.damage = 18;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 18;
		base.Item.useAnimation = 18;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.UseSound = SoundID.Item122;
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<StormSurgeTornado>();
		base.Item.shootSpeed = 12f;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<StormlionMandible>().AddIngredient<PearlShard>(3).AddIngredient<SeaPrism>(7)
			.AddIngredient<Navystone>(10)
			.AddTile(16)
			.Register();
	}
}
