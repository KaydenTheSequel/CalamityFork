using CalamityMod.Projectiles.Melee.MaceFlails;
using CalamityMod.Projectiles.Melee.Spears;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class YateveoBloom : ModItem, ILocalizedModType, IModType
{
	public static float ShootSpeed = 12f;

	public static float SpearSpeed = 4.5f;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 42;
		base.Item.height = 62;
		base.Item.damage = 30;
		base.Item.knockBack = 5f;
		base.Item.useAnimation = (base.Item.useTime = 22);
		base.Item.noUseGraphic = true;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.autoReuse = true;
		base.Item.useTurn = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item1;
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
		base.Item.Calamity().donorItem = true;
		base.Item.shoot = ModContent.ProjectileType<YateveoBloomMace>();
		base.Item.shootSpeed = ShootSpeed;
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.altFunctionUse != 2)
		{
			return 1f;
		}
		return 0.66f;
	}

	public override bool CanUseItem(Player player)
	{
		if (player.altFunctionUse == 2)
		{
			base.Item.channel = false;
			base.Item.autoReuse = true;
		}
		else
		{
			base.Item.channel = true;
			base.Item.autoReuse = false;
		}
		return player.ownedProjectileCounts[base.Item.shoot] + player.ownedProjectileCounts[ModContent.ProjectileType<YateveoBloomSpear>()] <= 0;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		float speedMult = SpearSpeed / ShootSpeed;
		if (player.altFunctionUse == 2)
		{
			Projectile.NewProjectile(source, position, velocity * speedMult, ModContent.ProjectileType<YateveoBloomSpear>(), damage, knockback, player.whoAmI);
		}
		else
		{
			Projectile.NewProjectile(source, position, velocity, type, (int)((float)damage * 0.5f), knockback, player.whoAmI);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(208).AddIngredient(620, 15).AddIngredient(331, 12)
			.AddIngredient(209, 4)
			.AddIngredient(210, 2)
			.AddTile(16)
			.Register();
	}
}
