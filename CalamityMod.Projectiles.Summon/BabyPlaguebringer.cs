using System;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class BabyPlaguebringer : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/Pets/PlaguebringerBab";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 56;
		base.Projectile.height = 46;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 15;
		base.Projectile.MaxUpdates = 2;
		base.Projectile.timeLeft = 360;
		base.Projectile.penetrate = 3;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_0281: Unknown result type (might be due to invalid IL or missing references)
		//IL_028c: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02de: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cd: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0347: Unknown result type (might be due to invalid IL or missing references)
		//IL_034e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0353: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0370: Unknown result type (might be due to invalid IL or missing references)
		//IL_0372: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0381: Unknown result type (might be due to invalid IL or missing references)
		//IL_0306: Unknown result type (might be due to invalid IL or missing references)
		//IL_030a: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_031d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0327: Unknown result type (might be due to invalid IL or missing references)
		//IL_032c: Unknown result type (might be due to invalid IL or missing references)
		//IL_032e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0338: Unknown result type (might be due to invalid IL or missing references)
		//IL_033d: Unknown result type (might be due to invalid IL or missing references)
		//IL_025b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0260: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0483: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_050c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0516: Unknown result type (might be due to invalid IL or missing references)
		//IL_051b: Unknown result type (might be due to invalid IL or missing references)
		//IL_051d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0527: Unknown result type (might be due to invalid IL or missing references)
		//IL_052c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_06e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f4: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if ((double)Math.Abs(base.Projectile.velocity.X) > 0.2)
		{
			base.Projectile.spriteDirection = -base.Projectile.direction;
		}
		float maxTargetDist = 800f;
		float lights = (float)Main.rand.Next(90, 111) * 0.01f;
		lights *= Main.essScale;
		Lighting.AddLight(base.Projectile.Center, 0f * lights, 0.75f * lights, 0f * lights);
		bool decelerate = false;
		if (base.Projectile.ai[0] == 2f)
		{
			base.Projectile.ai[1]++;
			base.Projectile.extraUpdates = 1;
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter > 3)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame > 5 || base.Projectile.frame < 4)
			{
				base.Projectile.frame = 4;
			}
			if (base.Projectile.ai[1] > 30f)
			{
				base.Projectile.ai[1] = 1f;
				base.Projectile.ai[0] = 0f;
				base.Projectile.extraUpdates = 0;
				base.Projectile.numUpdates = 0;
				base.Projectile.netUpdate = true;
			}
			else
			{
				decelerate = true;
			}
		}
		if (decelerate)
		{
			return;
		}
		Vector2 projPos = base.Projectile.position;
		bool canAttack = false;
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float targetDist = Vector2.Distance(npc.Center, base.Projectile.Center);
				if (!canAttack && targetDist < maxTargetDist)
				{
					maxTargetDist = targetDist;
					projPos = npc.Center;
					canAttack = true;
				}
			}
		}
		if (!canAttack)
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC nPC2 = enumerator.Current;
				if (nPC2.CanBeChasedBy(base.Projectile))
				{
					float targetDist2 = Vector2.Distance(nPC2.Center, base.Projectile.Center);
					if (!canAttack && targetDist2 < maxTargetDist)
					{
						maxTargetDist = targetDist2;
						projPos = nPC2.Center;
						canAttack = true;
					}
				}
			}
		}
		float separationAnxietyRange = 1200f;
		if (canAttack)
		{
			separationAnxietyRange = 3000f;
		}
		if (Vector2.Distance(player.Center, base.Projectile.Center) > separationAnxietyRange)
		{
			base.Projectile.ai[0] = 1f;
			base.Projectile.netUpdate = true;
		}
		if (canAttack && base.Projectile.ai[0] == 0f)
		{
			Vector2 projDirection = projPos - base.Projectile.Center;
			float num = ((Vector2)(ref projDirection)).Length();
			((Vector2)(ref projDirection)).Normalize();
			if (num > 200f)
			{
				float scaleFactor2 = 8f;
				projDirection *= scaleFactor2;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + projDirection) / 41f;
			}
			else
			{
				projDirection *= -4f;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + projDirection) / 41f;
			}
		}
		else
		{
			bool isReturning = false;
			if (!isReturning)
			{
				isReturning = base.Projectile.ai[0] == 1f;
			}
			float returnSpeed = 6f;
			if (isReturning)
			{
				returnSpeed = 15f;
			}
			Vector2 center2 = base.Projectile.Center;
			Vector2 playerDirection = player.Center - center2 + new Vector2(0f, -60f);
			float num2 = ((Vector2)(ref playerDirection)).Length();
			if (num2 > 200f && returnSpeed < 8f)
			{
				returnSpeed = 8f;
			}
			if (((num2 < 500f) & isReturning) && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
			{
				base.Projectile.ai[0] = 0f;
				base.Projectile.netUpdate = true;
			}
			if (num2 > 2000f)
			{
				base.Projectile.position.X = Main.player[base.Projectile.owner].Center.X - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = Main.player[base.Projectile.owner].Center.Y - (float)(base.Projectile.height / 2);
				base.Projectile.netUpdate = true;
			}
			if (num2 > 70f)
			{
				((Vector2)(ref playerDirection)).Normalize();
				playerDirection *= returnSpeed;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + playerDirection) / 41f;
			}
			else if (base.Projectile.velocity.X == 0f && base.Projectile.velocity.Y == 0f)
			{
				base.Projectile.velocity.X = -0.15f;
				base.Projectile.velocity.Y = -0.05f;
			}
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter > 12)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= 4)
		{
			base.Projectile.frame = 0;
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1] += Main.rand.Next(1, 4);
		}
		if (base.Projectile.ai[1] > 40f)
		{
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.ai[0] == 0f && ((base.Projectile.ai[1] == 0f) & canAttack) && maxTargetDist < 500f)
		{
			base.Projectile.ai[1]++;
			if (Main.myPlayer == base.Projectile.owner)
			{
				base.Projectile.ai[0] = 2f;
				Vector2 npcCenter = projPos - base.Projectile.Center;
				((Vector2)(ref npcCenter)).Normalize();
				base.Projectile.velocity = npcCenter * 8f;
				base.Projectile.netUpdate = true;
			}
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.NPCHit4, base.Projectile.position);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0074: Unknown result type (might be due to invalid IL or missing references)
		//IL_0089: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		SpriteEffects spriteEffects = (SpriteEffects)(base.Projectile.spriteDirection == -1);
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int framing = texture.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)texture.Width / 2f, (float)framing / 2f);
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, spriteEffects, 0f);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 120);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<Plague>(), 120);
	}
}
