using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Summon.Whips;

public class CnidarianSummonTagBuff : ModBuff
{
	public override string Texture => "CalamityMod/Buffs/Summon/Whips/SentinalLash";

	public override void SetStaticDefaults()
	{
		BuffID.Sets.IsATagBuff[base.Type] = true;
		Main.debuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}
}
