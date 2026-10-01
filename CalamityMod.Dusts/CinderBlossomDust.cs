using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Dusts;

public class CinderBlossomDust : ModDust
{
	public override void OnSpawn(Dust dust)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		dust.velocity *= 0.05f;
		dust.velocity.Y *= 0.5f;
		dust.noGravity = true;
		dust.noLight = true;
		dust.alpha = 0;
		dust.fadeIn = 12f;
		dust.scale *= Main.rand.NextFloat(0.75f, 1f);
	}

	public override bool Update(Dust dust)
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		float strength = dust.scale * 1.4f;
		Lighting.AddLight(dust.position, 0.025f * strength, 0.1f * strength, 0.1f * strength);
		if (Main.rand.NextBool(20))
		{
			dust.alpha += 5;
		}
		if (Main.rand.NextBool(12))
		{
			dust.velocity.X += Main.rand.NextFloat(-0.1f, 0.1f);
			dust.velocity.Y += Main.rand.NextFloat(0.02f, 0.08f);
		}
		dust.position -= dust.velocity;
		if (dust.alpha > 255)
		{
			dust.active = false;
		}
		return false;
	}
}
