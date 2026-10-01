using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatDebuffs;

public class IcarusFolly : ModBuff
{
	public static int MaxFlightTimeCap = 400;

	public static float FlightTimeLossPercent = 0.34f;

	public override LocalizedText Description => base.Description.WithFormatArgs(FlightTimeLossPercent.ToPercent());

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		BuffID.Sets.NurseCannotRemoveDebuff[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().icarusFolly = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		if (Main.rand.NextBool(12))
		{
			Dust dust = Dust.NewDustDirect(drawInfo.Position - new Vector2(2f), Player.width + 4, Player.height + 4, 244, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 3f);
			dust.noGravity = true;
			dust.velocity *= 1.8f;
			dust.velocity.Y -= 0.5f;
			drawInfo.DustCache.Add(dust.dustIndex);
		}
	}
}
