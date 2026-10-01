using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "Genisis" })]
public class Genesis : ModItem, ILocalizedModType, IModType
{
	public static float FireRate = 15f;

	public static float StarterWindup = 60f;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 108;
		base.Item.height = 46;
		base.Item.damage = 65;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 20;
		base.Item.useAnimation = (base.Item.useTime = 5);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 1.5f;
		base.Item.value = CalamityGlobalItem.RarityPurpleBuyPrice;
		base.Item.rare = 11;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 6f;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.shoot = ModContent.ProjectileType<GenesisHoldout>();
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] == 0;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseRotationListener = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(source, player.MountedCenter, Vector2.Zero, ModContent.ProjectileType<GenesisHoldout>(), damage, knockback, player.whoAmI).velocity = player.Calamity().mouseWorld - player.RotatedRelativePoint(player.MountedCenter);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(514).AddIngredient(3467, 5).AddIngredient<LifeAlloy>(5)
			.AddTile(134)
			.Register();
	}
}
