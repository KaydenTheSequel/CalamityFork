using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Mounts;

public class AndromedaSmallBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}
}
