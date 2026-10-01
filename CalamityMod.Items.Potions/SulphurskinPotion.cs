using CalamityMod.Buffs.Potions;
using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Abyss;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

public class SulphurskinPotion : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(133, 180, 49),
			new Color(80, 139, 81),
			new Color(117, 95, 133)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(22, 26, ModContent.BuffType<SulphurskinBuff>(), CalamityUtils.MinutesToFrames(4), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 2;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(126).AddIngredient<SulphurousSand>().AddIngredient(317)
			.AddTile(13)
			.Register();
		CreateRecipe().AddIngredient(126).AddIngredient<BloodOrb>(5).AddTile(355)
			.Register()
			.DisableDecraft();
	}
}
