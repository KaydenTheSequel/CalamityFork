using CalamityMod.Buffs.Pets;
using CalamityMod.Projectiles.Pets;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Pets;

[LegacyName(new string[] { "SparksSummon" })]
public class EnchantedButterfly : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Pets";

	public override void SetDefaults()
	{
		base.Item.DefaultToVanitypet(ModContent.ProjectileType<Sparks>(), ModContent.BuffType<SparksBuff>());
		base.Item.value = Item.sellPrice(0, 2);
		base.Item.rare = 3;
		base.Item.Calamity().donorItem = true;
	}

	public override void UseStyle(Player player, Rectangle heldItemFrame)
	{
		if (player.whoAmI == Main.myPlayer && player.itemTime == 0)
		{
			player.AddBuff(base.Item.buffType, 3600);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient(2891).AddIngredient(1994).AddIngredient(1995)
			.AddIngredient(1996)
			.AddIngredient(1997)
			.AddIngredient(1998)
			.AddIngredient(1999)
			.AddIngredient(2000)
			.AddIngredient(2001)
			.AddIngredient(4845)
			.AddTile(125)
			.Register();
	}
}
