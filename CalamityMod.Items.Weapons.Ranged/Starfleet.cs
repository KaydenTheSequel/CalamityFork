using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Ranged;

public class Starfleet : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Ranged";

	public override void SetDefaults()
	{
		base.Item.width = 76;
		base.Item.height = 36;
		base.Item.damage = 3170;
		base.Item.DamageType = DamageClass.Ranged;
		base.Item.useTime = 70;
		base.Item.useAnimation = 70;
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 15f;
		base.Item.value = CalamityGlobalItem.RarityPureGreenBuyPrice;
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.channel = true;
		base.Item.noUseGraphic = true;
		base.Item.autoReuse = true;
		base.Item.shoot = ModContent.ProjectileType<StarfleetStar>();
		base.Item.shootSpeed = 12f;
		base.Item.useAmmo = AmmoID.FallenStar;
	}

	public override bool CanUseItem(Player player)
	{
		return false;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		if (Main.LocalPlayer != null)
		{
			float rate = Main.GlobalTimeWrappedHourly * 3f;
			List<Color> eColors = new List<Color>
			{
				new Color(146, 255, 211),
				new Color(222, 225, 146),
				new Color(255, 233, 146)
			};
			int colorIndex = (int)(rate / 2f % (float)eColors.Count);
			Color val = eColors[colorIndex];
			Color nextColor = eColors[(colorIndex + 1) % eColors.Count];
			Color eTooltipColor = Color.Lerp(val, nextColor, (rate % 2f >= 1f) ? 1f : (rate % 1f));
			TooltipLine line = list.FirstOrDefault((TooltipLine x) => x.Mod == "Terraria" && x.Name == "Tooltip7");
			if (line != null)
			{
				line.OverrideColor = Color.Lerp(eTooltipColor, Color.White, 0.2f);
			}
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(4060).AddIngredient<RuinousSoul>(4).AddIngredient(75, 15)
			.AddIngredient<GalacticaSingularity>(3)
			.AddTile(412)
			.Register();
	}
}
