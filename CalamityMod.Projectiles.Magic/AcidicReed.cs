using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class AcidicReed : ModProjectile, ILocalizedModType, IModType
{
	public static readonly SoundStyle SaxSound = new SoundStyle("CalamityMod/Sounds/Item/Saxophone/Sax", 6);

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 600;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = false;
		base.Projectile.penetrate = 1;
	}

	public override void AI()
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[0] == 1f)
		{
			SoundEngine.PlaySound(in SaxSound, base.Projectile.Center);
			base.Projectile.ai[0] = 0f;
		}
		if (base.Projectile.velocity.Y < 10f)
		{
			base.Projectile.velocity.Y += 0.25f;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i <= 2; i++)
		{
			int idx = Dust.NewDust(base.Projectile.position, 8, 8, 75, 0f, 0f, 0, default(Color), 0.75f);
			Main.dust[idx].noGravity = true;
			Dust obj = Main.dust[idx];
			obj.velocity *= 3f;
			idx = Dust.NewDust(base.Projectile.position, 8, 8, 75, 0f, 0f, 0, default(Color), 0.75f);
			Main.dust[idx].noGravity = true;
			Dust obj2 = Main.dust[idx];
			obj2.velocity *= 3f;
		}
	}
}
