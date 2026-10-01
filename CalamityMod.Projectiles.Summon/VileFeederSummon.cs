using System;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon;

public class VileFeederSummon : ModProjectile, ILocalizedModType, IModType
{
	private bool spawnDust = true;

	private int eaterCooldown;

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
		base.Projectile.width = (base.Projectile.height = 32);
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
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 60;
	}

	public override void AI()
	{
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0073: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009e: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_0269: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		//IL_036c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_037b: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0462: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		if (this.spawnDust)
		{
			base.Projectile.ai[1] = -1f;
			int dustAmt = 36;
			for (int d = 0; d < dustAmt; d++)
			{
				Vector2 val = (Vector2.Normalize(base.Projectile.velocity) * new Vector2((float)base.Projectile.width / 2f, (float)base.Projectile.height) * 0.75f).RotatedBy((float)(d - (dustAmt / 2 - 1)) * ((float)Math.PI * 2f) / (float)dustAmt) + base.Projectile.Center;
				Vector2 dustVel = val - base.Projectile.Center;
				int dusty = Dust.NewDust(val + dustVel, 0, 0, 7, dustVel.X * 1.75f, dustVel.Y * 1.75f, 100, default(Color), 1.1f);
				Main.dust[dusty].noGravity = true;
				Main.dust[dusty].velocity = dustVel;
			}
			this.spawnDust = false;
		}
		bool num = base.Projectile.type == ModContent.ProjectileType<VileFeederSummon>();
		player.AddBuff(ModContent.BuffType<VileFeederBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.vileFeeder = false;
			}
			if (modPlayer.vileFeeder)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		if (eaterCooldown < 0)
		{
			eaterCooldown = 0;
		}
		if (base.Projectile.ai[0] != 3f)
		{
			if (eaterCooldown > 0)
			{
				eaterCooldown--;
			}
			base.Projectile.ChargingMinionAI(640f, 1100f, 2400f, 150f, 0, 40f, 8f, 4f, new Vector2(0f, -60f), 40f, 8f, tileVision: false, ignoreTilesWhenCharging: false);
			base.Projectile.frameCounter++;
			if (base.Projectile.frameCounter > 4)
			{
				base.Projectile.frame++;
				base.Projectile.frameCounter = 0;
			}
			if (base.Projectile.frame >= 3)
			{
				base.Projectile.frame = 0;
			}
			base.Projectile.rotation = base.Projectile.velocity.ToRotation() + MathHelper.ToRadians(270f);
			return;
		}
		base.Projectile.frame = 0;
		base.Projectile.extraUpdates = 0;
		bool breakAway = false;
		bool spawnDust = false;
		base.Projectile.localAI[0]++;
		if (base.Projectile.localAI[0] % 30f == 0f)
		{
			spawnDust = true;
		}
		int npcIndex = (int)base.Projectile.ai[1];
		if (base.Projectile.localAI[0] >= 600000f)
		{
			breakAway = true;
		}
		else if (!npcIndex.WithinBounds(Main.maxNPCs))
		{
			breakAway = true;
		}
		else if (Main.npc[npcIndex].active && !Main.npc[npcIndex].dontTakeDamage && Main.npc[npcIndex].defense < 9999)
		{
			base.Projectile.Center = Main.npc[npcIndex].Center - base.Projectile.velocity * 2f;
			base.Projectile.gfxOffY = Main.npc[npcIndex].gfxOffY;
			if (spawnDust)
			{
				Main.npc[npcIndex].HitEffect(0, 1.0);
			}
		}
		else
		{
			breakAway = true;
		}
		if (breakAway)
		{
			base.Projectile.ai[0] = 0f;
			base.Projectile.velocity.X = 0f;
			base.Projectile.velocity.Y = 0f;
		}
		if (base.Projectile.owner == Main.myPlayer)
		{
			if (eaterCooldown > 0)
			{
				eaterCooldown--;
			}
			if (eaterCooldown <= 0)
			{
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Main.rand.NextVector2Circular(7f, 7f), ModContent.ProjectileType<VileFeederProjectile>(), base.Projectile.damage, base.Projectile.knockBack, base.Projectile.owner);
				eaterCooldown = base.Projectile.localNPCHitCooldown;
			}
		}
	}

	public override bool MinionContactDamage()
	{
		return true;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		//IL_01e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_0245: Unknown result type (might be due to invalid IL or missing references)
		//IL_0250: Unknown result type (might be due to invalid IL or missing references)
		//IL_0255: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0264: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0301: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		Rectangle myRect = default(Rectangle);
		((Rectangle)(ref myRect))._002Ector((int)base.Projectile.position.X, (int)base.Projectile.position.Y, base.Projectile.width, base.Projectile.height);
		if (base.Projectile.owner != Main.myPlayer)
		{
			return;
		}
		for (int npcIndex = 0; npcIndex < Main.maxNPCs; npcIndex++)
		{
			NPC npc = Main.npc[npcIndex];
			if (!npc.active || npc.dontTakeDamage || npc.defense >= 9999 || !(npc.Calamity().DR < 0.99f) || ((!base.Projectile.friendly || (npc.friendly && (npc.type != 22 || base.Projectile.owner >= 255 || !player.killGuide) && (npc.type != 54 || base.Projectile.owner >= 255 || !player.killClothier))) && (!base.Projectile.hostile || !npc.friendly || npc.dontTakeDamageFromHostiles)) || (base.Projectile.owner >= 0 && npc.immune[base.Projectile.owner] != 0 && base.Projectile.maxPenetrate != 1) || (!npc.noTileCollide && base.Projectile.ownerHitCheck))
			{
				continue;
			}
			bool stickingToNPC;
			if (npc.type == 414)
			{
				Rectangle rect = npc.getRect();
				int num5 = 8;
				rect.X -= num5;
				rect.Y -= num5;
				rect.Width += num5 * 2;
				rect.Height += num5 * 2;
				stickingToNPC = base.Projectile.Colliding(myRect, rect);
			}
			else
			{
				stickingToNPC = base.Projectile.Colliding(myRect, npc.getRect());
			}
			if (!stickingToNPC)
			{
				continue;
			}
			if (npc.reflectsProjectiles && base.Projectile.CanBeReflected())
			{
				npc.ReflectProjectile(base.Projectile);
				break;
			}
			base.Projectile.ai[0] = 3f;
			base.Projectile.ai[1] = npcIndex;
			base.Projectile.velocity = (npc.Center - base.Projectile.Center) * 0.75f;
			base.Projectile.netUpdate = true;
			Point[] array2 = (Point[])(object)new Point[10];
			int projCount = 0;
			for (int projIndex = 0; projIndex < Main.maxProjectiles; projIndex++)
			{
				Projectile proj = Main.projectile[projIndex];
				if (projIndex != base.Projectile.whoAmI && proj.active && proj.owner == Main.myPlayer && proj.type == base.Projectile.type && proj.ai[0] == 3f && proj.ai[1] == (float)npcIndex)
				{
					array2[projCount++] = new Point(projIndex, proj.timeLeft);
					if (projCount >= array2.Length)
					{
						break;
					}
				}
			}
			if (projCount < array2.Length)
			{
				continue;
			}
			projCount = 0;
			for (int m = 1; m < array2.Length; m++)
			{
				if (array2[m].Y < array2[projCount].Y)
				{
					projCount = m;
				}
			}
			Main.projectile[array2[projCount].X].Kill();
		}
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
		{
			((Rectangle)(ref targetHitbox)).Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
		}
		return null;
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
		int framing = texture.Height / Main.projFrames[base.Type];
		int y6 = framing * base.Projectile.frame;
		Main.EntitySpriteDraw(texture, base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY), (Rectangle?)new Rectangle(0, y6, texture.Width, framing), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)framing / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		return false;
	}
}
