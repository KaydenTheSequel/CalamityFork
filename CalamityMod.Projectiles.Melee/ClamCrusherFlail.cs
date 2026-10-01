using System;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.NPCs.SunkenSea;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class ClamCrusherFlail : ModProjectile, ILocalizedModType, IModType
{
	public int finalDamage;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override void SetDefaults()
	{
		base.Projectile.width = 58;
		base.Projectile.height = 74;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = DamageClass.MeleeNoSpeed;
		base.Projectile.alpha = 255;
		base.Projectile.tileCollide = false;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_043e: Unknown result type (might be due to invalid IL or missing references)
		//IL_043f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0446: Unknown result type (might be due to invalid IL or missing references)
		//IL_044b: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] == 1f)
		{
			finalDamage = base.Projectile.damage * 4;
		}
		if (base.Projectile.ai[1] >= 5f && base.Projectile.ai[1] <= 10f)
		{
			for (int i = 0; i < 10; i++)
			{
				float shortXVel = base.Projectile.velocity.X / 3f * (float)i;
				float shortYVel = base.Projectile.velocity.Y / 3f * (float)i;
				int dustPos = 4;
				int waterDust = Dust.NewDust(new Vector2(base.Projectile.position.X + (float)dustPos, base.Projectile.position.Y + (float)dustPos), base.Projectile.width - dustPos * 2, base.Projectile.height - dustPos * 2, 33, 0f, 0f, 0, new Color(0, 142, 255), 1.5f);
				Dust obj = Main.dust[waterDust];
				obj.noGravity = true;
				obj.velocity *= 0.1f;
				obj.velocity += base.Projectile.velocity * 0.1f;
				obj.position.X -= shortXVel;
				obj.position.Y -= shortYVel;
			}
		}
		if (base.Projectile.ai[1] == 5f)
		{
			base.Projectile.tileCollide = true;
		}
		Vector2 flailDirection = Main.player[base.Projectile.owner].Center - base.Projectile.Center;
		base.Projectile.rotation = flailDirection.ToRotation() - (float)Math.PI / 2f;
		if (Main.player[base.Projectile.owner].dead)
		{
			base.Projectile.Kill();
			return;
		}
		Main.player[base.Projectile.owner].itemAnimation = 10;
		Main.player[base.Projectile.owner].itemTime = 10;
		if (flailDirection.X < 0f)
		{
			Main.player[base.Projectile.owner].ChangeDir(1);
			base.Projectile.direction = 1;
		}
		else
		{
			Main.player[base.Projectile.owner].ChangeDir(-1);
			base.Projectile.direction = -1;
		}
		Main.player[base.Projectile.owner].itemRotation = (flailDirection * -1f * (float)base.Projectile.direction).ToRotation();
		base.Projectile.spriteDirection = ((!(flailDirection.X > 0f)) ? 1 : (-1));
		if (base.Projectile.ai[1] >= 45f && (base.Projectile.ai[0] != 1f || base.Projectile.ai[0] != 2f))
		{
			base.Projectile.velocity.Y++;
			base.Projectile.velocity.X *= 0.995f;
			base.Projectile.damage = finalDamage;
		}
		if (base.Projectile.ai[0] == 0f && ((Vector2)(ref flailDirection)).Length() > 1000f)
		{
			base.Projectile.ai[0] = 1f;
		}
		if (base.Projectile.ai[0] == 1f || base.Projectile.ai[0] == 2f)
		{
			float flailDistance = ((Vector2)(ref flailDirection)).Length();
			if (flailDistance > 1500f)
			{
				base.Projectile.Kill();
				return;
			}
			if (flailDistance > 600f)
			{
				base.Projectile.ai[0] = 2f;
			}
			base.Projectile.tileCollide = false;
			float flailSpeed = 20f;
			if (base.Projectile.ai[0] == 2f)
			{
				flailSpeed = 40f;
			}
			base.Projectile.velocity = Vector2.Normalize(flailDirection) * flailSpeed;
			if (((Vector2)(ref flailDirection)).Length() < flailSpeed)
			{
				base.Projectile.Kill();
				return;
			}
		}
		base.Projectile.ai[1]++;
		if (base.Projectile.ai[1] > 5f)
		{
			base.Projectile.alpha = 0;
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] >= 5f)
		{
			Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
			base.Projectile.ai[0] = 1f;
			base.Projectile.netUpdate = true;
			SoundEngine.PlaySound(in GiantClam.SlamSound, base.Projectile.position);
			for (int i = 0; i < 50; i++)
			{
				Vector2 velocity = CalamityUtils.RandomVelocity(100f, 70f, 100f);
				int waterDust = Dust.NewDust(new Vector2(base.Projectile.Center.X, base.Projectile.Center.Y), base.Projectile.width / 2, base.Projectile.height / 2, 33, velocity.X, velocity.Y, 0, new Color(0, 142, 255), 1.5f);
				Dust obj = Main.dust[waterDust];
				obj.velocity *= 2f;
			}
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0109: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		Vector2 mountedCenter = Main.player[base.Projectile.owner].MountedCenter;
		Texture2D texture2D2 = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Melee/ClamCrusherChain", (AssetRequestMode)2).Value;
		Vector2 projCenter = base.Projectile.Center;
		Rectangle? sourceRectangle = null;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)texture2D2.Width * 0.5f, (float)texture2D2.Height * 0.5f);
		float projHeight = texture2D2.Height;
		Vector2 actualCenter = mountedCenter - projCenter;
		float flailRotate = (float)Math.Atan2(actualCenter.Y, actualCenter.X) - (float)Math.PI / 2f;
		bool isActive = true;
		if (float.IsNaN(projCenter.X) && float.IsNaN(projCenter.Y))
		{
			isActive = false;
		}
		if (float.IsNaN(actualCenter.X) && float.IsNaN(actualCenter.Y))
		{
			isActive = false;
		}
		while (isActive)
		{
			if (((Vector2)(ref actualCenter)).Length() < projHeight + 1f)
			{
				isActive = false;
				continue;
			}
			Vector2 value2 = actualCenter;
			((Vector2)(ref value2)).Normalize();
			projCenter += value2 * projHeight;
			actualCenter = mountedCenter - projCenter;
			Color colorArea = Lighting.GetColor((int)projCenter.X / 16, (int)(projCenter.Y / 16f));
			Main.spriteBatch.Draw(texture2D2, projCenter - Main.screenPosition, sourceRectangle, colorArea, flailRotate, origin, 1f, (SpriteEffects)0, 0f);
		}
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] >= 45f && (base.Projectile.ai[0] != 1f || base.Projectile.ai[0] != 2f))
		{
			target.AddBuff(ModContent.BuffType<Eutrophication>(), 120);
		}
		else
		{
			target.AddBuff(ModContent.BuffType<Eutrophication>(), 60);
		}
		base.Projectile.ai[0] = 1f;
		base.Projectile.netUpdate = true;
		SoundEngine.PlaySound(in GiantClam.SlamSound, base.Projectile.position);
	}
}
