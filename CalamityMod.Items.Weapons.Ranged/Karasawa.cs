using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Karasawa : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle FireSound = new SoundStyle("CalamityMod/Sounds/Item/MechGaussRifle");

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 94;
		base.Item.height = 44;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.damage = 2400;
		base.Item.knockBack = 12f;
		base.Item.useTime = 52;
		base.Item.useAnimation = 52;
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = FireSound;
		base.Item.noMelee = true;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.Calamity().donorItem = true;
		base.Item.shoot = ModContent.ProjectileType<KarasawaShot>();
		base.Item.shootSpeed = 1f;
		base.Item.useAmmo = AmmoID.Bullet;
	}

	public override bool CanUseItem(Player player)
	{
		return CalamityGlobalItem.HasEnoughAmmo(player, base.Item, 5);
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		if (((Vector2)(ref velocity)).Length() > 5f)
		{
			((Vector2)(ref velocity)).Normalize();
			velocity *= 5f;
		}
		Projectile.NewProjectile(source, position, velocity, base.Item.shoot, damage, knockback, player.whoAmI);
		CalamityGlobalItem.ConsumeAdditionalAmmo(player, base.Item, 5);
		return false;
	}

	public override bool CanConsumeAmmo(Item ammo, Player player)
	{
		return false;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-20f, 0f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(1526).AddIngredient<MysteriousCircuitry>(15).AddIngredient<DubiousPlating>(25)
			.AddIngredient<CosmiliteBar>(8)
			.AddIngredient<NightmareFuel>(20)
			.AddIngredient<GalacticaSingularity>(5)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
