using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class PlaguenadeProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/Plaguenade";

	public override void SetDefaults()
	{
		base.Projectile.width = 20;
		base.Projectile.height = 20;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.extraUpdates = 1;
		base.Projectile.timeLeft = 180;
		base.Projectile.DamageType = RogueDamageClass.Instance;
	}

	public override void AI()
	{
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 5f)
		{
			base.Projectile.ai[0] = 5f;
			if (base.Projectile.velocity.Y == 0f && base.Projectile.velocity.X != 0f)
			{
				base.Projectile.velocity.X *= 0.97f;
				if (base.Projectile.velocity.X > -0.01f && base.Projectile.velocity.X < 0.01f)
				{
					base.Projectile.velocity.X = 0f;
					base.Projectile.netUpdate = true;
				}
			}
			base.Projectile.velocity.Y += 0.2f;
		}
		base.Projectile.rotation += base.Projectile.velocity.X * 0.1f;
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.velocity.X != oldVelocity.X)
		{
			base.Projectile.velocity.X = oldVelocity.X * -0.5f;
		}
		if (base.Projectile.velocity.Y != oldVelocity.Y && oldVelocity.Y > 1f)
		{
			base.Projectile.velocity.Y = oldVelocity.Y * -0.5f;
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0244: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_026b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0270: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0307: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0336: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0346: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_038d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_043c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0440: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_046c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04be: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_04df: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0530: Unknown result type (might be due to invalid IL or missing references)
		//IL_0536: Unknown result type (might be due to invalid IL or missing references)
		//IL_0550: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_055c: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d4: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		Player player = Main.player[base.Projectile.owner];
		int projAmt = (base.Projectile.Calamity().stealthStrike ? 28 : 20);
		if (player.strongBees)
		{
			projAmt += (base.Projectile.Calamity().stealthStrike ? 7 : 5);
		}
		for (int projIndex = 0; projIndex < projAmt; projIndex++)
		{
			float speedX = (float)Main.rand.Next(-35, 36) * 0.02f;
			float speedY = (float)Main.rand.Next(-35, 36) * 0.02f;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.position.X, base.Projectile.position.Y, speedX, speedY, ModContent.ProjectileType<PlaguenadeBee>(), player.beeDamage(base.Projectile.damage), player.beeKB(0f), base.Projectile.owner);
		}
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.position);
		if (base.Projectile.Calamity().stealthStrike)
		{
			base.Projectile.position = base.Projectile.Center;
			base.Projectile.width = (base.Projectile.height = 75);
		}
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
		for (int i = 0; i < 10; i++)
		{
			int smoke = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[smoke];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[smoke].scale = 0.5f;
				Main.dust[smoke].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 15; j++)
		{
			int plague = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 89, 0f, 0f, 100, default(Color), 3f);
			Main.dust[plague].noGravity = true;
			Dust obj2 = Main.dust[plague];
			obj2.velocity *= 5f;
			int fire = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 6, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[fire];
			obj3.velocity *= 2f;
		}
		if (Main.dedServ)
		{
			return;
		}
		Vector2 goreSource = base.Projectile.Center;
		int goreAmt = 3;
		Vector2 source = default(Vector2);
		((Vector2)(ref source))._002Ector(goreSource.X - 24f, goreSource.Y - 24f);
		for (int goreIndex = 0; goreIndex < goreAmt; goreIndex++)
		{
			float velocityMult = 0.33f;
			if (goreIndex < goreAmt / 3)
			{
				velocityMult = 0.66f;
			}
			if (goreIndex >= 2 * goreAmt / 3)
			{
				velocityMult = 1f;
			}
			ModContent.GetInstance<CalamityMod>();
			int type = Main.rand.Next(61, 64);
			int smoke2 = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
			Gore obj4 = Main.gore[smoke2];
			obj4.velocity *= velocityMult;
			obj4.velocity.X++;
			obj4.velocity.Y++;
			type = Main.rand.Next(61, 64);
			smoke2 = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
			Gore obj5 = Main.gore[smoke2];
			obj5.velocity *= velocityMult;
			obj5.velocity.X--;
			obj5.velocity.Y++;
			type = Main.rand.Next(61, 64);
			smoke2 = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
			Gore obj6 = Main.gore[smoke2];
			obj6.velocity *= velocityMult;
			obj6.velocity.X++;
			obj6.velocity.Y--;
			type = Main.rand.Next(61, 64);
			smoke2 = Gore.NewGore(base.Projectile.GetSource_Death(), source, default(Vector2), type);
			Gore obj7 = Main.gore[smoke2];
			obj7.velocity *= velocityMult;
			obj7.velocity.X--;
			obj7.velocity.Y--;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 240);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 240);
	}
}
