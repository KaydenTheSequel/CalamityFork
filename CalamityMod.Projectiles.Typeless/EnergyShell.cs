using CalamityMod.Cooldowns;
using CalamityMod.Items.Weapons.Melee;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class EnergyShell : ModProjectile, ILocalizedModType, IModType
{
	private bool playedSound;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 58;
		base.Projectile.height = 72;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 300;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		if (!playedSound)
		{
			SoundEngine.PlaySound(in SoundID.Item92, base.Projectile.position);
			playedSound = true;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 6)
		{
			base.Projectile.frame = 0;
		}
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0.15f / 255f, (float)(255 - base.Projectile.alpha) * 0.15f / 255f, (float)(255 - base.Projectile.alpha) * 0.01f / 255f);
		if (base.Projectile.timeLeft < 51)
		{
			base.Projectile.alpha += 5;
		}
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.Center = player.Center;
		if (player.dead || player == null || player.HeldItem.type != ModContent.ItemType<LionHeart>())
		{
			base.Projectile.Kill();
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0027: Unknown result type (might be due to invalid IL or missing references)
		Player p = Main.player[base.Projectile.owner];
		SoundEngine.PlaySound(in SoundID.Item94, base.Projectile.position);
		p.AddCooldown(LionHeartShield.ID, CalamityUtils.SecondsToFrames(45));
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
