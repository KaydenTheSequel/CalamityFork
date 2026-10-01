using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

[LegacyName(new string[] { "StarfleetMK2" })]
public class Starmada : ModItem, ILocalizedModType, IModType
{
	public static int AmmoSavedPercent = 66;

	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(AmmoSavedPercent);

	public override void SetDefaults()
	{
		base.Item.width = 122;
		base.Item.height = 50;
		base.Item.damage = 4900;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useAnimation = (base.Item.useTime = 70);
		base.Item.knockBack = 15f;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<StarmadaStar>();
		base.Item.shootSpeed = 12f;
		base.Item.useAmmo = AmmoID.FallenStar;
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.value = CalamityGlobalItem.RarityVioletBuyPrice;
	}

	public override bool CanUseItem(Player player)
	{
		return false;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		if (Main.LocalPlayer != null)
		{
			float rate = Main.GlobalTimeWrappedHourly * 3f;
			List<Color> eColors = new List<Color>
			{
				new Color(164, 47, 160),
				new Color(227, 97, 72),
				new Color(193, 255, 146)
			};
			int colorIndex = (int)(rate / 2f % (float)eColors.Count);
			Color val = eColors[colorIndex];
			Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
			Color eTooltipColor = Color.Lerp(val, nextColor, (rate % 2f >= 1f) ? 1f : (rate % 1f));
			TooltipLine line = list.FirstOrDefault((TooltipLine x) => x.Mod == "Terraria" && x.Name == "Tooltip8");
			if (line != null)
			{
				line.OverrideColor = Color.Lerp(eTooltipColor, Color.White, 0.2f);
			}
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<Starfleet>().AddIngredient<AuricBar>(5).AddIngredient<ExodiumCluster>(25)
			.AddTile<CosmicAnvil>()
			.Register();
	}
}
