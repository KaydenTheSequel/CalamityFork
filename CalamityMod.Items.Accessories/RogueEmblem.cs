using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class RogueEmblem : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 24;
		base.Item.height = 24;
		base.Item.value = CalamityGlobalItem.RarityLightRedBuyPrice;
		base.Item.rare = 4;
		base.Item.accessory = true;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.GetDamage<ThrowingDamageClass>() += 0.15f;
	}

	public override void AddRecipes()
	{
		Recipe r = Recipe.Create(935);
		r.AddIngredient<RogueEmblem>();
		r.AddIngredient(548, 5);
		r.AddIngredient(549, 5);
		r.AddIngredient(547, 5);
		r.AddTile(114);
		r.Register();
		for (int i = 0; i < Recipe.maxRecipes; i++)
		{
			Recipe s = Main.recipe[i];
			if (s.createItem.type == 935 && s.HasIngredient(2998))
			{
				r.SortAfter(s);
				break;
			}
		}
	}
}
