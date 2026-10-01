using CalamityMod.Buffs.Potions;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

public class PotionofOmniscience : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(159, 67, 199),
			new Color(176, 147, 243),
			new Color(84, 50, 185)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(28, 30, ModContent.BuffType<Omniscience>(), CalamityUtils.MinutesToFrames(15), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 3;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(304).AddIngredient(296).AddIngredient(2329)
			.AddTile(355)
			.Register();
		CreateRecipe().AddIngredient(126).AddIngredient<BloodOrb>(20).AddTile(355)
			.Register()
			.DisableDecraft();
	}
}
