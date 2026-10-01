using CalamityMod.Buffs.Potions;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

[LegacyName(new string[] { "HolyWrathPotion", "ProfanedRagePotion" })]
public class FlaskOfHolyFlames : ModItem, ILocalizedModType, IModType
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
		base.Item.ResearchUnlockCount = 20;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[3]
		{
			new Color(252, 23, 23),
			new Color(199, 14, 51),
			new Color(143, 36, 72)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(44, 36, ModContent.BuffType<WeaponImbueHolyFlames>(), CalamityUtils.MinutesToFrames(20), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 5);
		base.Item.rare = 11;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(126).AddIngredient<UnholyEssence>().AddTile(243)
			.Register();
	}
}
