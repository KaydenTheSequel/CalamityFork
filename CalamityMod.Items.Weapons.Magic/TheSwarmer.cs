using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class TheSwarmer : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 60;
		base.Item.height = 52;
		base.Item.damage = 36;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 14;
		base.Item.useTime = 12;
		base.Item.useAnimation = 12;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.UseSound = SoundID.Item11;
		base.Item.autoReuse = true;
		base.Item.shoot = 189;
		base.Item.shootSpeed = 12f;
		base.Item.knockBack = 0.25f;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-15f, -5f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		int beeAmount = (player.strongBees ? 3 : 2);
		for (int i = 0; i <= beeAmount; i++)
		{
			float SpeedX = velocity.X + (float)Main.rand.Next(-35, 36) * 0.05f;
			float SpeedY = velocity.Y + (float)Main.rand.Next(-35, 36) * 0.05f;
			int wasps = Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, player.strongBees ? (damage + 5) : damage, 0f, player.whoAmI);
			if (wasps.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[wasps].DamageType = DamageClass.Magic;
				Main.projectile[wasps].idStaticNPCHitCooldown = 5;
				if (player.strongBees)
				{
					Main.projectile[wasps].ArmorPenetration += 5;
				}
			}
		}
		for (int j = 0; j <= beeAmount; j++)
		{
			float SpeedX2 = velocity.X + (float)Main.rand.Next(-35, 36) * 0.05f;
			float SpeedY2 = velocity.Y + (float)Main.rand.Next(-35, 36) * 0.05f;
			int bees = Projectile.NewProjectile(source, position.X, position.Y, SpeedX2, SpeedY2, player.beeType(), player.beeDamage(base.Item.damage), player.beeKB(0f), player.whoAmI);
			if (bees.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[bees].DamageType = DamageClass.Magic;
				Main.projectile[bees].idStaticNPCHitCooldown = 5;
			}
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1121).AddIngredient(1155).AddIngredient(3459, 6)
			.AddTile(412)
			.Register();
	}
}
