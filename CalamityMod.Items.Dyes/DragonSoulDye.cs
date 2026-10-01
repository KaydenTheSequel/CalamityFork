using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Items.Dyes;

public class DragonSoulDye : BaseDye
{
	public override ArmorShaderData ShaderDataToBind => new ArmorShaderData(base.Mod.Assets.Request<Effect>("Effects/Dyes/DragonSoulDyeShader"), "DyePass");

	public override void SafeSetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
	}

	public override void SafeSetDefaults()
	{
		base.Item.rare = ModContent.RarityType<BurnishedAuric>();
		base.Item.value = Item.sellPrice(0, 2, 50);
	}

	public override void AddRecipes()
	{
		CreateRecipe(3).AddIngredient(126, 3).AddIngredient<YharonSoulFragment>().AddTile(228)
			.Register();
	}
}
