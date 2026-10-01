using CalamityMod.CalPlayer;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class Omicron : ModItem, ILocalizedModType, IModType
{
	public static float FireRate = 15f;

	public static float StarterWinup = 60f;

	public static float WingmanFireRate = 10f;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 122;
		base.Item.height = 54;
		base.Item.damage = 80;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 10;
		base.Item.useAnimation = (base.Item.useTime = 4);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 1.5f;
		base.Item.value = CalamityGlobalItem.RarityDarkBlueBuyPrice;
		base.Item.UseSound = null;
		base.Item.autoReuse = true;
		base.Item.shootSpeed = 6f;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.shoot = ModContent.ProjectileType<OmicronHoldout>();
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override bool CanUseItem(Player player)
	{
		if (player.ownedProjectileCounts[base.Item.shoot] <= 0)
		{
			return player.ownedProjectileCounts[ModContent.ProjectileType<OmicronWingman>()] < 2;
		}
		return false;
	}

	public override void HoldItem(Player player)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		calamityPlayer.mouseWorldListener = true;
		calamityPlayer.mouseRotationListener = true;
		calamityPlayer.rightClickListener = true;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 2; i++)
		{
			Projectile.NewProjectileDirect(source, player.MountedCenter, Vector2.Zero, ModContent.ProjectileType<OmicronWingman>(), damage, knockback, player.whoAmI, 0f, 0f, (i == 0) ? 1 : (-1)).velocity = (player.Calamity().mouseWorld - player.MountedCenter).SafeNormalize(Vector2.Zero);
		}
		Projectile.NewProjectileDirect(source, player.MountedCenter, Vector2.Zero, ModContent.ProjectileType<OmicronHoldout>(), damage, knockback, player.whoAmI).velocity = (player.Calamity().mouseWorld - player.MountedCenter).SafeNormalize(Vector2.Zero);
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Genesis>().AddIngredient<Wingman>().AddIngredient<CosmiliteBar>(10)
			.AddIngredient<GalacticaSingularity>(5)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
