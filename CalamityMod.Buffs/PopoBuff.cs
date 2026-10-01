using CalamityMod.Items.Accessories.Vanity;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs;

public class PopoBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		Main.buffNoTimeDisplay[base.Type] = true;
		BuffID.Sets.NurseCannotRemoveDebuff[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity();
		if (player.Transformation().Type != ModContent.ItemType<Popo>())
		{
			player.DelBuff(buffIndex);
			buffIndex--;
		}
	}
}
