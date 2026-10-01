using CalamityMod.NPCs.Cryogen;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent.UI.BigProgressBar;
using Terraria.ModLoader;

namespace CalamityMod.UI.VanillaBossBars;

public class CryogenBossBar : ModBossBar
{
	public override Asset<Texture2D> GetIconTexture(ref Rectangle? iconFrame)
	{
		if (Main.zenithWorld)
		{
			return ModContent.Request<Texture2D>("CalamityMod/NPCs/Cryogen/Pyrogen_Head_Boss", (AssetRequestMode)2);
		}
		return ModContent.Request<Texture2D>("CalamityMod/NPCs/Cryogen/Cryogen_Phase1_Head_Boss", (AssetRequestMode)2);
	}

	public override bool? ModifyInfo(ref BigProgressBarInfo info, ref float life, ref float lifeMax, ref float shield, ref float shieldMax)
	{
		NPC target = Main.npc[info.npcIndexToAimAt];
		if (!target.active)
		{
			return false;
		}
		life = target.life;
		lifeMax = target.lifeMax;
		shield = 0f;
		shieldMax = 0f;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC part = enumerator.Current;
			if (part.type == ModContent.NPCType<CryogenShield>())
			{
				shield += part.life;
				shieldMax += part.lifeMax;
			}
		}
		return true;
	}
}
