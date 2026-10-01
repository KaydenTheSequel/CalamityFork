using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.NPCs;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

[PierceResistException(true)]
public class LightspeedM1Hitbox : ModProjectile, ILocalizedModType, IModType
{
	public bool gotEnergyThisSwing;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetDefaults()
	{
		base.Projectile.width = 84;
		base.Projectile.height = 64;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 6;
		base.Projectile.DamageType = TrueMeleeNoSpeedDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 4;
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		Vector2 toMouse = Owner.Center.DirectionTo(Owner.ClampedMouseWorld() + ((float)Math.PI).ToRotationVector2());
		float rotation = toMouse.ToRotation();
		base.Projectile.rotation = rotation;
		base.Projectile.velocity = toMouse * 34f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		Vector2 toMouse = Owner.Center.DirectionTo(Owner.ClampedMouseWorld() + ((float)Math.PI).ToRotationVector2());
		if (!gotEnergyThisSwing)
		{
			gotEnergyThisSwing = true;
			CalamityPlayer calamityPlayer = Main.player[base.Projectile.owner].Calamity();
			calamityPlayer.elementalMastery += 4;
			calamityPlayer.elementalMastery = Math.Min(calamityPlayer.elementalMastery, Lightspeed.MaxEnergy);
		}
		for (int i = 0; i < 2; i++)
		{
			Vector2 particleSpeed = (target.Center * toMouse).SafeNormalize(Vector2.One).RotatedByRandom(0.6283185482025146) * Main.rand.NextFloat(7f, 14f);
			GeneralParticleHandler.SpawnParticle(new SquishyLightParticle(target.Center, particleSpeed, Main.rand.NextFloat(0.25f, 0.5f), Color.OrangeRed, 22, 2f, 2.5f, 3f, 0.06f));
		}
		target.AddBuff(ModContent.BuffType<ElementalMix>(), 60);
	}
}
