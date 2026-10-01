using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Dusts;

public class AstralChunkDust : ModDust
{
	public override void OnSpawn(Dust dust)
	{
		dust.scale = Main.rand.NextFloat(0.9f, 1.2f);
	}

	public override bool Update(Dust dust)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		dust.velocity.Y += 0.1f;
		dust.position += dust.velocity;
		dust.rotation += dust.velocity.X;
		dust.alpha += 6;
		if (dust.alpha > 240)
		{
			dust.active = false;
		}
		return false;
	}
}
