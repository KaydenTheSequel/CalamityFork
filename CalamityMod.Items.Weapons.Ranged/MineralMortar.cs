using CalamityMod.Projectiles.Ranged;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class MineralMortar : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 68;
		base.Item.height = 32;
		base.Item.damage = 85;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.shoot = ModContent.ProjectileType<MineralMortarHoldout>();
		base.Item.useAnimation = (base.Item.useTime = 60);
		base.Item.shootSpeed = 25f;
		base.Item.knockBack = 20f;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.useAmmo = AmmoID.Rocket;
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
		base.Item.useStyle = 5;
		base.Item.UseSound = new SoundStyle("CalamityMod/Sounds/Item/DudFire")
		{
			Volume = 0.4f,
			Pitch = -0.9f,
			PitchVariance = 0.1f
		};
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] == 0;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] != 0;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseRotationListener = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		Vector2 spawnPosition = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		Projectile.NewProjectile(source, spawnPosition, player.Calamity().mouseWorld - spawnPosition, ModContent.ProjectileType<MineralMortarHoldout>(), 0, 0f, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddRecipeGroup("AnyAdamantiteBar", 13).AddIngredient(3783, 2).AddTile(134)
			.Register();
	}
}
