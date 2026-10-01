using CalamityMod.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Yharon;

public class YSky : CustomSky
{
	private bool isActive;

	private float intensity;

	private int YIndex = -1;

	public override void Update(GameTime gameTime)
	{
		if ((YIndex == -1 && Main.LocalPlayer.Calamity().monolithYharonShader <= 0) || BossRushEvent.BossRushActive)
		{
			UpdateYIndex();
			if ((YIndex == -1 && Main.LocalPlayer.Calamity().monolithYharonShader <= 0) || BossRushEvent.BossRushActive)
			{
				isActive = false;
			}
		}
		if (isActive && intensity < 1f)
		{
			intensity += 0.01f;
		}
		else if (!isActive && intensity > 0f)
		{
			intensity -= 0.01f;
		}
	}

	private float GetIntensity()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if (UpdateYIndex())
		{
			float x = 0f;
			if (YIndex != -1)
			{
				x = Vector2.Distance(Main.LocalPlayer.Center, Main.npc[YIndex].Center);
			}
			return (1f - Utils.SmoothStep(3000f, 6000f, x)) * intensity * 0.66f;
		}
		return 0.66f;
	}

	public override Color OnTileColor(Color inColor)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		float intensity = GetIntensity();
		return new Color(Vector4.Lerp(new Vector4(0.5f, 0.8f, 1f, 1f), ((Color)(ref inColor)).ToVector4(), 1f - intensity));
	}

	private bool UpdateYIndex()
	{
		int YType = ModContent.NPCType<Yharon>();
		if ((YIndex >= 0 && Main.npc[YIndex].active && Main.npc[YIndex].type == YType) || Main.LocalPlayer.Calamity().monolithYharonShader > 0)
		{
			return true;
		}
		YIndex = NPC.FindFirstNPC(YType);
		return YIndex != -1;
	}

	public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		if (maxDepth >= 0f && minDepth < 0f)
		{
			float intensity = GetIntensity();
			spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth * 2, Main.screenHeight * 2), new Color(77, 19, 0) * intensity);
		}
	}

	public override float GetCloudAlpha()
	{
		return 0f;
	}

	public override void Activate(Vector2 position, params object[] args)
	{
		isActive = true;
	}

	public override void Deactivate(params object[] args)
	{
		isActive = false;
	}

	public override void Reset()
	{
		isActive = false;
	}

	public override bool IsActive()
	{
		if (!isActive)
		{
			return intensity > 0f;
		}
		return true;
	}
}
