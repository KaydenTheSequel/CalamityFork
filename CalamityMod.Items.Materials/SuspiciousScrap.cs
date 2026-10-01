using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class SuspiciousScrap : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 5;
	}

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 30;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 0, 4);
		base.Item.rare = ModContent.RarityType<DarkOrange>();
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Item[base.Type].Value;
		Vector2 positionDisplace = new Vector2(32f, 32f) * scale;
		Rectangle variant = default(Rectangle);
		((Rectangle)(ref variant))._002Ector((WorldGen.SavedOreTiers.Copper == 7) ? 32 : 0, (WorldGen.SavedOreTiers.Iron == 6) ? 32 : 0, 30, 30);
		spriteBatch.Draw(tex, position + positionDisplace, (Rectangle?)variant, drawColor, 0f, origin, scale * 2f, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Item[base.Type].Value;
		Rectangle variant = default(Rectangle);
		((Rectangle)(ref variant))._002Ector((WorldGen.SavedOreTiers.Copper == 7) ? 32 : 0, (WorldGen.SavedOreTiers.Iron == 6) ? 32 : 0, 30, 30);
		Vector2 positionDisplace = new Vector2(16f, 16f) * scale;
		spriteBatch.Draw(tex, base.Item.position + positionDisplace - Main.screenPosition, (Rectangle?)variant, lightColor, rotation, variant.Size() / 2f, scale, (SpriteEffects)0, 0f);
		return false;
	}
}
