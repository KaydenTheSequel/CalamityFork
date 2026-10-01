using System.Collections.Generic;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee.Yoyos;
using CalamityMod.Rarities;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Lacerator : ModItem, ILocalizedModType, IModType
{
	public static float Reach = 640f;

	public static float Speed = 48f;

	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(Reach.ToTiles(), Speed);

	public override void SetStaticDefaults()
	{
		ItemID.Sets.Yoyo[base.Type] = true;
		ItemID.Sets.GamepadExtraRange[base.Type] = 15;
		ItemID.Sets.GamepadSmartQuickReach[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 26;
		base.Item.DamageType = DamageClass.MeleeNoSpeed;
		base.Item.damage = 115;
		base.Item.knockBack = 7f;
		base.Item.useTime = 20;
		base.Item.useAnimation = 20;
		base.Item.autoReuse = true;
		base.Item.useStyle = 5;
		base.Item.UseSound = SoundID.Item1;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.shoot = ModContent.ProjectileType<LaceratorYoyo>();
		base.Item.shootSpeed = 16f;
		base.Item.value = CalamityGlobalItem.RarityTurquoiseBuyPrice;
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		list.FindAndReplace("[GFB]", this.GetLocalizedValue(Main.zenithWorld ? "TooltipGFB" : "TooltipNormal"));
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<BloodstoneCore>(4).AddTile(134).Register();
	}
}
