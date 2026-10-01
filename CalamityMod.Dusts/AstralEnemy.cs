using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Dusts;

public class AstralEnemy : ModDust
{
	public override void OnSpawn(Dust dust)
	{
		dust.scale = Main.rand.NextFloat(0.9f, 1f);
	}

	public override bool Update(Dust dust)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		dust.position += dust.velocity;
		dust.velocity.Y += 0.1f;
		dust.scale -= 0.02f;
		if (dust.scale < 0.1f)
		{
			dust.active = false;
		}
		return false;
	}
}
