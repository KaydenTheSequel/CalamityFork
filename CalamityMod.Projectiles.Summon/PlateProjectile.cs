using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class PlateProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		Main.projFrames[base.Type] = 5;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 16;
		base.Projectile.friendly = true;
		base.Projectile.timeLeft = 180;
		base.Projectile.aiStyle = 1;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		base.Projectile.velocity.X *= 0.9995f;
		base.Projectile.velocity.Y = base.Projectile.velocity.Y + 0.01f;
		base.Projectile.rotation -= MathHelper.ToRadians(90f) * (float)base.Projectile.direction;
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 3)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 5)
		{
			base.Projectile.frame = 0;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Shatter, base.Projectile.Center);
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		for (int index = 0; index < 3; index++)
		{
			float SpeedX = (0f - base.Projectile.velocity.X) * (float)Main.rand.Next(40, 70) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
			float SpeedY = (0f - base.Projectile.velocity.Y) * (float)Main.rand.Next(40, 70) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
			int shard = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X + SpeedX, base.Projectile.Center.Y + SpeedY, SpeedX, SpeedY, 90, base.Projectile.damage / 2, 0f, base.Projectile.owner);
			if (shard.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[shard].DamageType = DamageClass.Summon;
			}
		}
	}
}
