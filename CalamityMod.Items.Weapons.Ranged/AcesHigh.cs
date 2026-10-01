using CalamityMod.Items.Materials;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Items.Weapons.Typeless;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class AcesHigh : ModItem, ILocalizedModType, IModType
{
	public int shots;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 48;
		base.Item.height = 30;
		base.Item.damage = 325;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 3;
		base.Item.useAnimation = 12;
		base.Item.reuseDelay = 8;
		base.Item.useLimitPerAnimation = 4;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 6f;
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.Calamity().donorItem = true;
		base.Item.UseSound = SoundID.Item36;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 24f;
		base.Item.shoot = ModContent.ProjectileType<CardHeart>();
		base.Item.useAmmo = AmmoID.Bullet;
		base.Item.consumeAmmoOnLastShotOnly = true;
	}

	public override Vector2? HoldoutOffset()
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(-5f, 0f);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2269).AddIngredient<ClaretCannon>().AddIngredient<FantasyTalisman>()
			.AddIngredient<AuricBar>(5)
			.AddTile<CosmicAnvil>()
			.Register();
	}

	public override void ModifyShootStats(Player player, ref Vector2 position, ref Vector2 velocity, ref int type, ref int damage, ref float knockback)
	{
		shots++;
		switch (shots)
		{
		case 4:
			type = ModContent.ProjectileType<CardSpade>();
			shots = 0;
			break;
		case 3:
			type = ModContent.ProjectileType<CardDiamond>();
			break;
		case 2:
			type = ModContent.ProjectileType<CardClub>();
			break;
		default:
			type = ModContent.ProjectileType<CardHeart>();
			break;
		}
	}
}
