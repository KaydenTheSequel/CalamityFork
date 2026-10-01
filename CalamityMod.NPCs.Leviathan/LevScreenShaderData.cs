using CalamityMod.Events;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Leviathan;

public class LevScreenShaderData : ScreenShaderData
{
	private int LevIndex;

	public LevScreenShaderData(string passName)
		: base(passName)
	{
	}

	private void UpdateLIndex()
	{
		int LevType = (Main.zenithWorld ? ModContent.NPCType<Anahita>() : ModContent.NPCType<Leviathan>());
		if (LevIndex >= 0 && Main.npc[LevIndex].active && Main.npc[LevIndex].type == LevType)
		{
			return;
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
	}

	public override void Update(GameTime gameTime)
	{
		if ((LevIndex == -1 && Main.LocalPlayer.Calamity().monolithLeviathanShader <= 0) || BossRushEvent.BossRushActive)
		{
			UpdateLIndex();
			if (LevIndex == -1 || BossRushEvent.BossRushActive)
			{
				Filters.Scene["CalamityMod:Leviathan"].Deactivate();
			}
		}
	}

	public override void Apply()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		UpdateLIndex();
		if (LevIndex != -1)
		{
			UseTargetPosition(Main.npc[LevIndex].Center);
		}
		if (Main.LocalPlayer.Calamity().monolithLeviathanShader > 0)
		{
			UseTargetPosition(Main.LocalPlayer.Center);
		}
		base.Apply();
	}
}
