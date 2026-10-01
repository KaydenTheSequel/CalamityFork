using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.BaseProjectiles;

public abstract class BaseSporeSacProjectile : ModProjectile
{
	public Player Owner => Main.player[base.Projectile.owner];

	public bool FadingOut => GeneralTimer > 5400f;

	public bool HomesInStronglyOnEnemies => base.Projectile.ai[1] == 1f;

	public ref float GeneralTimer => ref base.Projectile.ai[0];

	public ref float PulseIncrement => ref base.Projectile.localAI[0];

	public ref float MoveTimer => ref base.Projectile.localAI[1];

	public virtual Color? LightColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(0.25f, 0.025f, 0.275f);
		}
	}

	public virtual float HomeDistance => 600f;

	public virtual float HomeSpeed => 0.75f;

	public override void AI()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_037a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_0396: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b5: Unknown result type (might be due to invalid IL or missing references)
		if (LightColor.HasValue)
		{
			float lightFactor = base.Projectile.Opacity * base.Projectile.scale;
			Vector2 center = base.Projectile.Center;
			Color value = LightColor.Value;
			Lighting.AddLight(center, ((Color)(ref value)).ToVector3() * lightFactor);
		}
		PulseIncrement++;
		if (PulseIncrement >= 90f)
		{
			PulseIncrement *= -1f;
		}
		base.Projectile.scale += (float)(PulseIncrement >= 0f).ToDirectionInt() * 0.003f;
		base.Projectile.rotation += base.Projectile.scale * 0.0025f;
		Vector2 moveDirection = Vector2.One;
		switch (base.Projectile.identity % 6)
		{
		case 0:
			moveDirection.X *= -1f;
			break;
		case 1:
			moveDirection.Y *= -1f;
			break;
		case 2:
			moveDirection *= -1f;
			break;
		case 3:
			moveDirection.X = 0f;
			break;
		case 4:
			moveDirection.Y = 0f;
			break;
		}
		MoveTimer++;
		if (MoveTimer > 60f)
		{
			MoveTimer = -180f;
		}
		Projectile projectile = base.Projectile;
		projectile.velocity += moveDirection * (float)(MoveTimer >= -60f).ToDirectionInt() * 0.002f;
		GeneralTimer++;
		if (FadingOut)
		{
			base.Projectile.damage = 0;
			if (base.Projectile.alpha < 255)
			{
				base.Projectile.alpha = Utils.Clamp(base.Projectile.alpha + 5, 0, 255);
			}
			else if (base.Projectile.owner == Main.myPlayer)
			{
				base.Projectile.Kill();
			}
		}
		else
		{
			float ownerDistanceIncrement = base.Projectile.Distance(Owner.Center);
			if (ownerDistanceIncrement > 400f)
			{
				ownerDistanceIncrement *= 1.1f;
			}
			if (ownerDistanceIncrement > 500f)
			{
				ownerDistanceIncrement *= 1.2f;
			}
			if (ownerDistanceIncrement > 600f)
			{
				ownerDistanceIncrement *= 1.3f;
			}
			if (ownerDistanceIncrement > 700f)
			{
				ownerDistanceIncrement *= 1.4f;
			}
			if (ownerDistanceIncrement > 800f)
			{
				ownerDistanceIncrement *= 1.5f;
			}
			if (ownerDistanceIncrement > 900f)
			{
				ownerDistanceIncrement *= 1.6f;
			}
			if (ownerDistanceIncrement > 1000f)
			{
				ownerDistanceIncrement *= 1.7f;
			}
			GeneralTimer += ownerDistanceIncrement * 0.01f;
			base.Projectile.alpha = Utils.Clamp(base.Projectile.alpha - 10, 50, 255);
		}
		if (HomesInStronglyOnEnemies || (float)base.Projectile.timeLeft < 3300f + PulseIncrement)
		{
			NPC potentialTarget = base.Projectile.Center.ClosestNPCAt(HomeDistance);
			if (potentialTarget != null)
			{
				if (HomesInStronglyOnEnemies)
				{
					base.Projectile.extraUpdates = 5;
				}
				base.Projectile.velocity = (base.Projectile.velocity * 10f + base.Projectile.SafeDirectionTo(potentialTarget.Center) * HomeSpeed) / 11f;
				return;
			}
		}
		if (((Vector2)(ref base.Projectile.velocity)).Length() > 0.2f)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.98f;
		}
	}
}
