using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class SanguineFlare : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetStaticDefaults()
	{
		Item.staff[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 56;
		base.Item.height = 60;
		base.Item.damage = 1050;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 36;
		base.Item.useTime = 25;
		base.Item.useAnimation = 25;
		base.Item.useStyle = 5;
		base.Item.channel = true;
		base.Item.noMelee = true;
		base.Item.knockBack = 8f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.UseSound = SoundID.Item20;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<SanguineFlareProj>();
		base.Item.shootSpeed = 21f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		velocity *= 0f;
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BloodstoneCore>(5).AddTile(134).Register();
	}
}
