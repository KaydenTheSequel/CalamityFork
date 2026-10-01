using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Dusts;

public class RosePiece : ModDust
{
	public override void OnSpawn(Dust dust)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		dust.color = default(Color);
		dust.rotation = Main.rand.NextFloat((float)Math.PI * 2f);
	}

	public override bool Update(Dust dust)
	{
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		dust.rotation += MathHelper.ToRadians(1.56f);
		if (!dust.noGravity)
		{
			dust.velocity.Y = MathHelper.Clamp(dust.velocity.Y + 0.15f, -1.8f, 2.5f);
		}
		Vector2 velocity = Vector2.UnitY.RotatedBy(dust.rotation) * new Vector2(2f, 0.5f);
		if (velocity != Collision.TileCollision(dust.position, velocity, (int)(dust.scale * 4f), (int)(dust.scale * 4f)))
		{
			dust.rotation = -1f;
		}
		dust.position += velocity + dust.velocity;
		dust.scale = MathHelper.Clamp(dust.scale - 0.01f, 0f, 4f) * 0.99f;
		if (dust.scale < 0.4f)
		{
			dust.active = false;
		}
		return false;
	}

	public override Color? GetAlpha(Dust dust, Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		return lightColor;
	}
}
