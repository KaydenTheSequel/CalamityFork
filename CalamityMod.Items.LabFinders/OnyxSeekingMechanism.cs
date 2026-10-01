using CalamityMod.Projectiles.Typeless;
using CalamityMod.Rarities;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.LabFinders;

public class OnyxSeekingMechanism : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.DraedonItems";

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 26;
		base.Item.noUseGraphic = true;
		base.Item.useStyle = 1;
		base.Item.useAnimation = (base.Item.useTime = 36);
		base.Item.shoot = ModContent.ProjectileType<OnyxLabSeeker>();
		base.Item.Calamity().MaxCharge = 100f;
		base.Item.Calamity().ChargePerUse = 10f;
		base.Item.Calamity().UsesCharge = true;
		base.Item.value = Item.sellPrice(0, 0, 50);
		base.Item.rare = ModContent.RarityType<DarkOrange>();
	}

	public override bool CanUseItem(Player player)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		if (player.ownedProjectileCounts[base.Item.shoot] <= 0)
		{
			return CalamityWorld.CavernLabCenter != Vector2.Zero;
		}
		return false;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<LabSeekingMechanism>().AddIngredient(173, 25).AddTile(16)
			.Register();
	}
}
