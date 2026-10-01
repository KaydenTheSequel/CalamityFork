using CalamityMod.Events;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.CalClone;

public class CalScreenShaderData : ScreenShaderData
{
	private int CalIndex;

	public CalScreenShaderData(string passName)
		: base(passName)
	{
	}

	private void UpdateCalIndex()
	{
		int CalType = ModContent.NPCType<CalamitasClone>();
		if (CalIndex >= 0 && Main.npc[CalIndex].active && Main.npc[CalIndex].type == CalType)
		{
			return;
		}
		CalIndex = -1;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.type == CalType)
			{
				CalIndex = n.whoAmI;
				break;
			}
		}
	}

	public override void Update(GameTime gameTime)
	{
		if (CalIndex == -1 || BossRushEvent.BossRushActive)
		{
			UpdateCalIndex();
			if (CalIndex == -1 || BossRushEvent.BossRushActive)
			{
				Filters.Scene["CalamityMod:CalamitasRun3"].Deactivate();
			}
		}
	}

	public override void Apply()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		UpdateCalIndex();
		if (CalIndex != -1)
		{
			UseTargetPosition(Main.npc[CalIndex].Center);
		}
		base.Apply();
	}
}
