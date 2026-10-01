using CalamityMod.Items.Materials;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Items.Dyes;

public class WulfrumDye : BaseDye
{
	public override ArmorShaderData ShaderDataToBind
	{
		get
		{
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			return new ArmorShaderData(base.Mod.Assets.Request<Effect>("Effects/Dyes/WulfrumDyeShader"), "DyePass").UseColor(new Color(89, 247, 166)).UseSecondaryColor(new Color(102, 242, 255)).UseImage(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/TechyNoise", (AssetRequestMode)2));
		}
	}

	public override void SafeSetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
	}

	public override void SafeSetDefaults()
	{
		base.Item.rare = 1;
		base.Item.value = Item.sellPrice(0, 0, 20);
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient(126, 2).AddIngredient<WulfrumMetalScrap>().AddTile(228)
			.Register();
	}
}
