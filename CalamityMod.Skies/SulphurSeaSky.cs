using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;

namespace CalamityMod.Skies;

public class SulphurSeaSky : CustomSky
{
	private bool skyActive;

	private float opacity;

	private const float ScreenParralaxMultiplier = 0.4f;

	private const float Scale = 2.5f;

	public override void Deactivate(params object[] args)
	{
		skyActive = Main.LocalPlayer.Calamity().ZoneSulphur;
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

	public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
	{
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0160: Unknown result type (might be due to invalid IL or missing references)
		//IL_0162: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02df: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ed: Unknown result type (might be due to invalid IL or missing references)
		if (maxDepth >= 4f && minDepth < 4f)
		{
			spriteBatch.Draw(SkyTextureRefs.SulphurSeaSky.Value, new Rectangle(0, (int)((0f - Main.screenPosition.Y) / 6f) + 1300, Main.screenWidth, Main.screenHeight), Color.Lerp(Main.ColorOfTheSkies, Color.LightSeaGreen, 0.33f) * 0.2f * opacity);
		}
		int sulphurSeaHeight = (SulphurousSea.YStart + (int)Main.worldSurface) / 2;
		if (Main.maxTilesX >= 6400 && Main.maxTilesX < 8400)
		{
			sulphurSeaHeight = (SulphurousSea.YStart + (int)Main.worldSurface) / 5;
		}
		if (Main.maxTilesX >= 8400)
		{
			sulphurSeaHeight = (SulphurousSea.YStart + (int)Main.worldSurface) / 140;
		}
		if (maxDepth >= 1f && minDepth < 1f)
		{
			Texture2D texture = SkyTextureRefs.SulphurSeaSkyFront.Value;
			int x = (int)(Main.screenPosition.X * 0.4f);
			x %= (int)((float)texture.Width * 2.5f);
			int y = (int)(Main.screenPosition.Y * 0.5f * 0.4f);
			y -= 1800;
			float screenWidth = (float)Main.screenWidth / 2f;
			float screenHeight = (float)Main.screenHeight / 2f;
			Vector2 position = texture.Size() / 2f * 2.5f;
			Color color = Color.LightSeaGreen * 0.5f * opacity;
			Vector2 pos = default(Vector2);
			for (int k = -1; k <= 1; k++)
			{
				((Vector2)(ref pos))._002Ector(screenWidth - (float)x + (float)(texture.Width * k) * 2.5f, screenHeight - (float)y);
				spriteBatch.Draw(texture, pos - position, (Rectangle?)null, color, 0f, new Vector2(0f, (float)sulphurSeaHeight), 2.5f, (SpriteEffects)0, 0f);
			}
		}
		if (maxDepth >= 3f && minDepth < 3f)
		{
			Texture2D texture2 = SkyTextureRefs.SulphurSeaSurface.Value;
			int x2 = (int)(Main.screenPosition.X * 0.4f);
			x2 %= (int)((float)texture2.Width * 2.5f);
			int y2 = (int)(Main.screenPosition.Y * 0.5f * 0.4f);
			y2 -= 1800;
			float screenWidth2 = (float)Main.screenWidth / 2f;
			float screenHeight2 = (float)Main.screenHeight / 2f;
			Vector2 position2 = texture2.Size() / 2f * 2.5f;
			Color color2 = Main.ColorOfTheSkies * opacity;
			Vector2 pos2 = default(Vector2);
			for (int i = -1; i <= 1; i++)
			{
				((Vector2)(ref pos2))._002Ector(screenWidth2 - (float)x2 + (float)(texture2.Width * i) * 2.5f, screenHeight2 - (float)y2);
				spriteBatch.Draw(texture2, pos2 - position2, (Rectangle?)null, color2, 0f, new Vector2(0f, (float)sulphurSeaHeight), 2.5f, (SpriteEffects)0, 0f);
			}
		}
	}

	public override void Update(GameTime gameTime)
	{
		if (!Main.LocalPlayer.Calamity().ZoneSulphur || Main.gameMenu)
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
	}
}
