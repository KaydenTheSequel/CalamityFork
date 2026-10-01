using CalamityMod.Items.TreasureBags.MiscGrabBags;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Items.Dyes;

public class NecromanticDye : BaseDye
{
	public override ArmorShaderData ShaderDataToBind
	{
		get
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_003b: Unknown result type (might be due to invalid IL or missing references)
			return new ArmorShaderData(base.Mod.Assets.Request<Effect>("Effects/Dyes/NecromanticDyeShader"), "DyePass").UseColor(new Color(71, 23, 26)).UseSecondaryColor(new Color(10, 198, 255)).UseImage(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/SharpNoise", (AssetRequestMode)2));
		}
	}

	public override void SafeSetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
	}

	public override void SafeSetDefaults()
	{
		base.Item.rare = 8;
		base.Item.value = Item.sellPrice(0, 1, 50);
	}

	public override void AddRecipes()
	{
		CreateRecipe(2).AddIngredient(126, 2).AddIngredient<FleshyGeode>().AddTile(228)
			.Register();
		CreateRecipe(3).AddIngredient(126, 3).AddIngredient<NecromanticGeode>().AddTile(228)
			.Register();
	}
}
