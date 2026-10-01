using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class ThePrince : ModItem, ILocalizedModType, IModType
{
	public const int FlameSplitCount = 6;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 102;
		base.Item.height = 112;
		base.Item.damage = 166;
		base.Item.knockBack = 4.25f;
		base.Item.shootSpeed = 23.5f;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.noMelee = true;
		base.Item.mana = 12;
		base.Item.useAnimation = (base.Item.useTime = 21);
		base.Item.useStyle = 5;
		base.Item.autoReuse = true;
		base.Item.UseSound = SoundID.DD2_FlameburstTowerShot;
		base.Item.shoot = ModContent.ProjectileType<PrinceFlameLarge>();
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.Calamity().donorItem = true;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		Vector2 flameSpawnPosition = player.RotatedRelativePoint(player.MountedCenter, reverseRotation: true);
		flameSpawnPosition += velocity.SafeNormalize(Vector2.Zero) * 105f;
		Projectile.NewProjectile(source, flameSpawnPosition, velocity, type, damage, knockback, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<ArchAmaryllis>().AddIngredient<DivineGeode>(15).AddIngredient<UnholyEssence>(10)
			.AddTile(134)
			.Register();
	}
}
