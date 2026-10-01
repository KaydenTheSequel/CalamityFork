using CalamityMod.Items.Placeables.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Placeables;

public class BlueCandleBuff : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.pvpBuff[base.Type] = true;
		Main.persistentBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = false;
		Main.buffNoTimeDisplay[base.Type] = true;
		BuffID.Sets.TimeLeftDoesNotDecrease[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.moveSpeed += WeightlessCandle.MoveSpeedBoost;
		player.Calamity().blueCandle = true;
	}
}
