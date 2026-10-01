using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Mounts;

public class AndromedaBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		ExternalMods.crouchMod?.Call("CanCrouch", player.whoAmI, false, true);
	}
}
