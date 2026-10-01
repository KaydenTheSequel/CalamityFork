using CalamityMod.Items.Placeables.Astral;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;

namespace CalamityMod.Items.Dyes;

public class AstralDye : BaseDye
{
	public override ArmorShaderData ShaderDataToBind
	{
		get
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			return new ArmorShaderData(base.Mod.Assets.Request<Effect>("Effects/Dyes/AstralDyeShader"), "DyePass").UseColor(new Color(123, 99, 130)).UseSecondaryColor(new Color(189, 66, 84)).UseImage("Images/Misc/Perlin");
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
		CreateRecipe().AddIngredient(126).AddIngredient<AstralStone>(15).AddTile(228)
			.Register();
	}
}
