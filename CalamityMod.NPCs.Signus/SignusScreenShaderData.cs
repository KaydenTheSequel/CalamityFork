using Terraria;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.Signus;

public class SignusScreenShaderData : ScreenShaderData
{
	private int SignusIndex;

	public SignusScreenShaderData(string passName)
		: base(passName)
	{
	}

	private void UpdateSIndex()
	{
		int SignusType = ModContent.NPCType<Signus>();
		if (SignusIndex < 0 || !Main.npc[SignusIndex].active || Main.npc[SignusIndex].type != SignusType)
		{
			SignusIndex = NPC.FindFirstNPC(SignusType);
		}
	}

	public override void Apply()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		UpdateSIndex();
		if (SignusIndex != -1)
		{
			UseTargetPosition(Main.npc[SignusIndex].Center);
		}
		base.Apply();
	}
}
