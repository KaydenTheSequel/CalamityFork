using CalamityMod.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.PlaguebringerGoliath;

public class PbGSky : CustomSky
{
	private bool isActive;

	private float intensity;

	private int PbGIndex = -1;

	public override void Update(GameTime gameTime)
	{
		if ((PbGIndex == -1 && Main.LocalPlayer.Calamity().monolithPlagueShader <= 0) || BossRushEvent.BossRushActive)
		{
			UpdatePbGIndex();
			if ((PbGIndex == -1 && Main.LocalPlayer.Calamity().monolithPlagueShader <= 0) || BossRushEvent.BossRushActive)
			{
				isActive = false;
			}
		}
	}

	private float GetIntensity()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		if (UpdatePbGIndex())
		{
			float x = 0f;
			if (PbGIndex != -1)
			{
				x = Vector2.Distance(Main.LocalPlayer.Center, Main.npc[PbGIndex].Center);
			}
			return 1f - Utils.SmoothStep(3000f, 6000f, x);
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

	private bool UpdatePbGIndex()
	{
		int PbGType = ModContent.NPCType<PlaguebringerGoliath>();
		if (PbGIndex >= 0 && Main.npc[PbGIndex].active && Main.npc[PbGIndex].type == PbGType)
		{
			return true;
		}
		PbGIndex = NPC.FindFirstNPC(PbGType);
		return PbGIndex != -1;
	}

	public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		if (maxDepth >= 0f && minDepth < 0f)
		{
			float intensity = GetIntensity();
			spriteBatch.Draw(TextureAssets.BlackTile.Value, new Rectangle(0, 0, Main.screenWidth * 2, Main.screenHeight * 2), Color.Black * intensity);
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
