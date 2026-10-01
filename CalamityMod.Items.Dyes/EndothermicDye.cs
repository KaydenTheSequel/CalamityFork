using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Items.Dyes;

public class EndothermicDye : BaseDye
{
	public override ArmorShaderData ShaderDataToBind
	{
		get
		{
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			return new ArmorShaderData(base.Mod.Assets.Request<Effect>("Effects/Dyes/EndothermicDyeShader"), "DyePass").UseColor(new Color(123, 205, 237)).UseSecondaryColor(new Color(85, 85, 171));
		}
	}

	public override void SafeSetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
	}

	public override void SafeSetDefaults()
	{
		base.Item.rare = ModContent.RarityType<CosmicPurple>();
		base.Item.value = Item.sellPrice(0, 1, 50);
	}

	public override void AddRecipes()
	{
		CreateRecipe(3).AddIngredient(126, 3).AddIngredient<EndothermicEnergy>(5).AddTile(228)
			.Register();
	}
}
