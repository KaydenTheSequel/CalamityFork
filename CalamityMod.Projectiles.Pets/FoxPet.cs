using CalamityMod.CalPlayer;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Pets;

public class FoxPet : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Pets";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 11;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.CharacterPreviewAnimations[base.Type] = ProjectileID.Sets.SimpleLoop(0, 7, 6).WithOffset(-35f, 0f).WithSpriteDirection(1)
			.WhenNotSelected(0, 0);
	}

	public override void SetDefaults()
	{
		base.Projectile.netImportant = true;
		base.Projectile.width = 24;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.aiStyle = 26;
		base.AIType = 334;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
	}

	public override void AI()
	{
		Player player = Main.player[base.Projectile.owner];
		if (!player.active)
		{
			base.Projectile.active = false;
			return;
		}
		CalamityPlayer modPlayer = player.Calamity();
		if (player.dead)
		{
			modPlayer.fox = false;
		}
		if (modPlayer.fox)
		{
			base.Projectile.timeLeft = 2;
		}
		base.Projectile.spriteDirection = base.Projectile.direction;
	}
}
