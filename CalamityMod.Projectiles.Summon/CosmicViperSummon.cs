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

public class CosmicViperSummon : ModProjectile, ILocalizedModType, IModType
{
	public static Item FalseGun;

	public static Item CosmicViper;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 3;
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 48;
		base.Projectile.height = 48;
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 1f;
		base.Projectile.timeLeft = 18000;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.timeLeft *= 5;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	private static void DefineFalseGun(int baseDamage)
	{
		int p90ID = ModContent.ItemType<P90>();
		int CVEID = ModContent.ItemType<CosmicViperEngine>();
		FalseGun = new Item();
		CosmicViper = new Item();
		FalseGun.SetDefaults(p90ID, noMatCheck: true);
		CosmicViper.SetDefaults(CVEID, noMatCheck: true);
		FalseGun.damage = baseDamage;
		FalseGun.knockBack = CosmicViper.knockBack;
		FalseGun.shootSpeed = CosmicViper.shootSpeed;
		FalseGun.consumeAmmoOnFirstShotOnly = false;
		FalseGun.consumeAmmoOnLastShotOnly = false;
		FalseGun.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0215: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025e: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0082: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00be: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_037c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0387: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_02d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03de: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0328: Unknown result type (might be due to invalid IL or missing references)
		//IL_0333: Unknown result type (might be due to invalid IL or missing references)
		//IL_04c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_04d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e0: Unknown result type (might be due to invalid IL or missing references)
		//IL_04e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_04ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_044d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0452: Unknown result type (might be due to invalid IL or missing references)
		//IL_0460: Unknown result type (might be due to invalid IL or missing references)
		//IL_046a: Unknown result type (might be due to invalid IL or missing references)
		//IL_046f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0471: Unknown result type (might be due to invalid IL or missing references)
		//IL_047b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0480: Unknown result type (might be due to invalid IL or missing references)
		//IL_0401: Unknown result type (might be due to invalid IL or missing references)
		//IL_0405: Unknown result type (might be due to invalid IL or missing references)
		//IL_040a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0418: Unknown result type (might be due to invalid IL or missing references)
		//IL_0422: Unknown result type (might be due to invalid IL or missing references)
		//IL_0427: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_0433: Unknown result type (might be due to invalid IL or missing references)
		//IL_0438: Unknown result type (might be due to invalid IL or missing references)
		//IL_0350: Unknown result type (might be due to invalid IL or missing references)
		//IL_0355: Unknown result type (might be due to invalid IL or missing references)
		//IL_0644: Unknown result type (might be due to invalid IL or missing references)
		//IL_0617: Unknown result type (might be due to invalid IL or missing references)
		//IL_0542: Unknown result type (might be due to invalid IL or missing references)
		//IL_0566: Unknown result type (might be due to invalid IL or missing references)
		//IL_056a: Unknown result type (might be due to invalid IL or missing references)
		//IL_056f: Unknown result type (might be due to invalid IL or missing references)
		//IL_057d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0587: Unknown result type (might be due to invalid IL or missing references)
		//IL_058c: Unknown result type (might be due to invalid IL or missing references)
		//IL_058e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0598: Unknown result type (might be due to invalid IL or missing references)
		//IL_059d: Unknown result type (might be due to invalid IL or missing references)
		//IL_06a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_06d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_06f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0717: Unknown result type (might be due to invalid IL or missing references)
		//IL_072b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0735: Unknown result type (might be due to invalid IL or missing references)
		//IL_073a: Unknown result type (might be due to invalid IL or missing references)
		//IL_07e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_07f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_08d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_08e3: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_08ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0996: Unknown result type (might be due to invalid IL or missing references)
		//IL_099b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0954: Unknown result type (might be due to invalid IL or missing references)
		//IL_0959: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (base.Projectile.localAI[0] == 0f)
		{
			int dustAmt = 36;
			for (int dustIndex = 0; dustIndex < dustAmt; dustIndex++)
			{
				int dustType = (Main.rand.NextBool(3) ? 56 : 242);
				Vector2 faceDirection = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)dustIndex * ((float)Math.PI * 2f) / (float)dustAmt) + base.Projectile.Center - base.Projectile.Center;
				Dust.NewDustPerfect(base.Projectile.Center, dustType, faceDirection, 100, default(Color), 1.1f).noGravity = true;
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
		if (base.Projectile.frame >= 3)
		{
			base.Projectile.frame = 0;
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<CosmicViperSummon>();
		player.AddBuff(ModContent.BuffType<CosmicViperEngineBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.cosmicViper = false;
			}
			if (modPlayer.cosmicViper)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		float colorScale = (float)base.Projectile.alpha / 255f;
		Lighting.AddLight((int)base.Projectile.Center.X / 16, (int)base.Projectile.Center.Y / 16, 1f * colorScale, 0.1f * colorScale, 1f * colorScale);
		base.Projectile.MinionAntiClump();
		float detectRange = 2200f;
		Vector2 targetVec = base.Projectile.position;
		bool foundTarget = false;
		if (player.HasMinionAttackTargetNPC && player.HasAmmo(FalseGun))
		{
			NPC npc = Main.npc[player.MinionAttackTargetNPC];
			if (npc.CanBeChasedBy(base.Projectile))
			{
				float extraDist = npc.width / 2 + npc.height / 2;
				if (Vector2.Distance(npc.Center, base.Projectile.Center) < detectRange + extraDist)
				{
					targetVec = npc.Center;
					foundTarget = true;
					_ = npc.whoAmI;
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
					float targetDist = Vector2.Distance(npc2.Center, base.Projectile.Center);
					if (!foundTarget && targetDist < detectRange + extraDist2)
					{
						detectRange = targetDist;
						targetVec = npc2.Center;
						foundTarget = true;
					}
				}
			}
		}
		float returnDist = 1300f;
		if (foundTarget)
		{
			returnDist = 2600f;
		}
		if (Vector2.Distance(player.Center, base.Projectile.Center) > returnDist)
		{
			base.Projectile.ai[0] = 1f;
			base.Projectile.netUpdate = true;
		}
		if (foundTarget && base.Projectile.ai[0] == 0f)
		{
			Vector2 targetVector = targetVec - base.Projectile.Center;
			float num2 = ((Vector2)(ref targetVector)).Length();
			((Vector2)(ref targetVector)).Normalize();
			float speedMult = 30f;
			if (num2 > 200f)
			{
				targetVector *= speedMult;
				base.Projectile.velocity = (base.Projectile.velocity * 15f + targetVector) / 16f;
			}
			else
			{
				targetVector *= 0f - speedMult / 2f;
				base.Projectile.velocity = (base.Projectile.velocity * 15f + targetVector) / 16f;
			}
		}
		else
		{
			float safeDist = 600f;
			bool returnToPlayer = false;
			if (!returnToPlayer)
			{
				returnToPlayer = base.Projectile.ai[0] == 1f;
			}
			float velocityMult = 12f;
			if (returnToPlayer)
			{
				velocityMult = 30f;
			}
			Vector2 playerVec = player.Center - base.Projectile.Center + new Vector2(0f, -120f);
			float num3 = ((Vector2)(ref playerVec)).Length();
			if (num3 > 200f && velocityMult < 16f)
			{
				velocityMult = 16f;
			}
			if ((num3 < safeDist) & returnToPlayer)
			{
				base.Projectile.ai[0] = 0f;
				base.Projectile.netUpdate = true;
			}
			if (num3 > 2000f)
			{
				base.Projectile.Center = player.Center;
				base.Projectile.netUpdate = true;
			}
			if (num3 > 70f)
			{
				((Vector2)(ref playerVec)).Normalize();
				playerVec *= velocityMult;
				base.Projectile.velocity = (base.Projectile.velocity * 40f + playerVec) / 41f;
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
			int dustType2 = (Main.rand.NextBool(3) ? 56 : 242);
			float xVelOffset = base.Projectile.velocity.X / 3f;
			float yVelOffset = base.Projectile.velocity.Y / 3f;
			int trail = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, dustType2);
			Dust obj = Main.dust[trail];
			obj.position.X = base.Projectile.Center.X - xVelOffset;
			obj.position.Y = base.Projectile.Center.Y - yVelOffset;
			obj.velocity *= 0f;
			obj.scale = 0.5f;
		}
		if (base.Projectile.ai[1] > 0f)
		{
			base.Projectile.ai[1]++;
		}
		if (base.Projectile.ai[1] > 60f)
		{
			base.Projectile.ai[1] = 0f;
			base.Projectile.netUpdate = true;
		}
		if (base.Projectile.ai[0] != 0f || !foundTarget || base.Projectile.ai[1] != 0f)
		{
			return;
		}
		SoundEngine.PlaySound(in SoundID.Item20, base.Projectile.Center);
		base.Projectile.ai[1] += 2f;
		if (Main.myPlayer != base.Projectile.owner)
		{
			return;
		}
		bool shootRocket = ++base.Projectile.localAI[1] % 2f == 0f;
		int projType = ModContent.ProjectileType<CosmicViperSplittingRocket>();
		float num4 = base.Projectile.localAI[1] % 3f;
		if (num4 != 0f)
		{
			if (num4 != 1f)
			{
				if (num4 == 2f)
				{
					projType = ModContent.ProjectileType<CosmicViperConcussionMissile>();
				}
			}
			else
			{
				projType = ModContent.ProjectileType<CosmicViperHomingRocket>();
			}
		}
		else
		{
			projType = ModContent.ProjectileType<CosmicViperSplittingRocket>();
		}
		bool dontConsumeAmmo = Main.rand.NextBool() | shootRocket;
		player.PickAmmo(FalseGun, out var projID, out var shootSpeed, out var damage, out var kb, out var _, dontConsumeAmmo);
		Vector2 velocity = base.Projectile.SafeDirectionTo(targetVec) * shootSpeed;
		int projIndex;
		if (shootRocket)
		{
			velocity.Y += Main.rand.NextFloat(-15f, 15f) * 0.05f;
			velocity.X += Main.rand.NextFloat(-15f, 15f) * 0.05f;
			projIndex = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, projType, damage, kb, base.Projectile.owner);
		}
		else
		{
			projIndex = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, projID, damage, kb, base.Projectile.owner);
		}
		if (projIndex.WithinBounds(Main.maxProjectiles))
		{
			Main.projectile[projIndex].DamageType = DamageClass.Summon;
			Main.projectile[projIndex].minion = false;
		}
		base.Projectile.netUpdate = true;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		int frameHeight = texture.Height / Main.projFrames[base.Type];
		int y6 = frameHeight * base.Projectile.frame;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, y6, texture.Width, frameHeight), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)frameHeight / 2f), base.Projectile.scale, spriteEffects, 0f);
		return false;
	}

	public override void PostDraw(Color lightColor)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0051: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		Texture2D texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/CosmicViperGlow", (AssetRequestMode)2).Value;
		int frameHeight = texture.Height / Main.projFrames[base.Type];
		int y6 = frameHeight * base.Projectile.frame;
		SpriteEffects spriteEffects = (SpriteEffects)0;
		if (base.Projectile.spriteDirection == -1)
		{
			spriteEffects = (SpriteEffects)1;
		}
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, y6, texture.Width, frameHeight), Color.White, base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)frameHeight / 2f), base.Projectile.scale, spriteEffects, 0f);
	}
}
