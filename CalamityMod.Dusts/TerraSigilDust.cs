using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Dusts;

public class TerraSigilDust : ModDust
{
	public override void OnSpawn(Dust dust)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		dust.noLight = true;
		dust.rotation = Main.rand.NextFloat((float)Math.PI * 2f);
		dust.fadeIn = 1f;
		dust.frame = new Rectangle(0, Main.rand.Next(3) * 12, 10, 12);
	}

	public override bool Update(Dust dust)
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		dust.velocity.Y += 0.2f;
		dust.velocity *= 0.983f;
		dust.position += dust.velocity;
		dust.alpha++;
		if (dust.alpha > 255)
		{
			dust.active = false;
		}
		return false;
	}
}
