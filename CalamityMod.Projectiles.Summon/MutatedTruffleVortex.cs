using System;
using CalamityMod.Items.Weapons.Summon;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class MutatedTruffleVortex : ModProjectile, ILocalizedModType, IModType
{
	private const int TimeFullScale = 120;

	private const int FadeoutTime = 180;

	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public NPC Target
	{
		get
		{
			//IL_0006: Unknown result type (might be due to invalid IL or missing references)
			return base.Projectile.Center.MinionHoming(MutatedTruffle.EnemyDistanceDetection, Owner);
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.localNPCHitCooldown = 15;
		base.Projectile.timeLeft = MutatedTruffle.VortexTimeUntilNextState + 120 + 180;
		base.Projectile.width = (base.Projectile.height = 408);
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.netImportant = true;
	}

	public override void AI()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_0198: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		if (Target != null)
		{
			base.Projectile.Center = base.Projectile.Center.MoveTowards(Target.Center, Utils.Remap(base.Projectile.timeLeft, 180f, 0f, ((Vector2)(ref Target.velocity)).Length() + 10f, 0f));
		}
		base.Projectile.scale = ((base.Projectile.timeLeft > 180) ? Utils.Remap(base.Projectile.timeLeft, MutatedTruffle.VortexTimeUntilNextState + 120 + 180, MutatedTruffle.VortexTimeUntilNextState, 0f, 1f) : Utils.Remap(base.Projectile.timeLeft, 180f, 0f, 1f, 0f));
		base.Projectile.rotation += Utils.Remap(base.Projectile.timeLeft, 180f, 0f, (float)Math.PI / 24f, 0f);
		if (!Main.dedServ)
		{
			base.Projectile.alpha = (int)Utils.Remap(base.Projectile.timeLeft, 180f, 0f, 0f, 255f);
			float dustDistance = (float)base.Projectile.width * base.Projectile.scale + 25f;
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center + Main.rand.NextVector2CircularEdge(dustDistance, dustDistance), 75, null, 0, default(Color), 2f);
			dust.velocity = dust.position.DirectionTo(base.Projectile.Center) * 10f;
			dust.noGravity = true;
			if (base.Projectile.soundDelay == 0)
			{
				base.Projectile.soundDelay = 174;
				SoundStyle style = new SoundStyle("CalamityMod/Sounds/Custom/OldDukeVortex");
				style.Volume = ((base.Projectile.timeLeft > 180) ? 0.4f : 0f);
				SoundEngine.PlaySound(in style, base.Projectile.Center);
			}
		}
	}

	public override void OnSpawn(IEntitySource source)
	{
		base.Projectile.scale = 0f;
		base.Projectile.ForceNetUpdate();
	}

	public override bool? CanDamage()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		if (Target != null)
		{
			Rectangle rect = base.Projectile.getRect();
			if (!((Rectangle)(ref rect)).Intersects(Target.getRect()))
			{
				return false;
			}
			return null;
		}
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			GeneralParticleHandler.SpawnParticle(new DirectionalPulseRing(base.Projectile.Center, Vector2.Zero, Color.GreenYellow, Vector2.One, 0f, 0.05f, 0.6f, 15));
		}
	}
}
