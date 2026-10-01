using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class BlackAnurianPlankton : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 5;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.extraUpdates = 3;
	}

	public override void AI()
	{
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02da: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 4)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.alpha > 0)
		{
			base.Projectile.alpha -= 50;
		}
		else
		{
			base.Projectile.extraUpdates = 0;
		}
		if (base.Projectile.alpha < 0)
		{
			base.Projectile.alpha = 0;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		float projX = base.Projectile.position.X;
		float projY = base.Projectile.position.Y;
		float homingRange = 100000f;
		bool isHoming = false;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 30f)
		{
			base.Projectile.ai[0] = 30f;
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if (n.CanBeChasedBy(base.Projectile) && !n.wet)
				{
					float npcX = n.position.X + (float)(n.width / 2);
					float npcY = n.position.Y + (float)(n.height / 2);
					float npcDist = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - npcX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - npcY);
					if (npcDist < 800f && npcDist < homingRange && Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, n.position, n.width, n.height))
					{
						homingRange = npcDist;
						projX = npcX;
						projY = npcY;
						isHoming = true;
					}
				}
			}
		}
		if (!isHoming)
		{
			projX = base.Projectile.position.X + (float)(base.Projectile.width / 2) + base.Projectile.velocity.X * 100f;
			projY = base.Projectile.position.Y + (float)(base.Projectile.height / 2) + base.Projectile.velocity.Y * 100f;
		}
		float projVelModifier = 0.1f;
		Vector2 projDirection = base.Projectile.Center;
		float xDest = projX - projDirection.X;
		float yDest = projY - projDirection.Y;
		float destinationDist = (float)Math.Sqrt(xDest * xDest + yDest * yDest);
		destinationDist = 6f / destinationDist;
		xDest *= destinationDist;
		yDest *= destinationDist;
		if (base.Projectile.velocity.X < xDest)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X + projVelModifier;
			if (base.Projectile.velocity.X < 0f && xDest > 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X + projVelModifier * 2f;
			}
		}
		else if (base.Projectile.velocity.X > xDest)
		{
			base.Projectile.velocity.X = base.Projectile.velocity.X - projVelModifier;
			if (base.Projectile.velocity.X > 0f && xDest < 0f)
			{
				base.Projectile.velocity.X = base.Projectile.velocity.X - projVelModifier * 2f;
			}
		}
		if (base.Projectile.velocity.Y < yDest)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y + projVelModifier;
			if (base.Projectile.velocity.Y < 0f && yDest > 0f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y + projVelModifier * 2f;
			}
		}
		else if (base.Projectile.velocity.Y > yDest)
		{
			base.Projectile.velocity.Y = base.Projectile.velocity.Y - projVelModifier;
			if (base.Projectile.velocity.Y > 0f && yDest < 0f)
			{
				base.Projectile.velocity.Y = base.Projectile.velocity.Y - projVelModifier * 2f;
			}
		}
	}
}
