using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;

namespace CalamityMod.Items.Dyes;

public class PinkStatigelDye : BaseDye
{
	public override ArmorShaderData ShaderDataToBind
	{
		get
		{
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			return new ArmorShaderData(base.Mod.Assets.Request<Effect>("Effects/Dyes/SlimeGodDyeShader"), "DyePass").UseColor(new Color(249, 129, 185)).UseSecondaryColor(new Color(131, 58, 103)).UseImage("Images/Misc/Perlin");
		}
	}

	public override void SafeSetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
	}

	public override void SafeSetDefaults()
	{
		base.Item.rare = 4;
		base.Item.value = Item.sellPrice(0, 0, 20);
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient(126, 2).AddIngredient<PurifiedGel>().AddTile(228)
			.Register();
	}
}
