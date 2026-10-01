using System;
using CalamityMod.Rarities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Items.Materials;

public class PlasmaDriveCore : ModItem, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Items.Materials";

	public override void SetDefaults()
	{
		base.Item.width = 30;
		base.Item.height = 30;
		base.Item.maxStack = Item.CommonMaxStack;
		base.Item.value = 0;
		base.Item.rare = ModContent.RarityType<DarkOrange>();
	}

	public override bool PreDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0165: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Item[base.Type].Value;
		spriteBatch.End();
		Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
		Color outlineColor = Color.Lerp(Color.Cyan, Color.Orange, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2f) * 0.5f + 0.5f);
		Vector3 outlineHSL = Main.rgbToHsl(outlineColor);
		GameShaders.Misc["CalamityMod:BasicTint"].UseOpacity(1f);
		GameShaders.Misc["CalamityMod:BasicTint"].UseColor(Main.hslToRgb(1f - outlineHSL.X, outlineHSL.Y, outlineHSL.Z));
		GameShaders.Misc["CalamityMod:BasicTint"].Apply();
		float positionOffset = MathHelper.Lerp(2f, 3f, (float)Math.Sin(Main.GlobalTimeWrappedHourly * 2f) * 0.5f + 0.5f) * scale;
		for (float i = 0f; i < 1f; i += 0.25f)
		{
			spriteBatch.Draw(tex, position + (i * ((float)Math.PI * 2f)).ToRotationVector2() * positionOffset, (Rectangle?)frame, outlineColor, 0f, origin, scale, (SpriteEffects)0, 0f);
		}
		spriteBatch.End();
		spriteBatch.Begin((SpriteSortMode)0, (BlendState)null, (SamplerState)null, (DepthStencilState)null, (RasterizerState)null, (Effect)null, Main.UIScaleMatrix);
		return true;
	}
}
