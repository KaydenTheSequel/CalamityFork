using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace CalamityMod.Particles;

public class WulfrumBastionPartsParticle : Particle
{
	internal Rectangle Frame;

	internal Vector2 DestinationOffset;

	public Vector2 Offset;

	public float RotationOffset;

	public Player Owner;

	public float TimeOffset;

	public int AnimationTime;

	public override bool SetLifetime => true;

	public override string Texture => "CalamityMod/Particles/WulfrumBastionParts";

	public override bool UseCustomDraw => true;

	public float LifetimeCompletionAdjusted => Math.Clamp(((float)Time - (float)Lifetime * TimeOffset) / (float)AnimationTime, 0f, 1f);

	public WulfrumBastionPartsParticle(Player owner, int variant, int lifetime)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		base._002Ector();
		Offset = (-Vector2.UnitY * (40f + Main.rand.NextFloat(34f))).RotatedByRandom(0.03141592815518379);
		Owner = owner;
		Position = Owner.Center;
		Scale = 1f;
		Color = Color.White;
		Velocity = Vector2.Zero;
		Rotation = 0f;
		RotationOffset = Main.rand.NextFloat(0f) * (Main.rand.NextBool() ? (-1f) : 1f);
		Lifetime = lifetime;
		AnimationTime = (int)((float)lifetime * 0.44f);
		switch (variant)
		{
		case 0:
			Frame = new Rectangle(46, 4, 10, 14);
			DestinationOffset = new Vector2(7f, 16f);
			TimeOffset = 0f;
			break;
		case 1:
			Frame = new Rectangle(30, 4, 12, 14);
			DestinationOffset = new Vector2(-4f, 16f);
			TimeOffset = 0f;
			break;
		case 2:
			Frame = new Rectangle(4, 30, 22, 12);
			DestinationOffset = new Vector2(1f, 5f);
			TimeOffset = 0.15f;
			break;
		case 3:
			Frame = new Rectangle(30, 24, 30, 16);
			DestinationOffset = new Vector2(1f, -3f);
			TimeOffset = 0.35f;
			break;
		case 4:
			Frame = new Rectangle(2, 2, 22, 24);
			DestinationOffset = new Vector2(-1f, -15f);
			TimeOffset = 0.43f;
			break;
		case 5:
			Frame = new Rectangle(14, 46, 38, 18);
			DestinationOffset = new Vector2(-2f, -2f);
			TimeOffset = 0.6f;
			break;
		}
		Origin = Frame.Size() / 2f;
		if (variant == 5)
		{
			Origin.X -= 12f;
		}
	}

	public override void Update()
	{
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		Rotation = MathHelper.Lerp(RotationOffset, 0f, (float)Math.Pow(LifetimeCompletionAdjusted, 0.800000011920929));
		if (TimeOffset == 0.6f)
		{
			Rotation = (Owner.Calamity().mouseWorld - Owner.Center).ToRotation() + ((Owner.direction < 0) ? ((float)Math.PI) : 0f);
		}
		if (Owner.dead || !Owner.active)
		{
			Kill();
		}
	}

	public override void CustomDraw(SpriteBatch spriteBatch)
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_017e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		if (!Owner.dead && Owner.active)
		{
			Texture2D baseTex = GeneralParticleHandler.GetTexture(Type);
			Vector2 center = default(Vector2);
			((Vector2)(ref center))._002Ector((float)(int)Owner.MountedCenter.X, (float)(int)(Owner.MountedCenter.Y + Owner.gfxOffY));
			Vector2 currentOffset = Vector2.Lerp(Offset, Vector2.Zero, (float)Math.Pow(LifetimeCompletionAdjusted, 0.800000011920929));
			Color lightColor = Lighting.GetColor(Owner.Center.ToTileCoordinates());
			Vector2 realDestinationOffset = default(Vector2);
			((Vector2)(ref realDestinationOffset))._002Ector(DestinationOffset.X * (float)Owner.direction, DestinationOffset.Y * Owner.gravDir);
			Vector2 realCurrentOffset = default(Vector2);
			((Vector2)(ref realCurrentOffset))._002Ector(currentOffset.X * (float)Owner.direction, currentOffset.Y * Owner.gravDir);
			SpriteEffects spriteEffect = (SpriteEffects)0;
			Vector2 origin = Origin;
			if (Owner.direction < 0)
			{
				spriteEffect = (SpriteEffects)1;
				origin.X = (float)Frame.Width - Origin.X;
			}
			float opacity = Math.Clamp(LifetimeCompletionAdjusted * 4f, 0f, 1f);
			spriteBatch.Draw(baseTex, center + realDestinationOffset + realCurrentOffset - Main.screenPosition, (Rectangle?)Frame, lightColor * opacity, Rotation, origin, Scale, spriteEffect, 0f);
		}
	}
}
