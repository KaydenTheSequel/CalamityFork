using CalamityMod.CalPlayer;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class ThirdSage : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		Main.projFrames[base.Type] = 7;
		ProjectileID.Sets.CharacterPreviewAnimations[base.Type] = ProjectileID.Sets.SimpleLoop(0, 4, 6).WithOffset(-15f, -5f).WithSpriteDirection(1)
			.WhenNotSelected(0, 0);
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = 42;
		base.Projectile.height = 42;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		CalamityPlayer modPlayer = player.Calamity();
		if (player.dead)
		{
			modPlayer.thirdSage = false;
		}
		if (modPlayer.thirdSage)
		{
			base.Projectile.timeLeft = 2;
		}
		base.Projectile.FloatingPetAI(faceRight: true, 0.1f);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 4 && base.Projectile.ai[1] < 45f)
		{
			base.Projectile.frame = 0;
			base.Projectile.ai[1]++;
		}
		else if (base.Projectile.frame == 4 && base.Projectile.ai[1] >= 45f)
		{
			SoundEngine.PlaySound(in SoundID.Zombie32, base.Projectile.Center);
		}
		else if (base.Projectile.frame > 6)
		{
			base.Projectile.frame = 0;
			base.Projectile.ai[1] = 0f;
		}
	}
}
