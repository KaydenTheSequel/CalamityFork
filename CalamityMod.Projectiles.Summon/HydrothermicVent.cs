using System;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class HydrothermicVent : ModProjectile, ILocalizedModType, IModType
{
	public int dust = 3;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 26;
		base.Projectile.height = 34;
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
		//IL_014c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_025a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_0119: Unknown result type (might be due to invalid IL or missing references)
		//IL_0354: Unknown result type (might be due to invalid IL or missing references)
		//IL_0359: Unknown result type (might be due to invalid IL or missing references)
		//IL_039c: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_03af: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0408: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0443: Unknown result type (might be due to invalid IL or missing references)
		//IL_044a: Unknown result type (might be due to invalid IL or missing references)
		//IL_044f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_045b: Unknown result type (might be due to invalid IL or missing references)
		//IL_045d: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0474: Unknown result type (might be due to invalid IL or missing references)
		//IL_0491: Unknown result type (might be due to invalid IL or missing references)
		//IL_052e: Unknown result type (might be due to invalid IL or missing references)
		//IL_053e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0508: Unknown result type (might be due to invalid IL or missing references)
		//IL_0518: Unknown result type (might be due to invalid IL or missing references)
		//IL_04af: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0557: Unknown result type (might be due to invalid IL or missing references)
		//IL_056d: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0596: Unknown result type (might be due to invalid IL or missing references)
		//IL_059b: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_0622: Unknown result type (might be due to invalid IL or missing references)
		//IL_071d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0728: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_06b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c4: Unknown result type (might be due to invalid IL or missing references)
		bool isMinion = base.Projectile.type == ModContent.ProjectileType<HydrothermicVent>();
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (!modPlayer.chaosSpirit)
		{
			base.Projectile.active = false;
			return;
		}
		if (isMinion)
		{
			if (player.dead)
			{
				modPlayer.cSpirit = false;
			}
			if (modPlayer.cSpirit)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		this.dust--;
		if (this.dust >= 0)
		{
			for (int i = 0; i < 50; i++)
			{
				int dust = Dust.NewDust(new Vector2(base.Projectile.position.X, base.Projectile.position.Y + 16f), base.Projectile.width, base.Projectile.height - 16, Main.rand.NextBool(3) ? 16 : 127);
				Dust obj = Main.dust[dust];
				obj.velocity *= 2f;
				Main.dust[dust].scale *= 1.15f;
			}
		}
		Lighting.AddLight(base.Projectile.Center, (float)(255 - base.Projectile.alpha) * 1f / 255f, (float)(255 - base.Projectile.alpha) * 0.35f / 255f, (float)(255 - base.Projectile.alpha) * 0f / 255f);
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 9)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		bool reversedGravity = player.gravDir == -1f;
		base.Projectile.position.X = player.Center.X - (float)(base.Projectile.width / 2);
		base.Projectile.position.Y = player.Center.Y - (float)(base.Projectile.height / 2) + player.gfxOffY - 60f;
		if (reversedGravity)
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
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		if (base.Projectile.ai[0] != 0f)
		{
			base.Projectile.ai[0]--;
			return;
		}
		bool foundTarget = false;
		Vector2 targetVec = base.Projectile.Center;
		Vector2 half = default(Vector2);
		((Vector2)(ref half))._002Ector(0.5f);
		float range = 1000f;
		int targetIndex = -1;
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				Vector2 sizeCheck = npc.position + npc.Size * half;
				float npcDist = Vector2.Distance(sizeCheck, targetVec);
				if (npcDist < range && Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, npc.position, npc.width, npc.height))
				{
					range = npcDist;
					targetVec = sizeCheck;
					foundTarget = true;
					targetIndex = npc.whoAmI;
				}
			}
		}
		if (!foundTarget)
		{
			for (int k = 0; k < Main.maxNPCs; k++)
			{
				NPC npc2 = Main.npc[k];
				if (npc2.CanBeChasedBy(base.Projectile))
				{
					Vector2 sizeCheck2 = npc2.position + npc2.Size * half;
					float npcDist2 = Vector2.Distance(sizeCheck2, targetVec);
					if (npcDist2 < range && Collision.CanHitLine(base.Projectile.position, base.Projectile.width, base.Projectile.height, npc2.position, npc2.width, npc2.height))
					{
						range = npcDist2;
						targetVec = sizeCheck2;
						foundTarget = true;
						targetIndex = k;
					}
				}
			}
		}
		float yAdjust = ((player.gravDir == -1f) ? 0f : 10f);
		if (!foundTarget || targetIndex == -1)
		{
			return;
		}
		int projectileType = ModContent.ProjectileType<VolcanicFireballSummon>();
		if (reversedGravity ? (Main.npc[targetIndex].Bottom.Y > base.Projectile.Top.Y) : (Main.npc[targetIndex].Bottom.Y < base.Projectile.Top.Y))
		{
			Vector2 source = default(Vector2);
			((Vector2)(ref source))._002Ector(base.Projectile.Center.X - 4f, base.Projectile.Center.Y - yAdjust);
			float num = Main.rand.Next(14, 19);
			Vector2 velocity = targetVec - base.Projectile.Center;
			float targetDist = ((Vector2)(ref velocity)).Length();
			targetDist = num / targetDist;
			velocity.X *= targetDist;
			velocity.Y *= targetDist;
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), source, velocity, projectileType, base.Projectile.damage, 5f, base.Projectile.owner);
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.position);
			base.Projectile.ai[0] = 10f;
			return;
		}
		int amount = Main.rand.Next(2, 4);
		Vector2 velocity2 = default(Vector2);
		for (int j = 0; j < amount; j++)
		{
			((Vector2)(ref velocity2))._002Ector(Main.rand.NextFloat(-10f, 10f), Main.rand.NextFloat(-10f, -7f));
			if (reversedGravity)
			{
				velocity2.Y *= -1f;
			}
			Projectile.NewProjectileDirect(base.Projectile.GetSource_FromThis(), base.Projectile.oldPosition + base.Projectile.Size * 0.5f, velocity2, projectileType, base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner).aiStyle = 1;
		}
		SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.position);
		base.Projectile.ai[0] = 20f;
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
