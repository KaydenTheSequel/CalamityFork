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

public class SDFMG : ModItem, ILocalizedModType, IModType
{
	public static int AmmoSavedPercent = 66;

	private int ShotCounter;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetDefaults()
	{
		base.Item.width = 74;
		base.Item.height = 34;
		base.Item.damage = 118;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = (base.Item.useAnimation = 3);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2.75f;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.UseSound = SoundID.Item11;
		base.Item.autoReuse = true;
		base.Item.shoot = 10;
		base.Item.shootSpeed = 16f;
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 15f;
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
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		float SpeedX = velocity.X + Main.rand.NextFloat(-0.25f, 0.25f);
		float SpeedY = velocity.Y + Main.rand.NextFloat(-0.25f, 0.25f);
		ShotCounter++;
		if (ShotCounter >= 7)
		{
			ShotCounter = 0;
			Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, ModContent.ProjectileType<FishronRPG>(), damage, knockback, player.whoAmI);
		}
		Projectile.NewProjectile(source, position.X, position.Y, SpeedX, SpeedY, type, damage, knockback, player.whoAmI);
		return false;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return Main.rand.Next(100) >= AmmoSavedPercent;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1553).AddIngredient<CosmiliteBar>(8).AddIngredient<EndothermicEnergy>(20)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
