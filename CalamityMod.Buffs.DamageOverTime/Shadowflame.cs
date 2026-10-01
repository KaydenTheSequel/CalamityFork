using CalamityMod.DataStructures;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.DamageOverTime;

public class Shadowflame : ModBuff
{
	public override LocalizedText DisplayName => Language.GetOrRegister("BuffName.ShadowFlame");

	public override LocalizedText Description => Language.GetOrRegister("BuffDescription.ShadowFlame");

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
		BuffID.Sets.LongerExpertDebuff[base.Type] = true;
		BuffDatasets.DebuffDataset[base.Type] = DebuffData.Shadowflame;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().shadowflame = true;
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
		if (Main.rand.Next(5) < 4)
		{
			Dust flame = Dust.NewDustDirect(drawInfo.Position - new Vector2(2f), Player.width + 4, Player.height + 4, 27, Player.velocity.X * 0.4f, Player.velocity.Y * 0.4f, 100, default(Color), 1.1f);
			flame.noGravity = true;
			flame.velocity *= 0.75f;
			flame.velocity.X *= 0.75f;
			flame.velocity.Y -= 3f;
			if (Main.rand.NextBool(4))
			{
				flame.noGravity = false;
				flame.scale *= 0.3f;
			}
		}
	}
}
