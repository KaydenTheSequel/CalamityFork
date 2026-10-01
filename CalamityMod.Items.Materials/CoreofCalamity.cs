using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class CoreofCalamity : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		ItemID.Sets.ItemNoGravity[base.Type] = true;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 94;
	}

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 36;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 2);
		base.Item.rare = 8;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, ModContent.Request<Texture2D>("CalamityMod/Items/Materials/CoreofCalamityGlow", (AssetRequestMode)2).Value);
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<EssenceofSunlight>().AddIngredient<EssenceofHavoc>().AddIngredient<EssenceofEleum>()
			.AddIngredient<AshesofCalamity>()
			.AddIngredient(1508, 3)
			.AddTile(134)
			.Register();
	}
}
