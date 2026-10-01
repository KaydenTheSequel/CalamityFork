using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class StickyFeatherAero : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/Magic/StickyFeather";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft = 360;
		base.Projectile.penetrate = 3;
		base.Projectile.alpha = 255;
		base.Projectile.aiStyle = 93;
		base.AIType = 514;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		if (base.Projectile.timeLeft < 320)
		{
			base.Projectile.tileCollide = true;
		}
		CalamityUtils.HomeInOnNPC(base.Projectile, ignoreTiles: true, 150f, 12f, 20f);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0228: Unknown result type (might be due to invalid IL or missing references)
		//IL_0256: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0272: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
		base.Projectile.position.X = base.Projectile.position.X + (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y + (float)(base.Projectile.height / 2);
		base.Projectile.width = 50;
		base.Projectile.height = 50;
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		for (int i = 0; i < 15; i++)
		{
			int blueDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 206, 0f, 0f, 100, default(Color), 1.2f);
			Dust obj = Main.dust[blueDust];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[blueDust].scale = 0.5f;
				Main.dust[blueDust].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 30; j++)
		{
			int blueDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 206, 0f, 0f, 100, default(Color), 1.7f);
			Main.dust[blueDust2].noGravity = true;
			Dust obj2 = Main.dust[blueDust2];
			obj2.velocity *= 5f;
			blueDust2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 206, 0f, 0f, 100);
			Dust obj3 = Main.dust[blueDust2];
			obj3.velocity *= 2f;
		}
	}
}
