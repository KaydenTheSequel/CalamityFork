using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.Summon.Whips;

public class ProfanedCrystalWhipBuff : ModBuff
{
	public override string Texture => "CalamityMod/Buffs/Summon/Whips/SentinalLash";

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = false;
		Main.buffNoSave[base.Type] = true;
	}

	public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
	{
		if (Main.LocalPlayer.Calamity().pscState == 3)
		{
			tip = tip + "\n" + this.GetLocalizedValue("Empowered");
		}
	}

	public override void Update(Player player, ref int buffIndex)
	{
		if (player.Calamity().profanedCrystalStatePrevious < 1)
		{
			player.ClearBuff(base.Type);
			return;
		}
		int[] array = new int[4] { 312, 311, 308, 314 };
		foreach (int buff in array)
		{
			player.ClearBuff(buff);
		}
	}
}
