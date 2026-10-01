using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;

namespace CalamityMod.Items.Dyes;

public class ElementalDye : BaseDye
{
	public override ArmorShaderData ShaderDataToBind => new ArmorShaderData(base.Mod.Assets.Request<Effect>("Effects/Dyes/ElementalDyeShader"), "DyePass").UseImage("Images/Misc/Perlin");

	public override void SafeSetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
	}

	public override void SafeSetDefaults()
	{
		base.Item.rare = 11;
		base.Item.value = Item.sellPrice(0, 2, 50);
	}

	public override void AddRecipes()
	{
		CreateRecipe(5).AddIngredient(3526).AddIngredient(3528).AddIngredient(3527)
			.AddIngredient(3529)
			.AddIngredient<MeldDye>()
			.AddTile(228)
			.Register();
	}
}
