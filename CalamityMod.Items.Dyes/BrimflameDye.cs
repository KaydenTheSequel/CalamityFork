using CalamityMod.Items.Placeables.Crags;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;

namespace CalamityMod.Items.Dyes;

public class BrimflameDye : BaseDye
{
	public override ArmorShaderData ShaderDataToBind
	{
		get
		{
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			return new ArmorShaderData(base.Mod.Assets.Request<Effect>("Effects/Dyes/BrimflameDyeShader"), "DyePass").UseColor(new Color(252, 147, 34)).UseSecondaryColor(new Color(216, 41, 26)).UseImage("Images/Misc/Perlin");
		}
	}

	public override void SafeSetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
	}

	public override void SafeSetDefaults()
	{
		base.Item.rare = 5;
		base.Item.value = Item.sellPrice(0, 0, 20);
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient(126, 2).AddIngredient<BrimstoneSlag>(3).AddTile(228)
			.Register();
	}
}
