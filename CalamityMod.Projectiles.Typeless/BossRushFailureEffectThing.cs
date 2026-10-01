using CalamityMod.Events;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent.Events;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class BossRushFailureEffectThing : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public Player Owner => Main.player[base.Projectile.owner];

	public ref float Time => ref base.Projectile.ai[0];

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 2;
		base.Projectile.height = 2;
		base.Projectile.aiStyle = -1;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 120;
		base.Projectile.penetrate = -1;
	}

	public override void AI()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		MoonlordDeathDrama.RequestLight(Utils.GetLerpValue(0f, 8f, Time, clamped: true), Main.LocalPlayer.Center);
		if (Time >= 45f)
		{
			SoundEngine.PlaySound(in SoundID.DD2_EtherianPortalOpen, Main.LocalPlayer.Center);
			BossRushEvent.End();
			base.Projectile.Kill();
		}
		Time++;
	}
}
