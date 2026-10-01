using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Items.Dyes;

public class TarragonDye : BaseDye
{
	public override ArmorShaderData ShaderDataToBind
	{
		get
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			return new ArmorShaderData(base.Mod.Assets.Request<Effect>("Effects/Dyes/TarragonDyeShader"), "DyePass").UseColor(new Color(20, 117, 70)).UseSecondaryColor(new Color(28, 255, 55)).UseImage(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Bark", (AssetRequestMode)2));
		}
	}

	public override void SafeSetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
	}

	public override void SafeSetDefaults()
	{
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.value = Item.sellPrice(0, 1, 50);
	}

	public override void AddRecipes()
	{
		CreateRecipe(3).AddIngredient(126, 3).AddIngredient<UelibloomOre>(4).AddTile(228)
			.Register();
	}
}
