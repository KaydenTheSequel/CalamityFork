using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class LittleLightProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Pets";

	public Player Owner => Main.player[base.Projectile.owner];

	public Color LightColor
	{
		get
		{
			//IL_000f: Unknown result type (might be due to invalid IL or missing references)
			return new Color(160, 251, 255);
		}
	}

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 9;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.LightPet[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 24);
		base.Projectile.friendly = true;
		base.Projectile.netImportant = true;
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		if (!VerifyOwnerIsPresent())
		{
			HandleFrames();
			DoSpinEffect();
			HoverTowardsOwnersShoulder();
			base.Projectile.spriteDirection = Owner.direction;
			Vector2 center = base.Projectile.Center;
			Color lightColor = LightColor;
			Lighting.AddLight(center, ((Color)(ref lightColor)).ToVector3() * 2.5f);
			Time++;
		}
	}

	public bool VerifyOwnerIsPresent()
	{
		if (!Owner.active)
		{
			base.Projectile.Kill();
			return true;
		}
		if (Owner.dead)
		{
			Owner.Calamity().littleLightPet = false;
		}
		if (Owner.Calamity().littleLightPet)
		{
			base.Projectile.timeLeft = 2;
		}
		return false;
	}

	public void HandleFrames()
	{
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 5 % Main.projFrames[base.Type];
	}

	public void DoSpinEffect()
	{
		if ((float)base.Projectile.frameCounter % 180f > 150f)
		{
			base.Projectile.rotation += (float)Math.PI / 15f;
		}
		else
		{
			base.Projectile.rotation = 0f;
		}
	}

	public void HoverTowardsOwnersShoulder()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		Vector2 destination = Owner.Top;
		destination.X -= (float)(Owner.direction * Owner.width) * 1.25f;
		destination.Y += (float)Owner.height * 0.25f;
		destination.Y += (float)Math.Sin((float)Math.PI * 2f * Time / 45f) * 5f;
		base.Projectile.Center = Vector2.Lerp(base.Projectile.Center, destination, 0.125f);
		if (base.Projectile.WithinRange(destination, 10f))
		{
			base.Projectile.Center = destination;
		}
		else
		{
			Projectile projectile = base.Projectile;
			projectile.Center += base.Projectile.SafeDirectionTo(destination) * 4f;
		}
		base.Projectile.Center = destination;
	}
}
