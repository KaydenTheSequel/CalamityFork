using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Dusts.Furniture;

public class OtherworldlyTileCloth : ModDust
{
	public override void OnSpawn(Dust dust)
	{
		dust.noLight = true;
		dust.noGravity = false;
		dust.scale = 1.2f;
		dust.alpha = 100;
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
		dust.scale -= 0.03f;
		if (dust.scale < 0.5f)
		{
			dust.active = false;
		}
		return false;
	}
}
