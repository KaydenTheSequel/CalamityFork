using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class PlasmaRifle : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle HeavyShotSound = new SoundStyle("CalamityMod/Sounds/Item/PlasmaRifleMain");

	public static readonly SoundStyle FastShotSound = new SoundStyle("CalamityMod/Sounds/Item/PlasmaRifleAlt");

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.ItemsThatAllowRepeatedRightClick[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 72;
		base.Item.height = 20;
		base.Item.damage = 120;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 40;
		base.Item.useAnimation = (base.Item.useTime = 40);
		base.Item.knockBack = 4f;
		base.Item.shoot = ModContent.ProjectileType<PlasmaRifleShot>();
		base.Item.shootSpeed = 14f;
		base.Item.UseSound = HeavyShotSound;
		base.Item.useStyle = 5;
		base.Item.autoReuse = true;
		base.Item.noMelee = true;
		base.Item.rare = 10;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}

	public override bool AltFunctionUse(Player player)
	{
		return true;
	}

	public override bool CanUseItem(Player player)
	{
		base.Item.UseSound = ((player.altFunctionUse == 2) ? FastShotSound : HeavyShotSound);
		return base.CanUseItem(player);
	}

	public override void ModifyManaCost(Player player, ref float reduce, ref float mult)
	{
		if (player.altFunctionUse == 2)
		{
			mult *= 0.25f;
		}
	}

	public override float UseSpeedMultiplier(Player player)
	{
		if (player.altFunctionUse != 2)
		{
			return 1f;
		}
		return 5f;
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
		position += velocity.SafeNormalize(Vector2.UnitX) * 56f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (player.altFunctionUse == 2)
		{
			Projectile.NewProjectile(source, position, velocity, type, damage, knockback, player.whoAmI, 1f);
			return false;
		}
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(3105).AddIngredient(514).AddIngredient(3456, 6)
			.AddTile(412)
			.Register();
	}
}
