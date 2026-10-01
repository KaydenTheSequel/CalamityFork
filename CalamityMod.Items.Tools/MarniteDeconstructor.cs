using CalamityMod.Projectiles.Melee;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

[LegacyName(new string[] { "MarniteSpear" })]
public class MarniteDeconstructor : ModItem, ILocalizedModType, IModType
{
	public static int ArmorPenetration = 10;

	public new string LocalizationCategory => "Items.Tools";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ArmorPenetration);

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 18;
		base.Item.damage = 6;
		base.Item.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Item.ArmorPenetration = ArmorPenetration;
		base.Item.hammer = 59;
		base.Item.tileBoost = 7;
		base.Item.useAnimation = 25;
		base.Item.useTime = 4;
		base.Item.knockBack = 0.5f;
		base.Item.shoot = ModContent.ProjectileType<MarniteDeconstructorProj>();
		base.Item.shootSpeed = 40f;
		base.Item.UseSound = SoundID.Item23;
		base.Item.useStyle = 5;
		base.Item.autoReuse = true;
		base.Item.channel = true;
		base.Item.noMelee = true;
		base.Item.noUseGraphic = true;
		base.Item.value = CalamityGlobalItem.RarityBlueBuyPrice;
		base.Item.rare = 1;
	}

	public override void HoldItem(Player player)
	{
		player.Calamity().mouseWorldListener = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(177).AddRecipeGroup("AnyGoldBar", 3).AddIngredient(3086, 5)
			.AddIngredient(3081, 5)
			.AddTile(16)
			.Register();
	}
}
