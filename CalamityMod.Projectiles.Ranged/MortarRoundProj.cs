using System;
using CalamityMod.Items.Ammo;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Ranged;

public class MortarRoundProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Ranged";

	public override string Texture => "CalamityMod/Items/Ammo/MortarRound";

	public override void SetDefaults()
	{
		base.Projectile.width = 14;
		base.Projectile.height = 14;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = 1;
		base.Projectile.timeLeft = 150;
		base.Projectile.DamageType = DamageClass.Ranged;
	}

	public override void AI()
	{
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0149: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_021b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0225: Unknown result type (might be due to invalid IL or missing references)
		//IL_022a: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
		if (!(((Vector2)(ref base.Projectile.velocity)).Length() >= 8f))
		{
			return;
		}
		for (int d = 0; d < 2; d++)
		{
			float xOffset = 0f;
			float yOffset = 0f;
			if (d == 1)
			{
				xOffset = base.Projectile.velocity.X * 0.5f;
				yOffset = base.Projectile.velocity.Y * 0.5f;
			}
			int fire = Dust.NewDust(new Vector2(base.Projectile.position.X + 3f + xOffset, base.Projectile.position.Y + 3f + yOffset) - base.Projectile.velocity * 0.5f, base.Projectile.width - 8, base.Projectile.height - 8, 6, 0f, 0f, 100);
			Main.dust[fire].scale *= 2f + (float)Main.rand.Next(10) * 0.1f;
			Dust obj = Main.dust[fire];
			obj.velocity *= 0.2f;
			Main.dust[fire].noGravity = true;
			int smoke = Dust.NewDust(new Vector2(base.Projectile.position.X + 3f + xOffset, base.Projectile.position.Y + 3f + yOffset) - base.Projectile.velocity * 0.5f, base.Projectile.width - 8, base.Projectile.height - 8, 31, 0f, 0f, 100, default(Color), 0.5f);
			Main.dust[smoke].fadeIn = 1f + (float)Main.rand.Next(5) * 0.1f;
			Dust obj2 = Main.dust[smoke];
			obj2.velocity *= 0.05f;
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

	public override void OnKill(int timeLeft)
	{
		Explode();
	}

	private void Explode()
	{
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ExpandHitboxBy(MortarRound.HitboxBlastRadius);
		base.Projectile.maxPenetrate = (base.Projectile.penetrate = -1);
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.knockBack *= 2f;
		base.Projectile.Damage();
		if (base.Projectile.owner == Main.myPlayer)
		{
			base.Projectile.ExplodeTiles(MortarRound.TileBlastRadius);
		}
		SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
		SpawnDust(base.Projectile);
		SpawnExplosionGores(base.Projectile);
	}

	internal static void SpawnDust(Projectile p)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0101: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		for (int d = 0; d < 40; d++)
		{
			int smoke = Dust.NewDust(p.position, p.width, p.height, 31, 0f, 0f, 100, default(Color), 2f);
			Dust obj = Main.dust[smoke];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[smoke].scale = 0.5f;
				Main.dust[smoke].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int i = 0; i < 70; i++)
		{
			int fire = Dust.NewDust(p.position, p.width, p.height, 6, 0f, 0f, 100, default(Color), 3f);
			Main.dust[fire].noGravity = true;
			Dust obj2 = Main.dust[fire];
			obj2.velocity *= 5f;
			fire = Dust.NewDust(p.position, p.width, p.height, 6, 0f, 0f, 100, default(Color), 2f);
			Dust obj3 = Main.dust[fire];
			obj3.velocity *= 2f;
		}
	}

	internal static void SpawnExplosionGores(Projectile p)
	{
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f6: Unknown result type (might be due to invalid IL or missing references)
		if (Main.dedServ)
		{
			return;
		}
		Vector2 goreSource = p.Center;
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
			int type = Main.rand.Next(61, 64);
			int smoke = Gore.NewGore(p.GetSource_FromAI(), source, default(Vector2), type);
			Gore obj = Main.gore[smoke];
			obj.velocity *= velocityMult;
			obj.velocity.X++;
			obj.velocity.Y++;
			type = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(p.GetSource_FromAI(), source, default(Vector2), type);
			Gore obj2 = Main.gore[smoke];
			obj2.velocity *= velocityMult;
			obj2.velocity.X--;
			obj2.velocity.Y++;
			type = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(p.GetSource_FromAI(), source, default(Vector2), type);
			Gore obj3 = Main.gore[smoke];
			obj3.velocity *= velocityMult;
			obj3.velocity.X++;
			obj3.velocity.Y--;
			type = Main.rand.Next(61, 64);
			smoke = Gore.NewGore(p.GetSource_FromAI(), source, default(Vector2), type);
			Gore obj4 = Main.gore[smoke];
			obj4.velocity *= velocityMult;
			obj4.velocity.X--;
			obj4.velocity.Y--;
		}
	}
}
