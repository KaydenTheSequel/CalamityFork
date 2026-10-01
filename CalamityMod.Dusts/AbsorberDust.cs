using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Dusts;

public class AbsorberDust : ModDust
{
	public override void OnSpawn(Dust dust)
	{
		dust.velocity.Y = (float)Main.rand.Next(-10, 6) * 0.1f;
		dust.velocity.X *= 0.3f;
		dust.scale *= 0.7f;
	}

	public override bool MidUpdate(Dust dust)
	{
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (!dust.noGravity)
		{
			dust.velocity.Y += 0.05f;
		}
		if (!dust.noLight)
		{
			float strength = dust.scale * 1.8f;
			Vector3 DustLight = default(Vector3);
			((Vector3)(ref DustLight))._002Ector(0.149f, 0.245f, 0.195f);
			Lighting.AddLight(dust.position, DustLight * strength);
		}
		return true;
	}

	public override Color? GetAlpha(Dust dust, Color lightColor)
	{
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		return new Color((int)((Color)(ref lightColor)).R, (int)((Color)(ref lightColor)).G, (int)((Color)(ref lightColor)).B, 25);
	}
}
