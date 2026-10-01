using CalamityMod.Events;
using CalamityMod.World;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Signus;

public class SignusSky : CustomSky
{
	private bool isActive;

	private float intensity;

	private int SignusIndex = -1;

	public override void Update(GameTime gameTime)
	{
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
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if (UpdateSIndex())
		{
			float x = 0f;
			if (SignusIndex != -1)
			{
				x = Vector2.Distance(Main.LocalPlayer.Center, Main.npc[SignusIndex].Center);
			}
			float maxIntensity = 0.1f;
			if (CalamityWorld.revenge || BossRushEvent.BossRushActive)
			{
				maxIntensity = 1f - (float)Main.npc[SignusIndex].life / (float)Main.npc[SignusIndex].lifeMax;
			}
			return (1f - Utils.SmoothStep(3000f, 6000f, x)) * maxIntensity;
		}
		return 0f;
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

	private bool UpdateSIndex()
	{
		int SignusType = ModContent.NPCType<Signus>();
		if (SignusIndex >= 0 && Main.npc[SignusIndex].active && Main.npc[SignusIndex].type == SignusType)
		{
			return true;
		}
		SignusIndex = NPC.FindFirstNPC(SignusType);
		return SignusIndex != -1;
	}

	public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		if (maxDepth >= 0f && minDepth < 0f)
		{
			float intensity = GetIntensity();
			spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Black * intensity);
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
