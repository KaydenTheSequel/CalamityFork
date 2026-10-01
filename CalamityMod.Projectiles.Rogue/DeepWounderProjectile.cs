using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class DeepWounderProjectile : ModProjectile, ILocalizedModType, IModType
{
	public NPC StuckTo;

	public Vector2 StuckOffset;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/DeepWounder";

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 600;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 20;
	}

	public override void AI()
	{
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation += ((StuckTo == null) ? 0.3f : 0.6f) * (float)base.Projectile.direction;
		if (!base.Projectile.Calamity().stealthStrike)
		{
			return;
		}
		int spriteWidth = 52;
		int spriteHeight = 48;
		for (int i = 0; i < 10; i++)
		{
			int dust = Dust.NewDust(base.Projectile.position, spriteWidth, spriteHeight, 33, base.Projectile.velocity.X * 0.1f, base.Projectile.velocity.Y * 0.1f);
			Main.dust[dust].noGravity = true;
		}
		if (StuckTo != null)
		{
			if (!StuckTo.CanBeChasedBy(base.Projectile))
			{
				base.Projectile.Kill();
			}
			base.Projectile.Center = StuckTo.Center + StuckOffset;
			base.Projectile.velocity = Vector2.Zero;
			if (base.Projectile.timeLeft % 5 == 0)
			{
				Vector2 waterVelocity = (base.Projectile.Center - StuckTo.Center).SafeNormalize(Vector2.UnitX).RotatedByRandom(0.5235987901687622) * 8f;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, waterVelocity, ModContent.ProjectileType<DeepWounderWater>(), (int)((float)base.Projectile.damage * 0.1f), 1f, base.Projectile.owner);
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 120);
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 120);
		if (base.Projectile.Calamity().stealthStrike && StuckTo == null)
		{
			StuckTo = target;
			StuckOffset = base.Projectile.Center - target.Center;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<ArmorCrunch>(), 120);
		target.AddBuff(ModContent.BuffType<CrushDepth>(), 120);
		if (base.Projectile.Calamity().stealthStrike)
		{
			target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 120);
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		Collision.HitTiles(base.Projectile.position + base.Projectile.velocity, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.position);
		base.Projectile.Kill();
		return false;
	}

	public DeepWounderProjectile()
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		StuckOffset = Vector2.Zero;
		base._002Ector();
	}
}
