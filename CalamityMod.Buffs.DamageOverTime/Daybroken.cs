using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class Daybroken : ModBuff
{
	public override LocalizedText DisplayName => Language.GetOrRegister("BuffName.Daybreak");

	public override LocalizedText Description => Language.GetOrRegister("BuffDescription.Daybreak");

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		BuffID.Sets.LongerExpertDebuff[base.Type] = true;
		BuffDatasets.DebuffDataset[base.Type] = DebuffData.Daybroken;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().daybroken = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		if (Main.rand.Next(4) < 3)
		{
			Dust solarDust = Dust.NewDustDirect(Player.position, Player.width, Player.height, 158, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
			solarDust.noGravity = true;
			solarDust.velocity *= 2.8f;
			solarDust.velocity.Y -= 0.5f;
			if (Main.rand.NextBool(4))
			{
				solarDust.noGravity = false;
				solarDust.scale *= 0.5f;
			}
		}
		Lighting.AddLight((int)(Player.position.X / 16f), (int)(Player.position.Y / 16f + 1f), 1f, 0.3f, 0.1f);
	}
}
