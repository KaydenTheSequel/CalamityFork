using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class TumbleweedRolling : ModProjectile, ILocalizedModType, IModType
{
	public static int Lifetime = 120;

	public static int Rolltime = 30;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Melee/MaceFlails/TumbleweedFlail";

	public ref float RollState => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 42);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 6;
		base.Projectile.timeLeft = Lifetime;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
	}

	public override void AI()
	{
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += base.Projectile.velocity.X * 0.05f;
		if (base.Projectile.timeLeft < Lifetime - Rolltime)
		{
			RollState = 1f;
		}
		if (RollState == 1f && base.Projectile.velocity.Y < 10f)
		{
			base.Projectile.velocity.Y += 0.6f;
		}
		Vector2 position = base.Projectile.position;
		int width = base.Projectile.width;
		int height = base.Projectile.height;
		float scale = Main.rand.NextFloat(0.6f, 1.2f);
		Dust dust = Dust.NewDustDirect(position, width, height, 32, 0f, 0f, 100, default(Color), scale);
		dust.noGravity = RollState != 1f;
		dust.velocity = base.Projectile.velocity * 0.5f;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		if (RollState != 1f)
		{
			RollState = 1f;
		}
		base.Projectile.penetrate--;
		base.Projectile.numHits++;
		if (oldVelocity.Y != base.Projectile.velocity.Y)
		{
			base.Projectile.velocity.Y = MathHelper.Clamp(oldVelocity.Y * -0.5f * (float)base.Projectile.penetrate, -16f, -2f);
		}
		Point scanAreaStart = base.Projectile.TopLeft.ToTileCoordinates();
		Point scanAreaEnd = base.Projectile.BottomRight.ToTileCoordinates();
		base.Projectile.CreateImpactExplosion(2, base.Projectile.Center, ref scanAreaStart, ref scanAreaEnd, base.Projectile.width, out var causedShockwaves);
		base.Projectile.CreateImpactExplosion2_FlailTileCollision(base.Projectile.Center, causedShockwaves, base.Projectile.velocity);
		TumbleImpactEffects();
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		TumbleImpactEffects();
	}

	public void TumbleImpactEffects()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		float impactIntensity = 1f - (float)base.Projectile.numHits * 0.08f;
		SoundStyle style = SoundID.NPCDeath15 with
		{
			Volume = impactIntensity
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		for (int i = 0; i < (int)(8f * impactIntensity); i++)
		{
			Dust tumbleDust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 32, 0f, 0f, 100, default(Color), 1.2f);
			Dust dust = tumbleDust;
			dust.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				tumbleDust.scale = 0.5f;
				tumbleDust.fadeIn = Main.rand.NextFloat(1f, 1.1f);
			}
			tumbleDust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 85, 0f, 0f, 100, default(Color), 1.7f);
			tumbleDust.noGravity = true;
			Dust dust2 = tumbleDust;
			dust2.velocity *= 5f;
			tumbleDust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 85, 0f, 0f, 100);
			Dust dust3 = tumbleDust;
			dust3.velocity *= 2f;
		}
	}
}
