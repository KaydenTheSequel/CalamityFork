using System.Collections.Generic;
using System.Linq;
using CalamityMod.Items.BaseItems;
using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Melee;
using CalamityMod.Rarities;
using CalamityMod.Tiles.Furniture.CraftingStations;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Melee;

public class Earth : CustomUseProjItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Weapons.Melee";

	public override void SetDefaults()
	{
		base.Item.width = 186;
		base.Item.height = 186;
		base.Item.damage = 4200;
		base.Item.DamageType = TrueMeleeDamageClass.Instance;
		base.Item.useAnimation = 42;
		base.Item.useTime = 42;
		base.Item.useTurn = true;
		base.Item.knockBack = 15f;
		base.Item.autoReuse = true;
		base.Item.value = CalamityGlobalItem.RarityHotPinkBuyPrice;
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.Calamity().devItem = true;
		base.Item.channel = true;
		base.Item.shoot = ModContent.ProjectileType<EarthHoldout>();
		base.Item.noUseGraphic = true;
		base.Item.noMelee = true;
		base.Item.useStyle = 5;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Weapons/Melee/EarthGlow", (AssetRequestMode)2).Value);
	}

	public override void ModifyTooltips(List<TooltipLine> list)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		List<Color> earthColors = new List<Color>
		{
			Color.OrangeRed,
			Color.MediumTurquoise,
			Color.LimeGreen
		};
		int colorIndex = (int)(Main.GlobalTimeWrappedHourly / 2f % (float)earthColors.Count);
		Color val = earthColors[colorIndex];
		Color nextColor = earthColors[(colorIndex + 1) % earthColors.Count];
		Color earthTooltipColor = Color.Lerp(val, nextColor, (Main.GlobalTimeWrappedHourly % 2f > 1f) ? 1f : (Main.GlobalTimeWrappedHourly % 1f));
		TooltipLine line = list.FirstOrDefault((TooltipLine x) => x.Mod == "Terraria" && x.Name == "Tooltip3");
		if (line != null)
		{
			line.OverrideColor = Color.Lerp(earthTooltipColor, Color.White, 0.5f);
		}
	}

	public override bool MeleePrefix()
	{
		return true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<GrandGuardian>().AddIngredient<StellarStriker>().AddIngredient<ShadowspecBar>(5)
			.AddIngredient<LifeAlloy>(5)
			.AddTile<DraedonsForge>()
			.Register();
	}
}
