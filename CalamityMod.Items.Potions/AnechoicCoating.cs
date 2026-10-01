using CalamityMod.Buffs.Potions;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

public class AnechoicCoating : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Potions";

	public override void SetStaticDefaults()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[2]
		{
			new Color(118, 182, 199),
			new Color(150, 227, 230)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(22, 26, ModContent.BuffType<AnechoicCoatingBuff>(), CalamityUtils.MinutesToFrames(4));
		base.Item.UseSound = SoundID.Item3;
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 1;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(126).AddIngredient<BloodOrb>(10).AddTile(355)
			.Register()
			.DisableDecraft();
	}
}
