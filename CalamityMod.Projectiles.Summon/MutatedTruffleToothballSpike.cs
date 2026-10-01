using System;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class MutatedTruffleToothballSpike : ModProjectile, ILocalizedModType, IModType
{
	public const int TimeForHitbox = 15;

	public new string LocalizationCategory => "Projectiles.Summon";

	public ref float TargetShotID => ref base.Projectile.ai[0];

	public ref float TimerForHitbox => ref base.Projectile.ai[1];

	public NPC TargetShot => Main.npc[(int)TargetShotID];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.timeLeft = 600;
		base.Projectile.width = (base.Projectile.height = 26);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.netImportant = true;
	}

	public override void AI()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		TimerForHitbox++;
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (TargetShot != null && TargetShot.active && TimerForHitbox >= 15f)
		{
			float inertia = 25f;
			base.Projectile.velocity = (base.Projectile.velocity * inertia + base.Projectile.SafeDirectionTo(TargetShot.Center) * Utils.Remap(base.Projectile.timeLeft, 600f, 540f, MutatedTruffle.ToothballSpikeSpeed - 15f, MutatedTruffle.ToothballSpikeSpeed)) / (inertia + 1f);
			base.Projectile.ForceNetUpdate();
		}
	}

	public override bool? CanDamage()
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		if (TimerForHitbox >= 15f)
		{
			Rectangle rect = base.Projectile.getRect();
			if (((Rectangle)(ref rect)).Intersects(TargetShot.getRect()))
			{
				return null;
			}
		}
		return false;
	}
}
