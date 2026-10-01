using CalamityMod.Systems;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;

namespace CalamityMod.Skies;

public class AstralSky : CustomSky
{
	private bool skyActive;

	private float opacity;

	public override void Deactivate(params object[] args)
	{
		skyActive = (Main.LocalPlayer.Calamity().ZoneAstral && !Main.LocalPlayer.ZoneDungeon && BiomeTileCounterSystem.AstralTiles > 950) || Main.LocalPlayer.Calamity().monolithAstralShader > 0;
	}

	public override void Reset()
	{
		skyActive = false;
	}

	public override bool IsActive()
	{
		if (!skyActive)
		{
			return opacity > 0f;
		}
		return true;
	}

	public override void Activate(Vector2 position, params object[] args)
	{
		skyActive = true;
	}

	public override Color OnTileColor(Color inColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(inColor, new Color(63, 51, 90, (int)((Color)(ref inColor)).A), opacity);
	}

	public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
	{
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0222: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0258: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_030b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0317: Unknown result type (might be due to invalid IL or missing references)
		//IL_0382: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_043b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0445: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0486: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		_ = (AstralBiome.YStart + (int)Main.worldSurface) / 2;
		if (Main.maxTilesX >= 6400 && Main.maxTilesX < 8400)
		{
			_ = (AstralBiome.YStart + (int)Main.worldSurface) / 4;
		}
		if (Main.maxTilesX >= 8400)
		{
			_ = (AstralBiome.YStart + (int)Main.worldSurface) / 140;
		}
		float whateverTheFuckThisVariableIsSupposedToBe = float.MaxValue;
		if (!(maxDepth >= whateverTheFuckThisVariableIsSupposedToBe) || !(minDepth < whateverTheFuckThisVariableIsSupposedToBe))
		{
			return;
		}
		spriteBatch.Draw(SkyTextureRefs.AstralSky.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.White * opacity);
		if (Main.dedServ)
		{
			return;
		}
		int bgTop = (int)((double)(0f - Main.screenPosition.Y) / (Main.worldSurface * 16.0 - 600.0) * 200.0);
		float colorMult = 0.952f * opacity;
		Color astralcyan = default(Color);
		((Color)(ref astralcyan))._002Ector(100, 183, 255);
		Color purple = default(Color);
		((Color)(ref purple))._002Ector(201, 148, 255);
		Color yellow = default(Color);
		((Color)(ref yellow))._002Ector(255, 146, 73);
		float width1 = (float)Main.screenWidth / 500f;
		float height1 = (float)Main.screenHeight / 600f;
		float width2 = (float)Main.screenWidth / 600f;
		float height2 = (float)Main.screenHeight / 800f;
		float width3 = (float)Main.screenWidth / 200f;
		float height3 = (float)Main.screenHeight / 900f;
		float width4 = (float)Main.screenWidth / 1000f;
		float height4 = (float)Main.screenHeight / 200f;
		Vector2 origin = default(Vector2);
		Vector2 position = default(Vector2);
		for (int i = 0; i < Main.star.Length; i++)
		{
			Star star = Main.star[i];
			if (star != null)
			{
				Texture2D t2D = TextureAssets.Star[star.type].Value;
				((Vector2)(ref origin))._002Ector((float)t2D.Width * 0.5f, (float)t2D.Height * 0.5f);
				float posX = star.position.X * width1;
				float posY = star.position.Y * height1;
				((Vector2)(ref position))._002Ector(posX + origin.X, posY + origin.Y + (float)bgTop);
				spriteBatch.Draw(t2D, position, (Rectangle?)new Rectangle(0, 0, t2D.Width, t2D.Height), astralcyan * star.twinkle * colorMult, star.rotation, origin, star.scale * star.twinkle - 0.2f, (SpriteEffects)0, 0f);
				((Vector2)(ref origin))._002Ector((float)t2D.Width * 0.2f, (float)t2D.Height * 0.2f);
				posX = star.position.X * width2;
				posY = star.position.Y * height2;
				((Vector2)(ref position))._002Ector(posX + origin.X, posY + origin.Y + (float)bgTop);
				spriteBatch.Draw(t2D, position, (Rectangle?)new Rectangle(0, 0, t2D.Width, t2D.Height), purple * star.twinkle * colorMult, star.rotation, origin, star.scale * star.twinkle + 0.2f, (SpriteEffects)0, 0f);
				((Vector2)(ref origin))._002Ector((float)t2D.Width * 0.8f, (float)t2D.Height * 0.8f);
				posX = star.position.X * width3;
				posY = star.position.Y * height3;
				((Vector2)(ref position))._002Ector(posX + origin.X, posY + origin.Y + (float)bgTop);
				spriteBatch.Draw(t2D, position, (Rectangle?)new Rectangle(0, 0, t2D.Width, t2D.Height), yellow * star.twinkle * colorMult, star.rotation, origin, star.scale * star.twinkle, (SpriteEffects)0, 0f);
				((Vector2)(ref origin))._002Ector((float)t2D.Width * 0.5f, (float)t2D.Height * 0.5f);
				posX = star.position.X * width4;
				posY = star.position.Y * height4;
				((Vector2)(ref position))._002Ector(posX + origin.X, posY + origin.Y + (float)bgTop);
				spriteBatch.Draw(t2D, position, (Rectangle?)new Rectangle(0, 0, t2D.Width, t2D.Height), Color.White * star.twinkle * colorMult, star.rotation, origin, star.scale * star.twinkle, (SpriteEffects)0, 0f);
			}
		}
	}

	public override void Update(GameTime gameTime)
	{
		if ((!Main.LocalPlayer.Calamity().ZoneAstral && Main.LocalPlayer.Calamity().monolithAstralShader <= 0) || Main.gameMenu)
		{
			skyActive = false;
		}
		if (skyActive && opacity < 1f)
		{
			opacity += 0.02f;
		}
		else if (!skyActive && opacity > 0f)
		{
			opacity -= 0.02f;
		}
		Opacity = opacity;
	}

	public override float GetCloudAlpha()
	{
		return (1f - opacity) * 0.97f + 0.03f;
	}
}
