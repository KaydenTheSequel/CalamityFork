using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class GolemInfernoBolt : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 12);
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
	}

	public override void AI()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[2] == 0f)
		{
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
			base.Projectile.ai[2] = 1f;
		}
		bool num = (base.Projectile.velocity.X < 0f && base.Projectile.Center.X < base.Projectile.ai[0]) || (base.Projectile.velocity.X > 0f && base.Projectile.Center.X > base.Projectile.ai[0]);
		bool hasReachedY = (base.Projectile.velocity.Y < 0f && base.Projectile.Center.Y < base.Projectile.ai[1]) || (base.Projectile.velocity.Y > 0f && base.Projectile.Center.Y > base.Projectile.ai[1]);
		if (num & hasReachedY)
		{
			base.Projectile.Kill();
		}
		GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, Main.rand.NextVector2Circular(3f, 3f), Color.Gray, Color.DarkGray, 0.5f, 192f));
		Dust.NewDustPerfect(base.Projectile.Center, 174, Main.rand.NextVector2Circular(2f, 2f), 0, default(Color), 1.75f);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(24, 360);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer == base.Projectile.owner)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<GolemInfernoBlast>(), base.Projectile.damage, 0f, base.Projectile.owner);
		}
	}
}
