using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class AcidGunStream : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 32;
		base.Projectile.height = 32;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = 3;
		base.Projectile.extraUpdates = 2;
		base.Projectile.tileCollide = false;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0140: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_020d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_022e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_024c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.localAI[1]++;
		if (base.Projectile.localAI[1] >= 4f)
		{
			base.Projectile.tileCollide = true;
		}
		base.Projectile.scale -= 0.002f;
		if (base.Projectile.scale <= 0f)
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.localAI[0] <= 3f)
		{
			base.Projectile.localAI[0]++;
			return;
		}
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
		if (base.Projectile.localAI[1] >= 10f)
		{
			base.Projectile.velocity.Y += 0.075f;
		}
	}

	public override void OnKill(int timeLeft)
	{
		if (base.Projectile.penetrate <= 1)
		{
			if (Main.projectile.IndexInRange((int)base.Projectile.ai[0]))
			{
				Projectile obj = Main.projectile[(int)base.Projectile.ai[0]];
				obj.ai[0] = -1f;
				obj.ai[1] = -1f;
				obj.Kill();
			}
			if (Main.projectile.IndexInRange((int)base.Projectile.ai[1]))
			{
				Projectile obj2 = Main.projectile[(int)base.Projectile.ai[1]];
				obj2.ai[0] = -1f;
				obj2.ai[1] = -1f;
				obj2.Kill();
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 120);
	}
}
