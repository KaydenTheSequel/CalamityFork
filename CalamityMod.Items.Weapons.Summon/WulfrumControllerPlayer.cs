using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Weapons.Summon;

public class WulfrumControllerPlayer : ModPlayer
{
	public int buffingDrones;

	public override void ResetEffects()
	{
	}

	public override void UpdateDead()
	{
		buffingDrones = 0;
	}

	public override void UpdateLifeRegen()
	{
		if (buffingDrones > 0)
		{
			base.Player.lifeRegen += buffingDrones;
			base.Player.statDefense += buffingDrones * 3;
			buffingDrones = 0;
		}
	}

	public override void PostUpdateMiscEffects()
	{
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		if (buffingDrones > 0 && Main.rand.NextBool(3))
		{
			Vector2 position = base.Player.position + ((float)base.Player.height * Main.rand.NextFloat(0.7f, 1f) + base.Player.gfxOffY) * Vector2.UnitY + Vector2.UnitX * Main.rand.NextFloat() * (float)base.Player.width;
			Vector2? velocity = -Vector2.UnitY * Main.rand.NextFloat(1.4f, 7f) + base.Player.velocity;
			float scale = Main.rand.NextFloat(1.2f, 1.8f);
			Dust dust = Dust.NewDustPerfect(position, 274, velocity, 100, default(Color), scale);
			dust.noGravity = true;
			dust.noLight = true;
		}
	}
}
