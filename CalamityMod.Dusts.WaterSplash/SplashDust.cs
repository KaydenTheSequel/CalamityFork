using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Dusts.WaterSplash;

public abstract class SplashDust : ModDust
{
	public override void SetStaticDefaults()
	{
		base.UpdateType = 33;
	}

	public override void OnSpawn(Dust dust)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		dust.alpha = 170;
		dust.velocity *= 0.5f;
		dust.velocity.Y++;
	}
}
