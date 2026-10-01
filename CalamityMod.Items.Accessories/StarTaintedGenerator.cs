using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Items.Accessories;

public class StarTaintedGenerator : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Accessories";

	public override void SetDefaults()
	{
		base.Item.width = 38;
		base.Item.height = 60;
		base.Item.accessory = true;
		base.Item.value = CalamityGlobalItem.RarityYellowBuyPrice;
		base.Item.rare = 8;
	}

	public override void UpdateAccessory(Player player, bool hideVisual)
	{
		player.Calamity().voltaicJelly = true;
		player.Calamity().starbusterCore = true;
		player.Calamity().starTaintedGenerator = true;
		player.GetDamage<SummonDamageClass>() += 0.07f;
		player.buffImmune[ModContent.BuffType<Irradiated>()] = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<JellyChargedBattery>().AddIngredient<NuclearFuelRod>().AddIngredient<StarbusterCore>()
			.AddIngredient<LifeAlloy>(3)
			.AddTile(134)
			.Register();
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawInventoryCustomScale(spriteBatch, TextureAssets.Item[base.Type].Value, position, frame, drawColor, itemColor, origin, scale, 0.8f, new Vector2(0f, 0f), (SpriteEffects)0);
		return false;
	}
}
