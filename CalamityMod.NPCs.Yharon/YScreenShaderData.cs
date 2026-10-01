using CalamityMod.Events;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Yharon;

public class YScreenShaderData : ScreenShaderData
{
	private int YIndex;

	public YScreenShaderData(string passName)
		: base(passName)
	{
	}

	private void UpdateYIndex()
	{
		int YType = ModContent.NPCType<Yharon>();
		if (YIndex < 0 || !Main.npc[YIndex].active || Main.npc[YIndex].type != YType)
		{
			YIndex = NPC.FindFirstNPC(YType);
		}
	}

	public override void Update(GameTime gameTime)
	{
		if ((YIndex == -1 && Main.LocalPlayer.Calamity().monolithYharonShader <= 0) || BossRushEvent.BossRushActive)
		{
			UpdateYIndex();
			if ((YIndex == -1 && Main.LocalPlayer.Calamity().monolithYharonShader <= 0) || BossRushEvent.BossRushActive)
			{
				Filters.Scene["CalamityMod:Yharon"].Deactivate();
			}
		}
	}

	public override void Apply()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		UpdateYIndex();
		if (YIndex != -1)
		{
			UseTargetPosition(Main.npc[YIndex].Center);
		}
		if (Main.LocalPlayer.Calamity().monolithYharonShader > 0)
		{
			UseTargetPosition(Main.LocalPlayer.Center);
		}
		base.Apply();
	}
}
