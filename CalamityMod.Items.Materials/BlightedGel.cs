using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

[LegacyName(new string[] { "EbonianGel" })]
public class BlightedGel : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetStaticDefaults()
	{
		base.Item.ResearchUnlockCount = 25;
	}

	public override void SetDefaults()
	{
		base.Item.width = 16;
		base.Item.height = 18;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = Item.sellPrice(0, 0, 0, 10);
		base.Item.rare = 1;
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		if (base.Item.notAmmo)
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Materials/BlightedGelRed", (AssetRequestMode)2).Value;
			spriteBatch.Draw(texture, position, (Rectangle?)frame, Color.White, 0f, origin, scale, (SpriteEffects)0, 0f);
		}
		return !base.Item.notAmmo;
	}

	public override bool PreDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		if (base.Item.notAmmo)
		{
			Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Items/Materials/BlightedGelRed", (AssetRequestMode)2).Value;
			spriteBatch.Draw(texture, base.Item.position - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, base.Item.width, base.Item.height), lightColor, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
		}
		return !base.Item.notAmmo;
	}
}
