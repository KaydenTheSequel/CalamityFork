using System;
using System.Collections.Generic;
using CalamityMod.Events;
using CalamityMod.NPCs;
using CalamityMod.NPCs.ExoMechs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Skies;

public class ExoMechsSky : CustomSky
{
	public class Lightning
	{
		public int Lifetime;

		public float Depth;

		public Vector2 Position;
	}

	public float BackgroundIntensity;

	public float LightningIntensity;

	public List<Lightning> LightningBolts = new List<Lightning>();

	public static readonly Color DrawColor;

	public static bool CanSkyBeActive
	{
		get
		{
			if (BossRushEvent.BossRushActive)
			{
				return false;
			}
			int draedon = CalamityGlobalNPC.draedon;
			if (draedon == -1 || !Main.npc[draedon].active)
			{
				return Draedon.ExoMechIsPresent;
			}
			if ((Main.npc[draedon]?.ModNPC<Draedon>()?.DefeatTimer).GetValueOrDefault() <= 0f && !Draedon.ExoMechIsPresent)
			{
				return false;
			}
			return true;
		}
	}

	public static float CurrentIntensity
	{
		get
		{
			float combinedLifeRatio = 0f;
			if (CalamityGlobalNPC.draedonExoMechPrime != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechPrime].active)
			{
				combinedLifeRatio += (float)Main.npc[CalamityGlobalNPC.draedonExoMechPrime].life / (float)Main.npc[CalamityGlobalNPC.draedonExoMechPrime].lifeMax;
			}
			if (CalamityGlobalNPC.draedonExoMechTwinGreen != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].active)
			{
				combinedLifeRatio += (float)Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].life / (float)Main.npc[CalamityGlobalNPC.draedonExoMechTwinGreen].lifeMax;
			}
			if (CalamityGlobalNPC.draedonExoMechWorm != -1 && Main.npc[CalamityGlobalNPC.draedonExoMechWorm].active)
			{
				combinedLifeRatio += (float)Main.npc[CalamityGlobalNPC.draedonExoMechWorm].life / (float)Main.npc[CalamityGlobalNPC.draedonExoMechWorm].lifeMax;
			}
			if (combinedLifeRatio > 0f)
			{
				return (float)Math.Pow(1f - combinedLifeRatio / 3f, 2.0);
			}
			return MathHelper.Lerp(1f, 0f, (float)(Main.LocalPlayer.Calamity().monolithExoShader / 30));
		}
	}

	public static void CreateLightningBolt(int count = 1, bool playSound = false)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; i < count; i++)
			{
				Lightning lightning = new Lightning
				{
					Lifetime = 30,
					Depth = Main.rand.NextFloat(1.5f, 10f),
					Position = new Vector2(Main.LocalPlayer.Center.X + Main.rand.NextFloatDirection() * 5000f, Main.rand.NextFloat(4850f))
				};
				(SkyManager.Instance["CalamityMod:ExoMechs"] as ExoMechsSky).LightningBolts.Add(lightning);
			}
			if (count >= 10)
			{
				(SkyManager.Instance["CalamityMod:ExoMechs"] as ExoMechsSky).LightningIntensity = 1f;
				playSound = true;
			}
			if (playSound && !Main.gamePaused)
			{
				SoundStyle style = SoundID.Thunder with
				{
					Volume = SoundID.Thunder.Volume * 0.5f
				};
				SoundEngine.PlaySound(in style, Main.LocalPlayer.Center);
			}
		}
	}

	public override void Update(GameTime gameTime)
	{
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		if (!CanSkyBeActive)
		{
			Player localPlayer = Main.LocalPlayer;
			if (localPlayer != null && localPlayer.Calamity()?.monolithExoShader <= 0)
			{
				LightningIntensity = 0f;
				BackgroundIntensity = MathHelper.Clamp(BackgroundIntensity - 0.08f, 0f, 1f);
				LightningBolts.Clear();
				Deactivate();
				return;
			}
		}
		LightningIntensity = MathHelper.Clamp(LightningIntensity * 0.95f - 0.025f, 0f, 1f);
		BackgroundIntensity = MathHelper.Clamp(BackgroundIntensity + 0.01f, 0f, 1f);
		for (int i = 0; i < LightningBolts.Count; i++)
		{
			LightningBolts[i].Lifetime--;
		}
		if (Main.rand.NextBool((int)MathHelper.Lerp(50f, 195f, CurrentIntensity)))
		{
			CreateLightningBolt();
		}
		if (Main.rand.NextBool((int)MathHelper.Lerp(780f, 300f, CurrentIntensity)))
		{
			LightningIntensity = 1f;
			CreateLightningBolt(4);
			if (!Main.gamePaused)
			{
				SoundStyle style = SoundID.Thunder with
				{
					Volume = SoundID.Thunder.Volume * 0.5f
				};
				SoundEngine.PlaySound(in style, Main.LocalPlayer.Center);
			}
		}
		Opacity = BackgroundIntensity;
	}

	public override Color OnTileColor(Color inColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		Color drawColor = DrawColor;
		return new Color(Vector4.Lerp(((Color)(ref drawColor)).ToVector4(), ((Color)(ref inColor)).ToVector4(), 1f - BackgroundIntensity));
	}

	public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
	{
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0127: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0313: Unknown result type (might be due to invalid IL or missing references)
		//IL_031f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0326: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		if (!CanSkyBeActive)
		{
			Player localPlayer = Main.LocalPlayer;
			if (localPlayer != null && localPlayer.Calamity()?.monolithExoShader <= 0)
			{
				return;
			}
		}
		if (maxDepth >= float.MaxValue)
		{
			Vector2 scale = default(Vector2);
			((Vector2)(ref scale))._002Ector((float)Main.screenWidth * 1.1f / (float)TextureAssets.MagicPixel.Value.Width, (float)Main.screenHeight * 1.1f / (float)TextureAssets.MagicPixel.Value.Height);
			Vector2 screenArea = new Vector2((float)Main.screenWidth, (float)Main.screenHeight) * 0.5f;
			Color drawColor = Color.White * MathHelper.Lerp(0f, 0.24f, LightningIntensity) * BackgroundIntensity;
			Vector2 origin = TextureAssets.MagicPixel.Value.Size() * 0.5f;
			spriteBatch.Draw(TextureAssets.MagicPixel.Value, screenArea, (Rectangle?)null, OnTileColor(Color.Transparent), 0f, origin, scale, (SpriteEffects)0, 0f);
			for (int i = 0; i < 2; i++)
			{
				spriteBatch.Draw(TextureAssets.MagicPixel.Value, screenArea, (Rectangle?)null, drawColor, 0f, origin, scale, (SpriteEffects)0, 0f);
			}
		}
		Texture2D flashTexture = ModContent.Request<Texture2D>("Terraria/Images/Misc/VortexSky/Flash", (AssetRequestMode)2).Value;
		Texture2D boltTexture = ModContent.Request<Texture2D>("Terraria/Images/Misc/VortexSky/Bolt", (AssetRequestMode)2).Value;
		float spaceFade = Math.Min(1f, (Main.screenPosition.Y - 300f) / 300f);
		Vector2 screenCenter = Main.screenPosition + new Vector2((float)Main.screenWidth * 0.5f, (float)Main.screenHeight * 0.5f);
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(-1000, -1000, 4000, 4000);
		LightningBolts.RemoveAll((Lightning l) => l.Lifetime <= 0);
		Vector2 boltScale = default(Vector2);
		for (int i2 = 0; i2 < LightningBolts.Count; i2++)
		{
			if (!(LightningBolts[i2].Depth > minDepth) || !(LightningBolts[i2].Depth < maxDepth))
			{
				continue;
			}
			((Vector2)(ref boltScale))._002Ector(1f / LightningBolts[i2].Depth, 0.9f / LightningBolts[i2].Depth);
			Vector2 position = (LightningBolts[i2].Position - screenCenter) * boltScale + screenCenter - Main.screenPosition;
			if (((Rectangle)(ref rectangle)).Contains((int)position.X, (int)position.Y))
			{
				Texture2D texture = boltTexture;
				int life = LightningBolts[i2].Lifetime;
				if (life > 24 && life % 2 == 0)
				{
					texture = flashTexture;
				}
				float opacity = (float)life * spaceFade / 20f;
				spriteBatch.Draw(texture, position, (Rectangle?)null, Color.White * opacity, 0f, Vector2.Zero, boltScale.X * 5f, (SpriteEffects)0, 0f);
			}
		}
	}

	public override float GetCloudAlpha()
	{
		return 0f;
	}

	public override void Reset()
	{
	}

	public override void Activate(Vector2 position, params object[] args)
	{
	}

	public override void Deactivate(params object[] args)
	{
	}

	public override bool IsActive()
	{
		if (!Main.gameMenu)
		{
			if (!CanSkyBeActive)
			{
				Player localPlayer = Main.LocalPlayer;
				if (localPlayer == null)
				{
					return false;
				}
				return localPlayer.Calamity()?.monolithExoShader > 0;
			}
			return true;
		}
		return false;
	}

	static ExoMechsSky()
	{
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		DrawColor = new Color(0.16f, 0.16f, 0.16f);
	}
}
