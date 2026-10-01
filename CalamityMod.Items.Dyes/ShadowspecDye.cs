using CalamityMod.Items.Materials;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Items.Dyes;

public class ShadowspecDye : BaseDye
{
	public override ArmorShaderData ShaderDataToBind
	{
		get
		{
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_003e: Unknown result type (might be due to invalid IL or missing references)
			return new ArmorShaderData(base.Mod.Assets.Request<Effect>("Effects/Dyes/ShadowspecDyeShader"), "DyePass").UseColor(new Color(46, 27, 60)).UseSecondaryColor(new Color(132, 142, 191)).UseImage("Images/Misc/Perlin");
		}
	}

	public override void SafeSetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
	}

	public override void SafeSetDefaults()
	{
		base.Item.rare = ModContent.RarityType<HotPink>();
		base.Item.value = Item.sellPrice(0, 2, 50);
	}

	public override void AddRecipes()
	{
		CreateRecipe(10).AddIngredient(126, 10).AddIngredient<ShadowspecBar>().AddTile(228)
			.Register();
	}
}
