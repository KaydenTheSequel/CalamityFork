using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.StatDebuffs;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class EndoCooperBody : ModProjectile, ILocalizedModType, IModType
{
	private int AttackMode;

	private int LimbID;

	private int laserdirection = 1;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 6;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.minion = true;
		base.Projectile.minionSlots = 10f;
		base.Projectile.netImportant = true;
		base.Projectile.width = 90;
		base.Projectile.height = 90;
		base.Projectile.timeLeft = 18000;
		base.Projectile.timeLeft *= 5;
		base.Projectile.ignoreWater = true;
		base.Projectile.tileCollide = false;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.extraUpdates = 1;
		base.Projectile.coldDamage = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_020c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0217: Unknown result type (might be due to invalid IL or missing references)
		//IL_021d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0454: Unknown result type (might be due to invalid IL or missing references)
		//IL_045f: Unknown result type (might be due to invalid IL or missing references)
		//IL_04f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0503: Unknown result type (might be due to invalid IL or missing references)
		//IL_0479: Unknown result type (might be due to invalid IL or missing references)
		//IL_047e: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_054d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0555: Unknown result type (might be due to invalid IL or missing references)
		//IL_055a: Unknown result type (might be due to invalid IL or missing references)
		//IL_055f: Unknown result type (might be due to invalid IL or missing references)
		//IL_064a: Unknown result type (might be due to invalid IL or missing references)
		//IL_064f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0652: Unknown result type (might be due to invalid IL or missing references)
		//IL_0657: Unknown result type (might be due to invalid IL or missing references)
		//IL_0659: Unknown result type (might be due to invalid IL or missing references)
		//IL_0668: Unknown result type (might be due to invalid IL or missing references)
		//IL_066d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0672: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04de: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_05dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_05eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_05f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_05fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0606: Unknown result type (might be due to invalid IL or missing references)
		//IL_060b: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0592: Unknown result type (might be due to invalid IL or missing references)
		//IL_0597: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_05af: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_070a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0744: Unknown result type (might be due to invalid IL or missing references)
		//IL_077c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0780: Unknown result type (might be due to invalid IL or missing references)
		//IL_0785: Unknown result type (might be due to invalid IL or missing references)
		//IL_0793: Unknown result type (might be due to invalid IL or missing references)
		//IL_079d: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_07a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_07ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0913: Unknown result type (might be due to invalid IL or missing references)
		//IL_091e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0924: Unknown result type (might be due to invalid IL or missing references)
		//IL_092c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0931: Unknown result type (might be due to invalid IL or missing references)
		//IL_0936: Unknown result type (might be due to invalid IL or missing references)
		//IL_093f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0954: Unknown result type (might be due to invalid IL or missing references)
		//IL_095a: Unknown result type (might be due to invalid IL or missing references)
		//IL_095c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0961: Unknown result type (might be due to invalid IL or missing references)
		//IL_0984: Unknown result type (might be due to invalid IL or missing references)
		//IL_0989: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a35: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a3d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a42: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a47: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a56: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a5d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0a62: Unknown result type (might be due to invalid IL or missing references)
		if (Main.rand.NextBool(15))
		{
			int dusttype = (Main.rand.NextBool() ? 68 : 67);
			if (Main.rand.NextBool(4))
			{
				dusttype = 80;
			}
			int dust = Dust.NewDust(base.Projectile.position + base.Projectile.velocity, base.Projectile.width, base.Projectile.height, dusttype, base.Projectile.velocity.X * 0.5f, base.Projectile.velocity.Y * 0.5f, 50, default(Color), 0.6f);
			Main.dust[dust].noGravity = true;
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<EndoCooperBody>();
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		player.AddBuff(ModContent.BuffType<EndoCooperBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.endoCooper = false;
			}
			if (modPlayer.endoCooper)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			AttackMode = (int)base.Projectile.ai[0];
			LimbID = (int)base.Projectile.ai[1];
			base.Projectile.ai[0] = 0f;
			base.Projectile.ai[1] = 0f;
			base.Projectile.localAI[0]++;
			Vector2 dspeed = default(Vector2);
			for (int i = 0; i < 60; i++)
			{
				int dusttype2 = (Main.rand.NextBool() ? 68 : 67);
				if (Main.rand.NextBool(4))
				{
					dusttype2 = 80;
				}
				((Vector2)(ref dspeed))._002Ector(Main.rand.NextFloat(-7f, 7f), Main.rand.NextFloat(-7f, 7f));
				int dust2 = Dust.NewDust(base.Projectile.Center, 1, 1, dusttype2, dspeed.X, dspeed.Y, 50, default(Color), 0.8f);
				Main.dust[dust2].noGravity = true;
			}
		}
		base.Projectile.localAI[1] = AttackMode;
		float mindistance = 2000f;
		float longdistance = ((AttackMode != 2) ? 1300f : 1200f);
		float longestdistance = ((AttackMode != 2) ? 2600f : 2500f);
		float idledistance = ((AttackMode != 2) ? 600f : 400f);
		float chasespeed1 = 30f;
		float chasespeed2 = 18f;
		float firerate = 30f;
		Projectile limbs = Main.projectile[LimbID];
		switch (AttackMode)
		{
		case 0:
			chasespeed1 = 29f;
			chasespeed2 = 16f;
			firerate = 60f;
			break;
		case 1:
			chasespeed1 = 24f;
			chasespeed2 = 12f;
			firerate = 200f;
			break;
		case 2:
			chasespeed1 = 32f;
			chasespeed2 = 20f;
			firerate = 30f;
			break;
		case 3:
			chasespeed1 = 34f;
			chasespeed2 = 21f;
			firerate = 30f;
			break;
		}
		if (limbs.type != ModContent.ProjectileType<EndoCooperLimbs>() || !limbs.active)
		{
			base.Projectile.Kill();
		}
		base.Projectile.MinionAntiClump();
		bool accelerate = false;
		if (base.Projectile.ai[0] == 2f)
		{
			base.Projectile.ai[1]++;
			base.Projectile.extraUpdates = 2;
			if (base.Projectile.ai[1] > 30f)
			{
				base.Projectile.ai[1] = 1f;
				base.Projectile.ai[0] = 0f;
				base.Projectile.extraUpdates = 1;
				base.Projectile.numUpdates = 0;
				base.Projectile.netUpdate = true;
			}
			else
			{
				accelerate = true;
			}
		}
		if (accelerate)
		{
			return;
		}
		Vector2 objectivepos = base.Projectile.position;
		bool gotoenemy = false;
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float disttoobjective = Vector2.Distance(npc.Center, base.Projectile.Center);
				if (!gotoenemy && disttoobjective < mindistance)
				{
					mindistance = disttoobjective;
					objectivepos = npc.Center;
					gotoenemy = true;
				}
			}
		}
		if (!gotoenemy)
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC nPC2 = enumerator.Current;
				if (nPC2.CanBeChasedBy(base.Projectile))
				{
					float disttoobjective2 = Vector2.Distance(nPC2.Center, base.Projectile.Center);
					if (!gotoenemy && disttoobjective2 < mindistance)
					{
						mindistance = disttoobjective2;
						objectivepos = nPC2.Center;
						gotoenemy = true;
					}
				}
			}
		}
		float maxdisttoenemy = longdistance;
		if (gotoenemy)
		{
			maxdisttoenemy = longestdistance;
		}
		if (Vector2.Distance(player.Center, base.Projectile.Center) > maxdisttoenemy)
		{
			base.Projectile.ai[0] = 1f;
			base.Projectile.netUpdate = true;
		}
		if (gotoenemy && base.Projectile.ai[0] == 0f)
		{
			Vector2 speedtoenemy = objectivepos - base.Projectile.Center;
			float num2 = ((Vector2)(ref speedtoenemy)).Length();
			((Vector2)(ref speedtoenemy)).Normalize();
			float stopdistance = ((AttackMode == 3) ? 120f : 200f);
			if (num2 > stopdistance)
			{
				float scaleFactor2 = chasespeed1;
				speedtoenemy *= scaleFactor2;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + speedtoenemy) / 41f;
			}
			else
			{
				float scalefactor3 = chasespeed2;
				speedtoenemy *= 0f - scalefactor3;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + speedtoenemy) / 41f;
			}
		}
		else
		{
			bool gotoplayer = false;
			if (!gotoplayer)
			{
				gotoplayer = base.Projectile.ai[0] == 1f;
			}
			float speedtoplayer = 8f;
			if (gotoplayer)
			{
				speedtoplayer = 16f;
			}
			Vector2 center2 = base.Projectile.Center;
			Vector2 playerpos = player.Center - center2 + new Vector2(0f, -60f);
			float num3 = ((Vector2)(ref playerpos)).Length();
			if (num3 > 200f && speedtoplayer < 8f)
			{
				speedtoplayer = 10f;
			}
			if (((num3 < idledistance) & gotoplayer) && !Collision.SolidCollision(base.Projectile.position, base.Projectile.width, base.Projectile.height))
			{
				base.Projectile.ai[0] = 0f;
				base.Projectile.netUpdate = true;
			}
			if (num3 > 2700f)
			{
				base.Projectile.position.X = Main.player[base.Projectile.owner].Center.X - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = Main.player[base.Projectile.owner].Center.Y - (float)(base.Projectile.height / 2);
				base.Projectile.netUpdate = true;
			}
			if (num3 > 70f)
			{
				((Vector2)(ref playerpos)).Normalize();
				playerpos *= speedtoplayer;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + playerpos) / 41f;
			}
			else if (base.Projectile.velocity.X == 0f && base.Projectile.velocity.Y == 0f)
			{
				base.Projectile.velocity.X = -0.15f;
				base.Projectile.velocity.Y = -0.05f;
			}
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1] += Main.rand.Next(1, 4);
		}
		if (base.Projectile.ai[1] > firerate)
		{
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.ai[0] == 0f && ((base.Projectile.ai[1] == 0f) & gotoenemy) && mindistance < 600f)
		{
			base.Projectile.ai[1]++;
			if (Main.myPlayer == base.Projectile.owner)
			{
				switch (AttackMode)
				{
				case 0:
				{
					SoundEngine.PlaySound(in SoundID.Item15, base.Projectile.position);
					Vector2 aimlaser = objectivepos - base.Projectile.Center;
					((Vector2)(ref aimlaser)).Normalize();
					aimlaser = aimlaser.RotatedBy(MathHelper.ToRadians((float)(30 * -laserdirection)));
					float angularChange = 0.019198623f * (float)laserdirection;
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, aimlaser, ModContent.ProjectileType<EndoBeam>(), base.Projectile.damage, 0f, base.Projectile.owner, angularChange, base.Projectile.whoAmI);
					laserdirection *= -1;
					break;
				}
				case 1:
					if (limbs.ai[0] == 0f)
					{
						limbs.ai[0] = 1f;
					}
					else if (limbs.ai[0] == 2f)
					{
						limbs.ai[0] = 3f;
					}
					base.Projectile.netUpdate = true;
					break;
				case 2:
				{
					base.Projectile.ai[0] = 2f;
					Vector2 aimtoenemy = objectivepos - base.Projectile.Center;
					((Vector2)(ref aimtoenemy)).Normalize();
					base.Projectile.velocity = aimtoenemy * 18f;
					base.Projectile.netUpdate = true;
					break;
				}
				case 3:
					limbs.ai[0] = 4f;
					base.Projectile.netUpdate = true;
					break;
				}
			}
		}
		if (limbs.ai[0] == 2f && base.Projectile.ai[1] == 0f)
		{
			base.Projectile.ai[1]++;
			limbs.ai[0] = 3f;
			base.Projectile.netUpdate = true;
		}
		base.Projectile.spriteDirection = base.Projectile.direction;
		base.Projectile.rotation = base.Projectile.velocity.X * 0.07f;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		int dye = Main.player[base.Projectile.owner]?.cMinion ?? 0;
		CalamityUtils.DrawAfterimagesCentered(base.Projectile, ProjectileID.Sets.TrailingMode[base.Type], lightColor, 1, null, drawCentered: true, shrink: false, dye);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/EndoCooperBody_Glow", (AssetRequestMode)2).Value;
		Rectangle frame = default(Rectangle);
		((Rectangle)(ref frame))._002Ector(0, 0, texture.Width, texture.Height);
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, frame, Color.LightSkyBlue, base.Projectile.rotation, base.Projectile.Size / 2f, 1f, (SpriteEffects)0);
	}

	public override bool MinionContactDamage()
	{
		return AttackMode == 2;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<GlacialState>(), 60);
		target.AddBuff(ModContent.BuffType<Voidfrost>(), 180);
	}
}
