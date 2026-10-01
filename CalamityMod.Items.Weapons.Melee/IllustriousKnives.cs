using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

[LegacyName(new string[] { "RoyalKnives", "RoyalKnivesMelee", "RoyalKnivesRogue" })]
public class IllustriousKnives : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 62;
		base.Item.damage = 400;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 8;
		base.Item.useStyle = 1;
		base.Item.useTime = 8;
		base.Item.knockBack = 3f;
		base.Item.UseSound = SoundID.Item39;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
		base.Item.shoot = ModContent.ProjectileType<IllustriousKnife>();
		base.Item.shootSpeed = 9f;
	}

	public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		int knifeAmt = 4;
		if (Main.rand.NextBool())
		{
			knifeAmt++;
		}
		if (Main.rand.NextBool(4))
		{
			knifeAmt++;
		}
		if (Main.rand.NextBool(6))
		{
			knifeAmt++;
		}
		if (Main.rand.NextBool(8))
		{
			knifeAmt++;
		}
		for (int i = 0; i < knifeAmt; i++)
		{
			Vector2 knifeVel = velocity.RotatedByRandom(0.5235987901687622);
			Projectile.NewProjectile(source, position, knifeVel, type, damage, knockback, player.whoAmI);
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<EmpyreanKnives>().AddIngredient<ShadowspecBar>(5).AddIngredient<CoreofCalamity>(2)
			.AddTile<DraedonsForge>()
			.Register();
	}
}
