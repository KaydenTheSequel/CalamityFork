using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Projectiles.Rogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class IceBombFriendly : ModProjectile, ILocalizedModType, IModType
{
	public const float FormTime = 60f;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Projectiles/Boss/IceBomb";

	public ref float Timer => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = 30;
		base.Projectile.height = 30;
		base.Projectile.scale = 0.5f;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Melee;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 120;
		base.Projectile.tileCollide = false;
	}

	public override bool? CanDamage()
	{
		return false;
	}

	public override void AI()
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01de: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0204: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		NPC npc = Main.npc[(int)base.Projectile.ai[1]];
		if (!npc.active)
		{
			base.Projectile.Kill();
		}
		else
		{
			Vector2 centerOffset = default(Vector2);
			((Vector2)(ref centerOffset))._002Ector((float)((npc.height < npc.width) ? npc.height : npc.width) * 0.7f, 0f);
			centerOffset.X = MathHelper.Max(centerOffset.X, 45f);
			base.Projectile.Center = npc.Center + centerOffset.RotatedBy((float)Math.PI / 30f * Timer + (float)Math.PI / 2f * base.Projectile.ai[2]);
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			base.Projectile.scale += 0.01f;
			base.Projectile.alpha -= 50;
			if (base.Projectile.alpha <= 0)
			{
				base.Projectile.localAI[0] = 1f;
				base.Projectile.alpha = 0;
			}
		}
		else
		{
			base.Projectile.scale -= 0.01f;
			base.Projectile.alpha += 50;
			if (base.Projectile.alpha >= 255)
			{
				base.Projectile.localAI[0] = 0f;
				base.Projectile.alpha = 255;
			}
		}
		Timer++;
		if (Timer != 60f)
		{
			return;
		}
		for (int i = 0; i < 8; i++)
		{
			Dust icyDust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 67, 0f, 0f, 100, default(Color), 2f);
			icyDust.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				icyDust.scale = 0.5f;
				icyDust.fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int j = 0; j < 14; j++)
		{
			Dust dust = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 67, 0f, 0f, 100, default(Color), 3f);
			dust.noGravity = true;
			dust.velocity *= 5f;
			Dust dust2 = Dust.NewDustDirect(base.Projectile.position, base.Projectile.width, base.Projectile.height, 67, 0f, 0f, 100, default(Color), 2f);
			dust2.velocity *= 2f;
		}
		base.Projectile.scale = 1f;
		base.Projectile.ExpandHitboxBy((int)(30f * base.Projectile.scale));
		SoundEngine.PlaySound(in SoundID.Item30, base.Projectile.Center);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		return Main.dayTime ? new Color(50, 50, 255, base.Projectile.alpha) : new Color(255, 255, 255, base.Projectile.alpha);
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Item27, base.Projectile.position);
		if (base.Projectile.owner == Main.myPlayer)
		{
			for (int i = 0; i < 4; i++)
			{
				Vector2 shardVelocity = -Vector2.UnitY.RotatedByRandom(3.1415927410125732) * Main.rand.NextFloat(2.25f, 4.5f);
				int iceShard = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, shardVelocity, ModContent.ProjectileType<FrostShardFriendly>(), (int)((double)base.Projectile.damage * 0.5), 0f, base.Projectile.owner);
				if (iceShard.WithinBounds(Main.maxProjectiles))
				{
					Main.projectile[iceShard].DamageType = DamageClass.Melee;
				}
			}
		}
		for (int k = 0; k < 3; k++)
		{
			Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, 67, base.Projectile.oldVelocity.X * 0.5f, base.Projectile.oldVelocity.Y * 0.5f);
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GlacialState>(), 30);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2).Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, lightColor, base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}
}
