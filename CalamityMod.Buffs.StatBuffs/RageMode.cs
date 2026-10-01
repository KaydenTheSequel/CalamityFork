using CalamityMod.Balancing;
using CalamityMod.CalPlayer;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatBuffs;

public class RageMode : ModBuff
{
	public override LocalizedText Description => base.Description.WithFormatArgs((1f + BalancingConstants.DefaultRageDamageBoost).ToString("N2"));

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = false;
		Main.buffNoTimeDisplay[base.Type] = true;
		BuffID.Sets.NurseCannotRemoveDebuff[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		CalamityPlayer mp = player.Calamity();
		if (mp.rage > 0f)
		{
			player.buffTime[buffIndex] = 2;
			mp.rageModeActive = true;
			return;
		}
		if (player.whoAmI == Main.myPlayer)
		{
			SoundEngine.PlaySound(in CalamityPlayer.RageEndSound);
		}
		player.DelBuff(buffIndex--);
		mp.rageModeActive = false;
		mp.rage = 0f;
		player.Calamity().ragePulseTimer = 0;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		CalamityPlayer modPlayer = Player.Calamity();
		modPlayer.ragePulseTimer++;
		int dustID = (modPlayer.heartOfDarkness ? 240 : 114);
		if (modPlayer.shatteredCommunity && Main.rand.NextBool())
		{
			dustID = 112;
		}
		if (modPlayer.heartOfDarkness && !modPlayer.shatteredCommunity && Main.rand.NextBool())
		{
			dustID = 90;
		}
		if (modPlayer.ragePulseTimer == 60)
		{
			GeneralParticleHandler.SpawnParticle(new PlayerCenteredPulseRing(Player, Vector2.Zero, Color.Red, new Vector2(1f, 1f), 0f, 0f, 0.23f, 40));
			modPlayer.ragePulse = true;
		}
		if (modPlayer.ragePulse)
		{
			modPlayer.ragePulseVisualTimer++;
			if (modPlayer.ragePulseVisualTimer >= 30)
			{
				GeneralParticleHandler.SpawnParticle(new PlayerCenteredPulseRing(Player, Vector2.Zero, modPlayer.shatteredCommunity ? Color.MediumPurple : Color.Red, new Vector2(1f, 1f), 0f, 0f, 0.18f, 30));
				modPlayer.ragePulseVisualTimer = 0;
				modPlayer.ragePulse = false;
				modPlayer.ragePulseTimer = 0;
			}
		}
		Dust dust = Dust.NewDustPerfect(modPlayer.RandomDebuffVisualSpot, dustID);
		dust.scale = Main.rand.NextFloat(0.3f, 0.45f);
		if (dustID == 112)
		{
			dust.scale = Main.rand.NextFloat(0.7f, 0.8f);
		}
		if (dustID == 240)
		{
			dust.scale = Main.rand.NextFloat(0.8f, 0.95f);
		}
		dust.velocity = -Player.velocity / 3f;
		dust.noGravity = true;
	}
}
