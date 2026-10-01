using CalamityMod.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Leviathan;

public class LevSky : CustomSky
{
	private bool isActive;

	private float intensity;

	private int LevIndex = -1;

	public override void Update(GameTime gameTime)
	{
		if ((LevIndex == -1 && Main.LocalPlayer.Calamity().monolithLeviathanShader <= 0) || BossRushEvent.BossRushActive)
		{
			UpdateLIndex();
			if ((LevIndex == -1 && Main.LocalPlayer.Calamity().monolithLeviathanShader <= 0) || BossRushEvent.BossRushActive)
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
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if (UpdateLIndex())
		{
			float x = 0f;
			if (LevIndex != -1)
			{
				x = Vector2.Distance(Main.LocalPlayer.Center, Main.npc[LevIndex].Center);
			}
			float spawnAnimationTimer = 180f;
			float intensityScalar = 1f;
			if (Main.npc[LevIndex].Calamity().newAI[3] < spawnAnimationTimer)
			{
				intensityScalar = MathHelper.Lerp(0f, intensityScalar, Main.npc[LevIndex].Calamity().newAI[3] / spawnAnimationTimer);
			}
			return (1f - Utils.SmoothStep(3000f, 6000f, x)) * intensityScalar * intensity;
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

	private bool UpdateLIndex()
	{
		int LevType = (Main.zenithWorld ? ModContent.NPCType<Anahita>() : ModContent.NPCType<Leviathan>());
		if (LevIndex >= 0 && Main.npc[LevIndex].active && Main.npc[LevIndex].type == LevType)
		{
			return true;
		}
		LevIndex = -1;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.type == LevType)
			{
				LevIndex = n.whoAmI;
				break;
			}
		}
		return LevIndex != -1;
	}

	public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (maxDepth >= 0f && minDepth < 0f)
		{
			float intensity = GetIntensity();
			spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth * 2, Main.screenHeight * 2), new Color(0, 0, 15) * intensity);
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
