using CalamityMod.Systems;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Ores;

public class AerialiteOre : ModTile
{
	public static readonly SoundStyle MineSound = new SoundStyle("CalamityMod/Sounds/Custom/MagicalRockMine", 3);

	internal static Texture2D GlowTexture;

	private int animationFrameWidth = 234;

	public override void SetStaticDefaults()
	{
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			GlowTexture = ModContent.Request<Texture2D>("CalamityMod/Tiles/Ores/AerialiteOre", (AssetRequestMode)1).Value;
		}
		Main.tileOreFinderPriority[base.Type] = 450;
		Main.tileBlockLight[base.Type] = false;
		Main.tileSolid[base.Type] = true;
		Main.tileLighted[base.Type] = true;
		Main.tileNoSunLight[base.Type] = false;
		TileID.Sets.Ore[base.Type] = true;
		CalamityUtils.SetMerge(base.Type, 189);
		CalamityUtils.SetMerge(base.Type, 196);
		CalamityUtils.SetMerge(base.Type, 460);
		Main.tileShine[base.Type] = 3500;
		Main.tileShine2[base.Type] = false;
		TileID.Sets.ChecksForMerge[base.Type] = true;
		base.DustType = 33;
		AddMapEntry(new Color(145, 255, 255), CreateMapEntryName());
		base.MinPick = 65;
		base.HitSound = MineSound;
		Main.tileSpelunker[base.Type] = true;
		this.RegisterBlendMergeWith(189);
		this.RegisterBlendMergeWith(196);
		this.RegisterBlendMergeWith(460);
		this.RegisterBlendMergeWith(0);
	}

	public override void PostSetDefaults()
	{
		Main.tileNoSunLight[base.Type] = false;
	}

	public override void NumDust(int i, int j, bool fail, ref int num)
	{
		num = (fail ? 1 : 3);
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		r = 0.14f;
		g = 0.346f;
		b = 0.42f;
	}

	public override void AnimateIndividualTile(int type, int i, int j, ref int frameXOffset, ref int frameYOffset)
	{
		frameXOffset = animationFrameWidth * TileFramingSystem.GetVariation4x4_012_Low0(i, j);
	}

	public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		if (GlowTexture == null)
		{
			return;
		}
		Tile tile = Main.tile[i, j];
		if (!tile.IsTileActuallyInvisible())
		{
			int xPos = tile.TileFrameX;
			int yPos = tile.TileFrameY;
			int xOffset = animationFrameWidth * TileFramingSystem.GetVariation4x4_012_Low0(i, j);
			xPos += xOffset;
			Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
			Vector2 drawOffset = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + zero;
			Color drawColour = CalamityUtils.ApplyPaint(Main.tile[i, j].TileColor, new Color(100, 100, 100, 50));
			if (!tile.IsHalfBlock && tile.Slope == SlopeType.Solid)
			{
				Main.spriteBatch.Draw(GlowTexture, drawOffset, (Rectangle?)new Rectangle(xPos, yPos, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
			else if (tile.IsHalfBlock)
			{
				Main.spriteBatch.Draw(GlowTexture, drawOffset + new Vector2(0f, 8f), (Rectangle?)new Rectangle(xPos, yPos, 18, 8), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
		}
	}
}
