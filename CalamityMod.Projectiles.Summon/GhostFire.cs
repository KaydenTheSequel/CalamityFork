using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class GhostFire : ModProjectile, ILocalizedModType, IModType
{
	public bool ableToHit = true;

	public NPC target;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 20;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 16);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.penetrate = 200;
		base.Projectile.extraUpdates = 3;
		base.Projectile.timeLeft = 600;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 50;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override bool? CanDamage()
	{
		if (!ableToHit)
		{
			return false;
		}
		return null;
	}

	public override bool? CanHitNPC(NPC target)
	{
		if (!target.CanBeChasedBy())
		{
			return false;
		}
		return null;
	}

	public override void AI()
	{
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		base.Projectile.localAI[0] += 1f / (float)(base.Projectile.extraUpdates + 1);
		if (base.Projectile.penetrate < 200)
		{
			if (base.Projectile.timeLeft > 60)
			{
				base.Projectile.timeLeft = 60;
			}
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.88f;
		}
		else if (base.Projectile.localAI[0] < 60f)
		{
			Projectile projectile2 = base.Projectile;
			projectile2.velocity *= 0.93f;
		}
		else
		{
			FindTarget(player);
		}
		if (base.Projectile.timeLeft <= 20)
		{
			ableToHit = false;
		}
	}

	public void FindTarget(Player player)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		float maxDistance = 3000f;
		bool foundTarget = false;
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float targetDist = Vector2.Distance(npc.Center, base.Projectile.Center);
				if (targetDist < maxDistance)
				{
					maxDistance = targetDist;
					foundTarget = true;
					target = npc;
				}
			}
		}
		if (!foundTarget)
		{
			for (int npcIndex = 0; npcIndex < Main.maxNPCs; npcIndex++)
			{
				NPC npc2 = Main.npc[npcIndex];
				if (npc2.CanBeChasedBy(base.Projectile))
				{
					float targetDist2 = Vector2.Distance(npc2.Center, base.Projectile.Center);
					if (targetDist2 < maxDistance)
					{
						maxDistance = targetDist2;
						foundTarget = true;
						target = npc2;
					}
				}
			}
		}
		if (!foundTarget)
		{
			Projectile projectile = base.Projectile;
			projectile.velocity *= 0.98f;
		}
		else
		{
			KillTheThing(target);
		}
	}

	public void KillTheThing(NPC npc)
	{
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.velocity = base.Projectile.SuperhomeTowardsTarget(npc, 50f / (float)(base.Projectile.extraUpdates + 1), 60f / (float)(base.Projectile.extraUpdates + 1), 1f / (float)(base.Projectile.extraUpdates + 1));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0067: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0202: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0211: Unknown result type (might be due to invalid IL or missing references)
		//IL_0218: Unknown result type (might be due to invalid IL or missing references)
		Texture2D lightTexture = ModContent.Request<Texture2D>("CalamityMod/ExtraTextures/SmallGreyscaleCircle", (AssetRequestMode)2).Value;
		for (int i = 0; i < base.Projectile.oldPos.Length; i++)
		{
			float colorInterpolation = (float)Math.Cos((float)base.Projectile.timeLeft / 32f + Main.GlobalTimeWrappedHourly / 20f + (float)i / (float)base.Projectile.oldPos.Length * (float)Math.PI) * 0.5f + 0.5f;
			Color color = Color.Lerp(Color.Cyan, Color.LightBlue, colorInterpolation) * 0.4f;
			((Color)(ref color)).A = 0;
			Vector2 drawPosition = base.Projectile.oldPos[i] + lightTexture.Size() * 0.5f - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY) + new Vector2(-28f, -28f);
			Color outerColor = color;
			Color innerColor = color * 0.5f;
			float intensity = 0.9f + 0.15f * (float)Math.Cos(Main.GlobalTimeWrappedHourly % 60f * ((float)Math.PI * 2f));
			intensity *= MathHelper.Lerp(0.15f, 1f, 1f - (float)i / (float)base.Projectile.oldPos.Length);
			if (base.Projectile.timeLeft <= 60)
			{
				intensity *= (float)base.Projectile.timeLeft / 60f;
			}
			Vector2 outerScale = new Vector2(1f) * intensity;
			Vector2 innerScale = new Vector2(1f) * intensity * 0.7f;
			outerColor *= intensity;
			innerColor *= intensity;
			Main.EntitySpriteDraw(lightTexture, drawPosition, null, outerColor, 0f, lightTexture.Size() * 0.5f, outerScale * 0.6f, (SpriteEffects)0);
			Main.EntitySpriteDraw(lightTexture, drawPosition, null, innerColor, 0f, lightTexture.Size() * 0.5f, innerScale * 0.6f, (SpriteEffects)0);
		}
		return false;
	}
}
