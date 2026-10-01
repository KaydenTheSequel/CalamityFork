using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class PermafrostBlaster : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle SANSCharge = new SoundStyle("CalamityMod/Sounds/Custom/Ravager/GasterBlasterCharge");

	public static readonly SoundStyle SANSFire = new SoundStyle("CalamityMod/Sounds/Custom/Ravager/GasterBlasterFire");

	public Vector2 storedVelocity;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Items/Accessories/PermafrostsConcoction";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 42);
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 180;
		base.CooldownSlot = 1;
	}

	public override void AI()
	{
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] < 90f)
		{
			base.Projectile.ai[0]++;
			if (storedVelocity == Vector2.Zero)
			{
				storedVelocity = base.Projectile.SafeDirectionTo(base.Projectile.velocity);
				base.Projectile.velocity = Vector2.Zero;
				base.Projectile.netUpdate = true;
				base.Projectile.rotation = (float)Math.Atan2(storedVelocity.Y, storedVelocity.X) + (float)Math.PI / 2f;
				SoundEngine.PlaySound(in SANSCharge, base.Projectile.Center);
			}
			else if (base.Projectile.ai[0] >= 55f)
			{
				base.Projectile.ai[0] = 90f;
				if (base.Projectile.owner == Main.myPlayer)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, storedVelocity, ModContent.ProjectileType<PermafrostBlast>(), base.Projectile.damage, 0f, base.Projectile.owner, base.Projectile.ai[1], base.Projectile.whoAmI);
				}
				SoundEngine.PlaySound(in SANSFire, base.Projectile.Center);
			}
		}
		else
		{
			if (base.Projectile.velocity == Vector2.Zero)
			{
				base.Projectile.velocity = storedVelocity * -1f;
			}
			else
			{
				Projectile projectile = base.Projectile;
				projectile.velocity *= 1.01f;
			}
			base.Projectile.alpha += 3;
			if (base.Projectile.alpha >= 255)
			{
				base.Projectile.Kill();
			}
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
