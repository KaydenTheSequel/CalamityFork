using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.MaceFlails;

public class RemsRevengeProj : BaseMaceFlailProjectile
{
	public override int AssociatedItemID => ModContent.ItemType<RemsRevenge>();

	public override int SpinIFrames => 12;

	public override float SpinSpeed => 16f;

	public override float SpinHitboxRadius => 96f;

	public override float SpinVisualRadius => 64f;

	public override int AfterimageLength => 8;

	public override float LaunchSpeed => 30f;

	public override int LaunchLifespan => 24;

	public override float MaxDropRange => 960f;

	public override float MaxRetractSpeed => 36f;

	public override float RetractAcceleration => 4.5f;

	public ref float LaunchedHitCounter => ref base.Projectile.ai[2];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 26);
		base.SetDefaults();
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<WitherDebuff>(), 240);
		if (base.CurrentFlailState != FlailState.Spinning && base.CurrentFlailState != FlailState.Dropping)
		{
			LaunchedHitCounter++;
		}
		UpdateDamageKB(out var damageMult, out var kbMult);
		if (LaunchedHitCounter <= 5f)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), (base.CurrentFlailState == FlailState.Spinning) ? target.Center : base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<RemsRevengeExplosion>(), (int)((float)base.Projectile.damage * damageMult), base.Projectile.knockBack * kbMult, base.Projectile.owner);
		}
		if (LaunchedHitCounter >= 4f)
		{
			base.CurrentFlailState = FlailState.ForcedRetracting;
			base.StateTimer = 0f;
			base.Projectile.netUpdate = true;
		}
	}
}
