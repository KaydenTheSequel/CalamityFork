using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Summon;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class AquasScepter : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Summon";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.GamepadWholeScreenUseRange[base.Type] = true;
		ItemID.Sets.LockOnIgnoresCollision[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 56;
		base.Item.damage = 65;
		base.Item.mana = 50;
		base.Item.DamageType = DamageClass.Summon;
		base.Item.sentry = true;
		base.Item.useTime = (base.Item.useAnimation = 30);
		base.Item.useStyle = 1;
		base.Item.knockBack = 6f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.Calamity().devItem = true;
		base.Item.UseSound = SoundID.Item66;
		base.Item.noMelee = true;
		base.Item.shoot = ModContent.ProjectileType<AquasScepterCloud>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		position = player.ClampedMouseWorld();
		Projectile.NewProjectileDirect(source, position, velocity, type, damage, knockback, player.whoAmI).originalDamage = base.Item.damage;
		player.UpdateMaxTurrets();
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1244).AddIngredient(157).AddIngredient<ArmoredShell>(3)
			.AddTile(134)
			.Register();
	}
}
