using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

public class SupremeHealingPotion : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 30;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(255, 31, 25),
			new Color(217, 19, 15),
			new Color(255, 0, 221)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToHealingPotion(26, 38, 250);
		base.Item.value = Item.sellPrice(0, 0, 60);
		base.Item.rare = ModContent.RarityType<Turquoise>();
	}

	public override void AddRecipes()
	{
		CreateRecipe(4).AddIngredient(3544, 4).AddIngredient<Bloodstone>(3).AddTile(13)
			.Register()
			.DisableDecraft();
	}
}
