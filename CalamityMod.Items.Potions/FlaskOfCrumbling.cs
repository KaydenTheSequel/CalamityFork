using CalamityMod.Buffs.Potions;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

[LegacyName(new string[] { "CrumblingPotion" })]
public class FlaskOfCrumbling : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(243, 205, 45),
			new Color(192, 97, 38),
			new Color(225, 162, 58)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(32, 28, ModContent.BuffType<WeaponImbueCrumbling>(), CalamityUtils.MinutesToFrames(20), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 5);
		base.Item.rare = 4;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(126).AddIngredient<EssenceofSunlight>(2).AddTile(243)
			.Register();
	}
}
