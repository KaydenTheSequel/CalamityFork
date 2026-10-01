using System;
using CalamityMod.Items.Weapons.Rogue;
using CalamityMod.Particles;
using CalamityMod.Projectiles.Healing;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ShinobiBladeProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/ShinobiBlade";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 3;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.MaxUpdates = 5;
		base.Projectile.timeLeft = 60 * base.Projectile.MaxUpdates;
	}

	public override void AI()
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.spriteDirection = (base.Projectile.direction = (base.Projectile.velocity.X > 0f).ToDirectionInt());
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + ((base.Projectile.spriteDirection == 1) ? 0f : ((float)Math.PI)) + (float)Math.PI / 2f * (float)base.Projectile.spriteDirection;
		if (Main.rand.NextBool(5))
		{
			Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 15).noGravity = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0173: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0182: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		NPC firstTarget = Main.npc[(int)base.Projectile.ai[0]];
		if (base.Projectile.Calamity().stealthStrike && base.Projectile.ai[1] < 8f && (base.Projectile.ai[1] == 0f || firstTarget != null))
		{
			Vector2 targetPos = ((base.Projectile.ai[1] == 0f) ? target.Center : firstTarget.Center);
			Vector2 offset = Vector2.UnitX.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(80f, 120f);
			Vector2 eVelocity = Vector2.UnitX.RotatedBy(offset.ToRotation() + (float)Math.PI) * 4f;
			int realTarget = ((base.Projectile.ai[1] == 0f) ? target.whoAmI : firstTarget.whoAmI);
			Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), targetPos + offset, eVelocity, base.Projectile.type, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, realTarget, base.Projectile.ai[1] + 1f);
			projectile.Calamity().stealthStrike = true;
			projectile.tileCollide = false;
			Vector2 slashVel = base.Projectile.velocity.SafeNormalize(Vector2.UnitX);
			GeneralParticleHandler.SpawnParticle(new GlowSparkParticle(base.Projectile.Center, slashVel, affectedByGravity: false, 12, 0.06f, Color.DarkBlue, Vector2.One, quickShrink: true, glow: true, 0.9f));
			SoundEngine.PlaySound(in WulfrumKnife.TileHitSound, base.Projectile.Center);
		}
		if (target.life <= 0)
		{
			Main.player[base.Projectile.owner].SpawnLifeStealProjectile(target, base.Projectile, ModContent.ProjectileType<ShinobiHealOrb>(), 10, 0f);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		int dustType = 42;
		for (int i = 0; i < 5; i++)
		{
			Dust.NewDustDirect(base.Projectile.Center, 1, 1, dustType, base.Projectile.velocity.X / 2f, base.Projectile.velocity.Y / 2f, 0, default(Color), 1.5f).noGravity = true;
		}
	}
}
