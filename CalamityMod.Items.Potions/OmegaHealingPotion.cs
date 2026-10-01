using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

public class OmegaHealingPotion : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 30;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(255, 31, 25),
			new Color(162, 28, 25),
			new Color(159, 10, 111)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToHealingPotion(24, 32, 300);
		base.Item.value = Item.sellPrice(0, 1);
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
	}

	public override void AddRecipes()
	{
		CreateRecipe(20).AddIngredient<SupremeHealingPotion>(20).AddIngredient<AscendantSpiritEssence>().AddTile(13)
			.Register()
			.DisableDecraft();
	}
}
