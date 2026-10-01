using System.Collections.Generic;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Utilities;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class HomingGammaBullet : ModProjectile, ILocalizedModType, IModType
{
	private int targetNPC = -1;

	private List<int> previousNPCs = new List<int> { -1 };

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/Enemy/NuclearBulletMedium";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 24;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.alpha = 255;
		base.Projectile.timeLeft = 360;
		base.Projectile.tileCollide = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.penetrate = 3;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = -1;
		base.Projectile.extraUpdates = 2;
	}

	public override void AI()
	{
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		_ = Main.player[base.Projectile.owner];
		base.Projectile.alpha = Utils.Clamp(base.Projectile.alpha - 35, 0, 255);
		base.Projectile.rotation = base.Projectile.velocity.ToRotation();
		if (targetNPC == -1)
		{
			FindTarget();
		}
		NPC potentialTarget = null;
		if (targetNPC != -1)
		{
			potentialTarget = Main.npc[targetNPC];
		}
		if (potentialTarget != null && !base.Projectile.WithinRange(potentialTarget.Center, 100f) && base.Projectile.timeLeft < 320)
		{
			base.Projectile.velocity = (base.Projectile.velocity * 6f + base.Projectile.SafeDirectionTo(potentialTarget.Center) * 15f) / 7f;
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		previousNPCs.Add(target.whoAmI);
		target.AddBuff(ModContent.BuffType<SulphuricPoisoning>(), 240);
		targetNPC = -1;
		FindTarget();
	}

	private void FindTarget()
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0020: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0169: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		//IL_0175: Unknown result type (might be due to invalid IL or missing references)
		//IL_0177: Unknown result type (might be due to invalid IL or missing references)
		//IL_017c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_0194: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		float range = 1000f;
		bool foundTarget = false;
		Vector2 center = player.Center;
		Vector2 half = default(Vector2);
		((Vector2)(ref half))._002Ector(0.5f);
		half.Y = 0f;
		bool hasHitNPC = false;
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			for (int j = 0; j < previousNPCs.Count; j++)
			{
				if (previousNPCs[j] == player.MinionAttackTargetNPC)
				{
					hasHitNPC = true;
				}
			}
			if (npc.CanBeChasedBy(base.Projectile) && !hasHitNPC && Vector2.Distance(npc.position + npc.Size * half, center) < range && Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, npc.position, npc.width, npc.height))
			{
				foundTarget = true;
				targetNPC = npc.whoAmI;
			}
		}
		hasHitNPC = false;
		if (foundTarget)
		{
			return;
		}
		for (int k = 0; k < Main.maxNPCs; k++)
		{
			NPC npc2 = Main.npc[k];
			for (int i = 0; i < previousNPCs.Count; i++)
			{
				if (previousNPCs[i] == k)
				{
					hasHitNPC = true;
				}
			}
			if (npc2.CanBeChasedBy(base.Projectile) && !hasHitNPC)
			{
				float npcDist = Vector2.Distance(npc2.position + npc2.Size * half, center);
				if (npcDist < range && Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, npc2.position, npc2.width, npc2.height))
				{
					range = npcDist;
					targetNPC = k;
				}
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.dedServ)
		{
			for (int i = 0; i < 3; i++)
			{
				Dust dust = Dust.NewDustDirect(base.Projectile.position, 8, 8, 75, 0f, 0f, 0, default(Color), 0.75f);
				dust.noGravity = true;
				dust.velocity *= 1.8f;
				Dust dust2 = DustExtensions.BetterCloneDust(dust);
				dust2.velocity *= -1f;
			}
		}
	}
}
