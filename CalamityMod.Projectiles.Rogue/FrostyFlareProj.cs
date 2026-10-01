using System;
using CalamityMod.Buffs.StatDebuffs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class FrostyFlareProj : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/FrostyFlare";

	public override void SetDefaults()
	{
		base.Projectile.width = 10;
		base.Projectile.height = 10;
		base.Projectile.coldDamage = true;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft = 300;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_0097: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0150: Unknown result type (might be due to invalid IL or missing references)
		//IL_0227: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_02dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0343: Unknown result type (might be due to invalid IL or missing references)
		//IL_0345: Unknown result type (might be due to invalid IL or missing references)
		//IL_034d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_038e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0390: Unknown result type (might be due to invalid IL or missing references)
		//IL_039a: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		bool shoot = false;
		if ((float)base.Projectile.timeLeft % 30f == 0f && base.Projectile.owner == Main.myPlayer)
		{
			shoot = true;
		}
		if (base.Projectile.ai[0] == 0f)
		{
			base.Projectile.velocity.X *= 0.99f;
			base.Projectile.velocity.Y += 0.25f;
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI / 2f;
			if (shoot)
			{
				Vector2 pos = base.Projectile.Center - new Vector2((float)Main.rand.Next(-300, 301), (float)Main.rand.Next(500, 751));
				Vector2 vel = pos.DirectionTo(base.Projectile.Center) * 30f;
				vel.X += Main.rand.NextFloat(-4f, 4f);
				int shard = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), pos, vel + base.Projectile.velocity / 4f, ModContent.ProjectileType<FrostShardFriendly>(), (int)((float)base.Projectile.damage * 0.75f), base.Projectile.knockBack, base.Projectile.owner);
				Main.projectile[shard].alpha = base.Projectile.alpha;
			}
			int index2 = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, 172);
			Main.dust[index2].noGravity = true;
			if (base.Projectile.ai[2] == 1f)
			{
				CalamityUtils.HomeInOnSelectedNPC(base.Projectile, base.Projectile.Center.ClosestNPCAt(400f, ignoreTiles: false), ignoreTiles: false, 5f, 30f, 0.99f);
			}
			return;
		}
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		int id = (int)base.Projectile.ai[1];
		if (id >= 0 && id < Main.maxNPCs && Main.npc[id].active && !Main.npc[id].dontTakeDamage)
		{
			base.Projectile.Center = Main.npc[id].Center - base.Projectile.velocity * 2f;
			base.Projectile.gfxOffY = Main.npc[id].gfxOffY;
			if (shoot)
			{
				Vector2 pos2 = base.Projectile.Center - new Vector2((float)Main.rand.Next(-300, 301), (float)Main.rand.Next(500, 751));
				Vector2 vel2 = pos2.DirectionTo(base.Projectile.Center) * 30f;
				vel2.X += Main.rand.NextFloat(-4f, 4f);
				int shard2 = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), pos2, vel2 + Main.npc[id].velocity, ModContent.ProjectileType<FrostShardFriendly>(), (int)((float)base.Projectile.damage * 0.75f), base.Projectile.knockBack, base.Projectile.owner);
				Main.projectile[shard2].alpha = base.Projectile.alpha;
			}
		}
		else
		{
			base.Projectile.Kill();
		}
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		target.AddBuff(324, 180);
		target.AddBuff(ModContent.BuffType<GlacialState>(), 30);
		base.Projectile.ai[0] = 1f;
		base.Projectile.ai[1] = target.whoAmI;
		base.Projectile.velocity = target.Center - base.Projectile.Center;
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.75f;
		base.Projectile.netUpdate = true;
		int flaresFound = 0;
		int oldestFlare = -1;
		int oldestFlareTimeLeft = 300;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.owner == Main.myPlayer && p.type == base.Projectile.type && p.whoAmI != base.Projectile.whoAmI && p.ai[1] == (float)target.whoAmI)
			{
				flaresFound++;
				if (p.timeLeft < oldestFlareTimeLeft)
				{
					oldestFlareTimeLeft = p.timeLeft;
					oldestFlare = p.whoAmI;
				}
				if (flaresFound >= 5)
				{
					break;
				}
			}
		}
		if (flaresFound >= 5 && oldestFlare >= 0)
		{
			Main.projectile[oldestFlare].Kill();
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(324, 180);
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.ai[0] != 0f)
		{
			return false;
		}
		return null;
	}
}
