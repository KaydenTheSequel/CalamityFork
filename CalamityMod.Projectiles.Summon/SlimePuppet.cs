using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class SlimePuppet : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 1;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 60;
		base.Projectile.height = 60;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.timeLeft = 300;
		base.Projectile.penetrate = 1;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0117: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_027e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.MinionAntiClump();
		NPC potentialTarget = base.Projectile.Center.MinionHoming(800f, Owner);
		if (potentialTarget == null)
		{
			float destinationAngularOffset = (float)Math.Sin((float)base.Projectile.identity / 6f % 6f * (float)Math.PI) * (float)Math.PI / 10f;
			Vector2 destination = Owner.Center - Vector2.UnitY.RotatedBy(destinationAngularOffset) * 160f;
			if (base.Projectile.DistanceSQ(destination) > 324f)
			{
				base.Projectile.velocity = (base.Projectile.velocity * 20f + base.Projectile.SafeDirectionTo(destination) * 9f) / 21f;
			}
		}
		else
		{
			float nudgedVelocityDirection = base.Projectile.velocity.ToRotation().AngleTowards(base.Projectile.AngleTo(potentialTarget.Center), 0.078f);
			base.Projectile.velocity = nudgedVelocityDirection.ToRotationVector2() * MathHelper.Lerp(((Vector2)(ref base.Projectile.velocity)).Length(), 16f, 0.25f);
			if (base.Projectile.WithinRange(potentialTarget.Center, 240f))
			{
				base.Projectile.velocity = Vector2.Lerp(base.Projectile.velocity, base.Projectile.SafeDirectionTo(potentialTarget.Center) * 16f, 0.1f);
				base.Projectile.Center = base.Projectile.Center.MoveTowards(potentialTarget.Center, 6f);
			}
		}
		if (base.Projectile.ai[0] > 0f)
		{
			base.Projectile.rotation += MathHelper.ToRadians(((Vector2)(ref base.Projectile.velocity)).Length() / 4f) * (float)Math.Sign(base.Projectile.velocity.X);
			base.Projectile.ai[0]--;
		}
		else if (potentialTarget != null)
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		}
		if ((double)Vector2.Dot(base.Projectile.oldVelocity.SafeNormalize(Vector2.Zero), base.Projectile.velocity.SafeNormalize(Vector2.Zero)) < 0.87)
		{
			base.Projectile.ai[0] = 50f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(137, 300);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(100);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		SoundEngine.PlaySound(in SoundID.NPCDeath1, base.Projectile.Center);
		if (!Main.dedServ)
		{
			for (int i = 0; i < 20; i++)
			{
				Vector2 spawnOffset = Main.rand.NextVector2Unit() * Main.rand.NextFloat(4f, 36f);
				Dust dust = Dust.NewDustPerfect(base.Projectile.Center + spawnOffset, 243);
				dust.velocity = spawnOffset.RotatedBy((float)Math.PI / 2f * (float)Main.rand.NextBool().ToDirectionInt()) * 0.16f;
				dust.scale = 1.2f;
			}
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		int dye = Owner?.cMinion ?? 0;
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 1, null, drawCentered: true, shrink: false, dye);
		return false;
	}
}
