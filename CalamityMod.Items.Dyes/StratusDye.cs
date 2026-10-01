using CalamityMod.Items.Materials;
using CalamityMod.Items.Placeables.Ores;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Items.Dyes;

public class StratusDye : BaseDye
{
	public override ArmorShaderData ShaderDataToBind
	{
		get
		{
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			return new ArmorShaderData(base.Mod.Assets.Request<Effect>("Effects/Dyes/StratusDyeShader"), "DyePass").UseColor(new Color(36, 86, 163)).UseSecondaryColor(new Color(124, 204, 223)).UseImage("Images/Misc/Perlin");
		}
	}

	public override void SafeSetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
	}

	public override void SafeSetDefaults()
	{
		base.Item.rare = ModContent.RarityType<PureGreen>();
		base.Item.value = Item.sellPrice(0, 1, 50);
	}

	public override void AddRecipes()
	{
		CreateRecipe(3).AddIngredient(126, 3).AddIngredient<Lumenyl>().AddIngredient<RuinousSoul>()
			.AddIngredient<ExodiumCluster>()
			.AddTile(228)
			.Register();
	}
}
