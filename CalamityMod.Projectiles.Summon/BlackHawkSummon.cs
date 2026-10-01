using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using CalamityMod.Items.Weapons.Ranged;
using CalamityMod.Items.Weapons.Summon;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class BlackHawkSummon : ModProjectile, ILocalizedModType, IModType
{
	public static Item FalseGun;

	public static Item BlackHawk;

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
		base.Projectile.width = (base.Projectile.height = 32);
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.aiStyle = 66;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = true;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	private static void DefineFalseGun(int baseDamage)
	{
		int p90ID = ModContent.ItemType<P90>();
		int BHRID = ModContent.ItemType<BlackHawkRemote>();
		FalseGun = new Item();
		BlackHawk = new Item();
		FalseGun.SetDefaults(p90ID, noMatCheck: true);
		BlackHawk.SetDefaults(BHRID, noMatCheck: true);
		FalseGun.damage = baseDamage;
		FalseGun.knockBack = BlackHawk.knockBack;
		FalseGun.shootSpeed = BlackHawk.shootSpeed;
		FalseGun.consumeAmmoOnFirstShotOnly = false;
		FalseGun.consumeAmmoOnLastShotOnly = false;
		FalseGun.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00da: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0241: Unknown result type (might be due to invalid IL or missing references)
		//IL_0246: Unknown result type (might be due to invalid IL or missing references)
		//IL_0364: Unknown result type (might be due to invalid IL or missing references)
		//IL_036f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0295: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0311: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_04b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_0430: Unknown result type (might be due to invalid IL or missing references)
		//IL_0434: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_0451: Unknown result type (might be due to invalid IL or missing references)
		//IL_0456: Unknown result type (might be due to invalid IL or missing references)
		//IL_0458: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_03f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0409: Unknown result type (might be due to invalid IL or missing references)
		//IL_040e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0410: Unknown result type (might be due to invalid IL or missing references)
		//IL_041a: Unknown result type (might be due to invalid IL or missing references)
		//IL_041f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0339: Unknown result type (might be due to invalid IL or missing references)
		//IL_033e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0666: Unknown result type (might be due to invalid IL or missing references)
		//IL_063d: Unknown result type (might be due to invalid IL or missing references)
		//IL_052a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0554: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0590: Unknown result type (might be due to invalid IL or missing references)
		//IL_0595: Unknown result type (might be due to invalid IL or missing references)
		//IL_05a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_05ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_05b4: Unknown result type (might be due to invalid IL or missing references)
		//IL_05be: Unknown result type (might be due to invalid IL or missing references)
		//IL_05c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_072f: Unknown result type (might be due to invalid IL or missing references)
		//IL_073a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0784: Unknown result type (might be due to invalid IL or missing references)
		//IL_078f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0796: Unknown result type (might be due to invalid IL or missing references)
		//IL_079b: Unknown result type (might be due to invalid IL or missing references)
		//IL_07b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07be: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		base.Projectile.Calamity();
		if (base.Projectile.localAI[0] == 0f)
		{
			int dustAmt = 36;
			for (int dustIndex = 0; dustIndex < dustAmt; dustIndex++)
			{
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy(((float)dustIndex - ((float)dustAmt / 2f - 1f)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.Projectile.Center;
				Vector2 dustVel = val - base.Projectile.Center;
				int fire = Dust.NewDust(val + dustVel, 0, 0, 258, dustVel.X * 1.75f, dustVel.Y * 1.75f, 100, default(Color), 1.1f);
				Main.dust[fire].noGravity = true;
				Main.dust[fire].velocity = dustVel;
			}
			if (FalseGun == null)
			{
				DefineFalseGun(base.Projectile.originalDamage);
			}
			base.Projectile.localAI[0]++;
		}
		base.Projectile.frameCounter++;
		if (base.Projectile.frameCounter >= 4)
		{
			base.Projectile.frame++;
			base.Projectile.frameCounter = 0;
		}
		if (base.Projectile.frame >= Main.projFrames[base.Type])
		{
			base.Projectile.frame = 0;
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<BlackHawkSummon>();
		player.AddBuff(ModContent.BuffType<BlackHawkBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.blackhawk = false;
			}
			if (modPlayer.blackhawk)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		base.Projectile.MinionAntiClump();
		float maxDistance = 700f;
		Vector2 targetVec = base.Projectile.position;
		bool foundTarget = false;
		if (player.HasMinionAttackTargetNPC && player.HasAmmo(FalseGun))
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float extraDist = npc.width / 2 + npc.height / 2;
				float targetDist = Vector2.Distance(npc.Center, base.Projectile.Center);
				if (!foundTarget && targetDist < maxDistance + extraDist)
				{
					maxDistance = targetDist;
					targetVec = npc.Center;
					foundTarget = true;
				}
			}
		}
		if (!foundTarget && player.HasAmmo(FalseGun))
		{
			for (int npcIndex = 0; npcIndex < Main.maxNPCs; npcIndex++)
			{
				NPC npc2 = Main.npc[npcIndex];
				if (npc2.CanBeChasedBy(base.Projectile))
				{
					float extraDist2 = npc2.width / 2 + npc2.height / 2;
					float targetDist2 = Vector2.Distance(npc2.Center, base.Projectile.Center);
					if (!foundTarget && targetDist2 < maxDistance + extraDist2)
					{
						maxDistance = targetDist2;
						targetVec = npc2.Center;
						foundTarget = true;
					}
				}
			}
		}
		float separationAnxietyDist = 1300f;
		if (foundTarget)
		{
			separationAnxietyDist = 2600f;
		}
		if (Vector2.Distance(player.Center, base.Projectile.Center) > separationAnxietyDist)
		{
			base.Projectile.ai[0] = 1f;
			base.Projectile.netUpdate = true;
		}
		if (foundTarget && base.Projectile.ai[0] == 0f)
		{
			Vector2 vecToTarget = targetVec - base.Projectile.Center;
			float num2 = ((Vector2)(ref vecToTarget)).Length();
			((Vector2)(ref vecToTarget)).Normalize();
			if (num2 > 200f)
			{
				float speedMult = 18f;
				vecToTarget *= speedMult;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + vecToTarget) / 41f;
			}
			else
			{
				float speedMult2 = -9f;
				vecToTarget *= speedMult2;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + vecToTarget) / 41f;
			}
		}
		else
		{
			bool returningToPlayer = false;
			if (!returningToPlayer)
			{
				returningToPlayer = base.Projectile.ai[0] == 1f;
			}
			float speedMult3 = 12f;
			if (returningToPlayer)
			{
				speedMult3 = 30f;
			}
			Vector2 vecToPlayer = player.Center - base.Projectile.Center + new Vector2(0f, -120f);
			float num3 = ((Vector2)(ref vecToPlayer)).Length();
			if (num3 < 200f && speedMult3 < 16f)
			{
				speedMult3 = 16f;
			}
			if ((num3 < 600f) & returningToPlayer)
			{
				base.Projectile.ai[0] = 0f;
				base.Projectile.netUpdate = true;
			}
			if (num3 > 2000f)
			{
				base.Projectile.position.X = player.Center.X - (float)(base.Projectile.width / 2);
				base.Projectile.position.Y = player.Center.Y - (float)(base.Projectile.height / 2);
				base.Projectile.netUpdate = true;
			}
			if (num3 > 70f)
			{
				((Vector2)(ref vecToPlayer)).Normalize();
				vecToPlayer *= speedMult3;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + vecToPlayer) / 41f;
			}
			else if (base.Projectile.velocity.X == 0f && base.Projectile.velocity.Y == 0f)
			{
				base.Projectile.velocity.X = -0.15f;
				base.Projectile.velocity.Y = -0.05f;
			}
		}
		if (foundTarget)
		{
			base.Projectile.rotation = base.Projectile.rotation.AngleTowards(base.Projectile.AngleTo(targetVec) + (float)Math.PI, 0.1f);
		}
		else
		{
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + (float)Math.PI;
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1]++;
		}
		if (base.Projectile.ai[1] > 85f)
		{
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.ai[0] == 0f && foundTarget && base.Projectile.ai[1] == 0f && Main.myPlayer == base.Projectile.owner)
		{
			int projType = ModContent.ProjectileType<BlackHawkBullet>();
			SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
			base.Projectile.ai[1] += 2f;
			bool dontConsumeAmmo = Main.rand.NextBool();
			player.PickAmmo(FalseGun, out var projID, out var shootSpeed, out var damage, out var kb, out var _, dontConsumeAmmo);
			Vector2 velocity = base.Projectile.SafeDirectionTo(targetVec) * shootSpeed;
			if (projID == 14)
			{
				projID = projType;
			}
			int projIndex = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, projID, damage, kb, base.Projectile.owner);
			if (projIndex.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[projIndex].DamageType = DamageClass.Summon;
				Main.projectile[projIndex].minion = false;
			}
			base.Projectile.netUpdate = true;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0068: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = TextureAssets.Projectile[base.Type].Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(effects: (SpriteEffects)(base.Projectile.spriteDirection == -1), texture: value, position: base.Projectile.Center - Main.screenPosition, sourceRectangle: frame, color: base.Projectile.GetAlpha(lightColor), rotation: base.Projectile.rotation, origin: frame.Size() * 0.5f, scale: base.Projectile.scale);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0050: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_008b: Unknown result type (might be due to invalid IL or missing references)
		Texture2D value = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/BlackHawkGlow", (AssetRequestMode)2).Value;
		Rectangle frame = value.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Main.EntitySpriteDraw(effects: (SpriteEffects)(base.Projectile.spriteDirection == -1), texture: value, position: base.Projectile.Center - Main.screenPosition, sourceRectangle: frame, color: Color.White, rotation: base.Projectile.rotation, origin: frame.Size() * 0.5f, scale: base.Projectile.scale);
	}
}
