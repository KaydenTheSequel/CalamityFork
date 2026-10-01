using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.FishingRods;

public class VerstaltiteFishingRod : ModItem, ILocalizedModType, IModType
{
	public static float FishingPowerBiomeMult = 1.1f;

	public new string LocalizationCategory => "Items.Fishing";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(FishingPowerBiomeMult.ToString());

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 28;
		base.Item.useAnimation = 8;
		base.Item.useTime = 8;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.fishingPole = 35;
		base.Item.shootSpeed = 15f;
		base.Item.shoot = ModContent.ProjectileType<VerstaltiteBobber>();
		base.Item.value = CalamityGlobalItem.RarityPinkBuyPrice;
		base.Item.rare = 5;
	}

	public override void ModifyFishingLine(Projectile bobber, ref Vector2 lineOriginOffset, ref Color lineColor)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		lineOriginOffset = new Vector2(43f, -36f);
		lineColor = new Color(95, 158, 160, 100);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<CryonicBar>(6).AddTile(134).Register();
	}
}
