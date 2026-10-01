using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Rogue;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class LeonidStar : ModProjectile, ILocalizedModType, IModType
{
	private bool hasHit;

	private bool initialized;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.width = 22;
		base.Projectile.height = 24;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.MaxUpdates = 3;
	}

	public override void AI()
	{
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		if (!initialized)
		{
			base.Projectile.rotation += Main.rand.NextFloat();
			initialized = true;
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 120f)
		{
			base.Projectile.scale *= 0.98f;
			base.Projectile.ExpandHitboxBy(base.Projectile.scale);
			if (base.Projectile.scale <= 0.05f)
			{
				base.Projectile.Kill();
			}
		}
		base.Projectile.rotation += (float)base.Projectile.direction * 0.05f;
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = 20 + Main.rand.Next(40);
			if (Main.rand.NextBool(9))
			{
				SoundEngine.PlaySound(in SoundID.Item9, base.Projectile.position);
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 120);
		hasHit = true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 120);
		hasHit = true;
	}

	public override bool? CanDamage()
	{
		if (hasHit)
		{
			return false;
		}
		return null;
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0005: Unknown result type (might be due to invalid IL or missing references)
		//IL_000f: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.ColorSwap(LeonidProgenitor.blueColor, LeonidProgenitor.purpleColor, 2.5f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0104: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 5; i++)
		{
			int dustType = Utils.SelectRandom<int>(Main.rand, ModContent.DustType<AstralOrange>(), ModContent.DustType<AstralBlue>());
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, dustType, base.Projectile.velocity.X, base.Projectile.velocity.Y, 0, CalamityUtils.ColorSwap(LeonidProgenitor.blueColor, LeonidProgenitor.purpleColor, 1f), 1.5f);
			Main.dust[dust].noGravity = true;
		}
		if (!Main.dedServ)
		{
			Vector2 velocity = default(Vector2);
			for (int num480 = 0; num480 < 3; num480++)
			{
				((Vector2)(ref velocity))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
				velocity.SafeNormalize(default(Vector2));
				velocity *= (float)Main.rand.Next(1, 6) * 0.01f;
				Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.position, velocity, Main.rand.Next(16, 18));
			}
		}
	}
}
