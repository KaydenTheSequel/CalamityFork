using System;
using System.Collections.Generic;
using CalamityMod.Items.Placeables.Crags;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Crags.Tree;

internal class SpineTree : ModTile
{
	public Asset<Texture2D> BottomTexture;

	public Asset<Texture2D> SegmentsTexture;

	public Asset<Texture2D> Rib1LeftTexture;

	public Asset<Texture2D> Rib1RightTexture;

	public Asset<Texture2D> Rib2LeftTexture;

	public Asset<Texture2D> Rib2RightTexture;

	public Asset<Texture2D> Rib3LeftTexture;

	public Asset<Texture2D> Rib3RightTexture;

	public Asset<Texture2D> TopTexture;

	public static Vector2 TileOffset
	{
		get
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0029: Unknown result type (might be due to invalid IL or missing references)
			if (Lighting.LegacyEngine.Mode <= 1 || Main.GameZoomTarget != 1f)
			{
				return Vector2.One * 12f;
			}
			return Vector2.Zero;
		}
	}

	public override void SetStaticDefaults()
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		TileID.Sets.IsATreeTrunk[base.Type] = true;
		Main.tileFrameImportant[base.Type] = true;
		Main.tileAxe[base.Type] = true;
		Main.tileMergeDirt[base.Type] = false;
		Main.tileSolid[base.Type] = false;
		Main.tileLighted[base.Type] = false;
		Main.tileBlockLight[base.Type] = false;
		AddMapEntry(new Color(38, 25, 27), CreateMapEntryName());
		base.DustType = 155;
		base.HitSound = SoundID.DD2_SkeletonHurt;
		RegisterItemDrop(ModContent.ItemType<global::CalamityMod.Items.Placeables.Crags.ScorchedBone>());
	}

	public override bool TileFrame(int i, int j, ref bool resetFrame, ref bool noBreak)
	{
		resetFrame = false;
		noBreak = true;
		return false;
	}

	public static bool SolidTile(int i, int j)
	{
		if (Framing.GetTileSafely(i, j).HasTile)
		{
			return Main.tileSolid[Framing.GetTileSafely(i, j).TileType];
		}
		return false;
	}

	public static bool SolidTopTile(int i, int j)
	{
		if (Framing.GetTileSafely(i, j).HasTile)
		{
			if (!Main.tileSolidTop[Framing.GetTileSafely(i, j).TileType])
			{
				return Main.tileSolid[Framing.GetTileSafely(i, j).TileType];
			}
			return true;
		}
		return false;
	}

	public static bool Spawn(int i, int j, int minSize = 5, int maxSize = 18, bool saplingExists = false)
	{
		int height = Main.rand.Next(minSize, maxSize);
		for (int k = 1; k < height; k++)
		{
			if (SolidTile(i, j - k))
			{
				height = k - 2;
				break;
			}
		}
		if (height < minSize)
		{
			return false;
		}
		int sapplingType = ModContent.TileType<SpineSapling>();
		if (!WorldGen.EmptyTileCheck(i - 2, i + 2, j - height, j, sapplingType))
		{
			return false;
		}
		if (saplingExists)
		{
			WorldGen.KillTile(i, j, fail: false, effectOnly: false, noItem: true);
			WorldGen.KillTile(i, j - 1, fail: false, effectOnly: false, noItem: true);
		}
		if ((SolidTopTile(i, j + 1) || SolidTile(i, j + 1)) && !Framing.GetTileSafely(i, j).HasTile)
		{
			WorldGen.PlaceTile(i, j, ModContent.TileType<SpineTree>(), mute: true);
			Framing.GetTileSafely(i, j).TileFrameY = (short)(WorldGen.genRand.Next(3) * 18);
			int branchSegmentDelay = 0;
			for (int l = 1; l < height; l++)
			{
				WorldGen.PlaceTile(i, j - l, ModContent.TileType<SpineTree>(), mute: true);
				if (branchSegmentDelay > 0)
				{
					branchSegmentDelay--;
				}
				if (Main.rand.NextBool() && branchSegmentDelay == 0 && l > 5)
				{
					if (l > 1 && l < 10)
					{
						Framing.GetTileSafely(i, j - l).TileFrameX = 54;
					}
					if (l >= 10 && l < 17)
					{
						Framing.GetTileSafely(i, j - l).TileFrameX = 72;
					}
					if (l >= 17)
					{
						Framing.GetTileSafely(i, j - l).TileFrameX = 90;
					}
					Framing.GetTileSafely(i, j - l).TileFrameY = (short)(Main.rand.Next(3) * 18);
					branchSegmentDelay = 3;
				}
				else
				{
					Framing.GetTileSafely(i, j - l).TileFrameX = 18;
					Framing.GetTileSafely(i, j - l).TileFrameY = (short)(Main.rand.Next(3) * 18);
				}
				if (l == height - 1)
				{
					Framing.GetTileSafely(i, j - l).TileFrameX = 36;
					Framing.GetTileSafely(i, j - l).TileFrameY = (short)(Main.rand.Next(3) * 18);
				}
			}
			return true;
		}
		return false;
	}

	public override void NearbyEffects(int i, int j, bool closer)
	{
		if (!Framing.GetTileSafely(i, j + 1).HasTile)
		{
			WorldGen.KillTile(i, j);
			if (Main.netMode == 1)
			{
				NetMessage.SendData(17, -1, -1, null, 0, i, j);
			}
		}
	}

	public override IEnumerable<Item> GetItemDrops(int i, int j)
	{
		if (Framing.GetTileSafely(i, j).TileFrameX == 36)
		{
			int totalSeeds = Main.rand.Next(1, 3);
			for (int numSeed = 0; numSeed < totalSeeds; numSeed++)
			{
				yield return new Item(ModContent.ItemType<global::CalamityMod.Items.Placeables.Crags.SpineSapling>());
			}
		}
		if (Main.rand.NextBool())
		{
			yield return new Item(ModContent.ItemType<global::CalamityMod.Items.Placeables.Crags.ScorchedBone>());
		}
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		Framing.GetTileSafely(i, j);
		if (!fail)
		{
			Tile tileSafely = Framing.GetTileSafely(i, j + 1);
			_ = ref tileSafely.TileFrameX;
			SoundEngine.PlaySound(in SoundID.DD2_SkeletonHurt, new Vector2((float)i, (float)j) * 16f);
			tileSafely = Framing.GetTileSafely(i, j);
			_ = tileSafely.TileFrameX;
			tileSafely = Framing.GetTileSafely(i, j);
			_ = tileSafely.TileFrameX;
			_ = 18;
			tileSafely = Framing.GetTileSafely(i, j);
			_ = tileSafely.TileFrameX;
			_ = 54;
			tileSafely = Framing.GetTileSafely(i, j);
			_ = tileSafely.TileFrameX;
			_ = 72;
			tileSafely = Framing.GetTileSafely(i, j);
			_ = tileSafely.TileFrameX;
			_ = 90;
			tileSafely = Framing.GetTileSafely(i, j);
			_ = tileSafely.TileFrameX;
			_ = 36;
		}
	}

	public static Vector2 TileCustomPosition(int i, int j, Vector2? off = null)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		return (new Vector2((float)i, (float)j) + TileOffset) * 16f - Main.screenPosition - (Vector2)(((_003F?)off) ?? new Vector2(0f, -2f));
	}

	internal static void DrawTreeSegments(int i, int j, Texture2D tex, Rectangle? source, Vector2? offset = null, Vector2? origin = null, bool Glow = false)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		Vector2 drawPos = Utils.ToWorldCoordinates(new Vector2((float)i, (float)j), 8f, 8f) - Main.screenPosition + (Vector2)(((_003F?)offset) ?? new Vector2(0f, -2f));
		Color color = Lighting.GetColor(i, j);
		Main.spriteBatch.Draw(tex, drawPos, source, Glow ? Color.White : color, 0f, (Vector2)(((_003F?)origin) ?? (source.Value.Size() / 3f)), 1f, (SpriteEffects)0, 0f);
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02be: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0404: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_041d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_044c: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0465: Unknown result type (might be due to invalid IL or missing references)
		//IL_0628: Unknown result type (might be due to invalid IL or missing references)
		//IL_0632: Unknown result type (might be due to invalid IL or missing references)
		//IL_0641: Unknown result type (might be due to invalid IL or missing references)
		//IL_064b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0666: Unknown result type (might be due to invalid IL or missing references)
		//IL_0670: Unknown result type (might be due to invalid IL or missing references)
		//IL_067f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0689: Unknown result type (might be due to invalid IL or missing references)
		//IL_051c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_0535: Unknown result type (might be due to invalid IL or missing references)
		//IL_053f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0563: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_057c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0586: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c4: Unknown result type (might be due to invalid IL or missing references)
		if (Main.tile[i, j].IsTileActuallyInvisible())
		{
			return false;
		}
		Tile tile = Framing.GetTileSafely(i, j);
		float xOff = (float)Math.Sin((float)(j * 19) * 0.04f) * 1.2f;
		if (xOff == 1f && (float)j / 4f == 0f)
		{
			xOff = 0f;
		}
		int frameOff = 0;
		Vector2 baseSegmentOffset = default(Vector2);
		((Vector2)(ref baseSegmentOffset))._002Ector(xOff * 2f - (float)(frameOff / 2) + 26f, 14f);
		Vector2 treeSegmentOffset = default(Vector2);
		((Vector2)(ref treeSegmentOffset))._002Ector(xOff * 2f - (float)(frameOff / 2) + 25f, 14f);
		Vector2 topSegmentOffset = default(Vector2);
		((Vector2)(ref topSegmentOffset))._002Ector(xOff * 2f - (float)(frameOff / 2) + 20f, 16f);
		if (BottomTexture == null)
		{
			BottomTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Crags/Tree/SpineBottom", (AssetRequestMode)2);
		}
		if (SegmentsTexture == null)
		{
			SegmentsTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Crags/Tree/SpineSegments", (AssetRequestMode)2);
		}
		Texture2D segmentTex = SegmentsTexture.Value;
		if (Framing.GetTileSafely(i, j).TileFrameX == 0)
		{
			int frame = tile.TileFrameY / 18;
			DrawTreeSegments(i, j, BottomTexture.Value, (Rectangle?)new Rectangle(34 * frame, 0, 32, 20), (Vector2?)TileOffset.ToWorldCoordinates(), (Vector2?)baseSegmentOffset, false);
		}
		if (Framing.GetTileSafely(i, j).TileFrameX == 18)
		{
			int frame2 = tile.TileFrameY / 18;
			DrawTreeSegments(i, j, segmentTex, (Rectangle?)new Rectangle(34 * frame2, 0, 32, 20), (Vector2?)TileOffset.ToWorldCoordinates(), (Vector2?)treeSegmentOffset, false);
		}
		if (Framing.GetTileSafely(i, j).TileFrameX == 54)
		{
			int frame3 = tile.TileFrameY / 18;
			if (Rib1LeftTexture == null)
			{
				Rib1LeftTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Crags/Tree/SpineRib1Left", (AssetRequestMode)2);
			}
			if (Rib1RightTexture == null)
			{
				Rib1RightTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Crags/Tree/SpineRib1Right", (AssetRequestMode)2);
			}
			Vector2 leftBranchOffset = default(Vector2);
			((Vector2)(ref leftBranchOffset))._002Ector(xOff * 2f - (float)(frameOff / 2) + 50f, 14f);
			Vector2 rightBranchOffset = default(Vector2);
			((Vector2)(ref rightBranchOffset))._002Ector(xOff * 2f - (float)(frameOff / 2) + 4f, 14f);
			DrawTreeSegments(i, j, Rib1LeftTexture.Value, (Rectangle?)new Rectangle(38 * frame3, 0, 38, 40), (Vector2?)TileOffset.ToWorldCoordinates(), (Vector2?)leftBranchOffset, false);
			DrawTreeSegments(i, j, Rib1RightTexture.Value, (Rectangle?)new Rectangle(38 * frame3, 0, 38, 40), (Vector2?)TileOffset.ToWorldCoordinates(), (Vector2?)rightBranchOffset, false);
			DrawTreeSegments(i, j, segmentTex, (Rectangle?)new Rectangle(34 * frame3, 0, 32, 20), (Vector2?)TileOffset.ToWorldCoordinates(), (Vector2?)treeSegmentOffset, false);
		}
		if (Framing.GetTileSafely(i, j).TileFrameX == 72)
		{
			int frame4 = tile.TileFrameY / 18;
			if (Rib2LeftTexture == null)
			{
				Rib2LeftTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Crags/Tree/SpineRib2Left", (AssetRequestMode)2);
			}
			if (Rib2RightTexture == null)
			{
				Rib2RightTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Crags/Tree/SpineRib2Right", (AssetRequestMode)2);
			}
			Vector2 leftBranchOffset2 = default(Vector2);
			((Vector2)(ref leftBranchOffset2))._002Ector(xOff * 2f - (float)(frameOff / 2) + 60f, 14f);
			Vector2 rightBranchOffset2 = default(Vector2);
			((Vector2)(ref rightBranchOffset2))._002Ector(xOff * 2f - (float)(frameOff / 2) + 4f, 14f);
			DrawTreeSegments(i, j, Rib2LeftTexture.Value, (Rectangle?)new Rectangle(50 * frame4, 0, 50, 54), (Vector2?)TileOffset.ToWorldCoordinates(), (Vector2?)leftBranchOffset2, false);
			DrawTreeSegments(i, j, Rib2RightTexture.Value, (Rectangle?)new Rectangle(50 * frame4, 0, 50, 54), (Vector2?)TileOffset.ToWorldCoordinates(), (Vector2?)rightBranchOffset2, false);
			DrawTreeSegments(i, j, segmentTex, (Rectangle?)new Rectangle(34 * frame4, 0, 32, 20), (Vector2?)TileOffset.ToWorldCoordinates(), (Vector2?)treeSegmentOffset, false);
		}
		if (Framing.GetTileSafely(i, j).TileFrameX == 90)
		{
			int frame5 = tile.TileFrameY / 18;
			if (Rib3LeftTexture == null)
			{
				Rib3LeftTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Crags/Tree/SpineRib3Left", (AssetRequestMode)2);
			}
			if (Rib3RightTexture == null)
			{
				Rib3RightTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Crags/Tree/SpineRib3Right", (AssetRequestMode)2);
			}
			Vector2 leftBranchOffset3 = default(Vector2);
			((Vector2)(ref leftBranchOffset3))._002Ector(xOff * 2f - (float)(frameOff / 2) + 75f, 14f);
			Vector2 rightBranchOffset3 = default(Vector2);
			((Vector2)(ref rightBranchOffset3))._002Ector(xOff * 2f - (float)(frameOff / 2) + 4f, 14f);
			DrawTreeSegments(i, j, Rib3LeftTexture.Value, (Rectangle?)new Rectangle(62 * frame5, 0, 62, 60), (Vector2?)TileOffset.ToWorldCoordinates(), (Vector2?)leftBranchOffset3, false);
			DrawTreeSegments(i, j, Rib3RightTexture.Value, (Rectangle?)new Rectangle(62 * frame5, 0, 62, 60), (Vector2?)TileOffset.ToWorldCoordinates(), (Vector2?)rightBranchOffset3, false);
			DrawTreeSegments(i, j, segmentTex, (Rectangle?)new Rectangle(34 * frame5, 0, 32, 20), (Vector2?)TileOffset.ToWorldCoordinates(), (Vector2?)treeSegmentOffset, false);
		}
		if (Framing.GetTileSafely(i, j).TileFrameX == 36)
		{
			if (TopTexture == null)
			{
				TopTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Crags/Tree/SpineTop", (AssetRequestMode)2);
			}
			int frame6 = tile.TileFrameY / 18;
			DrawTreeSegments(i, j - 1, TopTexture.Value, (Rectangle?)new Rectangle(26 * frame6, 0, 24, 24), (Vector2?)TileOffset.ToWorldCoordinates(), (Vector2?)topSegmentOffset, false);
			DrawTreeSegments(i, j, segmentTex, (Rectangle?)new Rectangle(34 * frame6, 0, 32, 20), (Vector2?)TileOffset.ToWorldCoordinates(), (Vector2?)treeSegmentOffset, false);
		}
		return false;
	}
}
