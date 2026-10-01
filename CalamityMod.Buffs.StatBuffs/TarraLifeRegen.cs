using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatBuffs;

public class TarraLifeRegen : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = false;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().tRegen = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		if (Main.rand.NextBool(10))
		{
			Dust dust = Dust.NewDustDirect(drawInfo.Position - new Vector2(2f), Player.width + 4, Player.height + 4, 107, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100);
			dust.noGravity = true;
			dust.velocity *= 0.75f;
			dust.velocity.Y -= 0.35f;
			drawInfo.DustCache.Add(dust.dustIndex);
		}
	}
}
