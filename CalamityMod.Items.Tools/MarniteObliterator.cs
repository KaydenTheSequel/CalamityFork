using CalamityMod.Projectiles.Melee;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

public class MarniteObliterator : ModItem, ILocalizedModType, IModType
{
	public static readonly SoundStyle UseSound = new SoundStyle("CalamityMod/Sounds/Item/MarniteObliteratorUse")
	{
		PitchVariance = 0.3f
	};

	public static int ArmorPenetration = 5;

	public new string LocalizationCategory => "Items.Tools";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ArmorPenetration);

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 18;
		base.Item.damage = 7;
		base.Item.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Item.ArmorPenetration = ArmorPenetration;
		base.Item.pick = 59;
		base.Item.tileBoost = 7;
		base.Item.useAnimation = 25;
		base.Item.useTime = 3;
		base.Item.knockBack = 0.5f;
		base.Item.shoot = ModContent.ProjectileType<MarniteObliteratorProj>();
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
		CreateRecipe().AddIngredient(182).AddRecipeGroup("AnyGoldBar", 3).AddIngredient(3086, 5)
			.AddIngredient(3081, 5)
			.AddTile(16)
			.Register();
	}
}
