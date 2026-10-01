using System;
using CalamityMod.Effects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Mechanic;

public class BurrowerPingTileEffect : IPingedTileEffect, ILoadable
{
	internal static Texture2D emptyFrame;

	public const int MaxPingLife = 60;

	public const int MaxPingTravelTime = 10;

	private const float PingWaveThickness = 50f;

	public const float MaxPingRadius = 160f;

	public static Vector2 PingCenter;

	public static int PingTimer;

	public static string EffectName => "BurrowerPing";

	public static BurrowerPingTileEffect Instance { get; private set; }

	public static float PingProgress => (float)(60 - PingTimer) / 60f;

	public bool Active => PingTimer > 0;

	public BlendState BlendState => BlendState.Additive;

	void ILoadable.Load(Mod mod)
	{
		Instance = this;
		TilePingerSystem.RegisterEffect(EffectName, this);
	}

	void ILoadable.Unload()
	{
		Instance = null;
	}

	public bool TryAddPing(Vector2 position, Player pinger)
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		if (Active)
		{
			PingCenter = position;
			PingTimer = Math.Max(PingTimer, 50);
			return false;
		}
		PingCenter = position;
		PingTimer = 60;
		return true;
	}

	public Effect SetupEffect()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ca: Unknown result type (might be due to invalid IL or missing references)
		if (emptyFrame == null)
		{
			emptyFrame = ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj", (AssetRequestMode)2).Value;
		}
		Effect tileEffect = Filters.Scene["CalamityMod:WulfrumTilePing"].GetShader().Shader;
		tileEffect.Parameters["pingCenter"].SetValue(PingCenter);
		tileEffect.Parameters["pingRadius"].SetValue(160f);
		tileEffect.Parameters["pingWaveThickness"].SetValue(50f);
		tileEffect.Parameters["pingProgress"].SetValue(PingProgress);
		tileEffect.Parameters["pingTravelTime"].SetValue(1f / 6f);
		tileEffect.Parameters["pingFadePoint"].SetValue(0.9f);
		tileEffect.Parameters["edgeBlendStrength"].SetValue(1f);
		tileEffect.Parameters["edgeBlendOutLength"].SetValue(6f);
		tileEffect.Parameters["tileEdgeBlendStrenght"].SetValue(2f);
		tileEffect.Parameters["waveColor"].SetValue(((Color)(ref ArsenalEffects.ArsenalGaussColor)).ToVector4());
		EffectParameter obj = tileEffect.Parameters["baseTintColor"];
		Color orange = Color.Orange;
		obj.SetValue(((Color)(ref orange)).ToVector4() * 0.5f);
		tileEffect.Parameters["scanlineColor"].SetValue(((Color)(ref ArsenalEffects.ArsenalLaserColor)).ToVector4() * 1f);
		tileEffect.Parameters["tileEdgeColor"].SetValue(((Color)(ref ArsenalEffects.ArsenalGaussColor)).ToVector3());
		tileEffect.Parameters["Resolution"].SetValue(8f);
		tileEffect.Parameters["time"].SetValue((float)Main.GameUpdateCount);
		Vector4[] scanLines = (Vector4[])(object)new Vector4[7]
		{
			new Vector4(0f, 4f, 0.1f, 0.5f),
			new Vector4(1f, 4f, 0.1f, 0.5f),
			new Vector4(37f, 60f, 0.4f, 1f),
			new Vector4(2f, 6f, -0.2f, 0.3f),
			new Vector4(0f, 4f, 0.1f, 0.5f),
			new Vector4(1f, 4f, 0.1f, 0.5f),
			new Vector4(2f, 6f, -0.2f, 0.3f)
		};
		tileEffect.Parameters["ScanLines"].SetValue(scanLines);
		tileEffect.Parameters["ScanLinesCount"].SetValue(scanLines.Length);
		tileEffect.Parameters["verticalScanLinesIndex"].SetValue(4);
		return tileEffect;
	}

	public void PerTileSetup(Point pos, ref Effect effect)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		effect.Parameters["cardinalConnections"].SetValue(new bool[4]
		{
			Connected(pos, 0, -1),
			Connected(pos, -1, 0),
			Connected(pos, 1, 0),
			Connected(pos, 0, 1)
		});
		effect.Parameters["tilePosition"].SetValue(pos.ToVector2() * 16f);
	}

	public static bool Connected(Point pos, int displaceX, int displaceY)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		if (TileID.Sets.Ore[Main.tile[pos.X + displaceX, pos.Y + displaceY].TileType])
		{
			return Main.tile[pos].TileType == Main.tile[pos.X + displaceX, pos.Y + displaceY].TileType;
		}
		return false;
	}

	public bool ShouldRegisterTile(int x, int y)
	{
		if (!Main.tile[x, y].HasTile)
		{
			return false;
		}
		return TileID.Sets.Ore[Main.tile[x, y].TileType];
	}

	public void ModifyTileLight(int x, int y, Color tileLight, ref Color resultColor)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		Vector2 val = Utils.ToWorldCoordinates(new Point(x, y), 8f, 8f) - PingCenter;
		float distanceFromCenter = ((Vector2)(ref val)).Length();
		float currentExpansion = MathHelper.Clamp(PingProgress * 60f / 10f, 0f, 1f) * 160f;
		if (!(distanceFromCenter - 8f > currentExpansion))
		{
			float brightness = 1f;
			Tile tile = Framing.GetTileSafely(x, y);
			if (tile.Slope != SlopeType.Solid || tile.IsHalfBlock)
			{
				brightness = 0.64f;
			}
			if (distanceFromCenter + 8f > currentExpansion)
			{
				brightness *= 1f - (distanceFromCenter - currentExpansion + 8f) / 16f;
			}
			brightness *= 1f - Math.Max(PingProgress - 0.9f, 0f) / 0.1f;
			if ((float)(int)((Color)(ref tileLight)).R < 200f * brightness)
			{
				((Color)(ref tileLight)).R = (byte)(200f * brightness);
			}
			if ((float)(int)((Color)(ref tileLight)).G < 200f * brightness)
			{
				((Color)(ref tileLight)).G = (byte)(200f * brightness);
			}
			if ((float)(int)((Color)(ref tileLight)).B < 200f * brightness)
			{
				((Color)(ref tileLight)).B = (byte)(200f * brightness);
			}
			resultColor = tileLight;
		}
	}

	public void DrawTile(Point pos)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		Main.spriteBatch.Draw(emptyFrame, pos.ToWorldCoordinates() - Main.screenPosition, (Rectangle?)null, Color.White, 0f, new Vector2((float)emptyFrame.Width / 2f, (float)emptyFrame.Height / 2f), 16f, (SpriteEffects)0, 0f);
	}

	public void UpdateEffect()
	{
		if (PingTimer > 0)
		{
			PingTimer--;
		}
	}

	static BurrowerPingTileEffect()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		PingCenter = Vector2.Zero;
		PingTimer = 0;
	}
}
