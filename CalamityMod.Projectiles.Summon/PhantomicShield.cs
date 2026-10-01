using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class PhantomicShield : ModProjectile, ILocalizedModType, IModType
{
	public const float floatDist = 50f;

	public int deathTimer = 240;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 4;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 38;
		base.Projectile.height = 56;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = 1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(deathTimer);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		deathTimer = reader.ReadInt32();
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
			int shadow = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 27, 0f, 0f, 100, new Color(0, 0, 0), 2f);
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
			int shadow2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 27, 0f, 0f, 100, new Color(0, 0, 0), 3f);
			Main.dust[shadow2].noGravity = true;
			Dust obj2 = Main.dust[shadow2];
			obj2.velocity *= 5f;
			shadow2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 27, 0f, 0f, 100, new Color(0, 0, 0), 2f);
			Dust obj3 = Main.dust[shadow2];
			obj3.velocity *= 2f;
		}
	}

	public override void AI()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		Player owner = Main.player[base.Projectile.owner];
		NPC target = owner.position.MinionHoming(1500f, owner);
		if (target != null)
		{
			Vector2 pos1 = owner.position;
			Vector2 pos2 = target.position;
			base.Projectile.ai[0] = (pos2 - pos1).ToRotation();
		}
		else
		{
			base.Projectile.ai[0] -= MathHelper.ToRadians(2f);
		}
		if (target == null)
		{
			deathTimer--;
		}
		else
		{
			deathTimer = 240;
		}
		if (owner.dead || deathTimer <= 0)
		{
			base.Projectile.Kill();
		}
		base.Projectile.Center = owner.Center + base.Projectile.ai[0].ToRotationVector2() * 50f;
		base.Projectile.rotation = (owner.Center - base.Projectile.Center).ToRotation();
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 5)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
			if (base.Projectile.frame % 2 == 0)
			{
				base.Projectile.netUpdate = true;
			}
		}
		if (base.Projectile.frame > 3)
		{
			base.Projectile.frame = 0;
		}
	}
}
