using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Light;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Utilities;

namespace CalamityMod.Walls;

[LegacyName(new string[] { "AstralGrassWallSafe" })]
public class AstralGrassWall : ModWall
{
	public static Asset<Texture2D> leafTexture;

	public static Vector2 TileAdj
	{
		get
		{
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_001a: Unknown result type (might be due to invalid IL or missing references)
			if (Lighting.Mode != LightMode.Retro && Lighting.Mode != LightMode.Trippy)
			{
				return Vector2.One * 12f;
			}
			return Vector2.Zero;
		}
	}

	public override void SetStaticDefaults()
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		base.DustType = 27;
		WallID.Sets.Conversion.Grass[base.Type] = true;
		Main.wallHouse[base.Type] = true;
		AddMapEntry(new Color(60, 48, 64));
		if (!Main.dedServ)
		{
			leafTexture = ModContent.Request<Texture2D>("CalamityMod/Walls/AstralGrassWallLeaves", (AssetRequestMode)2);
		}
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		if ((float)i > Main.screenPosition.X / 16f && (float)i < Main.screenPosition.X / 16f + (float)(Main.screenWidth / 16) && (float)j > Main.screenPosition.Y / 16f && (float)j < Main.screenPosition.Y / 16f + (float)(Main.screenHeight / 16))
		{
			Texture2D tex = leafTexture.Value;
			FastRandom rand = new FastRandom(i + j * 100000);
			float offset = (float)(i * j) % 6.28f + rand.NextFloat() / 8f;
			float sin = MathF.Sin((float)Main.GameUpdateCount / 45f + offset);
			spriteBatch.Draw(tex, (new Vector2((float)i + 0.5f, (float)j + 0.5f) + TileAdj) * 16f + new Vector2(1f, 0.5f) * sin * 2.2f - Main.screenPosition, (Rectangle?)new Rectangle(rand.Next(4) * 26, 0, 24, 24), Lighting.GetColor(i, j), offset + sin * 0.09f, new Vector2(12f, 12f), 1f + sin / 14f, (SpriteEffects)0, 0f);
		}
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}
}
