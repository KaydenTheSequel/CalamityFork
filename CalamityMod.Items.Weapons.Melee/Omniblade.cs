using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Omniblade : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 64;
		base.Item.height = 146;
		base.Item.damage = 100;
		base.Item.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Item.useAnimation = 12;
		base.Item.useStyle = 1;
		base.Item.useTime = 12;
		base.Item.useTurn = true;
		base.Item.knockBack = 6f;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.channel = true;
		base.Item.shoot = ModContent.ProjectileType<OmnibladeSwing>();
		base.Item.shootSpeed = 24f;
		base.Item.rare = 8;
	}

	public override void ModifyWeaponCrit(Player player, ref float crit)
	{
		crit += 45f;
	}

	public override bool CanUseItem(Player player)
	{
		return player.ownedProjectileCounts[base.Item.shoot] <= 0;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2273).AddIngredient<LifeAlloy>(5).AddIngredient<CoreofCalamity>(2)
			.AddTile(134)
			.Register();
	}
}
