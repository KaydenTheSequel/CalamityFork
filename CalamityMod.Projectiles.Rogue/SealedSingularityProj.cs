using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SealedSingularityProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/SealedSingularity";

	public override void SetDefaults()
	{
		base.Projectile.width = 34;
		base.Projectile.height = 34;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 180;
	}

	public override void AI()
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 70f)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.96f;
		}
		else
		{
			base.Projectile.rotation += 0.3f * (float)base.Projectile.direction;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner == Main.myPlayer)
		{
			SoundEngine.PlaySound(in SoundID.Shatter, base.Projectile.position);
			int blackhole = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<SealedSingularityBlackhole>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, base.Projectile.Calamity().stealthStrike ? (-180f) : 0f);
			if (blackhole.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[blackhole].Center = base.Projectile.Center;
				Main.projectile[blackhole].Calamity().stealthStrike = base.Projectile.Calamity().stealthStrike;
			}
			for (int index = 0; index < 3; index++)
			{
				float SpeedX = (0f - base.Projectile.velocity.X) * (float)Main.rand.Next(40, 70) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
				float SpeedY = (0f - base.Projectile.velocity.Y) * (float)Main.rand.Next(40, 70) * 0.01f + (float)Main.rand.Next(-20, 21) * 0.4f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X + SpeedX, base.Projectile.Center.Y + SpeedY, SpeedX, SpeedY, ModContent.ProjectileType<SealedSingularityGore>(), (int)((double)base.Projectile.damage * 0.25), 0f, base.Projectile.owner, index);
			}
		}
	}
}
