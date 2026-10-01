using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Dusts;

public class AdrenDust : ModDust
{
	public override void OnSpawn(Dust dust)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		dust.color = default(Color);
		dust.velocity *= 0.5f;
		dust.noGravity = true;
		dust.noLight = true;
		dust.alpha = 50;
	}

	public override bool Update(Dust dust)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		dust.position += dust.velocity;
		dust.scale *= 0.94f;
		dust.velocity *= 0.9f;
		if (dust.scale < 0.4f)
		{
			dust.active = false;
		}
		return false;
	}

	public override Color? GetAlpha(Dust dust, Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		Color white = Color.White;
		byte r = ((Color)(ref white)).R;
		white = Color.White;
		byte g = ((Color)(ref white)).G;
		white = Color.White;
		return new Color((int)r, (int)g, (int)((Color)(ref white)).B, dust.alpha);
	}
}
