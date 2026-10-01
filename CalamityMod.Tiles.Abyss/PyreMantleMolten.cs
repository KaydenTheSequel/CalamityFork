using System;
using CalamityMod.Sounds;
using CalamityMod.Tiles.Abyss.AbyssAmbient;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Tiles.Abyss;

public class PyreMantleMolten : GlowMaskTile
{
	public override string GlowMaskAsset => Texture + "_Glowmask";

	public override void SetupStatic()
	{
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		Main.tileLighted[base.Type] = true;
		Main.tileSolid[base.Type] = true;
		Main.tileBlockLight[base.Type] = true;
		Main.tileBrick[base.Type] = true;
		CalamityUtils.MergeWithGeneral(base.Type);
		CalamityUtils.MergeWithAbyss(base.Type);
		TileID.Sets.ChecksForMerge[base.Type] = true;
		base.HitSound = CommonCalamitySounds.VoidstoneMine;
		base.MineResist = 10f;
		base.MinPick = 180;
		AddMapEntry(new Color(113, 49, 16));
		this.RegisterBlendMergeWith(0);
		this.RegisterBlendMergeWith(1);
		this.RegisterBlendMergeWith(ModContent.TileType<AbyssGravel>());
		this.RegisterBlendMergeWith(ModContent.TileType<PyreMantle>());
	}

	public override bool CreateDust(int i, int j, ref int type)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		Dust.NewDust(new Vector2((float)i, (float)j) * 16f, 16, 16, 162, 0f, 0f, 1, new Color(128, 128, 128));
		return false;
	}

	public override bool CanExplode(int i, int j)
	{
		return false;
	}

	public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
	{
		global::CalamityMod.World.Abyss.FillTileWithWater(i, j);
	}

	public override void RandomUpdate(int i, int j)
	{
		Tile tile = Main.tile[i, j];
		Tile up = Main.tile[i, j - 1];
		Tile up2 = Main.tile[i, j - 2];
		if (WorldGen.genRand.NextBool(12) && !up.HasTile && !up2.HasTile && up.LiquidAmount > 0 && up2.LiquidAmount > 0 && !tile.LeftSlope && !tile.RightSlope && !tile.IsHalfBlock)
		{
			up.TileType = (ushort)ModContent.TileType<PhoviamareHalm>();
			up.HasTile = true;
			up.TileFrameY = 0;
			up.TileFrameX = (short)(WorldGen.genRand.Next(16) * 18);
			WorldGen.SquareTileFrame(i, j - 1);
			if (Main.dedServ)
			{
				NetMessage.SendTileSquare(-1, i, j - 1, 3);
			}
		}
	}

	public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
	{
		float brightness = 0.7f;
		float lightspeed = (float)(Main.timeForVisualEffects * 0.01);
		brightness *= MathF.Sin((float)i / 60f + lightspeed);
		brightness += 0.3f;
		r = 1f;
		g = 0.33f;
		b = 0f;
		r *= brightness;
		g *= brightness;
		b *= brightness;
	}

	public override Color GetGlowMaskColor(int i, int j, TileDrawInfo drawData)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		float glowbrightness = 1f;
		float glowspeed = (float)(Main.timeForVisualEffects * 0.01);
		glowbrightness *= MathF.Sin((float)i / 60f + glowspeed);
		return Color.White * glowbrightness;
	}
}
