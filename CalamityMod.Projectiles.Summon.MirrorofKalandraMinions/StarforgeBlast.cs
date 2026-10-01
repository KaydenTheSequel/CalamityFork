using System;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.MirrorofKalandraMinions;

public class StarforgeBlast : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 12000;
	}

	public override void SetDefaults()
	{
		base.Projectile.timeLeft = MirrorofKalandra.Purple_BlastChargeTime;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = MirrorofKalandra.Purple_BlastChargeTime;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.width = (base.Projectile.height = MirrorofKalandra.Purple_BlastSize);
		base.Projectile.ignoreWater = true;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		GeneralParticleHandler.SpawnParticle(new GenericBloom(base.Projectile.Center, Vector2.Zero, Color.Lerp(Color.DarkMagenta, Color.White, MathF.Pow(Utils.GetLerpValue(MirrorofKalandra.Purple_BlastChargeTime, 0f, base.Projectile.timeLeft), 6f)), Utils.Remap(base.Projectile.timeLeft, MirrorofKalandra.Purple_BlastChargeTime, 5f, 0.5f, 4.2f), MirrorofKalandra.Purple_BlastChargeTime));
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		int dustAmount = 100;
		for (int dustIndex = 0; dustIndex < dustAmount; dustIndex++)
		{
			Vector2 velocity = ((float)Math.PI * 2f / (float)dustAmount * (float)dustIndex).ToRotationVector2() * Main.rand.NextFloat(2f, 15f);
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2Circular(MirrorofKalandra.Purple_BlastSize / 2, MirrorofKalandra.Purple_BlastSize / 2), 272, velocity);
			dust.noGravity = true;
			dust.velocity *= 0.8f;
			dust.scale = ((Vector2)(ref velocity)).Length() * 0.08f;
		}
		SoundEngine.PlaySound(in SoundID.Item94, base.Projectile.Center);
	}
}
