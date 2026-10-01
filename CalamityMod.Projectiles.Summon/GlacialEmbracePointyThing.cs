using System;
using System.IO;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class GlacialEmbracePointyThing : ModProjectile, ILocalizedModType, IModType
{
	public int recharging = -1;

	public bool circling = true;

	public bool circlingPlayer = true;

	public float floatyDistance = 90f;

	public NPC target;

	public new string LocalizationCategory => "Projectiles.Summon";

	private void homingAi()
	{
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.timeLeft > 240)
		{
			return;
		}
		if (target != null)
		{
			target.checkDead();
			if (target.life <= 0 || !target.active || !target.CanBeChasedBy(this))
			{
				target = null;
			}
		}
		if (target == null)
		{
			target = base.Projectile.Center.MinionHoming(1000f, Main.player[base.Projectile.owner]);
		}
		if (target != null)
		{
			float projVel = 40f;
			Vector2 targetDirection = base.Projectile.Center;
			float targetX = target.Center.X - targetDirection.X;
			float targetY = target.Center.Y - targetDirection.Y;
			float targetDist = (float)Math.Sqrt(targetX * targetX + targetY * targetY);
			if (targetDist < 100f)
			{
				projVel = 28f;
			}
			targetDist = projVel / targetDist;
			targetX *= targetDist;
			targetY *= targetDist;
			base.Projectile.velocity.X = (base.Projectile.velocity.X * 20f + targetX) / 21f;
			base.Projectile.velocity.Y = (base.Projectile.velocity.Y * 20f + targetY) / 21f;
		}
	}

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.CultistIsResistantTo[base.Type] = true;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 7;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 28;
		base.Projectile.height = 60;
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
		writer.Write(recharging);
		writer.Write(circling);
		writer.Write(circlingPlayer);
		writer.Write(floatyDistance);
		writer.Write((target == null) ? (-1) : target.whoAmI);
	}

	public override void ReceiveExtraAI(BinaryReader reader)
	{
		recharging = reader.ReadInt32();
		circling = reader.ReadBoolean();
		circlingPlayer = reader.ReadBoolean();
		floatyDistance = reader.ReadSingle();
		int targ = reader.ReadInt32();
		target = ((targ == -1) ? null : Main.npc[targ]);
	}

	private void dust(int dustAmt)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < dustAmt; i++)
		{
			Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 80, Main.rand.NextFloat(1f, 3f), Main.rand.NextFloat(1f, 3f), 0, Color.Cyan, Main.rand.NextFloat(0.5f, 1.5f));
		}
	}

	public override bool PreAI()
	{
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_014a: Unknown result type (might be due to invalid IL or missing references)
		if (recharging == -1)
		{
			recharging = ((base.Projectile.ai[1] == 0f) ? 210 : 0);
			dust(30);
		}
		if (base.Projectile.ai[1] == 1f && base.Projectile.timeLeft > 1000)
		{
			base.Projectile.ai[1] = 0f;
			base.Projectile.timeLeft = 250;
			circling = (circlingPlayer = false);
			base.Projectile.netUpdate = true;
		}
		else if (base.Projectile.ai[1] >= 2f && base.Projectile.timeLeft > 900)
		{
			target = base.Projectile.Center.MinionHoming(1000f, Main.player[base.Projectile.owner]);
			if (target != null)
			{
				base.Projectile.timeLeft = 669;
				base.Projectile.ai[1]++;
				circlingPlayer = false;
				float height = target.getRect().Height;
				float width = target.getRect().Width;
				floatyDistance = MathHelper.Min(((height > width) ? height : width) * 3f, (float)(Main.LogicCheckScreenWidth * Main.LogicCheckScreenHeight / 2));
				if (floatyDistance > (float)(Main.LogicCheckScreenWidth / 3))
				{
					floatyDistance = Main.LogicCheckScreenWidth / 3;
				}
				base.Projectile.penetrate = -1;
				base.Projectile.usesIDStaticNPCImmunity = true;
				base.Projectile.idStaticNPCHitCooldown = 4;
				base.Projectile.netUpdate = true;
			}
		}
		if (circlingPlayer)
		{
			ProjectileID.Sets.MinionSacrificable[base.Type] = true;
			if (base.Projectile.penetrate == 1)
			{
				base.Projectile.penetrate++;
			}
		}
		return true;
	}

	public override void AI()
	{
		//IL_0514: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0407: Unknown result type (might be due to invalid IL or missing references)
		//IL_0412: Unknown result type (might be due to invalid IL or missing references)
		//IL_0417: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0464: Unknown result type (might be due to invalid IL or missing references)
		//IL_0469: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0181: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_021f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0231: Unknown result type (might be due to invalid IL or missing references)
		//IL_0238: Unknown result type (might be due to invalid IL or missing references)
		//IL_023d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0242: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_030e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_0316: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_032b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0330: Unknown result type (might be due to invalid IL or missing references)
		//IL_0344: Unknown result type (might be due to invalid IL or missing references)
		//IL_0349: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (player.dead)
		{
			modPlayer.GlacialEmbrace = false;
		}
		if (!modPlayer.GlacialEmbrace)
		{
			base.Projectile.active = false;
			return;
		}
		if (circlingPlayer)
		{
			base.Projectile.minionSlots = 1f;
			base.Projectile.timeLeft = 2;
			if (!modPlayer.GlacialEmbrace && recharging > 0)
			{
				base.Projectile.Kill();
			}
		}
		if (circling && !circlingPlayer && target != null && (!target.active || target.life <= 0))
		{
			base.Projectile.Kill();
		}
		if (recharging > 0)
		{
			recharging--;
			if (recharging == 0)
			{
				dust(15);
				SoundStyle style = SoundID.Item30 with
				{
					Pitch = 0.2f
				};
				SoundEngine.PlaySound(in style, base.Projectile.position);
				base.Projectile.netUpdate = true;
			}
		}
		if (circling)
		{
			if (circling && !circlingPlayer && base.Projectile.timeLeft < 120)
			{
				recharging = 0;
				base.Projectile.usesIDStaticNPCImmunity = false;
				base.Projectile.penetrate = 1;
				if (target.getRect().Width <= target.getRect().Height)
				{
					target.getRect();
				}
				else
				{
					target.getRect();
				}
				if (base.Projectile.timeLeft > 60)
				{
					floatyDistance += 5f;
				}
				else
				{
					floatyDistance -= 10f;
				}
			}
			if (circlingPlayer)
			{
				float math = ((recharging == 0) ? 90f : ((float)((300 - recharging) / 3)));
				float regularDistance = ((math > 90f) ? 90f : math);
				base.Projectile.Center = player.Center + base.Projectile.ai[0].ToRotationVector2() * regularDistance + Vector2.UnitY * player.gfxOffY;
				base.Projectile.rotation = base.Projectile.ai[0] + (float)Math.Atan(90.0);
				base.Projectile.ai[0] -= MathHelper.ToRadians(4f);
				if (((recharging > 0) ? null : base.Projectile.Center.MinionHoming(800f, player)) != null && base.Projectile.owner == Main.myPlayer)
				{
					recharging = 180;
					Vector2 velocity = base.Projectile.ai[0].ToRotationVector2().RotatedBy(Math.Atan(0.0));
					((Vector2)(ref velocity)).Normalize();
					velocity *= 20f;
					int shard = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.position, velocity, base.Projectile.type, (int)((float)base.Projectile.damage * 1.05f), base.Projectile.knockBack, base.Projectile.owner, base.Projectile.ai[0], 1f);
					if (Main.projectile.IndexInRange(shard))
					{
						Main.projectile[shard].originalDamage = (int)((float)base.Projectile.originalDamage * 1.05f);
					}
				}
				base.Projectile.netUpdate = base.Projectile.owner == Main.myPlayer;
			}
			else
			{
				base.Projectile.Center = target.Center + base.Projectile.ai[0].ToRotationVector2() * floatyDistance;
				base.Projectile.rotation = base.Projectile.ai[0] + (float)Math.Atan(90.0);
				Vector2 vec = base.Projectile.rotation.ToRotationVector2() - target.Center;
				((Vector2)(ref vec)).Normalize();
				if (base.Projectile.timeLeft <= 120)
				{
					base.Projectile.rotation = ((base.Projectile.timeLeft <= 60) ? (base.Projectile.ai[0] - (float)Math.Atan(90.0)) : (base.Projectile.rotation - MathHelper.Distance(base.Projectile.rotation, 0f - base.Projectile.rotation) / 60f));
				}
				base.Projectile.ai[0] -= MathHelper.ToRadians(4f);
			}
		}
		else
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.Atan(90.0);
			homingAi();
		}
	}

	public override bool? CanDamage()
	{
		if (recharging > 0 || (!circlingPlayer && (!circling || (base.Projectile.timeLeft < 120 && base.Projectile.timeLeft > 45)) && circling) || base.Projectile.hide)
		{
			return false;
		}
		return null;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(324, 300);
		int circlers = 0;
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile p = enumerator.Current;
			if (p.owner == base.Projectile.owner && p.type == base.Projectile.type)
			{
				_ = (GlacialEmbracePointyThing)p.ModProjectile;
				if (p.ai[1] > 2f)
				{
					circlers += Main.rand.Next(1, 4);
				}
			}
		}
		circlers = (int)MathHelper.Min((float)Main.rand.Next(15, 21), (float)circlers);
		if (base.Projectile.ai[1] > 2f)
		{
			base.Projectile.ai[1]++;
		}
		if (base.Projectile.ai[1] >= 30f - (float)circlers && base.Projectile.timeLeft >= 120)
		{
			recharging = ((base.Projectile.timeLeft > 121) ? (base.Projectile.timeLeft - 121) : 0);
		}
		if (circling && target == this.target && base.Projectile.timeLeft < 60)
		{
			if (base.Projectile.timeLeft < 60)
			{
				base.Projectile.Kill();
			}
		}
		else if (circlingPlayer)
		{
			recharging = 300;
			dust(20);
		}
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		if (circling && target == this.target && base.Projectile.timeLeft < 60)
		{
			dust(30);
			SoundEngine.PlaySound(in SoundID.NPCHit5, base.Projectile.position);
			modifiers.SourceDamage *= 1.1f;
		}
		else if (circling && target == this.target && base.Projectile.timeLeft > 60)
		{
			dust(5);
			modifiers.SourceDamage *= 0.2f;
		}
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(324, 300);
		if (circlingPlayer)
		{
			recharging = 300;
			dust(20);
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCHit5, base.Projectile.position);
		dust(20);
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		return new Color((recharging > 0) ? ((Color)(ref lightColor)).R : 53, (recharging > 0) ? ((Color)(ref lightColor)).G : Main.DiscoG, (int)((recharging > 0) ? ((Color)(ref lightColor)).B : byte.MaxValue), (recharging > 200) ? 255 : (255 - recharging));
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		if (!circling || (!circlingPlayer && recharging == 0))
		{
			CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, (!circlingPlayer) ? 1 : 3);
		}
		return true;
	}
}
