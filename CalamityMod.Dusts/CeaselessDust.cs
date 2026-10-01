using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Dusts;

public class CeaselessDust : ModDust
{
	public override void OnSpawn(Dust dust)
	{
		dust.noGravity = true;
	}

	public override bool Update(Dust dust)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		float scale = dust.scale;
		Lighting.AddLight((int)(dust.position.X / 16f), (int)(dust.position.Y / 16f), scale * 0.4f, scale * 0.1f, scale);
		dust.position += dust.velocity;
		dust.velocity *= 0.95f;
		dust.scale -= 0.05f;
		if (dust.scale <= 0.05f)
		{
			dust.active = false;
		}
		return false;
	}

	public override Color? GetAlpha(Dust dust, Color lightColor)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		int num = (int)(250f * dust.scale);
		return new Color(num, num, num, 0);
	}
}
