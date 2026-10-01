using CalamityMod.Items.Materials;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.FishingRods;

public class HeronRod : ModItem, ILocalizedModType, IModType
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
		base.Item.fishingPole = 25;
		base.Item.shootSpeed = 14.5f;
		base.Item.shoot = ModContent.ProjectileType<HeronBobber>();
		base.Item.value = CalamityGlobalItem.RarityOrangeBuyPrice;
		base.Item.rare = 3;
	}

	public override void ModifyFishingLine(Projectile bobber, ref Vector2 lineOriginOffset, ref Color lineColor)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		lineOriginOffset = new Vector2(47f, -33f);
		lineColor = new Color(101, 149, 154, 100);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<AerialiteBar>(6).AddIngredient(824, 3).AddTile(16)
			.Register();
	}
}
