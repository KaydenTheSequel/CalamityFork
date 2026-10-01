using CalamityMod.Items.Accessories;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatBuffs;

public class Encased : ModBuff
{
	public override LocalizedText Description => base.Description.WithFormatArgs(PermafrostsConcoction.EncasedDefenseBoost, PermafrostsConcoction.EncasedDamageReductionBoost.ToPercent());

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		BuffID.Sets.NurseCannotRemoveDebuff[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		player.Calamity().encased = true;
		if (player.buffTime[buffIndex] == 2)
		{
			SoundEngine.PlaySound(in SoundID.Item27, player.Center);
			int encasedIFrames = PermafrostsConcoction.EncasedIFrames + (player.longInvince ? 40 : 0);
			player.GiveUniversalIFrames(encasedIFrames, blink: true);
		}
	}
}
