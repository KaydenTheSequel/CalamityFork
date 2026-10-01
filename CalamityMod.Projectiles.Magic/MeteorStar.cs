using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class MeteorStar : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Magic";

	public Player Owner => Main.player[base.Projectile.owner];

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		Main.projFrames[base.Type] = 3;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 42;
		base.Projectile.height = 34;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 361;
	}

	public override void AI()
	{
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_017a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0403: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0362: Unknown result type (might be due to invalid IL or missing references)
		//IL_0373: Unknown result type (might be due to invalid IL or missing references)
		//IL_0378: Unknown result type (might be due to invalid IL or missing references)
		//IL_03da: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c2: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		base.Projectile.frame = base.Projectile.frameCounter / 6 % Main.projFrames[base.Type];
		Color LightYellow = default(Color);
		((Color)(ref LightYellow))._002Ector(255, 255, 76);
		Lighting.AddLight(base.Projectile.Center, ((Color)(ref LightYellow)).ToVector3() * base.Projectile.Opacity * 0.5f);
		bool explodingSoon = base.Projectile.timeLeft <= 120;
		if (base.Projectile.soundDelay <= 0)
		{
			base.Projectile.soundDelay = 30 + Main.rand.Next(explodingSoon ? 10 : 40);
			if (Main.rand.NextBool(4) | explodingSoon)
			{
				SoundEngine.PlaySound(in SoundID.Item9, base.Projectile.Center);
			}
		}
		if ((Main.rand.NextBool(12) || (explodingSoon && Main.rand.NextBool(3))) && !Main.dedServ)
		{
			Gore gore = Gore.NewGoreDirect(base.Projectile.GetSource_FromAI(), base.Projectile.Center, base.Projectile.velocity * 0.2f, Main.rand.Next(16, 18));
			gore.velocity *= 0.66f;
			gore.velocity += base.Projectile.velocity * 0.3f;
		}
		if (explodingSoon)
		{
			for (int i = 0; i < 3; i++)
			{
				Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31, 0f, 0f, 100, default(Color), 2f);
				dust.velocity *= 0.3f;
				dust.position.X = base.Projectile.Center.X + 4f + Main.rand.NextFloat(-6f, 6f);
				dust.position.Y = base.Projectile.Center.Y + Main.rand.NextFloat(-6f, 6f);
				dust.noGravity = true;
			}
		}
		if (Main.myPlayer == base.Projectile.owner)
		{
			if (base.Projectile.ai[2] != 1f)
			{
				if (Owner.gravDir == -1f)
				{
					base.Projectile.Center = Owner.Top;
				}
				else
				{
					base.Projectile.Center = Owner.Bottom;
				}
				base.Projectile.ai[2] = 1f;
				base.Projectile.ForceNetUpdate();
			}
			if (Owner.channel)
			{
				Owner.mount?.Dismount(Owner);
				Owner.RemoveAllGrapplingHooks();
				base.Projectile.velocity = Owner.SafeDirectionTo(Owner.Calamity().mouseWorld) * 14f;
				Owner.velocity = base.Projectile.velocity;
				Owner.ChangeDir((Math.Sign(base.Projectile.velocity.X) > 0) ? 1 : (-1));
				if (Owner.gravDir == -1f)
				{
					Owner.Top = base.Projectile.Center;
				}
				else
				{
					Owner.Bottom = base.Projectile.Center;
				}
			}
			else
			{
				Explode(reducedDmg: true);
			}
		}
		if (Collision.SolidCollision(Owner.position + base.Projectile.velocity, Owner.width, Owner.height) && base.Projectile.velocity != Vector2.Zero)
		{
			Owner.velocity.Y = 0f;
			Explode();
		}
		if (base.Projectile.timeLeft <= 1)
		{
			Explode();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Explode();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		Explode();
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(value, base.Projectile.Center - Main.screenPosition, frame, Color.White, base.Projectile.rotation, frame.Size() * 0.5f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	private void Explode(bool reducedDmg = false)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(64);
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		Vector2 spawnPos = base.Projectile.Center;
		spawnPos.Y -= 70f;
		if (reducedDmg)
		{
			base.Projectile.damage /= 6;
		}
		Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), spawnPos, Vector2.Zero, ModContent.ProjectileType<MeteorStarExplosion>(), base.Projectile.damage * 3, base.Projectile.knockBack * 3f, base.Projectile.owner, reducedDmg.ToInt());
		for (int i = 0; i < 10; i++)
		{
			Dust smoke = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 31, 0f, 0f, 100, default(Color), 1.2f);
			smoke.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				smoke.scale = 0.5f;
				smoke.fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		if (!Main.dedServ)
		{
			for (int j = 0; j < 5; j++)
			{
				Gore.NewGore(base.Projectile.GetSource_Death(), base.Projectile.position, base.Projectile.velocity * 0.05f, Main.rand.Next(16, 18));
			}
		}
		base.Projectile.Kill();
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		Player owner = Owner;
		owner.velocity *= 0.8f;
		Owner.fullRotation = 0f;
	}
}
