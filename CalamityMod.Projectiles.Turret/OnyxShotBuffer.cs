using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Turret;

public class OnyxShotBuffer : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Misc";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 6;
		base.Projectile.height = 6;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 1;
	}

	public override void AI()
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundStyle style = SoundID.Item36 with
			{
				Volume = 0.65f
			};
			SoundEngine.PlaySound(in style, base.Projectile.Center);
			base.Projectile.localAI[0] = 1f;
		}
		if (Main.netMode != 1)
		{
			IEntitySource source = Main.LocalPlayer.GetSource_FromThis();
			for (int i = -1; i < 2; i++)
			{
				Projectile.NewProjectile(source, base.Projectile.Center + new Vector2(3f, 0f), base.Projectile.velocity.RotatedBy(Main.rand.NextFloat(0.035f, 0.11f) * (float)i), ModContent.ProjectileType<OnyxShot>(), base.Projectile.damage, base.Projectile.knockBack, Main.myPlayer);
			}
			base.Projectile.Kill();
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
