using CalamityMod.NPCs.DevourerofGods;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ModLoader;

namespace CalamityMod.Skies;

public class DoGScreenShaderData : ScreenShaderData
{
	private int DoGIndex;

	public DoGScreenShaderData(string passName)
		: base(passName)
	{
	}

	private void UpdateDoGIndex()
	{
		int DoGType = ModContent.NPCType<DevourerofGodsHead>();
		if (DoGIndex >= 0 && Main.npc[DoGIndex].active && Main.npc[DoGIndex].type == DoGType)
		{
			return;
		}
		DoGIndex = -1;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.type == DoGType)
			{
				DoGIndex = n.whoAmI;
				break;
			}
		}
	}

	public override void Update(GameTime gameTime)
	{
		if (DoGIndex == -1)
		{
			UpdateDoGIndex();
			if (DoGIndex == -1 && Main.LocalPlayer.Calamity().monolithDevourerPShader <= 0 && Main.LocalPlayer.Calamity().monolithDevourerBShader <= 0)
			{
				Filters.Scene["CalamityMod:DevourerofGodsHead"].Deactivate();
			}
		}
	}

	public override void Apply()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		UpdateDoGIndex();
		if (DoGIndex != -1)
		{
			UseTargetPosition(Main.npc[DoGIndex].Center);
		}
		else if (Main.LocalPlayer.Calamity().monolithDevourerPShader > 0 || Main.LocalPlayer.Calamity().monolithDevourerBShader > 0)
		{
			UseTargetPosition(Main.LocalPlayer.Center);
		}
		UseColor(DoGSky.DoGSkyColor);
		base.Apply();
	}
}
