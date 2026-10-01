using CalamityMod.NPCs.Crabulon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Skies;

public class DrunkCrabScreenShaderData : ScreenShaderData
{
	public int CrabIndex;

	public DrunkCrabScreenShaderData(string passName)
		: base(passName)
	{
	}

	public void UpdateBossIndex()
	{
		int CrabType = ModContent.NPCType<Crabulon>();
		if (CrabIndex < 0 || !Main.npc[CrabIndex].active || Main.npc[CrabIndex].type != CrabType)
		{
			CrabIndex = NPC.FindFirstNPC(CrabType);
		}
	}

	public override void Update(GameTime gameTime)
	{
		if (CrabIndex == -1 || !Main.zenithWorld)
		{
			UpdateBossIndex();
			if (CrabIndex == -1 || !Main.zenithWorld)
			{
				Filters.Scene["CalamityMod:DrunkCrabulon"].Deactivate();
			}
		}
	}

	public override void Apply()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		UpdateBossIndex();
		if (CrabIndex != -1)
		{
			UseTargetPosition(Main.npc[CrabIndex].Center);
		}
		base.Apply();
	}
}
