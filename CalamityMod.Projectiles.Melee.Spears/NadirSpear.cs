using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Melee;
using CalamityMod.Projectiles.BaseProjectiles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee.Spears;

public class NadirSpear : BaseSpearProjectile
{
	public override LocalizedText DisplayName => CalamityUtils.GetItemName<Nadir>();

	public override float InitialSpeed => 5.5f;

	public override float ReelbackSpeed => 2.1f;

	public override float ForwardSpeed => 1f;

	public override Action<Projectile> EffectBeforeReelback => delegate
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		int damage = (int)((float)base.Projectile.damage * 0.5f);
		float knockBack = base.Projectile.knockBack * 0.5f;
		Vector2 position = base.Projectile.Center + base.Projectile.velocity;
		Vector2 velocity = base.Projectile.velocity * 0.75f;
		if (base.Projectile.owner == Main.myPlayer)
		{
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), position, velocity, ModContent.ProjectileType<VoidEssence>(), damage, knockBack, base.Projectile.owner);
		}
		SoundEngine.PlaySound(in SoundID.Item104, base.Projectile.Center);
		int num = 18;
		Vector2 spinningpoint = default(Vector2);
		((Vector2)(ref spinningpoint))._002Ector(3.8f, 0f);
		for (int i = 0; i < num; i++)
		{
			int type = 27;
			float num2 = (float)i * ((float)Math.PI * 2f / (float)num);
			Vector2 velocity2 = spinningpoint.RotatedBy(num2);
			int num3 = Dust.NewDust(base.Projectile.Center, 1, 1, type);
			Main.dust[num3].noGravity = true;
			Main.dust[num3].position = base.Projectile.Center;
			Main.dust[num3].velocity = velocity2;
			Main.dust[num3].scale = 2.4f;
		}
	};

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 56);
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.timeLeft = 90;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 4;
	}

	public override void ExtraBehavior()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		int movingDust = 3;
		for (int i = 0; i < movingDust; i++)
		{
			int dustID = (Main.rand.NextBool(4) ? 27 : 118);
			int idx = Dust.NewDust(0.5f * base.Projectile.position + 0.5f * base.Projectile.Center, base.Projectile.width / 2, base.Projectile.height / 2, dustID);
			Main.dust[idx].noGravity = true;
			Main.dust[idx].velocity = Vector2.Zero;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Nightwither>(), 180);
	}
}
