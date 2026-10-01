using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Dusts;

public class HBSparkle : ModDust
{
	public override void OnSpawn(Dust dust)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		dust.velocity *= 0.1f;
		dust.alpha = 155;
		dust.noGravity = true;
		dust.noLight = true;
		dust.scale *= 0.25f;
	}

	public override bool Update(Dust dust)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		dust.position += dust.velocity;
		dust.rotation += dust.velocity.X * 0.05f;
		dust.scale *= 0.99f;
		_ = dust.scale;
		Lighting.AddLight(dust.position, 0.35f, 0.01f, 0.01f);
		if (dust.scale < 0.15f)
		{
			dust.active = false;
		}
		return false;
	}
}
