using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class ShadowflameFireball : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 16;
		base.Projectile.height = 16;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = false;
		base.Projectile.light = 0.8f;
		base.Projectile.alpha = 255;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 360;
		base.Projectile.scale = 1.25f;
	}

	public override void AI()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		//IL_022f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0234: Unknown result type (might be due to invalid IL or missing references)
		//IL_029e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_032d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0337: Unknown result type (might be due to invalid IL or missing references)
		//IL_033c: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.localAI[0] = 1f;
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
		}
		for (int i = 0; i < 2; i++)
		{
			int dustType = 27;
			float dustScale = Main.rand.NextFloat(1.4f, 2.4f);
			int dustID = Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, dustType);
			Main.dust[dustID].noGravity = true;
			Main.dust[dustID].velocity = base.Projectile.velocity;
			Main.dust[dustID].scale = dustScale;
		}
		int dustType2 = 70;
		float velMult = Main.rand.NextFloat(0.05f, 0.6f);
		float dustScale2 = Main.rand.NextFloat(1.2f, 1.8f);
		int dustID2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType2);
		Main.dust[dustID2].noGravity = true;
		Dust obj = Main.dust[dustID2];
		obj.velocity *= 0.1f;
		Dust obj2 = Main.dust[dustID2];
		obj2.velocity += base.Projectile.velocity * velMult;
		Main.dust[dustID2].scale = dustScale2;
		base.Projectile.rotation += 0.3f * (float)base.Projectile.direction;
		if (!(base.Projectile.ai[1] > 0f))
		{
			return;
		}
		int playerTracker = Player.FindClosest(base.Projectile.Center, 1, 1);
		Vector2 playerDirection = Main.player[playerTracker].Center - base.Projectile.Center;
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 60f)
		{
			if (base.Projectile.ai[0] < 240f)
			{
				float inertia = 25f;
				float scaleFactor = ((Vector2)(ref base.Projectile.velocity)).Length();
				((Vector2)(ref playerDirection)).Normalize();
				playerDirection *= scaleFactor;
				base.Projectile.velocity = (base.Projectile.velocity * (inertia - 1f) + playerDirection) / inertia;
				((Vector2)(ref base.Projectile.velocity)).Normalize();
				Projectile projectile = base.Projectile;
				projectile.velocity *= scaleFactor;
			}
			else if (((Vector2)(ref base.Projectile.velocity)).Length() < 18f)
			{
				base.Projectile.tileCollide = true;
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 1.02f;
			}
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			target.AddBuff(ModContent.BuffType<Shadowflame>(), 120);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0086: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item10, base.Projectile.Center);
		int killDust = 20;
		for (int i = 0; i < killDust; i++)
		{
			int dustType = (Main.rand.NextBool() ? 70 : 27);
			float dustScale = Main.rand.NextFloat(1f, 1.6f);
			int dustID = Dust.NewDust(base.Projectile.Center, 1, 1, dustType);
			Dust obj = Main.dust[dustID];
			obj.velocity *= 4f;
			Main.dust[dustID].scale = dustScale;
		}
	}
}
