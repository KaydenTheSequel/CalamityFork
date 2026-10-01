using CalamityMod.Items.Armor.MarniteArchitect;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Mounts;

public class MarniteLiftBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.mount.SetMount(ModContent.MountType<MarniteLift>(), player);
		player.buffTime[buffIndex] = 10;
		player.GetModPlayer<MarniteArchitectPlayer>().mounted = true;
	}
}
