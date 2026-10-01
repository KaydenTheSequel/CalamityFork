using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using CalamityMod.Balancing;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using CalamityMod.Cooldowns;
using CalamityMod.DataStructures;
using CalamityMod.Enums;
using CalamityMod.Events;
using CalamityMod.Items;
using CalamityMod.Items.Accessories;
using CalamityMod.Items.Armor;
using CalamityMod.Items.Armor.GodSlayer;
using CalamityMod.Items.BaseItems;
using CalamityMod.Items.Pets;
using CalamityMod.NPCs;
using CalamityMod.NPCs.NormalNPCs;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.NPCs.Providence;
using CalamityMod.NPCs.SlimeGod;
using CalamityMod.NPCs.SunkenSea;
using CalamityMod.NPCs.VanillaNPCAIOverrides;
using CalamityMod.NPCs.Yharon;
using CalamityMod.Packets;
using CalamityMod.Projectiles;
using CalamityMod.Projectiles.Magic;
using CalamityMod.Projectiles.Ranged;
using CalamityMod.Projectiles.Typeless;
using CalamityMod.Schematics;
using CalamityMod.Systems;
using CalamityMod.Systems.Collections;
using CalamityMod.Tiles;
using CalamityMod.Tiles.Abyss;
using CalamityMod.Tiles.Astral;
using CalamityMod.Tiles.AstralDesert;
using CalamityMod.Tiles.AstralSnow;
using CalamityMod.Tiles.Crags;
using CalamityMod.Tiles.FurnitureAbyss;
using CalamityMod.Tiles.FurnitureAshen;
using CalamityMod.Tiles.FurnitureDriftwood;
using CalamityMod.Tiles.FurnitureMonolith;
using CalamityMod.Tiles.FurnitureNavystone;
using CalamityMod.Tiles.FurnitureNavystone.FurnitureAncientNavystone;
using CalamityMod.Tiles.FurnitureOtherworldly;
using CalamityMod.Tiles.FurnitureProfaned;
using CalamityMod.Tiles.FurnitureVoid;
using CalamityMod.Tiles.FurnitureWulfrum;
using CalamityMod.Tiles.Ores;
using CalamityMod.Tiles.SunkenSea;
using CalamityMod.UI.CalamitasEnchants;
using CalamityMod.Utilities.Daybreak;
using CalamityMod.Utilities.Daybreak.Buffers;
using CalamityMod.World;
using log4net;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using ReLogic.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.Chat;
using Terraria.DataStructures;
using Terraria.GameContent;
using Terraria.GameContent.Drawing;
using Terraria.GameContent.Events;
using Terraria.GameInput;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.UI.Chat;
using Terraria.Utilities;
using Terraria.WorldBuilding;

namespace CalamityMod;

public static class CalamityUtils
{
	public delegate void ChromaAberrationDelegate(Vector2 offset, Color colorMult);

	public delegate float EasingFunction(float amount, int degree);

	public enum EasingType
	{
		Linear,
		SineIn,
		SineOut,
		SineInOut,
		SineBump,
		PolyIn,
		PolyOut,
		PolyInOut,
		ExpIn,
		ExpOut,
		ExpInOut,
		CircIn,
		CircOut,
		CircInOut
	}

	public struct CurveSegment
	{
		public EasingFunction easing;

		public float startingX;

		public float startingHeight;

		public float elevationShift;

		public int degree;

		public float EndingHeight => startingHeight + elevationShift;

		public CurveSegment(EasingType MODE, float startX, float startHeight, float elevationShift, int degree = 1)
			: this(EasingTypeToFunction[(int)MODE], startX, startHeight, elevationShift, degree)
		{
		}

		public CurveSegment(EasingFunction MODE, float startX, float startHeight, float elevationShift, int degree = 1)
		{
			easing = MODE;
			startingX = startX;
			startingHeight = startHeight;
			this.elevationShift = elevationShift;
			this.degree = degree;
		}
	}

	public struct RocketBehaviorInfo(int rocketID)
	{
		internal int rocketItemType = rocketID;

		public int smallRadius = 3;

		public int mediumRadius = 6;

		public int bigRadius = 7;

		public int largeRadius = 9;

		public bool respectStandardBlastImmunity = true;

		public List<int> tilesToCheck = null;

		public List<int> wallsToCheck = null;

		public int clusterProjectileID = 779;

		public int destructiveClusterProjectileID = 783;

		public float clusterSplitDamageMultiplier = 0.5f;
	}

	public static readonly Color[] ExoPalette;

	private static Asset<Texture2D> ItemDotTexture;

	internal static readonly Color DevItemColor;

	internal static readonly Color DonatorItemColor;

	private static readonly Dictionary<int, List<(Color, float)>> debuffColorWeightsCache;

	private const float WorldInsertionOffset = 15f;

	internal static readonly List<Vector2> Directions;

	private static readonly EasingFunction[] EasingTypeToFunction;

	private static readonly List<int> vanillaBlastImmuneTiles;

	internal static readonly string AlphanumericCharacters;

	public const int DefaultGenerationLoopLimit = 5000;

	internal static Texture2D AuroraTexture
	{
		get
		{
			Main.instance.LoadProjectile(874);
			return TextureAssets.Projectile[874].Value;
		}
	}

	public static Matrix BackgroundMatrix
	{
		get
		{
			//IL_0005: Unknown result type (might be due to invalid IL or missing references)
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			//IL_0017: Unknown result type (might be due to invalid IL or missing references)
			//IL_0047: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_0059: Unknown result type (might be due to invalid IL or missing references)
			//IL_005e: Unknown result type (might be due to invalid IL or missing references)
			//IL_005f: Unknown result type (might be due to invalid IL or missing references)
			//IL_0064: Unknown result type (might be due to invalid IL or missing references)
			//IL_006e: Unknown result type (might be due to invalid IL or missing references)
			Matrix backgroundMatrix = Main.BackgroundViewMatrix.TransformationMatrix;
			Vector3 translationDirection = default(Vector3);
			((Vector3)(ref translationDirection))._002Ector(1f, ((Enum)Main.BackgroundViewMatrix.Effects).HasFlag((Enum)(object)(SpriteEffects)2) ? (-1f) : 1f, 1f);
			Vector3 translation = ((Matrix)(ref backgroundMatrix)).Translation;
			Matrix zoomMatrix = Main.BackgroundViewMatrix.ZoomMatrix;
			((Matrix)(ref backgroundMatrix)).Translation = translation - ((Matrix)(ref zoomMatrix)).Translation * translationDirection;
			return backgroundMatrix;
		}
	}

