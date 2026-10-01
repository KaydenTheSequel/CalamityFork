using System;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SulphuricNukesplosion : ModProjectile, ILocalizedModType, IModType
{
	public int frameX;

	public int frameY;

	private bool stealthyNuke;

	private int boomerTime = -1;

	private int dustloop = 30;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public int currentFrame => frameY + frameX * 14;

	public override void SetDefaults()
	{
		base.Projectile.width = 140;
		base.Projectile.height = 290;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 300;
		base.Projectile.tileCollide = false;
		base.Projectile.alpha = 255;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 15;
	}

	public override bool PreAI()
	{
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bb: Unknown result type (might be due to invalid IL or missing references)
		if (stealthyNuke && boomerTime > -1)
		{
			if (boomerTime == 0)
			{
				base.Projectile.timeLeft = ((boomerTime == 0) ? 99 : base.Projectile.timeLeft);
				base.Projectile.position = base.Projectile.Center;
				base.Projectile.position.X = base.Projectile.position.X - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = base.Projectile.position.Y - (float)(base.Projectile.height / 2);
				base.Projectile.idStaticNPCHitCooldown = 9;
				boomerTime = 1;
			}
			if (base.Projectile.timeLeft <= 15)
			{
				dustloop--;
			}
			base.Projectile.Damage();
		}
		if (stealthyNuke && frameX >= 1)
		{
			if (boomerTime == -1)
			{
				dustloop += Main.rand.Next(1, 3);
			}
			for (int i = 0; i < dustloop; i++)
			{
				int dustType = 75;
				float scale = Main.rand.NextFloat(0.5f, 1.5f);
				float randX = Main.rand.NextFloat(-30f, 30f);
				float randY = Main.rand.NextFloat(-30f, 30f);
				float num = Main.rand.NextFloat(5f, 24f);
				float speed = (float)Math.Sqrt(randX * randX + randY * randY);
				speed = num / speed;
				randX *= speed;
				randY *= speed;
				int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType);
				Main.dust[idx].position.X = base.Projectile.Center.X + Main.rand.NextFloat(-10f, 10f);
				Main.dust[idx].position.Y = base.Projectile.Center.Y + Main.rand.NextFloat(-10f, 10f);
				Main.dust[idx].velocity.X = randX;
				Main.dust[idx].velocity.Y = randY;
				Main.dust[idx].scale = scale;
				Main.dust[idx].noGravity = true;
				Main.dust[idx].color = new Color(49, 180, 142);
			}
		}
		return true;
	}

	public override void AI()
	{
		if (boomerTime != -1)
		{
			return;
		}
		if (base.Projectile.Calamity().stealthStrike)
		{
			stealthyNuke = true;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter % 6 == 0)
		{
			frameY++;
			if (frameY >= 7)
			{
				frameX++;
				frameY = 0;
			}
			if (frameX >= 2)
			{
				if (!stealthyNuke)
				{
					base.Projectile.Kill();
				}
				else
				{
					boomerTime = 0;
					base.Projectile.hide = true;
				}
			}
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.position.Y -= base.Projectile.height / 2;
			base.Projectile.localAI[0] = 1f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 10 * (stealthyNuke ? 60 : 30));
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Irradiated>(), 10 * (stealthyNuke ? 60 : 30));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(frameX * base.Projectile.width, frameY * base.Projectile.height, base.Projectile.width, base.Projectile.height);
		Main.EntitySpriteDraw(ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/SulphuricNukesplosion", (AssetRequestMode)2).Value, base.Projectile.Center - Main.screenPosition, frame, Color.White, base.Projectile.rotation, base.Projectile.Size / 2f, 1f, (SpriteEffects)0);
		return false;
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		if (boomerTime == -1)
		{
			((Rectangle)(ref projHitbox)).Intersects(targetHitbox);
		}
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, 200f, targetHitbox);
	}
}
