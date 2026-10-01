using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class Mistlestorm : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 48;
		base.Item.height = 48;
		base.Item.damage = 54;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 5;
		base.Item.useTime = 6;
		base.Item.useAnimation = 6;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3.5f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.UseSound = SoundID.Item39;
		base.Item.autoReuse = true;
		base.Item.shoot = 336;
		base.Item.shootSpeed = 24f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		int projAmt = 2 + Main.rand.Next(3);
		for (int i = 0; i < projAmt; i++)
		{
			float randVelocityDampener = 0.025f * (float)i;
			velocity.X += (float)Main.rand.Next(-35, 36) * randVelocityDampener;
			velocity.Y += (float)Main.rand.Next(-35, 36) * randVelocityDampener;
			float projDistance = ((Vector2)(ref velocity)).Length();
			projDistance = base.Item.shootSpeed / projDistance;
			velocity.X *= projDistance;
			velocity.Y *= projDistance;
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, Main.rand.Next(0, 10 * (i + 1)));
			Projectile.NewProjectile(source, position, velocity, 206, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1930).AddIngredient(1178).AddIngredient<UelibloomBar>(5)
			.AddIngredient<DarkPlasma>(3)
			.AddTile(134)
			.Register();
	}
}
