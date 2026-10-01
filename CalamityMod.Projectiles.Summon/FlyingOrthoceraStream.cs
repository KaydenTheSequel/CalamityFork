using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class FlyingOrthoceraStream : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.SentryShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 3;
		base.Projectile.MaxUpdates = 3;
		base.Projectile.tileCollide = false;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 8;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0168: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0220: Unknown result type (might be due to invalid IL or missing references)
		//IL_0226: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0247: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0265: Unknown result type (might be due to invalid IL or missing references)
		//IL_026f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0274: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] >= 4f)
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.scale -= 0.002f;
		if (base.Projectile.scale <= 0f)
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.ai[0] <= 3f)
		{
			base.Projectile.ai[0]++;
			return;
		}
		base.Projectile.velocity.Y += 0.03f;
		for (int i = 0; i < 3; i++)
		{
			Vector2 positionDelta = base.Projectile.velocity / 3f * (float)i;
			int spawnDelta = 14;
			int dustIdx = Dust.NewDust(new Vector2(base.Projectile.position.X + (float)spawnDelta, base.Projectile.position.Y + (float)spawnDelta), base.Projectile.width - spawnDelta * 2, base.Projectile.height - spawnDelta * 2, 75, 0f, 0f, 100);
			Dust obj = Main.dust[dustIdx];
			obj.noGravity = true;
			obj.velocity *= 0.1f;
			obj.velocity += base.Projectile.velocity * 0.5f;
			obj.position -= positionDelta;
		}
		if (Main.rand.NextBool(8))
		{
			int spawnDelta2 = 16;
			int dustIdx2 = Dust.NewDust(new Vector2(base.Projectile.position.X + (float)spawnDelta2, base.Projectile.position.Y + (float)spawnDelta2), base.Projectile.width - spawnDelta2 * 2, base.Projectile.height - spawnDelta2 * 2, 75, 0f, 0f, 100, default(Color), 0.5f);
			Dust obj2 = Main.dust[dustIdx2];
			obj2.velocity *= 0.25f;
			Dust obj3 = Main.dust[dustIdx2];
			obj3.velocity += base.Projectile.velocity * 0.5f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(70, 180);
		target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
	}
}
