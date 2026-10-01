using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class DaedalusCrystal : ModProjectile, ILocalizedModType, IModType
{
	public int dust = 3;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 46;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minion = true;
		base.Projectile.minionSlots = 0f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0526: Unknown result type (might be due to invalid IL or missing references)
		//IL_053b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0564: Unknown result type (might be due to invalid IL or missing references)
		//IL_0569: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0579: Unknown result type (might be due to invalid IL or missing references)
		//IL_05bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04da: Unknown result type (might be due to invalid IL or missing references)
		bool isMinion = base.Projectile.type == ModContent.ProjectileType<DaedalusCrystal>();
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!modPlayer.daedalusCrystal)
		{
			base.Projectile.active = false;
			return;
		}
		if (isMinion)
		{
			if (player.dead)
			{
				modPlayer.dCrystal = false;
			}
			if (modPlayer.dCrystal)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		this.dust--;
		if (this.dust >= 0)
		{
			int constant = 50;
			for (int i = 0; i < constant; i++)
			{
				int dust = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + 16f), base.Projectile.width, base.Projectile.height - 16, 173);
				Dust obj = Main.dust[dust];
				obj.velocity *= 2f;
				Main.dust[dust].scale *= 1.15f;
			}
		}
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 0.35f / 255f, (float)(255 - base.Projectile.alpha) * 0f / 255f, (float)(255 - base.Projectile.alpha) * 0.75f / 255f);
		base.Projectile.Center = player.Center + Vector2.UnitY * (player.gfxOffY - 60f);
		if (player.gravDir == -1f)
		{
			base.Projectile.position.Y += 120f;
			base.Projectile.rotation = (float)Math.PI;
		}
		else
		{
			base.Projectile.rotation = 0f;
		}
		base.Projectile.position.X = (int)base.Projectile.position.X;
		base.Projectile.position.Y = (int)base.Projectile.position.Y;
		float projScale = (float)(int)Main.mouseTextColor / 200f - 0.35f;
		projScale *= 0.2f;
		base.Projectile.scale = projScale + 0.95f;
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		if (base.Projectile.ai[0] != 0f)
		{
			base.Projectile.ai[0]--;
			return;
		}
		bool isInRange = false;
		float projX = base.Projectile.Center.X;
		float projY = base.Projectile.Center.Y;
		float attackRange = 1000f;
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float npcX = npc.position.X + (float)(npc.width / 2);
				float npcY = npc.position.Y + (float)(npc.height / 2);
				if (Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - npcX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - npcY) < attackRange && Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, npc.position, npc.width, npc.height))
				{
					projX = npcX;
					projY = npcY;
					isInRange = true;
				}
			}
		}
		if (!isInRange)
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC n = enumerator.Current;
				if (n.CanBeChasedBy(base.Projectile))
				{
					float otherNPCX = n.position.X + (float)(n.width / 2);
					float otherNPCY = n.position.Y + (float)(n.height / 2);
					float otherNPCDist = Math.Abs(base.Projectile.position.X + (float)(base.Projectile.width / 2) - otherNPCX) + Math.Abs(base.Projectile.position.Y + (float)(base.Projectile.height / 2) - otherNPCY);
					if (otherNPCDist < attackRange && Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, n.position, n.width, n.height))
					{
						attackRange = otherNPCDist;
						projX = otherNPCX;
						projY = otherNPCY;
						isInRange = true;
					}
				}
			}
		}
		if (isInRange)
		{
			float projXStore = projX;
			float projYStore = projY;
			projX -= base.Projectile.Center.X;
			projY -= base.Projectile.Center.Y;
			int projectileType = ModContent.ProjectileType<DaedalusCrystalShot>();
			float num = Main.rand.Next(10, 15);
			Vector2 firingDirection = base.Projectile.Center;
			float projXDirection = projXStore - firingDirection.X;
			float projYDirection = projYStore - firingDirection.Y;
			float projSpeed = (float)Math.Sqrt(projXDirection * projXDirection + projYDirection * projYDirection);
			projSpeed = num / projSpeed;
			projXDirection *= projSpeed;
			projYDirection *= projSpeed;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center.X - 4f, base.Projectile.Center.Y, projXDirection, projYDirection, projectileType, base.Projectile.damage, 5f, base.Projectile.owner);
			base.Projectile.ai[0] = 50f;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, 200);
	}

	public override bool? CanDamage()
	{
		return false;
	}
}
