using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

[PierceResistException(false)]
public class AcidicSaxBubble : ModProjectile, ILocalizedModType, IModType
{
	public float counter;

	public float counter2;

	public int killCounter;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 7;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.penetrate = -1;
		base.Projectile.alpha = 255;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame > 6)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			if (counter >= 120f)
			{
				counter = 0f;
				Vector2 mistRandDirection = default(Vector2);
				((Vector2)(ref mistRandDirection))._002Ector((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
				((Vector2)(ref mistRandDirection)).Normalize();
				mistRandDirection *= (float)Main.rand.Next(50, 401) * 0.01f;
				int damage = (int)Main.player[base.Projectile.owner].GetTotalDamage<MagicDamageClass>().ApplyTo(32f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X, base.Projectile.Center.Y, mistRandDirection.X, mistRandDirection.Y, ModContent.ProjectileType<AcidicSaxMist>(), damage, 1f, base.Projectile.owner);
			}
			else
			{
				counter++;
			}
		}
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.ai[1]++;
			if (base.Projectile.ai[1] >= 6f)
			{
				if (base.Projectile.alpha > 0)
				{
					base.Projectile.alpha -= 20;
				}
				if (base.Projectile.alpha < 80)
				{
					base.Projectile.alpha = 80;
				}
			}
			if (base.Projectile.ai[1] >= 45f)
			{
				base.Projectile.ai[1] = 45f;
				if (counter2 < 1f)
				{
					counter2 += 0.002f;
					base.Projectile.scale += 0.002f;
					base.Projectile.width = (int)(30f * base.Projectile.scale);
					base.Projectile.height = (int)(30f * base.Projectile.scale);
				}
				else
				{
					base.Projectile.width = 60;
					base.Projectile.height = 60;
				}
				if (base.Projectile.wet)
				{
					if (base.Projectile.velocity.Y > 0f)
					{
						base.Projectile.velocity.Y = base.Projectile.velocity.Y * 0.98f;
					}
					if (base.Projectile.velocity.Y > -1f)
					{
						base.Projectile.velocity.Y = base.Projectile.velocity.Y - 0.2f;
					}
				}
				else if (base.Projectile.velocity.Y > -2f)
				{
					base.Projectile.velocity.Y = base.Projectile.velocity.Y - 0.05f;
				}
			}
			killCounter++;
			if (killCounter >= 200)
			{
				base.Projectile.Kill();
			}
		}
		base.Projectile.StickyProjAI(15);
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		base.Projectile.ModifyHitNPCSticky(3);
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.ai[0] != 1f)
		{
			return base.CanDamage();
		}
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
		{
			((Rectangle)(ref targetHitbox)).Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
		}
		return null;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture2D13 = TextureAssets.Projectile[base.Type].Value;
		int framing = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		Main.spriteBatch.Draw(texture2D13, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture2D13.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture2D13.Width / 2f, (float)framing / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
		if (base.Projectile.ai[2] == 1f)
		{
			target.AddBuff(20, 180);
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 180);
		if (base.Projectile.ai[2] == 1f)
		{
			target.AddBuff(20, 180);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.position = base.Projectile.Center;
		base.Projectile.width = (base.Projectile.height = 64);
		base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
		SoundEngine.PlaySound(in SoundID.Item54, base.Projectile.Center);
		for (int i = 0; i < 25; i++)
		{
			int toxicDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 75);
			Main.dust[toxicDust].position = (Main.dust[toxicDust].position + base.Projectile.position) / 2f;
			Main.dust[toxicDust].velocity = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
			((Vector2)(ref Main.dust[toxicDust].velocity)).Normalize();
			Dust obj = Main.dust[toxicDust];
			obj.velocity *= (float)Main.rand.Next(1, 30) * 0.1f;
			Main.dust[toxicDust].alpha = base.Projectile.alpha;
		}
		base.Projectile.maxPenetrate = -1;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.Damage();
	}
}
