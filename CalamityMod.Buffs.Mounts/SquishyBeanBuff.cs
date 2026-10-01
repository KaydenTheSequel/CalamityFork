using CalamityMod.Items.Mounts;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Mounts;

public class SquishyBeanBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.mount.SetMount(ModContent.MountType<SquishyBean>(), player);
		player.buffTime[buffIndex] = 10;
	}
}
