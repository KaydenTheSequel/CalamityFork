using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class MetalChunk : ModProjectile, ILocalizedModType, IModType
{
	public bool StuckInEnemy;

	public int StealthShardTimer;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/MetalMonstrosity";

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ignoreWater = true;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 60;
	}

	public override void AI()
	{
		if (!StuckInEnemy)
		{
			base.Projectile.velocity.Y += 0.11f;
			if (base.Projectile.velocity.Y > 16f)
			{
				base.Projectile.velocity.Y = 16f;
			}
			base.Projectile.rotation += 0.14f * (float)base.Projectile.direction;
		}
		if (base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.StickyProjAI(10);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		if (base.Projectile.Calamity().stealthStrike)
		{
			StuckInEnemy = true;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		if (base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.ModifyHitNPCSticky(1);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_020f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCHit42, base.Projectile.Center);
		for (int i = 0; i < 3; i++)
		{
			Vector2 sVelocity = -Vector2.UnitY.RotatedByRandom(0.7853981852531433) * 4.5f;
			Projectile projectile = Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.Center, sVelocity, 24, (int)((double)base.Projectile.damage * 0.3), 0f, base.Projectile.owner);
			projectile.DamageType = RogueDamageClass.Instance;
			projectile.timeLeft = 600;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 20;
			sVelocity = -Vector2.UnitY.RotatedByRandom(0.7853981852531433) * 3f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, sVelocity, ModContent.ProjectileType<MetalShard>(), (int)((double)base.Projectile.damage * 0.3), 0f, base.Projectile.owner);
		}
		if (base.Projectile.Calamity().stealthStrike)
		{
			for (int j = 0; j < 4; j++)
			{
				Vector2 shardVel = (Main.npc[(int)base.Projectile.ai[1]].Center - base.Projectile.Center).SafeNormalize(Vector2.UnitX).RotatedByRandom(0.5235987901687622);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, shardVel, ModContent.ProjectileType<MetalShard>(), (int)((float)base.Projectile.damage * 0.15f), 0f, base.Projectile.owner);
			}
		}
		for (int k = 0; k < 15; k++)
		{
			Dust.NewDust(base.Projectile.Center, 1, 1, 82, 0f, 0f, 0, default(Color), 1.1f);
		}
	}
}
