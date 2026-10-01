using System;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class MutatedTruffleToothball : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/NPCs/OldDuke/OldDukeToothBall";

	public ref float TargetShotID => ref base.Projectile.ai[0];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.timeLeft = 120;
		base.Projectile.width = (base.Projectile.height = 40);
		base.Projectile.penetrate = -1;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.netImportant = true;
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= Utils.Remap(base.Projectile.timeLeft, 120f, 0f, 1f, 0.9f);
		base.Projectile.rotation += ((Vector2)(ref base.Projectile.velocity)).Length() * 0.02f;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		modifiers.SourceDamage *= 1.25f;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0174: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 3; i++)
		{
			if (Main.myPlayer == base.Projectile.owner)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity.RotatedByRandom(6.2831854820251465).SafeNormalize(Vector2.Zero) * (MutatedTruffle.ToothballSpikeSpeed - 15f), ModContent.ProjectileType<MutatedTruffleToothballSpike>(), base.Projectile.damage / 3, base.Projectile.knockBack, base.Projectile.owner, TargetShotID);
			}
		}
		if (!Main.dedServ)
		{
			int dustAmount = 15;
			for (int dustIndex = 0; dustIndex < 15; dustIndex++)
			{
				Vector2 velocity = ((float)Math.PI * 2f / (float)dustAmount * (float)dustIndex).ToRotationVector2() * Main.rand.NextFloat(1f, 3f);
				Dust.NewDustPerfect(base.Projectile.Center, 7, velocity);
				Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 7).noGravity = true;
			}
			SoundEngine.PlaySound(in SoundID.NPCDeath1, base.Projectile.Center);
		}
		base.Projectile.ForceNetUpdate();
	}
}
