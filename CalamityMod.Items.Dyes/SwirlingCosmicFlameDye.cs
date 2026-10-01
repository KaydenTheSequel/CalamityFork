using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Items.Dyes;

public class SwirlingCosmicFlameDye : BaseDye
{
	public override ArmorShaderData ShaderDataToBind
	{
		get
		{
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			return new ArmorShaderData(base.Mod.Assets.Request<Effect>("Effects/Dyes/CosmicFlameShader"), "DyePass").UseColor(new Color(52, 212, 229)).UseSecondaryColor(new Color(255, 115, 221)).UseImage("Images/Misc/noise")
				.UseSaturation(1f);
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
		CreateRecipe(2).AddIngredient<BlueCosmicFlameDye>().AddIngredient<PinkCosmicFlameDye>().AddTile(228)
			.Register();
	}
}
