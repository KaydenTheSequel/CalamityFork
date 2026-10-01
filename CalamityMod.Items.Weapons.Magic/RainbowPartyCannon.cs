using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Magic;

public class RainbowPartyCannon : ModItem, ILocalizedModType, IModType
{
	public static readonly Color[] ColorSet;

	public new string LocalizationCategory => "Items.Weapons.Magic";

	public override void SetDefaults()
	{
		base.Item.width = 52;
		base.Item.height = 30;
		base.Item.damage = 170;
		base.Item.DamageType = DamageClass.Magic;
		base.Item.mana = 25;
		base.Item.crit += 4;
		base.Item.useAnimation = (base.Item.useTime = 24);
		base.Item.useStyle = 5;
		base.Item.noMelee = true;
		base.Item.knockBack = 3.5f;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.UseSound = SoundID.Item11;
		base.Item.autoReuse = true;
		base.Item.noUseGraphic = true;
		base.Item.shoot = ModContent.ProjectileType<RainbowPartyCannonProjectile>();
		base.Item.channel = true;
		base.Item.shootSpeed = 20f;
		base.Item.Calamity().devItem = true;
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		TooltipLine line = list.FirstOrDefault((TooltipLine x) => x.Mod == "Terraria" && x.Name == "Tooltip0");
		if (line != null)
		{
			line.OverrideColor = new Color((int)MathHelper.Lerp(156f, 255f, (float)Main.DiscoR / 256f), 108, 251);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CosmicRainbow>().AddIngredient(3930).AddIngredient(3369)
			.AddIngredient<ShadowspecBar>(5)
			.AddIngredient(520, 25)
			.AddIngredient(1345, 50)
			.AddTile<DraedonsForge>()
			.Register();
	}

	static RainbowPartyCannon()
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		ColorSet = (Color[])(object)new Color[7]
		{
			new Color(188, 192, 193),
			new Color(157, 100, 183),
			new Color(249, 166, 77),
			new Color(255, 105, 234),
			new Color(67, 204, 219),
			new Color(249, 245, 99),
			new Color(236, 168, 247)
		};
	}
}
