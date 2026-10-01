using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class HadalUrn : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle ShootSound = new SoundStyle("CalamityMod/Sounds/Item/HadalUrnOpen");

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 58;
		base.Item.height = 38;
		base.Item.damage = 37;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 10;
		base.Item.useTime = (base.Item.useAnimation = 20);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 2.75f;
		base.Item.value = CalamityGlobalItem.RarityLimeBuyPrice;
		base.Item.rare = 7;
		base.Item.Calamity().donorItem = true;
		base.Item.UseSound = ShootSound;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 8f;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.shoot = ModContent.ProjectileType<HadalUrnHoldout>();
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.Item103 with
		{
			Volume = SoundID.Item103.Volume
		};
		SoundEngine.PlaySound(in style, player.Center);
		Projectile.NewProjectile(source, position, velocity, ModContent.ProjectileType<HadalUrnHoldout>(), damage, knockback, player.whoAmI, 12f);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BlackAnurian>().AddIngredient<Voidstone>(20).AddIngredient<DepthCells>(15)
			.AddIngredient(154, 10)
			.AddTile(134)
			.Register();
	}
}
