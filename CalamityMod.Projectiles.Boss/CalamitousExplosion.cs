using System.IO;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Graphics.Metaballs;
using CalamityMod.NPCs.CalClone;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class CalamitousExplosion : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 130;
		base.Projectile.height = 130;
		base.Projectile.hostile = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 180;
		base.CooldownSlot = 1;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
	}

	public override void AI()
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0207: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		Lighting.AddLight(base.Projectile.Center, 0.75f, 0f, 0f);
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundEngine.PlaySound(in CalamitasClone.CalamitousExplosionSound, base.Projectile.Center);
			base.Projectile.localAI[0]++;
		}
		bool xflag = false;
		bool yflag = false;
		if (base.Projectile.velocity.X < 0f && base.Projectile.position.X < base.Projectile.ai[0])
		{
			xflag = true;
		}
		if (base.Projectile.velocity.X > 0f && base.Projectile.position.X > base.Projectile.ai[0])
		{
			xflag = true;
		}
		if (base.Projectile.velocity.Y < 0f && base.Projectile.position.Y < base.Projectile.ai[1])
		{
			yflag = true;
		}
		if (base.Projectile.velocity.Y > 0f && base.Projectile.position.Y > base.Projectile.ai[1])
		{
			yflag = true;
		}
		if (xflag & yflag)
		{
			base.Projectile.Kill();
		}
		float projTimer = 25f;
		if (base.Projectile.ai[0] > 180f)
		{
			projTimer -= (base.Projectile.ai[0] - 180f) / 2f;
		}
		if (projTimer <= 0f)
		{
			projTimer = 0f;
			base.Projectile.Kill();
		}
		projTimer *= 0.7f;
		base.Projectile.ai[0] += 4f;
		CalamitasMetaball.SpawnParticle(base.Projectile.Center, Main.rand.NextVector2CircularEdge(4f, 4f), 32f).SizeScaling = 0.96f;
		CalamitasMetaball.SpawnParticle(base.Projectile.Center, Main.rand.NextVector2CircularEdge(3f, 3f), 40f).SizeScaling = 0.96f;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<BrimstoneFlames>(), 150);
		}
	}
}
