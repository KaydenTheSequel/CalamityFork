using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class BrimbladeProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Brimblade";

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 26;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.aiStyle = 3;
		base.Projectile.timeLeft = 180;
		base.AIType = 52;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
		int numProj = (base.Projectile.Calamity().stealthStrike ? 3 : 2);
		float rotation = MathHelper.ToRadians(20f);
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int i = 0; i < numProj + 1; i++)
			{
				Vector2 perturbedSpeed = Utils.RotatedBy(new Vector2(base.Projectile.velocity.X, base.Projectile.velocity.Y), (double)MathHelper.Lerp(0f - rotation, rotation, (float)(i / (numProj - 1))), default(Vector2));
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, perturbedSpeed.X * 0.25f, perturbedSpeed.Y * 0.25f, ModContent.ProjectileType<Brimblade2>(), (int)((double)base.Projectile.damage * 0.6), base.Projectile.knockBack * 0.5f, base.Projectile.owner);
			}
		}
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 235, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 180);
		int numProj = (base.Projectile.Calamity().stealthStrike ? 3 : 2);
		float rotation = MathHelper.ToRadians(20f);
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int i = 0; i < numProj + 1; i++)
			{
				Vector2 perturbedSpeed = Utils.RotatedBy(new Vector2(base.Projectile.velocity.X, base.Projectile.velocity.Y), (double)MathHelper.Lerp(0f - rotation, rotation, (float)(i / (numProj - 1))), default(Vector2));
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, perturbedSpeed.X * 0.25f, perturbedSpeed.Y * 0.25f, ModContent.ProjectileType<Brimblade2>(), (int)((double)base.Projectile.damage * 0.6), base.Projectile.knockBack * 0.5f, base.Projectile.owner);
			}
		}
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 235, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}
}
