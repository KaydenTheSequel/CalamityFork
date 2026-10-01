using System;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class SporeKnifeBud : ModProjectile, ILocalizedModType, IModType
{
	public bool Sticky;

	public static int Lifetime = 300;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/Summon/PlantationStaffTentacle";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 50;
		base.Projectile.timeLeft = Lifetime;
	}

	public override bool? CanHitNPC(NPC target)
	{
		return base.Projectile.timeLeft <= 285 && target.CanBeChasedBy(base.Projectile);
	}

	public override void AI()
	{
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI));
		if (!Sticky && base.Projectile.timeLeft <= Lifetime - 15)
		{
			CalamityUtils.HomeInOnNPC(base.Projectile, !base.Projectile.tileCollide, 450f, 6.5f, 20f);
			int dust = Dust.NewDust(base.Projectile.position - new Vector2(10f, 10f), 30, 30, 298, base.Projectile.velocity.X, base.Projectile.velocity.Y, 50, default(Color), Main.rand.NextFloat(0.3f, 0.7f));
			Main.dust[dust].noGravity = true;
		}
		else if (Sticky)
		{
			base.Projectile.StickyProjAI(15);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SporeKnife.ChompSound, base.Projectile.Center);
		Sticky = true;
		target.AddBuff(20, 60);
		for (int i = 0; i < 3; i++)
		{
			Vector2 smokeVel = Main.rand.NextVector2Unit() * Main.rand.NextVector2Circular(6f, 6f);
			Color smokeColor = (Main.rand.NextBool(2) ? Color.GreenYellow : Color.Green);
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, smokeVel, smokeColor, Color.Black, Main.rand.NextFloat(0.2f, 0.4f), 250 - Main.rand.Next(60), 0.08f));
		}
		for (int k = 0; k < 2; k++)
		{
			Dust dust = Dust.NewDustPerfect(base.Projectile.Center, 15, Utils.RotatedByRandom(new Vector2(2f, 2f), 100.0) * Main.rand.NextFloat(0.5f, 0.8f), 0, Color.YellowGreen, Main.rand.NextFloat(0.4f, 0.9f));
			dust.noGravity = false;
			dust.alpha = Main.rand.Next(20, 31);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		base.Projectile.ModifyHitNPCSticky(6);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		for (int d = 0; d < 7; d++)
		{
			int idx = Dust.NewDust(base.Projectile.Center - Vector2.One * 4f, 25, 25, 15, 0f, -2f, 0, Color.YellowGreen, 0.4f);
			Dust obj = Main.dust[idx];
			obj.velocity /= 2f;
		}
	}
}
