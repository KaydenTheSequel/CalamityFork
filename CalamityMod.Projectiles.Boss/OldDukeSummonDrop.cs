using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.NPCs.OldDuke;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Boss;

public class OldDukeSummonDrop : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Boss";

	public override string Texture => "CalamityMod/Projectiles/Environment/AcidDrop";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 5;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 20;
		base.Projectile.hostile = true;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = false;
		base.Projectile.timeLeft = 400;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() - (float)Math.PI / 2f;
		if (base.Projectile.velocity.Y <= 8f)
		{
			base.Projectile.velocity.Y += 0.15f;
		}
		if (Main.rand.NextBool(3))
		{
			GeneralParticleHandler.SpawnParticle(new MediumMistParticle(base.Projectile.Center, -(base.Projectile.velocity / 4f).RotatedBy(Main.rand.NextFloat(-(float)Math.PI / 4f, (float)Math.PI / 4f)), OldDuke.GlowColor, Color.DarkSlateGray, Main.rand.NextFloat(0.5f, 1.5f), 150f));
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 4; i++)
		{
			int idx = Dust.NewDust(base.Projectile.position - base.Projectile.velocity, 2, 2, 154, 0f, 0f, 0, new Color(112, 150, 42, 127));
			Main.dust[idx].position.X -= 2f;
			Main.dust[idx].alpha = 38;
			Dust obj = Main.dust[idx];
			obj.velocity *= 0.1f;
			Dust obj2 = Main.dust[idx];
			obj2.velocity -= base.Projectile.velocity * 0.025f;
			Main.dust[idx].scale = 0.75f;
		}
		return true;
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		if (info.Damage > 0)
		{
			if (Main.rand.NextBool())
			{
				target.AddBuff(20, 60 * Main.rand.Next(1, 4));
			}
			else if (Main.rand.NextBool(4))
			{
				target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 60 * Main.rand.Next(1, 3));
			}
			else
			{
				target.AddBuff(ModContent.BuffType<Irradiated>(), 60 * Main.rand.Next(3, 6));
			}
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition, tex.Frame(), Color.White, base.Projectile.rotation, tex.Frame().Bottom(), new Vector2(1f, 1f / (float)tex.Height() * (((Vector2)(ref base.Projectile.velocity)).Length() * 3f + 6f)), (SpriteEffects)0);
		return false;
	}
}
