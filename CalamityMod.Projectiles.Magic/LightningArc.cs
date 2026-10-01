using System;
using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Magic;

public class LightningArc : ModProjectile, ILocalizedModType, IModType
{
	private HashSet<NPC> shockedbefore = new HashSet<NPC>();

	private int prevX;

	public new string LocalizationCategory => "Projectiles.Magic";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 8;
		base.Projectile.height = 8;
		base.Projectile.alpha = 255;
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Magic;
		base.Projectile.timeLeft = 20;
		base.Projectile.penetrate = 5;
		base.Projectile.tileCollide = true;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02db: Unknown result type (might be due to invalid IL or missing references)
		//IL_0219: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0248: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_0287: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0161: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0186: Unknown result type (might be due to invalid IL or missing references)
		//IL_0188: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0329: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_035b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[0] == 0f)
		{
			AdjustMagnitude(ref base.Projectile.velocity);
			base.Projectile.localAI[0] = 1f;
		}
		Vector2 move = Vector2.Zero;
		float distance = 160f;
		bool target = false;
		NPC npc = null;
		bool pastNPC = false;
		if (base.Projectile.timeLeft < 18)
		{
			for (int k = 0; k < Main.maxNPCs; k++)
			{
				if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5 && !shockedbefore.Contains(Main.npc[k]))
				{
					Vector2 newMove = Main.npc[k].Center - (base.Projectile.velocity + base.Projectile.Center);
					float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
						npc = Main.npc[k];
					}
				}
			}
		}
		if (!target)
		{
			foreach (NPC pastnpc in shockedbefore)
			{
				Vector2 newMove2 = pastnpc.Center - (base.Projectile.velocity + base.Projectile.Center);
				float distanceTo2 = (float)Math.Sqrt(newMove2.X * newMove2.X + newMove2.Y * newMove2.Y);
				if (distanceTo2 < distance)
				{
					move = newMove2;
					distance = distanceTo2;
					target = true;
					npc = pastnpc;
					pastNPC = true;
				}
			}
		}
		Vector2 current = base.Projectile.Center;
		if (target)
		{
			shockedbefore.Add(npc);
			npc.HitEffect(0, base.Projectile.damage);
			move += new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11)) * distance / 30f;
			if (pastNPC)
			{
				prevX++;
				move += new Vector2((float)Main.rand.Next(-10, 11), (float)Main.rand.Next(-10, 11)) * (float)prevX;
			}
		}
		else
		{
			move = (base.Projectile.velocity + new Vector2((float)Main.rand.Next(-5, 6), (float)Main.rand.Next(-5, 6))) * 5f;
		}
		for (int i = 0; i < 20; i++)
		{
			int dust = Dust.NewDust(current, base.Projectile.width, base.Projectile.height, 20);
			Main.dust[dust].velocity = new Vector2(0f);
			current += move / 20f;
		}
		base.Projectile.position = current;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = oldVelocity;
		base.Projectile.timeLeft -= 12;
		return false;
	}

	private void AdjustMagnitude(ref Vector2 vector)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0039: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
		if (magnitude > 6f)
		{
			vector *= 6f / magnitude;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<StaticDischarge>(), 120);
	}
}
