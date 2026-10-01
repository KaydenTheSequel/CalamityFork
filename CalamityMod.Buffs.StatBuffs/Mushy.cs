using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Buffs.StatBuffs;

public class Mushy : ModBuff
{
	public static int DefenseBoost = 5;

	public static int RegenBoost = 2;

	public override LocalizedText Description => base.Description.WithFormatArgs(DefenseBoost, RegenBoost.ToRegenPerSecond());

	public override void SetStaticDefaults()
	{
		Main.debuff[base.Type] = true;
		Main.pvpBuff[base.Type] = true;
		Main.buffNoSave[base.Type] = true;
	}

	public override void Update(Player player, ref int buffIndex)
	{
		player.Calamity().mushy = true;
	}

	internal static void DrawEffects(PlayerDrawSet drawInfo)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		Player Player = drawInfo.drawPlayer;
		if (Main.rand.NextBool(4))
		{
			Dust dust = Dust.NewDustPerfect(Player.Calamity().RandomDebuffVisualSpot, 56, Vector2.Zero, 100, default(Color), 0.9f);
			dust.noGravity = true;
			dust.velocity *= 0.5f;
			dust.velocity.Y -= 0.1f;
			dust.alpha = 200;
		}
		if (Main.rand.NextBool(15))
		{
			Dust.NewDustPerfect(Player.Center + new Vector2(Main.rand.NextFloat(-10f, 10f), 19f), Main.rand.NextBool(3) ? 41 : 56, new Vector2(Main.rand.NextFloat(-1f, 1f), Main.rand.NextFloat(-1f, -2f)) + Player.velocity / 3f, 0, default(Color), 0.9f).alpha = 145;
		}
	}
}
