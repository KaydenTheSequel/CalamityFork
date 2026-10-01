using System;
using CalamityMod.Items.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Systems.Mechanic;

public class WulfrumPingTileEffect : IPingedTileEffect, ILoadable
{
	internal static Texture2D emptyFrame;

	public const int MaxPingLife = 350;

	public const int MaxPingTravelTime = 60;

	private const float PingWaveThickness = 50f;

	public const float MaxPingRadius = 1700f;

	public static Vector2 PingCenter;

	public static int PingTimer;

	public static string EffectName => "WulfrumPing";

	public static WulfrumPingTileEffect Instance { get; private set; }

	public static float PingProgress => (float)(350 - PingTimer) / 350f;

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
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (Active)
		{
			return false;
		}
		PingCenter = position;
		PingTimer = 350;
		return true;
	}

	public Effect SetupEffect()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_024e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0253: Unknown result type (might be due to invalid IL or missing references)
		//IL_026e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_028e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d3: Unknown result type (might be due to invalid IL or missing references)
		if (emptyFrame == null)
		{
			emptyFrame = ModContent.Request<Texture2D>("CalamityMod/Projectiles/InvisibleProj", (AssetRequestMode)2).Value;
		}
		Effect tileEffect = Filters.Scene["CalamityMod:WulfrumTilePing"].GetShader().Shader;
		tileEffect.Parameters["pingCenter"].SetValue(PingCenter);
		tileEffect.Parameters["pingRadius"].SetValue(1700f);
		tileEffect.Parameters["pingWaveThickness"].SetValue(50f);
		tileEffect.Parameters["pingProgress"].SetValue(PingProgress);
		tileEffect.Parameters["pingTravelTime"].SetValue(6f / 35f);
		tileEffect.Parameters["pingFadePoint"].SetValue(0.9f);
		tileEffect.Parameters["edgeBlendStrength"].SetValue(1f);
		tileEffect.Parameters["edgeBlendOutLength"].SetValue(6f);
		tileEffect.Parameters["tileEdgeBlendStrenght"].SetValue(2f);
		EffectParameter obj = tileEffect.Parameters["waveColor"];
		Color val = Color.GreenYellow;
		obj.SetValue(((Color)(ref val)).ToVector4());
		EffectParameter obj2 = tileEffect.Parameters["baseTintColor"];
		val = Color.DeepSkyBlue;
		obj2.SetValue(((Color)(ref val)).ToVector4() * 0.5f);
		EffectParameter obj3 = tileEffect.Parameters["scanlineColor"];
		val = Color.YellowGreen;
		obj3.SetValue(((Color)(ref val)).ToVector4() * 1f);
		EffectParameter obj4 = tileEffect.Parameters["tileEdgeColor"];
		val = Color.GreenYellow;
		obj4.SetValue(((Color)(ref val)).ToVector3());
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
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		if (Main.IsTileSpelunkable(pos.X + displaceX, pos.Y + displaceY))
		{
			return Main.tile[pos].TileType == Main.tile[pos.X + displaceX, pos.Y + displaceY].TileType;
		}
		return false;
	}

	public bool ShouldRegisterTile(int x, int y)
	{
		return Main.IsTileSpelunkable(x, y);
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
		float currentExpansion = MathHelper.Clamp(PingProgress * 350f / 60f, 0f, 1f) * 1700f;
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
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		if (PingTimer > 0)
		{
			PingTimer--;
			if (PingTimer == 0 && Main.LocalPlayer.InventoryHas(ModContent.ItemType<WulfrumTreasurePinger>()))
			{
				SoundEngine.PlaySound(in WulfrumTreasurePinger.RechargeBeepSound);
			}
		}
	}

	static WulfrumPingTileEffect()
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		PingCenter = Vector2.Zero;
		PingTimer = 0;
	}
}
