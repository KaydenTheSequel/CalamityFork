using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class PalladiumJavelinProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/PalladiumJavelin";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 2;
		base.Projectile.aiStyle = 113;
		base.Projectile.timeLeft = 600;
		base.Projectile.MaxUpdates = 2;
		base.AIType = 598;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.localNPCHitCooldown = 15 * base.Projectile.MaxUpdates;
		base.Projectile.usesLocalNPCImmunity = true;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		//IL_0183: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_022b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0233: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 4f;
		if (!base.Projectile.Calamity().stealthStrike || base.Projectile.owner != Main.myPlayer || base.Projectile.ai[2] >= 2f)
		{
			return;
		}
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] >= 12f)
		{
			Vector2 vector2 = default(Vector2);
			((Vector2)(ref vector2))._002Ector(20f, 20f);
			for (int index1 = 0; index1 < 10; index1++)
			{
				int index2 = Dust.NewDust(base.Projectile.Center - vector2 / 2f, (int)vector2.X, (int)vector2.Y, 87, 0f, 0f, 100, default(Color), 1.5f);
				Dust obj = Main.dust[index2];
				obj.velocity *= 1.4f;
			}
			for (int i = 0; i < 5; i++)
			{
				Dust.NewDust(base.Projectile.Center - vector2 / 2f, (int)vector2.X, (int)vector2.Y, 144);
			}
			int javelin = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity.RotatedBy(MathHelper.ToRadians(3f)), base.Projectile.type, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 0f, base.Projectile.ai[2] + 1f);
			int javelin2 = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, base.Projectile.velocity.RotatedBy(MathHelper.ToRadians(-3f)), base.Projectile.type, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner, 0f, 0f, base.Projectile.ai[2] + 1f);
			if (javelin.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[javelin].Calamity().stealthStrike = true;
			}
			if (javelin2.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[javelin2].Calamity().stealthStrike = true;
			}
			base.Projectile.Kill();
		}
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

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		OnHitEffects();
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		OnHitEffects();
	}

	public void OnHitEffects()
	{
		if (base.Projectile.owner == Main.myPlayer && base.Projectile.Calamity().stealthStrike)
		{
			Main.player[base.Projectile.owner].AddBuff(58, 300);
		}
	}
}
