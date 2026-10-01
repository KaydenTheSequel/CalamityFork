using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class RavagerBlaster : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle SANSCharge = new SoundStyle("CalamityMod/Sounds/Custom/Ravager/GasterBlasterCharge");

	public static readonly SoundStyle SANSFire = new SoundStyle("CalamityMod/Sounds/Custom/Ravager/GasterBlasterFire");

	public Vector2 storedVelocity;

	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/NPCs/Ravager/RavagerHead";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 80);
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 180;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 1f, 1f, 1f);
		if (base.Projectile.ai[0] < 90f)
		{
			base.Projectile.ai[0]++;
			if (storedVelocity == Vector2.Zero)
			{
				storedVelocity = base.Projectile.SafeDirectionTo(base.Projectile.velocity);
				base.Projectile.velocity = Vector2.Zero;
				base.Projectile.netUpdate = true;
				base.Projectile.rotation = (float)Math.Atan2(storedVelocity.Y, storedVelocity.X) - (float)Math.PI / 2f;
				SoundEngine.PlaySound(in SANSCharge, base.Projectile.Center);
			}
			else if (base.Projectile.ai[0] >= 55f)
			{
				base.Projectile.ai[0] = 90f;
				if (base.Projectile.owner == Main.myPlayer)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, storedVelocity, ModContent.ProjectileType<RavagerBlast>(), base.Projectile.damage, 0f, base.Projectile.owner, base.Projectile.ai[1], base.Projectile.whoAmI);
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

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.DrawProjectileWithBackglow(Color.LightGray, lightColor, 5f, null, null, (SpriteEffects)0);
		return false;
	}
}
