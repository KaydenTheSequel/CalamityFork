using CalamityMod.Items.Placeables.FurnitureProfaned;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Items.Dyes;

public class ProfanedRockDye : BaseDye
{
	public override ArmorShaderData ShaderDataToBind
	{
		get
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			return new ArmorShaderData(base.Mod.Assets.Request<Effect>("Effects/Dyes/ProfanedRockDyeShader"), "DyePass").UseColor(new Color(79, 16, 16)).UseSecondaryColor(new Color(173, 116, 0)).UseImage(ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/GreyscaleGradients/Veins", (AssetRequestMode)2));
		}
	}

	public override void SafeSetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
	}

	public override void SafeSetDefaults()
	{
		base.Item.rare = 11;
		base.Item.value = Item.sellPrice(0, 1, 50);
	}

	public override void AddRecipes()
	{
		CreateRecipe(3).AddIngredient(126, 3).AddIngredient<ProfanedRock>(15).AddTile(228)
			.Register();
	}
}
