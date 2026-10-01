using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

[LegacyName(new string[] { "EssenceofChaos" })]
public class EssenceofHavoc : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
		ItemID.Sets.ItemNoGravity[base.Type] = true;
		ItemID.Sets.SortingPriorityMaterials[base.Type] = 71;
	}

	public override void SetDefaults()
	{
		base.Item.width = 36;
		base.Item.height = 22;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 4);
		base.Item.rare = 4;
	}

	public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
	{
		base.Item.DrawItemGlowmaskSingleFrame(spriteBatch, rotation, TextureAssets.Item[base.Type].Value);
	}

	public override void Update(ref float gravity, ref float maxFallSpeed)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		float brightness = Main.essScale * Main.rand.NextFloat(0.9f, 1.1f);
		Lighting.AddLight(base.Item.Center, 0.5f * brightness, 0.3f * brightness, 0.05f * brightness);
	}
}
