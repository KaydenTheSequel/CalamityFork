using CalamityMod.NPCs;
using CalamityMod.NPCs.NormalNPCs;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class SealedSingularityBlackhole : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 7;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 40;
		base.Projectile.height = 40;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.tileCollide = false;
		base.Projectile.usesIDStaticNPCImmunity = true;
		base.Projectile.idStaticNPCHitCooldown = 10;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor);
		return false;
	}

	public override void AI()
	{
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft % 5 == 0)
		{
			base.Projectile.frame++;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		float projCenX = base.Projectile.Center.X;
		float projCenY = base.Projectile.Center.Y;
		float maxDistance = (base.Projectile.Calamity().stealthStrike ? 1000f : 500f);
		float succPower = (base.Projectile.Calamity().stealthStrike ? 0.25f : 0.1f);
		for (int index = 0; index < Main.maxNPCs; index++)
		{
			NPC npc = Main.npc[index];
			if (!npc.CanBeChasedBy(base.Projectile) || !Collision.CanHit(base.Projectile.Center, 1, 1, npc.Center, 1, 1) || (!CalamityGlobalNPC.ShouldAffectNPC(npc) && npc.type != ModContent.NPCType<SuperDummyNPC>()))
			{
				continue;
			}
			float extraDistance = npc.width / 2 + npc.height / 2;
			if (Vector2.Distance(npc.Center, base.Projectile.Center) < maxDistance + extraDistance)
			{
				if (npc.position.X < projCenX)
				{
					npc.velocity.X += succPower;
				}
				else
				{
					npc.velocity.X -= succPower;
				}
				if (npc.position.Y < projCenY)
				{
					npc.velocity.Y += succPower;
				}
				else
				{
					npc.velocity.Y -= succPower;
				}
			}
		}
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] > 300f)
		{
			base.Projectile.scale *= 0.95f;
			base.Projectile.Opacity *= 0.95f;
			base.Projectile.height = (int)((float)base.Projectile.height * base.Projectile.scale);
			base.Projectile.width = (int)((float)base.Projectile.width * base.Projectile.scale);
		}
		if (base.Projectile.scale <= 0.05f)
		{
			base.Projectile.Kill();
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(80, 300);
	}
}
