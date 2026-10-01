using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace CalamityMod.NPCs;

public class BaseWormSegment
{
	public int segmentType;

	public Vector2 Center;

	public float rotation;

	public Vector2 velocity;

	public float Opacity;

	public BaseWormSegment(ModNPC Head, int segmentStyle = 0)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		Center = Vector2.Zero;
		velocity = Vector2.Zero;
		Opacity = 1f;
		base._002Ector();
		Center = Head.NPC.Center;
		rotation = Head.NPC.rotation;
		velocity = Head.NPC.velocity;
		segmentType = segmentStyle;
	}

	public BaseWormSegment(ModProjectile Head, int segmentStyle = 0)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		Center = Vector2.Zero;
		velocity = Vector2.Zero;
		Opacity = 1f;
		base._002Ector();
		Center = Head.Projectile.Center;
		rotation = Head.Projectile.rotation;
		velocity = Head.Projectile.velocity;
		segmentType = segmentStyle;
	}
}
