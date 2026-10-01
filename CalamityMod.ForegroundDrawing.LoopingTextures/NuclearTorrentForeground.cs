using System;
using System.Collections.Generic;
using CalamityMod.NPCs.OldDuke;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.ForegroundDrawing.LoopingTextures;

public class NuclearTorrentForeground : LoopingTextureForeground
{
	public class NuclearRaindrop
	{
		public Vector2 Position;

		public Vector2 Velocity;

		public NuclearRaindrop(Vector2 pos, Vector2 vel)
		{
			//IL_0007: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000e: Unknown result type (might be due to invalid IL or missing references)
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			base._002Ector();
			Position = pos;
			Velocity = vel;
		}

		public void Update(NuclearRaindrop drop, NuclearTorrentForeground foreground)
		{
			//IL_0002: Unknown result type (might be due to invalid IL or missing references)
			//IL_0008: Unknown result type (might be due to invalid IL or missing references)
			//IL_000d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0012: Unknown result type (might be due to invalid IL or missing references)
			//IL_0019: Unknown result type (might be due to invalid IL or missing references)
			//IL_0023: Unknown result type (might be due to invalid IL or missing references)
			//IL_0028: Unknown result type (might be due to invalid IL or missing references)
			//IL_002d: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0051: Unknown result type (might be due to invalid IL or missing references)
			//IL_0052: Unknown result type (might be due to invalid IL or missing references)
			//IL_0057: Unknown result type (might be due to invalid IL or missing references)
			drop.Position += drop.Velocity;
			drop.Position -= Main.LocalPlayer.velocity;
			drop.Velocity = Utils.RotatedBy(new Vector2(0f, 20f), (double)(0f - Main.windSpeedCurrent), default(Vector2));
			if (drop.Position.Y > (float)(Main.screenHeight + 200))
			{
				RemoveDrop(drop);
			}
		}

		public void Draw(NuclearRaindrop drop, NuclearTorrentForeground foreground)
		{
			//IL_0027: Unknown result type (might be due to invalid IL or missing references)
			//IL_0033: Unknown result type (might be due to invalid IL or missing references)
			//IL_003d: Unknown result type (might be due to invalid IL or missing references)
			//IL_0046: Unknown result type (might be due to invalid IL or missing references)
			//IL_004b: Unknown result type (might be due to invalid IL or missing references)
			//IL_0050: Unknown result type (might be due to invalid IL or missing references)
			//IL_0056: Unknown result type (might be due to invalid IL or missing references)
			//IL_006c: Unknown result type (might be due to invalid IL or missing references)
			//IL_0076: Unknown result type (might be due to invalid IL or missing references)
			//IL_0085: Unknown result type (might be due to invalid IL or missing references)
			Asset<Texture2D> tex = ModContent.Request<Texture2D>("CalamityMod/ForegroundDrawing/LoopingTextures/NuclearTorrentRaindrop", (AssetRequestMode)2);
			float intensity = foreground.Intensity / foreground.IntensityMaximum / 4f;
			Main.EntitySpriteDraw(tex.Value, drop.Position, tex.Frame(), Color.White.MultiplyRGBA(new Color(intensity, intensity, intensity, intensity)), Vector2.Zero.AngleTo(drop.Velocity) - MathHelper.ToRadians(90f), tex.Size() / 2f, new Vector2(1f, 4f), (SpriteEffects)0);
		}
	}

	public static List<NuclearRaindrop> Raindrops = new List<NuclearRaindrop>();

	public override Vector2 ParallaxDepth
	{
		get
		{
			//IL_000a: Unknown result type (might be due to invalid IL or missing references)
			return new Vector2(1f, 1f);
		}
	}

	public override float IntensityMaximum => 0.045f;

	public static void RemoveDrop(NuclearRaindrop drop)
	{
		Raindrops.Remove(drop);
	}

	public bool ShouldDisplayDuringOldDuke()
	{
		if (NPC.CountNPCS(ModContent.NPCType<OldDuke>()) > 0 && Main.LocalPlayer.Calamity().ZoneSulphur)
		{
			return !CalamityServerConfig.Instance.BossesStopWeather;
		}
		return false;
	}

	public override bool DoesThisShow()
	{
		if (!ShouldDisplayDuringOldDuke())
		{
			return Main.LocalPlayer.GetModPlayer<NuclearTorrentPlayer>().ShouldDisplayTorrentMonolith;
		}
		return true;
	}

	public override void Update()
	{
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		if (NPC.CountNPCS(ModContent.NPCType<OldDuke>()) > 0)
		{
			NPC.FindFirstNPC(ModContent.NPCType<OldDuke>());
			Main.windSpeedCurrent = MathHelper.Lerp(Main.windSpeedCurrent, MathHelper.ToRadians(50f), 0.005f);
			Rain[] rain = Main.rain;
			foreach (Rain rain2 in rain)
			{
				rain2.velocity = Utils.RotatedBy(new Vector2(0f, 15f), (double)(0f - Main.windSpeedCurrent), default(Vector2));
				rain2.rotation = Vector2.Zero.AngleTo(rain2.velocity) + (float)Math.PI / 2f;
			}
		}
		NuclearRaindrop dr = new NuclearRaindrop(new Vector2((float)Main.screenWidth / 2f, -100f) + Utils.RotatedBy(new Vector2(Main.rand.NextFloat(-Main.screenWidth, Main.screenWidth), (float)(-Main.screenWidth) * 0.65f), (double)(0f - Main.windSpeedCurrent), default(Vector2)), Utils.RotatedBy(new Vector2(0f, 20f), (double)(0f - Main.windSpeedCurrent), default(Vector2)));
		Raindrops.Add(dr);
		for (int j = 0; j < Raindrops.Count; j++)
		{
			NuclearRaindrop nuclearRaindrop = Raindrops[j];
			nuclearRaindrop.Update(nuclearRaindrop, this);
		}
	}

	public override void PostDraw()
	{
		for (int i = 0; i < Raindrops.Count; i++)
		{
			NuclearRaindrop nuclearRaindrop = Raindrops[i];
			nuclearRaindrop.Draw(nuclearRaindrop, this);
		}
	}
}
