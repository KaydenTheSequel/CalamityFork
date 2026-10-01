using CalamityMod.Items.Placeables.Furniture;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Placeables;

public class YellowCandleBuff : ModBuff
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
		player.Calamity().yellowCandle = true;
	}

	internal static void ModifyHitInfo_Spite(ref NPC.HitInfo info)
	{
		int damageBoost = (int)((float)info.SourceDamage * SpitefulCandle.ExtraChipDamageRatio);
		info.Damage += damageBoost;
	}
}