	public static Vector2 TileDrawOffset
	{
		get
		{
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			if (!Main.drawToScreen)
			{
				return new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange);
			}
			return Vector2.Zero;
		}
	}

	public static Color FireDebuffColor
	{
		get
		{
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			return new Color(253, 107, 2);
		}
	}

	public static Color SicknessDebuffColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(136, 198, 10);
		}
	}

	public static Color WaterDebuffColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(105, 147, 255);
		}
	}

	public static Color ColdDebuffColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(159, 230, 252);
		}
	}

	public static Color ElectricDebuffColor
	{
		get
		{
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			return new Color(255, 245, 0);
		}
	}

	public static Color BuffColor
	{
		get
		{
			//IL_000c: Unknown result type (might be due to invalid IL or missing references)
			return new Color(255, 105, 237);
		}
	}

	public static Color TypelessDebuffColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(230, 202, 250);
		}
	}

	public static Color VulnHexDebuffColor
	{
		get
		{
			//IL_0009: Unknown result type (might be due to invalid IL or missing references)
			return new Color(196, 35, 43);
		}
	}

	public static Color MiracleBlightDebuffColor
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			return Main.DiscoColor;
		}
	}

	private static int _minimumPickPower => ContentSamples.ItemsByType[3509].pick;

	public static Rectangle MouseHitbox
	{
		get
		{
			//IL_0000: Unknown result type (might be due to invalid IL or missing references)
			//IL_000b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Unknown result type (might be due to invalid IL or missing references)
			return new Rectangle((int)Main.MouseWorld.X, (int)Main.MouseWorld.Y, 2, 2);
		}
	}

	public static bool RotatingHitboxCollision(this Entity entity, Vector2 targetTopLeft, Vector2 targetHitboxDimensions, Vector2? directionOverride = null, float scale = 1f)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		Vector2 lineDirection = (Vector2)(((_003F?)directionOverride) ?? entity.velocity);
		lineDirection = lineDirection.SafeNormalize(Vector2.UnitY);
		Vector2 start = entity.Center - lineDirection * (float)entity.height * 0.5f * scale;
		Vector2 end = entity.Center + lineDirection * (float)entity.height * 0.5f * scale;
		float _ = 0f;
		return Collision.CheckAABBvLineCollision(targetTopLeft, targetHitboxDimensions, start, end, (float)entity.width * scale, ref _);
	}

	public static bool RotatingHitboxCollision(this Projectile proj, Rectangle targetHitbox)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return proj.RotatingHitboxCollision(targetHitbox.TopLeft(), targetHitbox.Size(), (proj.rotation + (float)Math.PI / 2f).ToRotationVector2(), proj.scale);
	}

	public static bool CircularHitboxCollision(Vector2 centerCheckPosition, float radius, Rectangle targetHitbox)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (radius <= 0f)
		{
			return false;
		}
		float closestX = MathHelper.Clamp(centerCheckPosition.X, (float)((Rectangle)(ref targetHitbox)).Left, (float)((Rectangle)(ref targetHitbox)).Right);
		float closestY = MathHelper.Clamp(centerCheckPosition.Y, (float)((Rectangle)(ref targetHitbox)).Top, (float)((Rectangle)(ref targetHitbox)).Bottom);
		float num = centerCheckPosition.X - closestX;
		float dy = centerCheckPosition.Y - closestY;
		return num * num + dy * dy <= radius * radius;
	}

	public static float? DistanceToTileCollisionHit(Vector2 startingPoint, Vector2 checkDirection, int giveUpLimit = 500)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		checkDirection = checkDirection.SafeNormalize(Vector2.Zero);
		for (int i = 1; i < giveUpLimit; i++)
		{
			Point checkPosition = startingPoint.ToTileCoordinates();
			checkPosition.X += (int)(checkDirection.X * (float)i);
			checkPosition.Y += (int)(checkDirection.Y * (float)i);
			if (!WorldGen.InWorld(checkPosition.X, checkPosition.Y, 2))
			{
				return null;
			}
			Tile tile = ParanoidTileRetrieval(checkPosition.X, checkPosition.Y);
			if (WorldGen.SolidTile(tile) || (checkDirection.Y >= 0f && tile.HasTile && Main.tileSolidTop[tile.TileType]))
			{
				return i;
			}
		}
		return null;
	}

	public static float PreciseDistanceToTileCollisionHit(Vector2 start, float rotation, float length, float step = 1f)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		Vector2 unitVect = rotation.ToRotationVector2();
		Vector2 end = unitVect * length;
		if (length < 1f)
		{
			Point endWorldPos = end.ToTileCoordinates();
			if (!ParanoidTileRetrieval(endWorldPos.X, endWorldPos.Y).IsTileSolid())
			{
				return length;
			}
			return 0f;
		}
		Vector2 currentPos = start;
		Point lastAirPos = default(Point);
		((Point)(ref lastAirPos))._002Ector(-1, -1);
		Vector2 tileWorldPos = default(Vector2);
		for (float i = 0f; i < length; i += step)
		{
			currentPos += unitVect * step;
			Point tilePos = currentPos.ToTileCoordinates();
			if (tilePos == lastAirPos || !WorldGen.InWorld(tilePos.X, tilePos.Y))
			{
				continue;
			}
			Tile tile = Main.tile[tilePos.X, tilePos.Y];
			if (!tile.IsTileSolid())
			{
				lastAirPos = tilePos;
				continue;
			}
			Vector2 val;
			if (tile.Slope == SlopeType.Solid && !tile.IsHalfBlock)
			{
				val = currentPos - start;
				return ((Vector2)(ref val)).Length();
			}
			((Vector2)(ref tileWorldPos))._002Ector((float)(tilePos.X * 16), (float)(tilePos.Y * 16));
			Vector2 currentPosInTile = currentPos - tileWorldPos;
			if (tile.IsHalfBlock)
			{
				if (currentPosInTile.Y >= 8f)
				{
					val = currentPos - start;
					return ((Vector2)(ref val)).Length();
				}
			}
			else if (tile.Slope == SlopeType.SlopeDownLeft)
			{
				if (currentPosInTile.X <= currentPosInTile.Y)
				{
					val = currentPos - start;
					return ((Vector2)(ref val)).Length();
				}
			}
			else if (tile.Slope == SlopeType.SlopeDownRight)
			{
				if (16f - currentPosInTile.X <= currentPosInTile.Y)
				{
					val = currentPos - start;
					return ((Vector2)(ref val)).Length();
				}
			}
			else if (tile.Slope == SlopeType.SlopeUpLeft)
			{
				if (currentPosInTile.X <= 16f - currentPosInTile.Y)
				{
					val = currentPos - start;
					return ((Vector2)(ref val)).Length();
				}
			}
			else if (tile.Slope == SlopeType.SlopeUpRight && currentPosInTile.X >= currentPosInTile.Y)
			{
				val = currentPos - start;
				return ((Vector2)(ref val)).Length();
			}
		}
		return length;
	}

	public static bool PreciseCanHitInLine(Vector2 start, float rotation, float length, float step = 1f)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		Vector2 unitVect = rotation.ToRotationVector2();
		Vector2 end = unitVect * length;
		if (length < 1f)
		{
			Point endWorldPos = end.ToTileCoordinates();
			return !ParanoidTileRetrieval(endWorldPos.X, endWorldPos.Y).IsTileSolid();
		}
		Vector2 currentPos = start;
		Point lastAirPos = default(Point);
		((Point)(ref lastAirPos))._002Ector(-1, -1);
		Vector2 tileWorldPos = default(Vector2);
		for (float i = 0f; i < length; i += step)
		{
			currentPos += unitVect * step;
			Point tilePos = currentPos.ToTileCoordinates();
			if (tilePos == lastAirPos || !WorldGen.InWorld(tilePos.X, tilePos.Y))
			{
				continue;
			}
			Tile tile = Main.tile[tilePos.X, tilePos.Y];
			if (!tile.IsTileSolid())
			{
				lastAirPos = tilePos;
				continue;
			}
			if (tile.Slope == SlopeType.Solid && !tile.IsHalfBlock)
			{
				return false;
			}
			((Vector2)(ref tileWorldPos))._002Ector((float)(tilePos.X * 16), (float)(tilePos.Y * 16));
			Vector2 currentPosInTile = currentPos - tileWorldPos;
			if (tile.IsHalfBlock)
			{
				if (currentPosInTile.Y >= 8f)
				{
					return false;
				}
			}
			else if (tile.Slope == SlopeType.SlopeDownLeft)
			{
				if (currentPosInTile.X <= currentPosInTile.Y)
				{
					return false;
				}
			}
			else if (tile.Slope == SlopeType.SlopeDownRight)
			{
				if (16f - currentPosInTile.X <= currentPosInTile.Y)
				{
					return false;
				}
			}
			else if (tile.Slope == SlopeType.SlopeUpLeft)
			{
				if (currentPosInTile.X <= 16f - currentPosInTile.Y)
				{
					return false;
				}
			}
			else if (tile.Slope == SlopeType.SlopeUpRight && currentPosInTile.X >= currentPosInTile.Y)
			{
				return false;
			}
		}
		return true;
	}

	public static Asset<Texture2D> GetTextureEfficient(ref Asset<Texture2D> textureAsset, string texture)
	{
		if (textureAsset == null)
		{
			textureAsset = ModContent.Request<Texture2D>(texture, (AssetRequestMode)2);
		}
		return textureAsset;
	}

	public static void DrawAfterimagesCentered(Projectile proj, int mode, Color lightColor, int typeOneIncrement = 1, Texture2D texture = null, bool drawCentered = true, bool shrink = false, int armorShaderToUse = 0)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0406: Unknown result type (might be due to invalid IL or missing references)
		//IL_0413: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0428: Unknown result type (might be due to invalid IL or missing references)
		//IL_042d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0432: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_043a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_0468: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0472: Unknown result type (might be due to invalid IL or missing references)
		//IL_0473: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0212: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0254: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_0267: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_0294: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_0240: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0302: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_030c: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0321: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		//IL_034f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0351: Unknown result type (might be due to invalid IL or missing references)
		//IL_0358: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_0374: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b2: Unknown result type (might be due to invalid IL or missing references)
		if (texture == null)
		{
			texture = TextureAssets.Projectile[proj.type].Value;
		}
		int frameHeight = texture.Height / Main.projFrames[proj.type];
		int frameY = frameHeight * proj.frame;
		float scale = proj.scale;
		float rotation = proj.rotation;
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(0, frameY, texture.Width, frameHeight);
		Vector2 origin = rectangle.Size() / 2f;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (proj.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		bool failedToDrawAfterimages = false;
		if (CalamityClientConfig.Instance.Afterimages)
		{
			Vector2 centerOffset = (drawCentered ? (proj.Size / 2f) : Vector2.Zero);
			Color alphaColor = proj.GetAlpha(lightColor);
			switch (mode)
			{
			case 0:
			{
				for (int j = 0; j < proj.oldPos.Length; j++)
				{
					Vector2 drawPos2 = proj.oldPos[j] + centerOffset - Main.screenPosition + new Vector2(0f, proj.gfxOffY);
					float interpolant2 = (float)(proj.oldPos.Length - j) / (float)proj.oldPos.Length;
					Color color2 = alphaColor * interpolant2;
					DrawData drawData = new DrawData(texture, drawPos2, rectangle, color2);
					drawData.rotation = rotation;
					drawData.origin = origin;
					drawData.effect = spriteEffects;
					DrawData drawData3 = drawData;
					GameShaders.Armor.Apply(armorShaderToUse, proj, drawData3);
					Main.spriteBatch.Draw(texture, drawPos2, (Rectangle?)rectangle, color2, rotation, origin, shrink ? (scale * interpolant2) : scale, spriteEffects, 0f);
				}
				break;
			}
			case 1:
			{
				int increment = Math.Max(1, typeOneIncrement);
				Color drawColor = alphaColor;
				int afterimageCount = ProjectileID.Sets.TrailCacheLength[proj.type];
				float afterimageColorCount = (float)afterimageCount * 1.5f;
				for (int k = 0; k < afterimageCount; k += increment)
				{
					Vector2 drawPos3 = proj.oldPos[k] + centerOffset - Main.screenPosition + new Vector2(0f, proj.gfxOffY);
					float interpolant3 = (float)(proj.oldPos.Length - k) / (float)proj.oldPos.Length;
					if (k > 0)
					{
						float colorMult = afterimageCount - k;
						drawColor *= colorMult / afterimageColorCount;
					}
					DrawData drawData = new DrawData(texture, drawPos3, rectangle, drawColor);
					drawData.rotation = rotation;
					drawData.origin = origin;
					drawData.effect = spriteEffects;
					DrawData drawData4 = drawData;
					GameShaders.Armor.Apply(armorShaderToUse, proj, drawData4);
					Main.spriteBatch.Draw(texture, drawPos3, (Rectangle?)rectangle, drawColor, rotation, origin, shrink ? (scale * interpolant3) : scale, spriteEffects, 0f);
				}
				break;
			}
			case 2:
			{
				for (int i = 0; i < proj.oldPos.Length; i++)
				{
					float afterimageRot = proj.oldRot[i];
					SpriteEffects sfxForThisAfterimage = (SpriteEffects)(proj.oldSpriteDirection[i] == -1);
					Vector2 drawPos = proj.oldPos[i] + centerOffset - Main.screenPosition + new Vector2(0f, proj.gfxOffY);
					float interpolant = (float)(proj.oldPos.Length - i) / (float)proj.oldPos.Length;
					Color color = alphaColor * interpolant;
					DrawData drawData = new DrawData(texture, drawPos, rectangle, color);
					drawData.rotation = rotation;
					drawData.origin = origin;
					drawData.effect = spriteEffects;
					DrawData drawData2 = drawData;
					GameShaders.Armor.Apply(armorShaderToUse, proj, drawData2);
					Main.spriteBatch.Draw(texture, drawPos, (Rectangle?)rectangle, color, afterimageRot, origin, shrink ? (scale * interpolant) : scale, sfxForThisAfterimage, 0f);
				}
				break;
			}
			default:
				failedToDrawAfterimages = true;
				break;
			}
		}
		if ((!CalamityClientConfig.Instance.Afterimages || ProjectileID.Sets.TrailCacheLength[proj.type] <= 0) | failedToDrawAfterimages)
		{
			Vector2 drawPos4 = (drawCentered ? proj.Center : proj.position) - Main.screenPosition + new Vector2(0f, proj.gfxOffY);
			DrawData drawData5 = new DrawData(texture, drawPos4, rectangle, proj.GetAlpha(lightColor));
			GameShaders.Armor.Apply(armorShaderToUse, proj, drawData5);
			Main.spriteBatch.Draw(texture, drawPos4, (Rectangle?)rectangle, proj.GetAlpha(lightColor), rotation, origin, scale, spriteEffects, 0f);
		}
	}

	public static void DrawAfterimagesFromEdge(Projectile proj, int mode, Color lightColor, Texture2D texture = null)
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		if (texture == null)
		{
			texture = TextureAssets.Projectile[proj.type].Value;
		}
		int frameHeight = texture.Height / Main.projFrames[proj.type];
		int frameY = frameHeight * proj.frame;
		float scale = proj.scale;
		float rotation = proj.rotation;
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(0, frameY, texture.Width, frameHeight);
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (proj.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Vector2 drawOrigin = default(Vector2);
		((Vector2)(ref drawOrigin))._002Ector((float)texture.Width * 0.5f, (float)proj.height * 0.5f);
		switch (mode)
		{
		case 0:
		{
			for (int j = 0; j < proj.oldPos.Length; j++)
			{
				Vector2 drawPos2 = proj.oldPos[j] + drawOrigin - Main.screenPosition + new Vector2(0f, proj.gfxOffY);
				Color color2 = proj.GetAlpha(lightColor) * ((float)(proj.oldPos.Length - j) / (float)proj.oldPos.Length);
				Main.spriteBatch.Draw(texture, drawPos2, (Rectangle?)rectangle, color2, rotation, drawOrigin, scale, spriteEffects, 0f);
			}
			break;
		}
		case 2:
		{
			for (int i = 0; i < proj.oldPos.Length; i++)
			{
				float afterimageRot = proj.oldRot[i];
				SpriteEffects sfxForThisAfterimage = (SpriteEffects)(proj.oldSpriteDirection[i] == -1);
				Vector2 drawPos = proj.oldPos[i] + drawOrigin - Main.screenPosition + new Vector2(0f, proj.gfxOffY);
				Color color = proj.GetAlpha(lightColor) * ((float)(proj.oldPos.Length - i) / (float)proj.oldPos.Length);
				Main.spriteBatch.Draw(texture, drawPos, (Rectangle?)rectangle, color, afterimageRot, drawOrigin, scale, sfxForThisAfterimage, 0f);
			}
			break;
		}
		}
	}

	public static void DrawInventoryCustomScale(SpriteBatch spriteBatch, Texture2D texture, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale, float wantedScale = 1f, Vector2 drawOffset = default(Vector2), SpriteEffects spriteEffects = (SpriteEffects)0, float rotation = 0f)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		wantedScale = Math.Max(scale, wantedScale * Main.inventoryScale);
		position += drawOffset * wantedScale;
		if (itemColor == Color.Transparent)
		{
			itemColor = Color.White;
		}
		spriteBatch.Draw(texture, position, (Rectangle?)frame, itemColor.MultiplyRGB(drawColor), 0f, origin, wantedScale, (SpriteEffects)0, 0f);
	}

	public static void DrawInventoryDot(SpriteBatch spriteBatch, Vector2 itemPosition, Vector2 dotOffset, bool enabled)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = GetTextureEfficient(ref ItemDotTexture, "Terraria/Images/Extra_20").Value;
		Rectangle dotFrame = tex.Frame(1, 4, 0, enabled ? 1 : 2);
		spriteBatch.Draw(tex, itemPosition + dotOffset, (Rectangle?)dotFrame, Color.White, 0f, dotFrame.Size() * 0.5f, Main.inventoryScale, (SpriteEffects)0, 0f);
	}

	public static bool DrawTreasureBagInWorld(Item item, SpriteBatch spriteBatch, ref float rotation, ref float scale, int whoAmI)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cd: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Item[item.type].Value;
		Rectangle frame = texture.Frame();
		if (Main.itemAnimations[item.type] != null)
		{
			frame = Main.itemAnimations[item.type].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
		}
		Vector2 frameOrigin = frame.Size() * 0.5f;
		Vector2 offset = default(Vector2);
		((Vector2)(ref offset))._002Ector((float)(item.width / 2) - frameOrigin.X, (float)(item.height - frame.Height));
		Vector2 drawPos = item.position - Main.screenPosition + frameOrigin + offset;
		float localTime = (float)item.timeSinceItemSpawned / 240f + Main.GlobalTimeWrappedHourly * 0.04f;
		float time = Main.GlobalTimeWrappedHourly % 4f / 2f;
		if (time >= 1f)
		{
			time = 2f - time;
		}
		time = time * 0.5f + 0.5f;
		for (int i = 0; i < 4; i++)
		{
			Vector2 pulseOffset = Vector2.UnitY.RotatedBy(((float)i / 4f + localTime) * ((float)Math.PI * 2f)) * time * 8f;
			spriteBatch.Draw(texture, drawPos + pulseOffset, (Rectangle?)frame, new Color(90, 70, 255, 50), rotation, frameOrigin, scale, (SpriteEffects)0, 0f);
		}
		for (int j = 0; j < 3; j++)
		{
			Vector2 pulseOffset2 = Vector2.UnitY.RotatedBy(((float)j / 3f + localTime) * ((float)Math.PI * 2f)) * time * 4f;
			spriteBatch.Draw(texture, drawPos + pulseOffset2, (Rectangle?)frame, new Color(140, 120, 255, 77), rotation, frameOrigin, scale, (SpriteEffects)0, 0f);
		}
		return true;
	}

	public static void CopyContentsFrom(this RenderTarget2D to, RenderTarget2D from)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		using (to.Scope(preserveContents: true, Color.Transparent))
		{
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Matrix.Identity);
			Main.spriteBatch.Draw((Texture2D)(object)from, Vector2.Zero, (Rectangle?)null, Color.White);
			Main.spriteBatch.End();
		}
		using (from.Scope(preserveContents: true, Color.Transparent))
		{
		}
	}

	public static void CalculatePerspectiveMatricies(out Matrix viewMatrix, out Matrix projectionMatrix)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		Vector2 zoom = Main.GameViewMatrix.Zoom;
		Matrix zoomScaleMatrix = Matrix.CreateScale(zoom.X, zoom.Y, 1f);
		Viewport viewport = ((Game)Main.instance).GraphicsDevice.Viewport;
		int width = ((Viewport)(ref viewport)).Width;
		viewport = ((Game)Main.instance).GraphicsDevice.Viewport;
		int height = ((Viewport)(ref viewport)).Height;
		viewMatrix = Matrix.CreateLookAt(Vector3.Zero, Vector3.UnitZ, Vector3.Up);
		viewMatrix *= Matrix.CreateTranslation(0f, (float)(-height), 0f);
		viewMatrix *= Matrix.CreateRotationZ((float)Math.PI);
		if (Main.LocalPlayer.gravDir == -1f)
		{
			viewMatrix *= Matrix.CreateScale(1f, -1f, 1f) * Matrix.CreateTranslation(0f, (float)height, 0f);
		}
		viewMatrix *= zoomScaleMatrix;
		projectionMatrix = Matrix.CreateOrthographicOffCenter(0f, (float)width * zoom.X, 0f, (float)height * zoom.Y, 0f, 1f) * zoomScaleMatrix;
	}

	public static void DrawLineBetter(this SpriteBatch spriteBatch, Vector2 start, Vector2 end, Color color, float width)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		if (!(start == end))
		{
			start -= Main.screenPosition;
			end -= Main.screenPosition;
			Texture2D line = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/Line", (AssetRequestMode)2).Value;
			float rotation = (end - start).ToRotation();
			Vector2 scale = default(Vector2);
			((Vector2)(ref scale))._002Ector(Vector2.Distance(start, end) / (float)line.Width, width);
			spriteBatch.Draw(line, start, (Rectangle?)null, color, rotation, line.Size() * Vector2.UnitY * 0.5f, scale, (SpriteEffects)0, 0f);
		}
	}

	public static void DrawBorderStringEightWay(SpriteBatch sb, DynamicSpriteFont font, string text, Vector2 baseDrawPosition, Color main, Color border, float scale = 1f)
	{
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		for (int x = -1; x <= 1; x++)
		{
			for (int y = -1; y <= 1; y++)
			{
				Vector2 drawPosition = baseDrawPosition + new Vector2((float)x, (float)y);
				if (x != 0 || y != 0)
				{
					DynamicSpriteFontExtensionMethods.DrawString(sb, font, text, drawPosition, border, 0f, default(Vector2), scale, (SpriteEffects)0, 0f);
				}
			}
		}
		DynamicSpriteFontExtensionMethods.DrawString(sb, font, text, baseDrawPosition, main, 0f, default(Vector2), scale, (SpriteEffects)0, 0f);
	}

	public static void DrawItemGlowmaskSingleFrame(this Item item, SpriteBatch spriteBatch, float rotation, Texture2D glowmaskTexture)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)glowmaskTexture.Width / 2f, (float)glowmaskTexture.Height / 2f);
		Color color = Color.White;
		spriteBatch.Draw(glowmaskTexture, item.Center - Main.screenPosition, (Rectangle?)null, color, rotation, origin, 1f, (SpriteEffects)0, 0f);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Rectangle GetFrame(int itemID, int whoAmI, Texture2D texture = null)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (texture == null)
		{
			texture = TextureAssets.Item[itemID].Value;
		}
		if (Main.itemAnimations[itemID] != null)
		{
			return Main.itemAnimations[itemID].GetFrame(texture, Main.itemFrameCounter[whoAmI]);
		}
		return texture.Frame();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Rectangle GetFrame(this Item item, int whoAmI, Texture2D texture = null)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		return GetFrame(item.type, whoAmI, texture);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Rectangle GetFrame(int itemID, Texture2D texture = null)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		if (texture == null)
		{
			texture = TextureAssets.Item[itemID].Value;
		}
		if (Main.itemAnimations[itemID] != null)
		{
			return Main.itemAnimations[itemID].GetFrame(texture);
		}
		return texture.Frame();
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining)]
	public static Rectangle GetFrame(this Item item, Texture2D texture = null)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return GetFrame(item.type, texture);
	}

	public static Rectangle GetCurrentFrame(this Item item, ref int frame, ref int frameCounter, int frameDelay, int frameAmt, bool frameCounterUp = true)
	{
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		if (frameCounter >= frameDelay)
		{
			frameCounter = -1;
			frame = ((frame != frameAmt - 1) ? (frame + 1) : 0);
		}
		if (frameCounterUp)
		{
			frameCounter++;
		}
		return new Rectangle(0, item.height * frame, item.width, item.height);
	}

	public static bool DrawHook(this Projectile projectile, Texture2D hookTexture, float angleAdditive = 0f)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[projectile.owner];
		Vector2 center = projectile.Center;
		float angleToMountedCenter = projectile.AngleTo(player.MountedCenter) - (float)Math.PI / 2f;
		bool canShowHook = true;
		while (canShowHook)
		{
			Vector2 val = player.MountedCenter - center;
			float distanceMagnitude = ((Vector2)(ref val)).Length();
			if (distanceMagnitude < (float)hookTexture.Height + 1f)
			{
				canShowHook = false;
				continue;
			}
			if (float.IsNaN(distanceMagnitude))
			{
				canShowHook = false;
				continue;
			}
			center += projectile.SafeDirectionTo(player.MountedCenter) * (float)hookTexture.Height;
			Color tileAtCenterColor = Lighting.GetColor((int)center.X / 16, (int)(center.Y / 16f));
			Main.spriteBatch.Draw(hookTexture, center - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, hookTexture.Width, hookTexture.Height), tileAtCenterColor, angleToMountedCenter + angleAdditive, hookTexture.Size() / 2f, 1f, (SpriteEffects)0, 0f);
		}
		return true;
	}

	internal static void IterateDisco(ref Color c, ref float aiParam, in byte discoIter = 7)
	{
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		float num = aiParam;
		if (num != 0f)
		{
			if (num != 1f)
			{
				if (num != 2f)
				{
					if (num != 3f)
					{
						if (num != 4f)
						{
							if (num == 5f)
							{
								((Color)(ref c)).B = (byte)(((Color)(ref c)).B - discoIter);
								if (((Color)(ref c)).B <= 0)
								{
									((Color)(ref c)).B = 0;
									aiParam = 0f;
								}
							}
							else
							{
								aiParam = 0f;
								c = Color.Red;
							}
						}
						else
						{
							((Color)(ref c)).R = (byte)(((Color)(ref c)).R + discoIter);
							if (((Color)(ref c)).R >= byte.MaxValue)
							{
								((Color)(ref c)).R = byte.MaxValue;
								aiParam = 5f;
							}
						}
					}
					else
					{
						((Color)(ref c)).G = (byte)(((Color)(ref c)).G - discoIter);
						if (((Color)(ref c)).G <= 0)
						{
							((Color)(ref c)).G = 0;
							aiParam = 4f;
						}
					}
				}
				else
				{
					((Color)(ref c)).B = (byte)(((Color)(ref c)).B + discoIter);
					if (((Color)(ref c)).B >= byte.MaxValue)
					{
						((Color)(ref c)).B = byte.MaxValue;
						aiParam = 3f;
					}
				}
			}
			else
			{
				((Color)(ref c)).R = (byte)(((Color)(ref c)).R - discoIter);
				if (((Color)(ref c)).R <= 0)
				{
					((Color)(ref c)).R = 0;
					aiParam = 2f;
				}
			}
		}
		else
		{
			((Color)(ref c)).G = (byte)(((Color)(ref c)).G + discoIter);
			if (((Color)(ref c)).G >= byte.MaxValue)
			{
				((Color)(ref c)).G = byte.MaxValue;
				aiParam = 1f;
			}
		}
	}

	public static string ColorMessage(string msg, Color color)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		StringBuilder sb;
		if (!msg.Contains('\n'))
		{
			sb = new StringBuilder(msg.Length + 12);
			sb.Append("[c/").Append(color.Hex3()).Append(':')
				.Append(msg)
				.Append(']');
		}
		else
		{
			sb = new StringBuilder();
			string[] array = msg.Split('\n');
			foreach (string newlineSlice in array)
			{
				sb.Append("[c/").Append(color.Hex3()).Append(':')
					.Append(newlineSlice)
					.Append(']')
					.Append('\n');
			}
		}
		return sb.ToString();
	}

	public static Color ColorSwap(Color firstColor, Color secondColor, float seconds)
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		float colorMePurple = (float)((Math.Sin((double)((float)Math.PI * 2f / seconds) * (double)Main.GlobalTimeWrappedHourly) + 1.0) * 0.5);
		return Color.Lerp(firstColor, secondColor, colorMePurple);
	}

	public static Color MulticolorLerp(float increment, params Color[] colors)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		increment %= 0.999f;
		int currentColorIndex = (int)(increment * (float)colors.Length);
		Color val = colors[currentColorIndex];
		Color nextColor = colors[(currentColorIndex + 1) % colors.Length];
		return Color.Lerp(val, nextColor, increment * (float)colors.Length % 1f);
	}

	[MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
	public static bool IsAnyChannelGreaterThan(this Color a, Color b, bool includeAlpha = false)
	{
		if (((Color)(ref a)).R > ((Color)(ref b)).R)
		{
			return true;
		}
		if (((Color)(ref a)).G > ((Color)(ref b)).G)
		{
			return true;
		}
		if (((Color)(ref a)).B > ((Color)(ref b)).B)
		{
			return true;
		}
		if (includeAlpha && ((Color)(ref a)).A > ((Color)(ref b)).A)
		{
			return true;
		}
		return false;
	}

	public static MiscShaderData SetShaderTexture(this MiscShaderData shader, Asset<Texture2D> texture, int index = 1)
	{
		switch (index)
		{
		case 0:
			shader._uImage0 = texture;
			break;
		case 1:
			shader._uImage1 = texture;
			break;
		}
		return shader;
	}

	public static ArmorShaderData SetShaderTextureArmor(this ArmorShaderData shader, Asset<Texture2D> texture)
	{
		shader._uImage = texture;
		return shader;
	}

	public static void EnterShaderRegion(this SpriteBatch spriteBatch, BlendState newBlendState = null, Effect effect = null, Matrix? matrix = null)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		spriteBatch.End();
		spriteBatch.Begin((SpriteSortMode)1, newBlendState ?? BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, effect, (Matrix)(((_003F?)matrix) ?? Main.GameViewMatrix.TransformationMatrix));
	}

	public static void ExitShaderRegion(this SpriteBatch spriteBatch, Matrix? matrix = null)
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		spriteBatch.End();
		spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, (Matrix)(((_003F?)matrix) ?? Main.GameViewMatrix.TransformationMatrix));
	}

	public static void EnforceCutoffRegion(this SpriteBatch spriteBatch, Rectangle cutoffRegion, Matrix perspective, SpriteSortMode sortMode = (SpriteSortMode)0, BlendState newBlendState = null)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		RasterizerState rasterizer = Main.Rasterizer;
		rasterizer.ScissorTestEnable = true;
		spriteBatch.End();
		spriteBatch.Begin((SpriteSortMode)0, newBlendState ?? BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, rasterizer, (Effect)null, perspective);
		((GraphicsResource)spriteBatch).GraphicsDevice.ScissorRectangle = cutoffRegion;
	}

	public static void ReleaseCutoffRegion(this SpriteBatch spriteBatch, Matrix perspective, SpriteSortMode sortMode = (SpriteSortMode)0)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		Viewport viewport = ((GraphicsResource)spriteBatch).GraphicsDevice.Viewport;
		int width = ((Viewport)(ref viewport)).Width;
		viewport = ((GraphicsResource)spriteBatch).GraphicsDevice.Viewport;
		int height = ((Viewport)(ref viewport)).Height;
		spriteBatch.End();
		spriteBatch.Begin(sortMode, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, perspective);
		((GraphicsResource)spriteBatch).GraphicsDevice.ScissorRectangle = new Rectangle(-1, -1, width + 2, height + 2);
	}

	public static IEnumerable<DrawData> DrawAuroras(Player player, float auroraCount, float opacity, Color color)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		float time = Main.GlobalTimeWrappedHourly % 3f / 3f;
		Texture2D auroraTexture = AuroraTexture;
		Vector2 offset = default(Vector2);
		for (int i = 0; (float)i < auroraCount; i++)
		{
			float incrementOffsetAngle = (float)Math.PI * 2f * (float)i / auroraCount;
			float xOffset = (float)Math.Sin(time * ((float)Math.PI * 2f) + incrementOffsetAngle * 2f) * 20f;
			float yOffset = (float)Math.Sin(time * ((float)Math.PI * 2f) + incrementOffsetAngle * 2f + MathHelper.ToRadians(60f)) * 6f;
			float rotation = (float)Math.Sin(incrementOffsetAngle) * (float)Math.PI / 12f;
			((Vector2)(ref offset))._002Ector(xOffset, yOffset - 14f);
			yield return new DrawData(auroraTexture, player.Top + offset - Main.screenPosition, null, color * opacity, rotation + (float)Math.PI / 2f, auroraTexture.Size() * 0.5f, 0.135f, (SpriteEffects)0, 1f);
		}
	}

	public static void DrawNewInventorySprite(this SpriteBatch spriteBatch, Texture2D newTexture, Vector2 originalSize, Vector2 position, Color drawColor, Vector2 origin, float scale, Vector2? offset = null)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		Vector2 extraOffset = (Vector2)(((_003F?)offset) ?? Vector2.Zero);
		float num = Math.Max(originalSize.X, originalSize.Y);
		float largestDimensionNew = Math.Max(newTexture.Width, newTexture.Height);
		float scaleRatio = Math.Min(num / largestDimensionNew, 1f);
		Vector2 positionOffset = Vector2.Zero;
		if (originalSize.X > (float)newTexture.Width)
		{
			positionOffset.X = (originalSize.X - (float)newTexture.Width) / 2f;
		}
		positionOffset *= scale;
		spriteBatch.Draw(newTexture, position + positionOffset + extraOffset, (Rectangle?)null, drawColor, 0f, origin, scale * scaleRatio, (SpriteEffects)0, 0f);
	}

	public static void DrawChromaticAberration(Vector2 direction, float strength, ChromaAberrationDelegate drawCall)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		for (int i = -1; i <= 1; i++)
		{
			Color aberrationColor = Color.White;
			switch (i)
			{
			case -1:
				((Color)(ref aberrationColor))._002Ector(255, 0, 0, 0);
				break;
			case 0:
				((Color)(ref aberrationColor))._002Ector(0, 255, 0, 0);
				break;
			case 1:
				((Color)(ref aberrationColor))._002Ector(0, 0, 255, 0);
				break;
			}
			Vector2 offset = direction.RotatedBy(1.5707963705062866) * (float)i;
			offset *= strength;
			drawCall(offset, aberrationColor);
		}
	}

	[Obsolete("Use RenderTargetScope")]
	public static void SwapTo(this RenderTarget2D target, Color? flushColor = null)
	{
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.gameMenu && !Main.dedServ && target != null && ((Game)Main.instance).GraphicsDevice != null && Main.spriteBatch != null)
		{
			((Game)Main.instance).GraphicsDevice.SetRenderTarget(target);
			((Game)Main.instance).GraphicsDevice.Clear((Color)(((_003F?)flushColor) ?? Color.Transparent));
		}
	}

	public static void DrawLineBetweenPoints(List<Vector2> pointList, Color lineColor, bool useTileColor = false, float scaleMod = 1f)
	{
		//IL_0004: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < pointList.Count - 1; i++)
		{
			Color color = lineColor;
			if (useTileColor)
			{
				color = Lighting.GetColor(pointList[i].ToTileCoordinates(), lineColor);
			}
			Main.spriteBatch.DrawLineBetter(pointList[i], pointList[i + 1], color, scaleMod);
		}
	}

	public static void GetScreenDrawArea(Vector2 unscaledScreenPosition, Vector2 offSet, out int firstTileX, out int lastTileX, out int firstTileY, out int lastTileY)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		firstTileX = (int)((unscaledScreenPosition.X - offSet.X) / 16f - 1f);
		lastTileX = (int)((unscaledScreenPosition.X + (float)Main.screenWidth + offSet.X) / 16f) + 2;
		firstTileY = (int)((unscaledScreenPosition.Y - offSet.Y) / 16f - 1f);
		lastTileY = (int)((unscaledScreenPosition.Y + (float)Main.screenHeight + offSet.Y) / 16f) + 5;
		if (firstTileX < 4)
		{
			firstTileX = 4;
		}
		if (lastTileX > Main.maxTilesX - 4)
		{
			lastTileX = Main.maxTilesX - 4;
		}
		if (firstTileY < 4)
		{
			firstTileY = 4;
		}
		if (lastTileY > Main.maxTilesY - 4)
		{
			lastTileY = Main.maxTilesY - 4;
		}
	}

	public static void SetBlendState(this SpriteBatch spriteBatch, BlendState blendState)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		spriteBatch.End();
		spriteBatch.Begin((SpriteSortMode)1, blendState, Main.DefaultSamplerState, DepthStencilState.None, RasterizerState.CullCounterClockwise, (Effect)null, Main.GameViewMatrix.TransformationMatrix);
	}

	public static bool HasBeginBeenCalled(this SpriteBatch spriteBatch)
	{
		return spriteBatch.beginCalled;
	}

	[Obsolete("Use SpriteBatch.Begin, SpriteBatchScope, or SpriteBatchSnapshot")]
	public static void SafeBegin(this SpriteBatch spriteBatch, SpriteSortMode sortMode, BatchSetting settings, Effect effect, Matrix transformMatrix, Action batchCallback)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if (spriteBatch != null)
		{
			spriteBatch.End(out var ss);
			spriteBatch.Begin(sortMode, settings.blendState, settings.samplerState, settings.depthStencilState, settings.rasterizerState ?? Main.Rasterizer, effect, transformMatrix);
			batchCallback?.Invoke();
			spriteBatch.Restart(in ss);
		}
	}

	[Obsolete("This is violative of spritebatch's control flow and will eventually be removed")]
	public static bool TryBegin(this SpriteBatch spriteBatch, SpriteSortMode sortMode, BlendState blendState, SamplerState samplerState, DepthStencilState depthStencilState, RasterizerState rasterizerState, Effect effect, Matrix transformMatrix)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		if (spriteBatch.HasBeginBeenCalled())
		{
			return false;
		}
		spriteBatch.Begin(sortMode, blendState, samplerState, depthStencilState, rasterizerState, effect, transformMatrix);
		return true;
	}

	[Obsolete("This is violative of spritebatch's control flow and will eventually be removed")]
	public static bool TryEnd(this SpriteBatch spriteBatch)
	{
		if (!spriteBatch.HasBeginBeenCalled())
		{
			return false;
		}
		spriteBatch.End();
		return true;
	}

	public static Vector2 SafeDirectionTo(this Entity entity, Vector2 destination, Vector2? fallback = null)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (!fallback.HasValue)
		{
			fallback = Vector2.Zero;
		}
		return (destination - entity.Center).SafeNormalize(fallback.Value);
	}

	public static void SetScreenshake(this Player player, float value)
	{
		if (player.Calamity().GeneralScreenShakePower < value)
		{
			player.Calamity().GeneralScreenShakePower = value;
		}
	}

	public static void AddScreenshakeAt(Vector2 position, float intensity, float range = 1000f)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		float dist = 1f;
		dist -= position.Distance(Main.LocalPlayer.Center) / range;
		dist = Math.Max(dist, 0f);
		Main.LocalPlayer.GetModPlayer<CalamityPlayer>().GeneralScreenShakePower += intensity * dist;
	}

	public static bool IsNullOrInactive(this Entity entity)
	{
		if (entity == null)
		{
			return true;
		}
		if (!entity.active)
		{
			return true;
		}
		return false;
	}

	public static bool IndexInRange(this NPC[] _, int index)
	{
		return (uint)index < Main.maxNPCs;
	}

	public static bool IndexInRange(this Player[] _, int index)
	{
		return (uint)index < 255u;
	}

	public static bool IndexInRange(this Projectile[] _, int index)
	{
		return (uint)index < Main.maxProjectiles;
	}

	public static bool IndexInRange(this Gore[] _, int index)
	{
		return (uint)index < 600u;
	}

	public static bool IndexInRange(this Dust[] _, int index)
	{
		return (uint)index < 6000u;
	}

	public static CalamityPlayer Calamity(this Player player)
	{
		return player.GetModPlayer<CalamityPlayer>();
	}

	public static CalamityGlobalNPC Calamity(this NPC npc)
	{
		return npc.GetGlobalNPC<CalamityGlobalNPC>();
	}

	public static CalamityVanillaAIOverrideNPC AIOverrideNPC(this NPC npc)
	{
		return npc.GetGlobalNPC<CalamityVanillaAIOverrideNPC>();
	}

	public static T? AIOverride<T>(this NPC npc)
	{
		VanillaAIOverride aiOverride = npc.GetGlobalNPC<CalamityVanillaAIOverrideNPC>().AIOverride;
		if (aiOverride == null)
		{
			throw new Exception("NPC does not have an AI Override");
		}
		if (aiOverride is T)
		{
			return (T)(object)((aiOverride is T) ? aiOverride : null);
		}
		throw new Exception("NPC's AI Override is not of Type " + typeof(T).Name);
	}

	public static CalamityTileCollisionHarmNPC FlungNPC(this NPC npc)
	{
		return npc.GetGlobalNPC<CalamityTileCollisionHarmNPC>();
	}

	public static CalamityPolarityNPC PolarityNPC(this NPC npc)
	{
		return npc.GetGlobalNPC<CalamityPolarityNPC>();
	}

	public static CalamityGlobalItem Calamity(this Item item)
	{
		return item.GetGlobalItem<CalamityGlobalItem>();
	}

	public static CalamityGlobalProjectile Calamity(this Projectile proj)
	{
		return proj.GetGlobalProjectile<CalamityGlobalProjectile>();
	}

	public static TransformationPlayer Transformation(this Player player)
	{
		return player.GetModPlayer<TransformationPlayer>();
	}

	public static bool IsTrueMelee(this Item item)
	{
		if (item == null || item.IsAir || item.accessory)
		{
			return false;
		}
		if (!item.CountsAsClass<TrueMeleeDamageClass>())
		{
			return item.CountsAsClass<TrueMeleeNoSpeedDamageClass>();
		}
		return true;
	}

	public static bool IsWhip(this Item item)
	{
		if (item.shoot > 0)
		{
			return ProjectileID.Sets.IsAWhip[item.shoot];
		}
		return false;
	}

	public static void SetRevExclusive(this Item item)
	{
		item.Calamity().revengeanceItem = true;
	}

	public static string TooltipHotkeyString(this ModKeybind mhk)
	{
		if (Main.dedServ || mhk == null)
		{
			return "";
		}
		List<string> keys = mhk.GetAssignedKeysOrEmpty();
		if (keys.Count == 0)
		{
			return GetText("Misc.HotkeyNotBound").Value;
		}
		StringBuilder sb = new StringBuilder(16);
		sb.Append(keys[0]);
		for (int i = 1; i < keys.Count; i++)
		{
			sb.Append(" / ").Append(keys[i]);
		}
		return sb.ToString();
	}

	public static void FindAndReplace(this List<TooltipLine> tooltips, string replacedKey, string newKey)
	{
		TooltipLine line = tooltips.FirstOrDefault((TooltipLine x) => x.Mod == "Terraria" && x.Text.Contains(replacedKey));
		if (line != null)
		{
			line.Text = line.Text.Replace(replacedKey, newKey);
		}
	}

	public static void FindAndReplaceAll(this List<TooltipLine> tooltips, string replacedKey, string newKey)
	{
		foreach (TooltipLine tooltip in tooltips)
		{
			tooltip.Text = tooltip.Text.Replace(replacedKey, newKey);
		}
	}

	public static void IntegrateHotkey(this List<TooltipLine> tooltips, ModKeybind mhk)
	{
		if (!Main.dedServ && mhk != null)
		{
			string finalKey = mhk.TooltipHotkeyString();
			tooltips.FindAndReplace("[KEY]", finalKey);
		}
	}

	public static void AddConsumedTooltip(this List<TooltipLine> tooltips, string tooltipSearchKey = "Tooltip1")
	{
		TooltipLine line = tooltips.FirstOrDefault((TooltipLine x) => x.Mod == "Terraria" && x.Name == tooltipSearchKey);
		if (line != null)
		{
			line.Text = line.Text + "\n" + GetTextValue("Misc.GenericConsumedText");
		}
	}

	public static Color GetDebuffTooltipNameColor(int debuffId)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		Color color = TypelessDebuffColor;
		if (debuffId == ModContent.BuffType<VulnerabilityHex>() || debuffId == ModContent.BuffType<TrueVulnerabilityHex>())
		{
			color = Color.Lerp(VulnHexDebuffColor, FireDebuffColor, (MathF.Sin(Main.GlobalTimeWrappedHourly * 2f) + 1f) / 4f);
		}
		else if (debuffId == ModContent.BuffType<MiracleBlight>())
		{
			color = MiracleBlightDebuffColor;
		}
		else if (BuffDatasets.DebuffDataset[debuffId] != null)
		{
			if (!debuffColorWeightsCache.TryGetValue(debuffId, out var weights))
			{
				int num = 5;
				List<(Color, float)> list = new List<(Color, float)>(num);
				CollectionsMarshal.SetCount(list, num);
				Span<(Color, float)> span = CollectionsMarshal.AsSpan(list);
				int num2 = 0;
				span[num2] = (SicknessDebuffColor, BuffDatasets.DebuffDataset[debuffId].SicknessDebuffScaling);
				num2++;
				span[num2] = (FireDebuffColor, BuffDatasets.DebuffDataset[debuffId].HeatDebuffScaling);
				num2++;
				span[num2] = (WaterDebuffColor, BuffDatasets.DebuffDataset[debuffId].WaterDebuffScaling);
				num2++;
				span[num2] = (ElectricDebuffColor, BuffDatasets.DebuffDataset[debuffId].ElectricDebuffScaling);
				num2++;
				span[num2] = (ColdDebuffColor, BuffDatasets.DebuffDataset[debuffId].ColdDebuffScaling);
				weights = list;
			}
			float totalWeight = 0f;
			Vector4 normalColor = default(Vector4);
			Color val2;
			foreach (var item in weights)
			{
				totalWeight += item.Item2;
				Vector4 val = normalColor;
				val2 = item.Item1;
				normalColor = val + ((Color)(ref val2)).ToVector4() * item.Item2;
			}
			if (totalWeight < 1f)
			{
				Vector4 val3 = normalColor;
				val2 = TypelessDebuffColor;
				normalColor = val3 + ((Color)(ref val2)).ToVector4() * (1f - totalWeight);
				totalWeight += 1f - totalWeight;
			}
			if (totalWeight != 0f)
			{
				normalColor /= totalWeight;
				((Color)(ref color))._002Ector(normalColor);
			}
		}
		return color;
	}

	public static bool ForceItemIntoWorld(Item item, float desiredDist = 15f)
	{
		if (item == null || !item.active)
		{
			return false;
		}
		float worldEdge = (float)Main.offLimitBorderTiles * 16f;
		float dist = worldEdge + desiredDist;
		float maxPosX = (float)Main.maxTilesX * 16f;
		float maxPosY = (float)Main.maxTilesY * 16f;
		bool moved = false;
		if (item.position.X < worldEdge)
		{
			item.position.X = dist;
			moved = true;
		}
		else if (item.position.X + (float)item.width > maxPosX - worldEdge)
		{
			item.position.X = maxPosX - (float)item.width - dist;
			moved = true;
		}
		if (item.position.Y < worldEdge)
		{
			item.position.Y = dist;
			moved = true;
		}
		else if (item.position.Y + (float)item.height > maxPosY - worldEdge)
		{
			item.position.Y = maxPosY - (float)item.height - dist;
			moved = true;
		}
		return moved;
	}

	public static bool IsEnchantable(this Item item)
	{
		if (item.IsAir)
		{
			return false;
		}
		if (item.maxStack > 1)
		{
			return false;
		}
		if (item.ammo != AmmoID.None)
		{
			return false;
		}
		if (item.Calamity().CannotBeEnchanted)
		{
			return false;
		}
		return true;
	}

	public static bool IsEnchanted(this Item item)
	{
		if (item.IsAir)
		{
			return false;
		}
		if (EnchantmentManager.ItemUpgradeRelationship.ContainsValue(item.type))
		{
			return true;
		}
		return item.Calamity().AppliedEnchantment.HasValue;
	}

	public static bool CheckWoodenAmmo(int type, Player player)
	{
		if (player.hasMoltenQuiver && type == 2)
		{
			return true;
		}
		return type == 1;
	}

	public static Rectangle FixSwingHitbox(float hitboxWidth, float hitboxHeight)
	{
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0300: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_049c: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.LocalPlayer;
		Item item = player.HeldItem;
		float mountOffsetY = player.mount.PlayerOffsetHitbox;
		float hitbox_X;
		float hitbox_Y;
		if ((double)player.itemAnimation < (double)player.itemAnimationMax * 0.333)
		{
			float shiftX = 10f;
			if (hitboxWidth >= 92f)
			{
				shiftX = 38f;
			}
			else if (hitboxWidth >= 64f)
			{
				shiftX = 28f;
			}
			else if (hitboxWidth >= 52f)
			{
				shiftX = 24f;
			}
			else if (hitboxWidth > 32f)
			{
				shiftX = 14f;
			}
			hitbox_X = player.position.X + (float)player.width * 0.5f + (hitboxWidth * 0.5f - shiftX) * (float)player.direction;
			hitbox_Y = player.position.Y + 24f + mountOffsetY;
		}
		else if ((double)player.itemAnimation < (double)player.itemAnimationMax * 0.666)
		{
			float shift = 10f;
			if (hitboxWidth >= 92f)
			{
				shift = 38f;
			}
			else if (hitboxWidth >= 64f)
			{
				shift = 28f;
			}
			else if (hitboxWidth >= 52f)
			{
				shift = 24f;
			}
			else if (hitboxWidth > 32f)
			{
				shift = 18f;
			}
			hitbox_X = player.position.X + ((float)player.width * 0.5f + (hitboxWidth * 0.5f - shift) * (float)player.direction);
			shift = 10f;
			if (hitboxHeight > 64f)
			{
				shift = 14f;
			}
			else if (hitboxHeight > 52f)
			{
				shift = 12f;
			}
			else if (hitboxHeight > 32f)
			{
				shift = 8f;
			}
			hitbox_Y = player.position.Y + shift + mountOffsetY;
		}
		else
		{
			float shift2 = 6f;
			if (hitboxWidth >= 92f)
			{
				shift2 = 38f;
			}
			else if (hitboxWidth >= 64f)
			{
				shift2 = 28f;
			}
			else if (hitboxWidth >= 52f)
			{
				shift2 = 24f;
			}
			else if (hitboxWidth >= 48f)
			{
				shift2 = 18f;
			}
			else if (hitboxWidth > 32f)
			{
				shift2 = 14f;
			}
			hitbox_X = player.position.X + (float)player.width * 0.5f - (hitboxWidth * 0.5f - shift2) * (float)player.direction;
			shift2 = 10f;
			if (hitboxHeight > 64f)
			{
				shift2 = 14f;
			}
			else if (hitboxHeight > 52f)
			{
				shift2 = 12f;
			}
			else if (hitboxHeight > 32f)
			{
				shift2 = 10f;
			}
			hitbox_Y = player.position.Y + shift2 + mountOffsetY;
		}
		if (player.gravDir == -1f)
		{
			hitbox_Y = player.position.Y + (float)player.height + (player.position.Y - hitbox_Y);
		}
		Rectangle hitbox = default(Rectangle);
		((Rectangle)(ref hitbox))._002Ector((int)hitbox_X, (int)hitbox_Y, 32, 32);
		if (item.damage >= 0 && item.type > 0 && !item.noMelee && player.itemAnimation > 0)
		{
			if (!Main.dedServ)
			{
				((Rectangle)(ref hitbox))._002Ector((int)hitbox_X, (int)hitbox_Y, (int)hitboxWidth, (int)hitboxHeight);
			}
			hitbox.Width = (int)((float)hitbox.Width * item.scale);
			hitbox.Height = (int)((float)hitbox.Height * item.scale);
			if (player.direction == -1)
			{
				hitbox.X -= hitbox.Width;
			}
			if (player.gravDir == 1f)
			{
				hitbox.Y -= hitbox.Height;
			}
			if (item.useStyle == 1)
			{
				if ((double)player.itemAnimation < (double)player.itemAnimationMax * 0.333)
				{
					if (player.direction == -1)
					{
						hitbox.X -= (int)((double)hitbox.Width * 1.4 - (double)hitbox.Width);
					}
					hitbox.Width = (int)((double)hitbox.Width * 1.4);
					hitbox.Y += (int)((double)hitbox.Height * 0.5 * (double)player.gravDir);
					hitbox.Height = (int)((double)hitbox.Height * 1.1);
				}
				else if ((double)player.itemAnimation >= (double)player.itemAnimationMax * 0.666)
				{
					if (player.direction == 1)
					{
						hitbox.X -= (int)((double)hitbox.Width * 1.2);
					}
					hitbox.Width *= 2;
					hitbox.Y -= (int)(((double)hitbox.Height * 1.4 - (double)hitbox.Height) * (double)player.gravDir);
					hitbox.Height = (int)((double)hitbox.Height * 1.4);
				}
			}
		}
		return hitbox;
	}

	public static Dust MeleeDustHelper(Player player, int dustType, float chancePerFrame, float minDistance, float maxDistance, float minRandRot = -0.2f, float maxRandRot = 0.2f, float minSpeed = 0.9f, float maxSpeed = 1.1f)
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextFloat(1f) < chancePerFrame)
		{
			float distance = Main.rand.NextFloat(minDistance, maxDistance);
			Vector2 offset = (player.itemRotation - (float)Math.PI / 4f * (float)player.direction + Main.rand.NextFloat(minRandRot, maxRandRot)).ToRotationVector2() * distance * (float)player.direction;
			Vector2 val = player.Center + offset;
			Vector2 vec = val - player.Center;
			Dust dust = Dust.NewDustPerfect(val, dustType);
			((Vector2)(ref vec)).Normalize();
			dust.velocity = vec * Main.rand.NextFloat(minSpeed, maxSpeed);
			return dust;
		}
		return null;
	}

	public static bool CanBeEnchantedBySomething(this Item item)
	{
		return EnchantmentManager.EnchantmentList.Any((Enchantment enchantment) => enchantment.ApplyRequirement(item));
	}

	public static void ConsumeItemViaQuickBuff(Player player, Item item, int buffType, int buffTime, bool reducedPotionSickness)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		bool showsOver = false;
		for (int l = 0; l < Player.MaxBuffs; l++)
		{
			int hasBuff = player.buffType[l];
			if (player.buffTime[l] > 0 && hasBuff == buffType)
			{
				showsOver = true;
			}
		}
		if (player.potionDelay > 0)
		{
			showsOver = true;
		}
		if (showsOver)
		{
			return;
		}
		SoundEngine.PlaySound(item.UseSound.GetValueOrDefault(), player.Center);
		int healAmt = (int)((float)item.healLife * player.Calamity().healingPotionMultiplier);
		if (healAmt > 0 && player.QuickHeal_GetItemToUse() != null && player.QuickHeal_GetItemToUse().type != item.type)
		{
			healAmt = 0;
		}
		player.HealPlayer(healAmt, (healAmt > 0) ? HealTextType.Broadcast : HealTextType.None);
		player.statMana += item.healMana;
		if (player.statMana > player.statManaMax2)
		{
			player.statMana = player.statManaMax2;
		}
		if (player.statLife > player.statLifeMax2)
		{
			player.statLife = player.statLifeMax2;
		}
		if (item.healMana > 0)
		{
			player.AddBuff(94, Player.manaSickTime);
		}
		if (Main.myPlayer == player.whoAmI && item.healMana > 0)
		{
			player.ManaEffect(item.healMana);
		}
		if (item.potion && healAmt > 0)
		{
			int duration = (reducedPotionSickness ? 3000 : 3600);
			if (player.pStone)
			{
				duration = (int)((double)duration * 0.75);
			}
			player.AddBuff(21, duration);
		}
		player.AddBuff(buffType, buffTime);
		item.stack--;
		if (item.stack <= 0)
		{
			item.TurnToAir();
		}
		Recipe.FindRecipes();
	}

	public static void TreasureBagLightAndDust(this Item item)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		Vector2 center = item.Center;
		Color newColor = Color.White;
		Lighting.AddLight(center, ((Color)(ref newColor)).ToVector3() * 0.4f);
		if (item.timeSinceItemSpawned % 12 == 0)
		{
			Vector2 val = item.Center + new Vector2(0f, (float)item.height * -0.1f);
			Vector2 direction = Main.rand.NextVector2CircularEdge((float)item.width * 0.6f, (float)item.height * 0.6f);
			float distance = 0.3f + Main.rand.NextFloat() * 0.5f;
			Vector2 velocity = default(Vector2);
			((Vector2)(ref velocity))._002Ector(0f, (0f - Main.rand.NextFloat()) * 0.3f - 1.5f);
			Vector2 position = val + direction * distance;
			Vector2? velocity2 = velocity;
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(position, 279, velocity2, 0, newColor);
			dust.scale = 0.5f;
			dust.fadeIn = 1.1f;
			dust.noGravity = true;
			dust.noLight = true;
			dust.alpha = 0;
		}
	}

	public static void RestoreConsumedItemByRightClick(this Item item)
	{
		bool favorited = item.favorited;
		item.SetDefaults(item.type);
		item.stack++;
		item.favorited = favorited;
		item.NetStateChanged();
	}

	public static int RandomRoguePrefix()
	{
		Mod mod = ModContent.GetInstance<CalamityMod>();
		UnifiedRandom rand = Main.rand;
		int[] obj = new int[39]
		{
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 36, 37, 38, 39,
			40, 53, 54, 55, 56, 57, 59, 60, 61, 42,
			43, 44, 45, 46, 47, 48, 49, 50, 51
		};
		obj[0] = mod.Find<ModPrefix>("Radical").Type;
		obj[1] = mod.Find<ModPrefix>("Pointy").Type;
		obj[2] = mod.Find<ModPrefix>("Sharp").Type;
		obj[3] = mod.Find<ModPrefix>("Glorious").Type;
		obj[4] = mod.Find<ModPrefix>("Feathered").Type;
		obj[5] = mod.Find<ModPrefix>("Sleek").Type;
		obj[6] = mod.Find<ModPrefix>("Hefty").Type;
		obj[7] = mod.Find<ModPrefix>("Mighty").Type;
		obj[8] = mod.Find<ModPrefix>("Serrated").Type;
		obj[9] = mod.Find<ModPrefix>("Vicious").Type;
		obj[10] = mod.Find<ModPrefix>("Lethal").Type;
		obj[11] = mod.Find<ModPrefix>("Flawless").Type;
		obj[12] = mod.Find<ModPrefix>("Blunt").Type;
		obj[13] = mod.Find<ModPrefix>("Flimsy").Type;
		obj[14] = mod.Find<ModPrefix>("Unbalanced").Type;
		obj[15] = mod.Find<ModPrefix>("Atrocious").Type;
		return Utils.SelectRandom(rand, obj);
	}

	public static bool NegativeRoguePrefix(int prefix)
	{
		Mod mod = ModContent.GetInstance<CalamityMod>();
		return new List<int>
		{
			mod.Find<ModPrefix>("Blunt").Type,
			mod.Find<ModPrefix>("Flimsy").Type,
			mod.Find<ModPrefix>("Unbalanced").Type,
			mod.Find<ModPrefix>("Atrocious").Type
		}.Contains(prefix);
	}

	public static LocalizedText GetText(string key)
	{
		return Language.GetOrRegister("Mods.CalamityMod." + key);
	}

	public static string GetTextValue(string key)
	{
		return Language.GetTextValue("Mods.CalamityMod." + key);
	}

	public static LocalizedText GetItemName(int itemID)
	{
		if (itemID < ItemID.Count)
		{
			return Language.GetText("ItemName." + ItemID.Search.GetName(itemID));
		}
		return GetTextFromModItem(itemID, "DisplayName");
	}

	public static LocalizedText GetItemName<T>() where T : ModItem
	{
		return GetTextFromModItem(ModContent.ItemType<T>(), "DisplayName");
	}

	public static LocalizedText GetTextFromModItem(int itemID, string suffix)
	{
		return ItemLoader.GetItem(itemID).GetLocalization(suffix);
	}

	public static LocalizedText GetTextFromModItem<T>(string suffix) where T : ModItem
	{
		return GetTextFromModItem(ModContent.ItemType<T>(), suffix);
	}

	public static string GetTextValueFromModItem(int itemID, string suffix)
	{
		return GetTextFromModItem(itemID, suffix).ToString();
	}

	public static string GetTextValueFromModItem<T>(string suffix) where T : ModItem
	{
		return GetTextFromModItem(ModContent.ItemType<T>(), suffix).ToString();
	}

	public static string EmbedItemIcon(this int itemID)
	{
		return $"[i:{itemID}] " + GetItemName(itemID);
	}

	public static string FramesToSeconds(this int frame)
	{
		return ((float)frame / 60f).Round("N2");
	}

	public static string ToMph(this float velocity)
	{
		return (velocity * 216000f / 42240f).Round("N0");
	}

	public static string ToMphps(this float velocity)
	{
		return (velocity * 60f * 216000f / 42240f).Round("N2");
	}

	public static string ToTiles(this float pixel)
	{
		return (pixel / 16f).Round();
	}

	public static string ToRegenPerSecond(this int regen)
	{
		return ((float)regen * 0.5f).Round("N2");
	}

	public static string ToJumpSpeedPercent(this float boost)
	{
		return (boost * 20f).Round("N2");
	}

	public static string ToStealth(this float stealth)
	{
		return (stealth * 100f).Round("N0");
	}

	public static string GetChanceFromDenominator(this int denominator)
	{
		return (1f / (float)denominator).ToPercent();
	}

	public static string ToPercent(this float percent, string precision = "N1")
	{
		return (percent * 100f).Round(precision);
	}

	public static string ToPercent(this double percent, string precision = "N1")
	{
		return (percent * 100.0).Round(precision);
	}

	public static string Round(this float number, string precision = "N4")
	{
		return float.Parse(number.ToString(precision)).ToString();
	}

	public static string Round(this double number, string precision = "N4")
	{
		return float.Parse(number.ToString(precision)).ToString();
	}

	public static string GetArmorSetBonusKey()
	{
		ModKeybind setBonusKey = CalamityKeybinds.ArmorSetBonusHotKey;
		bool hasHotkey = setBonusKey.GetAssignedKeysOrEmpty().Count != 0;
		string directionKey = (Main.ReversedUpDownArmorSetBonuses ? Language.GetTextValue("Key.UP") : Language.GetTextValue("Key.DOWN"));
		if (hasHotkey && CalamityClientConfig.Instance.SetBonusDoubleTap == SetBonusDoubleTapOptions.On)
		{
			return GetText("Common.BothArmorSetBonusKeys").Format(setBonusKey.TooltipHotkeyString(), directionKey);
		}
		if (!hasHotkey && CalamityClientConfig.Instance.SetBonusDoubleTap == SetBonusDoubleTapOptions.Off)
		{
			return GetTextValue("Common.NoArmorSetBonusKey");
		}
		if (hasHotkey)
		{
			return GetText("Common.ArmorSetBonusKey").Format(setBonusKey.TooltipHotkeyString());
		}
		return GetText("Common.DoubleTapDown").Format(directionKey);
	}

	public static void ILFailure(this ILog logger, string name, string reason)
	{
		logger.Warn((object)("IL edit \"" + name + "\" failed! " + reason));
	}

	public static float PerlinNoise2D(float x, float y, int octaves, int seed)
	{
		int frequency = (int)Math.Pow(2.0, octaves);
		x *= (float)frequency;
		y *= (float)frequency;
		int flooredX = (int)x;
		int flooredY = (int)y;
		int ceilingX = flooredX + 1;
		int ceilingY = flooredY + 1;
		float interpolatedX = x - (float)flooredX;
		float interpolatedY = y - (float)flooredY;
		float interpolatedX2 = interpolatedX - 1f;
		float interpolatedY2 = interpolatedY - 1f;
		float fadeX = SmoothFunction(interpolatedX);
		float fadeY = SmoothFunction(interpolatedY);
		float num = MathHelper.Lerp(NoiseGradient(seed, flooredX, flooredY, interpolatedX, interpolatedY), NoiseGradient(seed, ceilingX, flooredY, interpolatedX2, interpolatedY), fadeX);
		float smoothY = MathHelper.Lerp(NoiseGradient(seed, flooredX, ceilingY, interpolatedX, interpolatedY2), NoiseGradient(seed, ceilingX, ceilingY, interpolatedX2, interpolatedY2), fadeX);
		return MathHelper.Lerp(num, smoothY, fadeY);
		static float NoiseGradient(int s, int noiseX, int noiseY, float xd, float yd)
		{
			//IL_0031: Unknown result type (might be due to invalid IL or missing references)
			//IL_0036: Unknown result type (might be due to invalid IL or missing references)
			//IL_0038: Unknown result type (might be due to invalid IL or missing references)
			//IL_0041: Unknown result type (might be due to invalid IL or missing references)
			int hash = s;
			hash ^= 1619 * noiseX;
			hash ^= 31337 * noiseY;
			hash = hash * hash * hash * 60493;
			hash = (hash >> 13) ^ hash;
			Vector2 g = Directions[hash & 7];
			return xd * g.X + yd * g.Y;
		}
		static float SmoothFunction(float n)
		{
			return 3f * n * n - 2f * n * n * n;
		}
	}

	public static float AperiodicSin(float x, float dx = 0f, float a = (float)Math.PI, float b = (float)Math.E)
	{
		return (float)(Math.Sin(x * a + dx) + Math.Sin(x * b + dx)) * 0.5f;
	}

	public static float ManhattanDistance(this Vector2 a, Vector2 b)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
	}

	public static int ManhattanDistance(this Point a, Point b)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
	}

	public static float WrapAngle90Degrees(float theta)
	{
		if (theta > (float)Math.PI)
		{
			theta -= (float)Math.PI;
		}
		if (theta > (float)Math.PI / 2f)
		{
			theta -= (float)Math.PI;
		}
		if (theta < -(float)Math.PI / 2f)
		{
			theta += (float)Math.PI;
		}
		return theta;
	}

	public static float AngleBetween(this Vector2 v1, Vector2 v2)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		return (float)Math.Acos(Vector2.Dot(v1.SafeNormalize(Vector2.Zero), v2.SafeNormalize(Vector2.Zero)));
	}

	public static float Convert01To010(float value)
	{
		return (float)Math.Sin((float)Math.PI * MathHelper.Clamp(value, 0f, 1f));
	}

	public static Vector2 GetProjectilePhysicsFiringVelocity(Vector2 shootingPosition, Vector2 destination, float gravity, float shootSpeed, Vector2? nanFallback = null)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		gravity = 0f - Math.Abs(gravity);
		float horizontalRange = MathHelper.Distance(shootingPosition.X, destination.X);
		float fireAngleSine = gravity * horizontalRange / (float)Math.Pow(shootSpeed, 2.0);
		if (!nanFallback.HasValue)
		{
			fireAngleSine = MathHelper.Clamp(fireAngleSine, -1f, 1f);
		}
		float fireAngle = (float)Math.Asin(fireAngleSine) * 0.5f;
		if (float.IsNaN(fireAngle))
		{
			return nanFallback.Value * shootSpeed;
		}
		Vector2 fireVelocity = Utils.RotatedBy(new Vector2(0f, 0f - shootSpeed), (double)fireAngle, default(Vector2));
		fireVelocity.X *= (destination.X - shootingPosition.X < 0f).ToDirectionInt();
		return fireVelocity;
	}

	public static float ShortestDistanceToLine(this Vector2 point, Vector2 lineStart, Vector2 lineEnd)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		Vector2 perpendicular = (lineEnd - lineStart).RotatedBy(1.5707963705062866);
		Vector2 pointToOrigin = point - lineStart;
		return Math.Abs(pointToOrigin.X * perpendicular.X + pointToOrigin.Y * perpendicular.Y) / ((Vector2)(ref perpendicular)).Length();
	}

	public static Vector2 ClosestPointOnLine(this Vector2 point, Vector2 lineStart, Vector2 lineEnd)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 perpendicular = (lineEnd - lineStart).RotatedBy(1.5707963705062866).SafeNormalize(Vector2.Zero);
		float distanceToLine = point.ShortestDistanceToLine(lineStart, lineEnd);
		float lineSide = Math.Sign((point.X - lineStart.X) * (0f - lineEnd.Y + lineStart.Y) + (point.Y - lineStart.Y) * (lineEnd.X - lineStart.X));
		return point + distanceToLine * lineSide * perpendicular;
	}

	public static float Modulo(this float dividend, float divisor)
	{
		return dividend - (float)Math.Floor(dividend / divisor) * divisor;
	}

	public static Vector2 ClampMagnitude(this Vector2 v, float min, float max)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return v.SafeNormalize(Vector2.UnitY) * MathHelper.Clamp(((Vector2)(ref v)).Length(), min, max);
	}

	public static float AngleBetween(this float angle, float otherAngle)
	{
		return (otherAngle - angle + (float)Math.PI).Modulo((float)Math.PI * 2f) - (float)Math.PI;
	}

	public static int DirectionalSign(this float x)
	{
		return (x > 0f).ToDirectionInt();
	}

	public static double ApproximateDerivative(this Func<double, double> fx, double x)
	{
		double num = fx(x + 1E-07);
		double right = fx(x - 1E-07);
		return (num - right) * 5000000.0;
	}

	public static double IterativelySearchForRoot(Func<double, double> fx, double initialGuess, int iterations)
	{
		double result = initialGuess;
		for (int i = 0; i < iterations; i++)
		{
			double derivative = (float)fx.ApproximateDerivative(result);
			result -= fx(result) / derivative;
		}
		return result;
	}

	public static List<Point> GetIntersectingPointsInLine(Point start, Point end)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		List<Point> intersectingCells = new List<Point>();
		int dx = Math.Abs(end.X - start.X);
		int dy = Math.Abs(end.Y - start.Y);
		int sx = ((start.X < end.X) ? 1 : (-1));
		int sy = ((start.Y < end.Y) ? 1 : (-1));
		int err = dx - dy;
		while (WorldGen.InWorld(start.X, start.Y))
		{
			intersectingCells.Add(start);
			if (start == end)
			{
				break;
			}
			int num = 2 * err;
			if (num > -dy)
			{
				err -= dy;
				start.X += sx;
			}
			if (num < dx)
			{
				err += dx;
				start.Y += sy;
			}
		}
		return intersectingCells;
	}

	public static List<Point> GetIntersectingPointsInLine(Vector2 start, Vector2 end)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		return GetIntersectingPointsInLine(start.ToTileCoordinates(), end.ToTileCoordinates());
	}

	public static Vector2 RotateDirectionTowards(this Vector2 vec, float targetAngle, float maxChange)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		return vec.ToRotation().AngleTowards(targetAngle, maxChange).ToRotationVector2();
	}

	public static Vector2 RotateTowards(this Vector2 vec, float targetAngle, float maxChange)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return vec.RotateDirectionTowards(targetAngle, maxChange) * ((Vector2)(ref vec)).Length();
	}

	public static float LinearEasing(float amount, int degree)
	{
		return amount;
	}

	public static float SineInEasing(float amount, int degree)
	{
		return 1f - (float)Math.Cos(amount * (float)Math.PI / 2f);
	}

	public static float SineOutEasing(float amount, int degree)
	{
		return (float)Math.Sin(amount * (float)Math.PI / 2f);
	}

	public static float SineInOutEasing(float amount, int degree)
	{
		return (0f - ((float)Math.Cos(amount * (float)Math.PI) - 1f)) / 2f;
	}

	public static float SineBumpEasing(float amount, int degree)
	{
		return (float)Math.Sin(amount * (float)Math.PI);
	}

	public static float PolyInEasing(float amount, int degree)
	{
		return (float)Math.Pow(amount, degree);
	}

	public static float PolyOutEasing(float amount, int degree)
	{
		return 1f - (float)Math.Pow(1f - amount, degree);
	}

	public static float PolyInOutEasing(float amount, int degree)
	{
		if (!(amount < 0.5f))
		{
			return 1f - (float)Math.Pow(-2f * amount + 2f, degree) / 2f;
		}
		return (float)Math.Pow(2.0, degree - 1) * (float)Math.Pow(amount, degree);
	}

	public static float ExpInEasing(float amount, int degree)
	{
		if (amount != 0f)
		{
			return (float)Math.Pow(2.0, 10f * amount - 10f);
		}
		return 0f;
	}

	public static float ExpOutEasing(float amount, int degree)
	{
		if (amount != 1f)
		{
			return 1f - (float)Math.Pow(2.0, -10f * amount);
		}
		return 1f;
	}

	public static float ExpInOutEasing(float amount, int degree)
	{
		if (amount != 0f)
		{
			if (amount != 1f)
			{
				if (!(amount < 0.5f))
				{
					return (2f - (float)Math.Pow(2.0, -20f * amount - 10f)) / 2f;
				}
				return (float)Math.Pow(2.0, 20f * amount - 10f) / 2f;
			}
			return 1f;
		}
		return 0f;
	}

	public static float CircInEasing(float amount, int degree)
	{
		return 1f - (float)Math.Sqrt(1.0 - Math.Pow(amount, 2.0));
	}

	public static float CircOutEasing(float amount, int degree)
	{
		return (float)Math.Sqrt(1.0 - Math.Pow(amount - 1f, 2.0));
	}

	public static float CircInOutEasing(float amount, int degree)
	{
		if (!((double)amount < 0.5))
		{
			return ((float)Math.Sqrt(1.0 - Math.Pow(-2f * amount - 2f, 2.0)) + 1f) / 2f;
		}
		return (1f - (float)Math.Sqrt(1.0 - Math.Pow(2f * amount, 2.0))) / 2f;
	}

	public static float PiecewiseAnimation(float progress, params CurveSegment[] segments)
	{
		if (segments.Length == 0)
		{
			return 0f;
		}
		if (segments[0].startingX != 0f)
		{
			segments[0].startingX = 0f;
		}
		progress = MathHelper.Clamp(progress, 0f, 1f);
		float ratio = 0f;
		for (int i = 0; i <= segments.Length - 1; i++)
		{
			CurveSegment segment = segments[i];
			float startPoint = segment.startingX;
			float endPoint = 1f;
			if (progress < segment.startingX)
			{
				continue;
			}
			if (i < segments.Length - 1)
			{
				if (segments[i + 1].startingX <= progress)
				{
					continue;
				}
				endPoint = segments[i + 1].startingX;
			}
			float segmentLength = endPoint - startPoint;
			float segmentProgress = (progress - segment.startingX) / segmentLength;
			ratio = segment.startingHeight;
			ratio = ((segment.easing == null) ? (ratio + LinearEasing(segmentProgress, segment.degree) * segment.elevationShift) : (ratio + segment.easing(segmentProgress, segment.degree) * segment.elevationShift));
			break;
		}
		return ratio;
	}

	public static float EaseInOutExp(float lerpValue, float inPower, float outPower)
	{
		if (!(lerpValue < 0.5f))
		{
			return 0.5f + (1f - (float)Math.Pow(Utils.GetLerpValue(1f, 0.5f, lerpValue, clamped: true), outPower)) * 0.5f;
		}
		return (float)Math.Pow(Utils.GetLerpValue(0f, 0.5f, lerpValue, clamped: true), inPower) * 0.5f;
	}

	public static void BroadcastLocalizedText(string key, Color? textColor = null)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		if (!textColor.HasValue)
		{
			textColor = Color.White;
		}
		if (Main.netMode == 0)
		{
			Main.NewText(Language.GetTextValue(key), textColor.Value);
		}
		else if (Main.dedServ)
		{
			ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key), textColor.Value);
		}
	}

	public static void BroadcastFormattedText(string key, Color textColor, params object[] args)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode == 0)
		{
			Main.NewText(Language.GetOrRegister(key).Format(args), textColor);
		}
		else if (Main.dedServ)
		{
			ChatHelper.BroadcastChatMessage(NetworkText.FromKey(key, args), textColor);
		}
	}

	public static int IngredientIndex(this Recipe r, int itemID)
	{
		for (int i = 0; i < r.requiredItem.Count; i++)
		{
			if (r.requiredItem[i].type == itemID)
			{
				return i;
			}
		}
		return -1;
	}

	public static bool ChangeIngredientStack(this Recipe r, int itemID, int stack)
	{
		int idx = r.IngredientIndex(itemID);
		if (idx == -1)
		{
			return false;
		}
		r.requiredItem[idx].stack = stack;
		return true;
	}

	public static void SwapArrayIndices<T>(ref T[] array, int index1, int index2)
	{
		T temp = array[index1];
		array[index1] = array[index2];
		array[index2] = temp;
	}

	public static T[] ShuffleArray<T>(T[] array, Random rand = null)
	{
		if (rand == null)
		{
			rand = new Random();
		}
		for (int i = array.Length; i > 0; i--)
		{
			int j = rand.Next(i);
			T tempElement = array[j];
			array[j] = array[i - 1];
			array[i - 1] = tempElement;
		}
		return array;
	}

	public static T[,] ShaveOffEdge<T>(this T[,] array)
	{
		if (array.GetLength(0) <= 2 || array.GetLength(1) <= 2)
		{
			return array;
		}
		T[,] result = new T[array.GetLength(0) - 2, array.GetLength(1) - 2];
		for (int i = 0; i < result.GetLength(0); i++)
		{
			for (int j = 0; j < result.GetLength(1); j++)
			{
				result[i, j] = array[i + 1, j + 1];
			}
		}
		return result;
	}

	public static Color[,] GetColorsFromTexture(this Texture2D texture)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		Color[] alignedColors = (Color[])(object)new Color[texture.Width * texture.Height];
		texture.GetData<Color>(alignedColors);
		Color[,] colors2D = new Color[texture.Width, texture.Height];
		for (int x = 0; x < texture.Width; x++)
		{
			for (int y = 0; y < texture.Height; y++)
			{
				colors2D[x, y] = alignedColors[x + y * texture.Width];
			}
		}
		return colors2D;
	}

	public static bool ContainsType<T>(this IEnumerable<T> collection, Type type)
	{
		return collection.Any((T entry) => entry.GetType() == type);
	}

	public static (float, float) CalculateSoundStats(Vector2 soundPos, bool ambient = false)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		float volume = 0f;
		float pan = 0f;
		if (soundPos.X == -1f || soundPos.Y == -1f)
		{
			volume = 1f;
		}
		else if (WorldGen.gen || Main.dedServ)
		{
			volume = 0f;
		}
		else
		{
			float topLeftX = Main.screenPosition.X - (float)Main.screenWidth * 2f;
			float topLeftY = Main.screenPosition.Y - (float)Main.screenHeight * 2f;
			Rectangle audibleArea = default(Rectangle);
			((Rectangle)(ref audibleArea))._002Ector((int)topLeftX, (int)topLeftY, Main.screenWidth * 5, Main.screenHeight * 5);
			Rectangle soundHitbox = default(Rectangle);
			((Rectangle)(ref soundHitbox))._002Ector((int)soundPos.X, (int)soundPos.Y, 1, 1);
			Vector2 screenCenter = Main.screenPosition + new Vector2((float)Main.screenWidth * 0.5f, (float)Main.screenHeight * 0.5f);
			if (((Rectangle)(ref audibleArea)).Intersects(soundHitbox))
			{
				pan = (soundPos.X - screenCenter.X) / ((float)Main.screenWidth * 0.5f);
				float dist = Vector2.Distance(soundPos, screenCenter);
				volume = 1f - dist / ((float)Main.screenWidth * 1.5f);
			}
		}
		pan = MathHelper.Clamp(pan, -1f, 1f);
		volume = MathHelper.Clamp(volume, 0f, 1f);
		volume = ((!ambient) ? (volume * Main.soundVolume) : (Main.gameInactive ? 0f : (volume * Main.ambientVolume)));
		volume = MathHelper.Clamp(volume, 0f, 1f);
		return (volume, pan);
	}

	public static void ApplySoundStats(ref SoundEffectInstance sfx, Vector2 soundPos, bool ambient = false)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		if (sfx != null && !sfx.IsDisposed)
		{
			SoundEffectInstance obj = sfx;
			SoundEffectInstance val = sfx;
			(float, float) tuple = CalculateSoundStats(soundPos, ambient);
			obj.Volume = tuple.Item1;
			val.Pan = tuple.Item2;
		}
	}

	public static void SafeVolumeChange(ref SoundEffectInstance sfx, float volumeMultiplier)
	{
		if (sfx != null && !sfx.IsDisposed)
		{
			sfx.Volume = MathHelper.Clamp(sfx.Volume * volumeMultiplier, 0f, 1f);
		}
	}

	public static void AddWithCondition<T>(this List<T> list, T type, bool condition)
	{
		if (condition)
		{
			list.Add(type);
		}
	}

	public static int ScaleWithDifficulty(this int value)
	{
		return value * (Main.masterMode ? 3 : ((!Main.expertMode) ? 1 : 2));
	}

	public static int SecondsToFrames(int seconds)
	{
		return seconds * 60;
	}

	public static int SecondsToFrames(float seconds)
	{
		return (int)MathF.Round(seconds * 60f);
	}

	public static int MinutesToFrames(int minutes)
	{
		return minutes * 3600;
	}

	public static bool WithinBounds(this int index, int cap)
	{
		if (index >= 0)
		{
			return index < cap;
		}
		return false;
	}

	public static void DistanceClamp(ref Vector2 start, ref Vector2 end, float maxDistance)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		if (Vector2.Distance(end, start) > maxDistance)
		{
			end = start + Vector2.Normalize(end - start) * maxDistance;
		}
	}

	public static void ChangeTime(bool changeToDay)
	{
		Main.time = 0.0;
		Main.dayTime = changeToDay;
		CalamityNetcode.SyncWorld();
	}

	public static bool IntoMorseCode(string originalText, float completion)
	{
		int spaceLength = 13;
		int betweenLetterLength = 7;
		int betweenBlipLength = 4;
		int shortLength = 3;
		int longLength = 8;
		char[] TextKeys = new char[37]
		{
			' ', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i',
			'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's',
			't', 'u', 'v', 'w', 'x', 'y', 'z', '1', '2', '3',
			'4', '5', '6', '7', '8', '9', '0'
		};
		string[] MorseKeys = new string[37]
		{
			" ", ".-|", "-...|", "-.-. |", "-..|", ".|", "..-.|", "--.|", "....|", "..|",
			".---|", "-.-|", ".-..|", "--|", "-.|", "---|", ".--.|", "--.-|", ".-.|", "...|",
			"-|", "..-|", "...-|", ".--|", "-..-|", "-.--|", "--..|", ".----|", "..---|", "...--|",
			"....-|", ".....|", "-....|", "--...|", "---..|", "----.|", "-----|"
		};
		string morseText = "";
		originalText = originalText.ToLower();
		for (int i = 0; i < originalText.Length; i++)
		{
			for (int j = 0; j < 37; j++)
			{
				if (TextKeys[j] == originalText[i])
				{
					morseText += MorseKeys[j];
					break;
				}
			}
		}
		List<bool> morseState = new List<bool>();
		for (int k = 0; k < morseText.Length; k++)
		{
			if (morseText[k] == " ".ToCharArray()[0])
			{
				morseState.AddRange(Enumerable.Repeat(element: false, spaceLength));
			}
			if (morseText[k] == "|".ToCharArray()[0])
			{
				morseState.AddRange(Enumerable.Repeat(element: false, betweenLetterLength));
			}
			if (morseText[k] == ".".ToCharArray()[0])
			{
				morseState.AddRange(Enumerable.Repeat(element: true, shortLength));
			}
			if (morseText[k] == "-".ToCharArray()[0])
			{
				morseState.AddRange(Enumerable.Repeat(element: true, longLength));
			}
			morseState.AddRange(Enumerable.Repeat(element: false, betweenBlipLength));
		}
		return morseState[(int)((float)(morseState.Count - 1) * completion)];
	}

	public static List<string> GetAssignedKeysOrEmpty(this ModKeybind keybind, InputMode mode = InputMode.Keyboard)
	{
		if (keybind == null)
		{
			return new List<string>();
		}
		if (Main.dedServ)
		{
			return new List<string>();
		}
		try
		{
			return keybind.GetAssignedKeys(mode);
		}
		catch
		{
			return new List<string>();
		}
	}

	public static void WritePackedWorldPosition(this BinaryWriter writer, Vector2 worldPositionX16)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		writer.WritePackedWorldPosition((int)worldPositionX16.X, (int)worldPositionX16.Y);
	}

	public static void WritePackedWorldPosition(this BinaryWriter writer, int worldX, int worldY)
	{
		int tileX = Math.DivRem(worldX, 16, out var remX);
		int tileY = Math.DivRem(worldY, 16, out var remY);
		byte remByte = (byte)((remX << 4) | remY);
		writer.Write((ushort)Math.Clamp(tileX, 0, 65535));
		writer.Write((ushort)Math.Clamp(tileY, 0, 65535));
		writer.Write(remByte);
	}

	public static Vector2 ReadPackedWorldPosition(this BinaryReader reader)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		reader.ReadPackedWorldPosition(out var worldX, out var worldY);
		return new Vector2((float)worldX, (float)worldY);
	}

	public static void ReadPackedWorldPosition(this BinaryReader reader, out int worldX, out int worldY)
	{
		int tileX = reader.ReadUInt16();
		int tileY = reader.ReadUInt16();
		byte num = reader.ReadByte();
		int remX = num >> 4;
		int remY = num & 0xF;
		worldX = tileX * 16 + remX;
		worldY = tileY * 16 + remY;
	}

	public static void WriteTileEntityID(this BinaryWriter writer, TileEntity tileEntity)
	{
		if (tileEntity == null)
		{
			writer.Write(int.MaxValue);
		}
		else if (!TileEntity.ByID.ContainsKey(tileEntity.ID))
		{
			writer.Write(int.MaxValue);
		}
		else
		{
			writer.Write(tileEntity.ID);
		}
	}

	public static TileEntityType ReadTileEntity<TileEntityType>(this BinaryReader reader) where TileEntityType : TileEntity
	{
		return reader.ReadTileEntity() as TileEntityType;
	}

	public static TileEntity ReadTileEntity(this BinaryReader reader)
	{
		int id = reader.ReadInt32();
		if (!TileEntity.ByID.TryGetValue(id, out var tileEntity))
		{
			return null;
		}
		return tileEntity;
	}

	public static void WriteWhoAmI(this BinaryWriter writer, ModPlayer player)
	{
		writer.WriteWhoAmI(player?.Player);
	}

	public static void WriteWhoAmI(this BinaryWriter writer, Player player)
	{
		byte whoAmI = (byte)(player?.whoAmI ?? 255);
		writer.Write(whoAmI);
	}

	public static void WriteWhoAmI(this BinaryWriter writer, ModNPC npc)
	{
		writer.WriteWhoAmI(npc?.NPC);
	}

	public static void WriteWhoAmI(this BinaryWriter writer, NPC npc)
	{
		byte whoAmI = (byte)(npc?.whoAmI ?? Main.maxNPCs);
		writer.Write(whoAmI);
	}

	public static CalamityPlayer ReadCalamityPlayer(this BinaryReader reader, bool nullOnInactive = true)
	{
		return reader.ReadPlayer(nullOnInactive)?.Calamity() ?? null;
	}

	public static Player ReadPlayer(this BinaryReader reader, bool nullOnInactive = true)
	{
		int index = reader.ReadByte();
		if (index >= 255)
		{
			return null;
		}
		Player player = Main.player[index];
		if (nullOnInactive && player.IsNullOrInactive())
		{
			return null;
		}
		return player;
	}

	public static NPCType ReadModNPC<NPCType>(this BinaryReader reader, bool nullOnInactive = true) where NPCType : ModNPC
	{
		return reader.ReadNPC(nullOnInactive)?.ModNPC as NPCType;
	}

	public static ModNPC ReadModNPC(this BinaryReader reader, bool nullOnInactive = true)
	{
		return reader.ReadNPC(nullOnInactive)?.ModNPC ?? null;
	}

	public static NPC ReadNPC(this BinaryReader reader, bool nullOnInactive = true)
	{
		int index = reader.ReadByte();
		if (index >= Main.maxNPCs)
		{
			return null;
		}
		NPC npc = Main.npc[index];
		if (nullOnInactive && npc.IsNullOrInactive())
		{
			return null;
		}
		return npc;
	}

	public static T ModNPC<T>(this NPC npc) where T : ModNPC
	{
		return npc.ModNPC as T;
	}

	public static int CountNPCsBetter(params int[] typesToCheck)
	{
		if (typesToCheck.Length == 0)
		{
			return 0;
		}
		int count = 0;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (typesToCheck.Contains(n.type))
			{
				count++;
			}
		}
		return count;
	}

	public static bool AnyBossNPCS(bool checkForMechs = false)
	{
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (!npc.IsABoss())
			{
				continue;
			}
			if (checkForMechs)
			{
				if (npc.type != 134 && npc.type != 127 && npc.type != 126)
				{
					return npc.type == 125;
				}
				return true;
			}
			return true;
		}
		return false;
	}

	public static void HideFromBestiary(this ModNPC n)
	{
		NPCID.Sets.NPCBestiaryDrawModifiers nPCBestiaryDrawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers();
		nPCBestiaryDrawModifiers.Hide = true;
		NPCID.Sets.NPCBestiaryDrawModifiers value = nPCBestiaryDrawModifiers;
		NPCID.Sets.NPCBestiaryDrawOffset.Add(n.Type, value);
	}

	public static void LifeMaxNERB(this NPC npc, int normal, int? revengeance = null, int? bossRush = null)
	{
		npc.lifeMax = normal;
		if (bossRush.HasValue && BossRushEvent.BossRushActive)
		{
			npc.lifeMax = bossRush.Value;
		}
		else if (revengeance.HasValue && CalamityWorld.revenge)
		{
			npc.lifeMax = revengeance.Value;
		}
	}

	public static void DR_NERD(this NPC npc, float normal, float? revengeance = null, float? death = null, float? bossRush = null)
	{
		npc.Calamity().DR = normal;
		if (bossRush.HasValue && BossRushEvent.BossRushActive)
		{
			npc.Calamity().DR = bossRush.Value;
		}
		else if (revengeance.HasValue && CalamityWorld.revenge)
		{
			npc.Calamity().DR = (CalamityWorld.death ? death.Value : revengeance.Value);
		}
	}

	public static bool IsAnEnemy(this NPC npc, bool allowStatues = true, bool checkDead = true, bool checkDamage = true)
	{
		if (npc == null || (!npc.active && (!checkDead || npc.life > 0)) || npc.townNPC || npc.friendly)
		{
			return false;
		}
		if (!allowStatues && npc.SpawnedFromStatue)
		{
			return false;
		}
		if (npc.lifeMax <= 5 || (((npc.defDamage <= 5) & checkDamage) && npc.lifeMax <= 3000))
		{
			return false;
		}
		if (CalamityNPCSets.DontCountAsEnemy[npc.type])
		{
			return false;
		}
		return true;
	}

	public static bool IsABoss(this NPC npc)
	{
		if (npc == null || !npc.active)
		{
			return false;
		}
		if (npc.boss && npc.type != 395)
		{
			return true;
		}
		if (npc.type == 14 || npc.type == 13 || npc.type == 15)
		{
			return true;
		}
		if (npc.type != ModContent.NPCType<EbonianPaladin>() && npc.type != ModContent.NPCType<CrimulanPaladin>() && npc.type != ModContent.NPCType<SplitEbonianPaladin>())
		{
			return npc.type == ModContent.NPCType<SplitCrimulanPaladin>();
		}
		return true;
	}

	public static void SyncExtraAI(this NPC npc)
	{
		if (Main.netMode != 0)
		{
			SyncCalamityNPCAIArrayPacket.Send(npc);
		}
	}

	public static void SyncVanillaLocalAI(this NPC npc)
	{
		if (Main.netMode != 0)
		{
			SyncVanillaNPCLocalAIArrayPacket.Send(npc);
		}
	}

	public static void ForceNetUpdate(this NPC npc, bool ignoreCurrentNetSpam = true)
	{
		npc.netUpdate = true;
		if ((npc.netSpam >= 10) | ignoreCurrentNetSpam)
		{
			npc.netSpam = 0;
		}
	}

	public static void SyncMotionToServer(this NPC npc)
	{
		if (Main.netMode == 1)
		{
			SyncNPCMotionDataToServerPacket.Send(npc);
		}
	}

	public static void SyncNPCPosAndRotOnly(this NPC npc)
	{
		SyncNPCPosAndRotOnlyPacket.Send(npc);
	}

	public static void SmoothMovement(NPC npc, float movementDistanceGateValue, Vector2 distanceFromDestination, float baseVelocity, float acceleration, bool useSimpleFlyMovement)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		float lerpValue = Utils.GetLerpValue(movementDistanceGateValue, 2400f, ((Vector2)(ref distanceFromDestination)).Length(), clamped: true);
		float minVelocity = ((Vector2)(ref distanceFromDestination)).Length();
		if (minVelocity > baseVelocity)
		{
			minVelocity = baseVelocity;
		}
		Vector2 maxVelocity = distanceFromDestination / 24f;
		float maxVelocityCap = baseVelocity * 3f;
		if (((Vector2)(ref maxVelocity)).Length() > maxVelocityCap)
		{
			maxVelocity = distanceFromDestination.SafeNormalize(Vector2.Zero) * maxVelocityCap;
		}
		Vector2 desiredVelocity = Vector2.Lerp(distanceFromDestination.SafeNormalize(Vector2.Zero) * minVelocity, maxVelocity, lerpValue);
		if (useSimpleFlyMovement)
		{
			npc.SimpleFlyMovement(desiredVelocity, acceleration);
		}
		else
		{
			npc.velocity = desiredVelocity;
		}
	}

	public static int CalamityTargeting(this NPC npc, CalamityTargetingParameters options)
	{
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0331: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		if (options == default(CalamityTargetingParameters))
		{
			options = new CalamityTargetingParameters();
		}
		float distance = 0f;
		bool anyTargetAvailable = false;
		int tankMinionProjectileID = -1;
		ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Player p = enumerator.Current;
			if (p.dead || p.ghost)
			{
				continue;
			}
			bool sameTargetAsLastTime = p.whoAmI == npc.oldTarget;
			if (Main.netMode != 0 && (options.excludedPlayers.Contains(p.whoAmI) || ((options.targetType == NPCTargetType.ForceSwitch) & sameTargetAsLastTime)))
			{
				continue;
			}
			Vector2 pCenter = p.Center;
			Vector2 targetCenter = (Vector2)(((_003F?)options.targetingCenter) ?? npc.Center);
			float manhattanDist = Math.Abs(targetCenter.X - pCenter.X) + Math.Abs(targetCenter.Y - pCenter.Y);
			if (manhattanDist > options.maxSearchRange || (options.requireLineOfSight && !Collision.CanHit(npc.Center, 1, 1, pCenter, 1, 1)))
			{
				continue;
			}
			float aggroAdjustedDist = manhattanDist - options.aggroRatio * (float)p.aggro;
			if (options.finishThemOff)
			{
				float missingHPRatio = MathHelper.Clamp(1f - (float)p.statLife / (float)p.statLifeMax2, 0f, 1f);
				float bloodthirstAggro = MathHelper.Lerp(0f, 4000f, missingHPRatio);
				aggroAdjustedDist -= bloodthirstAggro;
			}
			bool aggroDisabled = p.npcTypeNoAggro[npc.type];
			if (aggroDisabled && npc.direction != 0)
			{
				aggroAdjustedDist += 1000f;
			}
			bool cancelTargeting = false;
			bool preferSameFound = (options.targetType == NPCTargetType.PreferSame) & sameTargetAsLastTime;
			bool standardTargetingRequirementsMet = !anyTargetAvailable || aggroAdjustedDist < distance;
			if (preferSameFound | standardTargetingRequirementsMet)
			{
				anyTargetAvailable = true;
				tankMinionProjectileID = -1;
				distance = aggroAdjustedDist;
				npc.target = p.whoAmI;
				if (preferSameFound)
				{
					cancelTargeting = true;
				}
			}
			if (p.tankPet >= 0 && !aggroDisabled && !options.ignoreTankMinions)
			{
				Vector2 tmCenter = Main.projectile[p.tankPet].Center;
				float manhattanDistToTankMinion = Math.Abs(targetCenter.X - tmCenter.X) + Math.Abs(targetCenter.Y - tmCenter.Y);
				manhattanDistToTankMinion -= options.aggroRatio * 200f;
				if (manhattanDistToTankMinion < distance && manhattanDistToTankMinion < 200f && Collision.CanHit(npc.Center, 1, 1, tmCenter, 1, 1))
				{
					tankMinionProjectileID = p.tankPet;
				}
			}
			if (cancelTargeting)
			{
				break;
			}
		}
		if (tankMinionProjectileID >= 0)
		{
			Projectile tankMinion = Main.projectile[tankMinionProjectileID];
			npc.targetRect = tankMinion.Hitbox;
			npc.direction = 1;
			if (tankMinion.Center.X < npc.Center.X)
			{
				npc.direction = -1;
			}
			npc.directionY = 1;
			if (tankMinion.Center.Y < npc.Center.Y)
			{
				npc.directionY = -1;
			}
		}
		else
		{
			bool shouldFaceTarget = options.faceTarget;
			if (npc.target < 0 || npc.target >= 255)
			{
				npc.target = 0;
			}
			Player targetPlayer = Main.player[npc.target];
			npc.targetRect = targetPlayer.Hitbox;
			if (targetPlayer.dead)
			{
				shouldFaceTarget = false;
			}
			if (targetPlayer.npcTypeNoAggro[npc.type] && npc.direction != 0)
			{
				shouldFaceTarget = false;
			}
			if (shouldFaceTarget)
			{
				bool oldTargetWasValid = npc.oldTarget >= 0 && npc.oldTarget < 255;
				bool targetIsLowAggroNotUsingItem = targetPlayer.itemAnimation == 0 && targetPlayer.aggro < 0;
				if (!((!npc.boss && options.ignoreStealthedPlayers) & oldTargetWasValid & targetIsLowAggroNotUsingItem))
				{
					npc.direction = 1;
					if (targetPlayer.Center.X < npc.Center.X)
					{
						npc.direction = -1;
					}
					npc.directionY = 1;
					if (targetPlayer.Center.Y < npc.Center.Y)
					{
						npc.directionY = -1;
					}
				}
			}
		}
		if (npc.confused)
		{
			npc.direction *= -1;
		}
		bool num = npc.direction != npc.oldDirection || npc.directionY != npc.oldDirectionY;
		bool targetChange = npc.target != npc.oldTarget;
		if (((num | targetChange) && !npc.collideX && !npc.collideY) || options.forceNetUpdate)
		{
			npc.netUpdate = true;
		}
		return npc.target;
	}

	public static NPC ClosestNPCAt(this Vector2 origin, float maxDistanceToCheck, bool ignoreTiles = true, bool bossPriority = false)
	{
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		NPC closestTarget = null;
		float distance = maxDistanceToCheck;
		if (bossPriority)
		{
			bool bossFound = false;
			for (int index = 0; index < Main.npc.Length; index++)
			{
				if ((bossFound && !Main.npc[index].boss && Main.npc[index].type != 114) || !Main.npc[index].CanBeChasedBy())
				{
					continue;
				}
				float num = Main.npc[index].width / 2 + Main.npc[index].height / 2;
				bool canHit = true;
				if (num < distance && !ignoreTiles)
				{
					canHit = Collision.CanHit(origin, 1, 1, Main.npc[index].Center, 1, 1);
				}
				if ((Vector2.Distance(origin, Main.npc[index].Center) < distance) & canHit)
				{
					if (Main.npc[index].boss || Main.npc[index].type == 114)
					{
						bossFound = true;
					}
					distance = Vector2.Distance(origin, Main.npc[index].Center);
					closestTarget = Main.npc[index];
				}
			}
		}
		else
		{
			for (int i = 0; i < Main.npc.Length; i++)
			{
				if (Main.npc[i].CanBeChasedBy())
				{
					float num2 = Main.npc[i].width / 2 + Main.npc[i].height / 2;
					bool canHit2 = true;
					if (num2 < distance && !ignoreTiles)
					{
						canHit2 = Collision.CanHit(origin, 1, 1, Main.npc[i].Center, 1, 1);
					}
					if ((Vector2.Distance(origin, Main.npc[i].Center) < distance) & canHit2)
					{
						distance = Vector2.Distance(origin, Main.npc[i].Center);
						closestTarget = Main.npc[i];
					}
				}
			}
		}
		return closestTarget;
	}

	public static NPC ClosestNPCToAngle(this Vector2 origin, Vector2 checkRotationVector, float maxDistanceToCheck, float wantedHalfCone = 0.125f, bool ignoreTiles = true)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		NPC closestTarget = null;
		float distance = maxDistanceToCheck;
		float angle = (float)Math.PI;
		for (int i = 0; i < Main.maxNPCs; i++)
		{
			NPC npc = Main.npc[i];
			if (!npc.CanBeChasedBy())
			{
				continue;
			}
			float checkDist = origin.Distance(npc.Center);
			if (checkDist >= distance)
			{
				continue;
			}
			float angleBetween = checkRotationVector.AngleBetween(npc.Center - origin);
			if (!(angleBetween > angle) && (ignoreTiles || Collision.CanHit(origin, 1, 1, npc.Center, 1, 1)))
			{
				if (angle <= wantedHalfCone)
				{
					angle = wantedHalfCone;
					distance = checkDist;
					closestTarget = npc;
				}
				else
				{
					angle = angleBetween;
					closestTarget = npc;
				}
			}
		}
		return closestTarget;
	}

	public static NPC MinionHoming(this Vector2 origin, float maxDistanceToCheck, Player owner, bool ignoreTiles = true, bool checksRange = false)
	{
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		if (owner == null || !owner.whoAmI.WithinBounds(255) || !owner.MinionAttackTargetNPC.WithinBounds(Main.maxNPCs))
		{
			return origin.ClosestNPCAt(maxDistanceToCheck, ignoreTiles);
		}
		NPC npc = Main.npc[owner.MinionAttackTargetNPC];
		bool canHit = true;
		if (!ignoreTiles)
		{
			canHit = Collision.CanHit(origin, 1, 1, npc.Center, 1, 1);
		}
		float extraDistance = npc.width / 2 + npc.height / 2;
		bool distCheck = Vector2.Distance(origin, npc.Center) < maxDistanceToCheck + extraDistance || !checksRange;
		if (owner.HasMinionAttackTargetNPC & canHit & distCheck)
		{
			return npc;
		}
		return origin.ClosestNPCAt(maxDistanceToCheck, ignoreTiles);
	}

	public static bool Organic(this NPC target)
	{
		if ((target.HitSound != SoundID.NPCHit4 && target.HitSound != SoundID.NPCHit41 && target.HitSound != SoundID.NPCHit2 && target.HitSound != SoundID.NPCHit5 && target.HitSound != SoundID.NPCHit11 && target.HitSound != SoundID.NPCHit30 && target.HitSound != SoundID.NPCHit34 && target.HitSound != SoundID.NPCHit36 && target.HitSound != SoundID.NPCHit42 && target.HitSound != SoundID.NPCHit49 && target.HitSound != SoundID.NPCHit52 && target.HitSound != SoundID.NPCHit53 && target.HitSound != SoundID.NPCHit54 && target.HitSound.HasValue) || target.type == ModContent.NPCType<Providence>() || target.type == ModContent.NPCType<ScornEater>() || target.type == ModContent.NPCType<Yharon>())
		{
			return true;
		}
		return false;
	}

	public static bool CanBeMoved(this NPC target, bool ignoreKBImmune = false)
	{
		if (CalamityPlayer.areThereAnyDamnBosses)
		{
			ignoreKBImmune = false;
		}
		if (target.type != 517 && target.type != 422 && target.type != 507 && target.type != 493 && !target.boss && target.IsAnEnemy(allowStatues: true, checkDead: true, checkDamage: false) && (ignoreKBImmune || target.knockBackResist > 0f))
		{
			return true;
		}
		return false;
	}

	public static void MoveNPC(this NPC target, Vector2 direction, float strength, bool ignoreKBImmune = false)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		if (target.CanBeMoved(ignoreKBImmune))
		{
			Vector2 launchVel = direction.SafeNormalize(Vector2.UnitX) * strength;
			float knockbackMult = Utils.Remap(target.knockBackResist, 0f, 1f, 0.5f, 1f, clamped: false);
			target.velocity = launchVel * ((knockbackMult > 1f) ? ((float)Math.Pow(knockbackMult, 10.0)) : knockbackMult);
			target.SyncMotionToServer();
		}
	}

	public static void StepUpBlocks(this NPC npc)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_029c: Unknown result type (might be due to invalid IL or missing references)
		Vector2 position = npc.position;
		position.X += npc.velocity.X;
		int x = (int)((position.X + (float)(npc.width / 2) + (float)(npc.width / 2 + 1) * (float)npc.direction) / 16f);
		int y = (int)((position.Y + (float)npc.height - 1f) / 16f);
		if ((float)(x * 16) >= position.X + (float)npc.width || (float)(x * 16 + 16) <= position.X)
		{
			return;
		}
		bool num = Main.tile[x, y].HasUnactuatedTile && !Main.tile[x, y].TopSlope && !Main.tile[x, y - 1].TopSlope && Main.tileSolid[Main.tile[x, y].TileType] && !Main.tileSolidTop[Main.tile[x, y].TileType];
		bool aboveTileHalfBlock = Main.tile[x, y - 1].IsHalfBlock && Main.tile[x, y - 1].HasUnactuatedTile;
		bool aboveTileHasRoom = Main.tile[x, y - 1].IsHalfBlock && IsPassableTile(x, y - 4);
		bool aboveTileEmpty = (!Main.tile[x, y - 1].HasUnactuatedTile || !Main.tileSolid[Main.tile[x, y - 1].TileType] || Main.tileSolidTop[Main.tile[x, y - 1].TileType]) | aboveTileHasRoom;
		bool tile3AbovePassable = !Main.tile[x - npc.direction, y - 3].HasUnactuatedTile || !Main.tileSolid[Main.tile[x - npc.direction, y - 3].TileType];
		if (!((((num | aboveTileHalfBlock) & aboveTileEmpty) && IsPassableTile(x, y - 2) && IsPassableTile(x, y - 3)) & tile3AbovePassable))
		{
			return;
		}
		float npcBottom = y * 16;
		if (Main.tile[x, y].IsHalfBlock)
		{
			npcBottom += 8f;
		}
		if (Main.tile[x, y - 1].IsHalfBlock)
		{
			npcBottom -= 8f;
		}
		if (!(npcBottom < position.Y + (float)npc.height))
		{
			return;
		}
		float percentageTileRisen = position.Y + (float)npc.height - npcBottom;
		if (percentageTileRisen <= 16.1f)
		{
			npc.gfxOffY += npc.position.Y + (float)npc.height - npcBottom;
			npc.position.Y = npcBottom - (float)npc.height;
			if (percentageTileRisen < 9f)
			{
				npc.stepSpeed = 1f;
			}
			else
			{
				npc.stepSpeed = 2f;
			}
		}
	}

	public static bool IsPassableTile(int x, int y)
	{
		if (Main.tile[x, y].HasUnactuatedTile && Main.tileSolid[Main.tile[x, y].TileType])
		{
			return Main.tileSolidTop[Main.tile[x, y].TileType];
		}
		return true;
	}

	public static void Inflict246DebuffsNPC(NPC target, int buff, float timeBase = 2f)
	{
		if (Main.rand.NextBool(4))
		{
			target.AddBuff(buff, SecondsToFrames(timeBase * 3f));
		}
		else if (Main.rand.NextBool())
		{
			target.AddBuff(buff, SecondsToFrames(timeBase * 2f));
		}
		else
		{
			target.AddBuff(buff, SecondsToFrames(timeBase));
		}
	}

	public static void DamageEffect(this NPC npc, int damageAmount)
	{
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		Rectangle r = default(Rectangle);
		((Rectangle)(ref r))._002Ector((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height);
		Color textColor = default(Color);
		((Color)(ref textColor))._002Ector(255, 30, 100);
		if (Main.dedServ)
		{
			NetMessage.SendData(81, -1, -1, null, (int)((Color)(ref textColor)).PackedValue, ((Rectangle)(ref r)).Center.X, ((Rectangle)(ref r)).Center.Y, damageAmount);
		}
		else
		{
			CombatText.NewText(r, textColor, damageAmount);
		}
	}

	public static void ProduceGoldCritterDust(this NPC npc)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		npc.position += npc.netOffset;
		Color color = Lighting.GetColor((int)npc.Center.X / 16, (int)npc.Center.Y / 16);
		if (((Color)(ref color)).R > 20 || ((Color)(ref color)).B > 20 || ((Color)(ref color)).G > 20)
		{
			int colorVal = ((Color)(ref color)).R;
			if (((Color)(ref color)).G > colorVal)
			{
				colorVal = ((Color)(ref color)).G;
			}
			if (((Color)(ref color)).B > colorVal)
			{
				colorVal = ((Color)(ref color)).B;
			}
			colorVal /= 30;
			if (Main.rand.Next(300) < colorVal)
			{
				int golddust = Dust.NewDust(npc.position, npc.width, npc.height, 43, 0f, 0f, 254, new Color(255, 255, 0), 0.5f);
				Dust obj = Main.dust[golddust];
				obj.velocity *= 0f;
			}
		}
		npc.position -= npc.netOffset;
	}

	public static void SpawnGores(Entity entity, string suffix, int variants = 1, int startFrom = -1, bool deathCheck = true)
	{
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		bool isADeadNPC = true;
		NPC n = entity as NPC;
		if (((n != null) & deathCheck) && n.life > 0)
		{
			isADeadNPC = false;
		}
		if (!isADeadNPC)
		{
			return;
		}
		for (int i = 0; i < variants; i++)
		{
			int? idx = ((startFrom == -1) ? ((int?)null) : new int?(startFrom));
			if (i > 0)
			{
				idx = ((startFrom == -1) ? new int?(i + 1) : (idx + i + 1));
			}
			IEntitySource source_Death = entity.GetSource_Death();
			Vector2 position = entity.position;
			Vector2 velocity = entity.velocity;
			CalamityMod instance = CalamityMod.Instance;
			int? num = idx;
			Gore.NewGore(source_Death, position, velocity, instance.Find<ModGore>(suffix + num).Type);
		}
	}

	public static NPCShop AddWithCustomValue(this NPCShop shop, int itemType, int customValue, params Condition[] conditions)
	{
		Item item = new Item(itemType)
		{
			shopCustomPrice = customValue
		};
		return shop.Add(item, conditions);
	}

	public static NPCShop AddWithCustomValue<T>(this NPCShop shop, int customValue, params Condition[] conditions) where T : ModItem
	{
		return shop.AddWithCustomValue(ModContent.ItemType<T>(), customValue, conditions);
	}

	public static void DrawBackglow(this NPC npc, Color backglowColor, float backglowArea, SpriteEffects spriteEffects, Rectangle frame, Vector2 screenPos, Texture2D overrideTexture = null)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ((overrideTexture == null) ? TextureAssets.Npc[npc.type].Value : overrideTexture);
		Vector2 drawPosition = npc.Center - screenPos;
		Vector2 origin = frame.Size() * 0.5f;
		Color backAfterimageColor = backglowColor * npc.Opacity;
		for (int i = 0; i < 10; i++)
		{
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 10f).ToRotationVector2() * backglowArea;
			Main.spriteBatch.Draw(texture, drawPosition + drawOffset, (Rectangle?)frame, backAfterimageColor, npc.rotation, origin, npc.scale, spriteEffects, 0f);
		}
	}

	public static bool DrawAnimatedBestiaryWorm(SpriteBatch spriteBatch, NPC npc, Color drawColor, Texture2D headTexture, Texture2D bodyTexture, int segmentCount, int segmentSpacing, float rotationStrength, Vector2 baseOffset, int animationSpeed, float range, float headOffset = 0f, float headSpeedOffset = 0f, bool flip = false)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		DrawAnimatedBestiaryWorm(spriteBatch, npc, drawColor, headTexture, (Texture2D[])(object)new Texture2D[1] { bodyTexture }, segmentCount, segmentSpacing, rotationStrength, baseOffset, animationSpeed, range, headOffset, headSpeedOffset, flip);
		return false;
	}

	public static bool DrawAnimatedBestiaryWorm(SpriteBatch spriteBatch, NPC npc, Color drawColor, Texture2D headTexture, Texture2D bodyTexture, Texture2D bodyTextureAlt, int segmentCount, int segmentSpacing, float rotationStrength, Vector2 baseOffset, int animationSpeed, float range, float headOffset = 0f, float headSpeedOffset = 0f, bool flip = false)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		DrawAnimatedBestiaryWorm(spriteBatch, npc, drawColor, headTexture, (Texture2D[])(object)new Texture2D[2] { bodyTexture, bodyTextureAlt }, segmentCount, segmentSpacing, rotationStrength, baseOffset, animationSpeed, range, headOffset, headSpeedOffset, flip);
		return false;
	}

	public static bool DrawAnimatedBestiaryWorm(SpriteBatch spriteBatch, NPC npc, Color drawColor, Texture2D headTexture, Texture2D[] bodyTextures, int segmentCount, int segmentSpacing, float rotationStrength, Vector2 baseOffset, int animationSpeed, float range, float headOffset = 0f, float headSpeedOffset = 0f, bool flip = false)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		npc.frame = headTexture.Frame();
		float offset = -0.2f;
		float startX = baseOffset.X;
		float startY = baseOffset.Y;
		SpriteEffects fx = (SpriteEffects)(flip ? 1 : 0);
		float wormTimer = npc.Calamity().bestiaryWormTimer;
		for (int i = segmentCount; i > 0; i--)
		{
			float bodyOffset = ((i == 1) ? ((float)(i * segmentSpacing) * 0.4f) : ((float)(i * segmentSpacing) - (float)segmentSpacing * 0.5f));
			Texture2D toUse = ((bodyTextures.Length == 1) ? bodyTextures[0] : ((bodyTextures.Length == 2) ? ((i % 2 == 0) ? bodyTextures[0] : bodyTextures[1]) : bodyTextures[i - 1]));
			spriteBatch.Draw(toUse, npc.position + new Vector2(startX + bodyOffset, MathF.Sin((wormTimer + offset * (float)i) * (float)animationSpeed) * range + startY), (Rectangle?)toUse.Frame(), npc.GetAlpha(drawColor), npc.rotation - (float)Math.PI / 2f - MathF.Cos((wormTimer + offset * (float)i) * (float)animationSpeed) * ((float)Math.PI / 4f) * rotationStrength, toUse.Size() / 2f, npc.scale, fx, 0f);
		}
		spriteBatch.Draw(headTexture, npc.position + new Vector2(startX + headOffset, MathF.Sin((wormTimer - headSpeedOffset) * (float)animationSpeed) * range + startY), (Rectangle?)npc.frame, npc.GetAlpha(drawColor), npc.rotation - (float)Math.PI / 2f - MathF.Cos((wormTimer - headSpeedOffset) * (float)animationSpeed) * ((float)Math.PI / 4f) * rotationStrength, new Vector2((float)headTexture.Width * 0.5f, (float)headTexture.Height), npc.scale, fx, 0f);
		return false;
	}

	public static bool HasSight(this NPC npc, Vector2 target)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		return Collision.CanHit(npc.Center, 1, 1, target, 1, 1);
	}

	public static Vector2? NPCTileDetection(NPC npc, int tileType, float radius, bool usesSunkenSeaValidity = false)
	{
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		Vector2? tileFoundPosition = null;
		int? tileIndexFound = null;
		for (int i = 0; i < 360; i += 15)
		{
			if (tileFoundPosition.HasValue)
			{
				break;
			}
			List<Point> points = GetIntersectingPointsInLine(npc.Center, npc.Center - Vector2.UnitY.RotatedBy(MathHelper.ToRadians((float)i)) * radius);
			for (int j = points.Count - 1; j >= 0; j--)
			{
				if (Main.tile[points[j]].TileType == tileType)
				{
					if (usesSunkenSeaValidity)
					{
						tileIndexFound = j;
					}
					else
					{
						tileFoundPosition = points[j].ToWorldCoordinates();
					}
					break;
				}
			}
			if (!usesSunkenSeaValidity || !tileIndexFound.HasValue)
			{
				continue;
			}
			for (int k = tileIndexFound.Value; k >= 0; k--)
			{
				Vector2 worldPos = points[k].ToWorldCoordinates();
				if (npc.HasSight(worldPos) && SunkenSeaNPC.SunkenSeaTileValidity(npc, points[k]))
				{
					tileFoundPosition = worldPos;
					break;
				}
			}
			tileIndexFound = null;
		}
		if (tileFoundPosition.HasValue)
		{
			return tileFoundPosition.Value;
		}
		return null;
	}

	public static void BossAwakenMessage(int npcIndex)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		string typeName = Main.npc[npcIndex].TypeName;
		if (Main.netMode == 0)
		{
			Main.NewText((object)Language.GetTextValue("Announcement.HasAwoken", typeName), (Color?)new Color(175, 75, 255));
		}
		else if (Main.dedServ)
		{
			ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", Main.npc[npcIndex].GetTypeNetName()), new Color(175, 75, 255));
		}
	}

	public static NPC SpawnBossBetter(Vector2 relativeSpawnPosition, int bossType, BaseBossSpawnContext spawnContext = null, float ai0 = 0f, float ai1 = 0f, float ai2 = 0f, float ai3 = 0f)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if (Main.netMode == 1)
		{
			return null;
		}
		if (spawnContext == null)
		{
			spawnContext = new ExactPositionBossSpawnContext();
		}
		Vector2 spawnPosition = spawnContext.DetermineSpawnPosition(relativeSpawnPosition);
		int bossIndex = NPC.NewNPC(NPC.GetBossSpawnSource(Player.FindClosest(spawnPosition, 1, 1)), (int)spawnPosition.X, (int)spawnPosition.Y, bossType, 0, ai0, ai1, ai2, ai3);
		if (Main.npc.IndexInRange(bossIndex))
		{
			BossAwakenMessage(bossIndex);
			return Main.npc[bossIndex];
		}
		return null;
	}

	public static void SpawnBossUsingItem(Player player, int npcType, in SoundStyle? spawnSound = null)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(spawnSound, (Vector2?)player.Center);
		if (player.whoAmI == Main.myPlayer)
		{
			switch (Main.netMode)
			{
			case 0:
				NPC.SpawnOnPlayer(player.whoAmI, npcType);
				break;
			case 1:
				NetMessage.SendData(61, -1, -1, null, player.whoAmI, npcType);
				break;
			}
		}
	}

	public static void SpawnBossUsingItem<BossType>(Player player, in SoundStyle? spawnSound = null) where BossType : ModNPC
	{
		SpawnBossUsingItem(player, ModContent.NPCType<BossType>(), in spawnSound);
	}

	public static NPC SpawnBossOnPosUsingItem(Player player, int npcType, int worldX, int worldY, in SoundStyle? spawnSound = null)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(spawnSound, (Vector2?)player.Center);
		if (player.whoAmI != Main.myPlayer)
		{
			return null;
		}
		if (Main.netMode == 0)
		{
			int spawnedNPCIdx = NPC.NewNPC(new EntitySource_BossSpawn(player), worldX, worldY, npcType, 1);
			if (spawnedNPCIdx >= 200)
			{
				return null;
			}
			BossAwakenMessage(spawnedNPCIdx);
			NPC obj = Main.npc[spawnedNPCIdx];
			obj.timeLeft *= 20;
			return obj;
		}
		SpawnBossOnPositionPacket.Send(worldX, worldY, npcType, player);
		return null;
	}

	public static NPC SpawnBossOnPosUsingItem<BossType>(Player player, int worldX, int worldY, in SoundStyle? spawnSound = null) where BossType : ModNPC
	{
		return SpawnBossOnPosUsingItem(player, ModContent.NPCType<BossType>(), worldX, worldY, in spawnSound);
	}

	internal static void SpawnOldDuke(int playerIndex)
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			return;
		}
		Player player = Main.player[playerIndex];
		if (!player.active || player.dead)
		{
			return;
		}
		Projectile projectile = null;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.bobber && p.owner == playerIndex)
			{
				projectile = p;
				break;
			}
		}
		if (projectile != null)
		{
			BossAwakenMessage(NPC.NewNPC(NPC.GetBossSpawnSource(playerIndex), (int)projectile.Center.X, (int)projectile.Center.Y + 100, ModContent.NPCType<OldDuke>()));
		}
	}

	public static float TaxicabDistance(Point a, Point b)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return MathF.Abs(a.X - b.X) + MathF.Abs(a.Y - b.Y);
	}

	public static float EuclideanDistance(Point a, Point b)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		float num = MathF.Abs(a.X - b.X);
		float dy = MathF.Abs(a.Y - b.Y);
		return MathF.Sqrt(num * num + dy * dy);
	}

	public static float ChebyshevDistance(Point a, Point b)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return MathF.Max(MathF.Abs(a.X - b.X), MathF.Abs(a.Y - b.Y));
	}

	public static float OctileDistance(Point a, Point b)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		float dx = MathF.Abs(a.X - b.X);
		float dy = MathF.Abs(a.Y - b.Y);
		return dx + dy - 0.586f * MathF.Min(dx, dy);
	}

	public static bool DoesEntityFitInPath(this Entity entity, Point point, int fluffX = 0, int fluffY = 0)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		Rectangle hitbox = entity.Hitbox;
		Vector2 worldCoordinatePoint = point.ToWorldCoordinates();
		((Rectangle)(ref hitbox)).Inflate(fluffX, fluffY);
		bool doesFit = true;
		for (int coordX = (int)(worldCoordinatePoint.X - (float)(hitbox.Width / 2)); (float)coordX < worldCoordinatePoint.X + (float)(hitbox.Width / 2); coordX++)
		{
			for (int coordY = (int)(worldCoordinatePoint.Y - (float)(hitbox.Height / 2)); (float)coordY < worldCoordinatePoint.Y + (float)(hitbox.Height / 2); coordY++)
			{
				Point p = Utils.ToTileCoordinates(new Vector2((float)coordX, (float)coordY));
				if (Main.tile[p].IsTileSolid())
				{
					doesFit = false;
					break;
				}
			}
		}
		return doesFit;
	}

	public static List<Point> GetIntersectingHitboxPoints(this Entity entity, Point position, int fluffX = 0, int fluffY = 0)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		Rectangle hitbox = entity.Hitbox;
		((Rectangle)(ref hitbox)).Location = new Point(position.X - hitbox.Width / 2, position.Y - hitbox.Height / 2);
		((Rectangle)(ref hitbox)).Inflate(fluffX, fluffY);
		int num = (int)MathF.Floor(((Rectangle)(ref hitbox)).Left / 16);
		int endX = (int)Math.Floor(((float)((Rectangle)(ref hitbox)).Right - float.Epsilon) / 16f);
		int startY = (int)MathF.Floor(((Rectangle)(ref hitbox)).Top / 16);
		int endY = (int)Math.Floor(((float)((Rectangle)(ref hitbox)).Bottom - float.Epsilon) / 16f);
		List<Point> intersectingPoints = new List<Point>();
		for (int i = num; i <= endX; i++)
		{
			for (int j = startY; j <= endY; j++)
			{
				intersectingPoints.Add(new Point(i, j));
			}
		}
		return intersectingPoints;
	}

	public static int GetCurrentDefense(this Player player, bool accountForDefenseDamage = false)
	{
		CalamityPlayer mp = player.Calamity();
		return player.statDefense + ((!accountForDefenseDamage) ? mp.CurrentDefenseDamage : 0);
	}

	public static int GetDefenseDamageFloor()
	{
		if (BossRushEvent.BossRushActive)
		{
			return BalancingConstants.DefenseDamageFloor_BossRush;
		}
		if (NPC.downedMoonlord)
		{
			if (!CalamityWorld.death)
			{
				if (!CalamityWorld.revenge)
				{
					return BalancingConstants.DefenseDamageFloor_NormalPML;
				}
				return BalancingConstants.DefenseDamageFloor_RevPML;
			}
			return BalancingConstants.DefenseDamageFloor_DeathPML;
		}
		if (Main.hardMode)
		{
			if (!CalamityWorld.death)
			{
				if (!CalamityWorld.revenge)
				{
					return BalancingConstants.DefenseDamageFloor_NormalHM;
				}
				return BalancingConstants.DefenseDamageFloor_RevHM;
			}
			return BalancingConstants.DefenseDamageFloor_DeathHM;
		}
		if (!CalamityWorld.death)
		{
			if (!CalamityWorld.revenge)
			{
				return BalancingConstants.DefenseDamageFloor_NormalPHM;
			}
			return BalancingConstants.DefenseDamageFloor_RevPHM;
		}
		return BalancingConstants.DefenseDamageFloor_DeathPHM;
	}

	public static float CalcDamage<T>(this Player player, float baseDamage) where T : DamageClass
	{
		return player.GetTotalDamage<T>().ApplyTo(baseDamage);
	}

	public static int CalcIntDamage<T>(this Player player, float baseDamage) where T : DamageClass
	{
		return (int)player.CalcDamage<T>(baseDamage);
	}

	public static DamageClass GetBestClass(this Player player)
	{
		float bestDamage = 1f;
		DamageClass bestClass = DamageClass.Generic;
		float melee = player.GetTotalDamage<MeleeDamageClass>().Additive;
		if (melee > bestDamage)
		{
			bestDamage = melee;
			bestClass = DamageClass.Melee;
		}
		float ranged = player.GetTotalDamage<RangedDamageClass>().Additive;
		if (ranged > bestDamage)
		{
			bestDamage = ranged;
			bestClass = DamageClass.Ranged;
		}
		float magic = player.GetTotalDamage<MagicDamageClass>().Additive;
		if (magic > bestDamage)
		{
			bestDamage = magic;
			bestClass = DamageClass.Magic;
		}
		float summon = player.GetTotalDamage<SummonDamageClass>().Additive * BalancingConstants.SummonAllClassScalingFactor;
		if (summon > bestDamage)
		{
			bestDamage = summon;
			bestClass = DamageClass.Summon;
		}
		if (player.GetTotalDamage<RogueDamageClass>().Additive - player.Calamity().stealthDamage > bestDamage)
		{
			bestClass = RogueDamageClass.Instance;
		}
		return bestClass;
	}

	public static StatModifier GetBestClassDamage(this Player player)
	{
		StatModifier ret = StatModifier.Default;
		StatModifier classless = player.GetTotalDamage<GenericDamageClass>();
		ret.Base = classless.Base;
		ret *= classless.Multiplicative;
		ret.Flat = classless.Flat;
		float best = 1f;
		float melee = player.GetTotalDamage<MeleeDamageClass>().Additive;
		if (melee > best)
		{
			best = melee;
		}
		float ranged = player.GetTotalDamage<RangedDamageClass>().Additive;
		if (ranged > best)
		{
			best = ranged;
		}
		float magic = player.GetTotalDamage<MagicDamageClass>().Additive;
		if (magic > best)
		{
			best = magic;
		}
		float summon = player.GetTotalDamage<SummonDamageClass>().Additive * BalancingConstants.SummonAllClassScalingFactor;
		if (summon > best)
		{
			best = summon;
		}
		float rogue = player.GetTotalDamage<RogueDamageClass>().Additive - player.Calamity().stealthDamage;
		if (rogue > best)
		{
			best = rogue;
		}
		return ret + (best - 1f);
	}

	public static float GetAmmoCostReduction(this Player player)
	{
		float vanillaCost = (player.ammoBox ? 0.8f : 1f);
		if (player.ammoPotion)
		{
			vanillaCost *= 0.8f;
		}
		if (player.ammoCost80)
		{
			vanillaCost *= 0.8f;
		}
		if (player.ammoCost75)
		{
			vanillaCost *= 0.75f;
		}
		return vanillaCost * player.Calamity().ammoCost;
	}

	public static float GetStandingStealthRegen(this Player player)
	{
		CalamityPlayer mp = player.Calamity();
		return mp.rogueStealthMax / BalancingConstants.BaseStealthGenTime * mp.stealthGenStandstill;
	}

	public static float GetMovingStealthRegen(this Player player)
	{
		CalamityPlayer mp = player.Calamity();
		return mp.rogueStealthMax / BalancingConstants.BaseStealthGenTime * BalancingConstants.MovingStealthGenRatio * mp.stealthGenMoving * mp.stealthAcceleration;
	}

	public static float GetJumpBoost(this Player player)
	{
		return player.jumpSpeedBoost + (player.wereWolf ? 0.2f : 0f) + (player.jumpBoost ? 0.75f : 0f);
	}

	public static void SetAbyssLightLevels(this Player player)
	{
		CalamityPlayer mp = player.Calamity();
		bool underwater = player.IsUnderwater();
		bool num = player.head == 11 || player.head == 216;
		if (mp.camper)
		{
			mp.abyssPlayerGlowMultiplier += Utils.Remap(((Vector2)(ref player.velocity)).Length(), 0f, 5f, 1f, 0f);
		}
		if (num)
		{
			mp.abyssPlayerGlowMultiplier += 0.2f;
		}
		if (player.nightVision)
		{
			mp.abyssDarkness -= 0.4f;
		}
		if (mp.giantPearl)
		{
			mp.abyssPlayerGlowMultiplier += 0.2f;
		}
		if (mp.fathomSwarmerVisage)
		{
			mp.abyssDarkness -= 0.2f;
			mp.abyssPlayerGlowMultiplier += 0.2f;
			mp.abyssFlashlightWidthMultiplier += 0.5f;
		}
		if (mp.aquaticHeart)
		{
			mp.abyssDarkness -= 0.1f;
		}
		if (mp.jellyfishNecklace & underwater)
		{
			mp.abyssPlayerGlowMultiplier += 0.2f;
		}
		if (mp.reaverExplore)
		{
			mp.abyssDarkness -= 0.2f;
			mp.abyssPlayerGlowMultiplier += 0.2f;
			mp.abyssFlashlightWidthMultiplier += 0.5f;
		}
		if (mp.shine)
		{
			mp.abyssPlayerGlowMultiplier += 0.2f;
		}
		if (mp.babyGhostBell & underwater)
		{
			mp.abyssDarkness -= 0.1f;
		}
		if (mp.sirenPet & underwater)
		{
			mp.abyssDarkness -= 0.2f;
		}
		if (mp.littleLightPet)
		{
			mp.abyssDarkness -= 0.4f;
		}
	}

	internal static Item GetBestPick(this Player player)
	{
		int bestPickPower = 0;
		Item bestPick = null;
		for (int item = 0; item < 58; item++)
		{
			if (player.inventory[item].pick > 0 && player.inventory[item].pick > bestPickPower)
			{
				bestPick = player.inventory[item];
				bestPickPower = bestPick.pick;
			}
		}
		return bestPick;
	}

	public static int GetBestPickPower(this Player player)
	{
		return player.GetBestPick()?.pick ?? _minimumPickPower;
	}

	public static bool ControlsEnabled(this Player player, bool allowWoFTongue = false)
	{
		if (player.CCed)
		{
			return false;
		}
		if (player.tongued && !allowWoFTongue)
		{
			return false;
		}
		return true;
	}

	public static bool StandingStill(this Player player, float velocity = 0.05f)
	{
		return ((Vector2)(ref player.velocity)).Length() < velocity;
	}

	public static bool CheckSolidGround(this Player player, int solidGroundAhead = 0, int airExposureNeeded = 0)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		if (player.velocity.Y != 0f)
		{
			return false;
		}
		bool ConditionMet = true;
		int playerCenterX = (int)player.Center.X / 16;
		int playerCenterY = (int)(player.position.Y + (float)player.height - 1f) / 16 + 1;
		for (int i = 0; i <= solidGroundAhead; i++)
		{
			ConditionMet = Main.tile[playerCenterX + player.direction * i, playerCenterY].IsTileSolidGround();
			if (!ConditionMet)
			{
				return ConditionMet;
			}
			for (int j = 1; j <= airExposureNeeded; j++)
			{
				Tile checkedTile = Main.tile[playerCenterX + player.direction * i, playerCenterY - j];
				ConditionMet = !(checkedTile != null) || !checkedTile.HasUnactuatedTile || !Main.tileSolid[checkedTile.TileType];
				if (!ConditionMet)
				{
					return ConditionMet;
				}
			}
		}
		return ConditionMet;
	}

	public static void DisableWingFlapSound(this Player player)
	{
		player.flapSound = true;
	}

	public static bool IsUnderwater(this Player player)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		return Collision.DrownCollision(player.position, player.width, player.height, player.gravDir);
	}

	public static bool ReducedSpaceGravity(this Player player)
	{
		float x = (float)Main.maxTilesX / 4200f;
		x *= x;
		return (float)((double)(player.position.Y / 16f - (60f + 10f * x)) / (Main.worldSurface / (Main.remixWorld ? 1.0 : 6.0))) < 1f;
	}

	public static bool PillarZone(this Player player)
	{
		if (!player.ZoneTowerStardust && !player.ZoneTowerSolar && !player.ZoneTowerVortex)
		{
			return player.ZoneTowerNebula;
		}
		return true;
	}

	public static bool InCalamity(this Player player)
	{
		return player.Calamity().ZoneCalamity;
	}

	public static bool InSunkenSea(this Player player)
	{
		return player.Calamity().ZoneSunkenSea;
	}

	public static bool InSulphur(this Player player)
	{
		return player.Calamity().ZoneSulphur;
	}

	public static bool InAstral(this Player player, int biome = 0)
	{
		switch (biome)
		{
		case 1:
			if (player.Calamity().ZoneAstral)
			{
				if (!player.ZoneOverworldHeight)
				{
					return player.ZoneSkyHeight;
				}
				return true;
			}
			return false;
		case 2:
			if (player.Calamity().ZoneAstral)
			{
				if (!player.ZoneDirtLayerHeight && !player.ZoneRockLayerHeight)
				{
					return player.ZoneUnderworldHeight;
				}
				return true;
			}
			return false;
		case 3:
			if (player.Calamity().ZoneAstral)
			{
				return player.ZoneDesert;
			}
			return false;
		default:
			return player.Calamity().ZoneAstral;
		}
	}

	public static bool InAbyss(this Player player, int layer = 0)
	{
		return layer switch
		{
			1 => player.Calamity().ZoneAbyssLayer1, 
			2 => player.Calamity().ZoneAbyssLayer2, 
			3 => player.Calamity().ZoneAbyssLayer3, 
			4 => player.Calamity().ZoneAbyssLayer4, 
			_ => player.Calamity().ZoneAbyss, 
		};
	}

	public static bool HoldingProjectileMeleeWeapon(this Player player)
	{
		Item item = player.HeldItem;
		if (item.CountsAsClass<MeleeDamageClass>())
		{
			return item.shoot != 0;
		}
		return false;
	}

	public static bool HoldingTrueMeleeWeapon(this Player player)
	{
		return player.HeldItem.IsTrueMelee();
	}

	public static bool InventoryHas(this Player player, params int[] items)
	{
		return player.inventory.Any((Item item) => items.Contains(item.type));
	}

	public static bool PortableStorageHas(this Player player, params int[] items)
	{
		bool hasItem = false;
		if (player.bank.item.Any((Item item) => items.Contains(item.type)))
		{
			hasItem = true;
		}
		if (player.bank2.item.Any((Item item) => items.Contains(item.type)))
		{
			hasItem = true;
		}
		if (player.bank3.item.Any((Item item) => items.Contains(item.type)))
		{
			hasItem = true;
		}
		if (player.bank4.item.Any((Item item) => items.Contains(item.type)))
		{
			hasItem = true;
		}
		return hasItem;
	}

	public static int ComputeHitIFrames(this Player player, Player.HurtInfo hurtInfo)
	{
		int num = 40 + (player.longInvince ? 40 : 0);
		int calBonusIFrames = player.GetExtraHitIFrames(hurtInfo);
		return num + calBonusIFrames;
	}

	public static int GetExtraHitIFrames(this Player player, Player.HurtInfo hurtInfo)
	{
		CalamityPlayer calamityPlayer = player.Calamity();
		int extraIFrames = 0;
		if (calamityPlayer.godSlayerThrowing && hurtInfo.Damage > GodSlayerHeadRogue.SetBonusHurtDamageThreshold)
		{
			extraIFrames += GodSlayerHeadRogue.ExtraIFrames;
		}
		if (calamityPlayer.dAmulet)
		{
			if (hurtInfo.Damage > 1)
			{
				float lifeRatio = (float)player.statLife / (float)player.statLifeMax2;
				float iframeEffectivenessRatio = Utils.GetLerpValue(1f, 0.25f, lifeRatio, clamped: true);
				extraIFrames += (int)(iframeEffectivenessRatio * (float)DeificAmulet.MaxBonusIFrames);
			}
			else
			{
				extraIFrames += 5;
			}
		}
		if (calamityPlayer.rampartOfDeities && hurtInfo.Damage > 200)
		{
			extraIFrames += 30;
		}
		return extraIFrames;
	}

	public static int ComputeDodgeIFrames(this Player player)
	{
		return 80 + (player.longInvince ? 40 : 0);
	}

	public static int ComputeParryIFrames(this Player player)
	{
		return 60 + (player.longInvince ? 30 : 0);
	}

	public static int ComputeReflectIFrames(this Player player)
	{
		return player.ComputeDodgeIFrames();
	}

	public static bool HasIFrames(this Player player)
	{
		if (player.immune || player.immuneTime > 0)
		{
			return true;
		}
		for (int i = 0; i < player.hurtCooldowns.Length; i++)
		{
			if (player.hurtCooldowns[i] > 0)
			{
				return true;
			}
		}
		return false;
	}

	public static bool GiveIFrames(this Player player, int cooldownSlot, int frames, bool blink = false)
	{
		if (!((cooldownSlot < 0) ? (player.immuneTime < frames) : (player.hurtCooldowns[cooldownSlot] < frames)))
		{
			return false;
		}
		player.immune = true;
		player.immuneNoBlink = !blink;
		if (cooldownSlot < 0)
		{
			player.immuneTime = frames;
		}
		else
		{
			player.hurtCooldowns[cooldownSlot] = frames;
		}
		return true;
	}

	public static bool GiveUniversalIFrames(this Player player, int frames, bool blink = false)
	{
		bool anyIFramesWouldBeGiven = false;
		for (int i = 0; i < player.hurtCooldowns.Length; i++)
		{
			if (player.hurtCooldowns[i] < frames)
			{
				anyIFramesWouldBeGiven = true;
			}
		}
		if (!anyIFramesWouldBeGiven)
		{
			return false;
		}
		player.immune = true;
		player.immuneNoBlink = !blink;
		player.immuneTime = frames;
		for (int j = 0; j < player.hurtCooldowns.Length; j++)
		{
			if (player.hurtCooldowns[j] < frames)
			{
				player.hurtCooldowns[j] = frames;
			}
		}
		return true;
	}

	public static void RemoveAllIFrames(this Player player)
	{
		player.immune = false;
		player.immuneNoBlink = false;
		player.immuneTime = 0;
		for (int i = 0; i < player.hurtCooldowns.Length; i++)
		{
			player.hurtCooldowns[i] = 0;
		}
	}

	public static void NullifyHit(this ref Player.HurtInfo hurtInfo)
	{
		hurtInfo._damage = 0;
		hurtInfo.Knockback = 0f;
	}

	public static void DoLifestealDirect(this Player player, NPC target, int amount, float cooldownMultiplier = 1f)
	{
		if (target == null || (target.IsAnEnemy(allowStatues: false) && target.canGhostHeal))
		{
			amount = Math.Min(amount, player.statLifeMax2 - player.statLife);
			amount = Math.Min(amount, BalancingConstants.LifeStealCap);
			if (amount > 0 && !(player.lifeSteal <= 0f) && !player.moonLeech)
			{
				player.lifeSteal -= (float)amount * cooldownMultiplier;
				player.HealPlayer(amount);
			}
		}
	}

	public static void SpawnLifeStealProjectile(this Player player, NPC target, Projectile projSource, int projType, int amount, float cooldownMultiplier = 1f, bool shared = false, float distanceRequired = 3000f)
	{
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		if (target != null && (!target.IsAnEnemy(allowStatues: false) || !target.canGhostHeal))
		{
			return;
		}
		int lowestHealthCheck = player.statLifeMax2 - player.statLife;
		int targetPlayer = player.whoAmI;
		if (shared)
		{
			ActiveEntityIterator<Player>.Enumerator enumerator = Main.ActivePlayers.GetEnumerator();
			while (enumerator.MoveNext())
			{
				Player otherPlayer = enumerator.Current;
				if (!otherPlayer.dead && ((!player.hostile && !otherPlayer.hostile) || player.team == otherPlayer.team) && Vector2.Distance(projSource.Center, otherPlayer.Center) < distanceRequired && otherPlayer.statLifeMax2 - otherPlayer.statLife > lowestHealthCheck)
				{
					lowestHealthCheck = otherPlayer.statLifeMax2 - otherPlayer.statLife;
					targetPlayer = otherPlayer.whoAmI;
				}
			}
		}
		amount = Math.Min(amount, lowestHealthCheck);
		amount = Math.Min(amount, BalancingConstants.LifeStealCap);
		if (amount > 0 && !(player.lifeSteal <= 0f) && !player.moonLeech)
		{
			player.lifeSteal -= (float)amount * cooldownMultiplier;
			if (projSource.owner == Main.myPlayer)
			{
				Projectile.NewProjectile(projSource.GetSource_FromThis(), projSource.Center, Vector2.Zero, projType, 0, 0f, projSource.owner, targetPlayer, amount);
			}
		}
	}

	public static void HealPlayer(this Player player, int amount, HealTextType healTextType = HealTextType.Broadcast)
	{
		player.statLife += amount;
		if (player.statLife > player.statLifeMax2)
		{
			player.statLife = player.statLifeMax2;
		}
		if (healTextType != HealTextType.None)
		{
			player.HealEffect(amount, healTextType == HealTextType.Broadcast);
		}
	}

	public static float GetAdrenalineDamage(this CalamityPlayer mp)
	{
		float adrenalineBoost = BalancingConstants.AdrenalineDamageBoost;
		if (mp.adrenalineBoostOne)
		{
			adrenalineBoost += BalancingConstants.AdrenalineDamagePerBooster;
		}
		if (mp.adrenalineBoostTwo)
		{
			adrenalineBoost += BalancingConstants.AdrenalineDamagePerBooster;
		}
		if (mp.adrenalineBoostThree)
		{
			adrenalineBoost += BalancingConstants.AdrenalineDamagePerBooster;
		}
		return adrenalineBoost;
	}

	public static float GetAdrenalineDR(this CalamityPlayer mp)
	{
		float dr = BalancingConstants.FullAdrenalineDR;
		if (mp.adrenalineBoostOne)
		{
			dr += BalancingConstants.AdrenalineDRPerBooster;
		}
		if (mp.adrenalineBoostTwo)
		{
			dr += BalancingConstants.AdrenalineDRPerBooster;
		}
		if (mp.adrenalineBoostThree)
		{
			dr += BalancingConstants.AdrenalineDRPerBooster;
		}
		return dr;
	}

	public static void ApplyRippersToDamage(CalamityPlayer mp, bool trueMelee, ref float damageMult)
	{
		if (mp.rageModeActive)
		{
			damageMult += (trueMelee ? (mp.RageDamageBoost * BalancingConstants.TrueMeleeRipperReductionFactor) : mp.RageDamageBoost);
		}
		if (mp.adrenalineModeActive && !mp.draedonsHeart)
		{
			damageMult += (trueMelee ? (mp.GetAdrenalineDamage() * BalancingConstants.TrueMeleeRipperReductionFactor) : mp.GetAdrenalineDamage());
		}
	}

	public static bool HasCooldown(this Player p, string id)
	{
		if (p == null)
		{
			return false;
		}
		return p.Calamity()?.cooldowns.ContainsKey(id) ?? false;
	}

	public static CooldownInstance AddCooldown(this Player p, string id, int duration, bool overwrite = true)
	{
		Cooldown cd = CooldownRegistry.Get(id);
		CooldownInstance instance = new CooldownInstance(p, cd, duration);
		if (!p.HasCooldown(id) | overwrite)
		{
			CalamityPlayer calamityPlayer = p.Calamity();
			calamityPlayer.cooldowns[id] = instance;
			calamityPlayer.SyncCooldownAddition(Main.dedServ, instance);
		}
		return instance;
	}

	public static CooldownInstance AddCooldown(this Player p, string id, int duration, bool overwrite = true, params object[] handlerArgs)
	{
		Cooldown cd = CooldownRegistry.Get(id);
		CooldownInstance instance = new CooldownInstance(p, cd, duration, handlerArgs);
		if (!p.HasCooldown(id) | overwrite)
		{
			p.Calamity().cooldowns[id] = instance;
		}
		return instance;
	}

	public static IList<CooldownInstance> GetDisplayedCooldowns(this Player p)
	{
		List<CooldownInstance> ret = new List<CooldownInstance>(16);
		if (p == null || p.Calamity() == null)
		{
			return ret;
		}
		foreach (CooldownInstance instance in p.Calamity().cooldowns.Values)
		{
			if (instance.handler.ShouldDisplay)
			{
				ret.Add(instance);
			}
		}
		return ret;
	}

	public static Player.CompositeArmStretchAmount ToStretchAmount(this float percent)
	{
		if (percent < 0.25f)
		{
			return Player.CompositeArmStretchAmount.None;
		}
		if (percent < 0.5f)
		{
			return Player.CompositeArmStretchAmount.Quarter;
		}
		if (percent < 0.75f)
		{
			return Player.CompositeArmStretchAmount.ThreeQuarters;
		}
		return Player.CompositeArmStretchAmount.Full;
	}

	public static Vector2 GetFrontHandPositionImproved(this Player player, Player.CompositeArmData arm)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		Vector2 position = player.GetFrontHandPosition(arm.stretch, arm.rotation * player.gravDir).Floor();
		if (player.gravDir == -1f)
		{
			position.Y = player.position.Y + (float)player.height + (player.position.Y - position.Y);
		}
		return position;
	}

	public static Vector2 GetBackHandPositionImproved(this Player player, Player.CompositeArmData arm)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		Vector2 position = player.GetBackHandPosition(arm.stretch, arm.rotation * player.gravDir).Floor();
		if (player.gravDir == -1f)
		{
			position.Y = player.position.Y + (float)player.height + (player.position.Y - position.Y);
		}
		return position;
	}

	public static void CleanHoldStyle(Player player, float desiredRotation, Vector2 desiredPosition, Vector2 spriteSize, Vector2? rotationOriginFromCenter = null, bool noSandstorm = false, bool flipAngle = false, bool stepDisplace = true)
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		if (noSandstorm)
		{
			player.sandStorm = false;
		}
		if (!rotationOriginFromCenter.HasValue)
		{
			rotationOriginFromCenter = Vector2.Zero;
		}
		Vector2 origin = rotationOriginFromCenter.Value;
		origin.X *= player.direction;
		origin.Y *= player.gravDir;
		player.itemRotation = desiredRotation;
		if (flipAngle)
		{
			player.itemRotation *= player.direction;
		}
		else if (player.direction < 0)
		{
			player.itemRotation += (float)Math.PI;
		}
		Vector2 consistentAnchor = player.itemRotation.ToRotationVector2() * (spriteSize.X / -2f - 10f) * (float)player.direction - origin.RotatedBy(player.itemRotation);
		Vector2 offsetAgain = spriteSize * -0.5f;
		Vector2 finalPosition = desiredPosition + offsetAgain + consistentAnchor;
		if (stepDisplace)
		{
			int frame = player.bodyFrame.Y / player.bodyFrame.Height;
			if ((frame > 6 && frame < 10) || (frame > 13 && frame < 17))
			{
				finalPosition -= Vector2.UnitY * 2f;
			}
		}
		player.itemLocation = finalPosition + new Vector2(spriteSize.X * 0.5f, 0f);
	}

	public static void HideAccessories(this Player player, bool hideHeadAccs = true, bool hideBodyAccs = true, bool hideLegAccs = true, bool hideShield = true)
	{
		if (hideHeadAccs)
		{
			player.face = -1;
		}
		if (hideBodyAccs)
		{
			player.handon = -1;
			player.handoff = -1;
			player.back = -1;
			player.front = -1;
			player.neck = -1;
		}
		if (hideLegAccs)
		{
			player.shoe = -1;
			player.waist = -1;
		}
		if (hideShield)
		{
			player.shield = -1;
		}
	}

	public static Vector2 ClampedMouseWorld(this Player player)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		Vector2 mouseWorld = player.Calamity().mouseWorld;
		mouseWorld.X = ((mouseWorld.X >= player.MountedCenter.X) ? MathF.Min(mouseWorld.X, player.MountedCenter.X + 960f) : MathF.Max(mouseWorld.X, player.MountedCenter.X - 960f));
		mouseWorld.Y = ((mouseWorld.Y >= player.MountedCenter.Y) ? MathF.Min(mouseWorld.Y, player.MountedCenter.Y + 540f) : MathF.Max(mouseWorld.Y, player.MountedCenter.Y - 540f));
		return mouseWorld;
	}

	public static bool CantUseHoldout(this Player player, bool needsToHold = true)
	{
		if (player != null && player.active && !player.dead && !(!player.channel & needsToHold) && !player.CCed)
		{
			return player.noItems;
		}
		return true;
	}

	public static bool ShouldTriggerSummonPenalty(Player player, Item item)
	{
		CalamityPlayer modPlayer = player.Calamity();
		bool num = player.armor[0].type == 3776 && player.armor[1].type == 3777 && player.armor[2].type == 3778 && item.CountsAsClass<MagicDamageClass>();
		bool circletWithRogueWeapon = player.armor[0].type == ModContent.ItemType<ForbiddenCirclet>() && player.armor[1].type == 3777 && player.armor[2].type == 3778 && item.CountsAsClass<RogueDamageClass>();
		bool gemTechBlueGem = modPlayer.GemTechSet && modPlayer.GemTechState.IsBlueGemActive;
		bool crossClassNerfDisabled = (((num | circletWithRogueWeapon) || modPlayer.fearmongerSet) | gemTechBlueGem) || modPlayer.profanedCrystalBuffs || DD2Event.Ongoing;
		if (item.type > 0 && !crossClassNerfDisabled)
		{
			bool num2 = !item.CountsAsClass<SummonDamageClass>() && (item.CountsAsClass<MeleeDamageClass>() || item.CountsAsClass<RangedDamageClass>() || item.CountsAsClass<MagicDamageClass>() || item.CountsAsClass<ThrowingDamageClass>());
			bool heldItemIsTool = (item.pick > 0 || item.axe > 0 || item.hammer > 0) && !CalamityItemSets.WeaponWithToolPowerAffectedBySummonPenalty[item.type];
			bool heldItemCanBeUsed = item.useStyle != 0;
			bool heldItemIsAccessoryOrAmmo = item.accessory || item.ammo != AmmoID.None;
			bool heldItemIsExcludedByModCall = CalamityItemSets.ItemWhichDisablesSummonerNerf[item.type];
			if ((num2 & heldItemCanBeUsed) && !heldItemIsTool && !heldItemIsAccessoryOrAmmo && !heldItemIsExcludedByModCall)
			{
				return true;
			}
		}
		return false;
	}

	public static void SendPacket(this Player player, ModPacket packet, bool server)
	{
		if (!server)
		{
			packet.Send();
		}
		else
		{
			packet.Send(-1, player.whoAmI);
		}
	}

	public static T ModProjectile<T>(this Projectile projectile) where T : ModProjectile
	{
		return projectile.ModProjectile as T;
	}

	public static bool AnyProjectiles(int projectileID)
	{
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			if (enumerator.Current.type == projectileID)
			{
				return true;
			}
		}
		return false;
	}

	public static bool AnyOwnedProjectiles(int projectileID, int ownerID)
	{
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == projectileID && p.owner == ownerID)
			{
				return true;
			}
		}
		return false;
	}

	public static int CountProjectiles(int projectileID)
	{
		return Main.projectile.Count((Projectile proj) => proj.type == projectileID && proj.active);
	}

	public static int CountHookProj()
	{
		return Main.projectile.Count((Projectile proj) => Main.projHook[proj.type] && proj.ai[0] == 2f && proj.active && proj.owner == Main.myPlayer);
	}

	public static Projectile FindProjectileByIdentity(int identity, int ownerIndex)
	{
		if (Main.netMode == 0)
		{
			return Main.projectile[identity];
		}
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.identity == identity && p.owner == ownerIndex)
			{
				return p;
			}
		}
		return null;
	}

	public static int FindFirstProjectile(int Type)
	{
		int index = -1;
		for (int x = 0; x < Main.maxProjectiles; x++)
		{
			Projectile proj = Main.projectile[x];
			if (proj.active && proj.type == Type)
			{
				index = x;
				break;
			}
		}
		return index;
	}

	public static void OnlyOneSentry(Player player, int Type)
	{
		int existingTurrets = player.ownedProjectileCounts[Type];
		if (existingTurrets <= 0)
		{
			return;
		}
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.type == Type && p.owner == player.whoAmI)
			{
				p.Kill();
				existingTurrets--;
				if (existingTurrets <= 0)
				{
					break;
				}
			}
		}
	}

	public static void ExpandHitboxBy(this Projectile projectile, int width, int height)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		projectile.position = projectile.Center;
		projectile.width = width;
		projectile.height = height;
		projectile.position -= projectile.Size * 0.5f;
	}

	public static void ExpandHitboxBy(this Projectile projectile, int newSize)
	{
		projectile.ExpandHitboxBy(newSize, newSize);
	}

	public static void ExpandHitboxBy(this Projectile projectile, Vector2 newSize)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		projectile.ExpandHitboxBy((int)newSize.X, (int)newSize.Y);
	}

	public static void ExpandHitboxBy(this Projectile projectile, float expandRatio)
	{
		projectile.ExpandHitboxBy((int)((float)projectile.width * expandRatio), (int)((float)projectile.height * expandRatio));
	}

	public static void PreventTileCollisionUntilHitboxIsOutsideOfTiles(Projectile projectile)
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		if (projectile.tileCollide)
		{
			return;
		}
		bool canCollide = true;
		float increment = 16f;
		float offset = increment * 2f;
		float num = projectile.position.X - increment;
		float endIndexX = num + offset + (float)projectile.width;
		float startIndexY = projectile.position.Y - increment;
		float endIndexY = projectile.position.Y + offset + (float)projectile.height;
		Vector2 tilePosition = default(Vector2);
		for (float i = num; i < endIndexX; i += increment)
		{
			if (!canCollide)
			{
				break;
			}
			for (float j = startIndexY; j < endIndexY; j += increment)
			{
				tilePosition.X = i;
				tilePosition.Y = j;
				Tile tileSafely = Framing.GetTileSafely(tilePosition.ToTileCoordinates());
				if (tileSafely.HasUnactuatedTile && Main.tileSolid[tileSafely.TileType] && !Main.tileSolidTop[tileSafely.TileType] && !TileID.Sets.Platforms[tileSafely.TileType])
				{
					canCollide = false;
					break;
				}
			}
		}
		if (canCollide)
		{
			projectile.tileCollide = true;
		}
	}

	public static void HomeInOnNPC(Projectile projectile, bool ignoreTiles, float distanceRequired, float homingVelocity, float inertia, bool respectIFrames = false)
	{
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		if (!projectile.friendly)
		{
			return;
		}
		if (projectile.Calamity().defExtraUpdates == -1)
		{
			projectile.Calamity().defExtraUpdates = projectile.extraUpdates;
		}
		Vector2 destination = projectile.Center;
		bool locatedTarget = false;
		float npcDistCompare = 25000f;
		int index = -1;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			float extraDistance = n.width / 2 + n.height / 2;
			if (n.CanBeChasedBy(projectile) && projectile.WithinRange(n.Center, distanceRequired + extraDistance) && (!respectIFrames || (projectile.localNPCImmunity[n.whoAmI] <= 0 && projectile.localNPCImmunity[n.whoAmI] != -1 && n.immune[projectile.owner] <= 0)))
			{
				float currentNPCDist = Vector2.Distance(n.Center, projectile.Center);
				if (respectIFrames && Projectile.perIDStaticNPCImmunity[projectile.type][n.whoAmI] > Main.GameUpdateCount)
				{
					currentNPCDist += 1600f;
				}
				if (currentNPCDist < npcDistCompare && (ignoreTiles || Collision.CanHit(projectile.Center, 1, 1, n.Center, 1, 1)))
				{
					npcDistCompare = currentNPCDist;
					index = n.whoAmI;
				}
			}
		}
		if (index != -1)
		{
			destination = Main.npc[index].Center;
			locatedTarget = true;
		}
		if (locatedTarget)
		{
			projectile.extraUpdates = projectile.Calamity().defExtraUpdates + 1;
			Vector2 homeDirection = (destination - projectile.Center).SafeNormalize(Vector2.UnitY);
			projectile.velocity = (projectile.velocity * inertia + homeDirection * homingVelocity) / (inertia + 1f);
			projectile.Calamity().HomingTarget = index;
		}
		else
		{
			projectile.extraUpdates = projectile.Calamity().defExtraUpdates;
		}
	}

	public static void HomeInOnSelectedNPC(Projectile projectile, NPC target, bool ignoreTiles = true, float homingVelocity = 0.5f, float maxSpeed = 10f, float inertia = 0.985f, float overspeedReduction = 0.95f, bool accelerate = false)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		if (target != null && (ignoreTiles || Collision.CanHit(projectile.Center, 1, 1, target.Center, 1, 1)))
		{
			Vector2 moveToNPC = (target.Center - projectile.Center).SafeNormalize(Vector2.UnitX);
			if (((Vector2)(ref projectile.velocity)).Length() < maxSpeed)
			{
				projectile.velocity = projectile.velocity * inertia + moveToNPC * homingVelocity;
			}
			else
			{
				projectile.velocity *= overspeedReduction;
			}
			projectile.Calamity().HomingTarget = target.whoAmI;
		}
		if ((target == null && ((Vector2)(ref projectile.velocity)).Length() < maxSpeed) & accelerate)
		{
			projectile.velocity *= 1.0055f;
		}
	}

	public static Vector2 CalculatePredictiveAimToTarget(Vector2 startingPosition, Vector2 targetPosition, Vector2 targetVelocity, float shootSpeed, int iterations = 4)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		float previousTimeToReachDestination = 0f;
		Vector2 currentTargetPosition = targetPosition;
		for (int i = 0; i < iterations; i++)
		{
			float timeToReachDestination = Vector2.Distance(startingPosition, currentTargetPosition) / shootSpeed;
			currentTargetPosition += targetVelocity * (timeToReachDestination - previousTimeToReachDestination);
			previousTimeToReachDestination = timeToReachDestination;
		}
		return (currentTargetPosition - startingPosition).SafeNormalize(Vector2.UnitY) * shootSpeed;
	}

	public static Vector2 CalculatePredictiveAimToTarget(Vector2 startingPosition, Entity target, float shootSpeed, int iterations = 4)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return CalculatePredictiveAimToTarget(startingPosition, target.Center, target.velocity, shootSpeed, iterations);
	}

	public static Vector2 CalculatePredictiveAimToTargetMaxUpdates(Vector2 startingPosition, Vector2 targetPosition, Vector2 targetVelocity, float shootSpeed, int projMaxUpdates, int iterations = 4)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		float previousTimeToReachDestination = 0f;
		Vector2 currentTargetPosition = targetPosition;
		for (int i = 0; i < iterations; i++)
		{
			float timeToReachDestination = Vector2.Distance(startingPosition, currentTargetPosition) / shootSpeed / (float)projMaxUpdates;
			currentTargetPosition += targetVelocity * (timeToReachDestination - previousTimeToReachDestination);
			previousTimeToReachDestination = timeToReachDestination;
		}
		return (currentTargetPosition - startingPosition).SafeNormalize(Vector2.UnitY) * shootSpeed;
	}

	public static Vector2 CalculatePredictiveAimToTargetMaxUpdates(Vector2 startingPosition, Entity target, float shootSpeed, int projMaxUpdates, int iterations = 4)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		return CalculatePredictiveAimToTargetMaxUpdates(startingPosition, target.Center, target.velocity, shootSpeed, projMaxUpdates, iterations);
	}

	public static Vector2 SuperhomeTowardsTarget(this Projectile projectile, NPC target, float homingSpeed, float inertia, float predictionStrength = 1f)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		if (predictionStrength < 0.01f)
		{
			predictionStrength = 0.01f;
		}
		Vector2 idealVelocity = CalculatePredictiveAimToTarget(projectile.Center, target, homingSpeed / predictionStrength) * predictionStrength;
		return (projectile.velocity * (inertia - 1f) + idealVelocity) / inertia;
	}

	public static Projectile ProjectileRain(IEntitySource source, Vector2 targetPos, float xLimit, float xVariance, float yLimitLower, float yLimitUpper, float projSpeed, int projType, int damage, float knockback, int owner)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		float x = targetPos.X + Main.rand.NextFloat(0f - xLimit, xLimit);
		if (projType == ModContent.ProjectileType<AstralStarMagic>())
		{
			x = targetPos.X + xLimit;
		}
		float y = targetPos.Y - Main.rand.NextFloat(yLimitLower, yLimitUpper);
		Vector2 spawnPosition = default(Vector2);
		((Vector2)(ref spawnPosition))._002Ector(x, y);
		Vector2 velocity = targetPos - spawnPosition;
		velocity.X += Main.rand.NextFloat(0f - xVariance, xVariance);
		float targetDist = ((Vector2)(ref velocity)).Length();
		targetDist = projSpeed / targetDist;
		velocity.X *= targetDist;
		velocity.Y *= targetDist;
		return Projectile.NewProjectileDirect(source, spawnPosition, velocity, projType, damage, knockback, owner);
	}

	public static Projectile ProjectileBarrage(IEntitySource source, Vector2 originVec, Vector2 targetPos, bool fromRight, float xOffsetMin, float xOffsetMax, float yOffsetMin, float yOffsetMax, float projSpeed, int projType, int damage, float knockback, int owner, bool clamped = false, float inaccuracyOffset = 5f)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		float xPos = originVec.X + Main.rand.NextFloat(xOffsetMin, xOffsetMax) * (float)fromRight.ToDirectionInt();
		float yPos = originVec.Y + Main.rand.NextFloat(yOffsetMin, yOffsetMax) * (float)Main.rand.NextBool().ToDirectionInt();
		Vector2 spawnPosition = default(Vector2);
		((Vector2)(ref spawnPosition))._002Ector(xPos, yPos);
		Vector2 velocity = targetPos - spawnPosition;
		velocity.X += Main.rand.NextFloat(0f - inaccuracyOffset, inaccuracyOffset);
		velocity.Y += Main.rand.NextFloat(0f - inaccuracyOffset, inaccuracyOffset);
		((Vector2)(ref velocity)).Normalize();
		velocity *= projSpeed * (clamped ? 150f : 1f);
		if (clamped)
		{
			velocity.X = MathHelper.Clamp(velocity.X, -15f, 15f);
			velocity.Y = MathHelper.Clamp(velocity.Y, -15f, 15f);
		}
		return Projectile.NewProjectileDirect(source, spawnPosition, velocity, projType, damage, knockback, owner);
	}

	public static Projectile SpawnOrb(Projectile projectile, int damage, int projType, float distanceRequired, float speedMult, bool gsPhantom = false)
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		float ai1 = Main.rand.NextFloat() + 0.5f;
		int[] array = new int[Main.maxNPCs];
		int targetArrayA = 0;
		int targetArrayB = 0;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (!npc.CanBeChasedBy(projectile))
			{
				continue;
			}
			float enemyDist = Vector2.Distance(projectile.Center, npc.Center);
			if (enemyDist < distanceRequired)
			{
				if (Collision.CanHit(projectile.position, 1, 1, npc.position, npc.width, npc.height) && enemyDist > 50f)
				{
					array[targetArrayB] = npc.whoAmI;
					targetArrayB++;
				}
				else if (targetArrayB == 0)
				{
					array[targetArrayA] = npc.whoAmI;
					targetArrayA++;
				}
			}
		}
		if (targetArrayA == 0 && targetArrayB == 0)
		{
			return Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), projectile.Center, Vector2.Zero, ModContent.ProjectileType<NobodyKnows>(), 0, 0f, projectile.owner);
		}
		int target = ((targetArrayB <= 0) ? array[Main.rand.Next(targetArrayA)] : array[Main.rand.Next(targetArrayB)]);
		Vector2 velocity = RandomVelocity(100f, speedMult, speedMult, 1f);
		return Projectile.NewProjectileDirect(projectile.GetSource_FromThis(), projectile.Center, velocity, projType, damage, 0f, projectile.owner, gsPhantom ? 0f : ((float)target), gsPhantom ? ai1 : 0f);
	}

	public static void MagnetSphereHitscan(Projectile projectile, float distanceRequired, float homingVelocity, float projectileTimer, int maxTargets, int spawnedProjectile, double damageMult = 1.0, bool attackMultiple = false, DamageClass damageType = null)
	{
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		projectile.localAI[1]++;
		if (!(projectile.localAI[1] > projectileTimer))
		{
			return;
		}
		projectile.localAI[1] = 0f;
		bool homeIn = false;
		int[] targetArray = new int[maxTargets];
		int targetArrayIndex = 0;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (!n.CanBeChasedBy(projectile))
			{
				continue;
			}
			float extraDistance = n.width / 2 + n.height / 2;
			bool canHit = true;
			if (extraDistance < distanceRequired)
			{
				canHit = Collision.CanHit(projectile.Center, 1, 1, n.Center, 1, 1);
			}
			if (projectile.WithinRange(n.Center, distanceRequired + extraDistance) & canHit)
			{
				if (targetArrayIndex >= maxTargets)
				{
					break;
				}
				targetArray[targetArrayIndex] = n.whoAmI;
				targetArrayIndex++;
				homeIn = true;
			}
		}
		if (!homeIn)
		{
			return;
		}
		int randomTarget = Main.rand.Next(targetArrayIndex);
		randomTarget = targetArray[randomTarget];
		projectile.localAI[1] = 0f;
		Vector2 spawnPos = projectile.Center + projectile.velocity * 4f;
		Vector2 velocity = Vector2.Normalize(Main.npc[randomTarget].Center - spawnPos) * homingVelocity;
		if (attackMultiple)
		{
			for (int i = 0; i < targetArrayIndex; i++)
			{
				velocity = Vector2.Normalize(Main.npc[targetArray[i]].Center - spawnPos) * homingVelocity;
				if (projectile.owner == Main.myPlayer)
				{
					int projectile2 = Projectile.NewProjectile(projectile.GetSource_FromThis(), spawnPos, velocity, spawnedProjectile, (int)((double)projectile.damage * damageMult), projectile.knockBack, projectile.owner);
					if (damageType != null && projectile2.WithinBounds(Main.maxProjectiles))
					{
						Main.projectile[projectile2].DamageType = damageType;
					}
				}
			}
		}
		else if (projectile.owner == Main.myPlayer)
		{
			int projectile3 = Projectile.NewProjectile(projectile.GetSource_FromThis(), spawnPos, velocity, spawnedProjectile, (int)((double)projectile.damage * damageMult), projectile.knockBack, projectile.owner);
			if (damageType != null && projectile3.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[projectile3].DamageType = damageType;
			}
		}
	}

	public static void KillAllHostileProjectiles()
	{
		for (int x = 0; x < Main.maxProjectiles; x++)
		{
			Projectile projectile = Main.projectile[x];
			if (projectile.active && projectile.hostile && !projectile.friendly && projectile.damage > 0)
			{
				projectile.Kill();
			}
		}
	}

	public static void KillShootProjectiles(bool shouldBreak, int projType, Player player)
	{
		for (int x = 0; x < Main.maxProjectiles; x++)
		{
			Projectile proj = Main.projectile[x];
			if (proj.active && proj.owner == player.whoAmI && proj.type == projType)
			{
				proj.Kill();
				if (shouldBreak)
				{
					break;
				}
			}
		}
	}

	public static void KillShootProjectileMany(Player player, params int[] projTypes)
	{
		for (int x = 0; x < Main.maxProjectiles; x++)
		{
			Projectile proj = Main.projectile[x];
			if (proj.active && proj.owner == player.whoAmI && projTypes.Contains(proj.type))
			{
				proj.Kill();
			}
		}
	}

	public static int RocketBehavior(this Projectile proj, RocketBehaviorInfo info)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0208: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0266: Unknown result type (might be due to invalid IL or missing references)
		//IL_026c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		int explosionRadius = 0;
		Point center = proj.Center.ToTileCoordinates();
		DelegateMethods.v2_1 = center.ToVector2();
		DelegateMethods.f_1 = 3f;
		switch (info.rocketItemType)
		{
		case 771:
			explosionRadius = info.smallRadius;
			break;
		case 772:
			explosionRadius = info.smallRadius;
			proj.ExplodeTiles(explosionRadius, info.respectStandardBlastImmunity, info.tilesToCheck, info.wallsToCheck);
			break;
		case 773:
			explosionRadius = info.mediumRadius;
			break;
		case 774:
			explosionRadius = info.mediumRadius;
			proj.ExplodeTiles(explosionRadius, info.respectStandardBlastImmunity, info.tilesToCheck, info.wallsToCheck);
			break;
		case 4457:
			explosionRadius = info.largeRadius;
			break;
		case 4458:
			explosionRadius = info.largeRadius;
			proj.ExplodeTiles(explosionRadius, info.respectStandardBlastImmunity, info.tilesToCheck, info.wallsToCheck);
			break;
		case 4445:
			explosionRadius = info.bigRadius;
			SpawnClusterFragments();
			break;
		case 4446:
			explosionRadius = info.bigRadius;
			SpawnClusterFragments(destructiveVariant: true);
			break;
		case 4459:
			DelegateMethods.f_1 = 3.5f;
			Utils.PlotTileArea(center.X, center.Y, DelegateMethods.SpreadDry);
			break;
		case 4447:
			Utils.PlotTileArea(center.X, center.Y, DelegateMethods.SpreadWater);
			break;
		case 4448:
			Utils.PlotTileArea(center.X, center.Y, DelegateMethods.SpreadLava);
			break;
		case 4449:
			Utils.PlotTileArea(center.X, center.Y, DelegateMethods.SpreadHoney);
			break;
		}
		return explosionRadius;
		void SpawnClusterFragments(bool destructiveVariant = false)
		{
			//IL_008e: Unknown result type (might be due to invalid IL or missing references)
			//IL_0095: Unknown result type (might be due to invalid IL or missing references)
			//IL_009a: Unknown result type (might be due to invalid IL or missing references)
			//IL_009f: Unknown result type (might be due to invalid IL or missing references)
			//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
			//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
			//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
			if (proj.owner == Main.myPlayer)
			{
				int projID = (destructiveVariant ? info.destructiveClusterProjectileID : info.clusterProjectileID);
				int clusterDamage = (int)((float)proj.damage * info.clusterSplitDamageMultiplier);
				float thetaStart = Main.rand.NextFloat(0f, (float)Math.PI * 2f);
				for (float i = 0f; i < 6f; i++)
				{
					float f = thetaStart + i * ((float)Math.PI * 2f) / 6f;
					float dist = Main.rand.NextFloat(4f, 6f);
					Vector2 clusterVel = f.ToRotationVector2() * dist - Vector2.UnitY;
					Projectile.NewProjectileDirect(proj.GetSource_FromThis(), proj.Center, clusterVel, projID, clusterDamage, 0f, proj.owner).timeLeft -= Main.rand.Next(30);
				}
			}
		}
	}

	public static bool DrawBeam(this Projectile projectile, float length, float spacer, Color lightColor, Texture2D texture = null, bool curve = false)
	{
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		if (texture == null)
		{
			texture = TextureAssets.Projectile[projectile.type].Value;
		}
		float widthOffset = (float)(texture.Width - projectile.width) * 0.5f + (float)projectile.width * 0.5f;
		float heightOffset = projectile.height / 2;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector(widthOffset, heightOffset);
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Rectangle roughScreenBounds = default(Rectangle);
		((Rectangle)(ref roughScreenBounds))._002Ector((int)Main.screenPosition.X - 500, (int)Main.screenPosition.Y - 500, Main.screenWidth + 1000, Main.screenHeight + 1000);
		Rectangle rect = projectile.getRect();
		if (((Rectangle)(ref rect)).Intersects(roughScreenBounds))
		{
			Vector2 drawPos = projectile.position - Main.screenPosition + origin;
			drawPos.Y += projectile.gfxOffY;
			float maxTrailPoints = length;
			if (projectile.ai[1] == 1f)
			{
				maxTrailPoints = (int)projectile.localAI[0];
			}
			Vector2 cumulativeOffset = Vector2.Zero;
			Color alpha = projectile.GetAlpha(lightColor);
			float fixedRotation = projectile.rotation + (float)Math.PI / 2f;
			for (int i = 1; i <= (int)projectile.localAI[0]; i++)
			{
				Vector2 velToUseThisIter = projectile.velocity;
				if (curve)
				{
					int oldVelIndex = (int)((float)i / projectile.localAI[0] * (float)projectile.oldRot.Length);
					if (oldVelIndex > 0)
					{
						float angleChange = projectile.oldRot[oldVelIndex - 1] - projectile.rotation;
						velToUseThisIter = projectile.velocity.RotatedBy(angleChange);
					}
				}
				cumulativeOffset += Vector2.Normalize(velToUseThisIter) * spacer;
				Color color = alpha;
				color *= (maxTrailPoints - (float)i) / maxTrailPoints;
				((Color)(ref color)).A = 0;
				Main.spriteBatch.Draw(texture, drawPos - cumulativeOffset, (Rectangle?)null, color, fixedRotation, origin, projectile.scale, spriteEffects, 0f);
			}
		}
		return false;
	}

	public static void DrawBackglow(this Projectile projectile, Color backglowColor, float backglowArea, Texture2D? texture = null, Rectangle? frame = null, SpriteEffects effects = (SpriteEffects)0, float xPos = 0f, float yPos = 0f)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		if (texture == null)
		{
			texture = TextureAssets.Projectile[projectile.type].Value;
		}
		Rectangle valueOrDefault = frame.GetValueOrDefault();
		if (!frame.HasValue)
		{
			valueOrDefault = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
			frame = valueOrDefault;
		}
		Vector2 drawPosition = (Vector2)((xPos == 0f && yPos == 0f) ? projectile.Center : new Vector2(xPos, yPos)) - Main.screenPosition;
		Vector2 origin = frame.Value.Size() * 0.5f;
		Color backAfterimageColor = backglowColor * projectile.Opacity;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		spriteEffects = ((projectile.spriteDirection != -1 || (int)effects != 0) ? effects : ((SpriteEffects)1));
		for (int i = 0; i < 10; i++)
		{
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 10f).ToRotationVector2() * backglowArea;
			Main.spriteBatch.Draw(texture, drawPosition + drawOffset, frame, backAfterimageColor, projectile.rotation, origin, projectile.scale, spriteEffects, 0f);
		}
	}

	public static void DrawBackglow(this Projectile projectile, Color backglowColor, float backglowArea, Vector2 scale, Texture2D? texture = null, Rectangle? frame = null, Vector2? offset = null)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		if (texture == null)
		{
			texture = TextureAssets.Projectile[projectile.type].Value;
		}
		Rectangle valueOrDefault = frame.GetValueOrDefault();
		if (!frame.HasValue)
		{
			valueOrDefault = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
			frame = valueOrDefault;
		}
		Vector2 drawPosition = projectile.Center - Main.screenPosition;
		Vector2 origin = frame.Value.Size() * 0.5f;
		Color backAfterimageColor = backglowColor * projectile.Opacity;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		for (int i = 0; i < 10; i++)
		{
			Vector2 off = Vector2.Zero;
			if (offset.HasValue)
			{
				off += offset.Value;
			}
			Vector2 drawOffset = ((float)Math.PI * 2f * (float)i / 10f).ToRotationVector2() * backglowArea;
			Main.spriteBatch.Draw(texture, drawPosition + drawOffset + off, frame, backAfterimageColor, projectile.rotation, origin, scale, spriteEffects, 0f);
		}
	}

	public static void DrawProjectileWithBackglow(this Projectile projectile, Color backglowColor, Color lightColor, float backglowArea, Texture2D? texture = null, Rectangle? frame = null, SpriteEffects effects = (SpriteEffects)0, float xPos = 0f, float yPos = 0f)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		if (texture == null)
		{
			texture = TextureAssets.Projectile[projectile.type].Value;
		}
		Rectangle valueOrDefault = frame.GetValueOrDefault();
		if (!frame.HasValue)
		{
			valueOrDefault = texture.Frame(1, Main.projFrames[projectile.type], 0, projectile.frame);
			frame = valueOrDefault;
		}
		Vector2 drawPosition = (Vector2)((xPos == 0f && yPos == 0f) ? projectile.Center : new Vector2(xPos, yPos)) - Main.screenPosition;
		Vector2 origin = frame.Value.Size() * 0.5f;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		spriteEffects = ((projectile.spriteDirection != -1 || (int)effects != 0) ? effects : ((SpriteEffects)1));
		projectile.DrawBackglow(backglowColor, backglowArea, texture, frame, spriteEffects, xPos, yPos);
		Main.spriteBatch.Draw(texture, drawPosition, frame, projectile.GetAlpha(lightColor), projectile.rotation, origin, projectile.scale, spriteEffects, 0f);
	}

	public static void DrawStarTrail(this Projectile projectile, Color outer, Color inner, float auraHeight = 10f)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c1: Unknown result type (might be due to invalid IL or missing references)
		Texture2D aura = ModContent.Request<Texture2D>("CalamityMod/Projectiles/StarTrail", (AssetRequestMode)2).Value;
		Vector2 offsets = new Vector2(0f, projectile.gfxOffY) - Main.screenPosition;
		Rectangle auraRec = aura.Frame();
		float auraRotation = projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		Vector2 auraOrigin = default(Vector2);
		((Vector2)(ref auraOrigin))._002Ector((float)auraRec.Width / 2f, auraHeight);
		Vector2 drawStartOuter = offsets + projectile.Center + projectile.velocity;
		Vector2 spinPoint = -Vector2.UnitY * auraHeight;
		float time = (float)Main.player[projectile.owner].miscCounter % 216000f / 60f;
		Color outerColor = outer * 0.2f;
		((Color)(ref outerColor)).A = 0;
		float rotation = (float)Math.PI * 2f * time;
		for (int o = 0; o < 6; o += 2)
		{
			Vector2 spinStart = drawStartOuter + spinPoint.RotatedBy(rotation - (float)Math.PI * (float)o / 3f);
			float scaleMultOuter = 1.5f - (float)o * 0.1f;
			Main.EntitySpriteDraw(aura, spinStart, auraRec, outerColor, auraRotation, auraOrigin, scaleMultOuter, (SpriteEffects)0);
		}
		Vector2 drawStartInner = offsets + projectile.Center - projectile.velocity * 0.5f;
		Color innerColor = inner * 0.5f;
		((Color)(ref innerColor)).A = 0;
		for (float i = 0f; i < 1f; i += 0.5f)
		{
			float scaleMult = time % 0.5f / 0.5f;
			scaleMult = (scaleMult + i) % 1f;
			float colorMult = scaleMult * 2f;
			if (colorMult > 1f)
			{
				colorMult = 2f - colorMult;
			}
			Main.EntitySpriteDraw(aura, drawStartInner, auraRec, innerColor * colorMult, auraRotation, auraOrigin, 0.3f + scaleMult * 0.5f, (SpriteEffects)0);
		}
	}

	public static bool FinalExtraUpdate(this Projectile proj)
	{
		return proj.numUpdates == -1;
	}

	public static bool IsTrueMelee(this Projectile proj)
	{
		if (proj == null || !proj.active)
		{
			return false;
		}
		if (!proj.CountsAsClass<TrueMeleeDamageClass>())
		{
			return proj.CountsAsClass<TrueMeleeNoSpeedDamageClass>();
		}
		return true;
	}

	public static int DamageSoftCap(double dmgInput, int cap)
	{
		if (dmgInput < (double)cap)
		{
			return (int)dmgInput;
		}
		double cappedRatio = Math.Pow(dmgInput / (double)cap, 0.5) / 1.25 + 0.2;
		return (int)((double)cap * cappedRatio);
	}

	public static Vector2 RandomVelocity(float directionMult, float speedLowerLimit, float speedCap, float speedMult = 0.1f)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		Vector2 velocity = default(Vector2);
		((Vector2)(ref velocity))._002Ector(Main.rand.NextFloat(0f - directionMult, directionMult), Main.rand.NextFloat(0f - directionMult, directionMult));
		while (velocity.X == 0f && velocity.Y == 0f)
		{
			((Vector2)(ref velocity))._002Ector(Main.rand.NextFloat(0f - directionMult, directionMult), Main.rand.NextFloat(0f - directionMult, directionMult));
		}
		((Vector2)(ref velocity)).Normalize();
		velocity *= Main.rand.NextFloat(speedLowerLimit, speedCap) * speedMult;
		return velocity;
	}

	public static void ForceNetUpdate(this Projectile proj, bool ignoreCurrentNetSpam = true)
	{
		proj.netUpdate = true;
		if ((proj.netSpam >= 10) | ignoreCurrentNetSpam)
		{
			proj.netSpam = 0;
		}
	}

	public static void ExplodeTiles(this Projectile p, int explosionRadius, bool respectStandardBlastImmunity = true, IEnumerable<int> customBlastImmuneTiles = null, IEnumerable<int> customBlastImmuneWalls = null)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		ExplodeTiles(p.Center, explosionRadius, respectStandardBlastImmunity, customBlastImmuneTiles, customBlastImmuneWalls);
	}

	public static void ExplodeTiles(Vector2 explosionPos, int explosionRadius, bool respectStandardBlastImmunity = true, IEnumerable<int> customBlastImmuneTiles = null, IEnumerable<int> customBlastImmuneWalls = null)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		int minTileX = (int)explosionPos.X / 16 - explosionRadius;
		int maxTileX = (int)explosionPos.X / 16 + explosionRadius;
		int minTileY = (int)explosionPos.Y / 16 - explosionRadius;
		int maxTileY = (int)explosionPos.Y / 16 + explosionRadius;
		if (minTileX < 0)
		{
			minTileX = 0;
		}
		if (maxTileX > Main.maxTilesX)
		{
			maxTileX = Main.maxTilesX;
		}
		if (minTileY < 0)
		{
			minTileY = 0;
		}
		if (maxTileY > Main.maxTilesY)
		{
			maxTileY = Main.maxTilesY;
		}
		bool allowWallDestruction = false;
		float projTileX = explosionPos.X / 16f;
		float projTileY = explosionPos.Y / 16f;
		Vector2 explodeArea = default(Vector2);
		for (int x = minTileX; x <= maxTileX; x++)
		{
			for (int y = minTileY; y <= maxTileY; y++)
			{
				((Vector2)(ref explodeArea))._002Ector(Math.Abs((float)x - projTileX), Math.Abs((float)y - projTileY));
				if (((Vector2)(ref explodeArea)).Length() < (float)explosionRadius && Main.tile[x, y] != null && Main.tile[x, y].WallType == 0)
				{
					allowWallDestruction = true;
					break;
				}
			}
		}
		HashSet<int> blastImmuneTiles = new HashSet<int> { 26, 466 };
		if (respectStandardBlastImmunity)
		{
			foreach (int tileID in vanillaBlastImmuneTiles)
			{
				blastImmuneTiles.Add(tileID);
			}
			if (!Main.hardMode)
			{
				blastImmuneTiles.Add(58);
			}
			if (Main.getGoodWorld)
			{
				blastImmuneTiles.Add(48);
				blastImmuneTiles.Add(232);
			}
		}
		if (customBlastImmuneTiles != null)
		{
			foreach (int tileID2 in customBlastImmuneTiles)
			{
				blastImmuneTiles.Add(tileID2);
			}
		}
		HashSet<int> blastImmuneWalls = null;
		if (customBlastImmuneWalls != null)
		{
			blastImmuneWalls = new HashSet<int>();
			foreach (int wallID in customBlastImmuneWalls)
			{
				blastImmuneWalls.Add(wallID);
			}
		}
		bool refTrue = true;
		bool refFalse = false;
		Vector2 explodeArea2 = default(Vector2);
		for (int tx = minTileX; tx <= maxTileX; tx++)
		{
			for (int ty = minTileY; ty <= maxTileY; ty++)
			{
				Tile tile = Main.tile[tx, ty];
				ushort type = tile.TileType;
				((Vector2)(ref explodeArea2))._002Ector(Math.Abs((float)tx - projTileX), Math.Abs((float)ty - projTileY));
				if (((Vector2)(ref explodeArea2)).Length() >= (float)explosionRadius)
				{
					continue;
				}
				bool canBlastThisTile = true;
				if (tile != null && tile.HasTile)
				{
					if (blastImmuneTiles.Contains(type) || Main.tileContainer[tile.TileType] || (respectStandardBlastImmunity && (Main.tileDungeon[type] || !TileLoader.CanExplode(tx, ty))) || !TileLoader.CanKillTile(tx, ty, tile.TileType, ref refTrue) || !TileLoader.CanKillTile(tx, ty, tile.TileType, ref refFalse))
					{
						canBlastThisTile = false;
					}
					if (canBlastThisTile)
					{
						WorldGen.KillTile(tx, ty);
						if (!tile.HasTile && Main.netMode != 0)
						{
							NetMessage.SendData(17, -1, -1, null, 0, tx, ty);
						}
					}
				}
				if (!canBlastThisTile || !allowWallDestruction)
				{
					continue;
				}
				for (int wx = tx - 1; wx <= tx + 1; wx++)
				{
					int wy = ty - 1;
					while (wy <= ty + 1)
					{
						bool canBlastThisWall = !respectStandardBlastImmunity || WallLoader.CanExplode(wx, wy, Main.tile[wx, wy].WallType);
						if (blastImmuneWalls == null || !blastImmuneWalls.Contains(Main.tile[wx, wy].WallType))
						{
							if ((Main.tile[wx, wy] != null && Main.tile[wx, wy].WallType > 0) & canBlastThisWall)
							{
								WorldGen.KillWall(wx, wy);
								if (Main.tile[wx, wy].WallType == 0 && Main.netMode != 0)
								{
									NetMessage.SendData(17, -1, -1, null, 2, wx, wy);
								}
							}
							wy++;
							continue;
						}
						goto IL_037c;
					}
					continue;
					IL_037c:
					allowWallDestruction = false;
					break;
				}
			}
		}
	}

	public static void LargeFieryExplosion(this Projectile projectile)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_020b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_0369: Unknown result type (might be due to invalid IL or missing references)
		Vector2 corner = projectile.position;
		for (int i = 0; i < 40; i++)
		{
			int idx = Dust.NewDust(corner, projectile.width, projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[idx];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[idx].scale = 0.5f;
				Main.dust[idx].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 70; j++)
		{
			int idx2 = Dust.NewDust(corner, projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 3f);
			Main.dust[idx2].noGravity = true;
			Dust obj2 = Main.dust[idx2];
			obj2.velocity *= 5f;
			idx2 = Dust.NewDust(corner, projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[idx2];
			obj3.velocity *= 2f;
		}
		if (Main.dedServ)
		{
			return;
		}
		Vector2 goreSource = projectile.Center;
		int goreAmt = 3;
		Vector2 source = default(Vector2);
		((Vector2)(ref source))._002Ector(goreSource.X - 24f, goreSource.Y - 24f);
		for (int goreIndex = 0; goreIndex < goreAmt; goreIndex++)
		{
			float velocityMult = 0.33f;
			if (goreIndex < goreAmt / 3)
			{
				velocityMult = 0.66f;
			}
			if (goreIndex >= 2 * goreAmt / 3)
			{
				velocityMult = 1f;
			}
			int type = Main.rand.Next(61, 64);
			int smoke = Gore.NewGore(projectile.GetSource_Death(), source, default(Vector2), type);
			Gore obj4 = Main.gore[smoke];
			obj4.velocity *= velocityMult;
			obj4.velocity.X++;
			obj4.velocity.Y++;
			type = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(projectile.GetSource_Death(), source, default(Vector2), type);
			Gore obj5 = Main.gore[smoke];
			obj5.velocity *= velocityMult;
			obj5.velocity.X--;
			obj5.velocity.Y++;
			type = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(projectile.GetSource_Death(), source, default(Vector2), type);
			Gore obj6 = Main.gore[smoke];
			obj6.velocity *= velocityMult;
			obj6.velocity.X++;
			obj6.velocity.Y--;
			type = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(projectile.GetSource_Death(), source, default(Vector2), type);
			Gore obj7 = Main.gore[smoke];
			obj7.velocity *= velocityMult;
			obj7.velocity.X--;
			obj7.velocity.Y--;
		}
	}

	public static int GetActiveRicoshotCoinCount(this Player player)
	{
		int count = 0;
		int coinID = ModContent.ProjectileType<RicoshotCoin>();
		int clipID = ModContent.ProjectileType<M1GarandEmptyClip>();
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.owner == player.whoAmI && (p.type == coinID || p.type == clipID))
			{
				count++;
			}
		}
		return count;
	}

	public static bool CanRicoshotCoinForceCrit(Projectile p)
	{
		int coinID = ModContent.ProjectileType<RicoshotCoin>();
		int clipID = ModContent.ProjectileType<M1GarandEmptyClip>();
		if (!p.active || (p.type != coinID && p.type != clipID))
		{
			return false;
		}
		if (p.type == clipID)
		{
			return M1GarandEmptyClip.ClipLifetime - p.timeLeft >= M1GarandEmptyClip.CritDelayTime;
		}
		return RicoshotCoin.CoinLifetime - p.timeLeft >= RicoshotCoin.CritDelayTime;
	}

	public static Vector2 GetCoinTossVelocity(this Player player)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		Vector2 vectorToMouse = player.Calamity().mouseWorld - player.MountedCenter;
		vectorToMouse.Y *= -1f;
		Vector2 tossVelocity = default(Vector2);
		((Vector2)(ref tossVelocity))._002Ector(0f, RicoshotCoin.CoinTossForce);
		Vector2 mouseDirNormalized = vectorToMouse.SafeNormalize(Vector2.UnitY);
		tossVelocity += RicoshotCoin.CoinTossForce * mouseDirNormalized;
		tossVelocity.Y *= -1f;
		return (player.velocity + tossVelocity) / (float)RicoshotCoin.UpdateCount;
	}

	public static Projectile[] GetAvailableCoins(this Projectile searchingShot)
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		IList<Projectile> coins = new List<Projectile>();
		int coinID = ModContent.ProjectileType<RicoshotCoin>();
		int clipID = ModContent.ProjectileType<M1GarandEmptyClip>();
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile proj = enumerator.Current;
			if (proj.ModProjectile != null && (proj.type == coinID || proj.type == clipID) && !(proj.localAI[0] > 0f))
			{
				float maxDistance = ((proj.type == clipID) ? M1GarandEmptyClip.MaxIntraClipRicoshotDistance : RicoshotCoin.MaxIntraCoinRicoshotDistance);
				if (searchingShot.DistanceSQ(proj.Center) <= maxDistance * maxDistance)
				{
					coins.Add(proj);
				}
			}
		}
		return coins.ToArray();
	}

	public static RicoshotTarget FindRicochetTarget(this Projectile theShot, Vector2 startPos, Projectile[] availableCoins, bool considerFrozenCoins = false)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		//IL_021a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0251: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_025d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ce: Unknown result type (might be due to invalid IL or missing references)
		RicoshotTarget ret = default(RicoshotTarget);
		bool foundAnyCoin = false;
		float closestCoinDistance = RicoshotCoin.MaxIntraCoinRicoshotDistance;
		foreach (Projectile coin in availableCoins)
		{
			float maxDistance = ((coin.type == ModContent.ProjectileType<M1GarandEmptyClip>()) ? M1GarandEmptyClip.MaxIntraClipRicoshotDistance : RicoshotCoin.MaxIntraCoinRicoshotDistance);
			if (!(coin.Distance(startPos) > maxDistance) && (considerFrozenCoins || !(coin.ai[1] > 0f)) && Collision.CanHitLine(startPos - theShot.Size / 2f, theShot.width, theShot.height, coin.position, coin.width, coin.height))
			{
				float coinDistance = coin.Distance(startPos);
				if (coinDistance < closestCoinDistance)
				{
					foundAnyCoin = true;
					closestCoinDistance = coinDistance;
					ret.type = RicoshotTargetType.Coin;
					ret.pos = coin.Center;
					ret.entityID = coin.whoAmI;
				}
			}
		}
		if (foundAnyCoin)
		{
			return ret;
		}
		bool foundAnyBullseye = false;
		float closestBullseyeDistance = DaawnlightSpiritOrigin.RicoshotSearchDistance;
		int bullseyeID = ModContent.ProjectileType<SpiritOriginBullseye>();
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile proj = enumerator.Current;
			if (proj.ModProjectile != null && proj.type == bullseyeID && proj.owner == theShot.owner)
			{
				float bullseyeDistance = proj.Distance(startPos);
				if (bullseyeDistance < closestBullseyeDistance)
				{
					foundAnyBullseye = true;
					closestBullseyeDistance = bullseyeDistance;
					ret.type = RicoshotTargetType.Bullseye;
					ret.entityID = proj.whoAmI;
					NPC bullseyesOwner = Main.npc[(int)proj.ai[0]];
					float currentSpeed = ((Vector2)(ref theShot.velocity)).Length();
					Vector2 superpredictiveDirection = CalculatePredictiveAimToTarget(startPos, bullseyesOwner, currentSpeed).SafeNormalize(Vector2.Zero);
					Vector2 superprediction = startPos + superpredictiveDirection * bullseyeDistance;
					ret.pos = Vector2.Lerp(proj.Center, superprediction, RicoshotCoin.SuperpredictionRatio);
				}
			}
		}
		if (foundAnyBullseye)
		{
			return ret;
		}
		NPC targetNPC = startPos.ClosestNPCAt(RicoshotCoin.RicoshotSearchDistance, ignoreTiles: false, bossPriority: true);
		if (targetNPC != null)
		{
			ret.type = RicoshotTargetType.NPC;
			ret.entityID = targetNPC.whoAmI;
			float currentSpeed2 = ((Vector2)(ref theShot.velocity)).Length();
			Vector2 superpredictiveDirection2 = CalculatePredictiveAimToTarget(startPos, targetNPC, currentSpeed2).SafeNormalize(Vector2.Zero);
			Vector2 superprediction2 = startPos + superpredictiveDirection2 * targetNPC.Distance(startPos);
			ret.pos = Vector2.Lerp(targetNPC.Center, superprediction2, RicoshotCoin.SuperpredictionRatio);
			return ret;
		}
		ret.type = RicoshotTargetType.None;
		ret.pos = startPos + Main.rand.NextVector2Unit() * 32f;
		return ret;
	}

	public static string GenerateRandomAlphanumericString(int length)
	{
		return new string((from s in Enumerable.Repeat(AlphanumericCharacters, length)
			select s[Main.rand.Next(s.Length)]).ToArray());
	}

	public static T FindTileEntity<T>(int i, int j, int width, int height, int sheetSquare = 16) where T : ModTileEntity
	{
		Tile t = Main.tile[i, j];
		int left = i - t.TileFrameX % (width * sheetSquare) / sheetSquare;
		int top = j - t.TileFrameY % (height * sheetSquare) / sheetSquare;
		int chargerType = ModContent.GetInstance<T>().Type;
		if (!TileEntity.ByPosition.TryGetValue(new Point16(left, top), out var te) || te.type != chargerType)
		{
			return null;
		}
		return (T)te;
	}

	public static void CancelSignsAndChests(this Player player)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		Main.mouseRightRelease = false;
		if (player.sign >= 0)
		{
			SoundEngine.PlaySound(in SoundID.MenuClose);
			player.sign = -1;
			Main.editSign = false;
			Main.npcChatText = "";
		}
		if (Main.editChest)
		{
			SoundEngine.PlaySound(in SoundID.MenuTick);
			Main.editChest = false;
			Main.npcChatText = "";
		}
		if (player.chest >= 0)
		{
			player.chest = -1;
		}
	}

	internal static void DrawPowercellSlot(SpriteBatch spriteBatch, Item item, Vector2 drawPosition, float iconScale = 0.7f)
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		Texture2D slotBackgroundTex = ModContent.Request<Texture2D>("CalamityMod/UI/DraedonsArsenal/PowerCellSlot_Empty", (AssetRequestMode)2).Value;
		if (item.stack > 0)
		{
			slotBackgroundTex = ((item.type != ModContent.ItemType<BloodyVein>()) ? ModContent.Request<Texture2D>("CalamityMod/UI/DraedonsArsenal/PowerCellSlot_Filled", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalamityMod/UI/DraedonsArsenal/PowerCellSlot_Blood", (AssetRequestMode)2).Value);
		}
		spriteBatch.Draw(slotBackgroundTex, drawPosition, (Rectangle?)null, Color.White, 0f, slotBackgroundTex.Size() * 0.5f, iconScale, (SpriteEffects)0, 0f);
		if (item.stack > 0)
		{
			float inventoryScale = Main.inventoryScale * iconScale;
			Vector2 numberOffset = slotBackgroundTex.Size() * 0.2f;
			numberOffset.X -= 17f;
			ChatManager.DrawColorCodedStringWithShadow(spriteBatch, FontAssets.ItemStack.Value, item.stack.ToString(), drawPosition + numberOffset * inventoryScale, Color.White, 0f, Vector2.Zero, new Vector2(inventoryScale), -1f, inventoryScale);
		}
	}

	public static void SafeSquareTileFrame(int x, int y, bool resetFrame = true)
	{
		for (int xIter = x - 1; xIter <= x + 1; xIter++)
		{
			if (xIter < 0 || xIter >= Main.maxTilesX)
			{
				continue;
			}
			for (int yIter = y - 1; yIter <= y + 1; yIter++)
			{
				if (yIter >= 0 && yIter < Main.maxTilesY)
				{
					if (xIter == x && yIter == y)
					{
						WorldGen.TileFrame(x, y, resetFrame);
					}
					else
					{
						WorldGen.TileFrame(xIter, yIter);
					}
				}
			}
		}
	}

	public static bool DrawSwayingMultiTile(int i, int j)
	{
		if (TileObjectData.IsTopLeft(Main.tile[i, j]))
		{
			Main.instance.TilesRenderer.AddSpecialPoint(i, j, TileDrawing.TileCounterType.MultiTileVine);
		}
		return false;
	}

	public static void DrawFlameEffect(Texture2D flameTexture, int i, int j, int offsetX = 0, int offsetY = 0)
	{
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		Tile tile = Main.tile[i, j];
		if (!tile.IsTileActuallyInvisible())
		{
			Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange, (float)Main.offScreenRange));
			int width = 16;
			int height = 16;
			int yOffset = TileObjectData.GetTileData(tile).DrawYOffset;
			ulong randShakeEffect = Main.TileFrameSeed ^ (ulong)(((long)j << 32) | (uint)i);
			float drawPositionX = (float)(i * 16 - (int)Main.screenPosition.X) - ((float)width - 16f) / 2f;
			float drawPositionY = j * 16 - (int)Main.screenPosition.Y;
			for (int c = 0; c < 7; c++)
			{
				float shakeX = (float)Utils.RandomInt(ref randShakeEffect, -10, 11) * 0.15f;
				float shakeY = (float)Utils.RandomInt(ref randShakeEffect, -10, 1) * 0.35f;
				Main.spriteBatch.Draw(flameTexture, new Vector2(drawPositionX + shakeX, drawPositionY + shakeY + (float)yOffset) + zero, (Rectangle?)new Rectangle(tile.TileFrameX + offsetX, tile.TileFrameY + offsetY, width, height), new Color(100, 100, 100, 0), 0f, default(Vector2), 1f, (SpriteEffects)0, 0f);
			}
		}
	}

	public static void DrawStaticFlameEffect(Texture2D flameTexture, int i, int j, int offsetX = 0, int offsetY = 0)
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		if (Main.tile[i, j].IsTileActuallyInvisible())
		{
			return;
		}
		int xPos = Main.tile[i, j].TileFrameX;
		int yPos = Main.tile[i, j].TileFrameY;
		Color drawColour = default(Color);
		((Color)(ref drawColour))._002Ector(100, 100, 100, 0);
		Vector2 zero = (Vector2)(Main.drawToScreen ? Vector2.Zero : new Vector2((float)Main.offScreenRange));
		Vector2 drawOffset = new Vector2((float)(i * 16) - Main.screenPosition.X, (float)(j * 16) - Main.screenPosition.Y) + zero;
		for (int x = -1; x < 2; x++)
		{
			for (int y = -1; y < 2; y++)
			{
				Vector2 flameOffset = Utils.SafeNormalize(new Vector2((float)x, (float)y), Vector2.Zero);
				flameOffset *= 1.5f;
				Main.spriteBatch.Draw(flameTexture, drawOffset + flameOffset, (Rectangle?)new Rectangle(xPos + offsetX, yPos + offsetY, 18, 18), drawColour, 0f, Vector2.Zero, 1f, (SpriteEffects)0, 0f);
			}
		}
	}

	public static void DrawFlameSparks(int dustType, int rarity, int i, int j)
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.gamePaused && ((Game)Main.instance).IsActive && !Main.tile[i, j].IsTileActuallyInvisible() && (!Lighting.UpdateEveryFrame || Main.rand.NextBool(4)) && Main.rand.NextBool(rarity))
		{
			int dust = Dust.NewDust(new Vector2((float)(i * 16 + 4), (float)(j * 16 + 2)), 4, 4, dustType, 0f, 0f, 100);
			if (!Main.rand.NextBool(3))
			{
				Main.dust[dust].noGravity = true;
			}
			Main.dust[dust].noLightEmittence = true;
			Dust obj = Main.dust[dust];
			obj.velocity *= 0.3f;
			Main.dust[dust].velocity.Y = Main.dust[dust].velocity.Y - 1.5f;
		}
	}

	public static void DrawItemFlame(Texture2D flameTexture, Item item)
	{
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		int width = flameTexture.Width;
		int height = flameTexture.Height;
		float drawPositionX = item.position.X - Main.screenPosition.X + (float)item.width * 0.5f;
		float drawPositionY = item.position.Y - Main.screenPosition.Y + (float)item.height - (float)flameTexture.Height * 0.5f + 2f;
		for (int c = 0; c < 7; c++)
		{
			float shakeX = (float)Main.rand.Next(-10, 11) * 0.15f;
			float shakeY = (float)Main.rand.Next(-10, 1) * 0.35f;
			Main.spriteBatch.Draw(flameTexture, new Vector2(drawPositionX + shakeX, drawPositionY + shakeY), (Rectangle?)new Rectangle(0, 0, width, height), new Color(100, 100, 100, 0), 0f, default(Vector2), 1f, (SpriteEffects)0, 0f);
		}
	}

	internal static int GetAnimationOffset(this ModTile mt, int i, int j, int frameAmt, int xLength, int yLength, int xTiles, int yTiles, int animationFrameLength)
	{
		int frameX = Main.tile[i, j].TileFrameX;
		int frameY = Main.tile[i, j].TileFrameY;
		frameX %= xLength * xTiles;
		i -= frameX / xLength;
		frameY %= yLength * yTiles;
		j -= frameY / yLength;
		int uniqueAnimationFrame = Main.tileFrame[mt.Type] + j;
		if (i % 2 == 0)
		{
			uniqueAnimationFrame += 3;
		}
		if (i % 3 == 0)
		{
			uniqueAnimationFrame += 3;
		}
		if (i % 4 == 0)
		{
			uniqueAnimationFrame += 3;
		}
		if (j % 2 == 0)
		{
			uniqueAnimationFrame += 3;
		}
		if (j % 3 == 0)
		{
			uniqueAnimationFrame += 3;
		}
		if (j % 4 == 0)
		{
			uniqueAnimationFrame += 3;
		}
		uniqueAnimationFrame %= frameAmt;
		return uniqueAnimationFrame * animationFrameLength;
	}

	public static bool IsTileActuallyInvisible(this Tile tile)
	{
		if (tile.IsTileInvisible)
		{
			return !Main.ShouldShowInvisibleWalls();
		}
		return false;
	}

	public static Color ApplyPaint(int paintType, Color color, bool deepPaintOnly = true)
	{
		//IL_0003: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		if (paintType == 0)
		{
			return color;
		}
		bool isDeep = IsDeepPaint(paintType);
		if (deepPaintOnly && !isDeep)
		{
			return color;
		}
		Color paintCol = WorldGen.paintColor(paintType);
		if (paintType < 13)
		{
			((Color)(ref paintCol)).R = (byte)((float)(int)((Color)(ref paintCol)).R / 2f + 128f);
			((Color)(ref paintCol)).G = (byte)((float)(int)((Color)(ref paintCol)).G / 2f + 128f);
			((Color)(ref paintCol)).B = (byte)((float)(int)((Color)(ref paintCol)).B / 2f + 128f);
		}
		if (paintType == 29)
		{
			paintCol = Color.Black;
		}
		color = color.MultiplyRGB(paintCol);
		return color;
	}

	private static bool IsDeepPaint(int paintType)
	{
		if (13 >= paintType)
		{
			return paintType <= 24;
		}
		return false;
	}

	public static Tile ParanoidTileRetrieval(int x, int y)
	{
		if (!WorldGen.InWorld(x, y))
		{
			return default(Tile);
		}
		return Main.tile[x, y];
	}

	public static bool AnySolidTileInSelection(int x, int y, int width, int height)
	{
		for (int i = x; i != x + width; i += Math.Sign(width))
		{
			for (int j = y; j != y + height; j += Math.Sign(height))
			{
				if (WorldGen.InWorld(i, j) && WorldGen.SolidTile(Framing.GetTileSafely(i, j)))
				{
					return true;
				}
			}
		}
		return false;
	}

	public static bool TileSelectionSolid(int x, int y, int width, int height)
	{
		for (int i = x; i != x + width; i += Math.Sign(width))
		{
			int j = y;
			while (y != y + height)
			{
				if (!WorldGen.InWorld(i, j))
				{
					return false;
				}
				if (!WorldGen.SolidTile(Framing.GetTileSafely(i, j)))
				{
					return false;
				}
				j += Math.Sign(height);
			}
		}
		return true;
	}

	public static bool TileSelectionSolidSquare(int x, int y, int width, int height)
	{
		for (int i = x - width; i != x + width; i += Math.Sign(width))
		{
			int j = y - height;
			while (y != y + height)
			{
				if (!WorldGen.InWorld(i, j))
				{
					return false;
				}
				if (!WorldGen.SolidTile(Framing.GetTileSafely(i, j)))
				{
					return false;
				}
				j += Math.Sign(height);
			}
		}
		return true;
	}

	public static bool IsTileExposedToAir(int x, int y)
	{
		float? angleToOpenAir;
		return IsTileExposedToAir(x, y, out angleToOpenAir);
	}

	public static bool IsTileExposedToAir(int x, int y, out float? angleToOpenAir)
	{
		angleToOpenAir = null;
		if (!ParanoidTileRetrieval(x - 1, y).HasTile)
		{
			angleToOpenAir = (float)Math.PI;
			return true;
		}
		if (!ParanoidTileRetrieval(x + 1, y).HasTile)
		{
			angleToOpenAir = 0f;
			return true;
		}
		if (!ParanoidTileRetrieval(x, y - 1).HasTile)
		{
			angleToOpenAir = (float)Math.PI / 2f;
			return true;
		}
		if (!ParanoidTileRetrieval(x, y + 1).HasTile)
		{
			angleToOpenAir = -(float)Math.PI / 2f;
			return true;
		}
		return false;
	}

	public static bool TileActiveAndOfType(int x, int y, int type)
	{
		if (ParanoidTileRetrieval(x, y).HasTile)
		{
			return ParanoidTileRetrieval(x, y).TileType == type;
		}
		return false;
	}

	[Obsolete("Use TileBlendMergeSystem.RegisterBlendMergeWith Instead")]
	public static void RegisterUniversalMerge(this ModTile tile, int mergeType, string blendSheetPath)
	{
		TileBlendMergeSystem.RegisterMerge(tile.Type, mergeType);
	}

	public static void RegisterBlendMergeWith(this ModTile tile, int mergeType)
	{
		TileBlendMergeSystem.RegisterMerge(tile.Type, mergeType);
	}

	public static void SetMerge(int type1, int type2, bool merge = true)
	{
		if (type1 != type2)
		{
			Main.tileMerge[type1][type2] = merge;
			Main.tileMerge[type2][type1] = merge;
		}
	}

	public static void MergeWithSet(int myType, params int[] otherTypes)
	{
		for (int i = 0; i < otherTypes.Length; i++)
		{
			SetMerge(myType, otherTypes[i]);
		}
	}

	public static void MergeWithGeneral(int type)
	{
		int[] obj = new int[31]
		{
			0, 59, 40, 1, 25, 203, 117, 53, 112, 234,
			116, 147, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0
		};
		obj[12] = ModContent.TileType<AstralDirt>();
		obj[13] = ModContent.TileType<AstralClay>();
		obj[14] = ModContent.TileType<AstralStone>();
		obj[15] = ModContent.TileType<AstralSand>();
		obj[16] = ModContent.TileType<AstralSnow>();
		obj[17] = ModContent.TileType<Driftwood>();
		obj[18] = ModContent.TileType<PinkPearlPile>();
		obj[19] = ModContent.TileType<BlackPearlPile>();
		obj[20] = ModContent.TileType<WhitePearlPile>();
		obj[21] = ModContent.TileType<Shellstone>();
		obj[22] = ModContent.TileType<Dunesand>();
		obj[23] = ModContent.TileType<Navystone>();
		obj[24] = ModContent.TileType<EutrophicSand>();
		obj[25] = ModContent.TileType<Limestone>();
		obj[26] = ModContent.TileType<LimestoneCobble>();
		obj[27] = ModContent.TileType<SulphurousShale>();
		obj[28] = ModContent.TileType<AbyssGravel>();
		obj[29] = ModContent.TileType<Voidstone>();
		obj[30] = ModContent.TileType<MossyStone>();
		MergeWithSet(type, obj);
	}

	public static void MergeWithOres(int type)
	{
		int[] obj = new int[25]
		{
			7, 166, 6, 167, 9, 168, 8, 169, 22, 204,
			107, 221, 108, 222, 111, 223, 408, 0, 0, 0,
			0, 0, 0, 0, 0
		};
		obj[17] = ModContent.TileType<AerialiteOre>();
		obj[18] = ModContent.TileType<CryonicOre>();
		obj[19] = ModContent.TileType<PerennialOre>();
		obj[20] = ModContent.TileType<InfernalSuevite>();
		obj[21] = ModContent.TileType<ScoriaOre>();
		obj[22] = ModContent.TileType<AstralOre>();
		obj[23] = ModContent.TileType<UelibloomOre>();
		obj[24] = ModContent.TileType<AuricOre>();
		MergeWithSet(type, obj);
	}

	public static void MergeWithDesert(int type)
	{
		int[] obj = new int[32]
		{
			53, 112, 234, 116, 397, 398, 399, 402, 396, 400,
			401, 403, 407, 404, 0, 0, 0, 0, 0, 0,
			0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
			0, 0
		};
		obj[14] = ModContent.TileType<AstralSand>();
		obj[15] = ModContent.TileType<HardenedAstralSand>();
		obj[16] = ModContent.TileType<AstralSandstone>();
		obj[17] = ModContent.TileType<CelestialRemains>();
		obj[18] = ModContent.TileType<Limestone>();
		obj[19] = ModContent.TileType<LimestoneCobble>();
		obj[20] = ModContent.TileType<PolypSand>();
		obj[21] = ModContent.TileType<VolcanicSand>();
		obj[22] = ModContent.TileType<PinkPearlPile>();
		obj[23] = ModContent.TileType<BlackPearlPile>();
		obj[24] = ModContent.TileType<WhitePearlPile>();
		obj[25] = ModContent.TileType<Shellstone>();
		obj[26] = ModContent.TileType<Runestone>();
		obj[27] = ModContent.TileType<Dunesand>();
		obj[28] = ModContent.TileType<EutrophicSand>();
		obj[29] = ModContent.TileType<Navystone>();
		obj[30] = ModContent.TileType<SeaPrism>();
		obj[31] = ModContent.TileType<MossyStone>();
		MergeWithSet(type, obj);
	}

	public static void MergeWithSnow(int type)
	{
		int[] obj = new int[8] { 147, 161, 163, 200, 164, 0, 0, 0 };
		obj[5] = ModContent.TileType<AstralIce>();
		obj[6] = ModContent.TileType<AstralSnow>();
		obj[7] = ModContent.TileType<NovaeSlag>();
		MergeWithSet(type, obj);
	}

	public static void MergeWithHell(int type)
	{
		int[] obj = new int[8] { 57, 58, 75, 76, 0, 0, 0, 0 };
		obj[4] = ModContent.TileType<BrimstoneSlag>();
		obj[5] = ModContent.TileType<BrimstoneSlab>();
		obj[6] = ModContent.TileType<ScorchedRemains>();
		obj[7] = ModContent.TileType<ScorchedRemainsGrass>();
		MergeWithSet(type, obj);
	}

	public static void MergeWithAbyss(int type)
	{
		MergeWithSet(type, ModContent.TileType<SulphurousSand>(), ModContent.TileType<SulphurousSandstone>(), ModContent.TileType<SulphurousShale>(), ModContent.TileType<AbyssGravel>(), ModContent.TileType<PyreMantle>(), ModContent.TileType<PyreMantleMolten>(), ModContent.TileType<Voidstone>(), ModContent.TileType<PlantyMush>(), ModContent.TileType<ScoriaOre>());
	}

	public static void MergeAstralTiles(int type)
	{
		SetMerge(type, ModContent.TileType<AstralDirt>());
		SetMerge(type, ModContent.TileType<AstralStone>());
		SetMerge(type, ModContent.TileType<AstralMonolith>());
		SetMerge(type, ModContent.TileType<AstralClay>());
		SetMerge(type, ModContent.TileType<AstralSand>());
		SetMerge(type, ModContent.TileType<HardenedAstralSand>());
		SetMerge(type, ModContent.TileType<AstralSandstone>());
		SetMerge(type, ModContent.TileType<CelestialRemains>());
		SetMerge(type, ModContent.TileType<AstralIce>());
		SetMerge(type, ModContent.TileType<AstralSnow>());
	}

	public static void MergeSmoothTiles(int type)
	{
		SetMerge(type, 357);
		SetMerge(type, 369);
		SetMerge(type, ModContent.TileType<AncientSmoothNavystone>());
		SetMerge(type, ModContent.TileType<SmoothNavystone>());
		SetMerge(type, ModContent.TileType<SmoothBrimstoneSlag>());
		SetMerge(type, ModContent.TileType<SmoothAbyssGravel>());
		SetMerge(type, ModContent.TileType<SmoothVoidstone>());
	}

	public static void MergeDecorativeTiles(int type)
	{
		Main.tileBrick[type] = true;
		SetMerge(type, ModContent.TileType<CryonicBrick>());
		SetMerge(type, ModContent.TileType<PerennialBrick>());
		SetMerge(type, ModContent.TileType<UelibloomBrick>());
		SetMerge(type, ModContent.TileType<OtherworldlyStone>());
		SetMerge(type, ModContent.TileType<ProfanedSlab>());
		SetMerge(type, ModContent.TileType<RunicProfanedBrick>());
		SetMerge(type, ModContent.TileType<AshenSlab>());
		SetMerge(type, ModContent.TileType<VoidstoneSlab>());
		SetMerge(type, ModContent.TileType<WulfrumPanels>());
		SetMerge(type, ModContent.TileType<WulfrumSiding>());
		SetMerge(type, ModContent.TileType<WulfrumPlating>());
		SetMerge(type, ModContent.TileType<WulfrumEnergyBarrier>());
		SetMerge(type, ModContent.TileType<RoundedAnodizedWulfrumPanels>());
		SetMerge(type, ModContent.TileType<AnodizedWulfrumTrim>());
		SetMerge(type, ModContent.TileType<AnodizedWulfrumPanels>());
	}

	public static int X(this Tile tile)
	{
		tile.TilePos(out var x, out var _);
		return x;
	}

	public static int Y(this Tile tile)
	{
		tile.TilePos(out var _, out var y);
		return y;
	}

	public static void TilePos(this Tile tile, out int x, out int y)
	{
		uint tileId = Unsafe.BitCast<Tile, uint>(tile);
		x = Math.DivRem((int)tileId, Main.tile.Height, out y);
	}

	public static bool IsTileSolidGround(this Tile tile)
	{
		if (tile != null && tile.HasUnactuatedTile)
		{
			if (!Main.tileSolid[tile.TileType])
			{
				return Main.tileSolidTop[tile.TileType];
			}
			return true;
		}
		return false;
	}

	public static bool IsTileSolid(this Tile tile)
	{
		if (tile != null && tile.HasUnactuatedTile && Main.tileSolid[tile.TileType])
		{
			return !Main.tileSolidTop[tile.TileType];
		}
		return false;
	}

	public static bool IsTileFull(this Tile tile)
	{
		if (tile != null && tile.HasTile)
		{
			return Main.tileSolid[tile.TileType];
		}
		return false;
	}

	public static float GetTileRNG(this Point tilePos, int shift = 0)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		return (float)(Math.Sin((double)tilePos.X * 17.07947 + (double)(shift * 36)) + Math.Sin((double)tilePos.Y * 25.13274)) * 0.25f + 0.5f;
	}

	public static Point GetNearestPointInDirection(this Point origin, float direction)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		return origin + new Point((int)Math.Round(Math.Cos(direction)), (int)Math.Round(Math.Sin(direction)));
	}

	public static Point ToSafeTileCoordinates(this Vector2 vec)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		return new Point(MathHelper.Clamp((int)vec.X >> 4, 0, Main.maxTilesX), MathHelper.Clamp((int)vec.Y >> 4, 0, Main.maxTilesY));
	}

	public static bool CanTileBeLatchedOnTo(this Tile theTile, bool grappleOnTrees = false)
	{
		return Main.tileSolid[theTile.TileType] | (theTile.TileType == 314) | (grappleOnTrees && TileID.Sets.IsATreeTrunk[theTile.TileType]) | (grappleOnTrees && theTile.TileType == 323);
	}

	public static int GetRequiredPickPower(this Tile tile, int i, int j)
	{
		int pickReq = 0;
		if (Main.tileNoFail[tile.TileType])
		{
			return pickReq;
		}
		ModTile moddedTile = TileLoader.GetTile(tile.TileType);
		if (moddedTile != null)
		{
			pickReq = moddedTile.MinPick;
		}
		else
		{
			switch (tile.TileType)
			{
			case 211:
				pickReq = 200;
				break;
			case 25:
			case 58:
			case 117:
			case 203:
				pickReq = 65;
				break;
			case 56:
				pickReq = 55;
				break;
			case 37:
				pickReq = 50;
				break;
			case 22:
			case 204:
				if ((double)j > Main.worldSurface)
				{
					pickReq = 55;
				}
				break;
			case 226:
			case 237:
				pickReq = 210;
				break;
			case 107:
			case 221:
				pickReq = 100;
				break;
			case 108:
			case 222:
				pickReq = 110;
				break;
			case 111:
			case 223:
				pickReq = 150;
				break;
			}
		}
		if (Main.tileDungeon[tile.TileType] && (double)j > Main.worldSurface)
		{
			pickReq = 100;
		}
		return pickReq;
	}

	public static bool ShouldBeMined(this Tile tile, bool ignoreAbyss = true)
	{
		List<int> tileExcludeList = new List<int> { 26, 466, 237, 88, 21 };
		if (ignoreAbyss)
		{
			tileExcludeList.Add(ModContent.TileType<AbyssGravel>());
			tileExcludeList.Add(ModContent.TileType<PyreMantle>());
			tileExcludeList.Add(ModContent.TileType<PyreMantleMolten>());
			tileExcludeList.Add(ModContent.TileType<Voidstone>());
		}
		if (!Main.tileContainer[tile.TileType])
		{
			return !tileExcludeList.Contains(tile.TileType);
		}
		return false;
	}

	public static int PixelsToTiles(this int pixels)
	{
		return pixels / 16;
	}

	public static int TilesToPixels(this int tiles)
	{
		return tiles * 16;
	}

	public static void SpawnOre(int type, double frequency, float verticalStartFactor, float verticalEndFactor, int strengthMin, int strengthMax, params int[] convertibleTiles)
	{
		int x = Main.maxTilesX;
		int y = Main.maxTilesY;
		if (Main.netMode == 1)
		{
			return;
		}
		for (int k = 0; k < (int)((double)(x * y) * frequency); k++)
		{
			int tilesX = WorldGen.genRand.Next(0, x);
			int tilesY = WorldGen.genRand.Next((int)((float)y * verticalStartFactor), (int)((float)y * verticalEndFactor));
			if (convertibleTiles.Length == 0 || convertibleTiles.Contains(ParanoidTileRetrieval(tilesX, tilesY).TileType))
			{
				WorldGen.OreRunner(tilesX, tilesY, WorldGen.genRand.Next(strengthMin, strengthMax), WorldGen.genRand.Next(3, 8), (ushort)type);
			}
		}
	}

	public static void SpawnOre(int type, double frequency, float verticalStartFactor, float verticalEndFactor, int strengthMin, int strengthMax, List<int> convertibleTiles)
	{
		int[] convertedArray = new int[convertibleTiles.Count];
		convertibleTiles.CopyTo(convertedArray);
		SpawnOre(type, frequency, verticalStartFactor, verticalEndFactor, strengthMin, strengthMax, convertedArray);
	}

	public static int CountCellsAtPosition(bool[,] map, int x, int y, bool checkForActiveCells)
	{
		int count = 0;
		for (int dx = -1; dx <= 1; dx++)
		{
			for (int dy = -1; dy <= 1; dy++)
			{
				if ((dx != 0 || dy != 0) && x + dx >= 0 && x + dx < map.GetLength(0) && y + dy >= 0 && y + dy < map.GetLength(1) && ((map[x + dx, y + dy] & checkForActiveCells) || (!map[x + dx, y + dy] && !checkForActiveCells)))
				{
					count++;
				}
			}
		}
		return count;
	}

	public static bool[,] SimulateCelluarAutomata(bool[,] originalMap)
	{
		bool[,] newMap = (bool[,])originalMap.Clone();
		for (int x = 0; x < originalMap.GetLength(0); x++)
		{
			for (int y = 0; y < originalMap.GetLength(1); y++)
			{
				if (originalMap[x, y] && CountCellsAtPosition(originalMap, x, y, checkForActiveCells: true) <= 3)
				{
					newMap[x, y] = false;
				}
				else if (!originalMap[x, y] && CountCellsAtPosition(originalMap, x, y, checkForActiveCells: true) >= 5)
				{
					newMap[x, y] = true;
				}
			}
		}
		return newMap;
	}

	public static void GrowVines(int VineX, int VineY, int numVines, ushort vineType, bool finished = false)
	{
		for (int Y = VineY; Y <= VineY + numVines; Y++)
		{
			if (finished)
			{
				break;
			}
			Tile tileBelow = Framing.GetTileSafely(VineX, Y + 1);
			if ((!tileBelow.HasTile || tileBelow.TileType == 51) && WorldGen.InWorld(VineX, Y))
			{
				WorldGen.PlaceTile(VineX, Y, vineType);
			}
			else
			{
				finished = true;
			}
			if (numVines <= 1)
			{
				finished = true;
			}
		}
	}

	public static void SettleWater(bool convertToLava = true)
	{
		Liquid.worldGenTilesIgnoreWater(ignoreSolids: true);
		if (convertToLava)
		{
			Liquid.QuickWater(3);
		}
		else
		{
			int waterLine = GenVars.waterLine;
			GenVars.waterLine = Main.maxTilesY;
			Liquid.QuickWater(3);
			GenVars.waterLine = waterLine;
		}
		WorldGen.WaterCheck();
		Liquid.quickSettle = true;
		for (int i = 0; i < 10; i++)
		{
			int maxLiquid = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
			int m = maxLiquid * 5;
			double maxLiquidDifferencePercentage = 0.0;
			while (Liquid.numLiquid > 0)
			{
				m--;
				if (m < 0)
				{
					break;
				}
				double liquidDifferencePercentage = (double)(maxLiquid - Liquid.numLiquid - LiquidBuffer.numLiquidBuffer) / (double)maxLiquid;
				if (Liquid.numLiquid + LiquidBuffer.numLiquidBuffer > maxLiquid)
				{
					maxLiquid = Liquid.numLiquid + LiquidBuffer.numLiquidBuffer;
				}
				if (liquidDifferencePercentage > maxLiquidDifferencePercentage)
				{
					maxLiquidDifferencePercentage = liquidDifferencePercentage;
				}
				Liquid.UpdateLiquid();
			}
			WorldGen.WaterCheck();
		}
		Liquid.quickSettle = false;
		Liquid.worldGenTilesIgnoreWater(ignoreSolids: false);
	}

	public static Rectangle GetSchematicProtectionArea(SchematicMetaTile[,] schematic, Point placementPoint, SchematicAnchor anchorType)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		int width = schematic.GetLength(0);
		int height = schematic.GetLength(1);
		int cornerX = placementPoint.X;
		int cornerY = placementPoint.Y;
		switch (anchorType)
		{
		case SchematicAnchor.TopCenter:
			cornerX -= width / 2;
			break;
		case SchematicAnchor.TopRight:
			cornerX -= width;
			break;
		case SchematicAnchor.CenterLeft:
			cornerY -= height / 2;
			break;
		case SchematicAnchor.Center:
			cornerX -= width / 2;
			cornerY -= height / 2;
			break;
		case SchematicAnchor.CenterRight:
			cornerX -= width;
			cornerY -= height / 2;
			break;
		case SchematicAnchor.BottomLeft:
			cornerY -= height;
			break;
		case SchematicAnchor.BottomCenter:
			cornerX -= width / 2;
			cornerY -= height;
			break;
		case SchematicAnchor.BottomRight:
			cornerX -= width;
			cornerY -= height;
			break;
		}
		return new Rectangle(cornerX, cornerY, width, height);
	}

	public static void AddProtectedStructure(Rectangle area, int padding = 0)
	{
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		GenVars.structures.AddProtectedStructure(area, padding);
		Rectangle paddedArea = default(Rectangle);
		((Rectangle)(ref paddedArea))._002Ector(area.X, area.Y, area.Width, area.Height);
		((Rectangle)(ref paddedArea)).Inflate(padding, padding);
		Mod fargos = ExternalMods.fargos;
		paddedArea.X *= 16;
		paddedArea.Y *= 16;
		paddedArea.Width *= 16;
		paddedArea.Height *= 16;
		fargos?.Call("AddIndestructibleRectangle", paddedArea);
	}

	static CalamityUtils()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		ExoPalette = (Color[])(object)new Color[8]
		{
			new Color(250, 255, 112),
			new Color(211, 235, 108),
			new Color(166, 240, 105),
			new Color(105, 240, 220),
			new Color(64, 130, 145),
			new Color(145, 96, 145),
			new Color(242, 112, 73),
			new Color(199, 62, 62)
		};
		DevItemColor = new Color(255, 0, 255);
		DonatorItemColor = new Color(255, 121, 156);
		debuffColorWeightsCache = new Dictionary<int, List<(Color, float)>>();
		Directions = new List<Vector2>
		{
			new Vector2(-1f, -1f),
			new Vector2(1f, -1f),
			new Vector2(-1f, 1f),
			new Vector2(1f, 1f),
			new Vector2(0f, -1f),
			new Vector2(-1f, 0f),
			new Vector2(0f, 1f),
			new Vector2(1f, 0f)
		};
		EasingTypeToFunction = new EasingFunction[14]
		{
			LinearEasing, SineInEasing, SineOutEasing, SineInOutEasing, SineBumpEasing, PolyInEasing, PolyOutEasing, PolyInOutEasing, ExpInEasing, ExpOutEasing,
			ExpInOutEasing, CircInEasing, CircOutEasing, CircInOutEasing
		};
		vanillaBlastImmuneTiles = new List<int> { 107, 108, 111, 221, 222, 223, 211, 226, 237 };
		AlphanumericCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
	}
}
