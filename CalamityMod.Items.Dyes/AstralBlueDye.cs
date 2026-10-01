using CalamityMod.Items.Placeables.Ores;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;

namespace CalamityMod.Items.Dyes;

public class AstralBlueDye : BaseDye
{
	public override ArmorShaderData ShaderDataToBind
	{
		get
		{
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			return new ArmorShaderData(base.Mod.Assets.Request<Effect>("Effects/Dyes/AstralBlueDyeShader"), "DyePass").UseColor(new Color(109, 242, 197)).UseSecondaryColor(new Color(42, 147, 154));
		}
	}

	public override void SafeSetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
	}

	public override void SafeSetDefaults()
	{
		base.Item.rare = 9;
		base.Item.value = Item.sellPrice(0, 0, 75);
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient(126, 2).AddIngredient<AstralOre>().AddTile(228)
			.Register();
	}
}
