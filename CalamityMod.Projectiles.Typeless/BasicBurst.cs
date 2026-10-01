using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class BasicBurst : ModProjectile, ILocalizedModType, IModType
{
	public Vector2 pushVelocity;

	public float customKnockback;

	public bool hasStongDisplacement;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 2;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
	}

	public override void AI()
	{
		if (customKnockback == 0f)
		{
			if (base.Projectile.knockBack < 0f)
			{
				hasStongDisplacement = true;
			}
			customKnockback = Math.Abs(base.Projectile.knockBack);
			base.Projectile.knockBack = 0f;
		}
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.ai[0] = 50f;
		}
		if (base.Projectile.ai[1] == 0f)
		{
			base.Projectile.ai[1] = 0.1f;
		}
		if (base.Projectile.ai[2] == 0f)
		{
			base.Projectile.ai[2] = 5f;
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] != 0f)
		{
			target.AddBuff((int)base.Projectile.localAI[0], (int)base.Projectile.localAI[1]);
		}
		if (base.Projectile.localAI[2] != 0f)
		{
			target.AddBuff((int)base.Projectile.localAI[2], (int)base.Projectile.localAI[1]);
		}
		pushVelocity = base.Projectile.Center.DirectionTo(target.Center) * customKnockback;
		float minMult = base.Projectile.ai[1];
		int hitsToMinMult = (int)base.Projectile.ai[2];
		float damageMult = Utils.Remap(base.Projectile.numHits, 0f, hitsToMinMult, 1f, minMult);
		modifiers.SourceDamage *= damageMult;
		if (customKnockback != 0f && target.CanBeMoved(hasStongDisplacement))
		{
			target.velocity = pushVelocity * ((target.knockBackResist == 0f) ? 0.5f : 1f);
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		return CalamityUtils.CircularHitboxCollision(base.Projectile.Center, base.Projectile.ai[0], targetHitbox);
	}

	public override void SendExtraAI(BinaryWriter writer)
	{
		writer.Write(base.Projectile.localAI[0]);
		writer.Write(base.Projectile.localAI[1]);
		writer.Write(base.Projectile.localAI[2]);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		base.Projectile.localAI[0] = reader.ReadSingle();
		base.Projectile.localAI[1] = reader.ReadSingle();
		base.Projectile.localAI[2] = reader.ReadSingle();
	}
}
