using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Summon;

public abstract class BaseSummonBuff : ModBuff
{
	protected abstract int MinionProjectileType { get; }

	protected abstract ref bool MinionBool { get; }

	protected Player BuffOwner { get; private set; }

	protected CalamityPlayer BuffModdedOwner { get; private set; }

	public override void SetStaticDefaults()
	{
		Main.buffNoTimeDisplay[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		BuffOwner = player;
		BuffModdedOwner = player.Calamity();
		if (player.ownedProjectileCounts[MinionProjectileType] > 0)
		{
			MinionBool = true;
		}
		if (!MinionBool)
		{
			player.DelBuff(buffIndex);
			buffIndex--;
		}
		else
		{
			player.buffTime[buffIndex] = 18000;
		}
	}
}
