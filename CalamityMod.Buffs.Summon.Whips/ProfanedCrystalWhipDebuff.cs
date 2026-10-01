using System.Linq;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Summon.Whips;

public class ProfanedCrystalWhipDebuff : ModBuff
{
	public override string Texture => "CalamityMod/Buffs/Summon/Whips/SentinalLash";

	public override void SetStaticDefaults()
	{
		BuffID.Sets.IsATagBuff[base.Type] = true;
		Main.debuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		int[] whipBuffs = new int[9] { 307, 313, 326, 310, 340, 319, 316, 309, 315 };
		for (int buff = 0; buff < NPC.maxBuffs; buff++)
		{
			int buffID = npc.buffType[buff];
			if (npc.buffTime[buff] > 0 && whipBuffs.Contains(buffID) && npc.buffType[buff] != base.Type)
			{
				npc.RequestBuffRemoval(npc.buffType[buff]);
			}
		}
	}
}
