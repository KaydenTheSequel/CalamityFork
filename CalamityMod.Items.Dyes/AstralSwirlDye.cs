using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;

namespace CalamityMod.Items.Dyes;

public class AstralSwirlDye : BaseDye
{
	public override ArmorShaderData ShaderDataToBind
	{
		get
		{
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			return new ArmorShaderData(base.Mod.Assets.Request<Effect>("Effects/Dyes/AstralSwirlDyeShader"), "DyePass").UseColor(new Color(42, 147, 154)).UseSecondaryColor(new Color(238, 93, 82)).UseImage("Images/Misc/Perlin");
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
		CreateRecipe(2).AddIngredient<AstralBlueDye>().AddIngredient<AstralOrangeDye>().AddTile(228)
			.Register();
	}
}
