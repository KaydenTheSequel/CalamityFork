using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "Megafleet" })]
public class Voidragon : ModItem, ILocalizedModType, IModType
{
	private int shotType = 1;

	public static int AmmoSavedPercent = 50;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetDefaults()
	{
		base.Item.width = 96;
		base.Item.height = 38;
		base.Item.damage = 240;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 5;
		base.Item.useAnimation = 5;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5f;
		base.Item.UseSound = SoundID.Item11;
		base.Item.autoReuse = true;
		base.Item.shoot = 10;
		base.Item.shootSpeed = 18f;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-10f, 0f);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		float SpeedX = velocity.X + (float)Main.rand.Next(-5, 6) * 0.05f;
		float SpeedY = velocity.Y + (float)Main.rand.Next(-5, 6) * 0.05f;
		if (shotType > 2)
		{
			shotType = 1;
		}
		if (shotType == 1)
		{
			Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI);
		}
		else
		{
			Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, ModContent.ProjectileType<global::CalamityMod.Projectiles.Ranged.Voidragon>(), damage, knockback, player.whoAmI);
		}
		shotType++;
		Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, ModContent.ProjectileType<VoidragonTentacle>(), damage, knockback, player.whoAmI, (float)Main.rand.Next(-160, 160) * 0.001f, (float)Main.rand.Next(-160, 160) * 0.001f);
		return false;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		if (Main.rand.Next(100) >= AmmoSavedPercent)
		{
			return shotType % 2 == 1;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Seadragon>().AddIngredient<ShadowspecBar>(5).AddTile<DraedonsForge>()
			.Register();
	}
}
