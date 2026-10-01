using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.SunkenSea;
using CalamityMod.Projectiles.Typeless;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Fishing.FishingRods;

public class NavyFishingRod : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Fishing";

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 28;
		base.Item.useAnimation = 8;
		base.Item.useTime = 8;
		base.Item.useStyle = 1;
		base.Item.UseSound = SoundID.Item1;
		base.Item.fishingPole = 20;
		base.Item.shootSpeed = 13f;
		base.Item.shoot = ModContent.ProjectileType<NavyBobber>();
		base.Item.value = CalamityGlobalItem.RarityGreenBuyPrice;
		base.Item.rare = 2;
	}

	public override void ModifyFishingLine(Projectile bobber, ref Vector2 lineOriginOffset, ref Color lineColor)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		lineOriginOffset = new Vector2(56f, -37f);
		lineColor = new Color(36, 61, 111, 100);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<PearlShard>().AddIngredient<SeaPrism>(5).AddIngredient<Navystone>(8)
			.AddTile(16)
			.Register();
	}
}
