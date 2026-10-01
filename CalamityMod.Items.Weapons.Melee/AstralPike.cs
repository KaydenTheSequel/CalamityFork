using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee.Spears;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class AstralPike : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetStaticDefaults()
	{
		ItemID.Sets.Spears[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 44;
		base.Item.height = 50;
		base.Item.damage = 90;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.noMelee = true;
		base.Item.useTurn = true;
		base.Item.noUseGraphic = true;
		base.Item.useAnimation = 13;
		base.Item.useStyle = 5;
		base.Item.useTime = 13;
		base.Item.knockBack = 8.5f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityCyanBuyPrice;
		base.Item.rare = 9;
		base.Item.shoot = ModContent.ProjectileType<AstralPikeProj>();
		base.Item.shootSpeed = 13f;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 25f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AstralBar>(8).AddTile(412).Register();
	}
}
