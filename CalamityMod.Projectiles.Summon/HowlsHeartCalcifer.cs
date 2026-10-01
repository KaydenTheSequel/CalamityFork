using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class HowlsHeartCalcifer : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.LightPet[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		bool correctMinion = base.Projectile.type == ModContent.ProjectileType<HowlsHeartCalcifer>();
		if ((!modPlayer.howlsHeart && !modPlayer.howlsHeartVanity) || !player.active)
		{
			base.Projectile.active = false;
			return;
		}
		if (correctMinion)
		{
			if (player.dead)
			{
				modPlayer.howlTrio = false;
			}
			if (modPlayer.howlTrio)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		if (!modPlayer.howlsHeartVanity)
		{
			Lighting.AddLight(base.Projectile.Center, 0.75f, 0.485f, 0f);
		}
		base.Projectile.FloatingPetAI(faceRight: false, 0.04f, lightPet: true);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
