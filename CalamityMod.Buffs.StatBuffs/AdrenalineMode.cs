using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatBuffs;

public class AdrenalineMode : ModBuff
{
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
		player.Calamity().adrenalineModeActive = true;
	}

	public override void ModifyBuffText(ref string buffName, ref string tip, ref int rare)
	{
		tip = base.Description.Format((1f + Main.LocalPlayer.Calamity().GetAdrenalineDamage()).Round());
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		Vector3 adrenDustLight = default(Vector3);
		((Vector3)(ref adrenDustLight))._002Ector(0.094f, 0.255f, 0.185f);
		Lighting.AddLight(Player.Center, adrenDustLight * 3f);
		for (int i = 0; i < 4; i++)
		{
			int dustID = ModContent.DustType<AdrenDust>();
			Vector2 dustVel = Player.velocity * 0.5f;
			Dust dust = Dust.NewDustDirect(Main.rand.NextBool(5) ? drawInfo.Position : (drawInfo.Position - Player.velocity * 1.5f), Player.width, Player.height, dustID, dustVel.X, dustVel.Y);
			dust.scale = Main.rand.NextFloat(0.4f, 1.2f);
			dust.noGravity = true;
			dust.noLight = false;
			drawInfo.DustCache.Add(dust.dustIndex);
		}
	}
}
