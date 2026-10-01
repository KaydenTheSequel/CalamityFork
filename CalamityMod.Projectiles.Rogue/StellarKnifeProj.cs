using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class StellarKnifeProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/StellarKnife";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 34;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 600;
		base.Projectile.tileCollide = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[1]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[1] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0210: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_022d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0232: Unknown result type (might be due to invalid IL or missing references)
		//IL_0239: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[1]++;
		if (base.Projectile.localAI[1] > 600f)
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.ai[0] == 1f)
		{
			base.Projectile.ai[0] = 0f;
			base.Projectile.damage = (int)((float)base.Projectile.damage * ((base.Projectile.ai[1] == 1f) ? 0.9f : 0.75f));
			base.Projectile.ai[1] = 0f;
		}
		base.Projectile.ai[1] += 0.75f;
		if (base.Projectile.ai[1] <= 60f)
		{
			base.Projectile.rotation--;
			base.Projectile.velocity.X *= 0.985f;
			base.Projectile.velocity.Y *= 0.985f;
			return;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(45f);
		Vector2 center = base.Projectile.Center;
		float maxDistance = 600f;
		bool homeIn = false;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (n.CanBeChasedBy(base.Projectile) && Collision.CanHit(base.Projectile.Center, 1, 1, n.Center, 1, 1))
			{
				float extraDistance = (float)(n.width / 2) + (float)(n.height / 2);
				if (Vector2.Distance(n.Center, base.Projectile.Center) < maxDistance + extraDistance)
				{
					center = n.Center;
					homeIn = true;
					break;
				}
			}
		}
		if (homeIn)
		{
			base.Projectile.timeLeft = 600;
			Vector2 moveDirection = base.Projectile.SafeDirectionTo(center, Vector2.UnitY);
			base.Projectile.velocity = (base.Projectile.velocity * 10f + moveDirection * 30f) / 11f;
			return;
		}
		if (base.Projectile.timeLeft > 60)
		{
			base.Projectile.timeLeft = 60;
		}
		base.Projectile.velocity.X *= 0.92f;
		base.Projectile.velocity.Y *= 0.92f;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 120);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 2);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0015: Unknown result type (might be due to invalid IL or missing references)
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		for (int k = 0; k < 5; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, ModContent.DustType<AstralBlue>(), base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}
}
