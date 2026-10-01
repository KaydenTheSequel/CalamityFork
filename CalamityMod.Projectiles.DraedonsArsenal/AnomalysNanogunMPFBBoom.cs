using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.DraedonsArsenal;

public class AnomalysNanogunMPFBBoom : BaseMassiveExplosionProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle MPFBExplosion = new SoundStyle("CalamityMod/Sounds/Item/AnomalysNanogunMPFBExplosion");

	public new string LocalizationCategory => "Projectiles.Misc";

	public override int Lifetime => 40;

	public override bool UsesScreenshake => true;

	public override float GetScreenshakePower(float pulseCompletionRatio)
	{
		return CalamityUtils.Convert01To010(pulseCompletionRatio) * 3f;
	}

	public override Color GetCurrentExplosionColor(float pulseCompletionRatio)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		return Color.Lerp(Color.Blue, Color.CornflowerBlue, MathHelper.Clamp(pulseCompletionRatio * 2.2f, 0f, 1f));
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 2);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.DamageType = DamageClass.Ranged;
	}

	public override void PostAI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0f, base.Projectile.Opacity * 0.7f / 255f, base.Projectile.Opacity);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.numHits > 0)
		{
			base.Projectile.damage = (int)((float)base.Projectile.damage * 0.6f);
		}
		if (base.Projectile.damage < 1)
		{
			base.Projectile.damage = 1;
		}
	}
}
