using System;
using CalamityMod.Items.Tools;
using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles;

public class WulfrumPipes : ModTile, ISpecialTempTileDraw
{
	public static int PlaceTimeMax = 10;

	public static Vector2 DisplaceStart(Point pos)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(0f, 0f - (pos.GetTileRNG() * 10f + 7f));
	}

	public static float RotationStart(Point pos)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		return -(float)Math.PI / 5f + (float)Math.PI * 2f / 5f * pos.GetTileRNG(1);
	}

	public override void SetStaticDefaults()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		base.HitSound = SoundID.Item52;
		base.DustType = 83;
		AddMapEntry(new Color(128, 90, 77));
		Main.tileLighted[base.Type] = true;
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0f;
		g = 0.6f;
		b = 0.3f;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (Main.rand.NextBool(3) ? 3 : (Main.rand.NextBool(3) ? 1 : 2));
	}

	public override void PlaceInWorld(int i, int j, Item item)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.Item52 with
		{
			Volume = SoundID.Item52.Volume * 0.75f,
			Pitch = SoundID.Item52.Pitch - 0.5f
		};
		SoundEngine.PlaySound(in style, (Vector2?)new Vector2((float)(i * 16), (float)(j * 16)), (SoundUpdateCallback?)null);
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		return false;
	}

	public void CoolDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		Point pos = default(Point);
		((Point)(ref pos))._002Ector(i, j);
		Tile tile = Main.tile[pos];
		if (!tile.IsTileActuallyInvisible())
		{
			float timeToGo = pos.GetTileRNG(2) * 22f;
			float animProgress = (float)Math.Pow(MathHelper.Clamp(((float)TempTilesManagerSystem.GetTemporaryTileTime(pos) - ((float)WulfrumScaffoldKit.TileTime - timeToGo)) / timeToGo, 0f, 1f), 2.0);
			Vector2 position = pos.ToWorldCoordinates() + DisplaceStart(pos) * animProgress - Main.screenPosition;
			Rectangle frame = default(Rectangle);
			((Rectangle)(ref frame))._002Ector((int)tile.TileFrameX, (int)tile.TileFrameY, 16, 16);
			Color tileColor = Lighting.GetColor(pos) * (1f - animProgress);
			Main.spriteBatch.Draw(TextureAssets.Tile[base.Type].Value, position, (Rectangle?)frame, tileColor, RotationStart(pos) * animProgress, frame.Size() / 2f, 1f, (SpriteEffects)0, 0f);
		}
	}
}
