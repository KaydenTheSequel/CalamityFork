using System;
using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Dusts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class AstrageldonSummon : ModProjectile, ILocalizedModType, IModType
{
	public bool dust;

	private int attackCounter = 1;

	private int teleportCounter = 400;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 6;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 64;
		base.Projectile.height = 62;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.alpha = 75;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.tileCollide = false;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
		base.Projectile.aiStyle = 26;
		base.AIType = 266;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		fallThrough = false;
		return true;
	}

	public override void AI()
	{
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_013a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0153: Unknown result type (might be due to invalid IL or missing references)
		//IL_0164: Unknown result type (might be due to invalid IL or missing references)
		//IL_016a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0276: Unknown result type (might be due to invalid IL or missing references)
		//IL_0293: Unknown result type (might be due to invalid IL or missing references)
		//IL_02af: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_0324: Unknown result type (might be due to invalid IL or missing references)
		//IL_0341: Unknown result type (might be due to invalid IL or missing references)
		//IL_035d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0368: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_02e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_038c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0391: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0492: Unknown result type (might be due to invalid IL or missing references)
		//IL_0498: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_055e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0599: Unknown result type (might be due to invalid IL or missing references)
		//IL_0690: Unknown result type (might be due to invalid IL or missing references)
		//IL_069c: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_06bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_06c1: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		base.Projectile.Calamity();
		float scale = (float)Math.Log(base.Projectile.minionSlots, 10.0) + 1f;
		if (base.Projectile.scale != scale)
		{
			base.Projectile.scale = scale;
		}
		base.Projectile.width = (int)(64f * base.Projectile.scale);
		base.Projectile.height = (int)(62f * base.Projectile.scale);
		if (!dust)
		{
			int dustAmt = 16;
			for (int dustIndex = 0; dustIndex < dustAmt; dustIndex++)
			{
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(dustIndex - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.Projectile.Center;
				Vector2 faceDirection = val - base.Projectile.Center;
				int dusty = Dust.NewDust(val + faceDirection, 0, 0, ModContent.DustType<AstralOrange>(), faceDirection.X * 1f, faceDirection.Y * 1f, 100, default(Color), 1.1f);
				Main.dust[dusty].noGravity = true;
				Main.dust[dusty].noLight = true;
				Main.dust[dusty].velocity = faceDirection;
			}
			dust = true;
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<AstrageldonSummon>();
		player.AddBuff(ModContent.BuffType<AbandonedSlimeBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.aSlime = false;
			}
			if (modPlayer.aSlime)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		if (base.Projectile.frame != 0 && base.Projectile.frame != 1)
		{
			return;
		}
		float mindistance = 1000f;
		float longdistance = 2000f;
		float longestdistance = 3000f;
		Vector2 objectivepos = base.Projectile.position;
		bool gotoenemy = false;
		if (player.HasMinionAttackTargetNPC)
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				bool lineOfSight = Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, npc.position, npc.width, npc.height);
				float disttoobjective = Vector2.Distance(npc.Center, base.Projectile.Center);
				if ((!gotoenemy && disttoobjective < mindistance) & lineOfSight)
				{
					mindistance = disttoobjective;
					objectivepos = npc.Center;
					gotoenemy = true;
				}
			}
		}
		else
		{
			ActiveEntityIterator<NPC>.Enumerator enumerator = Main.ActiveNPCs.GetEnumerator();
			while (enumerator.MoveNext())
			{
				NPC npc2 = enumerator.Current;
				if (npc2.CanBeChasedBy(base.Projectile))
				{
					bool lineOfSight2 = Collision.CanHit(base.Projectile.position, base.Projectile.width, base.Projectile.height, npc2.position, npc2.width, npc2.height);
					float disttoobjective2 = Vector2.Distance(npc2.Center, base.Projectile.Center);
					if ((!gotoenemy && disttoobjective2 < mindistance) & lineOfSight2)
					{
						mindistance = disttoobjective2;
						objectivepos = npc2.Center;
						gotoenemy = true;
					}
				}
			}
		}
		if (gotoenemy)
		{
			float teleportRange = ((Vector2)(ref objectivepos)).Length();
			float scaleAddition = base.Projectile.scale * 5f;
			if (teleportCounter <= 0 && teleportRange >= 800f)
			{
				for (int counter = 0; (float)counter < 50f; counter++)
				{
					int dustType = Utils.SelectRandom<int>(Main.rand, ModContent.DustType<AstralBlue>(), ModContent.DustType<AstralOrange>());
					float rand1 = Main.rand.Next(-10, 11);
					float rand2 = Main.rand.Next(-10, 11);
					float num2 = Main.rand.Next(3, 9);
					float randAdjust = (float)Math.Sqrt(rand1 * rand1 + rand2 * rand2);
					randAdjust = num2 / randAdjust;
					rand1 *= randAdjust;
					rand2 *= randAdjust;
					int astralDust = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType, 0f, 0f, 100, default(Color), 2f);
					Dust obj = Main.dust[astralDust];
					obj.noGravity = true;
					obj.position.X = base.Projectile.Center.X;
					obj.position.Y = base.Projectile.Center.Y;
					obj.position.X += Main.rand.Next(-10, 11);
					obj.position.Y += Main.rand.Next(-10, 11);
					obj.velocity.X = rand1;
					obj.velocity.Y = rand2;
				}
				base.Projectile.position.X = objectivepos.X - (float)(base.Projectile.width / 2) + Main.rand.NextFloat(-100f, 100f);
				base.Projectile.position.Y = objectivepos.Y - (float)(base.Projectile.height / 2) - Main.rand.NextFloat(0f + scaleAddition, 200f + scaleAddition);
				base.Projectile.netUpdate = true;
				teleportCounter = 600;
			}
			if (teleportCounter > 0)
			{
				teleportCounter -= Main.rand.Next(1, 4);
			}
		}
		if (attackCounter > 0)
		{
			attackCounter += Main.rand.Next(1, 4);
		}
		if (attackCounter > 300)
		{
			attackCounter = 0;
			base.Projectile.netUpdate = true;
		}
		float laserSpeed = 6f;
		int projType = ModContent.ProjectileType<AstrageldonLaser>();
		if (gotoenemy && attackCounter == 0)
		{
			attackCounter += 2;
			if (Main.myPlayer == base.Projectile.owner)
			{
				Vector2 laserVel = base.Projectile.SafeDirectionTo(objectivepos) * laserSpeed;
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, laserVel, projType, base.Projectile.damage, 0f, base.Projectile.owner);
				base.Projectile.netUpdate = true;
			}
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<AstralInfectionDebuff>(), 120);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int height = texture.Height / Main.projFrames[base.Type];
		int y6 = height * base.Projectile.frame;
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture.Width, height), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)height / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}
}
