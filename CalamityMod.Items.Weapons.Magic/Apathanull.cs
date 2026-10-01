using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

[LegacyName(new string[] { "TomeofFates" })]
public class Apathanull : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 28;
		base.Item.height = 30;
		base.Item.damage = 63;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 20;
		base.Item.useTime = 8;
		base.Item.useAnimation = 20;
		base.Item.reuseDelay = 8;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 5.5f;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.UseSound = new SoundStyle("CalamityMod/Sounds/Item/MeldBurn")
		{
			Volume = 0.7f,
			Pitch = Main.rand.NextFloat(-0.45f, -0.6f)
		};
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<CosmicTentacle>();
		base.Item.shootSpeed = 12f;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseRotationListener = true;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 12f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		Projectile.NewProjectileDirect(source, position, velocity.RotatedByRandom(0.699999988079071), ModContent.ProjectileType<CosmicTentacle>(), damage, knockback, player.whoAmI);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(531).AddIngredient<MeldConstruct>(9).AddTile(101)
			.Register();
	}
}
