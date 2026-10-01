using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.UI;

[Autoload(true, Side = ModSide.Client)]
public abstract class PopupGUI : ModType
{
	public int FadeTime;

	public bool Active;

	public virtual int FadeTimeMax { get; set; } = 30;

	protected sealed override void Register()
	{
		ModTypeLookup<PopupGUI>.Register(this);
		PopupGUIManager.gUIs.Add(this);
	}

	public virtual void Update()
	{
		if (Active)
		{
			if (FadeTime < FadeTimeMax)
			{
				FadeTime++;
			}
		}
		else if (FadeTime > 0)
		{
			FadeTime--;
		}
		if (Main.mouseLeft && Main.mouseLeftRelease && !Main.blockMouse && FadeTime >= 30)
		{
			Main.mouseLeftRelease = false;
			Main.mouseLeft = false;
			Active = false;
		}
	}

	public float GetYTop()
	{
		return MathHelper.Lerp((float)(Main.screenHeight * 2), (float)Main.screenHeight * 0.25f, (float)FadeTime / (float)FadeTimeMax);
	}

	public Vector2 GetScreenAdjustedScale(float textureScale, Texture2D drawTexture)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		return new Vector2(MathHelper.Lerp(0.004f, 1f, (float)FadeTime / (float)FadeTimeMax), 1f) * new Vector2((float)Main.screenWidth, (float)Main.screenHeight) / drawTexture.Size() * 0.5f * textureScale;
	}

	public virtual void Draw(SpriteBatch spriteBatch)
	{
	}
}
