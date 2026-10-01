using CalamityMod.Buffs.Potions;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Items.Potions;

public class AstralInjection : ModItem, ILocalizedModType, IModType
{
	public static int ManaPerFrame = 2;

	public static int SelfDamage = 5;

	public new string LocalizationCategory => "Items.Potions";

	public override LocalizedText Tooltip => base.Tooltip.WithFormatArgs(ManaPerFrame * 60, SelfDamage);

	public override void SetStaticDefaults()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		base.Item.ResearchUnlockCount = 30;
		ItemID.Sets.DrinkParticleColors[base.Type] = (Color[])(object)new Color[2]
		{
			new Color(255, 164, 94),
			new Color(109, 242, 196)
		};
	}

	public override void SetDefaults()
	{
		base.Item.DefaultToFood(14, 34, ModContent.BuffType<AstralInjectionBuff>(), CalamityUtils.SecondsToFrames(5), useGulpSound: true);
		base.Item.value = Item.sellPrice(0, 0, 2);
		base.Item.rare = 7;
	}

	public override void OnConsumeItem(Player player)
	{
		player.AddBuff(94, Player.manaSickTime / 2);
		player.statLife -= SelfDamage;
		if (Main.myPlayer == player.whoAmI)
		{
			player.HealEffect(-SelfDamage);
		}
		if (player.statLife <= 0)
		{
			player.KillMe(PlayerDeathReason.ByCustomReason(CalamityUtils.GetText("Status.Death.AstralInjection" + Main.rand.Next(1, 3)).ToNetworkText(player.name)), 1000.0, 0);
		}
	}

	public override void AddRecipes()
	{
		CreateRecipe(15).AddIngredient(126, 15).AddIngredient<StarblightSoot>(4).AddIngredient<AureusCell>()
			.AddTile(355)
			.AddConsumeIngredientCallback(Recipe.IngredientQuantityRules.Alchemy)
			.Register();
		CreateRecipe(15).AddIngredient(126, 15).AddIngredient<BloodOrb>(5).AddIngredient<AureusCell>()
			.AddTile(355)
			.Register()
			.DisableDecraft();
	}
}
