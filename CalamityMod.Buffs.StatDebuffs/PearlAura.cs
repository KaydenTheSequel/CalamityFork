using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatDebuffs;

public class PearlAura : ModBuff
{
	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(NPC npc, ref int buffIndex)
	{
		npc.Calamity().pearlAura = true;
	}

	internal static void DrawEffects(NPC npc, ref Color drawColor)
	{
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(4))
		{
			int dustType = (Main.rand.NextBool() ? 88 : 68);
			Vector2 dustVelocity = Vector2.UnitX.RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 12f, 0f)) * (Main.rand.NextBool() ? (-1f) : 1f);
			Dust.NewDustDirect(npc.position, npc.width, npc.height, dustType, dustVelocity.X, dustVelocity.Y).noGravity = true;
		}
	}
}
