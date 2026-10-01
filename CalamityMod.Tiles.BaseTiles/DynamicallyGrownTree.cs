using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.Utilities;

namespace CalamityMod.Tiles.BaseTiles;

public abstract class DynamicallyGrownTree : ModTile
{
	internal int ItemDrop;

	public const int ControlPointCountPerBranch = 8;

	public BranchDrawer BranchDrawer { get; internal set; }

	public abstract float MaxDistanceBeforeCutoff { get; }

	public abstract float DistanceUsedForTrunk { get; }

	public abstract float BranchMaxBendFactor { get; }

	public abstract float BranchTurnAngleVariance { get; }

	public abstract float MinBranchLength { get; }

	public abstract float TrunkWidth { get; }

	public abstract float ChanceToCreateNewBranches { get; }

	public abstract float VerticalStretchFactor { get; }

	public abstract float DownwardBiasFactor { get; }

	public abstract float BranchGrowthWidthDecay { get; }

	public abstract int MaxCutoffBranchesPerBranch { get; }

	public UnifiedRandom RNG => BranchDrawer.RNG;

	public override void Load()
	{
		BranchDrawer = new BranchDrawer
		{
			MaxDistanceBeforeCutoff = MaxDistanceBeforeCutoff,
			DistanceUsedForTrunk = DistanceUsedForTrunk,
			BranchMaxBendFactor = BranchMaxBendFactor,
			BranchTurnAngleVariance = BranchTurnAngleVariance,
			MinBranchLength = MinBranchLength,
			TrunkWidth = TrunkWidth,
			ChanceToCreateNewBranches = ChanceToCreateNewBranches,
			VerticalStretchFactor = VerticalStretchFactor,
			DownwardBiasFactor = DownwardBiasFactor,
			BranchGrowthWidthDecay = BranchGrowthWidthDecay,
			MaxCutoffBranchesPerBranch = MaxCutoffBranchesPerBranch
		};
		if (!Main.dedServ)
		{
			BranchDrawer.BarkTexture = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)1).Value;
		}
	}

	public virtual void DrawThingAtEndOfBranch(BranchDrawer.Branch branch)
	{
	}

	public void Draw(Point p)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		TileID.Sets.DrawTileInSolidLayer[base.Type] = true;
		BranchDrawer.Draw(p);
		Dictionary<BranchDrawer.Branch, List<BranchDrawer.Branch>> source = BranchDrawer.GenerateBranches(p);
		source.Select((KeyValuePair<BranchDrawer.Branch, List<BranchDrawer.Branch>> b) => b.Key);
		foreach (BranchDrawer.Branch outwardmostBranch in from b in source
			where (float)b.Value.Count <= 0f
			select b.Key)
		{
			DrawThingAtEndOfBranch(outwardmostBranch);
		}
	}

	public void UseDefaultSize()
	{
		TileObjectData.newTile.Width = (int)Math.Ceiling(TrunkWidth / 16f);
		TileObjectData.newTile.Height = (int)Math.Ceiling(DistanceUsedForTrunk / 16f);
		TileObjectData.newTile.Origin = new Point16(TileObjectData.newTile.Width / 2, TileObjectData.newTile.Height - 1);
		TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.SolidTile | AnchorType.SolidWithTop, TileObjectData.newTile.Width, 0);
		TileObjectData.newTile.CoordinateHeights = Enumerable.Repeat(16, TileObjectData.newTile.Height).ToArray();
		TileObjectData.newTile.CoordinateWidth = 16;
		TileObjectData.newTile.CoordinatePadding = 2;
		TileObjectData.newTile.DrawYOffset = 2;
	}

	public override bool CanDrop(int i, int j)
	{
		return false;
	}

	public override void KillMultiTile(int x, int y, int frameX, int frameY)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		foreach (BranchDrawer.Branch branch in from b in BranchDrawer.GenerateBranches(new Point(x, y))
			select b.Key)
		{
			int totalWood = (int)Math.Ceiling(branch.CurveLength / 56f);
			int totalDust = totalWood * 3;
			Main.rand.Next(1, 3);
			for (int i = 0; i < totalWood; i++)
			{
				Vector2 woodPosition = branch.Curve.Evaluate((float)i / ((float)totalWood - 1f));
				woodPosition += Utils.ToWorldCoordinates(new Vector2((float)x, (float)y), 8f, 8f) + Main.rand.NextVector2Circular(10f, 10f);
				woodPosition.Y += DistanceUsedForTrunk;
				Item.NewItem(new EntitySource_TileBreak(x, y), woodPosition, ItemDrop);
			}
			for (int i2 = 0; i2 < totalDust; i2++)
			{
				Vector2 dustPosition = branch.Curve.Evaluate((float)i2 / ((float)totalDust - 1f));
				dustPosition += Utils.ToWorldCoordinates(new Vector2((float)x, (float)y), 8f, 8f) + Main.rand.NextVector2Circular(5f, 5f);
				dustPosition.Y += DistanceUsedForTrunk;
				Dust.NewDustDirect(dustPosition, 4, 4, base.DustType);
			}
		}
	}

	public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		if (Main.tile[i, j].IsTileActuallyInvisible())
		{
			return false;
		}
		Tile t = CalamityUtils.ParanoidTileRetrieval(i, j);
		if (t.TileFrameX != 0 || t.TileFrameY != ((int)Math.Ceiling(DistanceUsedForTrunk / 16f) - 1) * 18)
		{
			return false;
		}
		Vector2 screenOffset = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
		Draw(new Point(i + (int)screenOffset.X / 16, j + (int)screenOffset.Y / 16));
		return false;
	}
}
