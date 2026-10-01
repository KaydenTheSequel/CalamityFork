using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class ShockBolt : ModProjectile, ILocalizedModType, IModType
{
	public static int frameWidth = 12;

	public static int frameHeight = 26;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 3;
		base.Projectile.timeLeft = 120;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_015a: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_016d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b4: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (base.Projectile.timeLeft < 55)
		{
			base.Projectile.tileCollide = true;
		}
		if (base.Projectile.ai[1] != 1f)
		{
			return;
		}
		float minDist = 999f;
		int index = 0;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC npc = enumerator.Current;
			if (npc.CanBeChasedBy(base.Projectile))
			{
				Vector2 val = base.Projectile.Center - npc.Center;
				float dist = ((Vector2)(ref val)).Length();
				if (dist < minDist)
				{
					minDist = dist;
					index = npc.whoAmI;
				}
			}
		}
		if (minDist < 999f)
		{
			Vector2 velocityNew = Main.npc[index].Center - base.Projectile.Center;
			((Vector2)(ref velocityNew)).Normalize();
			velocityNew *= 2f;
			Projectile projectile = base.Projectile;
			projectile.velocity += velocityNew;
			if (((Vector2)(ref base.Projectile.velocity)).Length() > 10f)
			{
				((Vector2)(ref base.Projectile.velocity)).Normalize();
				Projectile projectile2 = base.Projectile;
				projectile2.velocity *= 10f;
			}
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(144, 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(144, 120);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D sprite = ((base.Projectile.ai[0] != 0f) ? ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/ShockBolt2", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/ShockBolt", (AssetRequestMode)2).Value);
		Color drawColour = Color.White;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)(frameWidth / 2), (float)(frameHeight / 2));
		Main.EntitySpriteDraw(sprite, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, frameHeight * base.Projectile.frame, frameWidth, frameHeight), drawColour, base.Projectile.rotation, origin, 1f, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = SoundID.Item93 with
		{
			Volume = SoundID.Item93.Volume * 0.25f
		};
		SoundEngine.PlaySound(in style, base.Projectile.position);
		for (int i = 0; i < 5; i++)
		{
			int dustType = 132;
			int dust = Dust.NewDust(base.Projectile.Center, 1, 1, dustType, base.Projectile.velocity.X, base.Projectile.velocity.Y, 0, default(Color), 0.5f);
			Main.dust[dust].noGravity = true;
		}
	}
}
