using CalamityMod.Rarities;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Items.Dyes;

public class ProfanedMoonlightDye : BaseDye
{
	public override ArmorShaderData ShaderDataToBind => new ArmorShaderData(base.Mod.Assets.Request<Effect>("Effects/Dyes/ProfanedMoonlightDye"), "DyePass");

	public override void SafeSetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 3;
	}

	public override void SafeSetDefaults()
	{
		base.Item.rare = ModContent.RarityType<Turquoise>();
		base.Item.value = Item.sellPrice(0, 1, 50);
	}
}
