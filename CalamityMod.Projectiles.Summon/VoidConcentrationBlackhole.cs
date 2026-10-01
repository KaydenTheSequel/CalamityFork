using System;
using System.IO;
using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class VoidConcentrationBlackhole : ModProjectile, ILocalizedModType, IModType
{
	private int damage;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(damage);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		damage = reader.ReadInt32();
	}

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 80;
		base.Projectile.height = 85;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 1800;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.scale = 0.01f;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	private void ApplySucc(NPC npc)
	{
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0112: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		float succStrength = 4f / base.Projectile.scale;
		succStrength *= base.Projectile.scale;
		Vector2 velocity = base.Projectile.Center - npc.Center;
		velocity *= 2f;
		velocity.SafeNormalize(Vector2.Zero);
		float projSpeed = 5f * base.Projectile.scale;
		Vector2 fireDirection = npc.Center;
		float fireXVel = base.Projectile.Center.X - fireDirection.X;
		float fireYVel = base.Projectile.Center.Y - fireDirection.Y;
		float fireVelocity = (float)Math.Sqrt(fireXVel * fireXVel + fireYVel * fireYVel);
		if (fireVelocity < 100f)
		{
			projSpeed = 28f;
		}
		fireVelocity = projSpeed / fireVelocity;
		fireXVel *= fireVelocity;
		fireYVel *= fireVelocity;
		npc.velocity.X = (velocity.X * 15f + fireXVel) / 16f;
		npc.velocity.Y = (velocity.Y * 15f + fireYVel) / 16f;
		npc.velocity = velocity / succStrength;
	}

	private void Death()
	{
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.scale >= 2.5f)
		{
			base.Projectile.height *= 2;
			base.Projectile.width *= 2;
			base.Projectile.maxPenetrate = -1;
			base.Projectile.penetrate = -1;
			base.Projectile.usesLocalNPCImmunity = true;
			base.Projectile.localNPCHitCooldown = 10;
			base.Projectile.damage = damage;
			base.Projectile.Damage();
			base.Projectile.friendly = false;
			base.Projectile.Damage();
			SoundEngine.PlaySound(in SoundID.Item14, base.Projectile.Center);
			base.Projectile.Kill();
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_016e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		for (int d = 0; d < 6; d++)
		{
			int shadow = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 27, 0f, 0f, 100, new Color(0, 0, 0), 4f);
			Dust obj = Main.dust[shadow];
			obj.velocity *= 3f;
			if (Main.rand.NextBool())
			{
				Main.dust[shadow].scale = 0.5f;
				Main.dust[shadow].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
			}
		}
		for (int i = 0; i < 18; i++)
		{
			int shadow2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 27, 0f, 0f, 100, new Color(0, 0, 0), 4f);
			Main.dust[shadow2].noGravity = true;
			Dust obj2 = Main.dust[shadow2];
			obj2.velocity *= 5f;
			shadow2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 27, 0f, 0f, 100, new Color(0, 0, 0), 3f);
			Dust obj3 = Main.dust[shadow2];
			obj3.velocity *= 2f;
		}
	}

	public override bool PreAI()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (damage == 0)
		{
			damage = base.Projectile.damage;
			base.Projectile.damage = 0;
			base.Projectile.position = Main.player[base.Projectile.owner].position;
		}
		return true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0022: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		Vector2 drawPos = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		int height = texture.Height / Main.projFrames[base.Type];
		int frameHeight = height * base.Projectile.frame;
		Rectangle rectangle = default(Rectangle);
		((Rectangle)(ref rectangle))._002Ector(0, frameHeight, texture.Width, height);
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)texture.Width / 2f, (float)height / 2f);
		Main.EntitySpriteDraw(texture, drawPos, rectangle, lightColor, base.Projectile.rotation, origin, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void AI()
	{
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.scale += 0.01f;
		if (base.Projectile.scale % 0.02f == 0f)
		{
			base.Projectile.scale += 0.01f;
		}
		int radius = (int)(base.Projectile.scale * 100f);
		if (base.Projectile.scale >= 2f)
		{
			radius *= 2;
		}
		int baseHeight = 34;
		int newWidth = (int)(32f * base.Projectile.scale);
		int newHeight = (int)((float)baseHeight * base.Projectile.scale);
		base.Projectile.ExpandHitboxBy(newWidth, newHeight);
		if (base.Projectile.frameCounter > 6)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type] - 1)
		{
			base.Projectile.frame = 0;
		}
		base.Projectile.frameCounter++;
		ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
		while (enumerator.MoveNext())
		{
			NPC n = enumerator.Current;
			if (!n.friendly && CalamityGlobalNPC.ShouldAffectNPC(n) && Vector2.Distance(base.Projectile.Center, n.Center) <= (float)radius)
			{
				ApplySucc(n);
			}
		}
		if (base.Projectile.scale >= 2.5f)
		{
			Death();
		}
	}
}
