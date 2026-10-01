using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Mechanic;

public class ArenaWallSystem : ModSystem
{
	public class Box
	{
		public Func<bool> RemovalCondition;

		public Vector2 position;

		public Vector4 boxDimensions;

		public Vector4 NewDimensions;

		public Vector2 NewPosition;

		public float borderThickness;

		public Color borderColor;

		public Box oldData;

		public Action<Box> DrawBox;

		public Action<Box> UpdateBox;

		public Func<Box, bool> DespawnAction;

		public float DistanceUp => boxDimensions.X;

		public float DistanceRight => boxDimensions.Y;

		public float DistanceDown => boxDimensions.Z;

		public float DistanceLeft => boxDimensions.W;

		public Vector2 TopLeft
		{
			get
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0014: Unknown result type (might be due to invalid IL or missing references)
				//IL_0019: Unknown result type (might be due to invalid IL or missing references)
				return position + new Vector2(0f - DistanceLeft, 0f - DistanceUp);
			}
		}

		public Vector2 TopRight
		{
			get
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0013: Unknown result type (might be due to invalid IL or missing references)
				//IL_0018: Unknown result type (might be due to invalid IL or missing references)
				return position + new Vector2(DistanceRight, 0f - DistanceUp);
			}
		}

		public Vector2 BottomLeft
		{
			get
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0013: Unknown result type (might be due to invalid IL or missing references)
				//IL_0018: Unknown result type (might be due to invalid IL or missing references)
				return position + new Vector2(0f - DistanceLeft, DistanceDown);
			}
		}

		public Vector2 BottomRight
		{
			get
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0012: Unknown result type (might be due to invalid IL or missing references)
				//IL_0017: Unknown result type (might be due to invalid IL or missing references)
				return position + new Vector2(DistanceRight, DistanceDown);
			}
		}

		public Vector2 Center
		{
			get
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_0007: Unknown result type (might be due to invalid IL or missing references)
				//IL_000c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0016: Unknown result type (might be due to invalid IL or missing references)
				return (TopLeft + BottomRight) * 0.5f;
			}
		}

		public Vector2 Size
		{
			get
			{
				//IL_001a: Unknown result type (might be due to invalid IL or missing references)
				return new Vector2(DistanceLeft + DistanceRight, DistanceUp + DistanceDown);
			}
		}

		public Vector4 Hitbox
		{
			get
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_000c: Unknown result type (might be due to invalid IL or missing references)
				//IL_0017: Unknown result type (might be due to invalid IL or missing references)
				//IL_0022: Unknown result type (might be due to invalid IL or missing references)
				//IL_002c: Unknown result type (might be due to invalid IL or missing references)
				return new Vector4(TopLeft.X, TopLeft.Y, Size.X, Size.Y);
			}
		}

		public Rectangle HitboxRectangle
		{
			get
			{
				//IL_0001: Unknown result type (might be due to invalid IL or missing references)
				//IL_000d: Unknown result type (might be due to invalid IL or missing references)
				//IL_0019: Unknown result type (might be due to invalid IL or missing references)
				//IL_0025: Unknown result type (might be due to invalid IL or missing references)
				//IL_0030: Unknown result type (might be due to invalid IL or missing references)
				return new Rectangle((int)TopLeft.X, (int)TopLeft.Y, (int)Size.X, (int)Size.Y);
			}
		}

		public bool PullPlayerWhenSizeChanged => false;

		public void DrawBoxWithOffset(float Offset, float Thickness, Color color)
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0022: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			//IL_003c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_0073: Unknown result type (might be due to invalid IL or missing references)
			//IL_0078: Unknown result type (might be due to invalid IL or missing references)
			//IL_007d: Unknown result type (might be due to invalid IL or missing references)
			//IL_008a: Unknown result type (might be due to invalid IL or missing references)
			//IL_009b: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
			//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
			//IL_0101: Unknown result type (might be due to invalid IL or missing references)
			Main.spriteBatch.DrawLineBetter(TopLeft + new Vector2(0f - (Offset + Thickness * 0.5f), 0f - Offset), TopRight + new Vector2(Offset + Thickness * 0.5f, 0f - Offset), color, Thickness);
			Main.spriteBatch.DrawLineBetter(BottomLeft + new Vector2(0f - (Offset + Thickness * 0.5f), Offset), BottomRight + new Vector2(Offset + Thickness * 0.5f, Offset), color, Thickness);
			Main.spriteBatch.DrawLineBetter(TopLeft + new Vector2(0f - Offset, 0f - (Offset - Thickness * 0.5f)), BottomLeft + new Vector2(0f - Offset, Offset - Thickness * 0.5f), color, Thickness);
			Main.spriteBatch.DrawLineBetter(BottomRight + new Vector2(Offset, Offset - Thickness * 0.5f), TopRight + new Vector2(Offset, 0f - (Offset - Thickness * 0.5f)), color, Thickness);
		}

		public void SetOldData()
		{
			//IL_005c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0061: Unknown result type (might be due to invalid IL or missing references)
			//IL_006d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0072: Unknown result type (might be due to invalid IL or missing references)
			//IL_0010: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0039: Unknown result type (might be due to invalid IL or missing references)
			if (oldData == null)
			{
				oldData = new Box
				{
					borderColor = borderColor,
					borderThickness = borderThickness,
					boxDimensions = boxDimensions,
					position = position
				};
			}
			else
			{
				oldData.borderThickness = borderThickness;
				oldData.boxDimensions = boxDimensions;
				oldData.position = position;
			}
		}

		public bool Contains(Vector2 Position, Vector2 size)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0032: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			return Collision.CheckAABBvAABBCollision(TopLeft - new Vector2(borderThickness * 0.5f), Size + new Vector2(borderThickness), Position, size);
		}

		public bool ShouldEffectPlayer(Player player)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_001d: Unknown result type (might be due to invalid IL or missing references)
			//IL_002e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0043: Unknown result type (might be due to invalid IL or missing references)
			//IL_0049: Unknown result type (might be due to invalid IL or missing references)
			return Collision.CheckAABBvAABBCollision(TopLeft - new Vector2(borderThickness + 300f), Size + new Vector2(borderThickness + 300f) * 2f, player.TopLeft, player.Size);
		}

		public bool InnerEffect(Vector2 Position, Vector2 size)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_001b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0021: Unknown result type (might be due to invalid IL or missing references)
			//IL_002c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0037: Unknown result type (might be due to invalid IL or missing references)
			return Collision.CheckAABBvAABBCollision(TopLeft - new Vector2(borderThickness) * 0.5f, Size + new Vector2(borderThickness), Position, size);
		}

		public bool PointInWall(Vector2 pos)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0015: Unknown result type (might be due to invalid IL or missing references)
			//IL_0020: Unknown result type (might be due to invalid IL or missing references)
			//IL_0025: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0040: Unknown result type (might be due to invalid IL or missing references)
			//IL_0045: Unknown result type (might be due to invalid IL or missing references)
			//IL_004a: Unknown result type (might be due to invalid IL or missing references)
			if (!Vector2PointCollision(TopLeft, Size, pos))
			{
				return Vector2PointCollision(TopLeft - new Vector2(borderThickness), Size + new Vector2(borderThickness) * 2f, pos);
			}
			return false;
			static bool Vector2PointCollision(Vector2 position, Vector2 size, Vector2 point)
			{
				//IL_0000: Unknown result type (might be due to invalid IL or missing references)
				//IL_0006: Unknown result type (might be due to invalid IL or missing references)
				//IL_000e: Unknown result type (might be due to invalid IL or missing references)
				//IL_0014: Unknown result type (might be due to invalid IL or missing references)
				//IL_001a: Unknown result type (might be due to invalid IL or missing references)
				//IL_0023: Unknown result type (might be due to invalid IL or missing references)
				//IL_0029: Unknown result type (might be due to invalid IL or missing references)
				//IL_0031: Unknown result type (might be due to invalid IL or missing references)
				//IL_0037: Unknown result type (might be due to invalid IL or missing references)
				//IL_003d: Unknown result type (might be due to invalid IL or missing references)
				if (point.X >= position.X && point.X <= position.X + size.X && point.Y >= position.Y)
				{
					return point.Y <= position.Y + size.Y;
				}
				return false;
			}
		}

		public bool Vector2PairInWall(Vector2 pos, Vector2 size)
		{
			//IL_0001: Unknown result type (might be due to invalid IL or missing references)
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0011: Unknown result type (might be due to invalid IL or missing references)
			//IL_0016: Unknown result type (might be due to invalid IL or missing references)
			//IL_002f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0034: Unknown result type (might be due to invalid IL or missing references)
			//IL_0035: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			//IL_0062: Unknown result type (might be due to invalid IL or missing references)
			//IL_0075: Unknown result type (might be due to invalid IL or missing references)
			//IL_007a: Unknown result type (might be due to invalid IL or missing references)
			//IL_007b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0084: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009a: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			//IL_00be: Unknown result type (might be due to invalid IL or missing references)
			//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
			//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
			//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
			//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
			if (!Collision.CheckAABBvAABBCollision(TopLeft - new Vector2(borderThickness), new Vector2(size.X + borderThickness * 2f, borderThickness), pos, size) && !Collision.CheckAABBvAABBCollision(TopRight + new Vector2(0f, 0f - borderThickness), new Vector2(borderThickness, size.Y + borderThickness * 2f), pos, size) && !Collision.CheckAABBvAABBCollision(BottomLeft + new Vector2(0f - borderThickness, 0f), new Vector2(size.X + borderThickness * 2f, borderThickness), pos, size))
			{
				return Collision.CheckAABBvAABBCollision(TopLeft - new Vector2(borderThickness), new Vector2(borderThickness, size.Y + borderThickness * 2f), pos, size);
			}
			return true;
		}

		public Box()
		{
			//IL_0026: Unknown result type (might be due to invalid IL or missing references)
			//IL_002b: Unknown result type (might be due to invalid IL or missing references)
			RemovalCondition = () => false;
			borderColor = Color.Red;
			DespawnAction = (Box box) => true;
			base._002Ector();
		}
	}

	public static List<Box> ActiveBoxes = new List<Box>();

	public override void PostDrawTiles()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, (Effect)null, Main.Transform);
		foreach (Box box in ActiveBoxes)
		{
			if (box.DrawBox != null)
			{
				box.DrawBox(box);
				continue;
			}
			_ = Color.Black * 0.75f;
			box.DrawBoxWithOffset(box.borderThickness * 0.5f, box.borderThickness, Color.Black * 0.75f);
			box.DrawBoxWithOffset(4f, 8f, box.borderColor);
			box.DrawBoxWithOffset(box.borderThickness - 4f, 4f, box.borderColor);
		}
		Main.spriteBatch.End();
	}

	public override void PreUpdateEntities()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < ActiveBoxes.Count; i++)
		{
			Box box = ActiveBoxes[i];
			if (box.RemovalCondition())
			{
				if (box.DespawnAction(box))
				{
					ActiveBoxes.Remove(box);
					i--;
				}
				continue;
			}
			box.SetOldData();
			if (box.NewDimensions != Vector4.Zero)
			{
				box.boxDimensions = box.NewDimensions;
			}
			if (box.NewPosition != Vector2.Zero)
			{
				box.position = box.NewPosition;
			}
			if (box.UpdateBox != null)
			{
				box.UpdateBox(box);
			}
		}
	}

	public override void OnWorldUnload()
	{
		ActiveBoxes = new List<Box>();
	}
}
