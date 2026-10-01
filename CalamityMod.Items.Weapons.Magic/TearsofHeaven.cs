using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class TearsofHeaven : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/TearsOfHeavenUse");

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 48;
		base.Item.damage = 43;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 15;
		base.Item.useTime = (base.Item.useAnimation = 20);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5.5f;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
		base.Item.UseSound = UseSound;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<TearsofHeavenProjectile>();
		base.Item.shootSpeed = 5.5f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		for (int index = 0; index < 2; index++)
		{
			float SpeedX = velocity.X + Main.rand.NextFloat(-2f, 2f);
			float SpeedY = velocity.Y + Main.rand.NextFloat(-2f, 2f);
			Projectile.NewProjectile(source, position, new Vector2(SpeedX, SpeedY), type, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<FrigidflashBolt>().AddIngredient(165).AddIngredient<SeaPrism>(15)
			.AddIngredient(1508, 5)
			.AddTile(101)
			.Register();
	}
}
